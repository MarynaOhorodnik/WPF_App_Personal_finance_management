﻿using System.Collections;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management.User_Views
{
    /// <summary>
    /// Interaction logic for EditInfoView.xaml
    /// </summary>
    public partial class EditInfoView : UserControl
    {
        public EditInfoView()
        {
            InitializeComponent();

            tbName.Text = _CurrentUser.Name;
            tbLogin.Text = _CurrentUser.Login;
            tbEmail.Text = _CurrentUser.Email;
            passbox1.Password = _CurrentUser.Pass;
        }

        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text.Trim();
            bool flag = true;


            if (name == _CurrentUser.Name)
            {
                flag = false;
            }
            else if (name.Length < 3)
            {
                EditTextBox(tbName, "Мінімальна довжина ім'я 3 символи", true);
                flag = false;
            }
            else if (name.Length > 20)
            {
                EditTextBox(tbName, "Максимальна довжина ім'я - 20 символів", true);
                flag = false;
            }
            else if (!name.All(c => char.IsLetter(c)))
            {
                EditTextBox(tbName, "Ім'я може містити лише літери", true);
                flag = false;
            }
            else
            {
                EditTextBox(tbName);
            }

            if (flag)
            {
                DB db = new DB();

                string str_command = "UPDATE users SET name = @name WHERE users.id = @id;";
                ArrayList list_str = new ArrayList() { "@name", "@id" };
                ArrayList list_var = new ArrayList() { name, _CurrentUser.Id };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    MessageBox.Show("Ім'я змінено");
                    _CurrentUser.Name = name;
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }

        }

        private void EditLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            bool flag = true;


            if (login.Length < 5)
            {
                EditTextBox(tbLogin, "Мінімальна довжина логіну 5 символів", true);
                flag = false;
            }
            else if (login.Length > 20)
            {
                EditTextBox(tbLogin, "Максимальна довжина логіну - 20 символів", true);
                flag = false;
            }
            else if (!login.All(c => char.IsLetterOrDigit(c) || c == '_'))
            {
                EditTextBox(tbLogin, "Логін може містити лише букви, цифри та символ _ ", true);
                flag = false;
            }
            else if (CheckLogin(login))
            {
                EditTextBox(tbLogin, "Такий логін вже існує", true);
                flag = false;
            }
            else
            {
                EditTextBox(tbLogin);
            }

            if (flag)
            {
                DB db = new DB();

                string str_command = "UPDATE users SET login = @login WHERE users.id = @id;";
                ArrayList list_str = new ArrayList() { "@login", "@id" };
                ArrayList list_var = new ArrayList() { login, _CurrentUser.Id };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    MessageBox.Show("Логін змінено");
                    _CurrentUser.Login = login;
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }
        }

        private void EditEmailButton_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text.Trim();
            bool flag = true;


            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                EditTextBox(tbEmail, "Некоректне значення", true);
                flag = false;
            }
            else
            {
                EditTextBox(tbEmail);
            }

            if (flag)
            {
                DB db = new DB();

                string str_command = "UPDATE users SET email = @email WHERE users.id = @id;";
                ArrayList list_str = new ArrayList() { "@email", "@id" };
                ArrayList list_var = new ArrayList() { email, _CurrentUser.Id };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    MessageBox.Show("Email змінено");
                    _CurrentUser.Email = email;
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }
        }

        private void EditPassButton_Click(object sender, RoutedEventArgs e)
        {
            string pass1 = passbox1.Password.Trim();
            string pass2 = passbox2.Password.Trim();
            bool flag = true;

            if (pass1.Length < 5)
            {
                EditPassBox(passbox1, "Мінімальна довжина паролю 5 символів", true);
                flag = false;
            }
            else
            {
                EditPassBox(passbox1);
                if (pass1 != pass2)
                {
                    EditPassBox(passbox2, "Паролі не збігаються", true);
                    flag = false;
                }
                else
                {
                    EditPassBox(passbox2);
                }
            }

            if (flag)
            {
                DB db = new DB();

                string str_command = "UPDATE users SET password = @pass WHERE users.id = @id;";
                ArrayList list_str = new ArrayList() { "@pass", "@id" };
                ArrayList list_var = new ArrayList() { pass1, _CurrentUser.Id };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    MessageBox.Show("Пароль змінено");
                    _CurrentUser.Pass = pass1;
                    passbox1.Password = "";
                    passbox2.Password = "";
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }

        }

        private void EditTextBox(TextBox textbox, string str = "", bool mark = false)
        {
            if (mark)
            {
                textbox.ToolTip = str;
                textbox.Background = Brushes.MistyRose;
            }
            else
            {
                textbox.ToolTip = default;
                textbox.Background = Brushes.Transparent;
            }
        }

        private void EditPassBox(PasswordBox passbox, string str = "", bool mark = false)
        {
            if (mark)
            {
                passbox.ToolTip = str;
                passbox.Background = Brushes.MistyRose;
            }
            else
            {
                passbox.ToolTip = default;
                passbox.Background = Brushes.Transparent;
            }
        }

        private bool CheckLogin(string login)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM users WHERE login = @login AND is_delete = 0";

            ArrayList list_str = new ArrayList() { "@login" };
            ArrayList list_var = new ArrayList() { login };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }


    }
}
