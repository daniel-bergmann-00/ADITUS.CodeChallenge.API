namespace ADITUS.CodeChallenge.API.Exception
{
  public class ApiStatisticException : HttpRequestException
  {
    public ApiStatisticException(string message) : base(message) { }
  }
}
