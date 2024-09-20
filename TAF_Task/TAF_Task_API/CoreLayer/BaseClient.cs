using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using log4net.Config;
using log4net;

namespace TAF_Task_API.CoreLayer
{
    public class BaseClient
    {
        private readonly RestClient client;
        private static readonly ILog log = LogManager.GetLogger(typeof(BaseClient));

        public BaseClient(string baseUrl)
        {            
            client = new RestClient(baseUrl);
            log.Info("API Client initialized with base URL: " + baseUrl);
        }

        public RestResponse ExecuteRequest(RestRequest request)
        {
            log.Info($"Preparing to execute a {request.Method} request to {request.Resource}");

            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                log.Error($"Error executing request: {response.ErrorMessage}", response.ErrorException);
            }
            else
            {
                log.Info($"Received response with status: {response.StatusCode}");
                log.Debug("Response content: " + response.Content); 
            }

            return response;
        }
    }
}
