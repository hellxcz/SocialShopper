using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Entities.Interface;

namespace SocialShopper.Core.Services.Interface
{
    public interface IDataServiceBase<T>
		: IHaveInit
        where T : class, new()
    {
        void Insert(T data);
        void Insert(IEnumerable<T> data);
        void InsertWithChildren(IEnumerable<T> data);
        void InsertWithChildren(T data);
        void Update(T data);
		void UpdateWithChildren(IEnumerable<T> data);
		void UpdateWithChildren(T data);
        void Delete(T data);
        void DeleteAll();
        void DeleteAll(IEnumerable<T> data);
        IEnumerable<T> Filter(Func<T, bool> where);
        IEnumerable<T> GetAllWithChildren(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAllWithChildren();
        IEnumerable<T> GetAll();
    }

    public interface IDataServiceBase<T, I> : IDataServiceBase<T> 
        where T: class, IHaveId<I>, new()
    {
        T GetById(I id);
        T GetByIdWithChildren(I id);
        IList<T> GetByIds(IEnumerable<I> ids);
    }
}