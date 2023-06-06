using System.Collections;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management
{
    /// <summary>
    /// Interaction logic for RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text.Trim();
            string login = tbLogin.Text.Trim();
            string email = tbEmail.Text.Trim();
            string pass1 = passbox1.Password.Trim();
            string pass2 = passbox2.Password.Trim();
            bool flag = true;

            if (name.Length < 3)
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

            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                EditTextBox(tbEmail, "Некоректне значення", true);
                flag = false;
            }
            else
            {
                EditTextBox(tbEmail);
            }

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

                string str_command = "INSERT INTO users (name, login, email, password) VALUES (@name, @login, @email, @pass)";
                ArrayList list_str = new ArrayList() { "@name", "@login", "@email", "@pass" };
                ArrayList list_var = new ArrayList() { name, login, email, pass1 };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    MessageBox.Show("Успіх!");

                    ArrayList list_command = new ArrayList();
                    list_command.Add("INSERT INTO category_income (name, user_id) VALUES ('Заробітна плата', @user_id);");
                    list_command.Add("INSERT INTO category_income (name, user_id) VALUES ('Разова робота', @user_id);");
                    list_command.Add("INSERT INTO category_income (name, user_id) VALUES ('Подарунок', @user_id);");
                    list_command.Add("INSERT INTO category_outcome (name, user_id) VALUES ('Продукти', @user_id);");
                    list_command.Add("INSERT INTO category_outcome (name, user_id) VALUES ('Транспорт', @user_id);");
                    list_command.Add("INSERT INTO category_outcome (name, user_id) VALUES ('Розваги', @user_id);");
                    list_command.Add("INSERT INTO category_outcome (name, user_id) VALUES ('Спорт', @user_id);");

                    ArrayList list_str1 = new ArrayList() { "@user_id" };
                    ArrayList list_var1 = new ArrayList() { FindId(login) };

                    for (int i = 0; i < list_command.Count; i++)
                    {
                        db.EditTable(list_command[i].ToString(), list_str1, list_var1);
                    }

                    AuthWindow authWindow = new AuthWindow();
                    authWindow.Show();
                    this.Close();
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

        private string FindId(string login)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM users WHERE login = @login AND is_delete = 0";

            ArrayList list_str = new ArrayList() { "@login" };
            ArrayList list_var = new ArrayList() { login };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            string id = table.Rows[0][0].ToString();

            return id;
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }
}
