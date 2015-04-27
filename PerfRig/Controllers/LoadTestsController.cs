using PerfRig.Models;
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
        public IEnumerable<String> GetLoadTests()
        {
            return GetTestsFromStorage("SELECT DISTINCT LoadTestName FROM [LoadTest2010].[dbo].[LoadTestRun]");
        }

        public IEnumerable<String> GetWebTests(string id)
        {
            return GetTestsFromStorage("SELECT DISTINCT TestCaseName FROM [LoadTest2010].[dbo].[LoadTestCase]" +
                "WHERE LoadTestRunId in (SELECT LoadTestRunId FROM [LoadTest2010].[dbo].[LoadTestRun]" +
                    "WHERE LoadTestName = '" + id + "'" +
                ")");
        }

        public IEnumerable<TimeByDate> GetWebTestTimes(string id, string webId)
        {
            return GetTimesFromStorage("SELECT TimeStamp, ElapsedTime FROM [LoadTest2010].[dbo].[LoadTestTestDetail]" +
                "WHERE EXISTS (SELECT * FROM [LoadTest2010].[dbo].[LoadTestRun] JOIN [LoadTest2010].[dbo].[LoadTestCase] ON" +
                    "([LoadTest2010].[dbo].[LoadTestRun].LoadTestRunId = [LoadTest2010].[dbo].[LoadTestCase].LoadTestRunId)" +
                    "WHERE" +
                        "[LoadTest2010].[dbo].[LoadTestTestDetail].LoadTestRunId = [LoadTest2010].[dbo].[LoadTestRun].LoadTestRunId AND" +
                        "[LoadTest2010].[dbo].[LoadTestTestDetail].TestCaseId = [LoadTest2010].[dbo].[LoadTestCase].TestCaseId AND" +
                        "[LoadTest2010].[dbo].[LoadTestRun].LoadTestName = '" + id + "' AND" +
                        "[LoadTest2010].[dbo].[LoadTestCase].TestCaseName = '" + webId + "'" +
                ")");
        }

        private IEnumerable<String> GetTestsFromStorage(String queryString)
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

        private IEnumerable<TimeByDate> GetTimesFromStorage(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ToString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<TimeByDate> times = new List<TimeByDate>();
                try
                {
                    while (reader.Read())
                    {
                        times.Add(new TimeByDate
                        {
                            Id = Guid.NewGuid(),
                            TimeStamp = DateTime.Parse(reader[0].ToString()).ToString("yyyy-MM-dd"),
                            ElapsedTime = Double.Parse(reader[1].ToString())
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }

                return times;
            }
        }
    }
}
