﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }

        public string Status { get; set; }

        public Flight()
        {
            FlightNumber = "";
            Origin = "";
            Destination = "";
            ExpectedTime = DateTime.Now;
        }
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
        }
        public abstract double CalculateFees();

        public override string ToString()
        {
            return $"";
        }
    }
}
