using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_API.CoreLayer
{
    public class RequestBuilder
    {
        private readonly RestRequest request;

        public RequestBuilder() 
        {
            request = new RestRequest();
        }

        public RequestBuilder(string resource, Method method)
        {
            request = new RestRequest(resource, method);
        }

        public RequestBuilder AddParameter(string name, object value, ParameterType type)
        {
            request.AddParameter(name, value, type);
            return this;
        }

        public RestRequest Build()
        {
            return request;
        }
    }
}
