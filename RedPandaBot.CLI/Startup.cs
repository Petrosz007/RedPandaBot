using Microsoft.Extensions.Configuration;

namespace RedPandaBot.CLI;

internal static class Startup
{
    public static IHost CreateHost(string[] args)
    {
        SetupLogger();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(context => 
                context
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build()
            )
            .ConfigureServices(ConfigureServices)
            .UseSerilog()
            .Build();

        return host;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<Config>();
        services.AddHostedService<DiscordBot>();
    }

    private static void SetupLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(theme: SystemConsoleTheme.Literate, restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.File("log.txt",
                outputTemplate: "{Timestamp:dd MMM yyyy - hh:mm:ss tt} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .MinimumLevel.Debug()
            .CreateLogger();
    }
}
