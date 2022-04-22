using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
      
        IRoleRepository Roles { get; } 
        IEmployeeRepository Employees { get; }
        int Complete();
    }
}
