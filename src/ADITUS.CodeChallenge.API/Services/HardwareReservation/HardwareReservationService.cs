using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.BusinessException;

namespace ADITUS.CodeChallenge.API.Services
{
  public class HardwareReservationService : IHardwareReservationService
  {
    private readonly IList<HardwareReservation> _hardwareReservations;
    private readonly IList<Event> _events;
    private readonly IList<HardwareComponent> _hardwareStock;

    private readonly int m_MIN_DAYS_BEFORE_EVENT = 28; // 4 weeks in days

    public const string APPROVED_STATUS = "Approved";
    public const string PENDING_STATUS = "Pending";

    public HardwareReservationService()
    {
      _hardwareReservations = new List<HardwareReservation>();
      _events = EventList.events; // Mocking a database
      _hardwareStock = new List<HardwareComponent> {
         new HardwareComponent{Type = HardwareType.MobileScanningTerminal, Quantity = 50},
         new HardwareComponent{Type = HardwareType.Turnstile, Quantity = 50 },
         new HardwareComponent{Type = HardwareType.WirelessHandheldScanner, Quantity = 50 }

      }; // Mocking a database
    }

    /// <summary>
    /// Approves a hardware reservation for an event. 
    /// </summary>
    /// <param name="eventId">A valid event id</param>
    /// <exception cref="HardwareReservationNotFoundException">Throws when no hardware reservation can be found for the given event id</exception>

    public void ApproveHardwareReservation(Guid eventId)
    {
      var hardwareReservation = _hardwareReservations.FirstOrDefault(x => x.EventId == eventId);
      if (hardwareReservation != null)
      {
        hardwareReservation.Status = HardwareReservationStatus.Approved;
      }
      else
      {
        throw new HardwareReservationNotFoundException(eventId);
      }
    }

    /// <summary>
    /// Gets a hardware reservation by event id. 
    /// </summary>
    /// <param name="eventId">A valid event id</param>
    /// <returns>A hardware reservert for the event id</returns>
    /// <exception cref="HardwareReservationNotFoundException">Throws when no hardware reservation can be found for the given event id</exception>
    public HardwareReservation GetHardwareReservation(Guid eventId)
    {
      var hardwareReservation = _hardwareReservations.FirstOrDefault(x => x.EventId == eventId);
      if (hardwareReservation != null)
      {
        return hardwareReservation;
      }
      else
      {
        throw new HardwareReservationNotFoundException(eventId);
      }
    }

    /// <summary>
    /// Adds a hardware reservation to the system. 
    /// Validates that the event does not start in the next 28 days of the current date and that there is enough stock for the requested components. 
    /// If successful, returns the new hardware reservation with an approved status. 
    /// Otherwise throws exceptions.
    /// </summary>
    /// <param name="eventId">A valid event id</param>
    /// <param name="hardwareComponents">Every hardware compnent that needs to be reserved.</param>
    /// <returns>new hardware reservation</returns>
    /// <exception cref="Exception">Unkown exception</exception>
    /// <exception cref="HardwareReservationAlreadyExistsException">Throws when for the given event id a hardware reservation already exists. </exception>
    /// <exception cref="HardwareReservationNotEnoughStockException">Throws when there is not enough hardware in stock for the requested components.</exception>
    public HardwareReservation AddHardwareReservation(Guid eventId, HardwareComponent[] hardwareComponents)
    {
      var @event = _events.FirstOrDefault(x => x.Id == eventId);

      if (@event == null)
      {
        throw new Exception("Event id not found"); // The exception was not written as a BusinessException to simplify the example and focus on basic error handling.
      }

      if (@event.StartDate == null)
      {
        throw new Exception("Event has no start date"); // The exception was not written as a BusinessException to simplify the example and focus on basic error handling.
      }

      var timeDifference = DateTime.UtcNow - @event.StartDate.Value.ToUniversalTime();

      if (timeDifference.Days <= m_MIN_DAYS_BEFORE_EVENT)
      {
        throw new Exception("Event start date is too close to the current date."); // The exception was not written as a BusinessException to simplify the example and focus on basic error handling.
      }

      var existingHardwareReservation = _hardwareReservations.FirstOrDefault(x => x.EventId == eventId);
      if (existingHardwareReservation != null)
      {
        throw new HardwareReservationAlreadyExistsException();
      }

      foreach (var component in hardwareComponents)
      {
        if (!CheckStockAvailability(component.Type, component.Quantity))
        {
          throw new HardwareReservationNotEnoughStockException();
        }
      }

      foreach (var component in hardwareComponents)
      {
        var hardwareComponent = _hardwareStock.FirstOrDefault(x => x.Type == component.Type);
        hardwareComponent.Quantity -= component.Quantity;
      }

      var newHardwareReservation = new HardwareReservation
      {
        EventId = eventId,
        Status = HardwareReservationStatus.Pending,
        ReservedHardware = hardwareComponents,
      };

      _hardwareReservations.Add(newHardwareReservation);

      return newHardwareReservation;
    }

    private bool CheckStockAvailability(HardwareType type, int quantity)
    {
      var hardwareComponent = _hardwareStock.FirstOrDefault(x => x.Type == type);
      if (hardwareComponent != null && hardwareComponent.Quantity >= quantity)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}