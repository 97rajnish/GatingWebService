using CodeMetricsDBRepositoryContractsLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMetricsSQLDBRepositoryLib
{
    public class CodeMetricsSqldbRepository : ICodeMetricsDbRepository
    {
        public int GetSimianDuplicates(string gitRepo)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=StaticDB;Integrated Security=True";
            int a;
            SqlDataReader reader4;
            string str = "SELECT noOfDuplicates FROM CodeMetrics WHERE gitRepoId = @id";
            var con = new SqlConnection(strCon);
            var cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
                reader4 = cmd.ExecuteReader();
                if (reader4.Read())
                  {
                    if (reader4[0] == DBNull.Value)
                        a = -1;
                    else
                        a = Convert.ToInt32(reader4[0]);
                  }
              else
                 a = -2;
            con.Close();
            return a;
        } 

        public void PersistToDatabase(string gitRepo)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=StaticDB;Integrated Security=True";
            var con = new SqlConnection(strCon);
            string command1 = "Select Count(*) From CodeMetrics Where gitRepoId = @gitRepoId";
            var cmd1 = new SqlCommand(command1, con);            
            cmd1.Parameters.AddWithValue("@gitRepoId", gitRepo);
            con.Open();
            int count = (int)cmd1.ExecuteScalar();
            if (count == 0)
            {
                string command = "Insert into CodeMetrics Values(@gitRepoId, @noOfDuplicates, @noOfErrors)";
                var cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@gitRepoId", gitRepo);
                cmd.Parameters.AddWithValue("@noOfDuplicates", DBNull.Value);
                cmd.Parameters.AddWithValue("@noOfErrors", DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            con.Close();
        }

        public void UpdateSimianDuplicates(string gitRepo,int simianDuplicates)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=StaticDB;Integrated Security=True";
            var con = new SqlConnection(strCon);
            string command = "UPDATE CodeMetrics SET noOfDuplicates = @no Where gitRepoId = @id";           
            var cmd7 = new SqlCommand(command, con);
            cmd7.Parameters.AddWithValue("@no", simianDuplicates);
            cmd7.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
            cmd7.ExecuteNonQuery();
            con.Close();
        }
        public int GetTicsErrors(string gitRepo)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=StaticDB;Integrated Security=True";
            int a;
            SqlDataReader reader;
            string str = "SELECT noOfErrors FROM CodeMetrics WHERE gitRepoId = @id";
            var con = new SqlConnection(strCon);
            var cmd8 = new SqlCommand(str, con);
            cmd8.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
            reader = cmd8.ExecuteReader();
            if (reader.Read())
            {
                if (reader[0] == DBNull.Value)
                    a = -1;
                else
                    a = Convert.ToInt32(reader[0]);
            }
            else
                a = -2;
            con.Close();
            return a;
        }
        public void UpdateTicsErrors(string gitRepo, int ticsErrors)
        {
            const string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=StaticDB;Integrated Security=True";
            var con = new SqlConnection(strCon);
            string command = "UPDATE CodeMetrics SET noOfErrors = @no Where gitRepoId = @id";
            var cmd9 = new SqlCommand(command, con);
            cmd9.Parameters.AddWithValue("@no", ticsErrors);
            cmd9.Parameters.AddWithValue("@id", gitRepo);
            con.Open();
            cmd9.ExecuteNonQuery();
            con.Close();
        }

    }
}
