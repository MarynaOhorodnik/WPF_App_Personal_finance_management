using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace WPF_App_Personal_finance_management.Classes
{
    class DB
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-RJ8LN0H\\SQLEXPRESS;Initial Catalog=Finance_account;Integrated Security=True");

        public void openConnection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        public SqlConnection getConnection()
        {
            return connection;
        }

        public DataTable SelectTable(string str_command, ArrayList list_str, ArrayList list_var)
        {
            DataTable table = new DataTable();
            int v = 0;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(str_command, this.getConnection());
                for (int i = 0; i < list_str.Count; i++)
                {
                    command.Parameters.AddWithValue($"{list_str[v]}", list_var[v]);
                    v += 1;
                }

                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            catch
            {
                MessageBox.Show("Виникли проблеми з підключенням бази даних. Перевірте з'єднання.");
            }

            return table;
        }

        public bool EditTable(string str_command, ArrayList list_str, ArrayList list_var)
        {
            bool flag = true;
            int v = 0;

            try
            {
                SqlCommand command = new SqlCommand(str_command, this.getConnection());

                for (int i = 0; i < list_str.Count; i++)
                {
                    command.Parameters.AddWithValue($"{list_str[v]}", list_var[v]);
                    v += 1;
                }

                this.openConnection();

                command.ExecuteNonQuery();

                this.closeConnection();
            }

            catch
            {
                MessageBox.Show("Виникли проблеми з підключенням бази даних. Перевірте з'єднання.");
                flag = false;
            }

            return flag;
        }
    }
}
