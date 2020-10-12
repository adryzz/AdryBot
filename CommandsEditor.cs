using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdryBotGUI
{
    public partial class CommandsEditor : Form
    {
        SerializableDictionary<string, string> Commands;

        public CommandsEditor()
        {
            InitializeComponent();
            Commands = SimpleCommands.Commands;
        }

        private void CommandsEditor_Load(object sender, EventArgs e)
        {
            foreach(KeyValuePair<string, string> s in Commands)
            {
                listBox1.Items.Add(s.Key);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var v = new NewCommand();
            v.ShowDialog();
            if (v.IsOK)
            {
                Commands.Add(v.Key, v.Value);
                RefreshCommands();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;
            var pair = Commands.ElementAt(listBox1.SelectedIndex);
            var v = new NewCommand(pair.Key, pair.Value);
            v.ShowDialog();
            if (v.IsOK)
            {
                Commands.Remove(pair.Key);
                Commands.Add(v.Key, v.Value);
                RefreshCommands();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var pair = Commands.ElementAt(listBox1.SelectedIndex);
            Commands.Remove(pair.Key);
            RefreshCommands();
        }

        void RefreshCommands()
        {
            listBox1.Items.Clear();
            foreach (KeyValuePair<string, string> s in Commands)
            {
                listBox1.Items.Add(s.Key);
            }
        }

        private void CommandsEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SimpleCommands.Commands = Commands;
            SimpleCommands.Save("commands.xml");
        }
    }
}
