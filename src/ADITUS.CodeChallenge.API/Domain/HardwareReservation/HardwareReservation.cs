namespace ADITUS.CodeChallenge.API.Domain
{
  public record HardwareReservation

  {
    public Guid EventId { get; set; }
    public HardwareReservationStatus? Status { get; set; }
    public HardwareComponent[] ReservedHardware { get; set; }
  }
}
