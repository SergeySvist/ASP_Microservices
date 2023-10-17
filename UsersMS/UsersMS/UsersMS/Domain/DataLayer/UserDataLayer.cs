using Microsoft.EntityFrameworkCore;
using UsersMS.Domain.DbCtx;
using UsersMS.Domain.Models;

namespace UsersMS.Domain.DataLayer
{
    public class UserDataLayer : IUserDataLayer
    {
        public async Task<User> AddUser(User newuser)
        {
            using (UsersMsDbContext db = new UsersMsDbContext())
            {
                db.Users.Add(newuser);
                await db.SaveChangesAsync();

                return newuser;
            }
        }

        public async Task<User> GetUserById(long id)
        {
            using (UsersMsDbContext db = new UsersMsDbContext())
            {
                User userById = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

                return userById != null ? userById : new User();
            }
        }
    }
}
