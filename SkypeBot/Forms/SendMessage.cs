using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKYPE4COMLib;

namespace SkypeBot.Forms
{
    public partial class SendMessage : Form
    {
        private Skype skype;
        private string name;

        public SendMessage(string username)
        {
            skype = new Skype();
            InitializeComponent();
            this.name = username;
            this.Text = String.Format("Send Message to {0}", name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skype.SendMessage(name, textBox1.Text);
        }
    }
}
