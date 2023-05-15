using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_App_Personal_finance_management.Classes
{
    public static class _CurrentUser
    {
        private static string id;
        private static string name;
        private static string login;
        private static string email;
        private static string pass;

        public static string Id
        {
            get { return id; }
            set { id = value; }
        }

        public static string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static string Login
        {
            get { return login; }
            set { login = value; }
        }

        public static string Email
        {
            get { return email; }
            set { email = value; }
        }

        public static string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public static void NewUser(string login_user)
        {
            DB db = new DB();

            string str_command = "SELECT * FROM users WHERE login = @login";

            ArrayList list_str = new ArrayList() { "@login" };
            ArrayList list_var = new ArrayList() { login_user };

            DataTable table = db.SelectTable(str_command, list_str, list_var);

            Id = table.Rows[0][0].ToString();
            Name = table.Rows[0][1].ToString();
            Login = table.Rows[0][2].ToString();
            Email = table.Rows[0][3].ToString();
            Pass = table.Rows[0][4].ToString();
        }
    }
}
