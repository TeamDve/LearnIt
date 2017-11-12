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
    public class PossitionService : IPossitionService
    {
        private readonly ApplicationDbContext dbContext;

        public PossitionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext cannot be null");
        }

        public IEnumerable<PositionNames> ReturnAllPossitionNames()
        {
            IEnumerable<PositionNames> positionNames = this.dbContext
                .DeptRoles
                .Select
                (d => new PositionNames()
                {
                    PosNames = d.Name
                }).ToList();
            return positionNames;
        }
    }
}
