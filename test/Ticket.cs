using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParkingApp
{
    public class Ticket
    {
        public long Number { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueTime { get; set; }
        public string MeterId { get; set; }
        public int MarkedTime { get; set; }
        public string StatePlate { get; set; }
        public DateTime PlateExpiry { get; set; }
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Style { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public string Route { get; set; }
        public int Agency { get; set; }
        public string ViolationCode { get; set; }
        public string ViolationDesc { get; set; }
        public int FineAmount { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string AgencyDesc { get; set; }
        public string ColorDesc { get; set; }
        public string StyleDesc { get; set; }


        //Mapping the fields makes it easier to read the data from the CSV file and store it in the Ticket object
        public class TicketMap : ClassMap<Ticket>
        {
            public TicketMap()
            {
                Map(m => m.Number).Name("Number");
                Map(m => m.IssueDate).Name("IssueDate");
                Map(m => m.IssueTime).Name("IssueTime");
                Map(m => m.MeterId).Name("MeterId");
                Map(m => m.MarkedTime).Name("MarkedTime");
                Map(m => m.StatePlate).Name("StatePlate");
                Map(m => m.PlateExpiry).Name("PlateExpiry");
                Map(m => m.Vin).Name("Vin");
                Map(m => m.Make).Name("Make");
                Map(m => m.Style).Name("Style");
                Map(m => m.Color).Name("Color");
                Map(m => m.Location).Name("Location");
                Map(m => m.Route).Name("Route");
                Map(m => m.Agency).Name("Agency");
                Map(m => m.ViolationCode).Name("ViolationCode");
                Map(m => m.ViolationDesc).Name("ViolationDesc");
                Map(m => m.FineAmount).Name("FineAmount");
                Map(m => m.Latitude).Name("Latitude");
                Map(m => m.Longitude).Name("Longitude");
                Map(m => m.AgencyDesc).Name("AgencyDesc");
                Map(m => m.ColorDesc).Name("ColorDesc");
                Map(m => m.StyleDesc).Name("StyleDesc");
            }
        }
    }
}