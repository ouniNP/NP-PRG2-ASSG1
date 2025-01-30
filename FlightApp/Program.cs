//==========================================================
// Student Number : S10265849
// Student Name	  : Teo Hong Yi
// Partner Name	  : Wang Yi Nuo
//==========================================================


//Functions
using FlightApp;
using System.ComponentModel;
using System.ComponentModel.Design;
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
//Feature 2 : hongyi (completed)
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
//Feature 3.1 : hongyi (completed)
void AssignFlightToAIrline(Dictionary<string, Flight> FlightsDict , Dictionary<string, Airline> AirlinesDict)
{
    foreach (Flight flight in FlightsDict.Values)
    {
        string AirlineCode = flight.FlightNumber.Substring(0,2);
        AirlinesDict[AirlineCode].AddFlight(flight);
    }
}

//Feature 3.2: hongyi (completed)
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
//Feature 5 : hongyi (option 3) (completed with validation)
void AssignBoardingGateToFlight(Dictionary<string, Flight> FlightsDict, Dictionary<string, BoardingGate> BoardingGateDict)
{
    string SpecialRequestCode = "None"; //setting special request code for normal flights , will be overriden if flight has special request code

    //Getting flightnumber (validation completed)
    Console.WriteLine("Enter Flight Number:");
    string flightnumber = Console.ReadLine()?.Trim().ToUpper(); //ensure that null inputs and lower case inputs are being handled 
    if (string.IsNullOrEmpty(flightnumber) || !FlightsDict.ContainsKey(flightnumber))
    {
        Console.WriteLine("This flight does not exist, directing back to the main menu.");
        return;
    }
    Flight SelectedFlight = FlightsDict[flightnumber];

    
    //Getting boardinggate (validation completed)
    Console.WriteLine("Enter Boarding Gate Name:");
    string gatename = Console.ReadLine()?.Trim().ToUpper();  //ensure that null inputs and lower case inputs are being handled 
    if (string.IsNullOrEmpty(gatename) || !BoardingGateDict.ContainsKey(gatename))
    {
        Console.WriteLine("This boarding gate does not exist, directing back to the main menu.");
        return;
    }
    BoardingGate SelectedBoardingGate = BoardingGateDict[gatename];


    //check for special request
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


    //Display flight
    Console.WriteLine($"Flight Number: {SelectedFlight.FlightNumber}");
    Console.WriteLine($"Origin: {SelectedFlight.Origin}");
    Console.WriteLine($"Destination: {SelectedFlight.Destination}");
    Console.WriteLine($"Expected Time: {SelectedFlight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")}");
    Console.WriteLine($"Special Request Code: {SpecialRequestCode} ");

    //Display boarding gate
    Console.WriteLine($"Boarding Gate Name: {SelectedBoardingGate.GateName}");
    Console.WriteLine($"Supports DDJB: {SelectedBoardingGate.SupportsDDJB}");
    Console.WriteLine($"Supports CFFT: {SelectedBoardingGate.SupportsCFFT}");
    Console.WriteLine($"Supports LWTT: {SelectedBoardingGate.SupportsLWTT}");

    //Update Status of flight (validation for edit status and newstatus)
    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
    string EditStatus = Console.ReadLine().Trim().ToUpper();
    while (true)
    {
        if (EditStatus.Equals("Y"))
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
//Feature 6 : hongyi (option 4) (completed with validation)
void CreateFlight(Dictionary<string, Flight> FlightsDict)
{
    string addanother;
    do
    {
        string flightnumber;
        while (true)
        {
            Console.Write("Enter Flight Number: ");
            flightnumber = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrEmpty(flightnumber))
            {
                Console.WriteLine("Flight number cannot be empty. Please enter a valid flight number.");
                continue;
            }

            if (FlightsDict.ContainsKey(flightnumber))
            {
                Console.WriteLine("This flight number already exists. Please enter a unique flight number.");
                continue;
            }
            break;
        }

        string origin;
        while (true)
        {
            Console.Write("Enter Origin: ");
            origin = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(origin))
            {
                Console.WriteLine("Origin cannot be empty. Please enter a valid origin.");
                continue;
            }
            break;
        }

        string destination;
        while (true)
        {
            Console.Write("Enter Destination: ");
            destination = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(destination))
            {
                Console.WriteLine("Destination cannot be empty. Please enter a valid destination.");
                continue;
            }
            break;
        }

        DateTime expectedtime = DateTime.MinValue;
        while (true)
        {
            Console.Write("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
            string input = Console.ReadLine()?.Trim();

            if (DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out expectedtime))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use dd/MM/yyyy HH:mm.");
            }
        }

        string specialrequestcode;
        while (true)
        {
            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            specialrequestcode = Console.ReadLine()?.Trim().ToUpper();

            if (specialrequestcode == "LWTT" || specialrequestcode == "DDJB" || specialrequestcode == "CFFT" || specialrequestcode == "NONE")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid special request code. Please enter one of the following: CFFT, DDJB, LWTT, None.");
            }
        }

        Flight newflight = null;

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
        addanother = Console.ReadLine()?.Trim().ToUpper();
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
//Feature 8 (8.1, 8.2) : yinuo
void ModifyFlightDetails(Dictionary<string, Airline> airlineDict, Dictionary<string, BoardingGate> boardingGateDict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Airline Code    Airline Name");
    foreach (Airline airline in airlineDict.Values)
    {
        Console.WriteLine($"{airline.Code.PadRight(16)}{airline.Name}");
    }
    Console.WriteLine("Enter Airline Code:");
    string selectedAirline = Console.ReadLine().ToUpper();
    
    if (airlineDict.TryGetValue(selectedAirline, out Airline selectedAirlineObj))
    {
        Console.WriteLine($"List of Flights for {airlineDict[selectedAirline].Code}");
        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
        foreach (Flight flight in selectedAirlineObj.Flights.Values)
        {
            Console.WriteLine($"{flight.FlightNumber.PadRight(16)}{airlineDict[selectedAirline].Name.PadRight(23)}{flight.Origin.PadRight(23)}{flight.Destination.PadRight(23)}{flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")}");
        }
        Console.WriteLine("Choose an existing Flight to modify or delete:");
        string flightToEdit = Console.ReadLine().ToUpper();
        if (selectedAirlineObj.Flights.TryGetValue(flightToEdit, out Flight selectedFlight))
        {
            Console.WriteLine("1. Modify Flight Details");
            Console.WriteLine("2. Delete Flight");
            Console.WriteLine("Choose an option:");
            string option = Console.ReadLine();
            if (option == "1")
            {
                Console.WriteLine("1. Modify Basic Information");
                Console.WriteLine("2. Modify Status");
                Console.WriteLine("3. Modify Special Request Code");
                Console.WriteLine("4. Modify Boarding Gate");
                string modifyOption = Console.ReadLine();

                if (modifyOption == "1")
                {
                    Console.WriteLine("Enter new Origin:");
                    string newOrigin = Console.ReadLine();
                    Console.WriteLine("Enter new Destination:");
                    string newDestination = Console.ReadLine();
                    Console.WriteLine("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm):");
                    string newExpectedTime = Console.ReadLine();
                    selectedFlight.Origin = newOrigin;
                    selectedFlight.Destination = newDestination;
                    selectedFlight.ExpectedTime = DateTime.ParseExact(newExpectedTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    Console.WriteLine("Flight updated!");
                    Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
                    Console.WriteLine($"Airline Name: {selectedAirlineObj.Name}");
                    Console.WriteLine($"Origin: {selectedFlight.Origin}");
                    Console.WriteLine($"Destination: {selectedFlight.Destination}");
                    Console.WriteLine($"Expected Time: {selectedFlight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")}");
                    Console.WriteLine($"Status: {selectedFlight.Status}");
                    Console.WriteLine($"Special Request Code: {selectedFlight.GetType().Name}"); //Fix this, returns the type not special code
                    string assignedGate = "Unassigned";
                    foreach (BoardingGate bGate in boardingGateDict.Values)
                    {
                        if (bGate.Flight == selectedFlight)
                        {
                            assignedGate = bGate.GateName;
                        }
                        else
                        {
                            assignedGate = "Unassigned";
                        }
                    }
                    Console.WriteLine($"Boarding Gate: {assignedGate}");
                }
                else if (modifyOption == "2")
                {
                    Console.WriteLine($"Current Status: {selectedFlight.Status}");
                    Console.WriteLine("Enter new Status (Delayed/Boarding/On Time):");
                    string newStatus = Console.ReadLine().ToLower();
                    if (newStatus == "delayed" || newStatus == "boarding" || newStatus == "on time")
                    {
                        selectedFlight.Status = newStatus;
                        Console.WriteLine("Status updated!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid status, try again.");
                    }
                }
                else if (modifyOption == "3")
                {
                    Console.WriteLine("Enter updated Special Request Code (CFFT/DDJB/LWTT/None):");
                    string newSpecialRequestCode = Console.ReadLine().ToUpper();
                    var flightType = selectedFlight.GetType();
                    if (newSpecialRequestCode == "LWTT" && flightType != typeof(LWTTFlight))
                    {
                        selectedAirlineObj.Flights.Remove(selectedFlight.FlightNumber);
                        selectedFlight = new LWTTFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                        selectedAirlineObj.Flights.Add(selectedFlight.FlightNumber, selectedFlight);
                        Console.WriteLine("Flight updated with new Special Request Code: LWTT");
                    }
                    else if (newSpecialRequestCode == "DDJB" && flightType != typeof(DDJBFlight))
                    {
                        selectedAirlineObj.Flights.Remove(selectedFlight.FlightNumber);
                        selectedFlight = new DDJBFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                        selectedAirlineObj.Flights.Add(selectedFlight.FlightNumber, selectedFlight);
                        Console.WriteLine("Flight updated with new Special Request Code: DDJB");
                    }

                    else if (newSpecialRequestCode == "CFFT" && flightType != typeof(CFFTFlight))
                    {
                        selectedAirlineObj.Flights.Remove(selectedFlight.FlightNumber);
                        selectedFlight = new CFFTFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                        selectedAirlineObj.Flights.Add(selectedFlight.FlightNumber, selectedFlight);
                        Console.WriteLine("Flight updated with new Special Request Code: CFFT");
                    }
                    else if (newSpecialRequestCode == "None" && flightType != typeof(NORMFlight))
                    {
                        selectedAirlineObj.Flights.Remove(selectedFlight.FlightNumber);
                        selectedFlight = new NORMFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime);
                        Console.WriteLine("Flight updated with new Special Request Code: None");
                        selectedAirlineObj.Flights.Add(selectedFlight.FlightNumber, selectedFlight);
                    }
                    else
                    {
                        Console.WriteLine($"Error, flight is already of specified type/invalid type entered. Try again.");
                    }
                }
                else if (modifyOption == "4")
                {
                    BoardingGate? currentGate = new BoardingGate();
                    foreach (BoardingGate gate in boardingGateDict.Values)
                    {
                        if (gate.Flight == selectedFlight)
                        {
                            currentGate = gate;
                        }
                    }
                    Console.WriteLine($"Current Boarding Gate: {currentGate.GateName ?? "Unassigned"}");
                    Console.WriteLine("Enter new Boarding Gate:");
                    string newGate = Console.ReadLine().ToUpper();
                    foreach (BoardingGate bGate in boardingGateDict.Values)
                    {
                        if (bGate.GateName == newGate)
                        {
                            if (bGate.Flight is null && bGate.Flight != selectedFlight)
                            {
                                bGate.Flight = selectedFlight;
                                Console.WriteLine("Boarding Gate updated!");
                            }
                            else
                            {
                                Console.WriteLine("Error, boarding gate is already assigned to another flight.");
                            }
                        }
                    }
                }


            }
            else if (option == "2")
            {
                Console.WriteLine("Are you sure you want to delete this flight? (Y/N)");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    selectedAirlineObj.Flights.Remove(flightToEdit);
                    Console.WriteLine("Flight deleted!");
                }
                else if (Console.ReadLine().ToUpper() == "N")
                {
                    Console.WriteLine("Operation cancelled. Flight was NOT deleted.");
                }
                else
                {
                    Console.WriteLine("Invalid option, try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }
        else
        {
            Console.WriteLine("Invalid Flight Code");
        }
    }
    else
    {
        Console.WriteLine("Invalid Airline Code");
    }
}


//Feature 9 : hongyi (option 7) (completed)
void DisplayScheduledFlights(Dictionary<string, Flight> FlightsDict, Dictionary<string, Airline> AirlinesDict, Dictionary<string, BoardingGate> BoardingGateDict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time     Status          Boarding Gate");
    List<Flight> keysList = FlightsDict.Values.ToList();
    keysList.Sort();
    foreach (Flight flight in keysList)
    {
        string BoardingGate = "Not Assigned";
        foreach (var gate in BoardingGateDict.Values)
        {
            if (gate.Flight == flight)
            {
                BoardingGate = gate.GateName;
                break;
            }
        }
        string AirlineCode = flight.FlightNumber.Substring(0, 2);
        string AirlineName = AirlinesDict[AirlineCode].Name;
        Console.WriteLine($"{flight.FlightNumber,-16}{AirlineName,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime,-36}{flight.Status,-16}{BoardingGate}");
    }
}

//Feature 10 (personal creation)
void WhiteSpace()
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
}

