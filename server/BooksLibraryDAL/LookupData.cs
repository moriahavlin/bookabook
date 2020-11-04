using BooksLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryDAL
{
    public class LookupData
    {
        public static Dictionary<string, Dictionary<int, Lookup>> AllLookupData { get; set; }
        //  public static Dictionary<string, Dictionary<int, BooksName>> AllBooksName { get; set; }
        public static void loadLookups()
        {
            AllLookupData = new Dictionary<string, Dictionary<int, Lookup>>();
            //   AllBooksName = new Dictionary<string, Dictionary<int, BooksName>>();
            loadCities();
            loadBookName();
            loadGenders();
            //  loadBooksName();
            loadAuther();
            loadBookCategory();
            loadPublishing();
            loadlendsStatus();
            loadKealYaad();
        }
        public static void refreshLookups()
        {
            AllLookupData.Clear();
            // AllBooksName.Clear();
            loadLookups();
        }
        private static void loadKealYaad()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> KealYaads = new Dictionary<int, Lookup>();
                    foreach (var item in db.KealYhadTable)
                    {
                        KealYaads.Add(item.id, new Lookup()
                        {
                            Code = item.id,
                            Desc = item.name
                        });
                    }
                    AllLookupData.Add(Constants.ClientsTableName, KealYaads);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void loadPublishing()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> publishing = new Dictionary<int, Lookup>();
                    foreach (var item in db.PublishingTable.OrderBy(p => p.name))
                    {
                        publishing.Add(item.id, new Lookup()
                        {
                            Code = item.id,
                            Desc = item.name
                        });
                    }
                    AllLookupData.Add(Constants.PuplishingTableName, publishing);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static void loadBookName()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> BookNames = new Dictionary<int, Lookup>();
                    foreach (var item in db.BooksNameTable.OrderBy(bn=>bn.name))
                    {
                        BookNames.Add(item.id, new Lookup()
                        {
                            Code = item.id,
                            Desc = item.name
                        });
                    }
                    AllLookupData.Add(Constants.BooksNameTableName, BookNames);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void loadBookCategory()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> bookCategory = new Dictionary<int, Lookup>();
                    foreach (var item in db.BooksCategriesTable.OrderBy(c=>c.name))
                    {
                        bookCategory.Add(item.id, new Lookup()
                        {
                            Code = item.id,
                            Desc = item.name
                        });
                    }
                    AllLookupData.Add(Constants.BooksCategoryTableName, bookCategory);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static void loadlendsStatus()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> lendsStatus = new Dictionary<int, Lookup>();
                    foreach (var item in db.lendsStatusTable)
                    {
                        lendsStatus.Add(item.code, new Lookup()
                        {
                            Code = item.code,
                            Desc = item.statusName
                        });
                    }
                    AllLookupData.Add(Constants.lendsStatusTableName, lendsStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void loadAuther()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> auther = new Dictionary<int, Lookup>();
                    foreach (var item in db.AutherTable.OrderBy(o=>o.name))
                    {
                        auther.Add(item.id, new Lookup()
                        {
                            Code = item.id,
                            Desc = item.name
                        });
                    }
                    AllLookupData.Add(Constants.AutherTableName, auther);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private static void loadBooksName()
        //{
        //    try
        //    {
        //        using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
        //        {
        //            Dictionary<int, BooksName> bookNameCode = new Dictionary<int, BooksName>();
        //            foreach (var item in db.BooksNameTable)
        //            {
        //                BooksName a = new BooksName();
        //                a.id = item.id;
        //                a.name = item.name;
        //                a.rating = item.rating;
        //                bookNameCode.Add(item.id, a);
        //            }
        //            AllBooksName.Add(Constants.BooksNameTableName, bookNameCode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        private static void loadGenders()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> gender = new Dictionary<int, Lookup>();
                    foreach (var item in db.GenderTable)
                    {
                        gender.Add(item.id, new Lookup()
                        {
                            Code = item.id,
                            Desc = item.name
                        });
                    }
                    AllLookupData.Add(Constants.GenderTableName, gender);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void loadCities()
        {
            try
            {
                using (BooksLibraryProjectEntities db = new BooksLibraryProjectEntities())
                {
                    Dictionary<int, Lookup> cities = new Dictionary<int, Lookup>();
                    foreach (var item in db.CityTable.OrderBy(c=>c.name))
                    {
                        cities.Add(item.id, new Lookup()
                        {
                            Code = item.id,
                            Desc = item.name
                        });
                    }
                    AllLookupData.Add(Constants.CitiesTableName, cities);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
