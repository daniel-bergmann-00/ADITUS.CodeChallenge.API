using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public interface IHardwareReservationService
  {
    void ApproveHardwareReservation(Guid id);
    HardwareReservation GetHardwareReservation(Guid id);
    HardwareReservation AddHardwareReservation(Guid id, HardwareComponent[] hardwareComponents);
  }
}
