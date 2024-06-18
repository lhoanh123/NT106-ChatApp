namespace Client
{
    partial class Profile
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.avatar_pic = new System.Windows.Forms.PictureBox();
            this.username_lbl = new System.Windows.Forms.Label();
            this.addfriend_btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatar_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.panel1.Controls.Add(this.avatar_pic);
            this.panel1.Location = new System.Drawing.Point(37, 38);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 195);
            this.panel1.TabIndex = 0;
            // 
            // avatar_pic
            // 
            this.avatar_pic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(241)))), ((int)(((byte)(234)))));
            this.avatar_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.avatar_pic.Location = new System.Drawing.Point(17, 19);
            this.avatar_pic.Margin = new System.Windows.Forms.Padding(2);
            this.avatar_pic.Name = "avatar_pic";
            this.avatar_pic.Size = new System.Drawing.Size(151, 158);
            this.avatar_pic.TabIndex = 0;
            this.avatar_pic.TabStop = false;
            // 
            // username_lbl
            // 
            this.username_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.username_lbl.AutoSize = true;
            this.username_lbl.Font = new System.Drawing.Font("Comfortaa", 10.8F, System.Drawing.FontStyle.Bold);
            this.username_lbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.username_lbl.Location = new System.Drawing.Point(49, 250);
            this.username_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.username_lbl.Name = "username_lbl";
            this.username_lbl.Size = new System.Drawing.Size(0, 29);
            this.username_lbl.TabIndex = 1;
            this.username_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addfriend_btn
            // 
            this.addfriend_btn.AutoSize = true;
            this.addfriend_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.addfriend_btn.CausesValidation = false;
            this.addfriend_btn.FlatAppearance.BorderSize = 0;
            this.addfriend_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addfriend_btn.Font = new System.Drawing.Font("Comfortaa", 12F, System.Drawing.FontStyle.Bold);
            this.addfriend_btn.ForeColor = System.Drawing.Color.White;
            this.addfriend_btn.Location = new System.Drawing.Point(54, 288);
            this.addfriend_btn.Margin = new System.Windows.Forms.Padding(2);
            this.addfriend_btn.Name = "addfriend_btn";
            this.addfriend_btn.Size = new System.Drawing.Size(151, 45);
            this.addfriend_btn.TabIndex = 2;
            this.addfriend_btn.Text = "Add friend";
            this.addfriend_btn.UseVisualStyleBackColor = false;
            this.addfriend_btn.Click += new System.EventHandler(this.addfriend_btn_Click);
            this.addfriend_btn.MouseEnter += new System.EventHandler(this.listUser_MouseEnter);
            this.addfriend_btn.MouseLeave += new System.EventHandler(this.Friend_MouseLeave);
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.addfriend_btn);
            this.Controls.Add(this.username_lbl);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Profile";
            this.Size = new System.Drawing.Size(260, 370);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatar_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox avatar_pic;
        private System.Windows.Forms.Label username_lbl;     
        private System.Windows.Forms.Button addfriend_btn;
    }
}
