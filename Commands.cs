using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Discord.Addons.Interactive;
using System.Reflection;
using System.IO;
using System.Linq;


namespace _5thWheel_Bot
{
    public class Commands : InteractiveBase<SocketCommandContext>
    {
        [Command("setbday")]
        public async Task GetUserInfo()
        {
            await ReplyAsync("Name: ");
            var userName = await NextMessageAsync();
            if (userName != null)
                await ReplyAsync("Name: " + userName);

            await ReplyAsync("Birthday (DD/MM/YYYY): ");
            var userDate = await NextMessageAsync();
            if (userDate != null)
                await ReplyAsync("Birthday: " + userDate);
        }
    }
}
