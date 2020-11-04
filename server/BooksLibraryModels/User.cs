using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryModels
{
    public class User
    {

        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string phone { get; set; }
        public Lookup cityCode { get; set; }

        public string address { get; set; }
        public int houseNumber { get; set; }
        public string neighborhood { get; set; }
        public Lookup genderId { get; set; }
       
        public DateTime birthDate { get; set; }
       
        public string password { get; set; }


    }
}
