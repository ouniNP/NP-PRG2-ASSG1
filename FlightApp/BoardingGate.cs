using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate() { }

        public BoardingGate(string gatename, bool supportsDDJB, bool supportsCFFT, bool supportsLWTT, Flight flight)
        {
            GateName = gatename;
            SupportsDDJB = supportsDDJB;
            SupportsCFFT = supportsCFFT;
            SupportsLWTT = supportsLWTT;
            Flight = flight;
        }

        public double CalculateFees()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"";
        }
    }
}
