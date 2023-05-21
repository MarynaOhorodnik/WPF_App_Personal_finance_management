using System.Collections;
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
                    MessageBox.Show("Ім'я оновлено");
                    _CurrentUser.Name = name;
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
                    MessageBox.Show("Пароль оновлено");
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
    }
}
