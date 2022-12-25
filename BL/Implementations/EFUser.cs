using BuissnesLayaer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayaer.Implementations
{
    public class EFUser : IUserRep
    {
        private EFDBContext context;

        public EFUser(EFDBContext context)
        {
            this.context = context; 
        }

        public void DeleteUser(User user)
        {
            context.User.Remove(user);
            context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.User.ToList();
        }

        public User GetUserByLogin(string userLogin)
        {
            return context.User.FirstOrDefault(x => x.Login == userLogin);
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                context.User.Add(user);
            }
            else
            {
                context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

			context.SaveChanges();
		}
    }
}
