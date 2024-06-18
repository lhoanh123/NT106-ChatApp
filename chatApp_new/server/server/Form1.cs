using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Threading;
using System.Net.Configuration;
using System.Web.Script.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\codeVS\MY\crypto\chatApp_unfriend\chatApp_new\server\server\Database1.mdf;Integrated Security=True");
            cn.Open();
        }
        private SqlCommand cmd;
        private SqlDataReader dr;
        private SqlConnection cn;

        private Thread listener = null;
        private long id = 0;

        private struct MyClient
        {
            public long id;

            // lưu trữ tên người dùng của client.
            public StringBuilder username;

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

        /* ConcurrentDictionary là một cấu trúc dữ liệu thread-safe trong C#, cho phép truy cập đồng thời từ nhiều luồng mà không cần sử dụng khóa. 
         * Trong trường hợp này, clients được sử dụng để lưu trữ danh sách các client đang kết nối theo khóa dựa trên kiểu dữ liệu long 
         * và giá trị là một đối tượng của cấu trúc MyClient (đã được định nghĩa trước đó).
         */
        private ConcurrentDictionary<long, MyClient> clients = new ConcurrentDictionary<long, MyClient>();

        // được sử dụng để thực hiện việc gửi dữ liệu.
        private Task send = null;
        /* Task đại diện cho một công việc được thực hiện bất đồng bộ trong một luồng riêng biệt.*/

        // kiểm tra điều kiện thoát khỏi chương trình. 
        private bool exit = false;

        // In ra màn hình
        private void print(string msg)
        {
            // Sử dụng phương thức Invoke để thực hiện các thao tác trên giao diện người dùng (UI) mà không gây ra xung đột giữa các luồng (thread).
            listView1.Invoke((MethodInvoker)delegate
            {
                if (msg.Length > 0)
                {
                    listView1.Items.Add(msg);
                }
            });
        }

        private string listFriend(string user)
        {
            string friend = null;
            string querry = "Select username1 from Friend where username2 ='" + user + "'";
            List<string> list = Modify.send(querry);  // trả về ds trong db where username1 = username
            foreach (string item in list)
            {
                friend += item + ",";
            }
            querry = "Select username2 from Friend where username1 ='" + user + "'";
            list = Modify.send(querry);// trả về ds trong db where username2 = username
            foreach (string item in list)
            {
                friend += item + ",";
            }
            return friend;
        }

        /* Đọc dữ liệu từ luồng NetworkStream.
         * Xử lý dữ liệu đọc được và thực hiện các tác vụ tương ứng.
         * Gửi phản hồi trả về cho client thông qua phương thức Send.
         * Xóa dữ liệu đệm và đánh dấu hoàn thành xử lý tác vụ.
         * ///////////////////////////////////////////////////
         * IAsyncResult cho phép bạn thực hiện các hoạt động bất đồng bộ 
         * và theo dõi và xử lý kết quả của chúng một cách linh hoạt và tiện lợi.
         */
        private void ReadAuth(IAsyncResult result)
        {
            // Ép kiểu biến result.AsyncState về kiểu MyClient để lấy thông tin của client liên quan đến kết quả đọc.
            // result.AsyncState là một thuộc tính của đối tượng IAsyncResult
            // và được sử dụng để lưu trữ dữ liệu bổ sung được truyền vào khi gọi một hoạt động bất đồng bộ (asynchronous).
            MyClient obj = (MyClient)result.AsyncState;

            // lưu số lượng byte đã đọc từ luồng NetworkStream.
            int bytes = 0;

            // kiểm tra xem client vẫn đang kết nối hay không bằng cách sử dụng thuộc tính Connected của đối tượng TcpClient
            if (obj.client.Connected)
            {
                try
                {
                    // Gọi phương thức EndRead() của đối tượng NetworkStream để kết thúc việc đọc bất đồng bộ và trả về số lượng byte đã đọc thành công từ luồng. 
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (bytes > 0)
            {
                string msg = Crypt.Decryption(Encoding.UTF8.GetString(obj.buffer, 0, bytes)); //msg đầu tiên gửi từ client đến server
                print(msg);
                string[] m = msg.Split(',');
                string tag = m[0];     // tách chuỗi ra làm 3 phần
                string name = m[1];    // name (Sign in) hoặc success (Menu)
                string pass = m[2];    // pass(Sign in) hoặc username (Menu)
                try
                {
                    if (tag == "sign in" && name == "success") // Gửi từ form Menu, server trả về ds bạn bè
                    {
                        //send là listfriend của user
                        string send = listFriend(pass);
                        if (send != null)
                        {
                            //msg = Create_Message("Listfriend", "server", obj.username.ToString(), send);
                            Send(send, obj); //gửi listfriend cho client
                            print(send);
                        }
                        else
                        {
                            Send("Nope", obj);
                        }
                        obj.username.Append(pass); // add giá trị cho username để authorize() trả về true
                    }
                    else if (tag == "sign in")   // Gửi trừ form Sign in, server trả về success hoặc fail
                    {
                        string querry = "Select * from Login where username = '" + name + "' and password = '" + pass + "'";

                        if (Modify.TaiKhoans(querry).Count != 0) // Nếu có tài khoản nào có username và password như đã gửi về 
                        {
                            Send("Success", obj);
                        }
                        else
                        {
                            Send("Fail", obj);
                        }
                    }
                    else if (tag == "sign up")  // Gửi từ form Sign up, server trả về success hoặc fail(chưa check)
                    {
                        string querry = "Select * from Login where username = '" + name + "' and password = '" + pass + "'";
                        if (Modify.TaiKhoans(querry).Count != 0)// Nếu có tài khoản nào có username và password như đã gửi về 
                        {
                            Send("Already", obj);
                        }
                        else
                        {
                            try
                            {
                                //Thêm username và password của người dùng mới vô database
                                querry = "Insert into Login values ('" + name + "','" + pass + "')";
                                Modify.Command(querry);
                                Send("Valid", obj);
                            }
                            catch
                            {
                                Send("Error", obj);
                            }
                        }
                    }
                    else
                    {
                        Send("error format", obj);
                    }
                    obj.data.Clear();
                    obj.handle.Set();
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
        private bool Authorize(MyClient obj)
        {
            bool success = false;
            while (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), obj); //read thông tin gửi đến từ client
                    obj.handle.WaitOne();
                    if (obj.username.Length > 0)
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
            return success;
        }
        private void Read(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (bytes > 0)
            {
                obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                    }
                    else
                    {
                        string msg = Crypt.Decryption(string.Format("{0}", obj.data));
                        JavaScriptSerializer json = new JavaScriptSerializer();
                        Message Message = json.Deserialize<Message>(msg);
                        print(Message.message);
                        if (Message.header == "Message")
                        {
                            print(msg);
                            Send(msg, Message.Receiver);
                        }
                        else if (Message.header == "Allusers")
                        {
                            string querry = "Select * from Login";
                            List<TaiKhoan> list = Modify.TaiKhoans(querry);
                            string send = null;
                            foreach (TaiKhoan item in list)
                            {
                                send += item.Username + ",";
                            }
                            msg = Create_Message("Allusers", "server", obj.username.ToString(), send);
                            print(send);
                            Send(msg, obj);
                        }
                        else if (Message.header == "Addfriend" && Message.message == "1") //client gửi lời mời kết bạn
                        {
                            print(msg);
                            Send(msg, Message.Receiver.Trim());
                        }
                        else if (Message.header == "Accept" && Message.message == "2")
                        {
                            print(msg);
                            Send(msg, Message.Receiver.Trim());
                            string querry = "Select * from Friend where username1 = '" + Message.Sender + "' and username2 = '" + Message.Receiver.Trim() + "' or username2 = '" + Message.Sender +"'and username1 = '" + Message.Receiver.Trim()+"'";
                            if (Modify.send(querry).Count == 0)
                            {
                                try   // add tên vào table Friend
                                {
                                    querry = "Insert into Friend values ('" + Message.Sender + "','" + Message.Receiver + "')";
                                    Modify.Command(querry);
                                    print("new friends " + Message.Sender +" "+ Message.Receiver);
                                }
                                catch
                                {
                                    print("error add friend");
                                }
                            }
                            //gửi lại listFriend cho cả hai client để cập nhập Chat
                            string list = listFriend(Message.Sender);
                            string message = Create_Message("New listFriend", "server", Message.Sender, list);
                            Send(message, Message.Sender);
                            list = listFriend(Message.Receiver.Trim());
                            message = Create_Message("New listFriend", "server", Message.Receiver, list);
                            Send(message, Message.Receiver.Trim());

                        }
                        else if (Message.header == "Unfriend" && Message.message == "3")
                        {
                            print(msg);
                            Send(msg, Message.Receiver.Trim());
                            string querry = "Delete from Friend where username1 = '" + Message.Sender + "' and username2 = '" + Message.Receiver.Trim() + "' or username2 = '" + Message.Sender + "'and username1 = '" + Message.Receiver.Trim() + "'";
                            Modify.Command(querry);
                            print("delete friends " + Message.Sender + " " + Message.Receiver);

                            string list = listFriend(Message.Sender);
                            string message = Create_Message("New listFriend", "server", Message.Sender, list);
                            Send(message, Message.Sender);
                            list = listFriend(Message.Receiver.Trim());
                            message = Create_Message("New listFriend", "server", Message.Receiver, list);
                            Send(message, Message.Receiver.Trim());
                        }
                        obj.data.Clear();
                        obj.handle.Set();
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

        //tạo message để gửi đi
        private string Create_Message(string head, string send, string receive, string message) // tạo Message dạng json
        {
            Message Message = new Message
            {
                header = head,
                Sender = send,
                Receiver = receive,
                message = message
            };
            JavaScriptSerializer json = new JavaScriptSerializer(); // feel free to use JSON serializer
            string jsonString = json.Serialize(Message);
            return jsonString;
        }
        private void Connection(MyClient obj)
        {

            if (Authorize(obj))
            {
                clients.TryAdd(obj.id, obj); // xác thực thành công thêm client vào ds client
                AddToGrid(obj.id, obj.username.ToString());

                while (obj.client.Connected)
                {
                    try
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);// Read trong khi client kết nối
                        obj.handle.WaitOne();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                obj.client.Close();
                clients.TryRemove(obj.id, out MyClient tmp);
                RemoveFromGrid(tmp.id);
            }
        }
        private void Listener(IPAddress ip, int port)
        {
            TcpListener tcplistener = null;
            tcplistener = new TcpListener(ip, port);
            tcplistener.Start();
            while (true)
            {
                if (tcplistener.Pending())
                {
                    try
                    {
                        MyClient obj = new MyClient();
                        obj.id = id;
                        obj.username = new StringBuilder();
                        obj.client = tcplistener.AcceptTcpClient(); // chấp nhận kết nối
                        print("One connected");
                        obj.stream = obj.client.GetStream();
                        obj.buffer = new byte[obj.client.ReceiveBufferSize];
                        obj.data = new StringBuilder();
                        obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                        Thread th = new Thread(() => Connection(obj)) //Tạo luồng Connection của obj
                        {
                            IsBackground = true
                        };
                        th.Start();
                        id++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private string GetLocalIPAddress()
        {
            string ipAddress = string.Empty;

            // Lấy tất cả các địa chỉ IP của máy
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                // Chỉ lấy địa chỉ IP4
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }

            return ipAddress;
        }

        private void button1_Click(object sender, EventArgs e) //Mở kết nối
        {

            if (listener == null || !listener.IsAlive)
            {
                //string address = "127.0.0.1";
                string ipAddress = null;
                bool error = false;
                IPAddress ip = null;
                try
                {
                    //ip = Dns.Resolve(address).AddressList[0];
                    ipAddress = GetLocalIPAddress();
                    ip = IPAddress.Parse(ipAddress);
                }
                catch
                {
                    error = true;
                }
                int port = 8000;
                listView1.Items.Add(ipAddress + ":" + port);
                if (!error)
                {
                    listener = new Thread(() => Listener(ip, port))
                    {
                        IsBackground = true
                    };
                    listener.Start();
                }
            }

        }
        private void BeginWrite(string msg, string username) // gửi tin nhắn cho client có name = username (đang online)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            foreach (KeyValuePair<long, MyClient> obj in clients)
            {
                if (username == obj.Value.username.ToString() && obj.Value.client.Connected)
                {
                    try
                    {
                        obj.Value.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj.Value);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void BeginWriteAll(string msg, long id = -1) // // gửi tin nhắn cho tất cả client có trong ds (đang online)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            foreach (KeyValuePair<long, MyClient> obj in clients)
            {
                if (id != obj.Value.id && obj.Value.client.Connected)
                {
                    try
                    {
                        obj.Value.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj.Value);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void Write(IAsyncResult result) //Thực hiện write
        {
            MyClient obj = (MyClient)result.AsyncState;
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.EndWrite(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BeginWrite(string msg, MyClient obj) // gửi tin nhắn cho obj đang trong luồng thực thi
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        //gửi cho client khác được yêu cầu
        private void Send(string msg, string username)
        {
            string msgEncrypt = Crypt.Encryption(msg);

            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => BeginWrite(msgEncrypt, username));
            }
            else
            {
                send.ContinueWith(antecendent => BeginWrite(msgEncrypt, username));
            }
        }

        // gửi cho client đang thực hiện xử lí
        private void Send(string msg, MyClient obj)
        {
            string msgEncrypt = Crypt.Encryption(msg);

            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => BeginWrite(msgEncrypt, obj));
            }
            else
            {
                send.ContinueWith(antecendent => BeginWrite(msgEncrypt, obj));
            }
        }
        //gửi cho tất các client
        private void Send(string msg, long id = -1)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => BeginWriteAll(msg, id));
            }
            else
            {
                send.ContinueWith(antecendent => BeginWriteAll(msg, id));
            }
        }

        private void AddToGrid(long id, string name) // thêm client (online) vào grid
        {
            if (!exit)
            {
                clientsDataGridView.Invoke((MethodInvoker)delegate
                {
                    string[] row = new string[] { id.ToString(), name };
                    clientsDataGridView.Rows.Add(row);
                });
            }
        }

        private void RemoveFromGrid(long id)    // thêm client (online) vào grid
        {
            if (!exit)
            {
                clientsDataGridView.Invoke((MethodInvoker)delegate
                {
                    foreach (DataGridViewRow row in clientsDataGridView.Rows)
                    {
                        if (row.Cells["identifier"].Value.ToString() == id.ToString())
                        {
                            clientsDataGridView.Rows.RemoveAt(row.Index);
                            break;
                        }
                    }
                });
            }
        }

    }
}
