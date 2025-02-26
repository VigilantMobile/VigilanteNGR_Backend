using Application.DTOs.Location;
using Application.Features.AppTroopers.SecurityTips;
using Application.Features.Location;
using Application.Services.Interfaces.Location;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
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
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Persistence.Services.Implementations.Location
{
    public class GeocodingService : IGeoCodingService
    {
        //private readonly IGeocoder geocoder;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IUtilities _utilities;
        private readonly APIURLs _aPIURLs;
        private readonly AppConfig _appConfig;
        private readonly ApplicationDbContext _context;

        public GeocodingService(ILogger logger, IConfiguration configuration,
            IUtilities utilities, IOptions<APIURLs> apiURLs, IOptions<AppConfig> appConfig, ApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _utilities = utilities;
            _aPIURLs = apiURLs.Value;
            _appConfig = appConfig.Value;
            _context = context;
        }

        public async Task<Response<CustomerPreciseLocation>> GetCustomerLiveAddresses(string Coordinates)
        {
            ReverseGeocodingResponse reverseGeocodingResponse = new ReverseGeocodingResponse();
            CustomerPreciseLocation customerPreciseLocation = new CustomerPreciseLocation();
            RestResponse requestResponse = new RestResponse();

            try
            {
                string latitude = Convert.ToDouble(Coordinates.Split(",")[0]).ToString();
                string longitude = Convert.ToDouble(Coordinates.Split(",")[1]).ToString();

                //requestResponse = await _utilities.MakeHttpRequest(null, _aPIURLs.GoogleMapsAPIBaseURL, $"{_aPIURLs.ReverseGeocodingUrlSuffix}?latlng={latitude},{longitude}&key={_appConfig.GeocodingApiKey}", HttpMethod.Get, null);


                //if (requestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    reverseGeocodingResponse = JsonConvert.DeserializeObject<ReverseGeocodingResponse>(requestResponse.Content.ToString());

                //    //customerPreciseLocation.Country = reverseGeocodingResponse.results.First().address_components.Where(x => x.types[0] == "country").FirstOrDefault().long_name;

                //    var address = reverseGeocodingResponse.results.First();

                //    customerPreciseLocation.Country = address.address_components.Where(x => x.types[0] == "country").FirstOrDefault().long_name;
                //    customerPreciseLocation.StateOrProvinceOrRegion = address.address_components.Where(x => x.types[0] == "administrative_area_level_1").FirstOrDefault().long_name;
                //    customerPreciseLocation.CountryOrDistrictOrLGA = address.address_components.Where(x => x.types[0] == "administrative_area_level_2").FirstOrDefault().long_name;
                //    customerPreciseLocation.TownOrDistrict = address.address_components.Where(x => x.types[0] == "administrative_area_level_3").FirstOrDefault().long_name;
                //    customerPreciseLocation.FormattedAddress = address.formatted_address;

                //    return new Response<CustomerPreciseLocation>(customerPreciseLocation, $"Coordinates successfully reversed.");

                //}

                var reverseGeocodingRequestResponse = GetReverseGeocodingResponse(Coordinates).Result;
                reverseGeocodingResponse = reverseGeocodingRequestResponse.Data;

                if (reverseGeocodingResponse != null)
                {

                    //customerPreciseLocation.Country = reverseGeocodingResponse.results.First().address_components.Where(x => x.types[0] == "country").FirstOrDefault().long_name;

                    //var address = reverseGeocodingResponse.results.First();
                    var address = reverseGeocodingResponse.results[7];

                    customerPreciseLocation.Country = address.address_components.Where(x => x.types[0] == "country").FirstOrDefault().long_name;
                    customerPreciseLocation.StateOrProvinceOrRegion = address.address_components.Where(x => x.types[0] == "administrative_area_level_1").FirstOrDefault().long_name;
                    customerPreciseLocation.CountryOrDistrictOrLGA = address.address_components.Where(x => x.types[0] == "administrative_area_level_2").FirstOrDefault().long_name;
                    customerPreciseLocation.TownOrDistrict = address.address_components.Where(x => x.types[0] == "administrative_area_level_3").FirstOrDefault().long_name;
                    customerPreciseLocation.FormattedAddress = address.formatted_address;

                    return new Response<CustomerPreciseLocation>(customerPreciseLocation, $"Coordinates successfully reversed.");

                }

                else
                {
                    //var errorResponse = JsonConvert.DeserializeObject<ReverseGeocodingErrorResponse>(requestResponse.Content.ToString());
                    return new Response<CustomerPreciseLocation>(null, $"{reverseGeocodingRequestResponse.Message}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response<CustomerPreciseLocation>(null, $"An error occurred while reversing coordinates: e{ex.Message}");
            }
        }

        public async Task<Response<ReverseGeocodingResponse>> GetReverseGeocodingResponse(string Coordinates)
        {
            ReverseGeocodingResponse reverseGeocodingResponse = new ReverseGeocodingResponse();
            RestResponse requestResponse = new RestResponse();
            try
            {
                string latitude = Convert.ToDouble(Coordinates.Split(",")[0]).ToString();
                string longitude = Convert.ToDouble(Coordinates.Split(",")[1]).ToString();

                requestResponse = await _utilities.MakeHttpRequest(null, _aPIURLs.GoogleMapsAPIBaseURL, $"{_aPIURLs.ReverseGeocodingUrlSuffix}?latlng={latitude},{longitude}&key={_appConfig.GeocodingApiKey}", HttpMethod.Get, null);

                if (requestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    reverseGeocodingResponse = JsonConvert.DeserializeObject<ReverseGeocodingResponse>(requestResponse.Content.ToString());

                    return new Response<ReverseGeocodingResponse>(reverseGeocodingResponse, $"Coordinates successfully reversed.");
                }

                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<ReverseGeocodingErrorResponse>(requestResponse.Content.ToString());
                    return new Response<ReverseGeocodingResponse>(null, $"{errorResponse.error_message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response<ReverseGeocodingResponse>(null, $"An error occurred while retrieving info for coordinates: e{ex.Message}");
            }
        }

        public async Task<Guid> GetOrCreateTownIdAsync(string townName, string lgaName, string stateName, string countryName)
        {
            // Check if the town already exists
            var existingTown = await _context.Towns.FirstOrDefaultAsync(t => t.Name == townName);
            if (existingTown != null)
            {
                return existingTown.Id;
            }

            // Check if the LGA already exists
            var existingLGA = await _context.LGAs.FirstOrDefaultAsync(l => l.Name == lgaName);
            if (existingLGA == null)
            {
                // Check if the state already exists
                var existingState = await _context.States.FirstOrDefaultAsync(s => s.Name == stateName);
                if (existingState == null)
                {
                    // Check if the country already exists
                    var existingCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
                    if (existingCountry == null)
                    {
                        // Create the country
                        var country = new Country
                        {
                            Id = Guid.NewGuid(),
                            Name = countryName,
                            CreatedBy = "Admin",
                            Created = DateTime.UtcNow.AddHours(1)
                        };
                        _context.Countries.Add(country);
                        await _context.SaveChangesAsync();
                        existingCountry = country;
                    }

                    // Create the state
                    var state = new State
                    {
                        Id = Guid.NewGuid(),
                        CountryId = existingCountry.Id,
                        Name = stateName,
                        CreatedBy = "Admin",
                        Created = DateTime.UtcNow.AddHours(1)
                    };
                    _context.States.Add(state);
                    await _context.SaveChangesAsync();
                    existingState = state;
                }

                // Create the LGA
                var lga = new LGA
                {
                    Id = Guid.NewGuid(),
                    StateId = existingState.Id,
                    Name = lgaName,
                    CreatedBy = "Admin",
                    Created = DateTime.UtcNow.AddHours(1)
                };
                _context.LGAs.Add(lga);
                await _context.SaveChangesAsync();
                existingLGA = lga;
            }

            // Create the town
            var town = new Town
            {
                Id = Guid.NewGuid(),
                LGAId = existingLGA.Id,
                Name = townName,
                CreatedBy = "Admin",
                Created = DateTime.UtcNow.AddHours(1)
            };
            _context.Towns.Add(town);
            await _context.SaveChangesAsync();

            return town.Id;
        }
    }
}
