//==========================================================
// Student Number : S10265849
// Student Name	  : Teo Hong Yi
// Partner Name	  : Wang Yi Nuo
//==========================================================


//Functions
using FlightApp;

void MainMenu()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
}


//Feature 1 : yinuo
void LoadAirlinesAndBoardingGates(Dictionary<string, Airline> AirlinesDict, Dictionary<string, BoardingGate> BoardingGateDict) 
{
    string AIRLINESCSVPATH = "..\\..\\..\\..\\data\\airlines.csv";
    string BOARDINGGATESCSVPATH = "..\\..\\..\\..\\data\\boardingGates.csv";

    using (StreamReader airlinesReader = new StreamReader(AIRLINESCSVPATH))
    {
        string? line;
        airlinesReader.ReadLine(); // skip the header
        Console.WriteLine("Loading Airlines...");
        while ((line = airlinesReader.ReadLine()) != null)
        {
            string[] data = line.Split(",");
            Airline airline = new Airline(data[0], data[1]);
            AirlinesDict.Add(airline.Code, airline);
        }
        Console.WriteLine($"{AirlinesDict.Count} Airlines Loaded!");
    }

    using (StreamReader boardingGatesReader = new StreamReader(BOARDINGGATESCSVPATH))
    {
        string? line;
        boardingGatesReader.ReadLine(); //skip the header
        Console.WriteLine("Loading Boarding Gates...");
        while ((line = boardingGatesReader.ReadLine()) != null)
        {
            string[] data = line.Split(",");
            BoardingGate boardingGate = new BoardingGate(data[0], bool.Parse(data[1]), bool.Parse(data[2]), bool.Parse(data[3]));
            BoardingGateDict.Add(boardingGate.GateName, boardingGate);
        }
        Console.WriteLine($"{BoardingGateDict.Count} Boarding Gates Loaded!");
    }

}
//Feature 2 : hongyi
void LoadFlights(Dictionary<string, Flight> flightsdict)
{
    using (StreamReader flightsreader = new StreamReader("..\\..\\..\\..\\data\\flights.csv"))
    {
        string? line;
        Flight flight;
        flightsreader.ReadLine(); //reads the header
        Console.WriteLine("Loading Flights...");
        while ((line = flightsreader.ReadLine()) != null)
        {
            string[] strings = line.Split(",");
            string flightnumber = strings[0];
            string origin = strings[1];
            string destination = strings[2];
            DateTime expectedtime = Convert.ToDateTime(strings[3]);
            string? specialrequestcode = strings[4];
            if (specialrequestcode == "LWTT")
            {
                 flight = new LWTTFlight(flightnumber,origin,destination,expectedtime);
            }
            else if (specialrequestcode == "DDJB")
            {
                 flight = new DDJBFlight(flightnumber, origin, destination, expectedtime);
            }
            else if (specialrequestcode == "CFFT")
            {
                 flight = new CFFTFlight(flightnumber, origin, destination, expectedtime);
            }
            else
            {
                 flight = new NORMFlight(flightnumber, origin, destination, expectedtime);
            }
            flightsdict.Add(flightnumber, flight);
        }
        Console.WriteLine($"{flightsdict.Count} Flights Loaded!");
    }
}
//Feature 3.1 : hongyi
void AssignFlightToAIrline(Dictionary<string, Flight> FlightsDict , Dictionary<string, Airline> AirlinesDict)
{
    foreach (Flight flight in FlightsDict.Values)
    {
        string AirlineCode = flight.FlightNumber.Substring(0,2);
        AirlinesDict[AirlineCode].AddFlight(flight);
    }
}
//Feature 3.2: hongyi
void DisplayFlights(Dictionary<string, Flight> flightsdict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
    foreach (Flight flight in flightsdict.Values)
    {
        Console.WriteLine(flight);
    }
}
//Feature 4 : yinuo
void DisplayBoardingGates(Dictionary<string, BoardingGate> BoardingGateDict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Gate Name       DDJB                   CFFT                   LWTT");
    foreach (BoardingGate boardingGate in BoardingGateDict.Values)
    {
        Console.WriteLine(boardingGate);
    }
}
//Feature 5 : hongyi
void AssignBoardingGateToFlight()
{

}
//Feature 6 : hongyi
void CreateFlight()
{

}
//Feature 7 : yinuo (Option 5)
void DisplayFullFlightDetails(Dictionary<string, Airline> AirlinesDict)
{
    string selectedAirlineCode;
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Airline Code    Airline Name");
    foreach (KeyValuePair<string, Airline> airline in AirlinesDict)
    {
        Console.WriteLine($"{airline.Key.ToString().PadRight(16)}{airline.Value.Name.ToString()}");
    }
    while (true)
    {
        try
        {
            Console.Write("Enter Airline Code: ");
            selectedAirlineCode = Console.ReadLine().ToUpper();
            break;
        }
        catch (FormatException fe)
        {
            Console.WriteLine(fe.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    foreach (Airline airline in AirlinesDict.Values)
    {
        if (selectedAirlineCode == airline.Code)
        {
            Airline airlineQuery = airline;
            Console.WriteLine("=============================================");
            Console.WriteLine($"Airline Name: {airlineQuery.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
            foreach (KeyValuePair <string, Flight> kvp in airlineQuery.Flights)
            {
                Console.WriteLine($"{kvp.Value}");
            }
            break;
        }
        else
        {
            Console.WriteLine("Invalid Airline Code, try again.");
            break;
        }
    }
}
//Feature 8 : yinuo
void ModifyFlightDetails()
{

}
//Feature 9 : hongyi
void DisplayScheduledFlights()
{

}

//Feature 10 (personal creation)
void WhiteSpace()
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
}

//Collections
Dictionary<string, Airline> AirlinesDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> BoardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> FlightsDict = new Dictionary<string, Flight>();



//Main Program
LoadAirlinesAndBoardingGates(AirlinesDict, BoardingGateDict);
LoadFlights(FlightsDict);
AssignFlightToAIrline(FlightsDict, AirlinesDict);

WhiteSpace();


//Main Loop
while (true)
{
    MainMenu();
    int option;
    while (true)
    {
        try
        {
            Console.WriteLine("Please select your option:");
            option = Convert.ToInt32(Console.ReadLine());
            break;

        }
        catch (FormatException fe)
        {
            Console.WriteLine(fe.Message);
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
    }
    if (option == 1)
    {
        DisplayFlights(FlightsDict);
        WhiteSpace();
    }
    else if (option == 2)
    {
        DisplayBoardingGates(BoardingGateDict);
        WhiteSpace();
    }
    else if (option == 3)
    {
        throw new NotImplementedException();
    }
    else if (option == 4)
    {
        throw new NotImplementedException();
    }
    else if (option == 5)
    {
        DisplayFullFlightDetails(AirlinesDict); //Partial implementation, requires fix
    }
    else if (option == 6)
    {
        throw new NotImplementedException();
    }
    else if (option == 7)
    {
        throw new NotImplementedException();
    }
    else
    {
        Console.WriteLine("Invalid Option, try again.");
        Console.WriteLine();
    }
}


