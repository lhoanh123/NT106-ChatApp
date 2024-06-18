using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using chat_app;

namespace Client
{
    public partial class Menu : Form
    {
        public Menu(string name)
        {
            InitializeComponent();

            //Gọi sự kiện cập nhật danh sách bạn bè
            addfriend1.DataChanged += Addfriend_DataChanged;

            //Gọi sự kiện cập nhật dữ liệu tin nhắn
            chat1.DataChanged += Chat_DataChanged;

            //Gán biến username bằng biến name được truyền vô khi gọi form menu
            username = name;

            // Ẩn UserControl Addfriend
            addfriend1.Visible = false;

            // Ẩn UserControl Message_Visibl
            chat1.Visible = false;

            ConnectButton_Click();

            //hiển thị chữ cái đầu trong tên người dùng lên giao diện 
            username_lbl.Text = username.ToString();
        }

        private bool connected = false;
        private Thread client = null;
        //danh sách bạn bè
        private string listfriend = null;
        private struct MyClient
        {
            //tên của người dùng đang kết nối
            public string username;
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
        // tạo một biến kiểu MyClient đã khai báo ở trên
        private static MyClient obj;

        // được sử dụng để thực hiện việc gửi dữ liệu.
        private Task send = null;

        //sử dụng để lưu tên người dùng
        public static string username;

        public static string ipAd_menu;

        //connect với server
        private void ConnectButton_Click()
        {
            if (connected)
            {
                obj.client.Close();
            }

            // kiểm tra xem đã tồn tại một kết nối đến server hay chưa.
            if (client == null || !client.IsAlive)
            {
                /* cài đặt địa chỉ ip & port mặc định 
                    * tạo một luồng mới client và bắt đầu thực thi nó bằng cách gọi phương thức Connection(ip, port). 
                    * Đồng thời, đặt thuộc tính IsBackground của luồng là true để cho phép chương trình thoát ngay cả khi luồng đó chưa hoàn thành.
                    */

                ipAd_menu = SignIn.ipAd_signin;

                IPAddress ip = IPAddress.Parse(ipAd_menu);
                // IPAddress ip = IPAddress.Parse("127.0.0.1");

                client = new Thread(() => Connection(ip, 8000))
                {
                    IsBackground = true
                };
                client.Start();
            }
        }

