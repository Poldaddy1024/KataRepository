using Kata.Dto.Dtos;

namespace Kata.BL.Interfaces
{
    public interface ITripRepository
    {
        // Methods to Process the new file
        void AddFile(string fileName);
        void AddDrivers(string[] fileRead, string fileName);
        void AddTrips(string[] fileRead, string fileName);
        TripDto GetTripsByDriver(string fileName);        
    }
}
