//==========================================================
// Student Number : S10265849
// Student Name	  : Teo Hong Yi
// Partner Name	  : Wang Yi Nuo
//==========================================================


using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class Flight : Airline, IComparable<Flight>
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
            Status = "Scheduled";
        }
        public virtual double CalculateFees()
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


        public override string ToString()
        {
            return $"{FlightNumber}     {Origin}            {Destination}        {ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")}";
        }

        public int CompareTo(Flight? other)
        {
            if (other == null) return 1;
            return ExpectedTime.CompareTo(other.ExpectedTime);
        }
    }
}
