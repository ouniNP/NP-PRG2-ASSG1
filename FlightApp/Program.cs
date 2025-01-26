

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
void LoadAirlinesAndBoardingGates(Dictionary<string, Airline> loadedAirlines, Dictionary<string, BoardingGate> loadedBoardingGates) 
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
            loadedAirlines.Add(airline.Code, airline);
        }
        Console.WriteLine($"{loadedAirlines.Count} Airlines Loaded!");
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
            loadedBoardingGates.Add(boardingGate.GateName, boardingGate);
        }
        Console.WriteLine($"{loadedBoardingGates.Count} Boarding Gates Loaded!");
    }

}
//Feature 2 : hongyi
void LoadFlights()
{
    Dictionary<string,Flight> flightsdict = new Dictionary<string, Flight>();
    using (StreamReader flightsreader = new StreamReader("..\\..\\..\\..\\data\\flights.csv"))
    {
        string? line;
        Flight flight;
        flightsreader.ReadLine(); //reads the header
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
        //test code
        /*foreach (Flight f in flightsdict.Values)
        {
            Console.WriteLine(f);
        }*/
    }
}
//Feature 3 : hongyi
void DisplayFlights()
{

}
//Feature 4 : yinuo
void DisplayBoardingGates(Dictionary<string, BoardingGate> loadedBoardingGates)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Gate Name       DDJB                   CFFT                   LWTT");
    foreach (BoardingGate boardingGate in loadedBoardingGates.Values)
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
//Feature 7 : yinuo
void DisplayFullFlightDetails()
{

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
Dictionary<string, Airline> loadedAirlines = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> loadedBoardingGates = new Dictionary<string, BoardingGate>();




//Main Program
LoadAirlinesAndBoardingGates(loadedAirlines, loadedBoardingGates);
LoadFlights();
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
        DisplayFlights();
        WhiteSpace();
    }
    else if (option == 2)
    {
        DisplayBoardingGates(loadedBoardingGates);
        WhiteSpace();
    }
    else
    {
        Console.WriteLine("Invalid Option, try again.");
        Console.WriteLine();
    }
}


