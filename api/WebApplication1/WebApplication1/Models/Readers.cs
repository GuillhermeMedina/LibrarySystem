using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Readers
    {
        public int ReaderId { get; set; }
        public string ReaderName { get; set; }
        public string UserType { get; set; }
        public string ReaderPassword { get; set; }
        public string Reservations { get; set; }
    }
}
