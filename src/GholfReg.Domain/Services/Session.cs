using System;
using System.Collections.Generic;
using System.Linq;
using GholfReg.Domain;

namespace GholfReg.Domain.Services
{
    public class Session : ISession
    {
        private readonly List<object> _items = new List<object>();

        public T Get<T>(Guid id)
        {
            return (T)_items.FirstOrDefault(item => ((Entity)item).Id.Equals(id));
        }

        public IList<T> GetAll<T>()
        {
            return _items.Cast<T>().ToList();
        }

        public void Save<T>(T entity)
        {
            _items.Add(entity);
        }

        public void Delete<T>(T entity)
        {
            _items.Remove(entity);
        }
    }
}