//Extra feature (hongyi)
void DisplayAirlineFee(Dictionary<string, Flight> FlightsDict, Dictionary<string, BoardingGate> BoardingGateDict, Dictionary<string, Airline> AirlinesDict)
{
    //check if all flights are assigned to a boarding gate
    foreach (Flight flight in FlightsDict.Values)
    {
        string BoardingGate = "Not Assigned";
        foreach (var gate in BoardingGateDict.Values)
        {
            if (gate.Flight == flight)
            {
                BoardingGate = gate.GateName;
                break;
            }
        }
        if (BoardingGate == "Not Assigned")
        {
            Console.WriteLine("There are flights with unassigned boarding gates.");
            Console.WriteLine("Please ensure all flights are assigned to a boarding gate before running this feature again.");
            return;
        }
    }

    foreach (Airline airline in AirlinesDict.Values)
    {
        double TotalFee = airline.CalculateFees();
        double Discount = 0;
        int count = airline.Flights.Count;
        foreach (Flight flight in airline.Flights.Values)
        {
            if (flight.ExpectedTime.TimeOfDay < TimeSpan.FromHours(11) || flight.ExpectedTime.TimeOfDay > TimeSpan.FromHours(21))
            {
                Discount += 110;
            }


            if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)") //cheks if flight is from Dubai , Bangkok or Tokyo
            {
                Discount += 25;
            }

            if (flight is NORMFlight) //checks for special requests
            {
                Discount += 50;
            }
        }
        try
        {
            Discount += (count / 3) * 350; //For every 3 flights arriving/departing, airlines will receive a discount
            if (count > 5)
            {
                TotalFee = (TotalFee * 0.97);
            }
            Console.WriteLine(airline.Name);
            Console.WriteLine(TotalFee);
            Console.WriteLine(Discount);
            Console.WriteLine(TotalFee - Discount);
            Console.WriteLine("--------------------------------");
        }

        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

//testing for extra feature 
void assignflightstoboardinggate(Dictionary<string, Flight> FlightsDict, Dictionary<string, BoardingGate> BoardingGateDict, Dictionary<string, Airline> AirlinesDict)
{
    int i  = 0;
    var flightsList = FlightsDict.Values.ToList();  // Convert to list for easy indexing
    foreach (BoardingGate boardingGate in BoardingGateDict.Values)
    {
        if (i < flightsList.Count)
        {
            boardingGate.Flight = flightsList[i];  // Assign the flight to the boarding gate
            i++;
        }
        else
        {
            // Handle the case where there are more boarding gates than flights
            Console.WriteLine("Not enough flights to assign to all boarding gates.");
            break;
        }
    }
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
        WhiteSpace();
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
        ModifyFlightDetails(AirlinesDict, BoardingGateDict);
    }
    else if (option == 7)
    {
        DisplayScheduledFlights(FlightsDict, AirlinesDict, BoardingGateDict);
        WhiteSpace();
    }
    else if (option == 8)
    {
        DisplayAirlineFee(FlightsDict, BoardingGateDict, AirlinesDict);
    }
    else if (option == 9)
    { 
        assignflightstoboardinggate(FlightsDict, BoardingGateDict, AirlinesDict);

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


