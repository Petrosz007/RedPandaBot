using Microsoft.Extensions.Configuration;

namespace RedPandaBot.CLI.Configuration;

public record Config
{
    public string Token { get; init; }

    public string BotName { get; init; }

    public string BotAvatarUrl { get; init; }
    public ulong TestServerId { get; init; }

    public Config(IConfiguration configuration)
    {
        Token = configuration["token"];
        BotName = configuration["botname"];
        BotAvatarUrl = configuration["botavatarurl"];
        TestServerId = ulong.Parse(configuration["testServerId"]);
    }
}
