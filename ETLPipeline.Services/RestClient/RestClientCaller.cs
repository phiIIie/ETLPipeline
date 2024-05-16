using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPipeline.Services.RestClientCaller
{
    public abstract class RestClientCaller
    {
        private readonly string apiurl = "https://opensky-network.org/api/";
        protected RestClient _client { get; set; }

        public RestClientCaller()
        {
            RestClientOptions clientOptions = new(apiurl)
            {
                UserAgent = "Flights Rest Client",
                FollowRedirects = true

            };
            _client = new RestClient(options: clientOptions);
        }
        protected RestRequest CreateRequest(string resource)
        {
            RestRequest request = new(resource);
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept-version", "v1");
            return request;
        }

        protected async Task<T> CallEndpointAsync<T>(string endpointName) where T : new()
        {
            RestRequest request = CreateRequest(endpointName);

            RestResponse<T> response = await _client.ExecuteAsync<T>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Failed to call endpoint");
            }
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            if (response.Data == null)
            {
                throw new NullReferenceException("Received null data from the API");
            }

            return response.Data;
        }
    }
}
