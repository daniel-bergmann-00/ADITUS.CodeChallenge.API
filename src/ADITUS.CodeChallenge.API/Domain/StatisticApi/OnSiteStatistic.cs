namespace ADITUS.CodeChallenge.API.Domain
{
  public record OnSiteStatistic
  {
    public long VisitorsCount { get; set; }
    public long ExhibitorsCount { get; set; }
    public long BoothsCount { get; set; }
  }
}
