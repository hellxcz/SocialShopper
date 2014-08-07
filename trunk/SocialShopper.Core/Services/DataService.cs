using System.Linq;
using System.Linq.Expressions;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using System.Collections.Generic;
using SocialShopper.Core.Services.Interface;
using SQLiteNetExtensions.Extensions;
using System;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Entities.Interface;

namespace SocialShopper.Core.Services
{
    public class DataServiceBase<T> : IDataServiceBase<T> 
        where T : class, new()
    {
        protected readonly ISQLiteConnection _connection;

        public DataServiceBase(ISQLiteConnectionFactory factory)
        {
            _connection = factory.Create("SocialShopper.sql");
        }

        public virtual void Init()
        {
            var info = _connection.GetTableInfo(typeof(T).Name);
            if (info.Count < 1)
            {
                _connection.CreateTable<T>();
            }
        }

        public virtual void DeleteAll()
        {
            _connection.DeleteAll<T>();
        }

        public virtual void Insert(T data)
        {
            _connection.Insert(data);
        }

        public virtual void Update(T data)
        {
            _connection.Update(data);
        }

        public virtual void UpdateWithChildren(T data)
        {
            _connection.UpdateWithChildren(data);
        }

        public virtual void Delete(T data)
        {
            _connection.Delete(data);
        }

        public virtual void DeleteAll(IEnumerable<T> data)
        {
            _connection.DeleteAll(data);
        }

        public virtual void InsertWithChildren(IEnumerable<T> data)
        {
            foreach (T item in data)
            {
                InsertWithChildren(item);
            }
        }

        public virtual void InsertWithChildren(T data)
        {
            _connection.InsertWithChildren(data, recursive: true);
        }

        public virtual void Insert(IEnumerable<T> data)
        {
            _connection.InsertAll(data);
        }

        public virtual IEnumerable<T> Filter(System.Func<T, bool> where)
        {
            var result = _connection.Table<T>();

            return result.Where(where).AsEnumerable();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _connection.Table<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> GetAllWithChildren(Expression<Func<T, bool>> where)
        {
            return _connection.GetAllWithChildren(where, recursive: true);
        }

        public virtual IEnumerable<T> GetAllWithChildren()
        {
            return GetAllWithChildren(t => true);
        }
    }

    public class DataServiceBase<T, I> : DataServiceBase<T>, IDataServiceBase<T, I>
        where T : class, IHaveId<I>, new()
    {

        public DataServiceBase(ISQLiteConnectionFactory factory)
            : base (factory)
        {}
        
        public virtual T GetById(I id)
        {
            return _connection.Get<T>(id);
        }

        public virtual IList<T> GetByIds(IEnumerable<I> ids)
        {
            return _connection
                .Table<T>()
                .Where(arg => ids.Contains(arg.Id))
                .ToList();
        }

        public virtual T GetByIdWithChildren(I id)
        {
            return _connection.GetWithChildren<T>(id, recursive: true);   
        }
    }
}