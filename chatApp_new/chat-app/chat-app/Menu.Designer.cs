namespace Client
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.username_lbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.addfriend1 = new Client.Addfriend();
            this.chat1 = new Client.Chat();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.logout_btn = new System.Windows.Forms.Button();
            this.closeaddfriend_btn = new System.Windows.Forms.PictureBox();
            this.closechat_btn = new System.Windows.Forms.PictureBox();
            this.btnFriend = new System.Windows.Forms.Button();
            this.btnMessage = new System.Windows.Forms.Button();
            this.btnImage = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeaddfriend_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closechat_btn)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.panelDesktop);
            this.panel1.Controls.Add(this.panelMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 784);
            this.panel1.TabIndex = 1;
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.White;
            this.panelDesktop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDesktop.Controls.Add(this.username_lbl);
            this.panelDesktop.Controls.Add(this.pictureBox1);
            this.panelDesktop.Controls.Add(this.addfriend1);
            this.panelDesktop.Controls.Add(this.chat1);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(66, 0);
            this.panelDesktop.Margin = new System.Windows.Forms.Padding(2);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(1112, 784);
            this.panelDesktop.TabIndex = 1;
            // 
            // username_lbl
            // 
            this.username_lbl.AutoSize = true;
            this.username_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.username_lbl.Font = new System.Drawing.Font("Bitter", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_lbl.ForeColor = System.Drawing.Color.White;
            this.username_lbl.Location = new System.Drawing.Point(6, 19);
            this.username_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.username_lbl.Name = "username_lbl";
            this.username_lbl.Size = new System.Drawing.Size(0, 65);
            this.username_lbl.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1112, 784);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // addfriend1
            // 
            this.addfriend1.AutoSize = true;
            this.addfriend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(241)))), ((int)(((byte)(234)))));
            this.addfriend1.Location = new System.Drawing.Point(0, 0);
            this.addfriend1.Margin = new System.Windows.Forms.Padding(2);
            this.addfriend1.Name = "addfriend1";
            this.addfriend1.Size = new System.Drawing.Size(1112, 784);
            this.addfriend1.TabIndex = 6;
            // 
            // chat1
            // 
            this.chat1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(241)))), ((int)(((byte)(234)))));
            this.chat1.Location = new System.Drawing.Point(0, 0);
            this.chat1.Margin = new System.Windows.Forms.Padding(2);
            this.chat1.Name = "chat1";
            this.chat1.Size = new System.Drawing.Size(1112, 784);
            this.chat1.TabIndex = 5;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.panelMenu.Controls.Add(this.logout_btn);
            this.panelMenu.Controls.Add(this.closeaddfriend_btn);
            this.panelMenu.Controls.Add(this.closechat_btn);
            this.panelMenu.Controls.Add(this.btnFriend);
            this.panelMenu.Controls.Add(this.btnMessage);
            this.panelMenu.Controls.Add(this.btnImage);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(66, 784);
            this.panelMenu.TabIndex = 0;
            // 
            // logout_btn
            // 
            this.logout_btn.FlatAppearance.BorderSize = 0;
            this.logout_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout_btn.Image = ((System.Drawing.Image)(resources.GetObject("logout_btn.Image")));
            this.logout_btn.Location = new System.Drawing.Point(3, 724);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(60, 57);
            this.logout_btn.TabIndex = 7;
            this.logout_btn.UseVisualStyleBackColor = true;
            this.logout_btn.Click += new System.EventHandler(this.logout_btn_Click);
            // 
            // closeaddfriend_btn
            // 
            this.closeaddfriend_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeaddfriend_btn.BackgroundImage")));
            this.closeaddfriend_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeaddfriend_btn.Location = new System.Drawing.Point(0, 158);
            this.closeaddfriend_btn.Margin = new System.Windows.Forms.Padding(4);
            this.closeaddfriend_btn.Name = "closeaddfriend_btn";
            this.closeaddfriend_btn.Size = new System.Drawing.Size(66, 66);
            this.closeaddfriend_btn.TabIndex = 6;
            this.closeaddfriend_btn.TabStop = false;
            this.closeaddfriend_btn.Visible = false;
            this.closeaddfriend_btn.Click += new System.EventHandler(this.closeaddfriend_btn_Click);
            // 
            // closechat_btn
            // 
            this.closechat_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closechat_btn.BackgroundImage")));
            this.closechat_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closechat_btn.Location = new System.Drawing.Point(0, 91);
            this.closechat_btn.Margin = new System.Windows.Forms.Padding(4);
            this.closechat_btn.Name = "closechat_btn";
            this.closechat_btn.Size = new System.Drawing.Size(66, 66);
            this.closechat_btn.TabIndex = 5;
            this.closechat_btn.TabStop = false;
            this.closechat_btn.Visible = false;
            this.closechat_btn.Click += new System.EventHandler(this.closechat_btn_Click);
            // 
            // btnFriend
            // 
            this.btnFriend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.btnFriend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFriend.BackgroundImage")));
            this.btnFriend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFriend.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFriend.FlatAppearance.BorderSize = 0;
            this.btnFriend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFriend.ForeColor = System.Drawing.Color.Black;
            this.btnFriend.Location = new System.Drawing.Point(0, 163);
            this.btnFriend.Margin = new System.Windows.Forms.Padding(2);
            this.btnFriend.Name = "btnFriend";
            this.btnFriend.Size = new System.Drawing.Size(66, 66);
            this.btnFriend.TabIndex = 4;
            this.btnFriend.UseVisualStyleBackColor = false;
            this.btnFriend.Click += new System.EventHandler(this.btnFriend_Click);
            // 
            // btnMessage
            // 
            this.btnMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.btnMessage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMessage.BackgroundImage")));
            this.btnMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMessage.FlatAppearance.BorderSize = 0;
            this.btnMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMessage.ForeColor = System.Drawing.Color.Black;
            this.btnMessage.Location = new System.Drawing.Point(0, 97);
            this.btnMessage.Margin = new System.Windows.Forms.Padding(2);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(66, 66);
            this.btnMessage.TabIndex = 3;
            this.btnMessage.UseVisualStyleBackColor = false;
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            // 
            // btnImage
            // 
            this.btnImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImage.FlatAppearance.BorderSize = 0;
            this.btnImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImage.ForeColor = System.Drawing.Color.Black;
            this.btnImage.Image = ((System.Drawing.Image)(resources.GetObject("btnImage.Image")));
            this.btnImage.Location = new System.Drawing.Point(0, 0);
            this.btnImage.Margin = new System.Windows.Forms.Padding(2);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(66, 97);
            this.btnImage.TabIndex = 2;
            this.btnImage.UseVisualStyleBackColor = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1178, 784);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Menu_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panelDesktop.ResumeLayout(false);
            this.panelDesktop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closeaddfriend_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closechat_btn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnFriend;
        private System.Windows.Forms.Button btnMessage;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox closeaddfriend_btn;
        private System.Windows.Forms.PictureBox closechat_btn;
        private Chat chat1;
        private Addfriend addfriend1;
        private System.Windows.Forms.Label username_lbl;
        private System.Windows.Forms.Button logout_btn;
    }
}