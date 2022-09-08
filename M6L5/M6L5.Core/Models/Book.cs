using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L5.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string AuthorFullName { get; set; }
        public double Price { get; set; }
        public int BooksSold { get; set; }
    }
}
