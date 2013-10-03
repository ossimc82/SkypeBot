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

namespace SkypeBot
{
    public partial class UserController : Form
    {
        public UserController()
        {
            InitializeComponent();
            listBox1.DataSource = Program._users;
            listBox2.DataSource = UserListHandler.Chats;
        }
    }
}
