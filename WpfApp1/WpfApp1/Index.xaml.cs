using System;
using System.Collections.Generic;
using System.Media;
using System.Net.Sockets;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : UserControl
    {
        public Index()
        {
            InitializeComponent();
        }

        public Index(TcpClient tcpClient, string self_id, string self_name)
        {
            this.tcpClient = tcpClient;
            this.SelfId = self_id;
            this.SelfName = self_name;
            InitializeComponent();
        }

        private Dictionary<string, Chat> list_chat = new Dictionary<string, Chat>();
        private Dictionary<string, Friend> list_buddy = new Dictionary<string, Friend>();
        private int list_top = 0;
        private TcpClient tcpClient;
        private string SelfId;
        private string SelfName;

        public void AddBuddy(string id, string name)
        {
            if (list_chat.ContainsKey(id))
                return;

            Friend friend = new Friend(id, name);
            friend.MouseDoubleClick += new MouseButtonEventHandler(Friend_DoubleClick);
            list_buddy.Add(id, friend);
            Chat chat = new Chat(friend, tcpClient, SelfId, id, SelfName, name);
            list_chat.Add(id, chat);
            MsgList.Children.Insert(list_top, friend);
        }

        public void ReceiveMsg(string id, DateTime dateTime, string str)
        {
            list_buddy[id].FlashBuddy(dateTime, str, false);
            list_chat[id].ShowMsg(dateTime, str, false);

            using (SoundPlayer player = new SoundPlayer(@"Resources/ding.wav"))
            {
                player.Play();
            }
        }

        private void Friend_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Friend friend = sender as Friend;
            if (chatBox.Children.Contains(list_chat[friend.getId()]))
                return;

            chatBox.Children.Clear();
            chatBox.Children.Add(list_chat[friend.getId()]);
            friend.FlashBuddy(DateTime.Now, null);
        }
    }
}
