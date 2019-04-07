using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace optitraffic.Classes
{
    public class DataView
    {
        public int LocationId { get; }

        public int VehiclesPerHour { get; set; }
        public int VehiclesPerHourRecent { get; set; }
        public double AvgSpeed { get; set; }
        public double AvgSpeedRecent { get; set; }

        public double LevelDbl { get; }
        public TrafficLevel Level
        {
            get
            {
                return TrafficUtils.DoubleToLevel(this.LevelDbl);
            }
        }


        public DataView() : this(-1) { }
        public DataView(int id)
        {
            this.LocationId = id;
        }

        public DataView(int id, List<TmsData> dataList, List<FreeFlowSpeed> ffsList) : this(id)
        {
            int tmsDataCount = dataList.Count;

            int vehiclesPerHour_ = 0, vehiclesPerHourRecent_ = 0;
            double avgSpeed_ = 0, avgSpeedRecent_ = 0;
            double trafficLevelDbl = 0.0;

            foreach (TmsData data in dataList)
            {
                vehiclesPerHour_ += data.OverridesHourDirection1 + data.OverridesHourDirection2;
                avgSpeed_ += data.AvgSpeedHourDirection1 + data.AvgSpeedHourDirection2;

                vehiclesPerHourRecent_ += data.Overrides5MinDirection1 + data.Overrides5MinDirection2;
                avgSpeedRecent_ += data.AvgSpeed5MinDirection1 + data.AvgSpeed5MinDirection2;
            }

            // Average speed across all sensors is calculated as an arithmetic average.
            // Not the most precise approach probably, but good as an estimation value.
            this.AvgSpeed = (double)avgSpeed_ / (2 * tmsDataCount);
            this.AvgSpeedRecent = (double)avgSpeedRecent_ / (2 * tmsDataCount);


            int ffsCount = ffsList.Count;
            foreach (FreeFlowSpeed ffs in ffsList)
            {
                if (ffs.Total == 0)
                {
                    ffsCount -= 1;
                    continue;
                }

                trafficLevelDbl += TrafficUtils.CalculateLevelDouble(ffs.Total, this.AvgSpeed);
            }

            trafficLevelDbl /= (ffsList.Count > 0 ? ffsList.Count : 1);


            this.VehiclesPerHour = vehiclesPerHour_;
            this.VehiclesPerHourRecent = vehiclesPerHourRecent_;

            this.LevelDbl = trafficLevelDbl;
        }
    }
}