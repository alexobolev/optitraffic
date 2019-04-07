using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace optitraffic.Classes
{
    public class FreeFlowSpeed
    {
        public int StationId { get; set; }
        public double Direction1 { get; set; }
        public double Direction2 { get; set; }

        public double Total
        {
            get
            {
                return (this.Direction1 + this.Direction2) / 2;
            }
        }

        public FreeFlowSpeed() : this(-1) { }
        public FreeFlowSpeed(int id)
        {
            this.StationId = id;
        }

    }
}