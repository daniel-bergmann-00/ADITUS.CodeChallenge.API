#nullable enable

namespace ADITUS.CodeChallenge.API.Domain
{
  public record EventStatistic : Event
  {

    public OnlineStatistic? OnlineStatistic { get; set; }
    public OnSiteStatistic? OnSiteStatistic { get; set; }

    public static EventStatistic FromEvent(Event @event, OnlineStatistic? onlineStatistic, OnSiteStatistic? onSiteStatistic)
    {
      return new EventStatistic
      {

        Id = @event.Id,
        Year = @event.Year,
        Name = @event.Name,
        Type = @event.Type,
        StartDate = @event.StartDate,
        EndDate = @event.EndDate,
        OnlineStatistic = onlineStatistic,
        OnSiteStatistic = onSiteStatistic
      };
    }

    public static EventStatistic FromEvent(Event @event, OnlineStatistic onlineStatistic)
    {
      return FromEvent(@event, onlineStatistic, null);
    }

    public static EventStatistic FromEvent(Event @event, OnSiteStatistic onSiteStatistic)
    {
      return FromEvent(@event, null, onSiteStatistic);
    }
  }
}
