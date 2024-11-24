using System.Text.Json.Serialization;
using ADITUS.CodeChallenge.API.Services;
using ADITUS.CodeChallenge.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
{
  x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSingleton<IStatisticService, StatisticService>();
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<IHardwareReservationService, HardwareReservationService>();

builder.Services.Configure<StatisticApiSettings>(builder.Configuration.GetSection("StatisticApiSettings"));
builder.Services.AddHttpClient(HttpClientNames.StatisticApiClientName, (serviceProvider, client) =>
{
  var apiSettings = serviceProvider.GetRequiredService<IOptions<StatisticApiSettings>>().Value;
  client.BaseAddress = new Uri(apiSettings.BaseAddress);
  client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
