using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string time { get; set; }

        public int bestScore { get; set; }


        //public Person(int id, string fn, string ln, string time, int best)
        //{
        //    this.ID = id;
        //    this.firstName = fn;
        //    this.lastName = ln;
        //    this.time = time;
        //    this.bestScore = best;
        //}
        public string FullName
        {
            get{
                return $"{firstName}{lastName}";
            }
        }
    }
}
