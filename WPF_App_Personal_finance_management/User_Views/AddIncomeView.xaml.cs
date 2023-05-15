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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management.User_Views
{
    /// <summary>
    /// Interaction logic for AddIncomeView.xaml
    /// </summary>
    public partial class AddIncomeView : UserControl
    {
        public AddIncomeView()
        {
            InitializeComponent();

            FillCategory();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string total = tbTotal.Text;
            string category = cbIncomeCtg.Text;
            string date = dpDate.Text;
            string comment = tbComment.Text;
            bool flag = true;

            if (!total.All(c => char.IsDigit(c)) || total == "")
            {
                tbTotal.Background = Brushes.MistyRose;
                flag = false;
            }
            else
            {
                tbTotal.Background = Brushes.Transparent;
            }

            if (category == default || category == "")
            {
                cbIncomeCtg.Background = Brushes.MistyRose;
                flag = false;
            }
            else
            {
                cbIncomeCtg.Background = Brushes.Transparent;
            }

            if (date == "")
            {
                dpDate.Background = Brushes.MistyRose;
                flag = false;
            }
            else
            {
                dpDate.Background = Brushes.Transparent;
            }

            if (flag)
            {
                DataRowView oDataRowView = cbIncomeCtg.SelectedItem as DataRowView;
                int id_category = Convert.ToInt32(oDataRowView.Row["id"]);

                DB db = new DB();

                string str_command = "INSERT INTO income (total, category_id, date, comment, user_id) VALUES (@total, @category_id, @date, @comment, @user_id);";
                ArrayList list_str = new ArrayList() { "@total", "@category_id", "@date", "@comment", "@user_id" };
                ArrayList list_var = new ArrayList() { total, id_category, DateFormat(date), comment, _CurrentUser.Id };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    tbTotal.Text = "";
                    cbIncomeCtg.SelectedItem = default;
                    dpDate.SelectedDate = default;
                    tbComment.Text = "";
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }
        }

        private void FillCategory()
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_income WHERE is_delete = 0 AND user_id = @user_id ORDER BY name";

            ArrayList list_str = new ArrayList() { "@user_id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            cbIncomeCtg.ItemsSource = table.DefaultView;
            cbIncomeCtg.Text = default;
        }

        private string DateFormat(string str)
        {
            string day = str.Substring(0, 2);
            string month = str.Substring(3, 2);
            string year = str.Substring(6);

            string result = year + "-" + month + "-" + day;
            return result;
        }
    }
}
