﻿using DatabaseHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using User.Domain;
using UserDomain.Repositories;

namespace UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextOptions<ApiContext> _options = new DbContextOptionsBuilder<ApiContext>()
                               .UseInMemoryDatabase("ApiDb")
                               .Options;
        public void InsertUser(UserEntity user)
        {
            using (var context = new ApiContext(_options))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void UpdateUser(UserEntity user)
        {
            try
            {
                using (var context = new ApiContext(_options))
                {
                    context.Users.Update(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UserEntity SelectUser(string email)
        {
            try
            {
                using (var context = new ApiContext(_options))
                {
                    UserEntity result = context.Users
                                            .Where(s => s.Email.Equals(email))
                                            .Include(u => u.Phones)
                                            .FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
