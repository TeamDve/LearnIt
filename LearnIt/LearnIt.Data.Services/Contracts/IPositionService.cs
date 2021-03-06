﻿using LearnIt.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Data.Services.Contracts
{
    public interface IPositionService
    {
        IEnumerable<NameHolder> ReturnAllPossitionNames();

        Task AddPosToUserPossition(string username, string position);
    }
}
