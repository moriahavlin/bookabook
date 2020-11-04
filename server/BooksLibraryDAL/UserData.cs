using BooksLibraryDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryDAL
{
    public class UserData
    {
        public static UsersTable getUserById(int id)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    return db.UsersTable.Where(user => user.id == id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<UsersTable> getAllUsers()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    return db.UsersTable.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static UsersTable getUserByEmail(string email)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    return db.UsersTable.Where(user => user.email == email).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static UsersTable InsertUser(UsersTable newUser)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    db.UsersTable.Add(newUser);
                    db.SaveChanges();
                    return newUser;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string deleteUser(int uId)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    db.UsersTable.Remove(db.UsersTable.Find(uId));
                    db.SaveChanges();
                }
                return ("ok");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //?????????????????????????
        public static string updateUser(UsersTable u)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {

                    UsersTable u1 = db.UsersTable.Where(i => i.id == u.id).First();
                    u1.birthDate = u.birthDate;
                    db.SaveChanges();



                }
                return ("ok");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string EditUser(UsersTable newUser)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    UsersTable u = db.UsersTable.Where(x => x.id == newUser.id).FirstOrDefault();
                    if (u != null)
                    {
                        u.firstName = newUser.firstName;
                        u.lastName = newUser.lastName;
                        u.email = newUser.email;
                        u.phone = newUser.phone;
                        u.neighborhood = newUser.neighborhood;
                        u.address = newUser.address;
                        u.houseNumber = newUser.houseNumber;
                        if(newUser.cityCode!=0)
                        u.cityCode = newUser.cityCode;
                        else
                        {
                            u.CityTable = new CityTable() { name = newUser.CityTable.name };
                        }
                        u.birthDate = newUser.birthDate;
                        u.password = newUser.password;
                        u.genderId = newUser.genderId;
                    }
                    db.SaveChanges();
                }
                return ("ok");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool isMailExist(string email)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {            
                   return  db.UsersTable.Any(x => x.email == email);     
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int isExist(string email, string password)
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    //0=לא קיים
                    //-1=לא תקין
                    bool exist = db.UsersTable.Any(x => x.email == email);
                    if (exist == false)
                        return 0;
                   UsersTable u = db.UsersTable.Where(x => x.email == email && x.password == password).FirstOrDefault();
                    if (u == null)
                        return -1;
                    return u.id;
                   
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      

    }
}
