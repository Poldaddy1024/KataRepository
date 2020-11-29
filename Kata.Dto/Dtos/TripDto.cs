using Kata.Dto.Models;
using System.Collections.Generic;

namespace Kata.Dto.Dtos
{
    public class TripDto
    {
        // Constructor
        public TripDto()
        {
            Trips = new List<TripModel>();
        }

        // List of Trips by Driver
        public virtual ICollection<TripModel> Trips { get; set; }
    }
}
