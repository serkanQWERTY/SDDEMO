using AutoMapper;
using SDDEMO.Application.Interfaces.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Manager.Mapping
{
    public abstract class AutoMapperService : IAutoMapperService
    {
        public IMapper Mapper
        {
            get { return MapperObject.Mapper; }
        }
    }
}
