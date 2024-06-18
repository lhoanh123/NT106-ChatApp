using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Security.Cryptography;
using chat_app;

namespace Client
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
        private bool connected = false;
        private Thread client = null;
        public static string ipAd_signup = SignIn.ipAd_signin;
        private struct MyClient
        {
            // lưu trữ phản hồi từ server.
            public string response;

            // đại diện cho kết nối TCP tới một máy chủ hoặc client khác.
            public TcpClient client;

            // truy cập dữ liệu từ kết nối mạng TCP.
            public NetworkStream stream;

            // lưu trữ dữ liệu nhận được từ kết nối.
            public byte[] buffer;

            // xây dựng một chuỗi dữ liệu nhận được từ kết nối.
            public StringBuilder data;
            /*StringBuilder là một lớp trong C# được sử dụng để xây dựng và thao tác với chuỗi dữ liệu một cách hiệu quả. 
             * Nó cho phép thêm, cắt, nối, và chỉnh sửa chuỗi một cách hiệu quả hơn so với việc sử dụng các phép toán trực tiếp trên chuỗi (string).
             */

            // đồng bộ hóa quá trình đọc dữ liệu từ kết nối.
            public EventWaitHandle handle;
            /* EventWaitHandle là một lớp trong C# cho phép đồng bộ hóa hoặc chờ đợi cho một sự kiện xảy ra trong các luồng (threads) khác nhau. 
             * EventWaitHandle được thiết lập để thông báo cho các luồng khác biết rằng quá trình đã hoàn thành và dữ liệu có sẵn để xử lý. 
             * Điều này đảm bảo rằng quá trình đọc dữ liệu được thực hiện một cách đồng bộ và tránh xung đột giữa các luồng.
             */
        };

        private MyClient obj=new MyClient();
        private Task send = null;

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool CheckSpecialCharaters(string tempt)
        {
            foreach (char c in tempt)
            {
                if (((int)c == 32) || ((int)c >= 33 && (int)c <= 47) || ((int)c >= 58 && (int)c <= 64) || ((int)c >= 91 && (int)c <= 96) || ((int)c >= 123 && (int)c <= 126))
                    return true;
            }
            return false;
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            if (username_txt.Text == null || password_txt.Text == null)
            {
                username_txt.Clear();
                password_txt.Clear();
                MessageBox.Show("Vui lòng điển đầy đủ thông tin!", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (CheckSpecialCharaters(username_txt.Text) || CheckSpecialCharaters(password_txt.Text))
            {
                username_txt.Clear();
                password_txt.Clear();
                MessageBox.Show("Tên người dùng và mật khẩu không được chứa các ký tự đặt biệt!", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (username_txt.Text.Length > 10)
            {
                MessageBox.Show("Tên người dùng tối đa 10 ký tự", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password_txt.Text.Length < 8)
            {
                MessageBox.Show("Mật khẩu tối thiểu 8 ký tự", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // nếu đang kết nối thì đóng kết nối trước đó
            else if (connected)
            {
                obj.client.Close();
            }

            //check ip, port và tạo thread để connect
            else if (client == null || !client.IsAlive)
            {
                /* cài đặt địa chỉ ip & port mặc định 
                * tạo một luồng mới client và bắt đầu thực thi nó bằng cách gọi phương thức Connection(ip, port). 
                * Đồng thời, đặt thuộc tính IsBackground của luồng là true để cho phép chương trình thoát ngay cả khi luồng đó chưa hoàn thành.
                */
                IPAddress ip = IPAddress.Parse(ipAd_signup);

                client = new Thread(() => Connection(ip, 8000))
                {
                    IsBackground = true
                };
                client.Start();
            }
        }

        // được sử dụng để đọc dữ liệu từ luồng dữ liệu mạng (NetworkStream) trong một phương thức bất đồng bộ.
        private void ReadAuth(IAsyncResult result)
        {
            // lưu trữ số lượng byte đã đọc từ luồng dữ liệu.
            int bytes = 0;

            // Kiểm tra xem kết nối tới máy chủ (client) vẫn đang được duy trì (Connected) hay không.
            if (obj.client.Connected)
            {
                try
                {
                    // Khi kết nối vẫn còn hoạt động, sử dụng phương thức EndRead để kết thúc hoạt động đọc bất đồng bộ và lấy số lượng byte đã đọc vào biến bytes.
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Kiểm tra xem đã đọc thành công ít nhất một byte hay không.
            if (bytes > 0)
            {
                // Sử dụng phương thức Encoding.UTF8.GetString() để chuyển đổi dữ liệu từ mảng byte obj.buffer thành một chuỗi và thêm chuỗi này vào biến obj.data (kiểu StringBuilder).
                obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    // Kiểm tra xem còn dữ liệu khả dụng trong luồng dữ liệu (DataAvailable) hay không.
                    if (obj.stream.DataAvailable)
                    {
                        // Nếu còn dữ liệu khả dụng, gọi phương thức BeginRead để tiếp tục đọc bất đồng bộ từ luồng dữ liệu.
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), null);
                    }
                    else // Nếu không còn dữ liệu khả dụng, thực hiện các hành động sau đó.
                    {
                        // Chuyển đổi nội dung của obj.data thành một chuỗi.'
                        string msg = Crypt.Decryption(obj.data.ToString());
                        // Gán giá trị của msg cho thuộc tính response của đối tượng obj.
                        if (msg != null)
                        {
                            obj.response = msg;
                        }

                        // Xóa nội dung của obj.data để chuẩn bị cho lần đọc dữ liệu tiếp theo.
                        obj.data.Clear();

                        // Kích hoạt một tín hiệu hoàn thành cho đối tượng obj.
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    // Xóa nội dung của obj.data để chuẩn bị cho lần đọc dữ liệu tiếp theo.
                    obj.data.Clear();

                    // Kích hoạt một tín hiệu hoàn thành cho đối tượng obj.
                    obj.handle.Set();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // Xóa nội dung của obj.data để chuẩn bị cho lần đọc dữ liệu tiếp theo.
                obj.data.Clear();

                // Kích hoạt một tín hiệu hoàn thành cho đối tượng obj.
                obj.handle.Set();
            }
        }

        private bool Authorize()
        {
            // đại diện cho kết quả của quá trình xác thực.
            bool success = false;

            // Gửi dữ liệu xác thực tới server bằng cách sử dụng phương thức Send
            Send("sign up," + username_txt.Text.ToString() + "," + ComputeSha256Hash(password_txt.Text.ToString())); // xác thực bằng cách gửi tên đăng ký và mk
                                                                                                  // Vòng lặp này sẽ tiếp tục chạy khi kết nối với máy chủ vẫn còn hoạt động.
            while (obj.client.Connected)
            {
                try
                {
                    // Gửi yêu cầu đọc bất đồng bộ từ luồng dữ liệu tới máy chủ bằng phương thức BeginRead.
                    // Khi dữ liệu được đọc, phương thức ReadAuth sẽ được gọi để xử lý kết quả.
                    obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), null); //server trả về kết quả

                    // Chờ đợi cho đến khi nhận được tín hiệu hoàn thành từ máy chủ.
                    // Điều này đảm bảo rằng quá trình đọc dữ liệu đã hoàn thành và obj.response đã được cập nhật.
                    obj.handle.WaitOne();

                    // nếu giá trị của obj.response là "Valid" hoặc "Already", gán giá trị true cho biến success và thoát khỏi vòng lặp.
                    // nếu response là Valid hoặc Already thì success = true => Authorize() trả về true 
                    if (obj.response == "Valid" )
                    {

                        success = true;
                        break;
                    }
                    else
                    {
                        success = false;
                        break;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Trả về giá trị của biến success
            return success;
        }

        // thiết lập kết nối với máy chủ, xác thực đăng ký và xử lý các hành động sau khi đăng ký
        private void Connection(IPAddress ip, int port)
        {
            // Bắt đầu khối try-catch để xử lý các ngoại lệ có thể xảy ra trong quá trình thiết lập kết nối và đăng ký.
            try
            {
                //tạo obj connect tới server
                // lưu trữ thông tin và trạng thái của kết nối.
                obj = new MyClient();

                // tạo kết nối TCP.
                obj.client = new TcpClient();

                obj.response = "";

                // Thiết lập kết nối TCP tới máy chủ với địa chỉ IP ip và cổng port.
                obj.client.Connect(ip, port);

                // Lấy luồng dữ liệu từ kết nối TCP để đọc và ghi dữ liệu.
                obj.stream = obj.client.GetStream();

                // Khởi tạo bộ đệm dùng để nhận dữ liệu từ máy chủ với kích thước tối đa là kích thước bộ đệm nhận của TcpClient.
                obj.buffer = new byte[obj.client.ReceiveBufferSize];

                // xây dựng và lưu trữ dữ liệu đọc từ máy chủ.
                obj.data = new StringBuilder();

                // đồng bộ hóa quá trình đọc dữ liệu.
                obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                bool tempt = Authorize();

                // gọi phương thức để xác thực đăng ký 
                if (tempt)
                {
                    // trả về true đăng ký thành công
                    DialogResult dlr = MessageBox.Show("Đăng ký thành công", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // đóng kết nối TCP
                    obj.client.Close();

                    // Thực hiện các hành động trong luồng giao diện người dùng chính để chuyển sang Form Menu và đóng Form hiện tại.
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Hide();
                        Form form = new Menu(username_txt.Text.ToString()); // gọi form menu với tham số là tên đăng ký
                        form.ShowDialog();
                        this.Close();
                    });
                }
                else // trả về false đăng ký không thành công sau đó đóng kết nối
                {
                    DialogResult dlr = MessageBox.Show("Đăng ký không thành công", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    obj.client.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //gửi đến server
        private void Send(string msg)
        {
            string msgEncrypt = Crypt.Encryption(msg);

            // Kiểm tra nếu tác vụ gửi trước đó (send) là null hoặc đã hoàn thành, tức là không có tác vụ gửi nào đang chờ xử lý.
            if (send == null || send.IsCompleted)
            {
                // tạo một tác vụ (Task) mới bằng cách sử dụng Task.Factory.StartNew() để gọi phương thức BeginWrite()
                // với đối số là msg (dữ liệu cần gửi).
                send = Task.Factory.StartNew(() => BeginWrite(msgEncrypt));
            }
            else
            {
                // Nếu có tác vụ gửi chờ xử lý, sử dụng phương thức ContinueWith() để gọi lại phương thức BeginWrite()
                // với đối số là msg khi tác vụ gửi trước đó hoàn thành.
                send.ContinueWith(antecendent => BeginWrite(msgEncrypt));
            }
        }

        /* được sử dụng để kết thúc quá trình ghi dữ liệu từ client tới server. 
         * Nó đảm bảo rằng quá trình ghi được thực hiện trong trường hợp kết nối vẫn hoạt động 
         * và xử lý các ngoại lệ phát sinh trong quá trình ghi.
         */
        private void Write(IAsyncResult result)
        {
            // Kiểm tra xem kết nối của client với server có đang hoạt động hay không.
            if (obj.client.Connected)
            {
                try
                {
                    // Kết thúc quá trình ghi dữ liệu bằng cách gọi phương thức EndWrite() trên luồng (stream) của client,
                    // sử dụng đối số result để chỉ ra quá trình ghi đã hoàn thành.
                    obj.stream.EndWrite(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /* được sử dụng để bắt đầu quá trình ghi dữ liệu từ client tới server. 
         * Nó chuyển đổi chuỗi thành mảng byte và gửi dữ liệu qua luồng (stream). 
         * Nếu kết nối vẫn hoạt động, nó sẽ bắt đầu quá trình ghi không đồng bộ và xử lý các ngoại lệ phát sinh.
         */
        private void BeginWrite(string msg)
        {
            // cho phép gửi dữ liệu dưới dạng byte thông qua luồng (stream).
            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            // Kiểm tra xem kết nối của client với server có đang hoạt động hay không.
            if (obj.client.Connected)
            {
                try
                {
                    // Bắt đầu quá trình ghi dữ liệu từ mảng byte buffer vào luồng (stream) của client.
                    // BeginWrite() được sử dụng để bắt đầu quá trình ghi không đồng bộ và nhận một hàm gọi lại (Write) để xử lý kết quả của việc ghi.
                    obj.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client == null || !client.IsAlive)
            {
                Application.Exit();
            }
            else
            {
                obj.client.Close();
            }
        }
    }
}
