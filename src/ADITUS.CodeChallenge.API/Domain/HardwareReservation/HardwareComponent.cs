namespace ADITUS.CodeChallenge.API.Domain
{
  public record HardwareComponent

  {
    public HardwareType Type { get; set; }
    public int Quantity { get; set; }
  }
}