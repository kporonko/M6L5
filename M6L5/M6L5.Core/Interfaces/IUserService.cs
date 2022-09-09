using M6L5.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L5.Core.Services
{
    public interface IUserService
    {
        int GetLastId();
        List<User> GetAll();
        void Create(User user);
    }
}
