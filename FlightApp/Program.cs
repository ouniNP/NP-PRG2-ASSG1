//==========================================================
// Student Number : S10265849
// Student Name	  : Teo Hong Yi
// Partner Name	  : Wang Yi Nuo
//==========================================================


//Dependencies
using FlightApp;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;

void MainMenu()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1.  List All Flights");
    Console.WriteLine("2.  List Boarding Gates");
    Console.WriteLine("3.  Assign a Boarding Gate to a Flight");
    Console.WriteLine("4.  Create Flight");
    Console.WriteLine("5.  Display Airline Flights");
    Console.WriteLine("6.  Modify Flight Details");
    Console.WriteLine("7.  Display Flight Schedule");
    Console.WriteLine("8.  Display the total fee per Airline");
    Console.WriteLine("9.  Bulk Assign Flights to Boarding Gates");
    Console.WriteLine("10. Remove all flights from boarding gates");
    Console.WriteLine("0.  Exit");
    Console.WriteLine();
}


//Feature 1 : yinuo
void LoadAirlinesAndBoardingGates(Terminal terminal) 
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
            terminal.AddAirline(airline);
        }
        Console.WriteLine($"{terminal.Airlines.Count} Airlines Loaded!");
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
            terminal.AddBoardingGate(boardingGate);
        }
        Console.WriteLine($"{terminal.BoardingGates.Count} Boarding Gates Loaded!");
    }

}
//Feature 2 : hongyi (completed)
void LoadFlights(Terminal terminal)
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
            terminal.Flights.Add(flightnumber, flight);
        }
        Console.WriteLine($"{terminal.Flights.Count} Flights Loaded!");
    }
}

//Feature 3.1 : hongyi (completed)
void AssignFlightToAIrline(Terminal terminal)
{
    foreach (Flight flight in terminal.Flights.Values)
    {
        string AirlineCode = flight.FlightNumber.Substring(0,2);
        terminal.Airlines[AirlineCode].AddFlight(flight);
    }
}

//Feature 3.2: hongyi (completed)
void DisplayFlights(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
    foreach (Flight flight in terminal.Flights.Values)
    {
        if (flight == null || string.IsNullOrEmpty(flight.FlightNumber) || flight.FlightNumber.Length < 2)
        {
            Console.WriteLine("Error: Invalid flight data.");
            return;
        }
        try
        {
            string AirlineCode = flight.FlightNumber.Substring(0, 2);
            string AirlineName = terminal.Airlines[AirlineCode].Name;
            Console.WriteLine($"{flight.FlightNumber,-16}{AirlineName,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime}");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }
}

//Feature 4 : yinuo
void DisplayBoardingGates(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Gate Name       DDJB                   CFFT                   LWTT");
    foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
    {
        Console.WriteLine(boardingGate);
    }
}

//Feature 5 : hongyi (option 3) (completed with validation)
void AssignBoardingGateToFlight(Terminal terminal)
{
    string SpecialRequestCode = "None"; //setting special request code for normal flights , will be overriden if flight has special request code

    //Getting flightnumber (validation completed)
    Console.WriteLine("Enter Flight Number:");
    string flightnumber = Console.ReadLine()?.Trim().ToUpper(); //ensure that null inputs and lower case inputs are being handled 
    if (string.IsNullOrEmpty(flightnumber) || !terminal.Flights.ContainsKey(flightnumber))
    {
        Console.WriteLine("This flight does not exist, directing back to the main menu.");
        return;
    }
    Flight SelectedFlight = terminal.Flights[flightnumber];


    //Getting boardinggate (validation completed)
    BoardingGate SelectedBoardingGate;
    while (true)
    {
        Console.WriteLine("Enter Boarding Gate Name:");
        string gatename = Console.ReadLine()?.Trim().ToUpper(); // Handle null and lowercase inputs

        if (string.IsNullOrEmpty(gatename) || !terminal.BoardingGates.ContainsKey(gatename))
        {
            Console.WriteLine("This boarding gate does not exist. Please enter a valid gate.");
            continue;
        }
        SelectedBoardingGate = terminal.BoardingGates[gatename];

        // Check for special request and gate support
        bool isValidGate = true;
        if (SelectedFlight is DDJBFlight && !SelectedBoardingGate.SupportsDDJB)
        {
            Console.WriteLine("This boarding gate does not support DDJB flights. Please enter another gate.");
            isValidGate = false;
        }
        else if (SelectedFlight is CFFTFlight && !SelectedBoardingGate.SupportsCFFT)
        {
            Console.WriteLine("This boarding gate does not support CFFT flights. Please enter another gate.");
            isValidGate = false;
        }
        else if (SelectedFlight is LWTTFlight && !SelectedBoardingGate.SupportsLWTT)
        {
            Console.WriteLine("This boarding gate does not support LWTT flights. Please enter another gate.");
            isValidGate = false;
        }

        // If the selected gate is valid, break out of the loop
        if (isValidGate)
            break;
    }

    // Determine Special Request Code
    if (SelectedFlight is DDJBFlight) SpecialRequestCode = "DDJB";
    else if (SelectedFlight is CFFTFlight) SpecialRequestCode = "CFFT";
    else if (SelectedFlight is LWTTFlight) SpecialRequestCode = "LWTT";



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
void CreateFlight(Terminal terminal)
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

            if (terminal.Flights.ContainsKey(flightnumber))
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
        else if (specialrequestcode == "NONE")
        {
            newflight = new NORMFlight(flightnumber, origin, destination, expectedtime);
        }

        if (newflight != null)
        {
            terminal.Flights.Add(flightnumber, newflight);
            Console.WriteLine($"Flight {flightnumber} has been added!");
        }
        else
        {
            Console.WriteLine("Error: Failed to create flight.");
        }
        Console.WriteLine("Would you like to add another flight? (Y/N)");
        addanother = Console.ReadLine()?.Trim().ToUpper();
    }
    while (addanother == "Y");
}

