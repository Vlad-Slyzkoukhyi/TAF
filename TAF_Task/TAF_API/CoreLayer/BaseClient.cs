using log4net;
using RestSharp;

namespace TAF_API.CoreLayer
{
    public class BaseClient
    {
        private readonly RestClient client;
        protected ILog Log => LogManager.GetLogger(this.GetType());

        public BaseClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                FailOnDeserializationError = false,
                ThrowOnAnyError = false,
            };

            client = new RestClient(options);
            Log.Info("API Client initialized with base URL: " + baseUrl);
        }

        public RestResponse<T> ExecuteRequest<T>(RestRequest request) where T : new()
        {
            Log.Info($"Executing request to {request.Resource}");
            var response = client.Execute<T>(request);
            if (response.ErrorException != null)
                Log.Error($"Error executing request: {response.ErrorMessage}", response.ErrorException);
            else
                Log.Info($"Request successfully executed with status: {response.StatusCode}");

            return response;
        }

        public RestResponse ExecuteRequest(RestRequest request)
        {
            return client.Execute(request);
        }
    }
}
