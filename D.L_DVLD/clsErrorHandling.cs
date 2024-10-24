using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.L_DVLD_API
{
    public class clsErrorHandling
    {
        static string sourceName = "SimpleClinicApp";
        static public void HandleError(string Error)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }
            EventLog.WriteEntry(sourceName, Error, EventLogEntryType.Error);

        }
    }
}
