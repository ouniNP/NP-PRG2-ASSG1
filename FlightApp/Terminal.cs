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

        public Terminal() { }

        public Terminal(string terminalName)
        {
            terminalName = terminalName;
        }

        public bool AddAirline(Airline airline)
        {
            if (!Airlines.ContainsKey(airline))
            {
                Airlines.Add(airline.Name, airline);
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (!BoardingGates.ContainsKey(boardingGate))
            {
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }

            return false;
        }      

        public Airline GetAirlineFromFlight(Flight flight)
        {
            return airline.Name.Substring(0, 2);
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
