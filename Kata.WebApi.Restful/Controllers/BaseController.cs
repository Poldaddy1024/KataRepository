using Kata.BL.UoW;
using Kata.DAL.UoW;
using Kata.ORM.EntityFramework;
using Kata.WebApi.Restful.MetaData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Kata.WebApi.Restful.Controllers
{
    public class BaseController: ControllerBase
    {
        private IOptions<AppSettings> _appSettings;
        private DataDbContext _dataDbContext;
        private ITripUnitOfWork _tripUoW;
        private IWebHostEnvironment _environment;

        // Contructor
        public BaseController(IOptions<AppSettings> appSettings, IWebHostEnvironment environment)
        {
            _appSettings = appSettings;
            _environment = environment;
            _dataDbContext = new DataDbContext(_appSettings.Value.ConnString);
            _tripUoW = new TripUnitOfWork(_dataDbContext);            
        }

        // Unit Of Work
        public ITripUnitOfWork TripUoW
        {
            get { return _tripUoW; }
        }

        // DbContext
        public DataDbContext DataDbContext
        {
            get { return _dataDbContext; }
        }

        public IWebHostEnvironment Environment
        {
            get { return _environment; }
            set { _environment = value; }
        }

        // Application Settings
        public IOptions<AppSettings> AppSettings
        {
            get { return _appSettings; }
            set { _appSettings = value; }
        }
    }    
}
