using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Books
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAutor { get; set; }
        public string BookSinopse { get; set; }
        public DateTime BookRelease { get; set; }
        public string BookStatus { get; set; }
        public string Reservations { get; set; }
    }
}
