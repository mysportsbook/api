using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class BatchCountModel
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public int BatchCountId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CourtId { get; set; }
        public string CourtName { get; set; }
        public int Count { get; set; }
    }
}
