using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private const string connetStr = "server=47.113.121.87;port=5200;user=user;password=000000;database=test;Charset=utf8;";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();

                string id = textBox_id.Text.Trim();
                string password = textBox_passwor.Password.Trim();
                if (id == "" || password == "")
                {
                    MessageBox.Show("请输入id和密码！");
                    return;
                }

                string sql = "select * from users where id=" + id;
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string str = reader["password"].ToString();//接收一个返回值
                            if (password == str)
                            {
                                MainWindow mainwindow = new MainWindow(id, reader["name"].ToString());
                                Window window = GetWindow(this);//关闭父窗体
                                window.Close();
                                mainwindow.Show();
                            }
                            else
                            {
                                MessageBox.Show("密码输入错误！");
                                textBox_passwor.Clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("不存在该id!");
                            textBox_passwor.Clear();
                            textBox_id.Clear();
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                MessageBox.Show("连接数据库失败！");
            }
            finally
            {
                conn.Close();
            }
        }

        private void TextBox_id_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Button_Click(null, null);
        }
    }
}
