using Kata.BL.Interfaces;
using Kata.BL.UoW;
using Kata.DAL.Repositories;
using Kata.ORM.EntityFramework;

namespace Kata.DAL.UoW
{
    public class TripUnitOfWork : ITripUnitOfWork
    {
        private readonly DataDbContext _context;

        public TripUnitOfWork(DataDbContext context)
        {
            _context = context;
            TripRepository = new TripRepository(_context);            
        }

        public ITripRepository TripRepository { get; private set; }

        public ITripRepository TripCodeRepository => throw new System.NotImplementedException();

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
