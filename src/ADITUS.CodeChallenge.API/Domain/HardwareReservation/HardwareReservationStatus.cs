using System.ComponentModel.DataAnnotations;

namespace ADITUS.CodeChallenge.API.Domain
{
  [Flags]
  public enum HardwareReservationStatus
  {
    [Display(Name = "Pending")]
    Pending = 1,

    [Display(Name = "Approved")]
    Approved = 2
  }
}
