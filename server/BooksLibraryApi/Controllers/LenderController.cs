using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BooksLibraryBL;
using BooksLibraryModels;

namespace BooksLibraryApi.Controllers
{
    public class LenderController : ApiController
    {
        [HttpGet]
        public bool borrowBook(int idB, int idU)
        {
            // return
            return LenderLogic.borrowBook(idB, idU);
        }
        //עדכן אותי כשהספר מתפנה...
        [HttpGet]
        public void updateWhenBookIsAvailable(int idB, int idU)
        {
            LenderLogic.updateWhenBookIsAvailable(idB, idU);
        }
        //אשר השאלה - שלח למבקש את הפרטים שלי
        [HttpGet]
        public bool confirmBorrow(int idBorrow)
        {
          return  LenderLogic.confirmBorrow(idBorrow);
        }
        [HttpGet]
        //אני לא מאשר את ההשאלה 
        public bool rejectBorrow(int idBorrow)
        {
          return  LenderLogic.rejectBorrow(idBorrow);
        }
        //כל הספרים שממתינים שאישורי
        [HttpGet]
        public List<Lends> getAllWaitingForApprovalByUser(int idU)
        {
            
            return LenderLogic.getAllWaitingForApprovalByUser(idU);
        }
        //כל הספרים שאני מחכה שיאשרו לי את ההשאלה
        [HttpGet]
        public List<Lends> getBooksUserWantedToBorrow(int idU)
        {
            return LenderLogic.getBooksUserWantedToBorrow(idU);
        }

        //ספרים שאני שואל אותם - הם אצלי 
        [HttpGet]
        public List<Lends> getBorrowedBookByUser(int idU)
        {
            return LenderLogic.getBorrowedBookByUser(idU);
        }

   [HttpGet]
        public List<Lends> getAllmyBooksThatBorrowed(int idU)
        {
            return LenderLogic.getAllmyBooksThatBorrowed(idU);
        }
        //mv 05-03-2019
        [HttpGet]
        public bool returnBook(int idLender)
        {
            return LenderLogic.returnBook(idLender);
        }

        [HttpGet]
        public List<Lends> getBooksUserDidntReturn(int idU)
        {
            return LenderLogic.getBooksUserDidntReturn(idU);
        }

        [HttpGet]
        public List<Lends> getMyBooksNeedToBeReturned(int idU)
        {
            return LenderLogic.getMyBooksNeedToBeReturned(idU);
        }
    }
}
