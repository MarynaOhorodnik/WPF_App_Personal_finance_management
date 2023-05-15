using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string pass = passbox.Password.Trim();
            bool flag = true;

            if (login.Length < 1)
            {
                tbLogin.Background = Brushes.MistyRose;
                flag = false;
            }
            else
            {
                tbLogin.Background = Brushes.Transparent;
            }

            if (pass.Length < 1)
            {
                passbox.Background = Brushes.MistyRose;
                flag = false;
            }
            else
            {
                passbox.Background = Brushes.Transparent;
            }

            if (flag)
            {
                DB db = new DB();

                string str_command = "SELECT * FROM users WHERE login = @login AND password = @pass";

                ArrayList list_str = new ArrayList() { "@login", "@pass" };
                ArrayList list_var = new ArrayList() { login, pass };

                DataTable table = db.SelectTable(str_command, list_str, list_var);

                if (table.Rows.Count > 0)
                {
                    _CurrentUser.NewUser(login);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Логін або пароль введені неправильно");
                }
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            this.Close();
        }
    }



}
