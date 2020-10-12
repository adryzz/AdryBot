using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace AdryBotGUI
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("rep", RunMode = RunMode.Async)]
        //[RequireBotPermission(Discord.GuildPermission.Administrator)]
        public async Task Reputation(string value, string id)
        {
            ulong uid;
            if (ulong.TryParse(id, out uid))
            {
                await Context.Channel.SendMessageAsync("i cant");
            }
        }

        [Command("help")]
        public async Task Help()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(Program.Form.Bot.Config.Color.R, Program.Form.Bot.Config.Color.G, Program.Form.Bot.Config.Color.B);
            embed.WithTitle("AdryBot");
            embed.WithUrl("https://github.com/adryzz/adrybot/");
            embed.WithDescription($"Adryzz's bot\nDo **{Program.Form.Bot.Config.CmdPrefix}commands** to see all the available commands!");
            embed.WithCurrentTimestamp();
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("log", RunMode = RunMode.Async)]
        public async Task Log([Remainder] string text)
        {
            if (Context.User.ToString().Equals(Program.Form.Bot.Config.BotOwner))
            {
                Program.Form.Log(text);
                await Context.Channel.SendMessageAsync("Logged!");
            }
            else
            {
                await Context.Channel.SendMessageAsync(":x:Insufficient Permissions:x:");
            }
        }

        [Command("Sudo")]
        public async Task Sudo([Remainder] string message)
        {
            if (Context.User.ToString().Equals(Program.Form.Bot.Config.BotOwner))
            {
                if (!Context.IsPrivate)
                {
                    await Context.Message.DeleteAsync();
                }
                await Context.Channel.SendMessageAsync(message);
            }
            else
            {
                await Context.Channel.SendMessageAsync(":x:Insufficient Permissions:x:");
            }
        }
    }
}
