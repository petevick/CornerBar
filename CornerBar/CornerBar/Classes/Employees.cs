using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornerBar.Classes
{
   public class Employee
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
