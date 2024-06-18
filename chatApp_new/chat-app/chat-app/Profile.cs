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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    // tạo ra một UserControl tùy chỉnh trong ứng dụng.
    public partial class Profile : UserControl
    {

        //profile có 2 thuộc tính là tên user và giá trị button btn
        #region Properties

        // lưu trữ trên người dùng
        private string user;
        
        [Category("Users List")]

        /* Thuộc tính public User có getter và setter, cho phép truy cập và thiết lập giá trị của thuộc tính User. 
         * Khi giá trị của thuộc tính thay đổi, nó sẽ cập nhật giá trị của biểu đồ label1.Text thành giá trị mới.
         */
        public string User
        {
            get { return user; }
            set { user = value; username_lbl.Text = value; }
        }
        #endregion

        public Profile()
        {
            InitializeComponent();
        }

        #region Properties

        // lưu trữ giá trị của nút button.
        private string btn;

        [Category("Users List")]

        /* Thuộc tính public Btn có getter và setter, cho phép truy cập và thiết lập giá trị của thuộc tính Btn. 
         * Khi giá trị của thuộc tính thay đổi, nó sẽ cập nhật giá trị của biểu đồ button1.Text thành giá trị mới.
         */
        public string Btn
        {
            get { return btn; }
            set { btn = value; addfriend_btn.Text = value; }

        }
        #endregion

        // Định nghĩa phương thức Button1Status để thiết lập trạng thái của nút button.
        // Tham số b xác định trạng thái (true: kích hoạt, false: vô hiệu hóa) của nút button.
        public void Button1Status(bool b)  // set trạng thái của button
        {
            addfriend_btn.Enabled = b;
        }


        // gửi giá trị đến main form (add friend)
        // Sự kiện này sẽ được kích hoạt khi dữ liệu thay đổi.
        public event EventHandler<string> DataChanged;

        // Đây là phương thức OnDataChanged để gọi sự kiện DataChanged khi dữ liệu thay đổi.
        private void OnDataChanged(string data)
        {
            DataChanged?.Invoke(this, data);
        }



        private void addfriend_btn_Click(object sender, EventArgs e)
        {

            // tạo message mặc định và khởi tạo các thuộc tính mặc định của nó.
            Message Message = new Message
            {
                header = "Error",
                Sender = "",
                Receiver = user,
                message = "x",
            };

            if (this.btn == "Add friend")  //nếu nút button addfriend được bấm thì gửi thông điệp addfriend đến server
            {
                Message = new Message
                {
                    header = "Addfriend",
                    Sender = "",
                    Receiver = user,
                    message = "1",
                };
            }    
            else if(btn == "Accept")   //nếu nút button accept được bấm thì gửi thông điệp accept đến server
            {
                Message = new Message
                {
                    header = "Accept",
                    Sender = "",
                    Receiver = user,
                    message = "2",
                };
                Btn = "Unfriend";
            }
            else if (btn == "Unfriend")
            {
                Message = new Message
                {
                    header = "Unfriend",
                    Sender = "",
                    Receiver = user,
                    message = "3",
                };
                Btn = "Add friend";
            }

            //  Tạo một đối tượng JavaScriptSerializer để chuyển đổi đối tượng Message thành chuỗi JSON.
            JavaScriptSerializer json = new JavaScriptSerializer();

            // Chuyển đối tượng Message thành chuỗi JSON bằng cách sử dụng phương thức Serialize của JavaScriptSerializer.
            string jsonString = json.Serialize(Message);
            
            OnDataChanged(jsonString); //gửi jsonString đến main form (add friend)
        }
        private void listUser_MouseEnter(object sender, EventArgs e)
        {
            addfriend_btn.BackColor = Color.FromArgb(56, 216, 148);
        }
        private void Friend_MouseLeave(object sender, EventArgs e)
        {
            addfriend_btn.BackColor = Color.FromArgb(34, 169, 113);
        }
    }
}