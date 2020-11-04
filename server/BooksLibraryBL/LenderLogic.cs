using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksLibraryDAL;
using BooksLibraryModels;
namespace BooksLibraryBL
{
    public class LenderLogic
    {
        public static bool borrowBook(int idB, int idU)
        {
            //idU-השואל
            if (BooksLibraryDAL.LenderData.checkIfBookIsBorrow(idB) == false)
            {
                LendsTable lend = new LendsTable();
                lend.bookId = idB;
                lend.bookIsActive = false;
                lend.borrowerId = idU;
                lend.date = DateTime.Now;
                lend.statusCode = LookupBL.getLookupByName(Constants.lendsStatusTableName, "ממתין לאישור").Code;
                if (BooksLibraryDAL.LenderData.borrowBook(lend) == true)
                {//הספר
                    BooksTable bt = BooksData.getBookById(idB);
                    //המשאיל
                    UsersTable utL = UserData.getUserById(bt.lenderId);
                    //השואל
                    UsersTable ut = UserData.getUserById(lend.borrowerId);
                    string bookName = "";

                    if (bt != null)
                    {
                        bookName = LookupBL.getLookupByCode(Constants.BooksNameTableName, bt.nameId).Desc;
                    }
                    string message = "<br/><br/>סיפרך ";
                    message += bookName + " ";
                    message += "ממתין לאישור  על ידי  ";
                    if (ut != null)
                    {
                        message += ut.firstName + " " + ut.lastName + ", " + "טלפון ליצירת קשר:" + ut.phone + "<br/><br/>";
                    }
                    message += "בכדי לאשר את ההשאלה יש להיכנס לאתר ולאשר , המבקש יצור עמך קשר ";
                    //   message += "http://localhost:56996/api/Book/testString";
                    // message += "<br/>  <button> <a href=" + "http://localhost:56996/api/Book/testString" + "> אשר</a> </button>";
                    //נשלח מייל למשאיל
                    sendEmailFunc.sendEmailAsync(utL.email, utL.firstName + " " + utL.lastName, message, "בקשה להשאלת ספר " + bookName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static void updateWhenBookIsAvailable(int idB, int idU)
        {
            waitingForAbookTable userWait = new waitingForAbookTable();
            userWait.bookCode = idB;
            userWait.userCode = idU;
            LenderData.updateWhenBookIsAvailable(userWait);
        }
        public static bool confirmBorrow(int idBorrow)
        {

            bool res = LenderData.confirmBorrow(idBorrow);
            if (res == true)
            {
                LendsTable lendTable = LenderData.getLendsByIdBorrow(idBorrow);
                //השואל
                UsersTable borrow = UserData.getUserById(lendTable.borrowerId);
                BooksTable bt = BooksData.getBookById(lendTable.bookId);
                //המשאיל
                UsersTable ut = UserData.getUserById(bt.lenderId);
                string bookName = LookupBL.getLookupByCode(Constants.BooksNameTableName, bt.nameId).Desc;
                string message = "הספר " + bookName + " שביקשת אושר על ידי בעל הספר   : ";
                message += " פרטי בעל הספר ליצירת קשר  : ";
                message += ut.firstName + " " + ut.lastName + ", ";
                message += "טלפון: " + ut.phone + " מייל " + ut.email;
                message += "<br/>  מיקום הספר: רחוב " + ut.address + " " + ut.houseNumber + " " + ut.neighborhood + " " + 
                    LookupBL.getLookupByCode(Constants.CitiesTableName, ut.cityCode).Desc;
                //פניה לפונקציה ששולחת מייל על אישור השאלת הספר
                sendEmailFunc.sendEmailAsync(borrow.email, borrow.firstName +
                    " " + borrow.lastName, message, "אישור השאלת ספר " + bookName);

            }
            return false;
        }
        public static bool rejectBorrow(int idBorrow)
        {
            bool res = LenderData.rejectBorrow(idBorrow);
            if (res == true)
            {
                LendsTable lendTable = LenderData.getLendsByIdBorrow(idBorrow);
                //השואל
                UsersTable borrow = UserData.getUserById(lendTable.borrowerId);
                BooksTable bt = BooksData.getBookById(lendTable.bookId);
                //המשאיל
                //   UsersTable ut = bt.UsersTable;
                string bookName = LookupBL.getLookupByCode(Constants.BooksNameTableName, bt.nameId).Desc;
                string message = "מצטערים הספר " + bookName + " שביקשת  לא אושר על ידי בעל הספר באפשרותך לחפש ספר אחר במאגר הספרים שלנו  : ";
                //הצליח לשנות לאשר


                sendEmailFunc.sendEmailAsync(borrow.email, borrow.firstName + " " + borrow.lastName, message, "הודעה בדבר בקשה להשאלת ספר");

            }
            return false;
        }
        public static List<Lends> getAllWaitingForApprovalByUser(int idU)
        {
            //List<Books> lb = new List<Books>();
            //הרשימה שצריך להציג , היא צריכה לעבור המרה למודל שיוחזר
            List<LendsTable> lenderTableList = LenderData.getAllWaitingForApprovalByUser(idU);            //מה שיוחזר
            //מה שיוחזר
            List<Lends> lenderist = new List<Lends>();
            foreach (var item in lenderTableList)
            {
                //יש למלאות את פרטי היוזר שמבקש להשאיל ואת פרטי הספר  כדי שיהיה יותר נוח להציג בצד לקוח
                Lends lend = castLenderTableTOLender(item);
                if (lend != null)
                {
                    lenderist.Add(lend);
                }
            }

            //מה שהיה לפני זה .
            //foreach (var dbBook in )
            //{
            //    Books book = new Books()
            //    {
            //        picNAme = dbBook.picNAme,
            //        autherId = LookupBL.getLookupByCode(Constants.AutherTableName, dbBook.autherId),
            //        categoryId = LookupBL.getLookupByCode(Constants.BooksCategoryTableName, dbBook.categoryId),
            //        id = dbBook.id,
            //        description = dbBook.description,
            //        numOfPages = dbBook.numOfPages,
            //        isBorrowed = dbBook.isBorrowed,
            //        lenderId = dbBook.lenderId,
            //        nameId = LookupBL.getBookName(dbBook.nameId),
            //        publishingId = LookupBL.getLookupByCode(Constants.PuplishingTableName, dbBook.publishingId)
            //    };
            //    lb.Add(book); 
            return lenderist;
        }


        public static Lends castLenderTableTOLender(LendsTable lendsTable)
        {
            try
            {
                Lends lend = new Lends();
                lend.book = BooksLogic.getBookById(lendsTable.bookId);
                lend.bookIsActive = lendsTable.bookIsActive;
                lend.borrower = UserLogic.getUserDetailsById(lendsTable.borrowerId);
                lend.date = lendsTable.date;
                lend.id = lendsTable.id;
                lend.statusCode = lendsTable.statusCode;
                if (lend.book != null)
                    lend.lender = UserLogic.getUserDetailsById(lend.book.lenderId);
                return lend;
            }
            catch (Exception)
            {

                throw;
            }
            return null;

        }
        public static List<Lends> getBooksUserWantedToBorrow(int idU)
        {
            List<LendsTable> LenderTableList = LenderData.getBooksUserWantedToBorrow(idU);
            List<Lends> lenderist = new List<Lends>();
            foreach (var item in LenderTableList)
            {
                //יש למלאות את פרטי היוזר שמבקש להשאיל ואת פרטי הספר  כדי שיהיה יותר נוח להציג בצד לקוח
                Lends lend = castLenderTableTOLender(item);
                if (lend != null)
                {
                    lenderist.Add(lend);
                }
            }


            return lenderist;
        }
        public static List<Lends> getAllmyBooksThatBorrowed(int idU)
        {
            List<LendsTable> lenderTableList = LenderData.getAllmyBooksThatBorrowed(idU);
            List<Lends> lenderist = new List<Lends>();
            foreach (var item in lenderTableList)
            {
                //יש למלאות את פרטי היוזר שמבקש להשאיל ואת פרטי הספר  כדי שיהיה יותר נוח להציג בצד לקוח
                Lends lend = castLenderTableTOLender(item);
                if (lend != null)
                {
                    lenderist.Add(lend);
                }
            }
            return lenderist;//מה שיוחזר
        }
        //מה שמושאל על ידי - נמצא אצלי
        public static List<Lends> getBorrowedBookByUser(int idU)
        {
            //List<Books> lb = new List<Books>();
            //הרשימה שצריך להציג , היא צריכה לעבור המרה למודל שיוחזר
            List<LendsTable> lenderTableList = LenderData.getBorrowedBookByUser(idU);            //מה שיוחזר
            //מה שיוחזר
            List<Lends> lenderist = new List<Lends>();
            foreach (var item in lenderTableList)
            {
                //יש למלאות את פרטי היוזר שמבקש להשאיל ואת פרטי הספר  כדי שיהיה יותר נוח להציג בצד לקוח
                Lends lend = castLenderTableTOLender(item);
                if (lend != null)
                {
                    lenderist.Add(lend);
                }
            }
            return lenderist;
        }
        //mv 05-03-2019
        public static bool returnBook(int idLender)
        {

            bool res = LenderData.returnBook(idLender);
            if (res == true)
            {
                LendsTable lending = LenderData.getLendsByIdBorrow(idLender);
                List<waitingForAbookTable> listWaiting = new List<waitingForAbookTable>();
                listWaiting = LenderData.GetAllWaitingForBook(lending.bookId);
                if (listWaiting.Count > 0)
                {


                    foreach (var item in listWaiting)
                    {
                        try
                        {
                            UsersTable u = UserData.getUserById(item.userCode);
                            if (u != null)
                            {
                                string messageTo = "";

                                string bookNameToMail = LookupBL.getLookupByCode(Constants.BooksNameTableName, BooksData.getBookById(lending.bookId).nameId).Desc;
                                messageTo += " הספר " + bookNameToMail + " הפך להיות זמין במערכת הנך יכול להיכנס לאתר ולשאול את הספר ";
                                sendEmailFunc.sendEmailAsync(u.email, u.firstName + " " + u.lastName, messageTo, "הודעת מערכת");
                                LenderData.removeWaitingToBookByIdWaiting(item);
                            }
                        }
                        catch (Exception)
                        {

                            return false;
                        }

                    }


                }


                /*
                LendsTable lendTable = LenderData.getLendsByIdBorrow(lending.bookId);
                //השואל
                UsersTable borrow = UserData.getUserById(lendTable.borrowerId);//השואל
                BooksTable bt = BooksData.getBookById(lendTable.bookId);
                //המשאיל
                UsersTable ut = UserData.getUserById(bt.lenderId);// המשאיל
                string bookName = LookupBL.getLookupByCode(Constants.BooksNameTableName, bt.nameId).Desc;
                string message = "התקבלה בקשה להחזרת הספר " + bookName;
                message += " פרטי בעל הספר ליצירת קשר  : ";
                message += ut.firstName + " " + ut.lastName + ", ";
                message += "טלפון: " + ut.phone + " מייל " + ut.email;
                //הצליח לשנות לאשר
                //לשוח מייל לשואל עם הפרטים 
                // sendEmailFunc.sendEmailAsync(borrow.email, borrow.firstName + " " + borrow.lastName, message, "אישור החזרת ספר " + bookName);

                //שליחת מייל לבעלי הספר שרוצים להחזיר את הספר שלו
                message = "התקבלה בקשה להחזרת הספר " + bookName;
                message += " פרטי השואל ליצירת קשר  : ";
                message += borrow.firstName + " " + borrow.lastName + ", ";
                message += "טלפון: " + borrow.phone + " מייל " + borrow.email;
             
    //   sendEmailFunc.sendEmailAsync(ut.email, ut.firstName + " " + ut.lastName, message, "אישור החזרת ספר " + bookName);
          */
            }
            return res;
        }

        //   public static bool sendEmail
        public static List<Lends> getBooksUserDidntReturn(int idU)
        {
            List<LendsTable> lenderTableList = LenderData.getBooksUserDidntReturn(idU);
            List<Lends> lenderist = new List<Lends>();
            foreach (var item in lenderTableList)
            {
                Lends lend = castLenderTableTOLender(item);
                if (lend != null)
                {
                    lenderist.Add(lend);
                }
            }
            return lenderist;
        }

        public static List<Lends> getMyBooksNeedToBeReturned(int idU)
        {
            List<LendsTable> lenderTableList = LenderData.getMyBooksNeedToBeReturned(idU);
            List<Lends> lenderist = new List<Lends>();
            foreach (var item in lenderTableList)
            {
                Lends lend = castLenderTableTOLender(item);
                if (lend != null)
                {
                    lenderist.Add(lend);
                }
            }
            return lenderist;
        }
    }


}