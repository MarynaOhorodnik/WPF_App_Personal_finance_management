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
    /// Interaction logic for AddOutcomeView.xaml
    /// </summary>
    public partial class AddOutcomeView : UserControl
    {
        public AddOutcomeView()
        {
            InitializeComponent();

            FillCategory();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string total = tbTotal.Text;
            string category = cbOutcomeCtg.Text;
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
                cbOutcomeCtg.Background = Brushes.MistyRose;
                flag = false;
            }
            else
            {
                cbOutcomeCtg.Background = Brushes.Transparent;
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
                DataRowView oDataRowView = cbOutcomeCtg.SelectedItem as DataRowView;
                int id_category = Convert.ToInt32(oDataRowView.Row["id"]);
                double total_min = Convert.ToDouble(total);
                total_min = total_min - 2 * total_min;

                DB db = new DB();

                string str_command = "INSERT INTO outcome (total, category_id, date, comment, user_id) VALUES (@total, @category_id, @date, @comment, @user_id);";
                ArrayList list_str = new ArrayList() { "@total", "@category_id", "@date", "@comment", "@user_id" };
                ArrayList list_var = new ArrayList() { total_min, id_category, DateFormat(date), comment, _CurrentUser.Id };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    tbTotal.Text = "";
                    cbOutcomeCtg.SelectedItem = default;
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

            string str_command = "SELECT * FROM category_outcome WHERE is_delete = 0 AND user_id = @user_id ORDER BY name";

            ArrayList list_str = new ArrayList() { "@user_id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            cbOutcomeCtg.ItemsSource = table.DefaultView;
            cbOutcomeCtg.Text = default;
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
