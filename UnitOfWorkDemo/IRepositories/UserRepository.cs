using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UnitOfWorkDemo.Data;
using UnitOfWorkDemo.Models;

namespace UnitOfWorkDemo.IRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<User>> All()
        {
            try {
                return await dbSet.ToListAsync();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error returning all in {Repo}", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Upsert(User user)
        {
            try {
                var existingUser = await dbSet.FirstOrDefaultAsync(x => x.Id == user.Id);

                if(existingUser == null)
                {
                    return await Add(user);
                }

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;

                return true;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error Upserting in {Repo}", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var existingUser = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if(existingUser == null)
                {
                    return false;
                }

                dbSet.Remove(existingUser);
                return true;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user in {Repo}", typeof(UserRepository));
                return false;
            }
            
        }
    }
}