using LearnIt.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.DataModels;
using LearnIt.Data.Context;

namespace LearnIt.Data.Services
{
    public class DepartmentService : IDepartmenService
    {

        private readonly ApplicationDbContext dbContext;

        public DepartmentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext cannot be null");
        }

        public IEnumerable<NameHolder> ReturnAllDepartmentNames()
        {
            IEnumerable<NameHolder> departmentNames = this.dbContext
                .Departments
                .Select
                (d => new NameHolder()
                {
                    Names = d.Name
                }).ToList();
            return departmentNames;
        }
    }
}
