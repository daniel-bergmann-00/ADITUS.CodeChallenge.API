namespace ADITUS.CodeChallenge.API.BusinessException
{
  public class HardwareReservationNotFoundException : Exception
  {
    public HardwareReservationNotFoundException(Guid id) : base(id.ToString()) { }
  }
}
