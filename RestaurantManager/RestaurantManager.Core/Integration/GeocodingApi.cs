using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestaurantManager.Core.Integration.Model;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Core.Integration
{
    public interface IGeocodingApi
    {

    }
    public class GeocodingApi : IGeocodingApi
    {
        private readonly RestClient _client;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public GeocodingApi(IConfiguration iConfig)
        {
            var apiUrl = _configuration.GetSection("GeocodingApiConfig").GetSection("ApiUrl").Value;
            _apiKey = _configuration.GetSection("GeocodingApiConfig").GetSection("ApiKey").Value;

            _client = new RestClient(apiUrl);
            _configuration = iConfig;
        }

        public GeocodingApiResponse GetCordinatesFromAdress(string address)
        {
            var request = new RestRequest($"?api_key={_apiKey}&query={address}", DataFormat.Json);
            var response = _client.Get(request);

            // Map IRestResponse to GeocodingApiResponse.

            return new GeocodingApiResponse();
        }

    }
}
