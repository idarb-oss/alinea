using System;

namespace Alinea.Core.EventSourcing
{
    public interface IRepository<T, TVersion> where T : AggregateRoot, new()
    {
         void Save(AggregateRoot aggregate, TVersion version);

         T GetById(Guid id);
    }
}