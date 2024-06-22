using CompleteConsoleApp.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class FooService(ILogger<FooService> logger, IOptions<TestSettings> testSettingsOptions) : IFooService
{
    private readonly TestSettings _testSettings = testSettingsOptions.Value;

    public void DoThing(int number)
    {
        logger.LogWarning(_testSettings.SecondSetting);

        logger.LogInformation($"Doing the thing {number}");
    }
}