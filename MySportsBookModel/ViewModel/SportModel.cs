using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class SportModel : VenueModel
    {
        public int SportId { get; set; }
        public string SportCode { get; set; }
        public string SportName { get; set; }
    }
}
