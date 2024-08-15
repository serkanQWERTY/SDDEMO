using SDDEMO.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Interfaces.Managers
{
    public interface IManager<T> where T : BaseEntity
    {
        IQueryable<T> GetAllFromDatabase();
        IQueryable<T> GetByFilterFromDatabase(Expression<Func<T, bool>> predicate);
        T GetByIdFromDatabase(Guid id);
        T AddToDatabase(T entity);
        T GetDefaultRecord();
        void UpdateInDatabase(T entity);
        void DeleteFromDatabase(Guid id);
        bool AnyDataExists();
    }
}
