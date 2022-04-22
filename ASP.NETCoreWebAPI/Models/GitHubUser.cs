using Newtonsoft.Json;

namespace ASP.NETCoreWebAPI.Models;

//For external connection ale Polly Politics

public class GitHubUser
{
    [JsonProperty("login")]
    public string Login { get; set; } = string.Empty;

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; } = string.Empty;

    [JsonProperty("gravatar_id")]
    public string GravatarId { get; set; } = string.Empty;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    [JsonProperty("followers_url")]
    public string FollowersUrl { get; set; } = string.Empty;

    [JsonProperty("following_url")]
    public string FolloweingUrl { get; set; } = string.Empty;

    [JsonProperty("gists_url")]
    public string GistsUrl { get; set; } = string.Empty;

    [JsonProperty("starred_url")]
    public string StarredUrl { get; set; } = string.Empty;

    [JsonProperty("subscriptions_url")]
    public string SubscriptionsUrl { get; set; } = string.Empty;

    [JsonProperty("organizations_url")]
    public string OrganizationsUrl { get; set; } = string.Empty;

    [JsonProperty("repos_url")]
    public string ReposUrl { get; set; } = string.Empty;

    [JsonProperty("events_url")]
    public string EventsUrl { get; set; } = string.Empty;

    [JsonProperty("received_events_url")]
    public string ReceivedEventsUrl { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("company")]
    public string Company { get; set; } = string.Empty;

    [JsonProperty("blog")]
    public string Blog { get; set; } = string.Empty;

    [JsonProperty("location")]
    public string Location { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("hireable")]
    public bool? Hireable { get; set; }

    [JsonProperty("bio")]
    public string Bio { get; set; } = string.Empty;

    [JsonProperty("public_repos")]
    public int PublicRepos { get; set; }

    [JsonProperty("followers")]
    public int Followers { get; set; }

    [JsonProperty("following")]
    public int Following { get; set; }

    [JsonProperty("created_at")]
    public string CreatedAt { get; set; } = string.Empty;

    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; } = string.Empty;

    [JsonProperty("public_gists")]
    public int PublicGists { get; set; }
}