using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_EasySave.Model
{
    public static class DataBaseConnection
    {
        //public static List<Travail> RequestSelectOnly(string querystring)
        //{
        //    SqlConnection con;
        //    try
        //    {
        //        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Model\DataBase.mdf;Integrated Security=True");
        //        con.Open();
        //        SqlCommand command = new SqlCommand(querystring, con);
        //        con.Close();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            List<Travail> result = new List<Travail>();

        //            while (reader.Read())
        //            {
        //                Travail temp = new Travail(int.Parse(reader["Id"].ToString()));
        //                temp.Name = reader["Name"].ToString();
        //                temp.SaveType = (TypeSave)Enum.Parse(typeof(TypeSave), reader["SaveType"].ToString());
        //                temp.TaskName = reader["TaskName"].ToString();
        //                temp.SourceFile = reader["SourceFile"].ToString();
        //                temp.DestinationFile = reader["DestinationFile"].ToString();
        //                temp.FileSize = int.Parse(reader["FileSize"].ToString());
        //                temp.TransferTime = int.Parse(reader["TransferTime"].ToString());
        //                result.Add(temp);
        //            }                    
        //            return result;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //public static void RequestWithOutSelect(string querystring)
        //{
        //    SqlConnection con;
        //    try
        //    {
        //        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Model\DataBase.mdf;Integrated Security=True");
        //        con.Open();
        //        SqlCommand command = new SqlCommand(querystring, con);
        //        con.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
}
