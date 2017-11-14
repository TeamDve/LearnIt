﻿using LearnIt.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Data.Services.Contracts
{
    public interface IPossitionService
    {
        IEnumerable<NameHolder> ReturnAllPossitionNames();
    }
}
