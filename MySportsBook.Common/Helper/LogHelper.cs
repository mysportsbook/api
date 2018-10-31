using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MySportsBook.Common.Helper
{
    public abstract class LogBase
    {
        public abstract void Log(string message);
    }

    public class FileLogger : LogBase
    {
        public string filePath = HostingEnvironment.MapPath($"~/Log/{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
        public override void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message);
                streamWriter.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                streamWriter.Close();
            }
        }
    }
}
