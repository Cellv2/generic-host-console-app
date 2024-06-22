using Microsoft.Extensions.Logging;

public class BarService(ILogger<BarService> logger, IFooService fooService) : IBarService
{
    public void DoSomeRealWork()
    {
        logger.LogInformation("BAWB, DO SUMTIN!");

        for (int i = 0; i < 10; i++)
        {
            fooService.DoThing(i);
        }

        logger.LogInformation("It is done");
    }
}
