using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class Airline
    {
        public string Name { get; set; }
        
        public string Code { get; set; }

        public Dictionary<string, Flight> Flights { get; set; }

        public bool AddFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public double CalculateFees()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

    }
}
