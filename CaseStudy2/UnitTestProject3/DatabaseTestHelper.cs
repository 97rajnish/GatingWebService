using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CodeMetricsDBRepositoryContractsLib;

namespace UnitTestProject3
{
    class DatabaseTestHelper : ICodeMetricsDbRepository
    {
        public int GetSimianDuplicates(string gitRepo)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS01;Initial Catalog=FakeDatabase;Integrated Security=True";
            int a;
            SqlDataReader reader1;
            string str = "SELECT simian FROM FakeTable WHERE id = @id";
            var con = new SqlConnection(strCon);
            var cmd5 = new SqlCommand(str, con);
            cmd5.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
            reader1 = cmd5.ExecuteReader();
            if (reader1.Read())
            {
                if (reader1[0] == DBNull.Value)
                    a = -1;
                else
                    a = Convert.ToInt32(reader1[0]);
            }
            else
                a = -2;
            con.Close();
            return a;
        }

        public int GetTicsErrors(string gitRepo)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS01;Initial Catalog=FakeDatabase;Integrated Security=True";
            int a;
            SqlDataReader reader2;
            string str = "SELECT tics FROM FakeTable WHERE id = @id";
            var con = new SqlConnection(strCon);
            var cmd4 = new SqlCommand(str, con);
            cmd4.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
            reader2 = cmd4.ExecuteReader();
            if (reader2.Read())
            {
                if (reader2[0] == DBNull.Value)
                    a = -1;
                else
                    a = Convert.ToInt32(reader2[0]);
            }
            else
                a = -2;
            con.Close();
            return a;
        }

        public void PersistToDatabase(string gitRepo)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS01;Initial Catalog=FakeDatabase;Integrated Security=True";
            var con = new SqlConnection(strCon);
           
            con.Open();
           
                string command = "Insert into FakeTable Values(@id, @simian, @tics)";
                var cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@id", gitRepo);
                cmd.Parameters.AddWithValue("@simian", 50);
                cmd.Parameters.AddWithValue("@tics", 55);
                cmd.ExecuteNonQuery();
            

            con.Close();
        }

        public void UpdateSimianDuplicates(string gitRepo, int simianDuplicates)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS01;Initial Catalog=FakeDatabase;Integrated Security=True";
            var con = new SqlConnection(strCon);
            string command = "UPDATE FakeTable SET simian = @no Where id = @id";
            var cmd1 = new SqlCommand(command, con);
            cmd1.Parameters.AddWithValue("@no", simianDuplicates);
            cmd1.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateTicsErrors(string gitRepo, int ticsErrors)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS01;Initial Catalog=FakeDatabase;Integrated Security=True";
            var con = new SqlConnection(strCon);
            string command = "UPDATE FakeTable SET tics = @no Where id = @id";
            var cmd2 = new SqlCommand(command, con);
            cmd2.Parameters.AddWithValue("@no", ticsErrors);
            cmd2.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
        }
    }
}
