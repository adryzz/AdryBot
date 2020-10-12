using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdryBotGUI
{
    public class CommandHandler
    {
        DiscordSocketClient Client;
        CommandService Service;

        public async Task InitialiseAsync(DiscordSocketClient client)
        {
            Client = client;
            Service = new CommandService();
            await Service.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            Client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) { return; }
            var context = new SocketCommandContext(Client, msg);
            int argPos = 0;
            await SimpleCommands.Execute(context, s.Content);
            if (msg.HasStringPrefix(Program.Form.Bot.Config.CmdPrefix, ref argPos) || msg.HasMentionPrefix(Client.CurrentUser, ref argPos))
            {
                var result = await Service.ExecuteAsync(context, argPos, null);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Program.Form.Log(result.ErrorReason);
                }
            }
        }
    }
}
