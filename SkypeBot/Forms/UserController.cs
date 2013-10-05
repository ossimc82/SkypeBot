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
using System.Threading;

namespace SkypeBot.Forms
{
    public partial class UserController : Form
    {
        private Skype skype;
        //ListViewItem _chatName;
        //List<string> _chatNameString;

        public UserController()
        {
            skype = new Skype();
            InitializeComponent();
            LoadStaticPictureBox();
            this.skypeName.Text = String.Format("Currently logged in as: {0}", skype.CurrentUser.FullName);
            listBox1.DataSource = Program._users;
            listBox2.DataSource = UserListHandler.Chats;
            listView1.Sorting = SortOrder.Descending;

            for (int i = 0; i < Program._FriendlyName.Count; i++)
            {
                ListViewItem _chats = new ListViewItem(Program._FriendlyName[i]);
                _chats.SubItems.Add(Program._Name[i]);
                listView1.Items.Add(_chats);
            }
            
            UserListHandler.UpdateOnlineStatus(skype.CurrentUser, skype.CurrentUser, skype.CurrentUser.OnlineStatus, pictureBox1);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string SkypeName = FindSkypeName(this.listBox1.SelectedItem.ToString());
            string DisplayName = this.listBox1.SelectedItem.ToString();

            if (SkypeName != "not found")
                new Thread(() => new SendMessage(SkypeName, DisplayName).ShowDialog()).Start();
            else
                MessageBox.Show(String.Format("Couldnt find the skypename of {0}", listBox1.SelectedItem.ToString()), "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            string id = listBox2.SelectedItem.ToString();
            new Thread(() => new IgnoredChatsHandleForm(id).ShowDialog()).Start();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string SkypeName = FindSkypeName(this.listBox1.SelectedItem.ToString());
                string DisplayName = this.listBox1.SelectedItem.ToString();

                if (SkypeName != "not found")
                {
                    new Thread(() => new SendMessage(SkypeName, DisplayName).ShowDialog()).Start();
                }
                else
                {
                    string ErrorMsg = string.Format("Couldnt find the skypename of {0}", listBox1.SelectedItem.ToString());
                    MessageBox.Show(ErrorMsg, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private string FindSkypeName(string FullName)
        {
            for (int i = 0; i < Program.UsersArray.Length / 2; i++)
            {
                if (Program.UsersArray[i, 0] == FullName)
                    return Program.UsersArray[i, 1];
            }
            return "not found";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new Thread(() => new ChangeOnlineStatus(skype.CurrentUser.OnlineStatus).ShowDialog()).Start();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ossimc82/SkypeBot");
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listView1.SelectedItems)
            {
                new Thread(() => new SendMessage(i.SubItems[1].Text, i.SubItems[0].Text).ShowDialog()).Start();
            }
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            string id = listBox2.SelectedItem.ToString();

            if (e.KeyCode == Keys.Enter)
                new Thread(() => new IgnoredChatsHandleForm(id).ShowDialog()).Start();
            else { }
        }

        public void RefreshContent()
        {
            listBox1.ClearSelected();
            listBox2.ClearSelected();
            listBox1.DataSource = Program._users;
            listBox2.DataSource = UserListHandler.Chats;
        }
    }
}
