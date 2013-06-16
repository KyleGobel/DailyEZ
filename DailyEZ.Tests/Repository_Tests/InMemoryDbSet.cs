using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace JetNettApi.Data.Tests.Repository_Tests
{
    public class InMemoryDbSet<T> : IDbSet<T> where T : class
    {
        private readonly List<T> _data;

        public InMemoryDbSet()
        {
            _data = new List<T>();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Expression Expression
        {
            get { return _data.AsQueryable().Expression; }
        }
        public Type ElementType { get { return _data.AsQueryable().ElementType; } }
        public IQueryProvider Provider { get { return _data.AsQueryable().Provider; } }
        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local { get; private set; }
    }
}