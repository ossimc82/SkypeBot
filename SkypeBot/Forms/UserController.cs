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
using SkypeBot.Forms.Subforms;

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

            if (skype.CurrentUser.OnlineStatus == TOnlineStatus.olsOnline)
                Forms.UserController.pictureBox1.Image = global::SkypeBot.Properties.Resources.skype_Online;
            else if (skype.CurrentUser.OnlineStatus == TOnlineStatus.olsAway)
                Forms.UserController.pictureBox1.Image = global::SkypeBot.Properties.Resources.skype_Away;
            else if (skype.CurrentUser.OnlineStatus == TOnlineStatus.olsDoNotDisturb)
                Forms.UserController.pictureBox1.Image = global::SkypeBot.Properties.Resources.skype_DND;
            else if (skype.CurrentUser.OnlineStatus == TOnlineStatus.olsUnknown)
                Forms.UserController.pictureBox1.Image = global::SkypeBot.Properties.Resources.skype_Invisible;
            else if (skype.CurrentUser.OnlineStatus == TOnlineStatus.olsOffline)
                Forms.UserController.pictureBox1.Image = global::SkypeBot.Properties.Resources.skype_Offline;
            else { Writer.WriteWarningln("Unknown onlinestatus: " + skype.CurrentUser.OnlineStatus.ToString()); }
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
