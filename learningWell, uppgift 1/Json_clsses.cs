using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learingwell
{
    public class Antal
    {
        public string value { get; set; }
        public bool rojd { get; set; }
    }

    public class Dimensions
    {
        public string ar { get; set; }
        public string vardland_kod { get; set; }
        public string kon_kod { get; set; }
        public string row_nr { get; set; }
    }

    public class Observations
    {
        public Antal antal { get; set; }
    }

    public class Root
    {
        public Observations observations { get; set; }
        public Dimensions dimensions { get; set; }
    }
}
