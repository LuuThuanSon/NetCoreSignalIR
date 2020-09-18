using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalIR.Common
{
    public static class Strings
    {
        public static string HubUrl => "https://localhost:5001/hubs/clock";

        public static class Events
        {
            public static string TimeSent => nameof(IClock.ShowTime);
        }
    }
}
