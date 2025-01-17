using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class Airline
    {
        public string Name {  get; set; }

        public string Code { get; set; }

        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Airline()
        {
            
        }

        public Airline(string name, string code, Dictionary<string, Flight> flights)
        {
            Name = name;
            Code = code;
            Flights = flights;
        }

        public bool AddFlight(Flight flight)
        {
            return true;
        }

        public double CalculateFees()
        {
            return 0;
        }

        public bool RemoveFlight(Flight flight)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Airline Name: {Name.PadRight(10)} Code: {Code.PadRight(5)} Flights: {Flights.Values}";
        }


    }
}
