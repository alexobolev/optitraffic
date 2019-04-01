using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace optitraffic.Classes
{
    public struct Municipality
    {
        public string Name;
        public int Code;

        public Municipality(string name, int code)
        {
            Name = name;
            Code = code;
        }
    }
}