using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayaer.Interfaces
{
    public interface IUserRep
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByLogin(string userLogin);
        void SaveUser(User user);
        void DeleteUser(User user);
    }
}
