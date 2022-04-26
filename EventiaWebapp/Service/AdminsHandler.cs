using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Service
{
    public class AdminsHandler
    {
        public readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly EventDbContext _dbContext;
        private readonly Database _database;

        public AdminsHandler(EventDbContext dbContext, Database database, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dbContext;
            _database = database;
        }
        public async Task<User> ChangeRole(User user)
        {
            var organizerUser = await _dbContext.Users.Where(x => x.Id == user.Id).FirstAsync();

            if (user.isOrganizer)
            {
                organizerUser.isOrganizer = true;
                await _userManager.AddToRoleAsync(organizerUser, MyRole.Organizer);
            }
            else if (user.isOrganizer == false)
            {
                organizerUser.isOrganizer = false;
                await _userManager.AddToRoleAsync(organizerUser, MyRole.Attendee);
                await _userManager.RemoveFromRoleAsync(organizerUser, MyRole.Organizer);
            }
            _dbContext.Update(organizerUser);
            await _dbContext.SaveChangesAsync();
            return organizerUser;
        }
        public async Task<User> GetUserById(string id)
        {
            return await _dbContext.Users.Where(x => x.Id == id).FirstAsync();
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbContext.Users.Where(x => !x.UserName.Contains("Admin")).ToListAsync();
        }
        public async Task ResetDb()
        {
            await _database.RecreateAndSeed();
            await _signInManager.SignOutAsync();
        }
    }
}
