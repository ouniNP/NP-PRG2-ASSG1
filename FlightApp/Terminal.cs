using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class Terminal
    {
        public string TerminalName { get; set; }

        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();

        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
    }
}
