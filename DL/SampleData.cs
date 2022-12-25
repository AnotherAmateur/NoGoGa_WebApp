using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitData(EFDBContext context)
        {
            if (!context.User.Any())
            {
                context.User.Add(new User() {Login = "admin", FullName = "admin", Email = "admin@ya.ru", Password = "admin", RegistrationDate = new(2023, 1, 1) });
				context.SaveChanges();
			}
        }
    }
}
