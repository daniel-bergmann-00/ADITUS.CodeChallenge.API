namespace ADITUS.CodeChallenge.API.BusinessException
{
  public class HardwareReservationNotEnoughStockException : HardwareReservationException
  {
    public HardwareReservationNotEnoughStockException() : base("Not enough stock available for the requested components.") { }
  }
}
