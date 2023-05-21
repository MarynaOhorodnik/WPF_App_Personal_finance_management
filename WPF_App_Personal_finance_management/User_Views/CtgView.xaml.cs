using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_App_Personal_finance_management.Classes;
using WPF_App_Personal_finance_management.Edit_Windows;

namespace WPF_App_Personal_finance_management.User_Views
{
    /// <summary>
    /// Interaction logic for CtgView.xaml
    /// </summary>
    public partial class CtgView : UserControl
    {
        public CtgView()
        {
            InitializeComponent();
            ReloadTableInc();
            ReloadTableOutc();
        }

        private void ReloadTableInc()
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_income WHERE is_delete = 0 AND user_id = @user_id ORDER BY name";

            ArrayList list_str = new ArrayList() { "@user_id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            listIncomeCtg.ItemsSource = table.DefaultView;

            if (table.Rows.Count == 0)
            {
                ResultTxtInc.Text = "Немає даних";
            }
            else
            {
                ResultTxtInc.Text = default;
            }
        }

        private void SaveButtonInc_Click(object sender, RoutedEventArgs e)
        {
            string name = tbNameCtgInc.Text;

            if (name.Length < 1)
            {
                tbNameCtgInc.Background = Brushes.MistyRose;
            }
            else if (name.Length > 20)
            {
                tbNameCtgInc.Background = Brushes.MistyRose;
                tbNameCtgInc.ToolTip = "Максимальна назва категорії - 20 символів";
            }
            else if (!name.All(c => char.IsLetter(c) || c == ' '))
            {
                tbNameCtgInc.Background = Brushes.MistyRose;
                tbNameCtgInc.ToolTip = "Назва категорії може містити лише літери";
            }
            else if (CheckNameCtgInc(name))
            {
                tbNameCtgInc.Background = Brushes.MistyRose;
                tbNameCtgInc.ToolTip = "Така категорія вже існує";
            }
            else
            {
                tbNameCtgInc.Background = Brushes.Transparent;
                tbNameCtgInc.ToolTip = default;


                DB db = new DB();

                string str_command = "INSERT INTO category_income (name, user_id) VALUES (@name, @user_id);";
                ArrayList list_str = new ArrayList() { "@name", "@user_id" };
                ArrayList list_var = new ArrayList() { name, _CurrentUser.Id };

                bool flag = db.EditTable(str_command, list_str, list_var);

                if (flag)
                {
                    tbNameCtgInc.Text = "";
                    ReloadTableInc();
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }
        }

        private void ReloadButtonInc_Click(object sender, RoutedEventArgs e)
        {
            ReloadTableInc();
        }

        private bool CheckNameCtgInc(string nm)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_income WHERE name = @name AND is_delete = 0 AND user_id = @id";

            ArrayList list_str = new ArrayList() { "@name", "@id" };
            ArrayList list_var = new ArrayList() { nm, _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private bool CheckCtgInc(int id)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM income WHERE category_id = @id AND is_delete = 0 AND user_id = @user_id";

            ArrayList list_str = new ArrayList() { "@id", "@user_id" };
            ArrayList list_var = new ArrayList() { id, _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void EditButtonInc_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)(sender)).Tag);
            EditIncomeCtg edit = new EditIncomeCtg(id);
            edit.Show();
        }

