//==========================================================
// Student Number : S10265849
// Student Name	  : Teo Hong Yi
// Partner Name	  : Wang Yi Nuo
//==========================================================


//Functions
using FlightApp;
using System.Globalization;

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
void DisplayFlights(Dictionary<string, Flight> flightsdict, Dictionary<string, Airline> AirlinesDict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
    foreach (Flight flight in flightsdict.Values)
    {
        string AirlineCode = flight.FlightNumber.Substring(0, 2);
        string AirlineName = AirlinesDict[AirlineCode].Name;
        Console.WriteLine($"{flight.FlightNumber,-16}{AirlineName,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime}");
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
//Feature 5 : hongyi (option 3)
void AssignBoardingGateToFlight(Dictionary<string, Flight> FlightsDict, Dictionary<string, BoardingGate> BoardingGateDict)
{
    Flight SelectedFlight = null;
    BoardingGate SelectedBoardingGate = null;
    string SpecialRequestCode = "None";

    Console.WriteLine("Enter Flight Number:");
    string flightnumber = Console.ReadLine();
    Console.WriteLine("Enter Boarding Gate Name:");
    string gatename = Console.ReadLine();
    foreach (Flight flight in FlightsDict.Values)
    {
        if (flight.FlightNumber == flightnumber)
        {
            SelectedFlight = flight;
            break;
        }
    }

    foreach (BoardingGate boardingGate in BoardingGateDict.Values)
    {
        if (boardingGate.GateName == gatename)
        {
            SelectedBoardingGate = boardingGate;
            break;
        }
    }
    //Display flight
    Console.WriteLine($"Flight Number: {SelectedFlight.FlightNumber}");
    Console.WriteLine($"Origin: {SelectedFlight.Origin}");
    Console.WriteLine($"Destination: {SelectedFlight.Destination}");
    Console.WriteLine($"Expected Time: {SelectedFlight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")}");
    if (SelectedFlight is DDJBFlight)
    {
        SpecialRequestCode = "DDJB";
    }
    else if (SelectedFlight is CFFTFlight)
    {
        SpecialRequestCode = "CFFT";
    }
    else if (SelectedFlight is LWTTFlight)
    {
        SpecialRequestCode = "LWTT";
    }
    Console.WriteLine($"Special Request Code: {SpecialRequestCode} ");

    //Display boarding gate
    Console.WriteLine($"Boarding Gate Name: {SelectedBoardingGate.GateName}");
    Console.WriteLine($"Supports DDJB: {SelectedBoardingGate.SupportsDDJB}");
    Console.WriteLine($"Supports CFFT: {SelectedBoardingGate.SupportsCFFT}");
    Console.WriteLine($"Supports LWTT: {SelectedBoardingGate.SupportsLWTT}");

    //Update Status of flight (validation for edit status and newstatus)
    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
    string EditStatus = Console.ReadLine().ToUpper();
    while (true)
    {
        if (EditStatus == "Y")
        {
            Console.WriteLine("1. Delayed");
            Console.WriteLine("2. Boarding");
            Console.WriteLine("3. On Time");
            Console.WriteLine("Please select the new status of the flight:");
            string NewStatus;
            while (true)
            {
                NewStatus = Console.ReadLine();

                if (NewStatus == "1")
                {
                    SelectedFlight.Status = "Delayed";
                    break;
                }
                else if (NewStatus == "2")
                {
                    SelectedFlight.Status = "Boarding";
                    break;
                }
                else if (NewStatus == "3")
                {
                    SelectedFlight.Status = "On Time";
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option");
                    Console.WriteLine("1. Delayed");
                    Console.WriteLine("2. Boarding");
                    Console.WriteLine("3. On Time");
                    Console.WriteLine("Please select the new status of the flight:");
                }
            }
            break;
        }
        else if (EditStatus == "N")
        {
            break;
        }
        else
        {
            Console.WriteLine("Please enter a valid option");
            Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
            EditStatus = Console.ReadLine().ToUpper();
        }
    }
    SelectedBoardingGate.Flight = SelectedFlight;
    Console.WriteLine($"Flight {SelectedFlight.FlightNumber} has been assigned to Boarding Gate {SelectedBoardingGate.GateName}!");
}
//Feature 6 : hongyi (option 4)
void CreateFlight(Dictionary<string, Flight> FlightsDict)
{
    Flight newflight = null;
    string addanother;
    do
    {
        Console.Write("Enter Flight Number: ");
        string flightnumber = Console.ReadLine();
        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();
        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();
        DateTime expectedtime = DateTime.MinValue;
        bool validDate = false;
        while (!validDate)
        {
            Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            string input = Console.ReadLine();

            try
            {

                expectedtime = DateTime.ParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                validDate = true; 
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format. Please use dd/mm/yyyy hh:mm.");
            }
        }
        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string specialrequestcode = Console.ReadLine();
        if (specialrequestcode == "LWTT")
        {
            newflight = new LWTTFlight(flightnumber, origin, destination, expectedtime);
        }
        else if (specialrequestcode == "DDJB")
        {
            newflight = new DDJBFlight(flightnumber, origin, destination, expectedtime);
        }
        else if (specialrequestcode == "CFFT")
        {
            newflight = new CFFTFlight(flightnumber, origin, destination, expectedtime);
        }
        else if (specialrequestcode == "None")
        {
            newflight = new NORMFlight(flightnumber, origin, destination, expectedtime);
        }
        FlightsDict.Add(flightnumber, newflight);
        Console.WriteLine($"Flight {flightnumber} has been added!");
        Console.WriteLine("Would you like to add another flight? (Y/N)");
        addanother = Console.ReadLine().ToUpper();
    }
    while (addanother == "Y");
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
        DisplayFlights(FlightsDict, AirlinesDict);
        WhiteSpace();
    }
    else if (option == 2)
    {
        DisplayBoardingGates(BoardingGateDict);
        WhiteSpace();
    }
    else if (option == 3)
    {
        AssignBoardingGateToFlight(FlightsDict, BoardingGateDict);
        WhiteSpace() ;
    }
    else if (option == 4)
    {
        CreateFlight(FlightsDict);
        WhiteSpace();
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
    else if (option == 0)
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else
    {
        Console.WriteLine("Invalid Option, try again.");
        Console.WriteLine();
    }
}


