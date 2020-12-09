using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess
{
     public class DBConnection
    {
        public static SqlConnection connection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
            return con;
        }

        public static DataTable GetDataTable(string query)
        {
            SqlConnection con = connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            Adapter.Fill(dt);
            con.Close();

            return dt;
           
        }

        private string GenerateID()
        {

            int lastAddedId = 8;// get this value from database
            string demo = Convert.ToString(lastAddedId + 1).PadLeft(4, '0');
            return demo;
            // it will return 0009

        }




        public static int CRUDRecords(string query)
        {
            try
            {
                int i = 1;

                SqlConnection con = DBConnection.connection();

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetQueryData(string query)
        {
            SqlConnection con = connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            Adapter.Fill(dt);
            con.Close();

            return dt;
        }
        public void DeleteData(string id, string query)
        {
            try
            {
                SqlConnection con = DBConnection.connection();

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteRecords(string query)
        {
            try
            {
                SqlConnection con = DBConnection.connection();

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable GetData(string proc)
        {
            SqlConnection con = DBConnection.connection();
            DataTable allData = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch
            {
                con.Close();
            }
            return allData;
        }
    }
}
