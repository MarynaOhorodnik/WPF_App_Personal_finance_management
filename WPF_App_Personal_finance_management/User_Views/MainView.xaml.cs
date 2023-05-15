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
using WPF_App_Personal_finance_management.Charts;
using WPF_App_Personal_finance_management.Classes;
using WPF_App_Personal_finance_management.Edit_Windows;

namespace WPF_App_Personal_finance_management.User_Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            cbMonth.ItemsSource = new string[] { "Січень", "Лютий", "Березень", "Квітень", "Травень", "Червень", "Липень", "Серпень", "Вересень", "Жовтень", "Листопад", "Грудень" };
            cbYear.ItemsSource = new string[] { "2023", "2022", "2021", "2020", "2019", "2018", "2017", "2016", "2015" };

            FillDate();
            ReloadAll();
        }

        private void FillDate()
        {
            System.DateTime now = System.DateTime.Today;
            string month = System.DateTime.Today.Month.ToString();
            cbMonth.SelectedIndex = Convert.ToInt32(month) - 1;
            cbYear.SelectedIndex = 0;
        }

        private void ReloadAll(int flag = 0)
        {
            string str_command_inc = "";
            string str_command_outc = "";
            string str_command_incchart = "";
            string str_command_outcchart = "";
            string str_command_tot1 = "";
            string str_command_tot2 = "";
            ArrayList list_str = new ArrayList{ };
            ArrayList list_var = new ArrayList { };

            DB db = new DB();
            
            
            if(flag == 0)
            {
                int month = cbMonth.SelectedIndex + 1;
                int year = Convert.ToInt32(cbYear.SelectedItem);

                str_command_inc = "SELECT INC.id, INC.total, INC.category_id, FORMAT(INC.date, 'dd.MM.yy') AS date , INC.comment, INC.is_delete, INC.user_id, CTG.name " +
                    "FROM income AS INC, category_income AS CTG " +
                    "WHERE INC.category_id = CTG.id AND INC.is_delete = 0 AND INC.user_id = @id AND MONTH(INC.date) = @month AND YEAR(INC.date) = @year " +
                    "ORDER BY INC.date";
                str_command_outc = "SELECT OUTC.id, OUTC.total, OUTC.category_id, FORMAT(OUTC.date, 'dd.MM.yy') AS date , OUTC.comment, OUTC.is_delete, OUTC.user_id, CTG.name " +
                    "FROM outcome AS OUTC, category_outcome AS CTG " +
                    "WHERE OUTC.category_id = CTG.id AND OUTC.is_delete = '0' AND OUTC.user_id = @id AND MONTH(OUTC.date) = @month AND YEAR(OUTC.date) = @year " +
                    "ORDER BY OUTC.date";
                str_command_incchart = "SELECT SUM(INC.total) AS sum, INC.category_id, CTG.name " +
                    "FROM income AS INC, category_income AS CTG WHERE INC.category_id = CTG.id AND INC.is_delete = 0 AND INC.user_id = @id " +
                    "AND MONTH(INC.date) = @month AND YEAR(INC.date) = @year GROUP BY INC.category_id, CTG.name";
                str_command_outcchart = "SELECT SUM(OUTC.total) AS sum, OUTC.category_id, CTG.name " +
                    "FROM outcome AS OUTC, category_outcome AS CTG WHERE OUTC.category_id = CTG.id AND OUTC.is_delete = 0 AND OUTC.user_id = @id " +
                    "AND MONTH( OUTC.date) = @month AND YEAR( OUTC.date) = @year GROUP BY OUTC.category_id, CTG.name";
                str_command_tot1 = "SELECT SUM(total) AS sum FROM income WHERE is_delete = 0 AND user_id = @id AND MONTH(date) = @month AND YEAR(date) = @year";
                str_command_tot2 = "SELECT SUM(total) AS sum FROM outcome WHERE is_delete = 0 AND user_id = @id AND MONTH(date) = @month AND YEAR(date) = @year";

                list_str = new ArrayList() { "@id", "@month", "@year" };
                list_var = new ArrayList() { _CurrentUser.Id, month, year };
            }
            else if(flag == 1)
            {
                cbYear.SelectedIndex = 0;
                int year = Convert.ToInt32(cbYear.SelectedItem);

                str_command_inc = "SELECT INC.id, INC.total, INC.category_id, FORMAT(INC.date, 'dd.MM.yy') AS date , INC.comment, INC.is_delete, INC.user_id, CTG.name " +
                    "FROM income AS INC, category_income AS CTG " +
                    "WHERE INC.category_id = CTG.id AND INC.is_delete = 0 AND INC.user_id = @id AND YEAR(INC.date) = @year " +
                    "ORDER BY INC.date";
                str_command_outc = "SELECT OUTC.id, OUTC.total, OUTC.category_id, FORMAT(OUTC.date, 'dd.MM.yy') AS date , OUTC.comment, OUTC.is_delete, OUTC.user_id, CTG.name " +
                    "FROM outcome AS OUTC, category_outcome AS CTG " +
                    "WHERE OUTC.category_id = CTG.id AND OUTC.is_delete = '0' AND OUTC.user_id = @id AND YEAR(OUTC.date) = @year " +
                    "ORDER BY OUTC.date";
                str_command_incchart = "SELECT SUM(INC.total) AS sum, INC.category_id, CTG.name " +
                    "FROM income AS INC, category_income AS CTG WHERE INC.category_id = CTG.id AND INC.is_delete = 0 AND INC.user_id = @id " +
                    "AND YEAR(INC.date) = @year GROUP BY INC.category_id, CTG.name";
                str_command_outcchart = "SELECT SUM(OUTC.total) AS sum, OUTC.category_id, CTG.name " +
                    "FROM outcome AS OUTC, category_outcome AS CTG WHERE OUTC.category_id = CTG.id AND OUTC.is_delete = 0 AND OUTC.user_id = @id " +
                    "AND YEAR( OUTC.date) = @year GROUP BY OUTC.category_id, CTG.name";
                str_command_tot1 = "SELECT SUM(total) AS sum FROM income WHERE is_delete = 0 AND user_id = @id AND YEAR(date) = @year";
                str_command_tot2 = "SELECT SUM(total) AS sum FROM outcome WHERE is_delete = 0 AND user_id = @id AND YEAR(date) = @year";

                list_str = new ArrayList() { "@id", "@year" };
                list_var = new ArrayList() { _CurrentUser.Id, year };
            }
            else if (flag == 2)
            {
                str_command_inc = "SELECT INC.id, INC.total, INC.category_id, FORMAT(INC.date, 'dd.MM.yy') AS date , INC.comment, INC.is_delete, INC.user_id, CTG.name " +
                    "FROM income AS INC, category_income AS CTG " +
                    "WHERE INC.category_id = CTG.id AND INC.is_delete = 0 AND INC.user_id = @id " +
                    "ORDER BY INC.date";
                str_command_outc = "SELECT OUTC.id, OUTC.total, OUTC.category_id, FORMAT(OUTC.date, 'dd.MM.yy') AS date , OUTC.comment, OUTC.is_delete, OUTC.user_id, CTG.name " +
                    "FROM outcome AS OUTC, category_outcome AS CTG " +
                    "WHERE OUTC.category_id = CTG.id AND OUTC.is_delete = '0' AND OUTC.user_id = @id " +
                    "ORDER BY OUTC.date";
                str_command_incchart = "SELECT SUM(INC.total) AS sum, INC.category_id, CTG.name " +
                    "FROM income AS INC, category_income AS CTG WHERE INC.category_id = CTG.id AND INC.is_delete = 0 AND INC.user_id = @id " +
                    "GROUP BY INC.category_id, CTG.name";
                str_command_outcchart = "SELECT SUM(OUTC.total) AS sum, OUTC.category_id, CTG.name " +
                    "FROM outcome AS OUTC, category_outcome AS CTG WHERE OUTC.category_id = CTG.id AND OUTC.is_delete = 0 AND OUTC.user_id = @id " +
                    "GROUP BY OUTC.category_id, CTG.name";
                str_command_tot1 = "SELECT SUM(total) AS sum FROM income WHERE is_delete = 0 AND user_id = @id";
                str_command_tot2 = "SELECT SUM(total) AS sum FROM outcome WHERE is_delete = 0 AND user_id = @id";

                list_str = new ArrayList() { "@id" };
                list_var = new ArrayList() { _CurrentUser.Id };
            }




            DataTable table_inc = db.SelectTable(str_command_inc, list_str, list_var);
            ReloadTableIncome(table_inc);


            DataTable table_outc = db.SelectTable(str_command_outc, list_str, list_var);
            ReloadTableOutcome(table_outc);


            DataTable table_incchart = db.SelectTable(str_command_incchart, list_str, list_var);
            IncomePieChart(table_incchart);


            DataTable table_outcchart = db.SelectTable(str_command_outcchart, list_str, list_var);
            OutcomePieChart(table_outcchart);

            DataTable table_tot1 = db.SelectTable(str_command_tot1, list_str, list_var);
            DataTable table2_tot2 = db.SelectTable(str_command_tot2, list_str, list_var);
            Total(table_tot1 , table2_tot2);

        }

        private void ReloadTableIncome(DataTable table)
        {
            objectsListIncome.ItemsSource = table.DefaultView;

            if (table.Rows.Count == 0)
            {
                ResultTxtIncome.Text = "Немає даних";
            }
            else
            {
                ResultTxtIncome.Text = default;
            }
        }

        private void ReloadTableOutcome(DataTable table)
        {
            objectsListOutcome.ItemsSource = table.DefaultView;

            if (table.Rows.Count == 0)
            {
                ResultTxtOutcome.Text = "Немає даних";
            }
            else
            {
                ResultTxtOutcome.Text = default;
            }
        }

        private void IncomePieChart(DataTable table)
        {
            ArrayList list_title = new ArrayList();
            ArrayList list_var = new ArrayList();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                list_title.Add(table.Rows[i][2].ToString());
                list_var.Add(Convert.ToDouble(table.Rows[i][0]));
            }

            IncomeControl.DataContext = new IncomePieChart(list_title, list_var);
        }

        private void OutcomePieChart(DataTable table)
        {
            ArrayList list_title = new ArrayList();
            ArrayList list_var = new ArrayList();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                list_title.Add(table.Rows[i][2].ToString());
                list_var.Add(Convert.ToDouble(table.Rows[i][0]));
            }

            OutcomeControl.DataContext = new OutcomePieChart(list_title, list_var);
        }

        private void Total(DataTable table1, DataTable table2)
        {
            double income;
            double outcome;
            double outcome1;

            if (table1.Rows[0][0] != System.DBNull.Value)
            {
                income = Convert.ToDouble(table1.Rows[0][0]);
            }
            else
            {
                income = 0;
            }

            if (table2.Rows[0][0] != System.DBNull.Value)
            {
                outcome = Convert.ToDouble(table2.Rows[0][0]);
                outcome1 = outcome + (-2 * outcome);
            }
            else
            {
                outcome = outcome1 = 0;
            }

            double total = income + outcome;

            TotalControl.DataContext = new TotalBarChart(income, outcome1);

            btIncome.Content = "Надходження: " + income.ToString();
            btOutcome.Content = "Витрати: " + outcome.ToString();
            btTotal.Content = "Баланс: " + total.ToString();
        }

        private void IncomeEditButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)(sender)).Tag);
            EditIncome edit = new EditIncome(id);
            edit.Show();
        }

        private void IncomeDelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMes = MessageBox.Show("Ви бажаєте видалити?", "Підтвердження", MessageBoxButton.YesNo);
            switch (resultMes)
            {
                case MessageBoxResult.Yes:

                    int id = Convert.ToInt32(((Button)(sender)).Tag);
                    DB db = new DB();

                    string str_command = "UPDATE income SET is_delete = 1 WHERE income.id = @id";
                    ArrayList list_str = new ArrayList() { "@id" };
                    ArrayList list_var = new ArrayList() { id };

                    bool flag = db.EditTable(str_command, list_str, list_var);

                    if (flag)
                    {
                        ReloadAll();
                    }
                    else
                    {
                        MessageBox.Show("Щось пішло не так!");
                    }

                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void OutcomeEditButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)(sender)).Tag);
            EditOutcome edit = new EditOutcome(id);
            edit.Show();
        }

        private void OutcomeDelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMes = MessageBox.Show("Ви бажаєте видалити?", "Підтвердження", MessageBoxButton.YesNo);
            switch (resultMes)
            {
                case MessageBoxResult.Yes:

                    int id = Convert.ToInt32(((Button)(sender)).Tag);
                    DB db = new DB();

                    string str_command = "UPDATE outcome SET is_delete = 1 WHERE outcome.id = @id";
                    ArrayList list_str = new ArrayList() { "@id" };
                    ArrayList list_var = new ArrayList() { id };

                    bool flag = db.EditTable(str_command, list_str, list_var);

                    if (flag)
                    {
                        ReloadAll();
                    }
                    else
                    {
                        MessageBox.Show("Щось пішло не так!");
                    }

                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        
        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            FillDate();
            ReloadAll();
        }

        private void YearButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadAll(1);
            cbMonth.SelectedIndex = -1;
            cbYear.SelectedIndex = 0;
        }

        private void PeriodButton_Click(object sender, RoutedEventArgs e)
        {
            
            ReloadAll(2);
            cbMonth.SelectedIndex = -1;
            cbYear.SelectedIndex = -1;
        }

        private void CbYear_DropDownClosed(object sender, EventArgs e)
        {
            ReloadAll();
        }

        private void CbMonth_DropDownClosed(object sender, EventArgs e)
        {
            ReloadAll();
        }
    }
}
