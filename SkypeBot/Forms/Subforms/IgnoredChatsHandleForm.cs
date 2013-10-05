using SkypeBot.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkypeBot.Forms.Subforms
{
    public partial class IgnoredChatsHandleForm : Form
    {
        private string id;

        public IgnoredChatsHandleForm(string chatID)
        {
            InitializeComponent();
            this.id = chatID;
            this.Text = chatID;
            this.label1.Text = String.Format("Selected chat: {0}", chatID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileHandler.Replace(id);
            new UserController().RefreshContent();
        }
    }
}
