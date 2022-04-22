using DemoDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoRepository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoDbContext _context;

        public IRoleRepository Roles { get; } 
        public IEmployeeRepository Employees { get; }

        public UnitOfWork(DemoDbContext eformDbContext,
            IRoleRepository catalogueRepository,IEmployeeRepository employeeRepository)
        {
            this._context = eformDbContext;
            this.Roles = catalogueRepository;
            this.Employees = employeeRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
