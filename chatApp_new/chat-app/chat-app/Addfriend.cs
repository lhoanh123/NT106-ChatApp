using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static Client.Chat;

namespace Client
{
    public partial class Addfriend : UserControl
    {
        public Addfriend()
        {
            InitializeComponent();
            flowLayoutPanel1.Controls.Clear();
            panel1.Controls.Clear();
            //user = new List<string>();
        }

        // tạo một struct gồm user và profile của user đó, status là trạng thái (bạn hay ch phải bạn)
        public struct MyUser
        {
            public ListUser user;
            public Profile profile;
            public bool status;
        }
        public MyUser[] myusers; // ds Myuser

        //List<string> user;
        string listfriend = null;

        private int length;   // độ dài danh sách user

        private string tempt;

        private string allusers;
        public void flowpanel(string msg, string friend) // tạo Myuser với msg là listuser, friend là listfriend server gửi về
        {
            flowLayoutPanel1.Controls.Clear();
            string[] dataReceive = msg.Split(','); // tách chuỗi listuser
            allusers = msg;
            for (int i = 0; i < dataReceive.Count(); i++)
            {
                if (dataReceive[i].Trim() == Menu.username)
                {
                    List<string> list = new List<string>(dataReceive); // Chuyển đổi mảng thành List
                    list.RemoveAt(i); // Xóa phần tử tại chỉ số 1 trong List
                    dataReceive = list.ToArray(); // Chuyển đổi List thành mảng string[]
                }
            }
            length = (dataReceive.Count() - 1); // số user
            myusers = new MyUser[length];       // tạo Myuser với số lượng length
            listfriend = friend;
            if (listfriend != null)
            {
                for (int i = 0; i < length; i++)
                {
                    string usr = dataReceive[i].Trim();
                    AllUsers1(i, usr, myusers);   //khởi tạo các usercontrol của Myuser(khởi tạo obj của từng user)
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    string usr = dataReceive[i].Trim();
                    AllUsers2(i, usr, myusers);   //khởi tạo các usercontrol của Myuser(khởi tạo obj của từng user)
                }
            }

        }

        //một myuser gồm một ListUser(button user) và Profile
        public void AllUsers1(int i, string user, MyUser[] myuser)
        {
            myuser[i] = new MyUser(); // khởi tạo myuser
            myuser[i].user = new ListUser(); //khởi tạo listuser
            myuser[i].user.user = user;   //gán thuộc tính user của listuser bằng user 

            myuser[i].profile = new Profile();  //khởi tạo profile

            // khởi tạo các giá trị cho profile
            // profile có thuộc tính tên user và btn (giá trị của button: friend/ add friend/ accept)
            myuser[i].profile.BackColor = System.Drawing.Color.Transparent;
            myuser[i].profile.Location = new System.Drawing.Point(42, 44);
            myuser[i].profile.Name = user;
            myuser[i].profile.Size = new System.Drawing.Size(388, 453);
            myuser[i].profile.TabIndex = 0;
            myuser[i].profile = new Profile();
            myuser[i].profile.User = user;
            myuser[i].profile.Visible = true;

            // tách listfriend
            string[] friend = listfriend.Split(',');
            int lengthf = (friend.Count() - 1);

            //gán giá trị mặc định của button btn là add friend
            myuser[i].profile.Btn = "Add friend";
            myuser[i].status = false;  // cả hai chưa là bạn


            for (int j = 0; j < lengthf; j++) // chạy vòng lặp nếu user có trong listfriend thì gán giá trị button btn là friend và status là true
            {
                if (user == friend[j].Trim())
                {
                    myuser[i].profile.Btn = "Unfriend";
                    myuser[i].profile.Button1Status(true);  // hàm button1status để gán trạng thái của button btn(cả hai đang là friend thì false tức là không nhấn nút được)
                    //myuser[i].status = true;
                    break;
                }
            }

            flowLayoutPanel1.Controls.Add(myuser[i].user); // adđ usercontrol listuser của user vào flowlayoutpanel
            // datachanged là của profile    btnaddfriend_click là của form addfriend
            myuser[i].profile.DataChanged += unfriend_btn_Click;  //nhận giá trị khi button btn của profile được bấm
            myuser[i].user.Click += new System.EventHandler(this.User_Click);  //sự kiện clik của listuser
        }

