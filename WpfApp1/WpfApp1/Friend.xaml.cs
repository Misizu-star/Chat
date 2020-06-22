using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Friend.xaml 的交互逻辑
    /// </summary>
    public partial class Friend : UserControl
    {
        public Friend(string id, string name)
        {
            this.id = id;
            this.name = name;
            InitializeComponent();
        }

        string id;
        string name;

        public string getId()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            F_name.Content = name;
        }

        public void FlashBuddy(DateTime dateTime, string str=null, bool isSelf = true)
        {
            if (str == null)
            {
                F_new.Content = "";
                return;
            }

            F_msg.Content = str;
            F_time.Content = dateTime.ToShortTimeString();

            if (!isSelf)
                F_new.Content = "";
            else
                F_new.Content = "";
        }
    }
}
