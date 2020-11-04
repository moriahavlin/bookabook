using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksLibraryModels;
using BooksLibraryDAL;
using System.Data;
using System.Web;

namespace BooksLibraryBL
{
    public class BooksLogic
    {
        public static Books getBookAndOtherDetailesToStatisckById(int id)
        {
            BooksTable dbBook = BooksData.getBookAndOtherDetailesToStatisckById(id);

            //פונקצית המרת הנתונים למודל שיוכר בצד לקוח
            Books bookModel = CastBookTableToBook(dbBook);
            if (bookModel != null)
            {
                //שליפת הסטטיסטיקות של הספר
                //    bookModel.Statistics = Fullstatistics(dbBook);
                //שליפת הדרוגים והתגובות על הספר
                //  bookModel.ratingList = GetAllRatingByIdBook(dbBook.id);
                return bookModel;
            }
            return null;
        }
        public static Books getBookById(int id)
        {//שליפה -מחזיר אוביקט ממודל של אנטיטי
            BooksTable dbBook = BooksData.getBookById(id);
            //המרה מודל האנטיטי למודל משכבת BE 
            Books b = CastBookTableToBook(dbBook);
            if (b != null)
            {
                return b;
            }
            return null;
        }
        public static List<BookToList> GetAllBook()
        {

            List<BooksTable> booksDB = BooksData.getAllBooks();
            List<BookToList> resBooks = new List<BookToList>();
            foreach (var item in booksDB)
            {
                BookToList b = CastBookTableToBookLIST(item);
                if (b != null)
                {
                    resBooks.Add(b);
                }
            }
            return resBooks;

        }
        public static List<BookToList> Serch(string name, int? categoryIdSearch, string autherToSerch, string publishingToSerch,int? cityCode)
        {

            List<BooksTable> booksDB = BooksData.Serch(name, categoryIdSearch, autherToSerch, publishingToSerch, cityCode);
            List<BookToList> resBooks = new List<BookToList>();
            foreach (var item in booksDB)
            {
                BookToList b = CastBookTableToBookLIST(item);
                if (b != null)
                {
                    resBooks.Add(b);
                }
            }
            return resBooks;

        }
        public static int InsertBook(Books b)
        {
            BooksTable bt = new BooksTable();
            bt.numberOfViewers = 0;
            //אם הכנסת מחבר - 
            if (b.autherId.Code == 0)
            {
                //בדוק האם קיים כזה מחבר
                if (LookupBL.getLookupByName(Constants.AutherTableName, b.autherId.Desc) != null)
                {//אם כן חבר בקשרי גומלין
                    bt.autherId = LookupBL.getLookupByName(Constants.AutherTableName, b.autherId.Desc).Code;
                }
                else
                {//אם לא צור חדש
                    bt.AutherTable = new AutherTable() { name = b.autherId.Desc };
                }
            }
            else { bt.autherId = b.autherId.Code; }
            if (b.categoryId.Code == 0)
            {

                if (LookupBL.getLookupByName(Constants.BooksCategoryTableName, b.categoryId.Desc) != null)
                {
                    bt.categoryId = LookupBL.getLookupByName(Constants.BooksCategoryTableName, b.categoryId.Desc).Code;
                }
                else
                {
                    bt.BooksCategriesTable = new BooksCategriesTable() { name = b.categoryId.Desc };
                }
            }
            else bt.categoryId = b.categoryId.Code;
            bt.description = b.description;
            bt.isBorrowed = false;
            bt.lenderId = b.lenderId;
            if (b.nameId.Code == 0)
            {
                if (LookupBL.getLookupByName(Constants.BooksNameTableName, b.nameId.Desc) != null)
                {
                    bt.nameId = LookupBL.getLookupByName(Constants.BooksNameTableName, b.nameId.Desc).Code;
                }
                else
                {
                    bt.BooksNameTable = new BooksNameTable() { name = b.nameId.Desc };
                }
            }
            else bt.nameId = b.nameId.Code;

            bt.numOfPages = b.numOfPages;
            if (b.publishingId.Code == 0)
            {

                if (LookupBL.getLookupByName(Constants.PuplishingTableName, b.publishingId.Desc) != null)
                {
                    bt.publishingId = LookupBL.getLookupByName(Constants.PuplishingTableName, b.publishingId.Desc).Code;
                }
                else
                {
                    bt.PublishingTable = new PublishingTable() { name = b.publishingId.Desc };
                }
            }
            else bt.publishingId = b.publishingId.Code;
            int result = BooksData.InsertBook(bt);
            //var httpRequest = HttpContext.Current.Request;
            //if (httpRequest.Files.Count > 0)
            //{
            //    foreach (string file in httpRequest.Files)
            //    {
            //        var postedFile = httpRequest.Files[file];
            //        var filePath = HttpContext.Current.Server.MapPath("~/Images/picBook" + result);
            //        postedFile.SaveAs(filePath);
            //    }
            //}
            BooksLibraryDAL.LookupData.refreshLookups();
            return result;

        }
        public static void addBookToMybasket(MyBasketOfBooksModels obj)
        {
            MyBasketOfBooks bas = new MyBasketOfBooks()
            {
                idBook = obj.idBook,
                idUser = obj.idUser
            };
            BooksData.addBookToMybasket(bas);
            return;
        }
        public static void editBook(Books b)
        {
            BooksTable bt = new BooksTable();

            bt.id = b.id;
            if (b.autherId.Code == 0)
            {
                //בדוק האם קיים כזה מחבר
                if (LookupBL.getLookupByName(Constants.AutherTableName, b.autherId.Desc) != null)
                {//אם כן חבר בקשרי גומלין
                    bt.autherId = LookupBL.getLookupByName(Constants.AutherTableName, b.autherId.Desc).Code;
                }
                else
                {//אם לא צור חדש
                    bt.AutherTable = new AutherTable() { name = b.autherId.Desc };
                }
            }
            else { bt.autherId = b.autherId.Code; }
            if (b.categoryId.Code == 0)
            {

                if (LookupBL.getLookupByName(Constants.BooksCategoryTableName, b.categoryId.Desc) != null)
                {
                    bt.categoryId = LookupBL.getLookupByName(Constants.BooksCategoryTableName, b.categoryId.Desc).Code;
                }
                else
                {
                    bt.BooksCategriesTable = new BooksCategriesTable() { name = b.categoryId.Desc };
                }
            }
            else bt.categoryId = b.categoryId.Code;
            
            bt.description = b.description;
            bt.isBorrowed = false;
            bt.lenderId = 1;
            if (b.nameId.Code == 0)
            {
                if (LookupBL.getLookupByName(Constants.BooksNameTableName, b.nameId.Desc) != null)
                {
                    bt.nameId = LookupBL.getLookupByName(Constants.BooksNameTableName, b.nameId.Desc).Code;
                }
                else
                {
                    bt.BooksNameTable = new BooksNameTable() { name = b.nameId.Desc };
                }
            }
            else bt.nameId = b.nameId.Code;

            bt.numOfPages = b.numOfPages;
            if (b.publishingId.Code == 0)
            {

                if (LookupBL.getLookupByName(Constants.PuplishingTableName, b.publishingId.Desc) != null)
                {
                    bt.publishingId = LookupBL.getLookupByName(Constants.PuplishingTableName, b.publishingId.Desc).Code;
                }
                else
                {
                    bt.PublishingTable = new PublishingTable() { name = b.publishingId.Desc };
                }
            }
            else bt.publishingId = b.publishingId.Code;


            BooksData.editBook(bt);
            BooksLibraryDAL.LookupData.refreshLookups();
            return;
        }

