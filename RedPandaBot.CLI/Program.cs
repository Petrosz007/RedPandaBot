using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPandaBot.CLI
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var bot = new RedPandaBot();
            try
            {
                await bot.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal($"{ex.Message} | {ex.StackTrace}");
            }
        }

    }
}
