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

        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();

        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public bool AddAirline(Airline airline)
        {
            throw new NotImplementedException();
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            throw new NotImplementedException();
        }

        public bool GetAirlineFromFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public void PrintAirlineFees()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
