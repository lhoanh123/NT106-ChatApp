using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace server
{
    internal class Modify
    {
        public Modify()
        {


        }
        SqlCommand sqlCommand;
        SqlDataReader reader;
        public static List<TaiKhoan> TaiKhoans (string querry)
        {
            List<TaiKhoan> list = new List<TaiKhoan>();
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(querry, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                   list.Add(new TaiKhoan(reader.GetString(0), reader.GetString(1)));
                }    
                sqlConnection.Close();
            }    

                return list;
        }
        public static List<string> send (string querry)
        {
            List<string> list = new List<string>();
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(querry, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
                sqlConnection.Close();
            }
            return list;
        }
        public static void Command(string querry)
        {
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(querry, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
