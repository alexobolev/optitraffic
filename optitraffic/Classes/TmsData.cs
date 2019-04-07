﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace optitraffic.Classes
{
    public class TmsData
    {
        public int StationId { get; }

        public int OverridesHourDirection1 { get; set; }
        public int OverridesHourDirection2 { get; set; }
        public int AvgSpeedHourDirection1 { get; set; }
        public int AvgSpeedHourDirection2 { get; set; }

        public int Overrides5MinDirection1 { get; set; }
        public int Overrides5MinDirection2 { get; set; }
        public int AvgSpeed5MinDirection1 { get; set; }
        public int AvgSpeed5MinDirection2 { get; set; }

        public TmsData() : this(-1) { }
        public TmsData(int id)
        {
            this.StationId = id;
        }
    }
}