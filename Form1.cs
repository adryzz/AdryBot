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
    public partial class Form1 : Form
    {
        public AdryBot Bot = new AdryBot();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("commands.xml"))
            {
                SimpleCommands.FromFile("commands.xml");
            }
            else
            {
                SimpleCommands.Save("commands.xml");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigurationEditor editor = new ConfigurationEditor(TopMost);
            editor.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bot.LogIn();
            Bot.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bot.LogOut();
        }

        private void clearLogButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        public void Log(string message)
        {
            if (!listBox1.InvokeRequired)
            {
                listBox1.Items.Add(message);
                if (autoScrollCheckBox.Checked)
                {
                    int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
                    listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
                }
            }
            else
            {
                listBox1.Invoke(new LogMessage(() => {
                    listBox1.Items.Add(message);
                    if (autoScrollCheckBox.Checked)
                    {
                        int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
                        listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
                    }
                }));
            }
        }

        delegate void LogMessage();

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Bot.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CommandsEditor editor = new CommandsEditor();
            editor.ShowDialog();
        }

        private void saveLogButton_Click(object sender, EventArgs e)
        {
            string text = "";
            foreach(object o in listBox1.Items)
            {
                text += o.ToString() + "\n";
            }
            var res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, text);
            }
        }

        private void topMostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostCheckBox.Checked;
        }
    }
}
