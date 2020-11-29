using Kata.BL.Interfaces;
using System;

namespace Kata.BL.UoW
{
    public interface ITripUnitOfWork : IDisposable
    {
        ITripRepository TripRepository { get; }
        int Save();
    }
}
