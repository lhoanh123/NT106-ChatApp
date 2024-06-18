using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ListUser : UserControl
    {
        public ListUser()
        {
            InitializeComponent();
        }

        #region Properties

        private string User;
        private Image Avatar;

        [Category("Users List")]

        public string user
        {
            get { return User; }
            set { User = value; userName.Text = value; }
        }

        [Category("Users List")]

        public Image avatar
        {
            get { return Avatar; }
            set { Avatar = value; avatar_pic.Image = value; }
        }
        #endregion

        private void listUser_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(224, 241, 234);
        }

        private void listUser_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(155, 232, 222);
        }
    }
}
