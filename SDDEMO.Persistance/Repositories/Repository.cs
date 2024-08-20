using Microsoft.EntityFrameworkCore;
using SDDEMO.Application.Interfaces.Repositories;
using SDDEMO.Domain.Entity;
using SDDEMO.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            entity.isDefault = false;
            _dbSet.Add(entity);
        }

        public void AddRange(List<T> entity, bool considerDefaultValue = false)
        {
            if (!considerDefaultValue)
            {
                foreach (var item in entity)
                    item.isDefault = false;
            }

            _dbSet.AddRange(entity);
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("isDeleted") != null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("isDeleted").SetValue(_entity, true);
                    this.Update(_entity);
                }
            }
        }

        public void DeleteWithRange(List<Guid> guidList)
        {
            var entityList = GetAllWithFilter(x => guidList.Contains(x.id));

            foreach (var entity in entityList)
            {
                T _entity = entity;
                _entity.GetType().GetProperty("isDeleted").SetValue(_entity, true);
                this.Update(_entity);
            }
        }

        public void DeleteAll()
        {
            var entityList = GetAll();

            foreach (var entity in entityList)
            {
                T _entity = entity;
                _entity.GetType().GetProperty("isDeleted").SetValue(_entity, true);
                this.Update(_entity);
            }
        }

        public void DeleteByDate(DateTime date)
        {
            var entity = _dbSet.Where(a => a.creationDate >= date && !a.isDeleted && !a.isDefault).ToList();
            _dbSet.RemoveRange((IEnumerable<T>)entity);
        }

        public T GetDefaultRecord()
        {
            return _dbSet.SingleOrDefault(a => a.isDefault && !a.isDeleted);
        }

        public T GetById(Guid id)
        {
            return _dbSet.SingleOrDefault(a => a.id == id && !a.isDeleted && !a.isDefault);
        }

        public T GetByFilter(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(a => !a.isDeleted && !a.isDefault).Where(predicate).SingleOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(a => !a.isDeleted && !a.isDefault).OrderByDescending(a => a.creationDate);
        }

        public IQueryable<T> GetAllWithDefaultValues()
        {
            return _dbSet.Where(a => !a.isDeleted).OrderByDescending(a => a.creationDate);
        }

        public IQueryable<T> GetAllWithFilter(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(a => !a.isDeleted && !a.isDefault).Where(predicate).OrderByDescending(a => a.creationDate);
        }

        public void Update(T entity)
        {
            entity.isDefault = false;
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateWithoutDefaultProp(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void ChangeStatus(T entity)
        {
            if (entity.isActive == true)
            {
                entity.isActive = false;
            }
            else if (entity.isActive == false)
            {
                entity.isActive = true;
            }

            _dbSet.Update(entity);
        }

        public void DeletePermanently(Guid id)
        {
            var entity = GetById(id);

            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }
}
