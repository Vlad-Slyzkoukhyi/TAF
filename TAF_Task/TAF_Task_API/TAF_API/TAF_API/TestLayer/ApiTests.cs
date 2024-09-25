using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;
using TAF_API.CoreLayer;
using TAF_API.BusinessLayer;
using log4net;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)]

namespace TAF_API.TestLayer
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [Category("API")]

    public class UserApiTests
    {
        private BaseClient _client;
        private IConfigurationRoot _configuration;
        private string? _baseUrl;
        protected ILog Log => LogManager.GetLogger(this.GetType());

        [OneTimeSetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            _baseUrl = _configuration["ApiSettings:BaseUrl"];

            if (_baseUrl != null)
            {
                _client = new BaseClient(_baseUrl);
            }
        }

        [Test]
        public void ValidateUsersListing()
        {
            Log.Info("Test check validate users listing - start");
            var request = new RequestBuilder("users", Method.Get).Build();
            var response = _client.ExecuteRequest<List<User>>(request);
            
            Assert.Multiple(() =>
            {
                Log.Info("Check satuts code");
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Info("Check satuts Data");
                Assert.That(response.Data, Is.Not.Null);
                if (response.Data != null)
                {
                    foreach (var user in response.Data)
                    {
                        Log.Info("Check satuts Id");
                        Assert.That(user.Id, Is.Not.EqualTo(0), "User ID should not be 0");
                        Log.Info("Check satuts Name");
                        Assert.That(user.Name, Is.Not.Null.And.Not.Empty, "User Name should not be null or empty");
                        Log.Info("Check satuts Username");
                        Assert.That(user.Username, Is.Not.Null.And.Not.Empty, "User Username should not be null or empty");
                        Log.Info("Check satuts Email");
                        Assert.That(user.Email, Is.Not.Null.And.Not.Empty, "User Email should not be null or empty");
                        Log.Info("Check satuts Address");
                        Assert.That(user.Address, Is.Not.Null, "User Address should not be null");
                        Log.Info("Check satuts Phone");
                        Assert.That(user.Phone, Is.Not.Null.And.Not.Empty, "User Phone should not be null or empty");
                        Log.Info("Check satuts Website");
                        Assert.That(user.Website, Is.Not.Null.And.Not.Empty, "User Website should not be null or empty");
                        Log.Info("Check satuts Company");
                        Assert.That(user.Company, Is.Not.Null, "User Company should not be null");
                    }
                }
                Log.Info("Check Data is equal 10");
                Assert.That(response.Data, Has.Count.EqualTo(10));
                Log.Info("Check no error");
                Assert.That(response.ErrorException, Is.Null, "Expected no error messages, but some were received");
            });
        }

        [Test]
        public void TestValidateResponseHeaderForListOfUsers()
        {
            Log.Info("Test check validate response header for list of users");
            var request = new RestRequest("users", Method.Get);
            var response = _client.ExecuteRequest(request);
            var contentTypeHeader = response?.ContentHeaders?.FirstOrDefault(x => x.Name == "Content-Type")?.Value?.ToString();

            Assert.Multiple(() =>
            {
                Log.Info("Check content header is not null");
                Assert.That(contentTypeHeader, Is.Not.Null, "Content-Type header is missing.");
                Log.Info("Check content header is respond application/json; charset=utf-8");
                Assert.That(contentTypeHeader, Is.EqualTo("application/json; charset=utf-8"),
                    "Content-Type is not 'application/json; charset=utf-8'");
                Log.Info("Check responce");
                Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                    $"Expected status code 200 OK, but got {response?.StatusCode}");
                Log.Info("Check no error");
                Assert.That(response?.ErrorException, Is.Null, "Expected no error messages, but some were received");
            });
        }

        [Test]
        public void TestUserListResponseContent()
        {
            Log.Info("Test check user list response content");
            var request = new RestRequest("users", Method.Get);
            var response = _client.ExecuteRequest<List<User>>(request);

            Assert.Multiple(() =>
            {
                Log.Info("Check responce");
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP status code is not 200 OK.");
                Log.Info("Check no error");
                Assert.That(response.ErrorException, Is.Null, "There was an unexpected error executing the request.");
                Log.Info("Check no data is not null");
                Assert.That(response.Data, Is.Not.Null, "Response data is null.");
                Log.Info("Check data count is equal 10");
                Assert.That(response.Data, Has.Count.EqualTo(10), "The number of users returned is not equal to 10.");
            });

            var ids = response.Data.Select(user => user.Id).ToList();
            var names = response.Data.Select(user => user.Name).ToList();
            var usernames = response.Data.Select(user => user.Username).ToList();
            var companyNames = response.Data.Select(user => user.Company?.Name).ToList();

            Assert.Multiple(() =>
            {
                Log.Info("Check data id have not duplicate");
                Assert.That(ids.Distinct().Count(), Is.EqualTo(ids.Count), "There are duplicate IDs in the user list.");
                Log.Info("Check all name is not null and not empty");
                Assert.That(names, Is.All.Not.Null.And.Not.Empty, "One or more users have an empty or null name.");
                Log.Info("Check all username is not null and not empty");
                Assert.That(usernames, Is.All.Not.Null.And.Not.Empty, "One or more users have an empty or null username.");
                Log.Info("Check all company names is not null and not empty");
                Assert.That(companyNames, Is.All.Not.Null.And.Not.Empty, "One or more company names are null or empty.");
            });
        }

        [Test]
        public void TestUserCanBeCreated()
        {
            Log.Info("Test check user can be created");
            var newUser = new User
            {
                Name = "John Doe",
                Username = "john.doe"
            };

            var request = new RestRequest("users", Method.Post);
            request.AddJsonBody(newUser);
            var response = _client.ExecuteRequest<User>(request);

            Assert.Multiple(() =>
            {
                Log.Info("Responce is not null");
                Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
                Log.Info("Id is not null");
                Assert.That(response.Data?.Id.ToString(), Is.Not.Empty, "ID should not be empty");
                Log.Info("Check status code");
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Expected status code 201 Created");
                Log.Info("Check errors");
                Assert.That(response.ErrorException, Is.Null, "There should be no error messages during user creation");
            });
        }

        [Test]
        public void TestResourceNotFound()
        {
            Log.Info("Test resource not found");
            var request = new RestRequest("invalidendpoint", Method.Get);
            var response = _client.ExecuteRequest(request);

            Assert.Multiple(() =>
            {
                Log.Info("Check status codel");
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected status code 404 Not Found");
                Log.Info("Check status errors");
                Assert.That(response.ErrorMessage, Is.Null, "Expected no error messages, but an error occurred");
            });
        }
    }
}
