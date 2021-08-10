using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttApi
{
    public static class ListStrToString
    {
        public static string ListToString(this List<string> l)
        {
            string rstr = "";
            l.ForEach(p =>
            {
                if (rstr == "") rstr = p;
                else rstr = rstr + "," + p;

            });
            return rstr;
        }
    }
}
