namespace Client
{
    partial class SignIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignIn));
            this.username_txt = new System.Windows.Forms.TextBox();
            this.password_txt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.signin_btn = new nexus.Circularbutton();
            this.signup_btn = new nexus.Circularbutton();
            this.ip_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // username_txt
            // 
            this.username_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.username_txt.Font = new System.Drawing.Font("Bitter", 10.2F);
            this.username_txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.username_txt.Location = new System.Drawing.Point(505, 285);
            this.username_txt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.username_txt.Multiline = true;
            this.username_txt.Name = "username_txt";
            this.username_txt.Size = new System.Drawing.Size(282, 37);
            this.username_txt.TabIndex = 1;
            // 
            // password_txt
            // 
            this.password_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password_txt.Font = new System.Drawing.Font("Bitter", 10.2F);
            this.password_txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.password_txt.Location = new System.Drawing.Point(505, 385);
            this.password_txt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.password_txt.Multiline = true;
            this.password_txt.Name = "password_txt";
            this.password_txt.PasswordChar = '*';
            this.password_txt.Size = new System.Drawing.Size(282, 37);
            this.password_txt.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panel1.Controls.Add(this.signin_btn);
            this.panel1.Controls.Add(this.signup_btn);
            this.panel1.Location = new System.Drawing.Point(505, 450);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 157);
            this.panel1.TabIndex = 10;
            // 
            // signin_btn
            // 
            this.signin_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(169)))), ((int)(((byte)(112)))));
            this.signin_btn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(169)))), ((int)(((byte)(112)))));
            this.signin_btn.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.signin_btn.BorderRadius = 40;
            this.signin_btn.BorderSize = 0;
            this.signin_btn.FlatAppearance.BorderSize = 0;
            this.signin_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signin_btn.Font = new System.Drawing.Font("Pangram", 20F);
            this.signin_btn.ForeColor = System.Drawing.Color.White;
            this.signin_btn.Location = new System.Drawing.Point(93, 3);
            this.signin_btn.Name = "signin_btn";
            this.signin_btn.Size = new System.Drawing.Size(170, 70);
            this.signin_btn.TabIndex = 7;
            this.signin_btn.Text = "Sign in";
            this.signin_btn.TextColor = System.Drawing.Color.White;
            this.signin_btn.UseVisualStyleBackColor = false;
            this.signin_btn.Click += new System.EventHandler(this.signin_btn_Click);
            // 
            // signup_btn
            // 
            this.signup_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(169)))), ((int)(((byte)(112)))));
            this.signup_btn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(169)))), ((int)(((byte)(112)))));
            this.signup_btn.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.signup_btn.BorderRadius = 40;
            this.signup_btn.BorderSize = 0;
            this.signup_btn.FlatAppearance.BorderSize = 0;
            this.signup_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signup_btn.Font = new System.Drawing.Font("Pangram", 20F);
            this.signup_btn.ForeColor = System.Drawing.Color.White;
            this.signup_btn.Location = new System.Drawing.Point(93, 87);
            this.signup_btn.Name = "signup_btn";
            this.signup_btn.Size = new System.Drawing.Size(170, 70);
            this.signup_btn.TabIndex = 8;
            this.signup_btn.Text = "Sign up";
            this.signup_btn.TextColor = System.Drawing.Color.White;
            this.signup_btn.UseVisualStyleBackColor = false;
            this.signup_btn.Click += new System.EventHandler(this.signup_btn_Click);
            // 
            // ip_txt
            // 
            this.ip_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ip_txt.Font = new System.Drawing.Font("Bitter", 10.2F);
            this.ip_txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.ip_txt.Location = new System.Drawing.Point(589, 199);
            this.ip_txt.Margin = new System.Windows.Forms.Padding(2);
            this.ip_txt.Multiline = true;
            this.ip_txt.Name = "ip_txt";
            this.ip_txt.Size = new System.Drawing.Size(282, 37);
            this.ip_txt.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Pangram", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(499, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 32);
            this.label1.TabIndex = 12;
            this.label1.Text = "IP:";
            // 
            // SignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ip_txt);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.password_txt);
            this.Controls.Add(this.username_txt);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "SignIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignIn";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SignIn_FormClosed);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox username_txt;
        private System.Windows.Forms.TextBox password_txt;
        private nexus.Circularbutton signin_btn;
        private nexus.Circularbutton signup_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip_txt;
    }
}