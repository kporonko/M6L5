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
    public class BookService : IBookService
    {
        private readonly IConfiguration _configuration;
        private string dir = Directory.GetParent(Directory.GetCurrentDirectory()) + "\\M6L5.Core\\";
        private readonly string path;
        public BookService(IConfiguration configuration)
        {
            _configuration = configuration;
            path = _configuration["FilePathModel"];
        }
        public void Add(Book book)
        {
            var books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(dir + path));
            Book newBook = new Book { Id = book.Id, Description = book.Description, Title = book.Title, AuthorFullName = book.AuthorFullName, BooksSold = book.BooksSold, Image = book.Image, Price = book.Price, ReleaseDate = book.ReleaseDate };
            books.Add(newBook);
            File.WriteAllText(dir + path, JsonConvert.SerializeObject(books));
        }

        public void Delete(int id)
        {
            var books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(dir + path));
            Book toDelete = books.Where(book => book.Id == id).FirstOrDefault();
            books.Remove(toDelete);
            File.WriteAllText(dir + path, JsonConvert.SerializeObject(books));
        }

        public Book Get(int id)
        {
            var books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(dir + path));
            Book toShow = books.Where(x => x.Id == id).FirstOrDefault();
            return toShow;
        }

        public List<Book> Get()
        {
            var books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(dir + path));
            return books;
        }

        public void Update(int id, string newDesc)
        {
            var books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(dir + path));
            var bookUpd = books.Where((x) => x.Id == id).FirstOrDefault();
            bookUpd.Description = newDesc;
            File.WriteAllText(dir + path, JsonConvert.SerializeObject(books));
        }
    }
}
