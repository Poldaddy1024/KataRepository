using Kata.BL.Interfaces;
using Kata.BusinessModel.Entities;
using Kata.Dto.Dtos;
using Kata.Dto.Models;
using Kata.ORM.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace Kata.DAL.Repositories
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        public TripRepository(DataDbContext context)
            : base(context)
        {
        }

        
        // DbContext
        public DataDbContext dbContext
        {
            get { return _context as DataDbContext; }
        }
        

        // Method to Add the new File in Db
        public void AddFile(string fileName)
        {
            // Set the new file
            var file = new File
            {
                FileName = fileName
            };            

            // Add the new file
            dbContext.File.Add(file);           
        }

        // Method to add only new Drivers in Db
        public void AddDrivers(string[] fileRead, string fileName)
        {                     
            // Parse the file and add just Drivers
            foreach (string row in fileRead)
            {
                // Take the line
                string[] data = row.Split(" ");

                // Evaluate if Driver or Trip
                if(data[0] == "Driver")
                {
                    // Verify Driver not exist in Db
                    var driver = dbContext.Driver.Where(x => x.DriverName == data[1]).SingleOrDefault();                    

                    if(driver == null)
                    {
                        // Get the FileId
                        var file = dbContext.File.Where(x => x.FileName == fileName).SingleOrDefault();

                        // Set the Driver
                        var newDriver = new Driver
                        {
                            DriverName = data[1],
                            FileId = file.FileId
                        };

                        // Add the new Driver
                        dbContext.Driver.Add(newDriver);
                    }                    
                }                
            }            
        }

        // Methods to append Trips to Drivers in Db
        public void AddTrips(string[] fileRead, string fileName)
        {
            // Get Current Date
            DateTime currentDate = DateTime.Now;
            
            // Get the FileId
            var file = dbContext.File.Where(x => x.FileName == fileName).SingleOrDefault();

            // Parse the file and add just Trips
            foreach (string row in fileRead)
            {
                // Take the line
                string[] data = row.Split(" ");

                // Evaluate if Driver or Trip                                
                if (data[0] == "Trip")
                {                    

                    // Get time for Start Date
                    string[] startTime = data[2].Split(":");                    
                    long ticksStartDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
                                     Convert.ToInt32(startTime[0]), Convert.ToInt32(startTime[1]), 0,
                                     new CultureInfo("en-US", false).Calendar).Ticks;
                    DateTime startDate = new DateTime(ticksStartDate);

                    // Get time for End Date
                    string[] endTime = data[3].Split(":");
                    long ticksEndDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
                                     Convert.ToInt32(endTime[0]), Convert.ToInt32(endTime[1]), 0,
                                     new CultureInfo("en-US", false).Calendar).Ticks;
                    DateTime endDate = new DateTime(ticksEndDate);

                    // Get Time difference if any
                    var dateDifference = DateTime.Compare(startDate, endDate);

                    // Get Day difference if any
                    var daysDifference = (endDate - startDate).TotalDays;

                    // Business Rule: verify Start Time is before End Time and End Time does not pass Midnight
                    if (dateDifference < 0 && daysDifference < 1)
                    {                        
                        // Get Time of the Trip
                        TimeSpan timeDifference = (endDate - startDate);
                        var tripDuration = timeDifference.TotalHours;

                        // Get Miles of the Trip
                        var milesTrip = Math.Round(Convert.ToDouble(data[4]), MidpointRounding.AwayFromZero);
                        
                        // Get average mph
                        var averageMph = Math.Round((milesTrip / tripDuration),MidpointRounding.AwayFromZero);

                        // Business Rule: Average mph must be grather 5 and less than 100
                        if (averageMph >= 5 && averageMph <= 100 )
                        {
                            // Get the driver by his name
                            var driver = dbContext.Driver.Where(x => x.DriverName == data[1]).SingleOrDefault();

                            // If driver exist in Db
                            if (driver != null)
                            {
                                // Set the new Trip
                                var trip = new Trip
                                {
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    Miles = (int)milesTrip,
                                    AvgMph = (int)averageMph,
                                    TripDuration = (decimal)tripDuration,
                                    DriverId = driver.DriverId,
                                    FileId = file.FileId
                                };

                                // Add the new Trip to the Driver
                                dbContext.Trip.Add(trip);
                            }
                        }                                               
                    }                    
                }
            }            
        }

        // Get Report
        public TripDto GetTripsByDriver(string fileName)
        {
            // Get the Id of the File that is being processed
            var file = dbContext.File.Where(x => x.FileName == fileName).SingleOrDefault();

            // Get the list of all Drivers contained in that specific file
            var drivers = dbContext.Driver.Where(x => x.FileId == file.FileId).ToList();

            // Set Dto(Report Model View)
            var tripDto = new TripDto();

            // For each Driver get their totals
            foreach (var driver in drivers)
            {
                // Get Trips for current Driver included in processed File
                var result = dbContext.Trip.Where(x => x.DriverId == driver.DriverId && x.FileId == file.FileId)
                                     .GroupBy(g => new { g.Driver.DriverName })
                                     .Select(g => new TripModel
                                     {
                                         DriverName = g.Key.DriverName,
                                         TotalMiles = g.Sum(m => m.Miles),                                         
                                         AvgMph = (int)Math.Round((g.Sum(a => a.Miles) / g.Sum(a => a.TripDuration)), MidpointRounding.AwayFromZero)
                                     }).SingleOrDefault();                                     

                // If null add a blank record
                if(result != null)
                {
                    tripDto.Trips.Add(result);
                }
                else
                {
                    tripDto.Trips.Add(new TripModel {DriverName = driver.DriverName,TotalMiles = 0, AvgMph = 0 });
                }
                
            }                      

            return tripDto;
        }
        
    }
}
