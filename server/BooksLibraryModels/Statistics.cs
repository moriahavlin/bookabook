using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibraryModels
{
    public class Statistics
    {
        public int numberOfViewers { get; set; }
        public int numberOfLendings { get; set; }
        public int numberOfwomen  { get; set; }
      
        public int numberOfMen { get; set; }
        public float averageRating { get; set; }
        //מי שזה נמצא בסל שלו של אהבתי 
        public int numberOfLikeIt { get; set; }

        public List<Comment> Comments { get; set; }

       // public List<string> Comments { get; set; }
        public int[] rstingList = new int[5] ;



    }
}
