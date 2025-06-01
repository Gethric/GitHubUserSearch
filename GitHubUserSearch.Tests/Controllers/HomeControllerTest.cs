using GitHubUserSearch.Controllers;
using GitHubUserSearch.Models;
using GitHubUserSearch.Services;
using GitHubUserSearch.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHubUserSearch.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public async Task Index_ReturnsView_WhenUsernameIsEmpty()
        {
            // Arrange
            var mockService = new Mock<IGitHubService>();
            var controller = new HomeController(mockService.Object);

            // Act
            var result = await controller.Index("") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!controller.ModelState.IsValid);
        }

        [TestMethod]
        public async Task Index_ReturnsResultsViewModel_WhenUserExists()
        {
            // Arrange
            var mockService = new Mock<IGitHubService>();
            mockService.Setup(s => s.GetUserAsync("octocat")).ReturnsAsync(new GitHubUser
            {
                Login = "octocat",
                Name = "Octo",
                Location = "Net",
                Avatar_Url = "avatar.jpg",
                Repos_Url = "repos_url"
            });

            mockService.Setup(s => s.GetRepositoriesAsync("repos_url")).ReturnsAsync(new List<GitHubRepo>
            {
                new GitHubRepo { Name = "Repo1", Stargazers_Count = 100 },
                new GitHubRepo { Name = "Repo2", Stargazers_Count = 50 },
                new GitHubRepo { Name = "Repo3", Stargazers_Count = 75 },
                new GitHubRepo { Name = "Repo4", Stargazers_Count = 30 },
                new GitHubRepo { Name = "Repo5", Stargazers_Count = 60 },
                new GitHubRepo { Name = "Repo6", Stargazers_Count = 10 }
            });

            var controller = new HomeController(mockService.Object);

            // Act
            var result = await controller.Index("octocat") as ViewResult;
            var model = result.Model as GitHubViewModel;

            // Assert
            Assert.AreEqual("Results", result.ViewName);
            Assert.AreEqual(5, model.TopRepositories.Count);
        }

        [TestMethod]
        public async Task Index_ReturnsError_WhenUserNotFound()
        {
            // Arrange
            var mockService = new Mock<IGitHubService>();
            mockService.Setup(s => s.GetUserAsync("notfound")).ReturnsAsync((GitHubUser)null);

            var controller = new HomeController(mockService.Object);

            // Act
            var result = await controller.Index("notfound") as ViewResult;

            // Assert
            Assert.IsTrue(!controller.ModelState.IsValid);
        }
    }
}
