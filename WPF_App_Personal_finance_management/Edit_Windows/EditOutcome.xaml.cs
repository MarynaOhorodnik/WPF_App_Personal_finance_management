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

namespace WPF_App_Personal_finance_management.Edit_Windows
{
    /// <summary>
    /// Interaction logic for EditOutcome.xaml
    /// </summary>
    public partial class EditOutcome : Window
    {
        private int idOutc;
        private string totalOutc;
        private string nameCtgOutc;
        private string dateOutc;
        private string comOutc;
        public EditOutcome(int id)
        {
            InitializeComponent();

            idOutc = id;

            FillCategory();
            Fill_value(id);
        }

        private void FillCategory()
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_outcome WHERE is_delete = 0 AND user_id = @user_id";

            ArrayList list_str = new ArrayList() { "@user_id" };
            ArrayList list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            cbOutcomeCtg.ItemsSource = table.DefaultView;
            cbOutcomeCtg.Text = default;
        }

        private void Fill_value(int i)
        {
            DB db = new DB();

            string str_command = "SELECT OUTC.id, OUTC.total, OUTC.category_id, CTG.name, FORMAT(OUTC.date, 'dd.MM.yy') AS date , OUTC.comment FROM outcome AS OUTC, category_outcome AS CTG WHERE OUTC.category_id = CTG.id AND OUTC.id = @id";

            ArrayList list_str = new ArrayList() { "@id" };
            ArrayList list_var = new ArrayList() { i };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            totalOutc = table.Rows[0][1].ToString();
            nameCtgOutc = table.Rows[0][3].ToString();
            dateOutc = table.Rows[0][4].ToString();
            comOutc = table.Rows[0][5].ToString();

            tbTotal.Text = totalOutc;
            cbOutcomeCtg.SelectedIndex = IndexComboBox(nameCtgOutc);
            dpDate.SelectedDate = Convert.ToDateTime(dateOutc);
            tbComment.Text = comOutc;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbTotal.Text == totalOutc && (cbOutcomeCtg.SelectedItem as DataRowView).Row["name"].ToString() == nameCtgOutc && dpDate.Text == dateOutc && tbComment.Text == comOutc)
            {
                return;
            }
            else
            {
                DataRowView oDataRowView = cbOutcomeCtg.SelectedItem as DataRowView;
                int id_category = Convert.ToInt32(oDataRowView.Row["id"]);

                string total = tbTotal.Text;
                string date = dpDate.Text;
                string comment = tbComment.Text;

                DB db = new DB();

                string str_command = "UPDATE outcome SET total = @total, category_id = @category_id, date = @date, comment = @comment WHERE outcome.id = @id";
                ArrayList list_str = new ArrayList() { "@total", "@category_id", "@date", "@comment", "@id" };
                ArrayList list_var = new ArrayList() { total, id_category, DateFormat(date), comment, idOutc };

                bool flag = db.EditTable(str_command, list_str, list_var);

                if (flag)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Щось пішло не так!");
                }
            }
        }

        private int IndexComboBox(string name)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_outcome WHERE is_delete = 0 AND user_id = @id";

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
    }
}
