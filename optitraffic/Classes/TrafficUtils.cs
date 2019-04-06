using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace optitraffic.Classes
{
    public static class TrafficUtils
    {
        public const double NoTrafficThreshold = 0.15;
        public const double LowTrafficThreshold = 0.3;
        public const double MediumTrafficThreshold = 0.6;
        public const double HighTrafficThreshold = 0.85;


        public static TrafficLevel DoubleToLevel(double val)
        {
            if (val < NoTrafficThreshold)
                return TrafficLevel.No;
            else if (val < LowTrafficThreshold)
                return TrafficLevel.Low;
            else if (val < MediumTrafficThreshold)
                return TrafficLevel.Medium;
            else if (val < HighTrafficThreshold)
                return TrafficLevel.High;
            else
                return TrafficLevel.TakeBicycle;
        }

        public static string DoubleToColor(double val)
        {
            string[] colors = { "#00F65A", "#00F65A", "#ffa100", "#f44336", "#4a148c" };

            if (val < NoTrafficThreshold)
                return colors[0];
            else if (val < LowTrafficThreshold)
                return colors[1];
            else if (val < MediumTrafficThreshold)
                return colors[2];
            else if (val < HighTrafficThreshold)
                return colors[3];
            else
                return colors[4];
        }
    }
}