using System;
using System.Collections.Generic;
using System.Text;
using UserService2.Models;

namespace UserService2.Seed
{
    public static class UserSeeder
    {
        public static List<User> GetUsers()
        {
            return new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Иван",
                Email = "ivan@mail.ru"
            },

            new User
            {
                Id = 2,
                Name = "Пётр",
                Email = "petr@mail.ru"
            },

            new User
            {
                Id = 3,
                Name = "Анна",
                Email = "anna@mail.ru"
            }
        };
        }
    }
}
