using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string self_id, string self_name)
        {
            this.SelfId = self_id;
            this.SelfName = self_name;
            InitializeComponent();
        }

        private TcpClient tcpClient;
        IPEndPoint remoteIEP = new IPEndPoint(IPAddress.Parse("47.113.121.87"), 5202);
        private Index index_element;

        //待开发部分
        private UIElement buddy_element;

        private Dictionary<string, Chat> list_chat = new Dictionary<string, Chat>();


        //暂定为一号用户
        private string SelfId;
        private string SelfName;


        private const string ConnetMySQLBuddy = "server=47.113.121.87;port=5200;user=user;password=000000;database=buddy;Charset=utf8;";
        private const string ConnetMySQLMessages = "server=47.113.121.87;port=5200;user=user;password=000000;database=messages;Charset=utf8;";
        private const string ConnetMySQLUser = "server=47.113.121.87;port=5200;user=user;password=000000;database=test;Charset=utf8;";


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //连接服务器
            try
            {
                //连接服务器
                tcpClient = new TcpClient();
                tcpClient.Connect(remoteIEP);

                //向服务器发送消息（协议——self_id）
                NetworkStream stream = tcpClient.GetStream();
                byte[] msg = Encoding.UTF8.GetBytes(SelfId);
                stream.Write(msg, 0, msg.Length);
            }
            catch
            {
                MessageBox.Show("连接错误");
                this.Close();
            }

            user_Name.Content = SelfName;
            index_element = new Index(tcpClient, SelfId, SelfName);

            using (MySqlConnection conn = new MySqlConnection(ConnetMySQLBuddy))
            {
                try
                {
                    conn.Open();

                    string sql1 = "select * from `" + SelfId + "`";

                    using (MySqlCommand cmd = new MySqlCommand(sql1, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["id"].ToString();
                                string nickName = reader["nickName"].ToString();

                                index_element.AddBuddy(id, nickName);
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            main_box.Children.Add(index_element);

            //采用异步从数据库接受离线消息
            getNewMsg();

            //采用异步受服务器消息
            Receive();
        }

        /// <summary>
        /// 获取离线消息
        /// </summary>
        private async void getNewMsg()
        {
            using (MySqlConnection conn = new MySqlConnection(ConnetMySQLMessages))
            {
                try
                {
                    await conn.OpenAsync();
                    string sql1 = "select * from `" + SelfId + "`";
                    using (MySqlCommand cmd = new MySqlCommand(sql1, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string from_id = reader["from"].ToString();
                                string msg = reader["text"].ToString();
                                string dt = reader["datetime"].ToString();
                                DateTime dateTime = DateTime.ParseExact(dt, "yyyyMMddHHmm", CultureInfo.CurrentCulture);

                                index_element.ReceiveMsg(from_id, dateTime, msg);
                            }
                        }
                    }

                    string sql2 = "delete from `" + SelfId + "`";
                    using (MySqlCommand cmd2 = new MySqlCommand(sql2, conn))
                    {
                        cmd2.ExecuteNonQuery();
                    }

                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 采用异步接收消息
        /// </summary>
        private async void Receive()
        {
            try
            {
                NetworkStream stream = tcpClient.GetStream();
                while (true)
                {
                    byte[] buffer = new byte[tcpClient.Available];
                    await stream.ReadAsync(buffer, 0, buffer.Length);

                    //解决未接到消息时窗体添加空消息
                    if (buffer.Length > 0)
                    {
                        //分析消息协议
                        string dt = Encoding.UTF8.GetString(buffer.Skip(0).Take(12).ToArray());
                        DateTime dateTime = DateTime.ParseExact(dt, "yyyyMMddHHmm", CultureInfo.CurrentCulture);
                        string from_id = Encoding.UTF8.GetString(buffer.Skip(12).Take(6).ToArray());
                        string msg = Encoding.UTF8.GetString(buffer.Skip(24).Take(buffer.Length - 24).ToArray());

                        index_element.ReceiveMsg(from_id, dateTime, msg);
                    }
                }
            }
            catch
            {

            }
        }

        private void ShowIndex(object sender, MouseButtonEventArgs e)
        {
            if (!main_box.Children.Contains(index_element))
            {
                main_box.Children.Clear();
                main_box.Children.Add(index_element);
            }
        }

        private void ShowBuddy(object sender, MouseButtonEventArgs e)
        {
            main_box.Children.Clear();
        }
    }
}
