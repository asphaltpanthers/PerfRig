using Newtonsoft.Json;
using PerfRig.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PerfRig.Controllers
{
    public class DashboardController : ApiController
    {
        [ActionName("GetTranslationTimes")]
        public IEnumerable<TimeByDate> GetTranslationTimes(string id)
        {
            TranslationStatus[] statuses = GetStatuses(new Uri(ConfigurationManager.AppSettings["DashboardHost"].ToString() + "api/projectionstatus/" + id));
            return statuses.Select(s => {
                if (s.End != null)
                {
                    return new TimeByDate
                    {
                        Id = Guid.NewGuid(),
                        TimeStamp = DateTime.Parse(s.Start.ToString()).ToString(),
                        ElapsedTime = s.End.Value.Subtract(s.Start).TotalSeconds
                    };
                }

                return null;
            }).Where(s => s != null);
        }

        private TranslationStatus[] GetStatuses(Uri url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            Stream stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<TranslationStatus[]>(json);
        }
    }

    public class TranslationStatus{
        public DateTime? End;
        public String FileName;
        public String Id;
        public String Name;
        public String ProjectionStatusType;
        public Int32 RecordsProcessed;
        public DateTime Start;
        public String WorkflowId;
    }
}
