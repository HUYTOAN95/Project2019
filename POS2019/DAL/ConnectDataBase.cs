using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.DAL
{
    public static class ConnectDataBase
    {
        public static SqlConnection Cnn;

        public static SqlConnection InfoConnect(string server, string database, string user, string password)
        {
            string CnnString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + user + ";Password=" + password + ";Integrated Security=true";
            Cnn = new SqlConnection(CnnString);
            return Cnn;
        }

        public static bool ExIsOpen(this SqlConnection connection)
        {
            if (connection == null) return false;
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); DAL.Ultis.WriteLog(ex.ToString()); }
            }
            return false;
        }

        public static bool ExIsReady(this SqlConnection connction)
        {
            if (ExIsOpen(connction) == false) return false;
            try
            {
                using (SqlCommand command = new SqlCommand("select 1", connction))
                using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.Read()) return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); DAL.Ultis.WriteLog(ex.ToString()); }
            return false;
        }

    }
}
