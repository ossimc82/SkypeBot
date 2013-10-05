namespace SkypeBot.Forms.Subforms
{
    partial class ChangeOnlineStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeOnlineStatus));
            this.offline = new System.Windows.Forms.PictureBox();
            this.invisible = new System.Windows.Forms.PictureBox();
            this.dnd = new System.Windows.Forms.PictureBox();
            this.away = new System.Windows.Forms.PictureBox();
            this.online = new System.Windows.Forms.PictureBox();
            this.status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.offline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invisible)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.away)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.online)).BeginInit();
            this.SuspendLayout();
            // 
            // offline
            // 
            this.offline.Cursor = System.Windows.Forms.Cursors.Hand;
            this.offline.Image = global::SkypeBot.Properties.Resources.skype_Offline;
            this.offline.Location = new System.Drawing.Point(236, 12);
            this.offline.Name = "offline";
            this.offline.Size = new System.Drawing.Size(50, 50);
            this.offline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.offline.TabIndex = 4;
            this.offline.TabStop = false;
            this.offline.Click += new System.EventHandler(this.offline_Click);
            this.offline.MouseLeave += new System.EventHandler(this.offline_MouseLeave);
            this.offline.MouseMove += new System.Windows.Forms.MouseEventHandler(this.offline_MouseMove);
            // 
            // invisible
            // 
            this.invisible.Cursor = System.Windows.Forms.Cursors.Hand;
            this.invisible.Image = global::SkypeBot.Properties.Resources.skype_Invisible;
            this.invisible.Location = new System.Drawing.Point(180, 12);
            this.invisible.Name = "invisible";
            this.invisible.Size = new System.Drawing.Size(50, 50);
            this.invisible.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.invisible.TabIndex = 3;
            this.invisible.TabStop = false;
            this.invisible.Click += new System.EventHandler(this.invisible_Click);
            this.invisible.MouseLeave += new System.EventHandler(this.invisible_MouseLeave);
            this.invisible.MouseMove += new System.Windows.Forms.MouseEventHandler(this.invisible_MouseMove);
            // 
            // dnd
            // 
            this.dnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dnd.Image = global::SkypeBot.Properties.Resources.skype_DND;
            this.dnd.Location = new System.Drawing.Point(124, 12);
            this.dnd.Name = "dnd";
            this.dnd.Size = new System.Drawing.Size(50, 50);
            this.dnd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dnd.TabIndex = 2;
            this.dnd.TabStop = false;
            this.dnd.Click += new System.EventHandler(this.dnd_Click);
            this.dnd.MouseLeave += new System.EventHandler(this.dnd_MouseLeave);
            this.dnd.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dnd_MouseMove);
            // 
            // away
            // 
            this.away.Cursor = System.Windows.Forms.Cursors.Hand;
            this.away.Image = global::SkypeBot.Properties.Resources.skype_Away;
            this.away.Location = new System.Drawing.Point(68, 12);
            this.away.Name = "away";
            this.away.Size = new System.Drawing.Size(50, 50);
            this.away.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.away.TabIndex = 1;
            this.away.TabStop = false;
            this.away.Click += new System.EventHandler(this.away_Click);
            this.away.MouseLeave += new System.EventHandler(this.away_MouseLeave);
            this.away.MouseMove += new System.Windows.Forms.MouseEventHandler(this.away_MouseMove);
            // 
            // online
            // 
            this.online.Cursor = System.Windows.Forms.Cursors.Hand;
            this.online.Image = global::SkypeBot.Properties.Resources.skype_Online;
            this.online.Location = new System.Drawing.Point(12, 12);
            this.online.Name = "online";
            this.online.Size = new System.Drawing.Size(50, 50);
            this.online.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.online.TabIndex = 0;
            this.online.TabStop = false;
            this.online.Click += new System.EventHandler(this.online_Click);
            this.online.MouseLeave += new System.EventHandler(this.online_MouseLeave);
            this.online.MouseMove += new System.Windows.Forms.MouseEventHandler(this.online_MouseMove);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Monotype.com", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(9, 75);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(208, 18);
            this.status.TabIndex = 5;
            this.status.Tag = "";
            this.status.Text = "OnlineStatus: {null}";
            // 
            // ChangeOnlineStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 102);
            this.Controls.Add(this.status);
            this.Controls.Add(this.offline);
            this.Controls.Add(this.invisible);
            this.Controls.Add(this.dnd);
            this.Controls.Add(this.away);
            this.Controls.Add(this.online);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangeOnlineStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangeOnlineStatus";
            ((System.ComponentModel.ISupportInitialize)(this.offline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invisible)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.away)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.online)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox online;
        private System.Windows.Forms.PictureBox away;
        private System.Windows.Forms.PictureBox dnd;
        private System.Windows.Forms.PictureBox invisible;
        private System.Windows.Forms.PictureBox offline;
        private System.Windows.Forms.Label status;
    }
}