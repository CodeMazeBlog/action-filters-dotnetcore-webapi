using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFilters.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
