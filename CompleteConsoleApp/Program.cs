using CompleteConsoleApp.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


// configure serilog - 
Log.Logger = new LoggerConfiguration()
    //.ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build())
    // ^--  this should be loaded in through the host context
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

// we are using the .NET Generic Host builder
// dotnet add package Microsoft.Extensions.Hosting
// -----
// https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=hostbuilder
// https://stackoverflow.com/questions/68392429/how-to-run-net-core-console-app-using-generic-host-builder
// https://dfederm.com/building-a-console-app-with-.net-generic-host/
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {

        services.AddOptions<TestSettings>().Configure<IConfiguration>((options, configuration) => configuration.GetSection(nameof(TestSettings)).Bind(options));

        services.AddSingleton<IBarService, BarService>();
        services.AddSingleton<IFooService, FooService>();

    })
    .UseSerilog()
    .Build();

var service = host.Services.GetRequiredService<IBarService>();
service.DoSomeRealWork();

// shouldn't need to use host.Run() here because we actually want it to spin down once completed