using CompleteConsoleApp.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class FooService : IFooService
{
    private readonly ILogger<FooService> _logger;
    private readonly TestSettings _testSettings;

    public FooService(IOptions<TestSettings> options, ILoggerFactory loggerFactory)
    //public FooService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<FooService>();

        _testSettings = options.Value;
    }

    public void DoThing(int number)
    {
        _logger.LogWarning(_testSettings.SecondSetting);

        _logger.LogInformation($"Doing the thing {number}");
    }
}