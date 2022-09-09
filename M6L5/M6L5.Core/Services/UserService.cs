using M6L5.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L5.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private string dir = Directory.GetParent(Directory.GetCurrentDirectory()) + "\\M6L5.Core\\";
        private readonly string path;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            path = _configuration["UsersPath"];
        }

        public void Create(User user)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(dir + path));
            User newUser = new User { Id = user.Id, Login = user.Login, Password = user.Password};
            users.Add(newUser);
            File.WriteAllText(dir + path, JsonConvert.SerializeObject(users));
        }

        public List<User> GetAll()
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(dir + path));
            return users;
        }

        public int GetLastId()
        {
            var users = GetAll();
            return users.Select(x => x.Id).Max();
        }
    }
}
