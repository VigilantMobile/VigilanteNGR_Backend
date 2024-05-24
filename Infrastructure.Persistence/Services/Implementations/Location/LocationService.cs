using Application.DTOs.Location;
using Application.Features.AppTroopers.SecurityTips;
using Application.Features.Location;
using Application.Interfaces;
using Application.Services.Interfaces.Location;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities.LocationEntities;
using Domain.Settings;
using Geocoding;
using Geocoding.Google;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Persistence.Services.Implementations.Location
{
    public class LocationService : ILocationService
    {
        //private readonly IGeocoder geocoder;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IUtilities _utilities;
        private readonly IGeoCodingService _geoCodingService;
        private readonly APIURLs _aPIURLs;
        private readonly AppConfig _appConfig;
        private readonly ApplicationDbContext _context;


        public LocationService(ILogger logger, IConfiguration configuration,
            IUtilities utilities, IGeoCodingService geoCodingService, IOptions<APIURLs> apiURLs, IOptions<AppConfig> appConfig, ApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _utilities = utilities;
            _aPIURLs = apiURLs.Value;
            _appConfig = appConfig.Value;
            _geoCodingService = geoCodingService;
            _context = context;
        }

        public async Task<Response<string>> SaveCustomerLocationInfo(string Coordinates)
        {
            try
            {
                if (string.IsNullOrEmpty(Coordinates))
                {
                    return new Response<string>(responsestatus: APIResponseStatus.fail.ToString(), message: $"Enter valid GPS Coordinates.");
                }

                var customerDetailedLocation = _geoCodingService.GetReverseGeocodingResponse(Coordinates);

                if (customerDetailedLocation != null)
                {
                    //var countryLocation = customerDetailedLocation.Result.Data.results.First();
                    var locationCountry = customerDetailedLocation.Result.Data.results.Where(c => c.types[0] == "country").First();
                    var locationStateOrProvince = customerDetailedLocation.Result.Data.results.Where(c => c.types[0] == "administrative_area_level_1").First();
                    var locationDistrictOrCountryorLGA = customerDetailedLocation.Result.Data.results.Where(c => c.types[0] == "administrative_area_level_2").First();
                    var locationCityOrTown = customerDetailedLocation.Result.Data.results.Where(c => c.types[0] == "administrative_area_level_3").First();
                    //int LastAddressComponentIndex = customerDetailedLocation.Result.Data.results.Count - 1;

                    var existingCity = await _context.Towns.FirstOrDefaultAsync(u => u.GoogleMapsPlaceId == locationCityOrTown.place_id);
                    if (existingCity == null) //city
                    {
                        var existingDistrictorLGA = await _context.LGAs.FirstOrDefaultAsync(u => u.GoogleMapsPlaceId == locationCityOrTown.place_id);
                        if (existingDistrictorLGA == null) //lga
                        {
                            var existingState = await _context.Towns.FirstOrDefaultAsync(u => u.GoogleMapsPlaceId == locationCityOrTown.place_id);
                            if (existingState == null) //state
                            {
                                var existingCountry = await _context.Towns.FirstOrDefaultAsync(u => u.GoogleMapsPlaceId == locationCityOrTown.place_id);
                                if (existingCountry == null) //country
                                {
                                    //CreateCountry
                                    Country country = new Country
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = locationCountry.address_components[0].long_name,
                                        GoogleMapsShortName = locationCountry.address_components[0].short_name,
                                        GoogleMapsLongName = locationCountry.address_components[0].long_name,
                                        CreatedBy = "Admin",
                                        GoogleMapsFormattedAddress = locationCountry.formatted_address,
                                        GoogleMapsPlaceId = locationCountry.place_id,
                                        GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationCountry.geometry),
                                        GoogleMapsLocationType = JsonConvert.SerializeObject(locationCountry.types),
                                        Created = DateTime.UtcNow.AddHours(1),
                                    };

                                    _context.Countries.Add(country);
                                    await _context.SaveChangesAsync();

                                    // //Create State with CountryId  
                                    State state = new State
                                    {
                                        Id = Guid.NewGuid(),
                                        CountryId = country.Id,
                                        Name = locationStateOrProvince.address_components[0].long_name,
                                        GoogleMapsShortName = locationStateOrProvince.address_components[0].short_name,
                                        GoogleMapsLongName = locationStateOrProvince.address_components[0].long_name,
                                        CreatedBy = "Admin",
                                        GoogleMapsFormattedAddress = locationStateOrProvince.formatted_address,
                                        GoogleMapsPlaceId = locationStateOrProvince.place_id,
                                        GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationStateOrProvince.geometry),
                                        GoogleMapsLocationType = JsonConvert.SerializeObject(locationStateOrProvince.types),
                                        Created = DateTime.UtcNow.AddHours(1),
                                    };

                                    _context.States.Add(state);
                                    await _context.SaveChangesAsync();

                                    // Create LGA with StateId 
                                    LGA lga = new LGA
                                    {
                                        Id = Guid.NewGuid(),
                                        StateId = state.Id,
                                        Name = locationDistrictOrCountryorLGA.address_components[0].long_name,
                                        GoogleMapsShortName = locationDistrictOrCountryorLGA.address_components[0].short_name,
                                        GoogleMapsLongName = locationDistrictOrCountryorLGA.address_components[0].long_name,
                                        CreatedBy = "Admin",
                                        GoogleMapsFormattedAddress = locationDistrictOrCountryorLGA.formatted_address,
                                        GoogleMapsPlaceId = locationDistrictOrCountryorLGA.place_id,
                                        GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationDistrictOrCountryorLGA.geometry),
                                        GoogleMapsLocationType = JsonConvert.SerializeObject(locationDistrictOrCountryorLGA.types),
                                        Created = DateTime.UtcNow.AddHours(1),
                                    };

                                    _context.LGAs.Add(lga);
                                    await _context.SaveChangesAsync();

                                    // create city with LGA id
                                    Town city = new Town
                                    {
                                        Id = Guid.NewGuid(),
                                        LGAId = lga.Id,
                                        Name = locationCityOrTown.address_components[0].long_name,
                                        GoogleMapsShortName = locationCityOrTown.address_components[0].short_name,
                                        GoogleMapsLongName = locationCityOrTown.address_components[0].long_name,
                                        CreatedBy = "Admin",
                                        GoogleMapsFormattedAddress = locationCityOrTown.formatted_address,
                                        GoogleMapsPlaceId = locationCityOrTown.place_id,
                                        GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationCityOrTown.geometry),
                                        GoogleMapsLocationType = JsonConvert.SerializeObject(locationCityOrTown.types),
                                        Created = DateTime.UtcNow.AddHours(1),
                                    };

                                    _context.Towns.Add(city);
                                    await _context.SaveChangesAsync();

                                    return new Response<string>(data: city.Id.ToString(), responsestatus: APIResponseStatus.success.ToString(), message: $"Enter valid GPS Coordinates.");

                                }
                                else
                                {
                                    // //Create State with CountryId  
                                    State state = new State
                                    {
                                        Id = Guid.NewGuid(),
                                        CountryId = existingCountry.Id,
                                        Name = locationStateOrProvince.address_components[0].long_name,
                                        GoogleMapsShortName = locationStateOrProvince.address_components[0].short_name,
                                        GoogleMapsLongName = locationStateOrProvince.address_components[0].long_name,
                                        CreatedBy = "Admin",
                                        GoogleMapsFormattedAddress = locationStateOrProvince.formatted_address,
                                        GoogleMapsPlaceId = locationStateOrProvince.place_id,
                                        GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationStateOrProvince.geometry),
                                        GoogleMapsLocationType = JsonConvert.SerializeObject(locationStateOrProvince.types),
                                        Created = DateTime.UtcNow.AddHours(1),
                                    };

                                    _context.States.Add(state);
                                    await _context.SaveChangesAsync();

                                    // Create LGA with StateId 
                                    LGA lga = new LGA
                                    {
                                        Id = Guid.NewGuid(),
                                        StateId = state.Id,
                                        Name = locationDistrictOrCountryorLGA.address_components[0].long_name,
                                        GoogleMapsShortName = locationDistrictOrCountryorLGA.address_components[0].short_name,
                                        GoogleMapsLongName = locationDistrictOrCountryorLGA.address_components[0].long_name,
                                        CreatedBy = "Admin",
                                        GoogleMapsFormattedAddress = locationDistrictOrCountryorLGA.formatted_address,
                                        GoogleMapsPlaceId = locationDistrictOrCountryorLGA.place_id,
                                        GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationDistrictOrCountryorLGA.geometry),
                                        GoogleMapsLocationType = JsonConvert.SerializeObject(locationDistrictOrCountryorLGA.types),
                                        Created = DateTime.UtcNow.AddHours(1),
                                    };

                                    _context.LGAs.Add(lga);
                                    await _context.SaveChangesAsync();

                                    // create city with LGA id
                                    Town city = new Town
                                    {
                                        Id = Guid.NewGuid(),
                                        LGAId = lga.Id,
                                        Name = locationCityOrTown.address_components[0].long_name,
                                        GoogleMapsShortName = locationCityOrTown.address_components[0].short_name,
                                        GoogleMapsLongName = locationCityOrTown.address_components[0].long_name,
                                        CreatedBy = "Admin",
                                        GoogleMapsFormattedAddress = locationCityOrTown.formatted_address,
                                        GoogleMapsPlaceId = locationCityOrTown.place_id,
                                        GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationCityOrTown.geometry),
                                        GoogleMapsLocationType = JsonConvert.SerializeObject(locationCityOrTown.types),
                                        Created = DateTime.UtcNow.AddHours(1),
                                    };

                                    _context.Towns.Add(city);
                                    await _context.SaveChangesAsync();

                                    return new Response<string>(data: city.Id.ToString(), responsestatus: APIResponseStatus.success.ToString(), message: $"Enter valid GPS Coordinates.");

                                }
                            }
                            else
                            {
                                // Create LGA with StateId 
                                LGA lga = new LGA
                                {
                                    Id = Guid.NewGuid(),
                                    StateId = existingState.Id,
                                    Name = locationDistrictOrCountryorLGA.address_components[0].long_name,
                                    GoogleMapsShortName = locationDistrictOrCountryorLGA.address_components[0].short_name,
                                    GoogleMapsLongName = locationDistrictOrCountryorLGA.address_components[0].long_name,
                                    CreatedBy = "Admin",
                                    GoogleMapsFormattedAddress = locationDistrictOrCountryorLGA.formatted_address,
                                    GoogleMapsPlaceId = locationDistrictOrCountryorLGA.place_id,
                                    GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationDistrictOrCountryorLGA.geometry),
                                    GoogleMapsLocationType = JsonConvert.SerializeObject(locationDistrictOrCountryorLGA.types),
                                    Created = DateTime.UtcNow.AddHours(1),
                                };

                                _context.LGAs.Add(lga);
                                await _context.SaveChangesAsync();

                                // create city with LGA id
                                Town city = new Town
                                {
                                    Id = Guid.NewGuid(),
                                    LGAId = lga.Id,
                                    Name = locationCityOrTown.address_components[0].long_name,
                                    GoogleMapsShortName = locationCityOrTown.address_components[0].short_name,
                                    GoogleMapsLongName = locationCityOrTown.address_components[0].long_name,
                                    CreatedBy = "Admin",
                                    GoogleMapsFormattedAddress = locationCityOrTown.formatted_address,
                                    GoogleMapsPlaceId = locationCityOrTown.place_id,
                                    GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationCityOrTown.geometry),
                                    GoogleMapsLocationType = JsonConvert.SerializeObject(locationCityOrTown.types),
                                    Created = DateTime.UtcNow.AddHours(1),
                                };

                                _context.Towns.Add(city);
                                await _context.SaveChangesAsync();

                                return new Response<string>(data: city.Id.ToString(), responsestatus: APIResponseStatus.success.ToString(), message: $"Enter valid GPS Coordinates.");

                            }
                        }
                        else 
                        {
                            // create city with LGA id
                            Town city = new Town
                            {
                                Id = Guid.NewGuid(),
                                LGAId = existingDistrictorLGA.Id,
                                Name = locationCityOrTown.address_components[0].long_name,
                                GoogleMapsShortName = locationCityOrTown.address_components[0].short_name,
                                GoogleMapsLongName = locationCityOrTown.address_components[0].long_name,
                                CreatedBy = "Admin",
                                GoogleMapsFormattedAddress = locationCityOrTown.formatted_address,
                                GoogleMapsPlaceId = locationCityOrTown.place_id,
                                GoogleMapsGeometryInfo = JsonConvert.SerializeObject(locationCityOrTown.geometry),
                                GoogleMapsLocationType = JsonConvert.SerializeObject(locationCityOrTown.types),
                                Created = DateTime.UtcNow.AddHours(1),
                            };

                            _context.Towns.Add(city);
                            await _context.SaveChangesAsync();

                            return new Response<string>(data: city.Id.ToString(), responsestatus: APIResponseStatus.success.ToString(), message: $"Enter valid GPS Coordinates.");

                        }
                    }
                    else
                    {
                        return new Response<string>(data: existingCity.Id.ToString(), responsestatus: APIResponseStatus.success.ToString(), message: $"Enter valid GPS Coordinates.");
                    }
                }

                else
                {
                    return new Response<string>(responsestatus: APIResponseStatus.fail.ToString(), message: $"Could not retrieve customer location from Coordinates.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException.ToString()}: Details {ex.StackTrace}");
                return new Response<string>(responsestatus: APIResponseStatus.fail.ToString(), message: $"{ex.Message}");
            }
        }
    }
}
