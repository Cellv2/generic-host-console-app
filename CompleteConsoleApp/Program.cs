using CompleteConsoleApp.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");





var services = new ServiceCollection();
// optional if you want to read the config.
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

services.AddOptions();

services.AddOptions<TestSettings>().Configure<IConfiguration>((options, configuration) => configuration.GetSection(nameof(TestSettings)).Bind(options));

services.AddSingleton(configuration);

services.AddSingleton<IBarService, BarService>();
services.AddSingleton<IFooService, FooService>();

services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

using var sp = services.BuildServiceProvider();
var requiredService = sp.GetRequiredService<ILogger<Program>>();
//requiredService.LogError("Test");
requiredService.LogInformation("Test");

var svc = sp.GetService<IBarService>();
svc.DoSomeRealWork();

