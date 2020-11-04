using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryModels
{
    public class Rating
    {
        public int id { get; set; }
        public int? borrowerId { get; set; }
        public int? rating { get; set; }
        public string desk { get; set; }
        public Lookup kealYhad { get; set; }
        public int bookId { get; set; }
        public DateTime? dateInsert { get; set; }
    }
}
