using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllWithDefaultValues();
        IQueryable<T> GetAllWithFilter(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
        T GetDefaultRecord();
        T GetByFilter(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void ChangeStatus(T entity);
        void AddRange(List<T> entity, bool considerDefaultValue = false);
        void Update(T entity);
        void UpdateWithoutDefaultProp(T entity);
        void Delete(Guid id);
        void DeleteWithRange(List<Guid> guidList);
        void DeleteAll();
        void DeleteByDate(DateTime datetime);
        void DeletePermanently(Guid id);
    }
}
