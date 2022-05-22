using Application.DTOs.Location;
using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Services.Interfaces.Location;
using Application.Wrappers;
using Domain.Settings;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        public GeocodingService(ILogger logger, IConfiguration configuration,
            IUtilities utilities, IOptions<APIURLs> apiURLs, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _configuration = configuration;
            _utilities = utilities;
            _aPIURLs = apiURLs.Value;
            _appConfig = appConfig.Value;
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

                requestResponse = await _utilities.MakeHttpRequest(null, _aPIURLs.GoogleGeocodingBaseURL, $"{_aPIURLs.ReverseGeocodingUrlSuffix}?latlng={latitude},{longitude}&key={_appConfig.GocodingApiKey}", HttpMethod.Get, null);

                if (requestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    reverseGeocodingResponse = JsonConvert.DeserializeObject<ReverseGeocodingResponse>(requestResponse.Content.ToString());

                    //customerPreciseLocation.Country = reverseGeocodingResponse.results.First().address_components.Where(x => x.types[0] == "country").FirstOrDefault().long_name;

                    var address = reverseGeocodingResponse.results.First();

                    customerPreciseLocation.StateName = address.address_components.Where(x => x.types[0] == "administrative_area_level_1").FirstOrDefault().long_name;
                    customerPreciseLocation.LGAName = address.address_components.Where(x => x.types[0] == "administrative_area_level_2").FirstOrDefault().long_name;
                    customerPreciseLocation.DistrictName = address.address_components.Where(x => x.types[0] == "neighborhood").FirstOrDefault().long_name;
                    customerPreciseLocation.FormattedAddress = address.formatted_address;

                    return new Response<CustomerPreciseLocation>(customerPreciseLocation, $"Coordinates successfully reversed.", successStatus: true);

                }

                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<ReverseGeocodingErrorResponse>(requestResponse.Content.ToString());
                    return new Response<CustomerPreciseLocation>(null, $"{errorResponse.error_message}", successStatus: false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response<CustomerPreciseLocation>(null, $"An error occurred while reversing coordinates: e{ex.Message}", successStatus: false);
            }
        }
    }
}
