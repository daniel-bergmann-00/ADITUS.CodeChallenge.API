using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public interface IStatisticService
  {
    Task<OnlineStatistic> GetOnlineStatisticBy(Event @event);
    Task<OnSiteStatistic> GetOnSiteStatisticBy(Event @event);
  }
}
