using System;
using System.Collections;
using System.Data;
using System.Windows.Controls;
using WPF_App_Personal_finance_management.Charts;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management.User_Views
{
    /// <summary>
    /// Interaction logic for ChartView.xaml
    /// </summary>
    public partial class ChartView : UserControl
    {
        public ChartView()
        {
            InitializeComponent();

            LineChart();
        }

        private void LineChart()
        {
            DB db = new DB();

            string str_command1 = "SELECT SUM(total) AS sum, MONTH(date) as month FROM income WHERE is_delete = 0 AND user_id = @id GROUP BY MONTH(date) ORDER BY month";
            string str_command2 = "SELECT SUM(total) AS sum, MONTH(date) as month FROM outcome WHERE is_delete = 0 AND user_id = @id GROUP BY MONTH(date) ORDER BY month";
            ArrayList  list_str = new ArrayList() { "@id" };
            ArrayList  list_var = new ArrayList() { _CurrentUser.Id };

            DataTable table1 = db.SelectTable(str_command1, list_str, list_var);
            DataTable table2 = db.SelectTable(str_command2, list_str, list_var);


            ArrayList list_inc = new ArrayList();
            ArrayList list_inc_tit = new ArrayList();
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                list_inc.Add(Convert.ToDouble(table1.Rows[i][0]));
                list_inc_tit.Add(Convert.ToInt32(table1.Rows[i][1]));
            }

            ArrayList list_outc = new ArrayList();
            ArrayList list_outc_tit = new ArrayList();
            string s;
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                s = table2.Rows[i][0].ToString().Substring(1);
                list_outc.Add(Convert.ToDouble(s));
                list_outc_tit.Add(Convert.ToInt32(table2.Rows[i][1]));
            }


            LineControl.DataContext = new TotalLineChart(list_inc, list_inc_tit, list_outc, list_outc_tit);
        }
    }
}
