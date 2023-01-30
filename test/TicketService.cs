/*Steps to install the CSVHelper from NuGet Package Manager:
Step 1 - Right Click on the Project
Step 2 - Go To "Manage NuGet Packages..."
Step 3 - Go To Browse Tab then move to the Search area
Step 4 - Type "CSVHelper" in the search box */

using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the CSV file
            using (var reader = new StreamReader("parking-citations-sm.csv"))
            using (var csv = new CsvReader(reader))
            {
                // Configure the reader to use the TicketMap class
                csv.Configuration.RegisterClassMap<TicketMap>();

                // Read the records into a list
                var tickets = csv.GetRecords<Ticket>().ToList();

                // Combine the Issue Date and Issue Time into a single Issue Date Time value
                foreach (var ticket in tickets)
                {
                    ticket.IssueDateTime = DateTime.Parse(ticket.IssueDate.ToShortDateString() + " " + ticket.IssueTime);
                }

                // Convert the plate expiry value into a date value with the day as the last day of the month
                foreach (var ticket in tickets)
                {
                    ticket.PlateExpiry = new DateTime(ticket.PlateExpiry.Year, ticket.PlateExpiry.Month, DateTime.DaysInMonth(ticket.PlateExpiry.Year, ticket.PlateExpiry.Month));
                }

                // Convert invalid latitude and longitude values into none values
                foreach (var ticket in tickets)
                {
                    if (ticket.Latitude == 999999 || ticket.Longitude == 999999)
                    {
                        ticket.Latitude = null;
                        ticket.Longitude = null;
                    }
                }

                // Write the updated records to a new CSV file
                using (var writer = new StreamWriter("parking-citations-sm-updated.csv"))
                using (var csvWriter = new CsvWriter(writer))
                {
                    csvWriter.WriteRecords(tickets);
                }

                // Total of fines issues per year per make of vehicle
                var finesByYearAndMake = from ticket in tickets
                                         group ticket by new { ticket.IssueDateTime.Year, ticket.Make } into g
                                         select new
                                         {
                                             Year = g.Key.Year,
                                             Make = g.Key.Make,
                                             TotalFines = g.Sum(x => x.FineAmount)
                                         };

                // Average and Standard Deviation of the fine amount per year per agency
                var avgAndStdDevByYearAndAgency = from ticket in tickets
                                                  group ticket by new { ticket.IssueDateTime.Year, ticket.Agency } into g
                                                  select new
                                                  {
                                                      Year = g.Key.Year,
                                                      Agency = g.Key.Agency,
                                                      Average = g.Average(x => x.FineAmount),
                                                      StandardDeviation = Math.Round(g.StdDev(x => x.FineAmount), 2)
                                                  };

                // Print the result of finesByYearAndMake
                Console.WriteLine("Year, Make, Total Fines");
                foreach (var item in finesByYearAndMake)
                {
                    Console.WriteLine("{0}, {1}, {2}", item.Year, item.Make, item.TotalFines);
                }

                // Print the result of avgAndStdDevByYearAndAgency
                Console.WriteLine("Year, Agency, Average, Standard Deviation");
                foreach (var item in avgAndStdDevByYearAndAgency)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", item.Year, item.Agency, item.Average, item.StandardDeviation);
                }

            }
        }
    }
}
