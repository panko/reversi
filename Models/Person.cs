using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Models
{
    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string time { get; set; }

        public int bestScore { get; set; }

        public string FullName
        {
            get{
                return $"{firstName}{lastName}";
            }
        }
    }
}
