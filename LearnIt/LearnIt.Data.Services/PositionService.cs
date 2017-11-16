using LearnIt.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using LearnIt.Data.DataModels;
using LearnIt.Data.Context;
using LearnIt.Data.Models;

namespace LearnIt.Data.Services
{
    public class PositionService : IPositionService
    {
        private readonly ApplicationDbContext dbContext;
        //Tested
        public PositionService(ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }
        //----------------------------------------------
        public IEnumerable<NameHolder> ReturnAllPossitionNames()
        {
            IEnumerable<NameHolder> positionNames = this.dbContext
                .DeptRoles
                .Select
                (d => new NameHolder()
                {
                    Names = d.Name
                }).ToList();
            return positionNames;
        }
        //----------------------------------------------

        public async Task AddPosToUserPossition(string username, string position)
        {
            var isPositionTrue = this.dbContext
                .DeptRoles
                .Where(p=>p.Name==position)
                .FirstOrDefault();

            if (isPositionTrue == null)
            {
                this.dbContext.DeptRoles.Add(new Position { Name = position });
                await this.ExecuteQuery();
            }

            var userPosition = this.dbContext.DeptRoles.Where(p => p.Name == position).FirstOrDefault();

            this.dbContext
                .Users
                .FirstOrDefault(u => u.UserName == username)
                .Position = userPosition;

            await this.ExecuteQuery();
        }

        private async Task ExecuteQuery()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
