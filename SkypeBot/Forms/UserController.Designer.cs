namespace SkypeBot.Forms
{
    partial class UserController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserController));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.skypePage = new System.Windows.Forms.TabPage();
            this.PLACEHOLDER = new System.Windows.Forms.Panel();
            this.skypeName = new System.Windows.Forms.Label();
            this.onlineStatus = new System.Windows.Forms.Label();
            this.contatcsPage = new System.Windows.Forms.TabPage();
            this.ignorePage = new System.Windows.Forms.TabPage();
            this.groupChatPage = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chat_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chat_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.skypePage.SuspendLayout();
            this.contatcsPage.SuspendLayout();
            this.ignorePage.SuspendLayout();
            this.groupChatPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(470, 380);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 0;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(3, 3);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(470, 380);
            this.listBox2.Sorted = true;
            this.listBox2.TabIndex = 1;
            this.listBox2.DoubleClick += new System.EventHandler(this.listBox2_DoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.skypePage);
            this.tabControl1.Controls.Add(this.contatcsPage);
            this.tabControl1.Controls.Add(this.ignorePage);
            this.tabControl1.Controls.Add(this.groupChatPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 412);
            this.tabControl1.TabIndex = 5;
            // 
            // skypePage
            // 
            this.skypePage.BackColor = System.Drawing.Color.White;
            this.skypePage.Controls.Add(this.PLACEHOLDER);
            this.skypePage.Controls.Add(this.skypeName);
            this.skypePage.Controls.Add(this.onlineStatus);
            this.skypePage.Location = new System.Drawing.Point(4, 22);
            this.skypePage.Name = "skypePage";
            this.skypePage.Size = new System.Drawing.Size(476, 386);
            this.skypePage.TabIndex = 2;
            this.skypePage.Text = "Skype";
            // 
            // PLACEHOLDER
            // 
            this.PLACEHOLDER.BackColor = System.Drawing.Color.Gray;
            this.PLACEHOLDER.Location = new System.Drawing.Point(230, 3);
            this.PLACEHOLDER.Name = "PLACEHOLDER";
            this.PLACEHOLDER.Size = new System.Drawing.Size(50, 50);
            this.PLACEHOLDER.TabIndex = 7;
            this.PLACEHOLDER.Visible = false;
            // 
            // skypeName
            // 
            this.skypeName.AutoSize = true;
            this.skypeName.Location = new System.Drawing.Point(8, 62);
            this.skypeName.Name = "skypeName";
            this.skypeName.Size = new System.Drawing.Size(175, 13);
            this.skypeName.TabIndex = 6;
            this.skypeName.Text = "Currently logged in as {skypeName}";
            // 
            // onlineStatus
            // 
            this.onlineStatus.AutoSize = true;
            this.onlineStatus.Font = new System.Drawing.Font("Monotype.com", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineStatus.Location = new System.Drawing.Point(3, 3);
            this.onlineStatus.Name = "onlineStatus";
            this.onlineStatus.Size = new System.Drawing.Size(221, 30);
            this.onlineStatus.TabIndex = 5;
            this.onlineStatus.Text = "Onlinestatus:";
            // 
            // contatcsPage
            // 
            this.contatcsPage.Controls.Add(this.listBox1);
            this.contatcsPage.Location = new System.Drawing.Point(4, 22);
            this.contatcsPage.Name = "contatcsPage";
            this.contatcsPage.Padding = new System.Windows.Forms.Padding(3);
            this.contatcsPage.Size = new System.Drawing.Size(476, 386);
            this.contatcsPage.TabIndex = 0;
            this.contatcsPage.Text = "All Contacts";
            this.contatcsPage.UseVisualStyleBackColor = true;
            // 
            // ignorePage
            // 
            this.ignorePage.Controls.Add(this.listBox2);
            this.ignorePage.Location = new System.Drawing.Point(4, 22);
            this.ignorePage.Name = "ignorePage";
            this.ignorePage.Padding = new System.Windows.Forms.Padding(3);
            this.ignorePage.Size = new System.Drawing.Size(476, 386);
            this.ignorePage.TabIndex = 1;
            this.ignorePage.Text = "Ignored Chats";
            this.ignorePage.UseVisualStyleBackColor = true;
            // 
            // groupChatPage
            // 
            this.groupChatPage.Controls.Add(this.listView1);
            this.groupChatPage.Location = new System.Drawing.Point(4, 22);
            this.groupChatPage.Name = "groupChatPage";
            this.groupChatPage.Size = new System.Drawing.Size(476, 386);
            this.groupChatPage.TabIndex = 3;
            this.groupChatPage.Text = "Group Chats";
            this.groupChatPage.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chat_name,
            this.chat_id});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(476, 386);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // chat_name
            // 
            this.chat_name.Text = "Chat";
            this.chat_name.Width = 253;
            // 
            // chat_id
            // 
            this.chat_id.Text = "ID";
            this.chat_id.Width = 219;
            // 
            // UserController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 412);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 450);
            this.Name = "UserController";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skype";
            this.tabControl1.ResumeLayout(false);
            this.skypePage.ResumeLayout(false);
            this.skypePage.PerformLayout();
            this.contatcsPage.ResumeLayout(false);
            this.ignorePage.ResumeLayout(false);
            this.groupChatPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private void LoadStaticPictureBox()
        {
            pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            pictureBox1.BackColor = System.Drawing.Color.White;
            pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            pictureBox1.Image = global::SkypeBot.Properties.Resources.skype_Offline;
            pictureBox1.Location = new System.Drawing.Point(230, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(50, 50);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            skypePage.Controls.Add(pictureBox1);
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
        }
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage skypePage;
        private System.Windows.Forms.Label skypeName;
        private System.Windows.Forms.Label onlineStatus;
        private System.Windows.Forms.TabPage contatcsPage;
        private System.Windows.Forms.TabPage ignorePage;
        public static System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel PLACEHOLDER;
        private System.Windows.Forms.TabPage groupChatPage;
        public System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ColumnHeader chat_name;
        private System.Windows.Forms.ColumnHeader chat_id;
        private System.Windows.Forms.ListView listView1;
    }
}