using LearnIt.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.DataModels;
using LearnIt.Data.Context;
using LearnIt.Data.Models;

namespace LearnIt.Data.Services
{
    public class DepartmentService : IDepartmenService
    {

        private readonly ApplicationDbContext dbContext;
        //Tested
        public DepartmentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext cannot be null");
        }
        //Tested
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
        //----------------------------------------------
        public async Task AddDepToUserDepartment(string username, string department)
        {
            var isDepartmentTrue = this.dbContext
                .Departments
                .Where(d => d.Name == department)
                .FirstOrDefault();

            if (isDepartmentTrue == null)
            {
                this.dbContext.Departments.Add(new Department { Name = department });
                await this.ExecuteQuery();
            }

            var userDepartment = this.dbContext.Departments.Where(d => d.Name == department).FirstOrDefault();

            this.dbContext
                .Users
                .FirstOrDefault(u => u.UserName == username)
                .Department = userDepartment;

            await this.ExecuteQuery();
        }

        private async Task ExecuteQuery()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
