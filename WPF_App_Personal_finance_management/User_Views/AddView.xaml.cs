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
using System.Xml.Linq;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management.User_Views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();
            FillCategory();
        }

        private void SaveButtonInc_Click(object sender, RoutedEventArgs e)
        {
            string total = tbTotalInc.Text;
            string category = cbIncomeCtg.Text;
            string date = dpDateInc.Text;
            string comment = tbCommentInc.Text;
            bool flag = true;

            float f;
            int i;

            if (total == "")
            {
                EditTextBox(tbTotalInc, false);
                flag = false;
            }
            else if (int.TryParse(total, out i) | float.TryParse(total.Replace(".", ","), out f))
            {
                if(f > 0)
                {
                    EditTextBox(tbTotalInc);
                }
                else if(i > 0)
                {
                    EditTextBox(tbTotalInc);
                }
                else
                {
                    EditTextBox(tbTotalInc, false);
                    flag = false;
                }          
            }
            else
            {
                EditTextBox(tbTotalInc, false);
                flag = false;
            }

            if (category == default || category == "")
            {
                EditComboBox(cbIncomeCtg, false);
                flag = false;
            }
            else
            {
                EditComboBox(cbIncomeCtg);
            }

            if (date == "")
            {
                EditDatePicker(dpDateInc, false);
                flag = false;
            }
            else
            {
                EditDatePicker(dpDateInc);
            }

            if (flag)
            {
                DataRowView oDataRowView = cbIncomeCtg.SelectedItem as DataRowView;
                int id_category = Convert.ToInt32(oDataRowView.Row["id"]);

                DB db = new DB();

                string str_command = "INSERT INTO income (total, category_id, date, comment, user_id) VALUES (@total, @category_id, @date, @comment, @user_id)";
                ArrayList list_str = new ArrayList() { "@total", "@category_id", "@date", "@comment", "@user_id" };
                ArrayList list_var = new ArrayList() { total.Replace(",", "."), id_category, DateFormat(date), comment, _CurrentUser.Id };

                bool flag2 = db.EditTable(str_command, list_str, list_var);

                if (flag2)
                {
                    tbTotalInc.Text = "";
                    cbIncomeCtg.SelectedItem = default;
                    dpDateInc.SelectedDate = default;
                    tbCommentInc.Text = "";
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }
        }

        private void SaveButtonOutc_Click(object sender, RoutedEventArgs e)
        {
            string total = tbTotalOutc.Text;
            string category = cbOutcomeCtg.Text;
            string date = dpDateOutc.Text;
            string comment = tbCommentOutc.Text;
            bool flag = true;


            float f;
            int i;

            if (total == "")
            {
                EditTextBox(tbTotalOutc, false);
                flag = false;
            }
            else if (int.TryParse(total, out i) | float.TryParse(total.Replace(".", ","), out f))
            {
                if (f > 0)
                {
                    EditTextBox(tbTotalOutc);
                }
                else if (i > 0)
                {
                    EditTextBox(tbTotalOutc);
                }
                else
                {
                    EditTextBox(tbTotalOutc, false);
                    flag = false;
                }
            }
            else
            {
                EditTextBox(tbTotalOutc, false);
                flag = false;
            }


            if (category == default || category == "")
            {
                EditComboBox(cbOutcomeCtg, false);
                flag = false;
            }
            else
            {
                EditComboBox(cbOutcomeCtg);
            }

            if (date == "")
            {
                EditDatePicker(dpDateOutc, false);
                flag = false;
            }
            else
            {
                EditDatePicker(dpDateOutc);
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
                    tbTotalOutc.Text = "";
                    cbOutcomeCtg.SelectedItem = default;
                    dpDateOutc.SelectedDate = default;
                    tbCommentOutc.Text = "";
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

            string str_command1 = "SELECT * FROM category_income WHERE is_delete = 0 AND user_id = @user_id ORDER BY name";
            string str_command2 = "SELECT * FROM category_outcome WHERE is_delete = 0 AND user_id = @user_id ORDER BY name";

            ArrayList list_str = new ArrayList() { "@user_id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table1 = db.SelectTable(str_command1, list_str, list_var);
            DataTable table2 = db.SelectTable(str_command2, list_str, list_var);

            cbIncomeCtg.ItemsSource = table1.DefaultView;
            cbIncomeCtg.Text = default;
            cbOutcomeCtg.ItemsSource = table2.DefaultView;
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

        private void EditTextBox(TextBox box, bool mark = true)
        {
            if (mark)
            {
                box.Background = Brushes.Transparent;
            }
            else
            {
                box.Background = Brushes.MistyRose;
            }
        }

        private void EditComboBox(ComboBox box, bool mark = true)
        {
            if (mark)
            {
                box.Background = Brushes.Transparent;
            }
            else
            {
                box.Background = Brushes.MistyRose;
            }
        }

        private void EditDatePicker(DatePicker box, bool mark = true)
        {
            if (mark)
            {
                box.Background = Brushes.Transparent;
            }
            else
            {
                box.Background = Brushes.MistyRose;
            }
        }
    }
}
