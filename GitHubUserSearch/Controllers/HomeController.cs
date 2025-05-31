using GitHubUserSearch.Services;
using GitHubUserSearch.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHubUserSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubService _gitHubService;

        public HomeController()
        {
            _gitHubService = new GitHubService(); // could inject in real app
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ModelState.AddModelError("", "Username cannot be empty.");
                return View();
            }

            var user = await _gitHubService.GetUserAsync(username);
            if (user == null)
            {
                ModelState.AddModelError("", "GitHub user not found.");
                return View();
            }

            var repos = await _gitHubService.GetRepositoriesAsync(user.Repos_Url);
            var topRepos = repos
                .OrderByDescending(r => r.Stargazers_Count)
                .Take(5)
                .ToList();

            var vm = new GitHubViewModel
            {
                User = user,
                TopRepositories = topRepos
            };

            return View("Results", vm);
        }
    }
}
