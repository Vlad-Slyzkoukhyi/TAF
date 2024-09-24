using log4net;
using RestSharp;

namespace TAF_API.CoreLayer
{
    public class BaseClient
    {
        private readonly RestClient client;
        private static readonly ILog log = LogManager.GetLogger(typeof(BaseClient));

        public BaseClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                FailOnDeserializationError = false,
                ThrowOnAnyError = false,
            };

            client = new RestClient(options);
            log.Info("API Client initialized with base URL: " + baseUrl);
        }

        public RestResponse<T> ExecuteRequest<T>(RestRequest request) where T : new()
        {
            log.Info($"Executing request to {request.Resource}");
            var response = client.Execute<T>(request);
            if (response.ErrorException != null)
                log.Error($"Error executing request: {response.ErrorMessage}", response.ErrorException);
            else
                log.Info($"Request successfully executed with status: {response.StatusCode}");

            return response;
        }

        public RestResponse ExecuteRequest(RestRequest request)
        {
            return client.Execute(request);
        }
    }
}
