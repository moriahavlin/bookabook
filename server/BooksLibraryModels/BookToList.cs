using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryModels
{
    public class BookToList
    {
        public Lookup nameId { get; set; }
        public int id { get; set; }
      //  public Lookup categoryId { get; set; }
      //  public Lookup publishingId { get; set; }
     //   public string description { get; set; }
        public bool isBorrowed { get; set; }
    //    public int lenderId { get; set; }
        public Lookup autherId { get; set; }
    //    public int numOfPages { get; set; }
        public string picNAme { get; set; }
        public int numberOfViewers { get; set; }
    }
}
