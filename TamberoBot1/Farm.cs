using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamberoBot1
{
    public class Farm
    {
        public string animalControlTotal { get; set; }
        public class RootObject
        {
            public Farm farm { get; set; }
        }
    }
}