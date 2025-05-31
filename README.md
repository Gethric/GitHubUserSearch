# GitHub User Search (ASP.NET MVC)

A user-facing ASP.NET MVC (.NET Framework) application that allows searching for a GitHub user and displays their basic profile information along with their top 5 repositories by stargazer count.

## 💡 Features

- Search for any GitHub username
- Displays:
  - Username
  - Name
  - Location
  - Avatar
  - Top 5 repositories (name, description, stargazers)
- Handles edge cases:
  - Invalid input
  - User not found
  - No repositories
- Clean MVC architecture
- Fully unit tested service and controller logic

## 🛠 Tech Stack

- ASP.NET MVC (.NET Framework 4.7.2)
- C#
- Razor Views
- HttpClient for API access
- Newtonsoft.Json for JSON parsing
- MSTest for unit testing

## 📁 Structure

/Controllers         → MVC controller logic  
/Models              → GitHubUser & GitHubRepo POCOs  
/ViewModels          → GitHubViewModel for view data bundling  
/Services            → IGitHubService & GitHubService for API logic  
/Views/Home          → Index.cshtml (form), Results.cshtml (output)

## 🔍 Design Decisions

- **No third-party GitHub libraries (e.g. Octokit)**: per task specification
- **Modular service layer**: promotes testability and separation of concerns
- **Manually created folders**: `/Services` and `/ViewModels` reflect real-world maintainability needs
- **HttpClient with custom User-Agent**: GitHub API requires this
- **Top 5 repos sorted by stargazer_count**: using LINQ in controller layer

## 🧪 Unit Testing

- `GitHubService` logic tested using mock `HttpMessageHandler`
- `HomeController` logic tested using mock `IGitHubService`
- Edge cases tested: user not found, empty username, no repos

## 🚀 Running the App

1. Clone the repo
2. Open the solution in Visual Studio
3. Set `GitHubUserSearch` as the Startup Project
4. Run with `F5` or `Ctrl+F5`

## 🔒 Notes

- Built using .NET Framework MVC as specified (not .NET Core)
- No external libraries used for API wrapping to stay compliant with spec