        public void AllUsers2(int i, string user, MyUser[] myuser)
        {
            myuser[i] = new MyUser(); // khởi tạo myuser
            myuser[i].user = new ListUser(); //khởi tạo listuser
            myuser[i].user.user = user;   //gán thuộc tính user của listuser bằng user 

            myuser[i].profile = new Profile();  //khởi tạo profile

            // khởi tạo các giá trị cho profile
            // profile có thuộc tính tên user và btn (giá trị của button: friend/ add friend/ accept)
            myuser[i].profile.BackColor = System.Drawing.Color.Transparent;
            myuser[i].profile.Location = new System.Drawing.Point(42, 44);
            myuser[i].profile.Name = user;
            myuser[i].profile.Size = new System.Drawing.Size(388, 453);
            myuser[i].profile.TabIndex = 0;
            myuser[i].profile = new Profile();
            myuser[i].profile.User = user;
            myuser[i].profile.Visible = true;

            //gán giá trị mặc định của button btn là add friend
            myuser[i].profile.Btn = "Add friend";
            myuser[i].status = false;  // cả hai chưa là bạn

            flowLayoutPanel1.Controls.Add(myuser[i].user); // adđ usercontrol listuser của user vào flowlayoutpanel
            // datachanged là của profile    btnaddfriend_click là của form addfriend
            myuser[i].profile.DataChanged += addfriend_btn_Click;  //nhận giá trị khi button btn của profile được bấm
            myuser[i].user.Click += new System.EventHandler(this.User_Click);  //sự kiện click của listuser
        }

        void User_Click(object sender, EventArgs e)
        {
            ListUser obj = (ListUser)sender;
            panel1.Controls.Clear();
            int j = -1; ;
            for (int i = 0; i < length; i++)     //chạy vòng lặp nếu tên user bằng với tên user trong list thì add profile của user đó vào panel
            {
                if (myusers[i].user.user == obj.user)
                {
                    j = i;
                    break;
                }
            }
            close_btn.Visible = true;
            tempt = obj.user;
            panel1.Controls.Add(myusers[j].profile);
        }

        public event EventHandler<string> DataChanged;

        private void OnDataChanged(string data)  //truyền data tới main form(menu) 
        {
            DataChanged?.Invoke(this, data);
        }
        public void Print(string sender, string tag)   //in dữ liệu được truyền đến (sender là người gửi, tag(accept/addfriend))
        {
            for (int i = 0; i < length; i++)
            {
                if (myusers[i].user.user.Trim() == sender)
                {
                    myusers[i].profile.Btn = tag;
                }
            }
        }
        private void addfriend_btn_Click(object sender, string data) //data là dữ liệu nhận đươc từ usercontrol profile cụ thể là button btn
        {
            //gửi data đến main form(menu) để gửi đi
            OnDataChanged(data);
        }

        private void unfriend_btn_Click(object sender, string data) //data là dữ liệu nhận đươc từ usercontrol profile cụ thể là button btn
        {
            //gửi data đến main form(menu) để gửi đi
            OnDataChanged(data);
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            //Kiểm tra xem người dùng nhập vô search_txt có tồn tại 
            if (allusers.Contains(search_txt.Text))
            {
                panel1.Controls.Clear();
                int j = -1; ;
                for (int i = 0; i < length; i++)
                {
                    if (myusers[i].user.user == search_txt.Text)//chạy vòng lặp nếu tên user bằng với tên user trong search_txt thì add profile của user đó vào panel
                    {
                        j = i;
                        break;
                    }
                }
                close_btn.Visible = true;
                tempt = search_txt.Text;
                panel1.Controls.Add(myusers[j].profile);
                search_txt.Clear();
            }
            else
                MessageBox.Show("This user does not exist");
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            int j = -1;
            for (int i = 0; i < length; i++)
            {
                if (myusers[i].user.user == tempt)//chạy vòng lặp nếu tên user bằng với tên user trong search_txt thì close profile đó 
                {
                    j = i;
                    break;
                }
            }
            tempt = null;
            panel1.Controls.Remove(myusers[j].profile);
            close_btn.Visible = false;
        }
    }
}
