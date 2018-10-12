using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class BatchModel : CourtModel
    {
        public int BatchId { get; set; }
        public string BatchCode { get; set; }
        public string BatchName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayer { get; set; }
        public bool IsAttendanceRequired { get; set; }
    }
}
