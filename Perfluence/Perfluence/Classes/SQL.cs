using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Perfluence.Classes
{
    internal class SQL
    {
        public static MySqlConnection con = new MySqlConnection("server=sql7.freesqldatabase.com; port = 3306;user=sql7624617;pwd=DGsSj9pd2S;database=sql7624617; ConvertZeroDateTime=True ");
        public static DataTable GetTable(string query)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
            adapter.Fill(table);
            return table;
        }
        public static void DoQuery(string query)
        {
            MySqlCommand command= new MySqlCommand(query, con);
            command.ExecuteNonQuery();
        }
    }
}
