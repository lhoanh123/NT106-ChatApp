using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class Friend : UserControl
    {
        public Friend()
        {
            InitializeComponent();
       
        }

        #region Properties

        private string User;

        [Category("Users List")]

        public string user
        {
            get { return User; }
            set { User = value; username_lbl.Text = value; }
        }
        #endregion


        private void Friend_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }

        private void Friend_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(155, 232, 222);
        }
    }
}
