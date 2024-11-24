namespace ADITUS.CodeChallenge.API.BusinessException
{
  public class HardwareReservationAlreadyExistsException : HardwareReservationException
  {
    public HardwareReservationAlreadyExistsException() : base("Hardware reservation already exists.") { }
  }
}