        // thiết lập kết nối với máy chủ, xác thực
        private void Connection(IPAddress ip, int port)
        {
            // Bắt đầu khối try-catch để xử lý các ngoại lệ có thể xảy ra trong quá trình thiết lập kết nối và đăng nhập.
            try
            {
                //tạo obj connect tới server
                // lưu trữ thông tin và trạng thái của kết nối.
                obj = new MyClient();

                // tạo kết nối TCP.
                obj.client = new TcpClient();


                obj.username = username;

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

                // gọi phương thức để xác thực
                if (Authorize())
                {
                    // Vòng lặp này sẽ tiếp tục chạy khi kết nối với máy chủ vẫn còn hoạt động.
                    while (obj.client.Connected)
                    {
                        try
                        {
                            // Gửi yêu cầu đọc bất đồng bộ từ luồng dữ liệu tới máy chủ bằng phương thức BeginRead.
                            // Khi dữ liệu được đọc, phương thức ReadAuth sẽ được gọi để xử lý kết quả.
                            obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), null);

                            // Chờ đợi cho đến khi nhận được tín hiệu hoàn thành từ máy chủ.
                            // Điều này đảm bảo rằng quá trình đọc dữ liệu đã hoàn thành và obj.response đã được cập nhật.
                            obj.handle.WaitOne();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    //Đóng kết nối
                    obj.client.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // được sử dụng để đọc dữ liệu từ luồng dữ liệu mạng (NetworkStream) trong một phương thức bất đồng bộ.
        private void ReadAuth(IAsyncResult result) //được xác thực và server trả về listfriend của client
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
                        // Gán nội dung của obj.data vào chuỗi.
                        string msg = Crypt.Decryption(obj.data.ToString());
                        if (msg != "Nope")
                        {
                            listfriend = msg;
                            if (listfriend != null)
                            {
                                chat1.Invoke((MethodInvoker)delegate
                                {
                                    chat1.flowpanel(listfriend); // Tạo listfriend và in ra màn hình
                                });
                            }
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
                obj.client.Close();
                obj.handle.Set();
            }
        }

        // Xử lý dữ liệu nhận được từ UserControl(Chat) tại đây
        private void Chat_DataChanged(object sender, string jsdata) // Nhận tin nhắn từ Usercontrol Chat khi client bấm nút Send
        {
            JavaScriptSerializer json = new JavaScriptSerializer(); // Tạo một đối tượng để chuyển đổi dữ liệu giữa định dạng JSON và C#
            Message Message = json.Deserialize<Message>(jsdata); // Chuyển đổi dữ liệu từ JSON sang đối tượng Message
            Message.Sender = username; // Gán tên người gửi cho Message

            string jsonString = json.Serialize(Message); // Chuyển đổi Message sang định dạng JSON
            if (obj.client.Connected) // Kiểm tra xem client có kết nối với server hay không
            {
                Send(jsonString); // Gọi phương thức Send để gửi dữ liệu JSON lên server
            }
        }

        // Xử lý dữ liệu nhận được từ UserControl(Addfriend) tại đây
        private void Addfriend_DataChanged(object sender, string jsdata)// Xử lý dữ liệu nhận được từ UserControl(Addfriend) tại đây
        {
            JavaScriptSerializer json = new JavaScriptSerializer(); // Tạo một đối tượng để chuyển đổi dữ liệu giữa định dạng JSON và C#
            Message Message = json.Deserialize<Message>(jsdata); // Chuyển đổi dữ liệu từ JSON sang đối tượng Message
            Message.Sender = username; // Gán tên người gửi cho Message

            string jsonString = json.Serialize(Message); // Chuyển đổi Message sang định dạng JSON
            if (obj.client.Connected) // Kiểm tra xem client có kết nối với server hay không
            {
                Send(jsonString); // Gọi phương thức Send để gửi dữ liệu JSON lên server
            }
        }

        private bool Authorize()
        {
            // đại diện cho kết quả của quá trình xác thực.
            bool success = false;
            Send("sign in,success," + username); // gửi thông tin là đã xác thực đến server
            // Vòng lặp này sẽ tiếp tục chạy khi kết nối với máy chủ vẫn còn hoạt động.
            while (obj.client.Connected)
            {
                try
                {
                    // Gửi yêu cầu đọc bất đồng bộ từ luồng dữ liệu tới máy chủ bằng phương thức BeginRead.
                    // Khi dữ liệu được đọc, phương thức ReadAuth sẽ được gọi để xử lý kết quả.
                    obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), null); // nhận thông báo kết quả

                    // Chờ đợi cho đến khi nhận được tín hiệu hoàn thành từ máy chủ.
                    // Điều này đảm bảo rằng quá trình đọc dữ liệu đã hoàn thành và obj.response đã được cập nhật.
                    obj.handle.WaitOne();

                    // nếu giá trị của obj.response là "Success", gán giá trị true cho biến success và thoát khỏi vòng lặp.
                    // nếu response Success thì success = true => Authorize() trả về true
                    if (obj.username != null)
                    {
                        success = true;
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

        private void Read(IAsyncResult result) // Đọc tin nhắn server gửi về
        {
            // lưu trữ số lượng byte đã đọc từ luồng dữ liệu.
            int bytes = 0;

            // Kiểm tra xem kết nối của client với server có đang hoạt động hay không.
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
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), null);
                    }
                    else // Nếu không còn dữ liệu khả dụng, thực hiện các hành động sau đó.
                    {
                        JavaScriptSerializer json = new JavaScriptSerializer(); // từ tin nhắn server gửi về Deserialize thành obj Message
                        string msg = Crypt.Decryption(obj.data.ToString());
                        Message Message = json.Deserialize<Message>(msg);
                        AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();
                        if (Message.Receiver.Trim() == username) // Kiểm tra người nhận
                        {
                            if (Message.header == "Message") // Nếu tin nhắn đã nhận là một Message từ người khác
                            {
                                chat1.Invoke((MethodInvoker)delegate
                                {
                                    chat1.Print(Message.message, Message.Sender, Message.Sender);
                                });
                            }
                            else if (Message.header == "Allusers" && Message.Sender == "server") // Server gửi về Alluses đã yêu cầu
                            {
                                addfriend1.Invoke((MethodInvoker)delegate
                                {
                                    addfriend1.flowpanel(Message.message, listfriend);
                                });
                            }
                            else if (Message.header == "Addfriend") // Có client gửi lời mời kết bạn
                            {
                                addfriend1.Invoke((MethodInvoker)delegate
                                {
                                    addfriend1.Print(Message.Sender, "Accept");
                                });

                            }
                            else if (Message.header == "Accept") //Có client đồng ý kết bạn
                            {
                                addfriend1.Invoke((MethodInvoker)delegate
                                {
                                    addfriend1.Print(Message.Sender, "Unfriend");
                                });
                            }
                            else if (Message.header == "Unfriend")
                            {
                                addfriend1.Invoke((MethodInvoker)delegate
                                {
                                    addfriend1.Print(Message.Sender, "Add Friend");
                                });
                            }
                            else if (Message.header == "New listFriend")
                            {
                                listfriend = Message.message;
                                chat1.Invoke((MethodInvoker)delegate
                                {
                                    chat1.flowpanel(listfriend); // Tạo listfriend và in ra màn hình
                                });
                            }
                            obj.data.Clear();
                            obj.handle.Set();
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj.data.Clear();
                    obj.handle.Set();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                obj.client.Close();
                obj.handle.Set();
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

        private void btnMessage_Click(object sender, EventArgs e) // Bấm nút Message thì ẩn Usercontrol AddFriend
        {
            closechat_btn.Visible = true;
            pictureBox1.Visible = false;
            addfriend1.Visible = false;
            closeaddfriend_btn.Visible = false;
            username_lbl.Visible = false;
            chat1.Visible = true;
        }

        private void btnFriend_Click(object sender, EventArgs e) // Bấm nút Friend thì ẩn Usercontrol Message và gửi yêu cần Allusers tới server
        {
            closeaddfriend_btn.Visible = true;
            pictureBox1.Visible = false;
            chat1.Visible = false;
            closechat_btn.Visible = false;
            username_lbl.Visible = false;
            addfriend1.Visible = true;
            Message Message = new Message
            {
                header = "Allusers",
                Sender = username,
                Receiver = "server",
                message = ""
            };
            JavaScriptSerializer json = new JavaScriptSerializer();
            string jsonString = json.Serialize(Message);
            if (obj.client.Connected)
            {
                Send(jsonString);
            }
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e) //Ngắt kết nối
        {
            obj.client.Close();
        }

        private void closechat_btn_Click(object sender, EventArgs e)
        {
            chat1.Visible = false;
            pictureBox1.Visible = true;
            closechat_btn.Visible = false;
            username_lbl.Visible = true;
        }

        private void closeaddfriend_btn_Click(object sender, EventArgs e)
        {
            addfriend1.Visible = false;
            pictureBox1.Visible = true;
            closeaddfriend_btn.Visible = false;
            username_lbl.Visible = true;
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            logout_btn.BackColor = Color.FromArgb(103, 215, 169);
            DialogResult result = MessageBox.Show("Bạn muốn đăng xuất ?", "Log Out Window", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                obj.client.Close();
                this.Invoke((MethodInvoker)delegate
                {
                    this.Hide();
                    Form form = new SignIn();
                    form.ShowDialog();
                    this.Close();
                });
            }
        }
    }
}
