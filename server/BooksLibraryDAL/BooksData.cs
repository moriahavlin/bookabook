using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryDAL
{
    public class BooksData
    {
        public static void PromoteNumberOfViewers(int idBook)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    BooksTable bt = db.BooksTable.Where(b => b.id == idBook).FirstOrDefault();
                    if (bt != null)
                    {
                        bt.numberOfViewers++;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {


            }

        }
        public static List<BooksTable> getAllBooks()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    List<BooksTable> res = db.BooksTable.Where(b => b.isDeleted != true).ToList();
                    return res;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<BooksTable> Serch(string nameBookToSearch, int? categoryIdSearch, string autherToSerch, string publishingToSerch,int? cityCode)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    List<BooksTable> res = db.BooksTable.ToList();
                    if (nameBookToSearch != null)
                        res = res.Where(x => x.BooksNameTable.name.Contains(nameBookToSearch)).ToList();
                    if (categoryIdSearch != 0 && categoryIdSearch != null)
                        res = res.Where(x => x.categoryId == categoryIdSearch).ToList();
                    if (publishingToSerch != null)
                        res = res.Where(x => x.PublishingTable.name.Contains(publishingToSerch)).ToList();
                    if (autherToSerch != null)
                        res = res.Where(x => x.AutherTable.name.Contains(autherToSerch)).ToList();
                    if (cityCode != null)
                        res = res.Where(x => x.UsersTable.CityTable.id == cityCode).ToList();
                    res = res.Where(b => b.isDeleted != true).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<BooksTable> getAllUsersBooksByIDU(int id)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    return db.BooksTable.Where(book => book.lenderId == id).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static BooksTable getBookById(int id)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    return db.BooksTable.Where(book => book.id == id)

                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static BooksTable getBookAndOtherDetailesToStatisckById(int id)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    return db.BooksTable.Where(book => book.id == id)
                        .Include(x => x.RatingTable)
                        .Include(x => x.LendsTable)
                        .Include(x => x.UsersTable)
                        .Include(x => x.MyBasketOfBooks)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int[] numWomenRead(int idBook)
        {

            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    BooksTable bt = db.BooksTable.Where(b => b.id == idBook).FirstOrDefault();
                    if (bt != null)
                    {
                        int numWomen = bt.LendsTable.Where(l => (l.statusCode == 2 || l.statusCode == 4) && l.UsersTable.genderId == 1).Count();
                        int numMen = bt.LendsTable.Where(l => (l.statusCode == 2 || l.statusCode == 4) && l.UsersTable.genderId == 2).Count();
                        int[] WomenAndMen = { numWomen, numMen };
                        return WomenAndMen;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int InsertBook(BooksTable b)
        {
            int idB;
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    db.BooksTable.Add(b);
                    //????????????????????????????????????????????????????  
                    db.SaveChanges();
                    idB = b.id;
                }
                return idB;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static void saveImageName(int idBook,string bookName)
        {
            
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                   BooksTable book= db.BooksTable.Where(b => b.id == idBook).FirstOrDefault();
                    if (book!=null)
                    {
                        book.picNAme = bookName;
                        db.SaveChanges();
                    }              
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static void addBookToMybasket(MyBasketOfBooks bas)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    db.MyBasketOfBooks.Add(bas);
                    //????????????????????????????????????????????????????  
                    db.SaveChanges();

                }
                return;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static void editBook(BooksTable book)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    BooksTable booktable = db.BooksTable.Where(x => x.id == book.id).FirstOrDefault();

                    if (booktable != null)
                    {
                        if (book.autherId != 0)
                            booktable.autherId = book.autherId;
                        else
                        {
                            //  db.PublishingTable.
                            booktable.AutherTable = new AutherTable() { name = book.AutherTable.name };
                        }
                        if (book.nameId != 0)
                            booktable.nameId = book.nameId;
                        else
                        {
                            //  db.PublishingTable.
                            booktable.BooksNameTable = new BooksNameTable() { name = book.BooksNameTable.name };
                        }


                        booktable.categoryId = book.categoryId;
                        booktable.description = book.description;
                        booktable.numOfPages = book.numOfPages;
                        if (book.publishingId != 0)
                            booktable.publishingId = book.publishingId;
                        else
                        {
                            booktable.PublishingTable = new PublishingTable() { name = book.PublishingTable.name };
                        }
                    }
                    //????????????????????????????????????????????????????  
                    db.SaveChanges();
                }
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<BooksTable> getAllMyBasket(int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    var mybasket = db.MyBasketOfBooks.Where(x => x.idUser == idU).ToList();
                    List<BooksTable> books = new List<BooksTable>();

                    foreach (var item in mybasket)
                    {
                        BooksTable x = db.BooksTable.Where(b => b.id == item.idBook).FirstOrDefault();
                        if (x != null)
                            books.Add(x);
                    }
                    return books.Where(b => b.isDeleted != true).ToList();


                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static void deleteBookFromMyBuskate(int idB, int idU)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {


                    List<MyBasketOfBooks> busketList = db.MyBasketOfBooks.Where(x => x.idUser == idU).ToList();
                    if (busketList.Count > 0)
                    {
                        MyBasketOfBooks busketBookToRemove = busketList.Where(x => x.idBook == idB).FirstOrDefault();
                        if (busketBookToRemove != null)
                        {
                            db.MyBasketOfBooks.Remove(busketBookToRemove);
                            db.SaveChanges();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<RatingTable> GetAllRatingByIdBook(int idBook)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    return db.RatingTable.Where(b => b.bookId == idBook).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool RateBook(RatingTable rating)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    db.RatingTable.Add(rating);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int rateBook(RatingTable b)
        {
            int idB;
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    db.RatingTable.Add(b);
                    //????????????????????????????????????????????????????  
                    db.SaveChanges();
                    idB = b.id;
                }
                return idB;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void deleteBook(int idB)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    BooksTable b = db.BooksTable.Where(book => book.id == idB).First();
                    b.isDeleted = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
