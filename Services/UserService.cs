using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data;
using TrainingDiary.Data.POCO;

namespace TrainingDiary.Services
{
    public interface IUserService
    {
        public Task<User> GetByUsernameAndPassword(string username, string password);
        public Task<User> GetByGoogleId(string googleId);
    }
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        private List<User> users = new List<User>
        {
            new User { Id = 3522, Name = "roland", Password = "secret",
                                                         Role = "Admin", GoogleId = "101517359495305583936" }
        };

        public async Task<User> GetByUsernameAndPassword(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.Name == username &&
            u.Password == password);
            return user;
        }

        public Task<User> GetByGoogleId(string googleId)
        {
            throw new NotImplementedException();
        }
    }
}
