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

        public UserController()
        {
            skype = new Skype();
            InitializeComponent();
            listBox1.DataSource = Program._users;
            listBox2.DataSource = UserListHandler.Chats;

            UserListHandler.UpdateOnlineStatus(skype.CurrentUser, skype.CurrentUser, skype.CurrentUser.OnlineStatus);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string SkypeName = FindSkypeName(this.listBox1.SelectedItem.ToString());
                if (SkypeName != "not found")
                {
                    new Thread(() => new SendMessage(SkypeName).ShowDialog()).Start();
                }
                else 
                {
                    string ErrorMsg = string.Format("Couldnt find the skypename of {0}", listBox1.SelectedItem.ToString());
                    MessageBox.Show(ErrorMsg, "Fatal Error",MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string SkypeName = FindSkypeName(this.listBox1.SelectedItem.ToString());
                if (SkypeName != "not found")
                {
                    new Thread(() => new SendMessage(SkypeName).ShowDialog()).Start();
                }
                else 
                {
                    string ErrorMsg = string.Format("Couldnt find the skypename of {0}", listBox1.SelectedItem.ToString());
                    MessageBox.Show(ErrorMsg, "Fatal Error",MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private string FindSkypeName(string FullName)
        {
            for (int i = 0; i < Program.UsersArray.Length / 2; i++)
            {
                if (Program.UsersArray[i,0] ==FullName)
                    return Program.UsersArray[i,1];
            }
            return "not found";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new Thread(() => new ChangeOnlineStatus().ShowDialog()).Start();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = DefaultBackColor;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
        }
    }
}
