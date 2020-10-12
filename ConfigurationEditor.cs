using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord;

namespace AdryBotGUI
{
    public partial class ConfigurationEditor : Form
    {
        public Configuration Config;
        bool top = false;
        public ConfigurationEditor(bool topmost)
        {
            InitializeComponent();
            Config = Program.Form.Bot.Config;
            tokenTextBox.Text = Config.Token;
            prefixTextBox.Text = Config.CmdPrefix;
            gameTextBox.Text = Config.Game;
            urlTextBox.Text = Config.Url;
            colorDialog1.Color = Config.Color;
            top = topmost;
            statusComboBox.SelectedIndex = (int)Config.Status;
            ownerTextBox.Text = Config.BotOwner;
        }

        private void ConfigurationEditor_Load(object sender, EventArgs e)
        {
            TopMost = top;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.Token = tokenTextBox.Text;
            cfg.CmdPrefix = prefixTextBox.Text;
            cfg.Game = gameTextBox.Text;
            cfg.Url = urlTextBox.Text;
            cfg.Color = colorDialog1.Color;
            cfg.Status = (UserStatus)statusComboBox.SelectedIndex;
            cfg.BotOwner = ownerTextBox.Text;
            var res = MessageBox.Show(this, "Restart bot client?", "ConfigurationEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
            {
                Program.Form.Bot.LogOut();
                Program.Form.Bot.Config = cfg;
                cfg.Save("config.xml");
                Program.Form.Bot.LogIn();
            }
            else
            {
                Program.Form.Bot.Config = cfg;
                cfg.Save("config.xml");
            }
            Program.Form.Bot.Refresh();
            Close();
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
    }
}
