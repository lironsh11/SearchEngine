using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManager
{
    public class DBManager
    {
        //crating database event
        DbSearchEntities db = new DbSearchEntities();
        //function to return the the number of rows in Table SearchDetails
        public int DetailsCounter()
        {
            return db.SearchDetails.Count();
        }
        //function to return the the number of rows in Table SearchResults
        public int ResultCounter()
        {
            return db.SearchResults.Count();
        }
        //function that get search value , SearchID and path and adding them to the SearchDetails Table in DataBase
        public void AddSearchDetails(string value, int id, string path = "c:/")
        {
            SearchDetail detail = new SearchDetail() { SearchString = value, SearchID = id, SearchPath = path };
            db.SearchDetails.Add(detail);
        }
        //function that get search SearchID , ResultID and SearchResults and adding them to the SearchResults Table in DataBase
        public void AddSearchResults(int id, int resultId, string value = null)
        {
            SearchResult result = new SearchResult() { SearchResults = value, SearchID = id, ResultID = resultId };
            db.SearchResults.Add(result);
        }
        //function that let you save the changes to the DataBase
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
