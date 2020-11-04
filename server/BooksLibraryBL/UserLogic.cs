using BooksLibraryDAL;
using BooksLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryBL
{
    public class UserLogic
    {
        public static User GetUserBymail(string emailaddress)
        {
            User u = new User();
            return u;
        }
        public static User getUserDetailsById(int idu)
        {
            UsersTable dbUser = UserData.getUserById(idu);

            User user = castUserTableToUser(dbUser);
            if (user != null)
                return user;
            return null;
        }

        public static List<User> GetAllUser()
        {
            List<UsersTable> DBUsers = UserData.getAllUsers();
            List<User> Users = new List<User>();
            foreach (var item in DBUsers)
            {
                //המרות

                User u = castUserTableToUser(item);
                if (u != null)
                    Users.Add(u);
            }

            return Users;
        }

        //
        public static User InsertUser(User u)
        {
            if (UserData.isMailExist(u.email) == true)
                return null;
            UsersTable ut = new UsersTable()
            {
                address = u.address,
                birthDate = u.birthDate,
                email = u.email,
                firstName = u.firstName,
                genderId = u.genderId.Code,
                houseNumber = u.houseNumber,
                lastName = u.lastName,
                neighborhood = u.neighborhood,
                password = u.password,
                phone = u.phone
            };
            if (u.cityCode.Code == 0)
            {
                if (LookupBL.getLookupByName(Constants.CitiesTableName, u.cityCode.Desc) != null)
                    ut.cityCode = LookupBL.getLookupByName(Constants.CitiesTableName, u.cityCode.Desc).Code;
                else
                    ut.CityTable = new CityTable() { name = u.cityCode.Desc };
            }
            else ut.cityCode = u.cityCode.Code;
            UsersTable dbUser = UserData.InsertUser(ut);
            if (dbUser != null)
            {
                BooksLibraryDAL.LookupData.refreshLookups();
                User user = castUserTableToUser(dbUser);
                string message = "נרשמת בהצלחה לאתר BOOK A BOOK <br/>";
                message += "מחכים לך הרבה מאוד ספרים מעינים , מחכים לך , צוות האתר ";
                sendEmailFunc.sendEmailAsync(user.email, user.firstName + " " + user.lastName, message, "אישור הרשמה");
                return user;
            }
            return null;
        }
        public static string deleteUser(int uId)
        {
            return UserData.deleteUser(uId);
        }

        public static string EditUser(User u)
        {

            UsersTable ut = new UsersTable()
            {
                id = u.id,
                address = u.address,
                birthDate = u.birthDate,
                //cityCode = u.cityCode.Code,
                email = u.email,
                firstName = u.firstName,
                genderId = u.genderId.Code,
                houseNumber = u.houseNumber,
                lastName = u.lastName,
                neighborhood = u.neighborhood,
                password = u.password,
                phone = u.phone
            };
            if (u.cityCode.Code == 0)
            {
                if (LookupBL.getLookupByName(Constants.CitiesTableName, u.cityCode.Desc) != null)
                    ut.cityCode = LookupBL.getLookupByName(Constants.CitiesTableName, u.cityCode.Desc).Code;
                else
                    ut.CityTable = new CityTable() { name = u.cityCode.Desc };
            }
            else ut.cityCode = u.cityCode.Code;

            string result = UserData.EditUser(ut);
            BooksLibraryDAL.LookupData.refreshLookups();
            return result;

        }
        public static int Login(login loginModel)
        {
           
           
            return  UserData.isExist(loginModel.email, loginModel.password);
        }


       
        public static User castUserTableToUser(UsersTable userTable)
        {
            try
            {
                User user = new User()
                {
                    id = userTable.id,
                    cityCode = LookupBL.getLookupByCode(Constants.CitiesTableName, userTable.cityCode),
                    address = userTable.address,
                    birthDate = DateTime.Parse(userTable.birthDate.ToString()),
                    email = userTable.email,
                    firstName = userTable.firstName,
                    genderId = LookupBL.getLookupByCode(Constants.GenderTableName, int.Parse(userTable.genderId.ToString())),
                    houseNumber = userTable.houseNumber,
                    lastName = userTable.lastName,
                    neighborhood = userTable.neighborhood,
                    password = userTable.password,
                    phone = userTable.phone

                };
                return user;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
