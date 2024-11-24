using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public class EventService : IEventService
  {
    private readonly IStatisticService _statisticService;
    private readonly IList<Event> _events;

    public EventService(IStatisticService statisticService)
    {
      _statisticService = statisticService;
      _events = EventList.events; // Mocking a database
    }

    public async Task<EventStatistic> GetEvent(Guid id)
    {
      var @event = _events.FirstOrDefault(e => e.Id == id);

      if (@event == null)
      {
        return null;
      }

      if (@event.Type == EventType.Online)
      {
        var online = await _statisticService.GetOnlineStatisticBy(@event);
        return EventStatistic.FromEvent(@event, online);
      }

      if (@event.Type == EventType.OnSite)
      {
        var onSite = await _statisticService.GetOnSiteStatisticBy(@event);
        return EventStatistic.FromEvent(@event, onSite);
      }

      if (@event.Type == EventType.Hybrid)
      {
        var online = await _statisticService.GetOnlineStatisticBy(@event);
        var onSite = await _statisticService.GetOnSiteStatisticBy(@event);
        return EventStatistic.FromEvent(@event, online, onSite);
      }

      throw new NotImplementedException($"Event with ID {id} encountered an unimplemented or unknown event type.");
    }

    public Task<IList<Event>> GetEvents()
    {
      return Task.FromResult(_events);
    }
  }
}
