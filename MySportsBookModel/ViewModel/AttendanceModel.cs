using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class AttendanceModel : PlayerModel
    {
        public int? AttendanceId { get; set; }
        public DateTime? Date { get; set; }
        public bool? Present { get; set; }
    }
}
