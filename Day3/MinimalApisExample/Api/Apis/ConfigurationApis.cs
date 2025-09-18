
using Api.Model.Config;
using Microsoft.Extensions.Options;

namespace Api.Apis;

//1. appsettings.json
//2. appsettings.{ENVNAME}.json
//3. user secrets (in development)
//4. environment variables
//5. command line args


public static class ConfigurationApis
{
    public static void MapConfigurationApis(this IEndpointRouteBuilder app)
    {
        var configGroup = app.MapGroup("/api/config");
        configGroup.MapGet("", GetConfiguration);
        configGroup.MapGet("myConfigNested", GetMyConfig);
        configGroup.MapGet("myConfigComplex", GetYVCollectionConfig);
        configGroup.MapGet("myConfigComplexSnapshot", GetYVCollectionConfigSnapshot);
        configGroup.MapGet("myConfigComplexMonitor", GetYVCollectionConfigSnapshotMonitor);
    }

    private static string GetMyConfig(IConfiguration configuration)
    {
        var valueFromConfig = configuration["YVCollection:Address:Port"];
        return valueFromConfig;
    }

    static string? GetConfiguration(IConfiguration configuration)
    {
        var valueFromConfig = configuration["MyConfigYV"];
        return valueFromConfig;
    }

    static YVCollectionConfig GetYVCollectionConfig(IOptions<YVCollectionConfig> yvCollectionConfig)
    {
        return yvCollectionConfig.Value;
    }
    static YVCollectionConfig GetYVCollectionConfigSnapshot(IOptionsSnapshot<YVCollectionConfig> yvCollectionConfig)
    {
        return yvCollectionConfig.Value;
    }

    static YVCollectionConfig GetYVCollectionConfigSnapshotMonitor(IOptionsMonitor<YVCollectionConfig> yvCollectionConfig)
    {
        yvCollectionConfig.OnChange(currentValue => {
            Console.WriteLine( currentValue.AllowUsers);
        });

        return yvCollectionConfig.CurrentValue;
    }


}
