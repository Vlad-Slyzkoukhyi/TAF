using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_Task_API.CoreLayer
{
    public class RequestBuilder
    {
        private RestRequest request;

        public RequestBuilder(string resource, Method method)
        {
            request = new RestRequest(resource, method);
        }

        public RestRequest Build()
        {
            return request;
        }
    }
}
