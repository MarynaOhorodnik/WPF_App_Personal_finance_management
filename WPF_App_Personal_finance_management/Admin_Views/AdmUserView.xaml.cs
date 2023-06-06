using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF_App_Personal_finance_management.Classes;

namespace WPF_App_Personal_finance_management.Admin_Views
{
    /// <summary>
    /// Interaction logic for AdmUserView.xaml
    /// </summary>
    public partial class AdmUserView : UserControl
    {
        public AdmUserView()
        {
            InitializeComponent();

            ReloadTable();
        }

        private void ReloadTable()
        {
            DB db = new DB();

            string str_command = "SELECT * FROM users WHERE id != 1";

            ArrayList list_str = new ArrayList() { };
            ArrayList list_var = new ArrayList() { };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            listUsers.ItemsSource = table.DefaultView;

            if (table.Rows.Count == 0)
            {
                ResultTxt.Text = "Немає даних";
            }
            else
            {
                ResultTxt.Text = default;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).Tag);
            AdmWinEditUser edit = new AdmWinEditUser(id);
            edit.Show();
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadTable();
        }
    }
}
