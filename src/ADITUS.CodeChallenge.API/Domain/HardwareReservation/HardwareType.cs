using System.ComponentModel.DataAnnotations;

namespace ADITUS.CodeChallenge.API.Domain
{
  [Flags]
  public enum HardwareType
  {
    [Display(Name = "Turnstile")]
    Turnstile = 1,

    [Display(Name = "WirelessHandheldScanner")]
    WirelessHandheldScanner = 2,

    [Display(Name = "MobileScanningTerminal")]
    MobileScanningTerminal = 3,
  }
}
