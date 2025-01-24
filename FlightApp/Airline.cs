using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp
{
    class Airline
    {
        public string Name {  get; set; }

        public string Code { get; set; }

        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Airline() { }
        

        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Add(flight.FlightNumber, flight);
                return true; //returns true upon successful addition of the flight
            }
            return false; //return false if unsucccessful addition  of the flight
        }

        public double CalculateFees()
        {
            double TotalFee = 0;
            double Discount = 0;
            int count = Flights.Count;
            foreach (Flight flight in Flights.Values)
            {
                if (flight.ExpectedTime.TimeOfDay < TimeSpan.FromHours(11) || flight.ExpectedTime.TimeOfDay > TimeSpan.FromHours(21))
                {
                    Discount += 110;
                }


                if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)") //cheks if flight is from Dubai , Bangkok or Tokyo
                {
                    Discount += 25;
                }

                if (!(flight is CFFTFlight) && !(flight is DDJBFlight) && !(flight is LWTTFlight)) //checks for special requests
                {
                    Discount += 50;
                }

                TotalFee += flight.CalculateFees(); //adds the price of the flight

            }
            try
            {
                Discount += (count / 3) * 350; //For every 3 flights arriving/departing, airlines will receive a discount
                if (count > 5)
                {
                    TotalFee = (TotalFee * 0.97); 
                }
                TotalFee = TotalFee - Discount;
                return TotalFee;
            }

            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true; //returns true upon successful removal of the flight
            }
            return false;//return false if unsucccessful removal of the flight

        }

        public override string ToString()
        {
            return $"Airline Name: {Name.PadRight(10)} Code: {Code.PadRight(5)} Flights: {Flights.Values}";
        }

    }
}
