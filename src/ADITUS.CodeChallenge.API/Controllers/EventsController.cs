using System.Diagnostics;
using System.Text.Json;
using ADITUS.CodeChallenge.API.BusinessException;
using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API
{
  [Route("events")]
  public class EventsController : ControllerBase
  {
    private readonly IEventService _eventService;
    private readonly IHardwareReservationService _HardwareReservationService;

    public EventsController(IEventService eventService, IHardwareReservationService hardwareReservationService)
    {
      _eventService = eventService;
      _HardwareReservationService = hardwareReservationService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetEvents()
    {
      var events = await _eventService.GetEvents();
      return Ok(events);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
      var @event = await _eventService.GetEvent(id);
      if (@event == null)
      {
        return NotFound();
      }
      return Ok(@event);
    }

    [HttpGet]
    [Route("{id}/hardware-reservations")]
    public IActionResult GetHardwareReservation(Guid id)

    {
      try
      {
        var hardwareReservation = _HardwareReservationService.GetHardwareReservation(id);
        return Ok(hardwareReservation);
      }
      catch (HardwareReservationNotFoundException ex)
      {
        Debug.WriteLine(ex.ToString());
        return NotFound();
      }
    }

    [HttpPatch]
    [Route("{id}/hardware-reservations/approve")]
    public IActionResult ApproveHardwareReservation(Guid id)
    {
      try
      {
        _HardwareReservationService.ApproveHardwareReservation(id);
        return Ok();
      }
      catch (HardwareReservationNotFoundException ex)
      {
        Debug.WriteLine(ex.ToString());
        return NotFound();
      }
    }

    [HttpPost]
    [Route("{id}/hardware-reservations")]
    public IActionResult AddHardwareReservation(Guid id, [FromBody] HardwareComponent[] hardwareComponents)

    {
      var hardwareReservation = _HardwareReservationService.AddHardwareReservation(id, hardwareComponents);
      return Ok(hardwareReservation);
    }
  }
}