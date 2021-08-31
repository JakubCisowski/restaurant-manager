using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestaurantManager.Consts.Configs;
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
    public interface IGeocodingApiService
    {
        Task<CoordinatesResponse> GetCordinatesFromAdressAsync(string address);
    }
    public class GeocodingApiService : IGeocodingApiService
    {
        private readonly RestClient _client;
        private readonly GeocodingApiConfig _geocodingApiConfig;

        public GeocodingApiService(IOptions<GeocodingApiConfig> geocodingApiConfig)
        {
            _geocodingApiConfig = geocodingApiConfig.Value;
            _client = new RestClient(_geocodingApiConfig.ApiUrl);
        }

        public async Task<CoordinatesResponse> GetCordinatesFromAdressAsync(string address)
        {
            var request = new RestRequest(Method.GET);
            request.AddParameter("query", address);
            request.AddParameter("api_key", _geocodingApiConfig.ApiKey);
            request.AddHeader("referer", "localhost");

            var response = await _client.ExecuteAsync(request)
                .ConfigureAwait(false);

            var geocodingResponse = JsonConvert.DeserializeObject<List<GeocodingApiResponseItem>>(response.Content)
                .OrderByDescending(x => x.score)
                .FirstOrDefault();

            return new CoordinatesResponse(geocodingResponse.point.coordinates);
        }
    }
}
