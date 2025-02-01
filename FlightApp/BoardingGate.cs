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
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate() { }

        public BoardingGate(string gatename, bool supportsDDJB, bool supportsCFFT, bool supportsLWTT)
        {
            GateName = gatename;
            SupportsDDJB = supportsDDJB;
            SupportsCFFT = supportsCFFT;
            SupportsLWTT = supportsLWTT;
        }

        public double CalculateFees()
        {
            return Flight.CalculateFees(); //not sure about this line 
        }
        public bool SupportsFlight(Flight flight)
        {
            if (flight is DDJBFlight && this.SupportsDDJB)
            {
                return true;
            }
            else if (flight is CFFTFlight && this.SupportsCFFT)
            {
                return true;
            }
            else if (flight is LWTTFlight && this.SupportsLWTT)
            {
                return true;
            }
            else if (flight is NORMFlight)
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{this.GateName.PadRight(16)}{this.SupportsCFFT.ToString().PadRight(23)}{this.SupportsDDJB.ToString().PadRight(23)}{this.SupportsLWTT.ToString()}";
        }
    }
}
