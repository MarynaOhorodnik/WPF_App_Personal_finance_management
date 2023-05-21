using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management.Edit_Windows
{
    /// <summary>
    /// Interaction logic for EditIncome.xaml
    /// </summary>
    public partial class EditIncome : Window
    {
        private int idInc;
        private string totalInc;
        private string nameCtgInc;
        private string dateInc;
        private string comInc;
        public EditIncome(int id)
        {
            InitializeComponent();
            idInc = id;

            FillCategory();
            Fill_value(id);
        }

        private void FillCategory()
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_income WHERE is_delete = 0 AND user_id = @user_id";

            ArrayList list_str = new ArrayList() { "@user_id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            cbIncomeCtg.ItemsSource = table.DefaultView;
            cbIncomeCtg.Text = default;
        }

        private void Fill_value(int i)
        {
            DB db = new DB();

            string str_command = "SELECT INC.id, INC.total, INC.category_id, CTG.name, FORMAT(INC.date, 'dd.MM.yy') AS date , INC.comment FROM income AS INC, category_income AS CTG WHERE INC.category_id = CTG.id AND INC.id = @id";

            ArrayList list_str = new ArrayList() { "@id" };
            ArrayList list_var = new ArrayList() { i };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            totalInc = table.Rows[0][1].ToString();
            nameCtgInc = table.Rows[0][3].ToString();
            dateInc = table.Rows[0][4].ToString();
            comInc = table.Rows[0][5].ToString();

            tbTotal.Text = totalInc;
            cbIncomeCtg.SelectedIndex = IndexComboBox(nameCtgInc);
            dpDate.SelectedDate = Convert.ToDateTime(dateInc);
            tbComment.Text = comInc;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbTotal.Text == totalInc && (cbIncomeCtg.SelectedItem as DataRowView).Row["name"].ToString() == nameCtgInc && dpDate.Text == dateInc && tbComment.Text == comInc)
            {
                return;
            }
            else
            {
                DataRowView oDataRowView = cbIncomeCtg.SelectedItem as DataRowView;
                int id_category = Convert.ToInt32(oDataRowView.Row["id"]);

                string total = tbTotal.Text;
                string date = dpDate.Text;
                string comment = tbComment.Text;
                bool flag = true;

                float f;
                int i;

                if (total == "")
                {
                    EditTextBox(tbTotal, false);
                    flag = false;
                }
                else if (int.TryParse(total, out i) | float.TryParse(total.Replace(".", ","), out f))
                {
                    if (f > 0)
                    {
                        EditTextBox(tbTotal);
                    }
                    else if (i > 0)
                    {
                        EditTextBox(tbTotal);
                    }
                    else
                    {
                        EditTextBox(tbTotal, false);
                        flag = false;
                    }
                }
                else
                {
                    EditTextBox(tbTotal, false);
                    flag = false;
                }

                if (date == "")
                {
                    EditDatePicker(dpDate, false);
                    flag = false;
                }
                else
                {
                    EditDatePicker(dpDate);
                }

                if (flag)
                {
                    DB db = new DB();

                    string str_command = "UPDATE income SET total = @total, category_id = @category_id, date = @date, comment = @comment WHERE income.id = @id";
                    ArrayList list_str = new ArrayList() { "@total", "@category_id", "@date", "@comment", "@id" };
                    ArrayList list_var = new ArrayList() { total.Replace(",", "."), id_category, DateFormat(date), comment, idInc };

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

        private int IndexComboBox(string name)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_income WHERE is_delete = 0 AND user_id = @id";

            ArrayList list_str = new ArrayList() { "@id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][1].ToString() == name)
                {
                    return i;
                }
            }
            return -1;
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
