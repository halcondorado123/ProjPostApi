﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessUsers
{
    public interface IUserRepository
    {
        bool IsUser(string userName, string password);
    }
}
