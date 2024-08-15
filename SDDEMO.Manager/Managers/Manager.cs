using Microsoft.AspNetCore.Http;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Extensions;
using SDDEMO.Application.Interfaces.Managers;
using SDDEMO.Application.Interfaces.Repositories;
using SDDEMO.Application.Interfaces.UnitOfWork;
using SDDEMO.Domain.Entity;
using SDDEMO.Manager.Helpers;
using SDDEMO.Manager.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Manager.Managers
{
    public class Manager<T> : AutoMapperService, IManager<T> where T : BaseEntity
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<T> _repository;
        private readonly ILoggingManager logger;

        public Manager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ILoggingManager logger)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
            _httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public T AddToDatabase(T entity)
        {
            User user = new TokenProvider(_httpContextAccessor, _unitOfWork).GetUserByToken();

            entity.creationDate = DateTime.Now;
            entity.updatedDate = DateTime.Now;
            entity.isDeleted = false;
            entity.isActive = true;
            entity.id = new Guid();
            entity.user = user;

            _repository.Add(entity);
            _unitOfWork.CommitChanges();

            logger.InfoLog(LogMessages.InsertDatabase.ToDescriptionString().Replace("{entity}", typeof(T).Name).Replace("{guid}", entity.id.ToString()));

            return entity;
        }

        public void DeleteFromDatabase(Guid id)
        {
            _repository.Delete(id);
            _unitOfWork.CommitChanges();

            logger.InfoLog(LogMessages.DeleteDatabase.ToDescriptionString().Replace("{entity}", typeof(T).Name).Replace("{guid}", id.ToString()));
        }

        public IQueryable<T> GetAllFromDatabase()
        {
            User user = new TokenProvider(_httpContextAccessor, _unitOfWork).GetUserByToken();

            logger.InfoLog(LogMessages.GetAllDatabase.ToDescriptionString().Replace("{entity}", typeof(T).Name));

            return _repository.GetAllWithFilter(a => Guid.Equals(user.id, a.user.id));
        }

        public IQueryable<T> GetByFilterFromDatabase(Expression<Func<T, bool>> predicate)
        {
            User user = new TokenProvider(_httpContextAccessor, _unitOfWork).GetUserByToken();

            logger.InfoLog(LogMessages.GetAllDatabase.ToDescriptionString().Replace("{entity}", typeof(T).Name));

            return _repository.GetAllWithFilter(predicate).Where(a => Guid.Equals(user.id, a.user.id));
        }

        public T GetByIdFromDatabase(Guid id)
        {
            logger.InfoLog(LogMessages.GetDatabase.ToDescriptionString().Replace("{entity}", typeof(T).Name).Replace("{guid}", id.ToString()));
            return _repository.GetById(id);
        }
        public T GetDefaultRecord()
        {
            return _repository.GetDefaultRecord();
        }

        public void UpdateInDatabase(T entity)
        {
            _repository.Update(entity);
            _unitOfWork.CommitChanges();

            logger.InfoLog(LogMessages.UpdateDatabase.ToDescriptionString().Replace("{entity}", typeof(T).Name).Replace("{guid}", entity.id.ToString()));
        }

        public bool AnyDataExists()
        {
            User user = new TokenProvider(_httpContextAccessor, _unitOfWork).GetUserByToken();
            var data = GetByFilterFromDatabase(a => Guid.Equals(a.user.id, user.id)).ToList();

            return (data != null && data.Count() > 0);
        }
    }
}
