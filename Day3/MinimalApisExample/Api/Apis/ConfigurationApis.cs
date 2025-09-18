namespace Api.Apis;

public static class ConfigurationApis
{
    public static void MapConfigurationApis(this IEndpointRouteBuilder app)
    {
        var configGroup = app.MapGroup("/api/config");
        configGroup.MapGet("", GetConfiguration);
    }

    static string? GetConfiguration(IConfiguration configuration)
    {
        var valueFromConfig = configuration["MyConfigYV"];
        return valueFromConfig;
    }

    //1. appsettings.json
    //2. appsettings.{ENVNAME}.json
    //3. environment variables
    //4. command line args

}
