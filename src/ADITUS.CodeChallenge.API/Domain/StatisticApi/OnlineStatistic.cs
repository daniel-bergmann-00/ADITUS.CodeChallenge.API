namespace ADITUS.CodeChallenge.API.Domain
{
  public record OnlineStatistic
  {
    public long Attendees { get; set; }
    public long Invites { get; set; }
    public long Visits { get; set; }
    public long VirtualRooms { get; set; }
  }
}
