using LearnIt.Data.DataModels;
using LearnIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Data.Services.Contracts
{
    public interface IUserServices
    {
        IEnumerable<NameHolder> ReturnAllUserNames();

        Task AsignUserToAdmin(string username);

        Task DeasignUserFromAdmin(string id);

        ApplicationUser ReturnUserByUsername(string username);

        bool IsUserAdmin(string id);

    }
}
