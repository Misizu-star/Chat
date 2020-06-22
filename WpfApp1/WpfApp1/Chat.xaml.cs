using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Chat.xaml 的交互逻辑
    /// </summary>
    public partial class Chat : UserControl
    {
        public Chat(Friend friend, TcpClient client, string self_id, string side_id, string self_name, string side_name)
        {
            this.friend = friend;
            this.tcpClient = client;
            this.self_id = self_id;
            this.side_id = side_id;
            this.self_name = self_name;
            this.side_name = side_name;

            InitializeComponent();
        }

        Friend friend;

        TcpClient tcpClient;
        string self_id;
        string side_id;
        string self_name;
        string side_name;

        private void Time_Loaded(object sender, RoutedEventArgs e)
        {
            name.Content = side_name;
        }

        /// <summary>
        /// 向TextBox插入换行键及按下Enter键发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //按下Ctrl+Enter输入换行键
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Enter)
            {
                //添加换行符
                text_msg.AppendText("\n");
                //重新使光标定位到文本末尾
                text_msg.SelectionStart = text_msg.Text.Length;
            }
            //按下Enter发送消息
            else if (e.Key == Key.Enter)
            {
                Send_MouseDown(null, null);
            }
        }

        /// <summary>
        /// 点击发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if(text_msg.Text.Length==0 || text_msg.Text.Length > 400)
            {
                return;
            }

            //向服务器发送消息
            SendToServe(text_msg.Text);

            //清空文本框
            text_msg.Clear();
        }

        /// <summary>
        /// 发送消息给服务器
        /// </summary>
        /// <param name="str">发送内容</param>
        private void SendToServe(string str)
        {
            try
            {
                //向服务器发送消息
                NetworkStream stream = tcpClient.GetStream();

                byte[] msg = Encoding.UTF8.GetBytes(str);
                byte[] buffer = new byte[24 + msg.Length];

                string time = DateTime.Now.ToString("yyyyMMddHHmm");
                Encoding.UTF8.GetBytes(time).CopyTo(buffer, 0);
                Encoding.UTF8.GetBytes(self_id).CopyTo(buffer, 12);
                Encoding.UTF8.GetBytes(side_id).CopyTo(buffer, 18);
                msg.CopyTo(buffer, 24);
                stream.Write(buffer, 0, buffer.Length);

                //显示消息
                ShowMsg(DateTime.Now, str);
                friend.FlashBuddy(DateTime.Now, str);
            }
            catch
            {
                MessageBox.Show("CHat Wrong");
            }
        }


        /// <summary>
        /// 窗口显示发送或接受的消息
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="str"></param>
        /// <param name="isSelf"></param>
        public void ShowMsg(DateTime dateTime, string str, bool isSelf = true)
        {
            //使用委托解决跨线程访问空间问题
            Action action = () =>
            {
                Message message = new Message();
                if (isSelf)
                {
                    message.FlowDirection = FlowDirection.RightToLeft;
                    message.HorizontalAlignment = HorizontalAlignment.Right;
                    ((TextBox)message.FindName("text")).FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    message.HorizontalAlignment = HorizontalAlignment.Left;
                }
                ((TextBox)message.FindName("text")).Text = str;
                //((Image)message.FindName("img")).Source = @"C:\Users\狐狸233\Desktop\123.jpg";
                
                ((Label)message.FindName("time")).Content = dateTime.ToShortTimeString();
                MsgBox.Children.Add(message);
            };

            MsgBox.Dispatcher.Invoke(action);
        }

        
    }
}
