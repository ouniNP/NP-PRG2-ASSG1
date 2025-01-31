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
    class Terminal
    {
        public string TerminalName { get; set; }

        public Dictionary<string, Airline> Airlines { get; set; } 

        public Dictionary<string, Flight> Flights { get; set; } 

        public Dictionary<string, BoardingGate> BoardingGates { get; set; } 

        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal() { }

        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }

        public bool AddAirline(Airline airline)
        {
            if (!Airlines.ContainsKey(airline.Code))
            {
                Airlines.Add(airline.Name, airline);
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (!BoardingGates.ContainsKey(boardingGate.GateName))
            {
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }

            return false;
        }      

        public Airline GetAirlineFromFlight(Flight flight)
        {
            if (flight == null)
            {
                throw new ArgumentException("Flight cannot be null.");
            }
            foreach (Airline airline in Airlines.Values)
            {
                if (airline.Flights.ContainsKey(flight.FlightNumber))
                {
                    return airline;
                }
            }
            throw new Exception("Flight not found.");
        }

        public void PrintAirlineFees()
        {
            foreach (Airline airline in Airlines.Values)
            {
                Console.WriteLine($"{airline.Name}: ${airline.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            return $"{TerminalName}";
        }
    }
}
