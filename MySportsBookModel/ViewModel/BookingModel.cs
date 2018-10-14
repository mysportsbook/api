using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class BookingModel : VenueModel
    {
        public int BookingId { get; set; }
        public string BookingNo { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Venue { get; set; }
        public string Court { get; set; }
        public string Date { get; set; }
        public string Slot { get; set; }
        public string Amount { get; set; }
        public string Mode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int BookingDetailId { get; set; }
    }
}
