using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using UHQKEK.Start;

namespace UHQKEK.Discordb
{
    class DiscordBotr
    {
        public static void MainDC() => new DiscordBotr().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        //private CommandService _commands;
        public static string Prefix = "#";
        //private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            string token = "OTI1Nzk2NDM0MjAzMjcxMTc4.YcyVHg.iDyhpUqAnQCscdHYvywtR7CK4p0";
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await _client.SetGameAsync($"{Prefix}help");
            Thread.Sleep(250);
            await Task.Delay(-1);
        }
        private static Task CommandHandler(SocketMessage message)
        {
            string command = "";
            var lengthOfCommand = -1;

            if (!message.Content.StartsWith(Prefix))
                return Task.CompletedTask;

            if (message.Author.IsBot)
                return Task.CompletedTask;

            if (message.Content.Contains(" "))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;

            command = message.Content.ToLower();
            command = message.Content.Replace("#", "");

            if (command.Equals("stats"))
            {
                string DiscordREALID = message.Author.ToString();
                if (Settings.config.DiscordID == DiscordREALID)
                {
                    EmbedBuilder embedBuilder = new EmbedBuilder();
                    embedBuilder.AddField("Hits", (object)Program.Good, true);
                    embedBuilder.AddField("Frees", (object)Program.Free, true);
                    embedBuilder.AddField("Bads", (object)Program.Fail, true);
                    embedBuilder.AddField("Checked", (object)Program.Check, true);
                    embedBuilder.AddField("Error", (object)Program.Error, true);
                    embedBuilder.AddField("Cpm", (object)Program.Cps, true);
                    embedBuilder.AddField("Modules Selected", (object)string.Join(", ", Program.ModuleName, true));
                    embedBuilder.WithThumbnailUrl("");
                    embedBuilder.WithColor(Discord.Color.Green);
                    message.Channel.SendMessageAsync("", false, embedBuilder.Build());
                }
            }
            if (command.Equals("help"))
            {
                string DiscordREALID = message.Author.ToString();
                if (Settings.config.DiscordID == DiscordREALID)
                {
                    var builder = new EmbedBuilder();
                    builder.WithTitle("DIVINE AIO • Help");
                    builder.WithDescription(
                        "**#help** - Shows a list of Commands!\n**#stats** - Shows Checker Stats. Hits, Fails, ect. \n **#exit** - Stops / Closes The Keker and Saves Remaining Lines");
                    builder.WithColor(Discord.Color.Purple);
                    builder.WithFooter("Coded By CrackerShadow#2814 • DIVINE Discord -> https://discord.gg/ngtEsrvWqe", "");
                    message.Channel.SendMessageAsync("", false, builder.Build());
                }
            }
            if (command.Equals("exit"))
            {
                string DiscordREALID = message.Author.ToString();
                if (Settings.config.DiscordID == DiscordREALID)
                {
                    EmbedBuilder embedBuilder = new EmbedBuilder();
                    embedBuilder.AddField("Success", (object)"DIVINE AIO is now closed...", true);
                    embedBuilder.WithThumbnailUrl("");
                    embedBuilder.WithColor(Color.Green);
                    embedBuilder.WithFooter("Coded By CrackerShadow#2814 • DIVINE Discord -> https://discord.gg/ngtEsrvWqe", "");
                    AutoSave.manualsave();
                    Environment.Exit(0);
                }
            }
            return Task.CompletedTask;
        }
    }
}
