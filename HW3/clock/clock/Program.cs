using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            clock clock = new clock();
            DateTime atime = new DateTime();
            atime = DateTime.Now.AddSeconds(2);
            clock.SetAlarmTime(atime);
            clock.Start();
        }
    }
}
