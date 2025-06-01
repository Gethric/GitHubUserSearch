using GitHubUserSearch.Models;
using GitHubUserSearch.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubUserSearch.Tests.Services
{
    [TestClass]
    public class GitHubServiceTests
    {
        [TestMethod]
        public async Task GetUserAsync_ReturnsUser_WhenSuccessful()
        {
            // Arrange
            var expectedUser = new GitHubUser
            {
                Login = "octocat",
                Name = "The Octocat",
                Location = "Internet",
                Avatar_Url = "https://example.com/avatar",
                Repos_Url = "https://api.github.com/users/octocat/repos"
            };

            var json = JsonConvert.SerializeObject(expectedUser);

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json),
                });

            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubUserSearchApp/1.0");

            var service = new GitHubService(httpClient);

            // Act
            var result = await service.GetUserAsync("octocat");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("octocat", result.Login);
        }

        // Internal subclass to inject HttpClient for testing
        private class GitHubServiceForTest : GitHubService
        {
            public GitHubServiceForTest(HttpClient client)
            {
                this._httpClient = client;
            }

            protected new HttpClient _httpClient;
        }
    }
}
