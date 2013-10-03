using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkypeBot.Handlers;
using SKYPE4COMLib;
using SkypeBot.Forms;

namespace SkypeBot.Forms
{
    public partial class UserController : Form
    {
        private Skype skype;
        private SendMessage sendMSG;

        public UserController()
        {
            skype = new Skype();
            InitializeComponent();
            listBox1.DataSource = Program._users;
            listBox2.DataSource = UserListHandler.Chats;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            sendMSG = new SendMessage(this.listBox1.SelectedItem.ToString());
            sendMSG.ShowDialog();
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                sendMSG = new SendMessage(this.listBox1.SelectedItem.ToString());
                sendMSG.ShowDialog(this);
            }
        }
    }
}