//Feature 7 : yinuo (Option 5)
void DisplayFullFlightDetails(Terminal terminal)
{
    string selectedAirlineCode;
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Airline Code    Airline Name");
    foreach (Airline airline in terminal.Airlines.Values)
    {
        Console.WriteLine($"{airline.Code.PadRight(16)}{airline.Name}");
    }
    selectedAirlineCode = Console.ReadLine().ToUpper();
    Airline airlineQuery = new Airline();
    foreach (Airline airline in terminal.Airlines.Values)
    {
        if (selectedAirlineCode == airline.Code)
        {
            airlineQuery = airline;
            Console.WriteLine("=============================================");
            Console.WriteLine($"Airline Name: {airlineQuery.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
            foreach (Flight flight in airlineQuery.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber.PadRight(16)}{airlineQuery.Name.PadRight(23)}{flight.Origin.PadRight(23)}{flight.Destination.PadRight(23)}{flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")}");
            }
            break;
        }
        Console.WriteLine("Invalid Airline Code entered. Try again.");
    }
    
}


//Feature 8 : yinuo
void ModifyFlightDetails(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Airline Code    Airline Name");
    foreach (Airline airline in terminal.Airlines.Values)
    {
        Console.WriteLine($"{airline.Code.PadRight(16)}{airline.Name}");
    }
    Console.WriteLine("Enter Airline Code:");
    string selectedAirline = Console.ReadLine().ToUpper();
    
    if (terminal.Airlines.TryGetValue(selectedAirline, out Airline selectedAirlineObj))
    {
        Console.WriteLine($"List of Flights for {terminal.Airlines[selectedAirline].Code}");
        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
        foreach (Flight flight in selectedAirlineObj.Flights.Values)
        {
            Console.WriteLine($"{flight.FlightNumber.PadRight(16)}{terminal.Airlines[selectedAirline].Name.PadRight(23)}{flight.Origin.PadRight(23)}{flight.Destination.PadRight(23)}{flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt")}");
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
                    if (selectedFlight is CFFTFlight || selectedFlight is DDJBFlight || selectedFlight is LWTTFlight)
                    {
                        Console.WriteLine($"Special Request Code: {selectedFlight.ToString()}");
                    }
                    else
                    {
                        Console.WriteLine("Special Request Code: None");
                    }
                    string assignedGate = "Unassigned";
                    foreach (BoardingGate bGate in terminal.BoardingGates.Values)
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
                    foreach (BoardingGate gate in terminal.BoardingGates.Values)
                    {
                        if (gate.Flight == selectedFlight)
                        {
                            currentGate = gate;
                        }
                    }
                    Console.WriteLine($"Current Boarding Gate: {currentGate.GateName ?? "Unassigned"}");
                    Console.WriteLine("Enter new Boarding Gate:");
                    string newGate = Console.ReadLine().ToUpper();
                    foreach (BoardingGate bGate in terminal.BoardingGates.Values)
                    {
                        if (bGate.GateName == newGate)
                        {
                            if (selectedFlight is NORMFlight)
                            {
                                if (bGate.Flight is null && bGate.Flight != selectedFlight)
                                {
                                    bGate.Flight = selectedFlight;
                                    Console.WriteLine("Boarding Gate updated!");
                                }
                                else
                                {
                                    Console.WriteLine("Operation failed.");
                                    Console.WriteLine("This might have been due to:");
                                    Console.WriteLine("-The boarding gate is already assigned to another flight.");
                                    Console.WriteLine("The boarding gate is already assigned to this flight.");
                                }
                            }
                            else
                            { 
                                if (selectedFlight is CFFTFlight)
                                {
                                    if (bGate.Flight is null && bGate.SupportsCFFT && bGate.Flight != selectedFlight)
                                    {
                                        bGate.Flight = selectedFlight;
                                        Console.WriteLine("Boarding Gate updated!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Operation failed.");
                                        Console.WriteLine("This might have been due to:");
                                        Console.WriteLine("-The boarding gate is already assigned to another flight.");
                                        Console.WriteLine("-The boarding gate is already assigned to this flight.");
                                        Console.WriteLine("-The boarding gate does not support flights with request code CFFT");
                                    }
                                    
                                }
                                else if (selectedFlight is DDJBFlight)
                                {
                                    if (bGate.Flight is null && bGate.SupportsDDJB && bGate.Flight != selectedFlight)
                                    {
                                        bGate.Flight = selectedFlight;
                                        Console.WriteLine("Boarding Gate updated!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Operation failed.");
                                        Console.WriteLine("This might have been due to:");
                                        Console.WriteLine("-The boarding gate is already assigned to another flight.");
                                        Console.WriteLine("-The boarding gate is already assigned to this flight.");
                                        Console.WriteLine("-The boarding gate does not support flights with request code DDJB");
                                    }
                                }
                                else if (selectedFlight is LWTTFlight)
                                {
                                    if (bGate.Flight is null && bGate.SupportsLWTT && bGate.Flight != selectedFlight)
                                    {
                                        bGate.Flight = selectedFlight;
                                        Console.WriteLine("Boarding Gate updated!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Operation failed.");
                                        Console.WriteLine("This might have been due to:");
                                        Console.WriteLine("-The boarding gate is already assigned to another flight.");
                                        Console.WriteLine("-The boarding gate is already assigned to this flight.");
                                        Console.WriteLine("-The boarding gate does not support flights with request code LWTT");
                                    }
                                }
                            }
                            
                        }
                    }
                }


            }
            else if (option == "2")
            {
                Console.WriteLine("Are you sure you want to delete this flight? (Y/N)");
                string delFlight = Console.ReadLine().ToUpper();
                if (delFlight == "Y")
                {
                    selectedAirlineObj.Flights.Remove(flightToEdit);
                    Console.WriteLine("Flight deleted!");
                }
                else if (delFlight == "N")
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
void DisplayScheduledFlights(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time     Status          Boarding Gate");
    List<Flight> keysList = terminal.Flights.Values.ToList();
    keysList.Sort();
    foreach (Flight flight in keysList)
    {
        string BoardingGate = "Not Assigned";
        foreach (var gate in terminal.BoardingGates.Values)
        {
            if (gate.Flight == flight)
            {
                BoardingGate = gate.GateName;
                break;
            }
        }
        try
        {
            string AirlineCode = flight.FlightNumber.Substring(0, 2);
            string AirlineName = terminal.Airlines[AirlineCode].Name;
            Console.WriteLine($"{flight.FlightNumber,-16}{AirlineName,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime,-36}{flight.Status,-16}{BoardingGate}");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
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
//Extra feature: yinuo
void ProcessUnassignedFlightsToBoardingGates(Terminal terminal)
{
    Queue<Flight> unassignedFlights = new Queue<Flight>();
    foreach (Flight flight in terminal.Flights.Values.ToList())
    {
        bool isUnassigned = terminal.BoardingGates.Values.All(gate => gate.Flight != flight);
        if (isUnassigned)
        {
            unassignedFlights.Enqueue(flight);
        }
    }
    
    int unassignedCount = unassignedFlights.Count;
    int flightsSuccessCount = 0;

    List<Flight> FlightsAssignedSuccessfully = new List<Flight>();
    while (unassignedFlights.Count > 0)
    {
        Flight flightToAssign = unassignedFlights.Dequeue();
        foreach (BoardingGate gate in terminal.BoardingGates.Values)
        {
            if (gate.SupportsFlight(flightToAssign) && gate.Flight is null)
            {
                gate.Flight = flightToAssign;
                Console.WriteLine($"Flight {flightToAssign.FlightNumber} has been assigned to boarding gate {gate.GateName}.");
                flightsSuccessCount++;
                FlightsAssignedSuccessfully.Add(flightToAssign);
                break;
            }

        }
    }
    float percentageAssignedAutomatically = flightsSuccessCount == 0 ? 100 : ((float)flightsSuccessCount / terminal.Flights.Count);
    Console.WriteLine($"Total of {flightsSuccessCount} flights have been successfully assigned to boarding gates.");
    Console.WriteLine();
    Console.WriteLine("Flight Details:");
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
    foreach (Flight successfulFlight in FlightsAssignedSuccessfully)
    {
        Console.WriteLine($"{successfulFlight.FlightNumber,-16}{terminal.GetAirlineFromFlight(successfulFlight).Name,-23}{successfulFlight.Origin,-23}{successfulFlight.Destination,-23}{successfulFlight.ExpectedTime}");
    }
    Console.WriteLine($"Percentage of flights processed automatically: {percentageAssignedAutomatically.ToString("P1")}");
    if (flightsSuccessCount != unassignedCount)
    {
        Console.WriteLine($"{unassignedCount - flightsSuccessCount} flights could not be assigned automatically. Please try manual assignment instead.");
    }
}
//Extra feature : hongyi (option 8)
void DisplayAirlineFee(Terminal terminal)
{
    //check if all flights are assigned to a boarding gate
    foreach (Flight flight in terminal.Flights.Values)
    {
        string BoardingGate = "Not Assigned";
        foreach (var gate in terminal.BoardingGates.Values)
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
    Console.WriteLine("=============================================");
    Console.WriteLine("Airlines Fee for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Airline Name           Total Fee                 Discount            Final Total Fee");

    double totalfee = 0;
    double totaldiscount = 0;
    double finaltoalfee = 0;
    foreach (Airline airline in terminal.Airlines.Values)
    {
        double airlinefee = airline.CalculateFees();
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
                airlinefee = (airlinefee * 0.97);
            }
            double finalfee = airlinefee - Discount;
            totalfee += airlinefee;
            totaldiscount += Discount;
            finaltoalfee += finalfee;
            Console.WriteLine($"{airline.Name,-23}${airlinefee,-25}${Discount,-19}${finalfee}");
        }

        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    double discountpercentage = (totaldiscount / finaltoalfee) * 100;
    Console.WriteLine();
    Console.WriteLine("Subtotal of all Airline Fees: $" + totalfee);
    Console.WriteLine("Subtotal of all Airline Discounts: $" + totaldiscount);
    Console.WriteLine("Final Total of Airline Fees (after discounts): $" + finaltoalfee);
    Console.WriteLine("Percentage of Discounts over Final Total Fees: " + discountpercentage.ToString("F2") + "%");
}


//testing for extra feature : hong yi (option 10 )
void RemoveFlightsFromBoardingGate(Terminal terminal)
{
    foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
    {
        boardingGate.Flight = null;  // Remove assigned flight
    }
    Console.WriteLine("All flights have been removed from the boarding gates.");
}

//Collections
Terminal Terminal5 = new Terminal("Terminal 5");


//Main Program

LoadAirlinesAndBoardingGates(Terminal5);
LoadFlights(Terminal5);
AssignFlightToAIrline(Terminal5);




WhiteSpace();


//Main Loop
while (true)
{
    int option;
    while (true)
    {
        try
        {
            MainMenu();
            Console.WriteLine("Please select your option:");
            option = Convert.ToInt32(Console.ReadLine());
            break;

        }
        catch (FormatException)
        {
            Console.WriteLine("Your input is invalid. Maybe try entering just a number?");
            Console.WriteLine();
        }
        catch (Exception)
        {
            Console.WriteLine("Your number is too big/small for the program. Maybe try entering a valid number instead?");
            Console.WriteLine();
        }
    }
    if (option == 1)
    {
        DisplayFlights(Terminal5);
        WhiteSpace();
    }
    else if (option == 2)
    {
        DisplayBoardingGates(Terminal5);
        WhiteSpace();
    }
    else if (option == 3)
    {
        AssignBoardingGateToFlight(Terminal5);
        WhiteSpace();
    }
    else if (option == 4)
    {
        CreateFlight(Terminal5);
        WhiteSpace();
    }
    else if (option == 5)
    {
        DisplayFullFlightDetails(Terminal5);
    }
    else if (option == 6)
    {
        ModifyFlightDetails(Terminal5);
    }
    else if (option == 7)
    {
        DisplayScheduledFlights(Terminal5);
        WhiteSpace();
    }
    else if (option == 8)
    {
        DisplayAirlineFee(Terminal5);
        WhiteSpace();
    }
    else if (option == 9)
    {
        ProcessUnassignedFlightsToBoardingGates(Terminal5);
        WhiteSpace();

    }
    else if (option == 10)
    {
        RemoveFlightsFromBoardingGate(Terminal5);
        WhiteSpace();

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


