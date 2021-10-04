namespace RedPandaBot.CLI;

public class DiscordBot : IHostedService
{
    private InteractivityExtension? Interactivity { get; }
    private SlashCommandsExtension? SlashCommands { get; }
    private static readonly EventId BotEventId = new(1);
    private readonly DiscordShardedClient ShardedClient;

    public DiscordBot(Config config, ILoggerFactory loggerFactory)
    {
        ShardedClient = new(new()
        {
            Token = config.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            LoggerFactory = loggerFactory,
            Intents = DiscordIntents.AllUnprivileged,
        });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await RunAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Log.Fatal($"{ex.Message} {Environment.NewLine} {ex.StackTrace}");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("Stopping the Discord bot...");
        await ShardedClient.StopAsync();
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
        await ShardedClient.UseInteractivityAsync(new()
        {
            Timeout = TimeSpan.FromSeconds(60),
            PollBehaviour = PollBehaviour.KeepEmojis,
            PaginationBehaviour = PaginationBehaviour.WrapAround,
            ButtonBehavior = ButtonPaginationBehavior.Disable,
            ResponseBehavior = InteractionResponseBehavior.Respond,
            ResponseMessage = "Invalid response.",
            AckPaginationButtons = true,
            PaginationButtons = new()
            {
                SkipLeft = new(ButtonStyle.Primary, "first", "First"),
                Left = new(ButtonStyle.Success, "left", "Left"),
                Stop = new(ButtonStyle.Danger, "stop", "Stop"),
                Right = new(ButtonStyle.Success, "right", "Right"),
                SkipRight = new(ButtonStyle.Primary, "last", "Last"),
            }
        });

        var slashCommandsConfig = await ShardedClient.UseSlashCommandsAsync();

        // Command registration will go here

        // Client, Slash Command and error event binding will happen here

        Log.Information("Connecting to Discord..");
        await ShardedClient.StartAsync();

        Log.Information("Connected to Discord successfully!");
        Log.Information($"Bot version {Assembly.GetExecutingAssembly().GetName().Version}");
    }
}
