using System;

namespace Kata.BusinessModel.Entities
{
    public class Trip
    {
        // PK 
        public int TripId { get; set; }
        // Properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Miles { get; set; }
        public int AvgMph { get; set; }
        public decimal TripDuration { get; set; }
        // FK        
        public int DriverId { get; set; }
        public int FileId { get; set; }
        // Navegation Properties                      
        public Driver Driver { get; set; }
        public File File { get; set; }
    }
}
