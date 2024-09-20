using log4net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TAF_Task_API.BusinessLayer;
using TAF_Task_API.CoreLayer;
using TAF_Task_API.Utils;

namespace TAF_Task_API.TestsLayer
{
    [TestFixture]
    public class ValidateThatTheListOfUsersReceivedSuccessfully
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ValidateThatTheListOfUsersReceivedSuccessfully));
        private BaseClient client;

        [SetUp]
        public void Setup()
        {
            LoggerInitializer.Initialize();
            client = new BaseClient("https://jsonplaceholder.typicode.com");
            log.Info("API Client has been initialized.");
        }

        [Test, Category("API")]
        public void ValidateUsersCanBeRetrieved()
        {
            log.Info("Starting test to validate user retrieval.");
            var request = new RequestBuilder("users", Method.Get).Build();
            var response = client.ExecuteRequest(request);

            log.Info("Request executed. Validating response...");
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode), "Status code is not OK.");

            var users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            Assert.That(users, Is.Not.Null, "Users list cannot be null.");
            log.Info("User retrieval validation passed. Number of users received: " + users.Count);
        }
    }
}
