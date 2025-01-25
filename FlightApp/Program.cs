

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
}


//Feature 1 : yinuo
void LoadAirlinesAndBoardingGates() 
{
    string AIRLINESCSVPATH = "..\\..\\..\\..\\data\\airlines.csv";
    string BOARDINGGATESCSVPATH = "..\\..\\..\\..\\data\\boardingGates.csv";
    
    List<Airline> loadedAirlines = new List<Airline>();
    List<BoardingGate> loadedBoardingGates = new List<BoardingGate>();

    using (StreamReader airlinesReader = new StreamReader(AIRLINESCSVPATH))
    {
        string? line;
        airlinesReader.ReadLine(); // skip the header
        while ((line = airlinesReader.ReadLine()) != null)
        {
            string[] data = line.Split(",");
            Airline airline = new Airline(data[0], data[1]);
            loadedAirlines.Add(airline);
        }
        Console.WriteLine($"{loadedAirlines.Count} Airlines Loaded!");
    }

    using (StreamReader boardingGatesReader = new StreamReader(BOARDINGGATESCSVPATH))
    {
        string? line;
        boardingGatesReader.ReadLine(); //skip the header
        while ((line = boardingGatesReader.ReadLine()) != null)
        {
            string[] data = line.Split(",");
            BoardingGate boardingGate = new BoardingGate(data[0], bool.Parse(data[1]), bool.Parse(data[2]), bool.Parse(data[3]));
            loadedBoardingGates.Add(boardingGate);
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
        flightsreader.ReadLine(); //reads the header 
        while ((line = flightsreader.ReadLine()) != null)
        {
            string[] strings = line.Split(",");
            string flightnumber = strings[0];
            string origin = strings[1];
            string destination = strings[2];
            DateTime expectedtime = Convert.ToDateTime(strings[3]);
            string specialrequestcode = strings[4];
        }
    }
}
//Feature 3 : hongyi
void DisplayFlights()
{

}
//Feature 4 : yinuo
void DisplayBoardingGates()
{

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


//Main Program
MainMenu();
LoadAirlinesAndBoardingGates();
LoadFlights();

