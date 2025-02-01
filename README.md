# FlightApp - Terminal 5 Management System

## Release Notes (v0.0.1-alpha)

### Overview
FlightApp is a C# application designed to manage airlines, boarding gates, and flights at Terminal 5. It provides an interactive menu for users to perform various operations, such as loading data, managing flights, assigning boarding gates, and calculating fees. The application supports both **basic** and **advanced features** for comprehensive terminal management.

---

## Key Features

### Basic Features

1. **Load Airlines and Boarding Gates**  
   - Loads airline and boarding gate data from CSV files.

2. **Load Flights**  
   - Loads flight data from a CSV file, including special flight types.

3. **List All Flights**  
   - Displays basic flight information for all flights.

4. **List All Boarding Gates**  
   - Displays boarding gate details, including assigned flights and special request codes.

5. **Assign a Boarding Gate to a Flight**  
   - Assigns a boarding gate to a flight and updates the flight status.

6. **Create a New Flight**  
   - Adds a new flight to the system and updates the CSV file.

7. **Display Full Flight Details for an Airline**  
   - Shows detailed information for all flights of a selected airline.

8. **Modify Flight Details**  
   - Allows users to modify or delete flight details.

9. **Display Scheduled Flights in Chronological Order**  
   - Displays all flights ordered by departure/arrival time, including boarding gate assignments.

---

### Advanced Features

1. **Process All Unassigned Flights to Boarding Gates in Bulk**  
   - Automatically assigns boarding gates to unassigned flights based on special request codes.

2. **Display Total Fee per Airline for the Day**  
   - Calculates and displays fees for each airline, including discounts and totals.

---

## How to Use

1. **Download the Standalone Executable**  
   - Visit the [Releases](https://github.com/ouniNP/FlightApp/releases](https://github.com/ouniNP/S10269277_PRG2Assignment/releases, "Releases Page") section of this repository.  
   - Download the latest standalone `.exe` file.

2. **Run the Program**  
   - Double-click the downloaded `.exe` file to launch the application.  
   - The program will load data from the provided CSV files and display a menu.

3. **Enjoy!**  
   - Use the menu to perform operations such as:  
     - Listing flights and boarding gates.  
     - Assigning boarding gates.  
     - Creating or modifying flights.  
     - Processing unassigned flights (Advanced Feature).  
     - Calculating airline fees (Advanced Feature).
   - To exit the program, at the main menu section key in **0**.

---

## Technical Details
- **Programming Language**: C#  
- **Dependencies**: None (self-contained executable).  
- **Data Files**:  
  - `airlines.csv`: Contains airline data.  
  - `boardinggates.csv`: Contains boarding gate data.  
  - `flights.csv`: Contains flight data.

---

## Future Enhancements
- Add support for real-time flight updates.  
- Implement a graphical user interface (GUI).  
- Integrate with external APIs for live flight data.

---

## Contributors
- [Hong Yi](https://github.com/pewpewwgun)
- [Yinuo](https://github.com/ouniNP)

---

## License
All rights reserved. This project is for personal and educational use only. Unauthorized copying, distribution, or modification is prohibited.