        public static List<Books> getAllMyBasket(int idU)
        {

            List<BooksTable> MyBookTableList = BooksData.getAllMyBasket(idU);
            List<Books> MyBooksList = new List<Books>();
            foreach (var item in MyBookTableList)
            {
                Books b = CastBookTableToBook(item);
                if (b != null)
                {
                    MyBooksList.Add(b);
                }
            }
            return MyBooksList;
        }
        public static void saveImageName(int idBook, string nameBook)
        {
            BooksData.saveImageName(idBook, nameBook);

        }
        public static List<Books> getAllUsersBooksByIDU(int idu)
        {
            List<BooksTable> MyBookTableList = BooksData.getAllUsersBooksByIDU(idu);
            List<Books> MyBooksList = new List<Books>();
            if (MyBookTableList != null)
            {
                foreach (var item in MyBookTableList)
                {
                    Books b = CastBookTableToBook(item);
                    if (b != null)
                    {
                        MyBooksList.Add(b);
                    }
                }
                return MyBooksList;
            }
            return null;
        }
        public static void deleteBookFromMyBuskate(int idB, int idU)
        {
            BooksData.deleteBookFromMyBuskate(idB, idU);
        }

        public static Books CastBookTableToBook(BooksTable bookTable)
        {
            try
            {
                Books book = new Books()
                {
                    numberOfViewers = bookTable.numberOfViewers,
                    picNAme = bookTable.picNAme,
                    //שליפת פרטי מחבר לפי קוד מחבר
                    autherId = LookupBL.getLookupByCode(Constants.AutherTableName, bookTable.autherId),
                    //שליפת פרטי קטגורית ספר לפי קוד קטגוריה
                    categoryId = LookupBL.getLookupByCode(Constants.BooksCategoryTableName, bookTable.categoryId),
                    id = bookTable.id,
                    description = bookTable.description,
                    numOfPages = bookTable.numOfPages,
                    isBorrowed = bookTable.isBorrowed,
                    lenderId = bookTable.lenderId,
                    //שליפת פרטי שם ספר לפי קוד שם
                    nameId = LookupBL.getLookupByCode(Constants.BooksNameTableName, bookTable.nameId),
                    //שליפת פרטי מוציא לאור לפי קוד מוציא לאור 
                    publishingId = LookupBL.getLookupByCode(Constants.PuplishingTableName, bookTable.publishingId)
                    // city=LookupBL.getLookupByCode(Constants.CitiesTableName,bookTable.UsersTable.cityCode)

                };
                UsersTable lender = UserData.getUserById(bookTable.lenderId);
                book.city = LookupBL.getLookupByCode(Constants.CitiesTableName, lender.cityCode);
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static BookToList CastBookTableToBookLIST(BooksTable bookTable)
        {
            try
            {
                BookToList book = new BookToList()
                {
                    numberOfViewers = bookTable.numberOfViewers,
                    picNAme = bookTable.picNAme,
                    autherId = LookupBL.getLookupByCode(Constants.AutherTableName, bookTable.autherId),
                    //   categoryId = LookupBL.getLookupByCode(Constants.BooksCategoryTableName, bookTable.categoryId),
                    id = bookTable.id,
                    // description = bookTable.description,
                    //  numOfPages = bookTable.numOfPages,
                    isBorrowed = bookTable.isBorrowed,
                    // lenderId = bookTable.lenderId,
                    nameId = LookupBL.getLookupByCode(Constants.BooksNameTableName, bookTable.nameId),
                    //  publishingId = LookupBL.getLookupByCode(Constants.PuplishingTableName, bookTable.publishingId)
                };
                return book;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static void PromoteNumberOfViewers(int idBook)
        {
            BooksData.PromoteNumberOfViewers(idBook);
        }

        public static List<Rating> GetAllRatingByIdBook(int idBook)
        {
            List<Rating> listRating = new List<Rating>();

            List<RatingTable> listDataRating = BooksLibraryDAL.BooksData.GetAllRatingByIdBook(idBook);
            foreach (var item in listDataRating)
            {
                Rating rating = CastRaitingTableToRating(item);
                if (rating != null)
                    listRating.Add(rating);
            }
            return listRating;
        }
        public static Statistics Fullstatistics(int id)
        {
            BooksTable dbBook = BooksData.getBookAndOtherDetailesToStatisckById(id);
            Statistics statistic = new Statistics();
            statistic.Comments = new List<Comment>();
            foreach (var item in dbBook.RatingTable.OrderByDescending(r=>r.dateInsert).ToList())
            {
                if (item.desk != null)
                {
                    Comment c = new Comment();
                    c.comment = item.desk;
                    if (item.dateInsert != null)
                        c.date = (DateTime)item.dateInsert;

                    statistic.Comments.Add(c);
                }
            }
            statistic.numberOfViewers = dbBook.numberOfViewers;
            statistic.numberOfLendings = dbBook.LendsTable.Where(l => l.statusCode == 2 || l.statusCode == 4).Count();
            int[] WomenAndMen = BooksData.numWomenRead(dbBook.id);
            if (WomenAndMen != null)
            {
                statistic.numberOfwomen = WomenAndMen[0];
                statistic.numberOfMen = WomenAndMen[1];
            }
            statistic.numberOfLikeIt = dbBook.MyBasketOfBooks.Count();
            int count = 0, sum = 0;
            for (int i = 0; i < 5; i++)
            {
                statistic.rstingList[i] = dbBook.RatingTable.Where(r => r.rating == i + 1).Count();
                if (statistic.rstingList[i] != 0)
                {
                    count += statistic.rstingList[i];
                    sum += ((i + 1) * statistic.rstingList[i]);
                }
            }
            if (count != 0)
                statistic.averageRating = sum / count;
            return statistic;
        }
        public static Rating CastRaitingTableToRating(RatingTable ratingTable)
        {

            if (ratingTable != null)
            {
                Rating Rating = new Rating();
                Rating.bookId = ratingTable.bookId;
                Rating.borrowerId = ratingTable.borrowerId;
                Rating.desk = ratingTable.desk;
                Rating.id = ratingTable.id;
                Rating.rating = ratingTable.rating;
                //?????????
                //   Rating.kealYhad = ratingTable.rating;
                return Rating;

            }
            return null;

        }
        public static void rateBook(int idB, string desc, int rate)
        {
            Books b = new Books();
            b = getBookById(idB);
            RatingTable RateB = new RatingTable();
            RateB.bookId = b.id;
            //  RateB.borrowerId = b.lenderId;
            RateB.desk = desc;
            RateB.rating = rate;
            RateB.dateInsert = DateTime.Now;
            BooksData.rateBook(RateB);
        }


        public static void deleteBook(int idB)
        {
            BooksData.deleteBook(idB);
        }
    }

}
