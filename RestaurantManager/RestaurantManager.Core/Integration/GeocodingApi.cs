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

        public GeocodingApi(IConfiguration iConfig)
        {
            //config
            string apiUrl = _configuration.GetSection("GeocodingApiConfig").GetSection("ApiUrl").Value;

            _client = new RestClient();//api url
            _configuration = iConfig;
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");

        }

        public GeocodingApiResponse GetCordinatesFromAdress(string adress)
        {

            return new GeocodingApiResponse();
        }

    }
}
