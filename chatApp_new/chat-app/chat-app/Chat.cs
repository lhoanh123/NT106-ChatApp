using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class Chat : UserControl
    {
        public Chat()
        {
            InitializeComponent();
        }

        public struct myfriend
        {
            public Friend f;
            public RichTextBox richtextbox;
        }

        public myfriend[] myfriends;
        private int length;
        List<string> friends = new List<string>();

        public void flowpanel(string msg)
        {
            flowLayoutPanel1.Controls.Clear();

            string[] dataReceive = msg.Split(','); //tạo mảng gồm tên user
            length = (dataReceive.Count() - 1);
            myfriends = new myfriend[length];
            for (int i = 0; i < length; i++)
            {
                string usr = dataReceive[i].Trim();
                friends.Add(usr);
                Allfriend(i, usr, myfriends);  // thực hiện tạo từng obj của user
            }
        }

        public void Allfriend(int i, string user, myfriend[] myfriend)
        {
            myfriend[i] = new myfriend();
            myfriend[i].f = new Friend(); //tạo mới một usercontrol Friend
            myfriend[i].f.user = user;

            myfriend[i].richtextbox = new RichTextBox
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                HideSelection = false,
                Location = new System.Drawing.Point(0, 0),
                Name = user,
                Size = new System.Drawing.Size(654, 533),
                TabIndex = 0,
                Visible = true,
                Font = new Font("Bitter", 11),
                ReadOnly = true
            }; //tạo một listview

            flowLayoutPanel1.Controls.Add(myfriend[i].f); // Add usercontrol Friend vào flowlayoutpanel1
            myfriend[i].f.Click += new System.EventHandler(this.User_Click); // set sự kiện cho Friend
        }
        void User_Click(object sender, EventArgs e)
        {
            send_btn.Visible = true;
            txtSend.Visible = true;
            Friend obj = (Friend)sender;
            panel1.Controls.Clear();

            int j = -1; ;
            for (int i = 0; i < length; i++)// duyệt các phàn tử trong myfriend, nếu bằng với tên Friend thì add listview của Friend đó vào panel1
            {
                if (myfriends[i].f.user == obj.user)
                {
                    j = i;
                }
            }
            panel1.Controls.Add(myfriends[j].richtextbox);
            username_lbl.Text = obj.user;
            close_btn.Visible = true;

            myfriends[j].richtextbox.Visible = true;
        }

        public void Print(string msg, string sender, string label)// nhận dữ liệu client khác gửi đến và in ra màn hình
        {
            for (int i = 0; i < length; i++)
            {
                if (myfriends[i].f.user.Trim() == label)
                {
                    myfriends[i].richtextbox.AppendText(sender + ": " + msg + "\n");
                }
            }
        }

        //Nhấn nút send để gửi 
        private void send_btn_Click(object sender, EventArgs e)
        {
            
            byte[] bytes = Encoding.UTF8.GetBytes(txtSend.Text);

            if (bytes.Length > 1024*1024*2)
            {
                MessageBox.Show("Tối đa 2MB");
                return;
            }
            Message Message = new Message
            {
                header = "Message",
                Sender = "",
                Receiver = username_lbl.Text.Trim(),
                message = txtSend.Text.ToString(),
            };
            JavaScriptSerializer json = new JavaScriptSerializer(); // feel free to use JSON serializer
            string jsonString = json.Serialize(Message);
            Print(txtSend.Text.ToString(), "You", username_lbl.Text.Trim());

            OnDataChanged(jsonString); // chuyển dữ liệu đến main form
            txtSend.Clear();
        }

        // chuyển dữ liệu đến main form (menu)
        public event EventHandler<string> DataChanged;

        private void OnDataChanged(string data)
        {
            DataChanged?.Invoke(this, data);
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            //Kiểm tra xem người dùng nhập vô search_txt có tồn tại trong danh sách bạn bè
            if (friends.Contains(search_txt.Text))
            {
                panel1.Controls.Clear();

                int j = -1; ;
                for (int i = 0; i < length; i++)// duyệt các phàn tử trong myfriend, nếu bằng với tên Friend thì add listview của Friend đó vào panel1
                {
                    if (myfriends[i].f.user == search_txt.Text)
                    {
                        j = i;
                    }
                }
                panel1.Controls.Add(myfriends[j].richtextbox);
                send_btn.Visible = true;
                txtSend.Visible = true;
                close_btn.Visible = true;
                username_lbl.Text = search_txt.Text;
                search_txt.Clear();
            }
            else
                MessageBox.Show("This user is not on your friend list");
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            int j = -1; ;
            for (int i = 0; i < length; i++)// duyệt các phàn tử trong myfriend, nếu bằng với tên Friend thì xóa listview của Friend đó khỏi panel1
            {
                if (myfriends[i].f.user == username_lbl.Text)
                {
                    j = i;
                }
            }
            panel1.Controls.Remove(myfriends[j].richtextbox);
            send_btn.Visible = false;
            txtSend.Visible = false;
            close_btn.Visible = false;
            username_lbl.Text = null;
        }
    }
}
