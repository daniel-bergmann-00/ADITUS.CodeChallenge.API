using System.Text.Json;
using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Exception;

namespace ADITUS.CodeChallenge.API.Services
{
  public class StatisticService : IStatisticService
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _JsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public StatisticService(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Calling <c>api/online-statistics/:eventId</c> for the given event.
    /// </summary>
    /// <param name="event">any event with a id</param>
    /// <returns>Statistic for the given event</returns>
    public async Task<OnlineStatistic> GetOnlineStatisticBy(Event @event)
    {
      var url = $"api/online-statistics/{@event.Id}";
      return await GetStatisticBy<OnlineStatistic>(url);
    }

    /// <summary>
    /// Calling <c>api/onsite-statistics/:eventId</c> for the given event.
    /// </summary>
    /// <param name="event">any event with a id</param>
    /// <returns>Statistic for the given event</returns>
    public async Task<OnSiteStatistic> GetOnSiteStatisticBy(Event @event)
    {
      var url = $"api/onsite-statistics/{@event.Id}";
      return await GetStatisticBy<OnSiteStatistic>(url);
    }

    private async Task<T> GetStatisticBy<T>(String url)
    {
      var apiClient = _httpClientFactory.CreateClient(HttpClientNames.StatisticApiClientName);
      var response = await apiClient.GetAsync(url);
      if (response.IsSuccessStatusCode)
      {
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse, _JsonOptions);
      }
      throw new ApiStatisticException($"Error occurred while calling {url}. Status Code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
    }
  }
}
