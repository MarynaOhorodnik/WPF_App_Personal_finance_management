using System.Collections;
using System.Data;
using System.Windows;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management.Admin_Views
{
    /// <summary>
    /// Interaction logic for AdmWinEditUser.xaml
    /// </summary>
    public partial class AdmWinEditUser : Window
    {
        private int idUser;
        private string name;
        private string login;
        private string email;
        private string password;
        private string is_delete;
        public AdmWinEditUser(int id)
        {
            InitializeComponent();

            idUser = id;

            Fill_value();
        }

        private void Fill_value()
        {
            DB db = new DB();

            string str_command = "SELECT * FROM users WHERE id = @id";

            ArrayList list_str = new ArrayList() { "@id" };
            ArrayList list_var = new ArrayList() { idUser };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            name = table.Rows[0][1].ToString();
            login = table.Rows[0][2].ToString();
            email = table.Rows[0][3].ToString();
            password = table.Rows[0][4].ToString();
            is_delete = table.Rows[0][5].ToString();

            tbName.Text = name;
            tbLogin.Text = login;
            tbEmail.Text = email;
            tbPass.Text = password;
            tbDelete.Text = is_delete;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == name && tbLogin.Text == login && tbPass.Text == password && tbEmail.Text == email && tbDelete.Text == is_delete)
            {
                return;
            }
            else
            {
                string name = tbName.Text;
                string login = tbLogin.Text;
                string email = tbEmail.Text;
                string pass = tbPass.Text;
                string delete = tbDelete.Text;

                bool flag = true;

                if (flag)
                {
                    DB db = new DB();

                    string str_command = "UPDATE users SET name = @name, login = @login, email = @email, password = @password, is_delete = @delete WHERE id = @id";
                    ArrayList list_str = new ArrayList() { "@name", "@login", "@email", "@password", "@delete", "@id" };
                    ArrayList list_var = new ArrayList() { name, login, email, pass, delete, this.idUser };

                    bool flag1 = db.EditTable(str_command, list_str, list_var);

                    if (flag1)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Щось пішло не так!");
                    }
                }
            }
        }
    }
}
