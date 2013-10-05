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
using System.Threading;

namespace SkypeBot.Forms.Subforms
{
    public partial class ChangeOnlineStatus : Form
    {
        private Skype skype;

        public ChangeOnlineStatus()
        {
            skype = new Skype();
            InitializeComponent();
            status.Text = String.Format("OnlineStatus: {0}", skype.CurrentUser.OnlineStatus).Replace("ols", String.Empty);
        }

        private void online_Click(object sender, EventArgs e)
        {
            skype.ChangeUserStatus(TUserStatus.cusOnline);
            status.Text = String.Format("OnlineStatus: {0}", skype.CurrentUser.OnlineStatus).Replace("ols", String.Empty);
        }

        private void away_Click(object sender, EventArgs e)
        {
            skype.ChangeUserStatus(TUserStatus.cusAway);
            status.Text = String.Format("OnlineStatus: {0}", skype.CurrentUser.OnlineStatus).Replace("ols", String.Empty);
        }

        private void dnd_Click(object sender, EventArgs e)
        {
            skype.ChangeUserStatus(TUserStatus.cusDoNotDisturb);
            status.Text = String.Format("OnlineStatus: {0}", skype.CurrentUser.OnlineStatus).Replace("ols", String.Empty);
        }

        private void invisible_Click(object sender, EventArgs e)
        {
            skype.ChangeUserStatus(TUserStatus.cusInvisible);
            status.Text = String.Format("OnlineStatus: {0}", skype.CurrentUser.OnlineStatus).Replace("ols", String.Empty).Replace("Unknown", "Invisible");
        }

        private void offline_Click(object sender, EventArgs e)
        {
            skype.ChangeUserStatus(TUserStatus.cusOffline);
            status.Text = String.Format("OnlineStatus: {0}", skype.CurrentUser.OnlineStatus).Replace("ols", String.Empty);
        }

        private void offline_MouseMove(object sender, MouseEventArgs e)
        {
            offline.BackColor = Color.Gray;
        }

        private void online_MouseMove(object sender, MouseEventArgs e)
        {
            online.BackColor = Color.Gray;
        }

        private void away_MouseMove(object sender, MouseEventArgs e)
        {
            away.BackColor = Color.Gray;
        }

        private void dnd_MouseMove(object sender, MouseEventArgs e)
        {
            dnd.BackColor = Color.Gray;
        }

        private void invisible_MouseMove(object sender, MouseEventArgs e)
        {
            invisible.BackColor = Color.Gray;
        }

        private void offline_MouseLeave(object sender, EventArgs e)
        {
            offline.BackColor = DefaultBackColor;
        }

        private void invisible_MouseLeave(object sender, EventArgs e)
        {
            invisible.BackColor = DefaultBackColor;
        }

        private void dnd_MouseLeave(object sender, EventArgs e)
        {
            dnd.BackColor = DefaultBackColor;
        }

        private void away_MouseLeave(object sender, EventArgs e)
        {
            away.BackColor = DefaultBackColor;
        }

        private void online_MouseLeave(object sender, EventArgs e)
        {
            online.BackColor = DefaultBackColor;
        }
    }
}
