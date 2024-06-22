using CompleteConsoleApp.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");





//var services = new ServiceCollection();
//// optional if you want to read the config.
//var configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json")
//    .Build();

//services.AddOptions();

//services.AddOptions<TestSettings>().Configure<IConfiguration>((options, configuration) => configuration.GetSection(nameof(TestSettings)).Bind(options));

//services.AddSingleton(configuration);

//services.AddSingleton<IBarService, BarService>();
//services.AddSingleton<IFooService, FooService>();

//services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

//using var sp = services.BuildServiceProvider();
//var requiredService = sp.GetRequiredService<ILogger<Program>>();
////requiredService.LogError("Test");
//requiredService.LogInformation("Test");

//var svc = sp.GetService<IBarService>();
//svc.DoSomeRealWork();



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
    .Build();

var service = host.Services.GetRequiredService<IBarService>();
service.DoSomeRealWork();

// shouldn't need to use host.Run() here because we actually want it to spin down once completed