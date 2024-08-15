using SDDEMO.Application.Interfaces.Repositories;
using SDDEMO.Domain.Entity;
using SDDEMO.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Persistance.Repositories
{
    class UserRepository : Repository<User>, IUserRepository
    {
        private DatabaseContext _context { get => _dbContext as DatabaseContext; }
        public UserRepository(DatabaseContext context) : base(context) { }
    }
}
