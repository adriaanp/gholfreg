using System;
using System.Collections.Generic;

namespace GholfReg.Domain.Services
{
    public interface ISession
    {
        T Get<T>(Guid id);
        IList<T> GetAll<T>();
        void Save<T>(T entity);
        void Delete<T>(T entity);
    }
}