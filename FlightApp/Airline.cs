//==========================================================
// Student Number : S10265849
// Student Name	  : Teo Hong Yi
// Partner Name	  : Wang Yi Nuo
//==========================================================


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

        public Airline() { }
        

        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Add(flight.FlightNumber, flight);
                return true; //returns true upon successful addition of the flight
            }
            return false; //return false if unsucccessful addition  of the flight
        }

        public double CalculateFees()
        {
            double totalfee = 0;
            foreach (var flight in Flights.Values)
            {
                totalfee += flight.CalculateFees();
            }
            return totalfee;
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
