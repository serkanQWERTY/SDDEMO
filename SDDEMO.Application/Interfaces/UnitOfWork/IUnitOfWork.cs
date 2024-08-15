using SDDEMO.Application.Interfaces.Repositories;
using SDDEMO.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        IUserRepository userRepository { get; }
        void CommitChanges();
    }
}
