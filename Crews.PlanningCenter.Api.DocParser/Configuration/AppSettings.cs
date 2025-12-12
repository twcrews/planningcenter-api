namespace Crews.PlanningCenter.Api.DocParser.Configuration;

class AppSettings
{
    public string? OutputDirectory { get; set; }
    public PlanningCenterClientOptions? PlanningCenterClient { get; set; }

    public class PlanningCenterClientOptions
    {
        public string? BaseAddress { get; set; }
    }
}
