using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; } = 500;

        public LWTTFlight() { }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            RequestFee = this.RequestFee;
        }
        public override double CalculateFees()
        {
            const double BOARDING_GATE_BASE_FEE = 300;
            double totalFees = 0;
            if (this.Destination.ToUpper().Contains("SIN"))
            {
                totalFees = BOARDING_GATE_BASE_FEE + 500 + RequestFee;
                return totalFees;
            }
            else if (this.Origin.ToUpper().Contains("SIN"))
            {
                totalFees = BOARDING_GATE_BASE_FEE + 800 + RequestFee;
                return totalFees;
            }
            else
            {
                throw new Exception("Invalid operation: Your flight does NOT arrive to SIN/depart from SIN.");
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
