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

namespace SkypeBot.Forms.Subforms
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
            try
            {
                skype.SendMessage(name, textBox1.Text);
                Writer.WriteSuccessln(String.Format("[{0}] Manual sendchat to \"{0}\": {2}", DateTime.Now, name, textBox1.Text));
            }
            catch (Exception ex)
            {
                Writer.WriteErrorln(ex.ToString());
                MessageBox.Show(String.Format("Please report it on github http://github.com/ossimc82/SkypeBot: \n\n{0}", ex.ToString()), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                button1.Enabled = false;
            }
            else
            {
                textBox1.BackColor = Color.Lime;
                button1.Enabled = true;
            }
        }
    }
}
