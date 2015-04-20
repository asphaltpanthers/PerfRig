using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PerfRig.Controllers
{
    public class LoadTestsController : ApiController
    {
        public IEnumerable<String> GetAllTests()
        {
            return QueryLoadTests("SELECT DISTINCT TestCaseName FROM [LoadTest2010].[dbo].[LoadTestCase]");
        }

        private IEnumerable<String> QueryLoadTests(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<String> loadTests = new List<String>();
                try
                {
                    while (reader.Read())
                    {
                        loadTests.Add(reader[0].ToString());
                    }
                }
                finally
                {
                    reader.Close();
                }

                return loadTests;
            }
        }
    }
}
