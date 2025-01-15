using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; };

        public override double CalculateFees()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
