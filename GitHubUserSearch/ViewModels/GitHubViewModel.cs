using GitHubUserSearch.Models;
using System.Collections.Generic;

namespace GitHubUserSearch.ViewModels
{
    public class GitHubViewModel
    {
        public GitHubUser User { get; set; }
        public List<GitHubRepo> TopRepositories { get; set; }
    }
}
