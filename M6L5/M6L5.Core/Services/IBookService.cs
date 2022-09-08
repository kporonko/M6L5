using M6L5.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L5.Core.Services
{
    public interface IBookService
    {
        Book Get(int id);
        List<Book> Get();
        void Delete(int Id);
        void Add(Book book);
        void Update(int id, string desc);
    }
}
