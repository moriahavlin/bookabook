using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryDAL
{
    public class LenderData
    {
        public static bool checkIfBookIsBorrow(int idB)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    BooksTable booktable = db.BooksTable.Where(x => x.id == idB).FirstOrDefault();
                    if (booktable != null)
                    {
                        if (booktable.isBorrowed == true)
                            return true;
                    }
                }
            }
            catch
            {
            }
            return false;

        }
        public static void removeWaitingToBookByIdWaiting(waitingForAbookTable watingToboook)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    db.waitingForAbookTable.Remove(watingToboook);
                }
            }
            catch (Exception ex)
            {

               // throw ex;
            }

        }
       public static List<waitingForAbookTable> GetAllWaitingForBook(int idB)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    return db.waitingForAbookTable.Where(x => x.bookCode == idB).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
      
        public static bool borrowBook(LendsTable lend)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    db.LendsTable.Add(lend);
                    BooksTable b = db.BooksTable.Where(x => x.id == lend.bookId).FirstOrDefault();
                    b.isBorrowed = true;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
 public static LendsTable getLendsByIdBorrow(int idBorrow)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    return db.LendsTable.Where(l => l.id == idBorrow).FirstOrDefault();
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }

        public static bool updateWhenBookIsAvailable(waitingForAbookTable userWait)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    if (db.waitingForAbookTable.Any(w => w.bookCode == userWait.bookCode && w.userCode == userWait.userCode) == false)
                    {
                        db.waitingForAbookTable.Add(userWait);

                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
        public static bool confirmBorrow( int idBorrow)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    LendsTable lend = db.LendsTable.Where(l => l.id == idBorrow).FirstOrDefault();
                    if(lend!=null)
                    {
                        if (lend.statusCode == 1)
                        {
                            lend.statusCode = 2;
                            lend.date = DateTime.Now;
                            db.SaveChanges();
                            return true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
        public static bool rejectBorrow( int idBorrow)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    LendsTable lend = db.LendsTable.Where(l => l.id == idBorrow).FirstOrDefault();
                    if(lend!=null)
                    {
                        if (lend.statusCode == 1)
                        {
                            lend.statusCode = 3;
                            BooksTable bt = db.BooksTable.Where(b => b.id == lend.bookId).FirstOrDefault();
                            if (bt != null)
                                bt.isBorrowed = false;
                            db.SaveChanges();
                            return true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
        public static List<LendsTable> getAllWaitingForApprovalByUser(int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    List<LendsTable> lt = db.LendsTable.Where(l => l.statusCode == 1 && l.BooksTable.lenderId == idU).ToList();
                    //List<BooksTable> listBt = new List<BooksTable>();
                    //foreach (var item in lt)
                    //{
                    //    BooksTable b = db.BooksTable.Where(x => x.id == item.bookId).FirstOrDefault();
                    //    if (b != null)
                    //    {
                    //        listBt.Add(b);
                    //    }
                    //}
                    return lt;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<LendsTable> getBooksUserWantedToBorrow(int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    List<LendsTable> lt = db.LendsTable.Where(l => l.statusCode == 1 && l.borrowerId == idU).ToList();
                    //List<BooksTable> listBt = new List<BooksTable>();
                    //foreach (var item in lt)
                    //{
                    //    BooksTable b = db.BooksTable.Where(x => x.id == item.bookId).FirstOrDefault();
                    //    if (b != null)
                    //    {
                    //        listBt.Add(b);
                    //    }
                    //}
                    return lt;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //mv 05-03-2019
        //הספרים שאני משאיל אותם - נמצאים אצלי
        public static List<LendsTable> getBorrowedBookByUser(int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    //יחזיר רק את הספרים שכרגע מושאלים
                    //mv 05-03-2019
                    List<LendsTable> lt = db.LendsTable.Where(l => (l.statusCode == 2) && l.borrowerId == idU).ToList();
                    return lt;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //הספרים שלי שמושאלים אצל אנשים אחרים
        public static List<LendsTable> getAllmyBooksThatBorrowed(int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    List<LendsTable> lt = db.LendsTable.Where(l => (l.statusCode == 2) && l.BooksTable.lenderId==idU ).ToList();
                    return lt;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //mv 05-03-2019
        public static bool returnBook(int idLender)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    LendsTable lend = db.LendsTable.Where(l => l.id == idLender).FirstOrDefault();
                    if (lend != null)
                    {
                        if (lend.statusCode == 2)
                        {
                            lend.BooksTable.isBorrowed = false;
                            lend.statusCode = 4;
                            lend.date = DateTime.Now;
                            db.SaveChanges();
                            return true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
        public static List<LendsTable> getBooksUserDidntReturn(int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    DateTime dt = DateTime.Today.AddDays(-60);
                    List<LendsTable> lt = db.LendsTable.Where(l => l.statusCode == 2 && l.borrowerId == idU && dt > l.date).ToList();
                    return lt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<LendsTable> getMyBooksNeedToBeReturned(int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    DateTime dt = DateTime.Today.AddDays(-60);
                    List<LendsTable> lt = db.LendsTable.Where(l => (l.statusCode == 2) && l.BooksTable.lenderId == idU && dt > l.date).ToList();
                    return lt;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
