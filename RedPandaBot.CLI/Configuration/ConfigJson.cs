namespace RedPandaBot.Configuration
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("botname")]
        public string BotName { get; private set; }

        [JsonProperty("botavatarurl")]
        public string BotAvatarUrl { get; private set; }
    }
}
