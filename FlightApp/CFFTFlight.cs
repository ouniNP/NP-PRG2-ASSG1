using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }
        public override double CalculateFees()
        {
            throw new NotImplementedException();
        }

        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber,origin,destination,expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
            RequestFee = requestFee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
