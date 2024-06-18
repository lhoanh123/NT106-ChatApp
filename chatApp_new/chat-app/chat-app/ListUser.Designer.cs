namespace Client
{
    partial class ListUser
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
            this.userName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.avatar_pic = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatar_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Font = new System.Drawing.Font("Comfortaa", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.Location = new System.Drawing.Point(72, 10);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(0, 29);
            this.userName.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(169)))), ((int)(((byte)(113)))));
            this.panel2.Controls.Add(this.avatar_pic);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(53, 54);
            this.panel2.TabIndex = 4;
            // 
            // avatar_pic
            // 
            this.avatar_pic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(241)))), ((int)(((byte)(234)))));
            this.avatar_pic.Location = new System.Drawing.Point(8, 9);
            this.avatar_pic.Name = "avatar_pic";
            this.avatar_pic.Size = new System.Drawing.Size(35, 35);
            this.avatar_pic.TabIndex = 0;
            this.avatar_pic.TabStop = false;
            // 
            // ListUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(241)))), ((int)(((byte)(234)))));
            this.Controls.Add(this.userName);
            this.Controls.Add(this.panel2);
            this.Name = "ListUser";
            this.Size = new System.Drawing.Size(249, 54);
            this.MouseEnter += new System.EventHandler(this.listUser_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.listUser_MouseLeave);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatar_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox avatar_pic;
        #endregion

      
    }
}
