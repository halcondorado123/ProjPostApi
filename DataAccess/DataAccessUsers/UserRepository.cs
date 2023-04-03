using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessUsers
{
    public class UserRepository : IUserRepository
    {
        List<UserME> users = new List<UserME>()
        {
            new UserME() { userName = "adminMaster", password = "123456" }
        };

        public bool IsUser(string userName, string password)
        {
            return users.Where(d => d.userName == userName && d.password == password).Count() > 0;
;       }
    }
}
