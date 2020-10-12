using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;

namespace AdryBotGUI
{
    public class AdryBot : IDisposable
    {
        protected DiscordSocketClient Client;
        public Configuration Config = new Configuration();
        protected CommandHandler Handler = new CommandHandler();

        public AdryBot()
        {
            if (System.IO.File.Exists("config.xml"))
            {
                Config = Configuration.FromFile("config.xml");
            }
            else
            {
                Config = new Configuration();
                Config.Save("config.xml");
            }

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            Client.Log += Client_Log;
            Client.Connected += Client_Connected;
            Client.Disconnected += Client_Disconnected;
            //Client.MessageReceived += Client_MessageReceived;
        }

        public void LogIn()
        {
            if (Config.Token == "" || Config.Token == null) { throw new InvalidOperationException("The token isn't present in the configuration file."); }
            Client.LoginAsync(TokenType.Bot, Config.Token);
            Handler = new CommandHandler();
            Handler.InitialiseAsync(Client);
        }

        public void LogOut()
        {
            if (Config.Token == "" || Config.Token == null) { return; }
            Client.LogoutAsync();
            Handler = null;
        }

        public void Start()
        {
            Client.StartAsync();
            Client.SetStatusAsync(Config.Status);
            Client.SetGameAsync(Config.Game, Config.Url);
        }

        public void Refresh()
        {
            Client.SetStatusAsync(Config.Status);
            Client.SetGameAsync(Config.Game, Config.Url);
        }

        private Task Client_MessageReceived(SocketMessage arg)
        {
            Program.Form.Log("Message");
            if (arg.Content.StartsWith(Config.CmdPrefix))
            {
                arg.Channel.SendMessageAsync("no");
            }
            return Task.CompletedTask;
        }

        private Task Client_Disconnected(Exception arg)
        {
            Program.Form.Log("Disconnected");
            return Task.CompletedTask;
        }

        private Task Client_Connected()
        {
            Program.Form.Log("Connected");
            return Task.CompletedTask;
        }

        private Task Client_Log(LogMessage arg)
        {
            Program.Form.Log(arg.Message);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
