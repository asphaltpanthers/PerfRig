using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfRig.Models
{
    public class TimeByDate
    {
        public Guid Id { get; set; }
        public String TimeStamp { get; set; }
        public Double ElapsedTime { get; set; }
    }
}