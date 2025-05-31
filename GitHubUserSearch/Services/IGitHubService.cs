using System.Threading.Tasks;
using GitHubUserSearch.Models;
using System.Collections.Generic;

namespace GitHubUserSearch.Services
{
    public interface IGitHubService
    {
        Task<GitHubUser> GetUserAsync(string username);
        Task<List<GitHubRepo>> GetRepositoriesAsync(string reposUrl);
    }
}
