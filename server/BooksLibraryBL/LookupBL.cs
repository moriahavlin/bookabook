using BooksLibraryDAL;
using BooksLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryBL
{
    public class LookupBL
    {
        public static void loadLookups()
        {
            LookupData.loadLookups();
        }
        public static Lookup getLookupByCode(string tableName, int code)
        {
            if (LookupData.AllLookupData.ContainsKey(tableName))
            {
                if (LookupData.AllLookupData[tableName].ContainsKey(code))
                {
                    Lookup a = LookupData.AllLookupData[tableName][code];
                    //    return LookupData.AllLookupData[tableName][code];
                    return a;
                }
            }
            return null;
        }
        //public static BooksName getBookName(int code)
        //{
        //    if (LookupData.AllBooksName != null)
        //    {
        //        if (LookupData.AllBooksName[Constants.BooksNameTableName].ContainsKey(code))
        //        {
        //            BooksName b = LookupData.AllBooksName[Constants.BooksNameTableName][code];
        //            return b;
        //        }
        //    }
        //    return null;
        //}
        //public static BooksName getBookNameByname(string name)
        //{
        //    if (LookupData.AllBooksName != null)
        //    {
        //        foreach (var item in LookupData.AllBooksName[Constants.BooksNameTableName])
        //        {
        //            if (item.Value.name == name)
        //            {
        //                BooksName a = LookupData.AllBooksName[Constants.BooksNameTableName][item.Value.id];
        //                return a;
        //            }
        //        }
        //    }
        //    return null;
        //}
        //public static List<BooksName> getAllBookName()
        //{
        //    List<BooksName> bookNameList = new List<BooksName>();
        //    if (LookupData.AllBooksName != null)
        //    {
        //        foreach (var item in LookupData.AllBooksName[Constants.BooksNameTableName])
        //        {
        //            BooksName a = LookupData.AllBooksName[Constants.BooksNameTableName][item.Value.id];
        //            bookNameList.Add(a);
        //        }
        //        return bookNameList;
        //    }
        //    return null;
        //}
        public static Lookup getLookupByName(string tableName, string name)
        {
            if (LookupData.AllLookupData.ContainsKey(tableName))
            {
                foreach (var item in LookupData.AllLookupData[tableName])
                {
                    if (item.Value.Desc == name)
                    {
                        Lookup a = LookupData.AllLookupData[tableName][item.Value.Code];
                        return a;
                    }
                }
            }
            return null;
        }
        public static List<Lookup> getListLookupByTableName(string tableName)
        {//אם קיים כזה lookup
            List<Lookup> listLookup = null;
            if (LookupData.AllLookupData.ContainsKey(tableName))
            {
                listLookup = new List<Lookup>();
                foreach (var item in LookupData.AllLookupData[tableName])
                {
                    listLookup.Add(item.Value);
                }

            }
            return listLookup;
        }
    }
}
