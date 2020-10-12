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
    public partial class NewCommand : Form
    {
        public bool IsOK = false;
        public string Key = "";
        public string Value = "";
        public NewCommand(string v1, string v2)
        {
            InitializeComponent();
            textBox1.Text = v1;
            textBox2.Text = v2;
        }

        public NewCommand()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 1 && textBox2.TextLength > 1)
            {
                Key = textBox1.Text;
                Value = textBox2.Text;
                IsOK = true;
                Close();
            }
        }
    }
}
