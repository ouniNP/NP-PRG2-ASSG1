using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    abstract class Flight : Airline
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }

        public string Status { get; set; }

        public Flight()
        {
        }
        //note to self: implement data validation for expected time (dd/MM/yyyy HH:mm)
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
        }
        public virtual double CalculateFees()
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            return $"";
        }
    }
}
