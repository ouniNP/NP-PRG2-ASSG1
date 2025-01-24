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
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Add(flight.FlightNumber, flight))
                return true; //returns true upon successful addition of the flight
            }
            return false; //return false if unsucccessful addition  of the flight
        }

        public double CalculateFees()
        {
            return 0;
        }

        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true; //returns true upon successful removal of the flight
            }
            return false;//return false if unsucccessful removal of the flight

        }

        public override string ToString()
        {
            return $"Airline Name: {Name.PadRight(10)} Code: {Code.PadRight(5)} Flights: {Flights.Values}";
        }


    }
}
