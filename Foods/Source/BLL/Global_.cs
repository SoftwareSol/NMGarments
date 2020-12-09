using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foods;

namespace Foods
{
    public class Global_
    {

        public decimal convert(decimal rate, decimal amt, decimal freight)
        {
            decimal convert = (amt * rate) + freight;

            return convert;
        }

    }
}