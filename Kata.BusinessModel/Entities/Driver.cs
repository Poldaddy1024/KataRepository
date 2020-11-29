using System.Collections.Generic;

namespace Kata.BusinessModel.Entities
{
    public class Driver
    {
        public Driver()
        {
            Trips = new List<Trip>();
        }
        // PK
        public int DriverId { get; set; }
        // Properties
        public string DriverName { get; set; }    
        // PK
        public int FileId { get; set; }
        // Navegation Properties       
        public File File { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
