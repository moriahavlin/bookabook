using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryModels
{
    public class Lends
    {
        public int id { get; set; }
        //  public int borrowerId { get; set; }
        //   public int bookId { get; set; }
        public User borrower { get; set; }
        public Books book { get; set; }
        public User lender { get; set; }
        public DateTime date { get; set; }
        public bool bookIsActive { get; set; }
        public int statusCode { get; set; }


    }
}
