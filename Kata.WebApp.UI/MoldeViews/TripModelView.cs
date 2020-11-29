using Kata.WebApp.UI.Models;
using System.Collections.Generic;

namespace Kata.WebApp.UI.MoldeViews
{
    public class TripModelView
    {
        // Constructor
        public TripModelView()
        {
            Trips = new List<TripModel>();
        }

        // List of Trips by Driver
        public virtual ICollection<TripModel> Trips { get; set; }
    }
}
