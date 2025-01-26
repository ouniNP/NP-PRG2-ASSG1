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
using System.Text.RegularExpressions;

namespace FlightApp
{
    class NORMFlight : Flight
    {
        
        public override double CalculateFees()
        {
            const double BOARDING_GATE_BASE_FEE = 300;
            double totalFees = 0;

            if (this.Destination.ToUpper().Contains("SINGAPORE"))
            {
                totalFees = BOARDING_GATE_BASE_FEE + 500;
                return totalFees;
            }
            else if (this.Origin.ToUpper().Contains("SINGAPORE"))
            {
                totalFees = BOARDING_GATE_BASE_FEE + 800;
                return totalFees;
            }
            else
            {
                throw new Exception("Invalid operation: Your flight does NOT arrive to SIN/depart from SIN.");
            }
        }

        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber,origin,destination,expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
        }

        public override string ToString()
        {
            return base.ToString() + $"Flight type: {this.GetType().Name}";
        }
    }
}
