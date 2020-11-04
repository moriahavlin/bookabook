using BooksLibraryBL;
using BooksLibraryModels;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace BooksLibraryApi.Controllers
{

    public class BookController : ApiController
    {
        //[HttpGet]
        //public Books GetBookByName(string name)
        //{
        //    return new Books() { description = "הפתעה" };
        //}
     //   [HttpGet]
        //public string teststring()
        //{
        //    return "הפתעה";
        //}
        [HttpGet]
        public List<BookToList> GetAllBook()
        {
            return BooksLogic.GetAllBook();
        }
        [HttpGet]

        public List<BookToList> Serch(string name,int? categoryIdSearch,string autherToSerch, string publishingToSerch,int? cityCode)
        {
            return BooksLogic.Serch(name, categoryIdSearch, autherToSerch, publishingToSerch,cityCode);

        }
        [HttpGet]
        public Books GetBookById(int idBook)
        {
            return BooksLogic.getBookAndOtherDetailesToStatisckById(idBook);
        }
        [HttpPost]
        public int InsertBook(Books newBook)
        {
            return BooksLogic.InsertBook(newBook);
        }
        //public int InsertBook()
        //{
        //    var httprequste = HttpContext.Current.Request;
        //    var file = httprequste.Files["image"];
        //    var newBook = httprequste.Files["book"];
        //    return BooksLogic.InsertBook( newBook);
        //}
       // [HttpGet]
        //public int test()
        //{
        //    Books newBook = new Books()
        //    {
        //        autherId = LookupBL.getLookupByCode(Constants.AutherTableName, 1),
        //        categoryId = LookupBL.getLookupByCode(Constants.BooksCategoryTableName, 1),
        //        description = "ספר מותח עם עלילה מענינת ",
        //        isBorrowed = false,
        //        publishingId = LookupBL.getLookupByCode(Constants.PuplishingTableName, 1),
        //        lenderId = 1,
        //        numOfPages = 100,
        //        nameId = LookupBL.getBookName(1)

        //    };
        //    return BooksLogic.InsertBook(newBook);
        //}
        [HttpPost]
        public void addBookToMybasket(MyBasketOfBooksModels obj)
        {
            BooksLogic.addBookToMybasket(obj);
            return;
        }
        public List<Books> getAllMyBasket(int idU)
        {

            return BooksLogic.getAllMyBasket(idU);
        }

        public List<Books> getAllUsersBooksByIDU(int idu)
        {
            return BooksLogic.getAllUsersBooksByIDU(idu);
        }
        [HttpGet]
        public void deleteBookFromMyBuskate(int idB,int idU)
        {
             BooksLogic.deleteBookFromMyBuskate(idB,idU);
        }
        [HttpPost]
        public void editBook(Books b)
        {
            BooksLogic.editBook(b);
            return;
        }
        [HttpGet]
        public void PromoteNumberOfViewers(int idBook)
        {
            BooksLogic.PromoteNumberOfViewers(idBook);
            return;
        }
        [HttpGet]
        //לא משתמשים בזה !!!
        public List<Rating> GetAllRatingByIdBook(int idBook)
        {
            return BooksLibraryBL.BooksLogic.GetAllRatingByIdBook(idBook);
        }
        [HttpGet]
        
        //בזה כן משתמשים
        public Statistics GetAllStatisticsToBook(int IdB)
        {
           return BooksLogic.Fullstatistics(IdB);
        }
       

        [HttpPost]
        public IHttpActionResult UploadPic(int bookId)
        {
            string imageName = "picBook" + bookId + ".jpg";
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/"+ imageName);
                    postedFile.SaveAs(filePath);
                }
            }
            BooksLogic.saveImageName(bookId, imageName);
            return Ok(1);
        }
        [HttpGet]
        public void rateBook(int idB, string desc, int rate)
        {
            BooksLogic.rateBook(idB, desc, rate);
        }

        [HttpGet]
        public void deleteBook(int idB)
        {
            BooksLogic.deleteBook(idB);
        }
    }
}
