using Discord.WebSocket;
using System;

namespace _5thWheel_Bot
{
    public class Program
    {
        static void Main(string[] args)
        {
            BotManager manager = new BotManager();
            manager.RunBot().GetAwaiter().GetResult();
        }
    }
}