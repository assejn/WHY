using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Discord.Addons.Interactive;
using System.Reflection;
using System.IO;

namespace _5thWheel_Bot
{
    public class BotManager
    {
        public static DiscordSocketClient BotClient;
        public static CommandService Commands;
        public static IServiceProvider Services;
        public const string prefix = "!";

        public async Task RunBot()
        {
            BotClient = new DiscordSocketClient();
            await BotClient.LoginAsync(Discord.TokenType.Bot, Token.GetToken());
            await BotClient.StartAsync();
            BotClient.Log += BotLog;
            BotClient.Ready += BotIsReady;
            Services = ConfigureServices();
            Commands = new CommandService();
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);



            BotClient.MessageReceived += AsyncCommands;

            await Task.Delay(-1);
        }

        public Task BotLog(LogMessage message)
        {
            Console.Write("Log: " + message);
            return Task.CompletedTask;
        }

        public IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(BotClient)
                .AddSingleton<InteractiveService>()
                .AddSingleton<Commands>()
                .BuildServiceProvider();
        }

        public async Task BotIsReady()
        {
            await BotClient.SetGameAsync("lmaoo");
        }

        public async Task AsyncCommands(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;
            int prefixPosition = 0;
            if (message.HasStringPrefix(prefix, ref prefixPosition))
            {
                SocketCommandContext context = new SocketCommandContext(BotClient, message);
                IResult result = await Commands.ExecuteAsync(context, prefixPosition, Services);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}