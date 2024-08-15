using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Interfaces.Mapping
{
    public interface IAutoMapperService
    {
        IMapper Mapper { get; }
    }
}
