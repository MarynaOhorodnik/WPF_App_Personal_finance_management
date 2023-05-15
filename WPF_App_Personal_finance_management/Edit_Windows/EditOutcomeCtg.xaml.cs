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
    /// Interaction logic for EditOutcomeCtg.xaml
    /// </summary>
    public partial class EditOutcomeCtg : Window
    {
        private int idCtg;
        private string nameCtg;
        public EditOutcomeCtg(int id)
        {
            InitializeComponent();
            idCtg = id;
            nameCtg = FindName(id);
            tbName.Text = nameCtg;
        }

        private string FindName(int i)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM category_outcome WHERE id = @id";

            ArrayList list_str = new ArrayList() { "@id" };
            ArrayList list_var = new ArrayList() { i };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            string name = table.Rows[0][1].ToString();
            return name;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text != nameCtg && tbName.Text.Length > 0)
            {
                DB db = new DB();

                string str_command = "UPDATE category_outcome SET name = @name WHERE category_outcome.id = @id";
                ArrayList list_str = new ArrayList() { "@name", "@id" };
                ArrayList list_var = new ArrayList() { tbName.Text, idCtg };

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
    }
}
