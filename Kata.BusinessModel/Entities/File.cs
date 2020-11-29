using System.Collections.Generic;

namespace Kata.BusinessModel.Entities
{
    public class File
    {
        // Constructor
        public File()
        {            
            Drivers = new List<Driver>();
            Trips = new List<Trip>();
        }
        // PK 
        public int FileId { get; set; }
        // Properties
        public string FileName { get; set; }
        // Navegation Properties        
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }        
    }
}
