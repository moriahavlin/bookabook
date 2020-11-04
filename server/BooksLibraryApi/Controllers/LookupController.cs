using BooksLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BooksLibraryBL;

namespace BooksLibraryApi.Controllers
{
    public class LookupController : ApiController
    {
        [HttpGet]
        public List<Lookup> getGenderList()
        {
            return LookupBL.getListLookupByTableName(Constants.GenderTableName);
        }
        public List<Lookup> getAllAuther()
        {
            return LookupBL.getListLookupByTableName(Constants.AutherTableName);
        }
         public List<Lookup> getAllPublishing()
        {
            return LookupBL.getListLookupByTableName(Constants.PuplishingTableName);
        }
        public List<Lookup> getAllCategory()
        {
            return LookupBL.getListLookupByTableName(Constants.BooksCategoryTableName);
        }
        public List<Lookup> getAllBookName()
        {
            return LookupBL.getListLookupByTableName(Constants.BooksNameTableName); ;
        }  public List<Lookup> getAllCities()
        {
            return LookupBL.getListLookupByTableName(Constants.CitiesTableName); ;
        }
    }
}
