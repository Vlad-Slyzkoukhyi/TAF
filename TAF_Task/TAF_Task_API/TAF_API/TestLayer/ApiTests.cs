using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;
using TAF_API.CoreLayer;
using TAF_API.BusinessLayer;

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
            var request = new RequestBuilder("users", Method.Get).Build();
            var response = _client.ExecuteRequest<List<User>>(request);
            
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Data, Is.Not.Null);
                if (response.Data != null)
                {
                    foreach (var user in response.Data)
                    {
                        Assert.That(user.Id, Is.Not.EqualTo(0), "User ID should not be 0");
                        Assert.That(user.Name, Is.Not.Null.And.Not.Empty, "User Name should not be null or empty");
                        Assert.That(user.Username, Is.Not.Null.And.Not.Empty, "User Username should not be null or empty");
                        Assert.That(user.Email, Is.Not.Null.And.Not.Empty, "User Email should not be null or empty");
                        Assert.That(user.Address, Is.Not.Null, "User Address should not be null");
                        Assert.That(user.Phone, Is.Not.Null.And.Not.Empty, "User Phone should not be null or empty");
                        Assert.That(user.Website, Is.Not.Null.And.Not.Empty, "User Website should not be null or empty");
                        Assert.That(user.Company, Is.Not.Null, "User Company should not be null");
                    }
                }
                Assert.That(response.Data, Has.Count.EqualTo(10));
                Assert.That(response.ErrorException, Is.Null, "Expected no error messages, but some were received");
            });
        }

        [Test]
        public void TestValidateResponseHeaderForListOfUsers()
        {
            var request = new RestRequest("users", Method.Get);
            var response = _client.ExecuteRequest(request);
            var contentTypeHeader = response?.ContentHeaders?.FirstOrDefault(x => x.Name == "Content-Type")?.Value?.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(contentTypeHeader, Is.Not.Null, "Content-Type header is missing.");
                Assert.That(contentTypeHeader, Is.EqualTo("application/json; charset=utf-8"),
                    "Content-Type is not 'application/json; charset=utf-8'");
                Assert.That(response?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK),
                    $"Expected status code 200 OK, but got {response?.StatusCode}");
                Assert.That(response?.ErrorException, Is.Null, "Expected no error messages, but some were received");
            });
        }

        [Test]
        public void TestUserListResponseContent()
        {
            var request = new RestRequest("users", Method.Get);
            var response = _client.ExecuteRequest<List<User>>(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "HTTP status code is not 200 OK.");
                Assert.That(response.ErrorException, Is.Null, "There was an unexpected error executing the request.");
                Assert.That(response.Data, Is.Not.Null, "Response data is null.");
                Assert.That(response.Data, Has.Count.EqualTo(10), "The number of users returned is not equal to 10.");
            });

            var ids = response.Data.Select(user => user.Id).ToList();
            var names = response.Data.Select(user => user.Name).ToList();
            var usernames = response.Data.Select(user => user.Username).ToList();
            var companyNames = response.Data.Select(user => user.Company?.Name).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(ids.Distinct().Count(), Is.EqualTo(ids.Count), "There are duplicate IDs in the user list.");
                Assert.That(names, Is.All.Not.Null.And.Not.Empty, "One or more users have an empty or null name.");
                Assert.That(usernames, Is.All.Not.Null.And.Not.Empty, "One or more users have an empty or null username.");
                Assert.That(companyNames, Is.All.Not.Null.And.Not.Empty, "One or more company names are null or empty.");
            });
        }

        [Test]
        public void TestUserCanBeCreated()
        {
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
                Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
                Assert.That(response.Data?.Id.ToString(), Is.Not.Empty, "ID should not be empty");
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Expected status code 201 Created");
                Assert.That(response.ErrorException, Is.Null, "There should be no error messages during user creation");
            });
        }

        [Test]
        public void TestResourceNotFound()
        {
            var request = new RestRequest("invalidendpoint", Method.Get);
            var response = _client.ExecuteRequest(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected status code 404 Not Found");
                Assert.That(response.ErrorMessage, Is.Null, "Expected no error messages, but an error occurred");
            });
        }
    }
}