        private void DelButtonInc_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMes = MessageBox.Show("Ви бажаєте видалити дану категорію?", "Підтвердження", MessageBoxButton.YesNo);
            switch (resultMes)
            {
                case MessageBoxResult.Yes:

                    int id = Convert.ToInt32(((Button)(sender)).Tag);
                    if (CheckCtgInc(id))
                    {
                        MessageBox.Show("Ви не можете видалити дану категорію. Спочатку видаліть надходження, що пов'язані з цією категорією.");
                    }
                    else
                    {
                        DB db = new DB();

                        string str_command = "UPDATE category_income SET is_delete = 1 WHERE category_income.id = @id;";
                        ArrayList list_str = new ArrayList() { "@id" };
                        ArrayList list_var = new ArrayList() { id };

                        bool flag = db.EditTable(str_command, list_str, list_var);

                        if (!flag)
                        {
                            MessageBox.Show("Щось пішло не так!");
                        }

                        ReloadTableInc();
                    }

                    break;

                case MessageBoxResult.No:
                    break;
            }
        }




        private void ReloadTableOutc()
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_outcome WHERE is_delete = 0 AND user_id = @user_id ORDER BY name";

            ArrayList list_str = new ArrayList() { "@user_id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            listOutcomeCtg.ItemsSource = table.DefaultView;

            if (table.Rows.Count == 0)
            {
                ResultTxtOutc.Text = "Немає даних";
            }
            else
            {
                ResultTxtOutc.Text = default;
            }
        }
        
        private void SaveButtonOutc_Click(object sender, RoutedEventArgs e)
        {
            string name = tbNameCtgOutc.Text;

            if (name.Length < 1)
            {
                tbNameCtgOutc.Background = Brushes.MistyRose;
            }
            else if (name.Length > 20)
            {
                tbNameCtgOutc.Background = Brushes.MistyRose;
                tbNameCtgOutc.ToolTip = "Максимальна назва категорії - 20 символів";
            }
            else if (!name.All(c => char.IsLetter(c) || c == ' '))
            {
                tbNameCtgOutc.Background = Brushes.MistyRose;
                tbNameCtgOutc.ToolTip = "Назва категорії може містити лише літери";
            }
            else if (CheckNameCtgOutc(name))
            {
                tbNameCtgOutc.Background = Brushes.MistyRose;
                tbNameCtgOutc.ToolTip = "Така категорія вже існує";
            }
            else
            {
                tbNameCtgOutc.Background = Brushes.Transparent;
                tbNameCtgOutc.ToolTip = default;

                DB db = new DB();

                string str_command = "INSERT INTO category_outcome (name, user_id) VALUES (@name, @user_id);";
                ArrayList list_str = new ArrayList() { "@name", "@user_id" };
                ArrayList list_var = new ArrayList() { name, _CurrentUser.Id };

                bool flag = db.EditTable(str_command, list_str, list_var);

                if (flag)
                {
                    tbNameCtgOutc.Text = "";
                    ReloadTableOutc();
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }
        }

        private void ReloadButtonOutc_Click(object sender, RoutedEventArgs e)
        {
            ReloadTableOutc();
        }

        private bool CheckNameCtgOutc(string nm)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_outcome WHERE name = @name AND is_delete = 0 AND user_id = @id";

            ArrayList list_str = new ArrayList() { "@name", "@id" };
            ArrayList list_var = new ArrayList() { nm, _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private bool CheckCtgOutc(int id)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM outcome WHERE category_id = @id AND is_delete = 0 AND user_id = @user_id";

            ArrayList list_str = new ArrayList() { "@id", "@user_id" };
            ArrayList list_var = new ArrayList() { id, _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void EditButtonOutc_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)(sender)).Tag);
            EditOutcomeCtg edit = new EditOutcomeCtg(id);
            edit.Show();
        }

        private void DelButtonOutc_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMes = MessageBox.Show("Ви бажаєте видалити дану категорію?", "Підтвердження", MessageBoxButton.YesNo);
            switch (resultMes)
            {
                case MessageBoxResult.Yes:

                    int id = Convert.ToInt32(((Button)(sender)).Tag);
                    if (CheckCtgOutc(id))
                    {
                        MessageBox.Show("Ви не можете видалити дану категорію. Спочатку видаліть витрати, що пов'язані з цією категорією.");
                    }
                    else
                    {
                        DB db = new DB();

                        string str_command = "UPDATE category_outcome SET is_delete = 1 WHERE category_outcome.id = @id;";
                        ArrayList list_str = new ArrayList() { "@id" };
                        ArrayList list_var = new ArrayList() { id };

                        bool flag = db.EditTable(str_command, list_str, list_var);

                        if (!flag)
                        {
                            MessageBox.Show("Щось пішло не так!");
                        }

                        ReloadTableOutc();
                    }

                    break;

                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
