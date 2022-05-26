using mysql_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysql_test
{
    class Program
    {
        static void Main(string[] args)
        {
            using(MyDbContext myDbContext = new MyDbContext())
            {
                myDbContext.Database.EnsureCreated();
            }
        }
    }
}
