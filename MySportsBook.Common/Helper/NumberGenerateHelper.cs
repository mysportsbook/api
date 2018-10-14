using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBook.Common.Helper
{
    public class NumberGenerateHelper
    {
        public static string GenerateInvoiceNo()
        {
            Random generator = new Random();
            return generator.Next(0, 999999).ToString("D6");
        }
    }
}
