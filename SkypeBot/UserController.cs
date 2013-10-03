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

namespace SkypeBot
{
    public partial class UserController : Form
    {
        private SendMessage sendMSG;

        public UserController()
        {
            InitializeComponent();
            listBox1.DataSource = Program._users;
            listBox2.DataSource = UserListHandler.Chats;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            sendMSG = new Forms.SendMessage(this.listBox1.SelectedItem.ToString());
            Skype skype = new Skype();
            sendMSG.ShowDialog();
        }
    }
}
