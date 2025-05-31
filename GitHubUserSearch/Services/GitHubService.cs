using GitHubUserSearch.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubUserSearch.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("GitHubUserSearchApp", "1.0"));
        }

        public async Task<GitHubUser> GetUserAsync(string username)
        {
            var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GitHubUser>(json);
        }

        public async Task<List<GitHubRepo>> GetRepositoriesAsync(string reposUrl)
        {
            var response = await _httpClient.GetAsync(reposUrl);
            if (!response.IsSuccessStatusCode) return new List<GitHubRepo>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GitHubRepo>>(json);
        }
    }
}
