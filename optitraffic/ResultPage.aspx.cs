using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using optitraffic.Classes;

namespace optitraffic
{
    public partial class ResultPage : BasePage
    {
        protected Random rnd = new Random();

        protected List<TmsStation> LocalStations;
        protected DataView Data;

        protected Municipality Subject;

        protected bool IncompleteData = false;
        protected string ErrorReason = "";


        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.ParseRequestData();

            this.LocalStations = ((List<TmsStation>)HttpContext.Current.Session["TmsStations"])
                                    .Where(o => o.MunicipalityCode == this.Subject.Code && o.MunicipalityCode != -1)
                                    .ToList();

            TmsData stationData = new TmsData();
            List<TmsData> stationDataList = new List<TmsData>(this.LocalStations.Count);
            FreeFlowSpeed ffsData = new FreeFlowSpeed();
            List<FreeFlowSpeed> ffsDataList = new List<FreeFlowSpeed>(this.LocalStations.Count);

            foreach (TmsStation station in this.LocalStations)
            {
                DataRetriever.GetTmsData(ref stationData, station.Id);
                stationDataList.Add(stationData);

                DataRetriever.GetFreeFlowSpeed(ref ffsData, station.Id);
                ffsDataList.Add(ffsData);
            }

            Data = new DataView(this.Subject.Code, stationDataList, ffsDataList);
        }

        protected void ParseRequestData()
        {
            try
            {
                string locName = this.Request["LocationName"];

                if (null == locName)
                    throw new Exception();

                this.Subject = ((List<Municipality>)HttpContext.Current.Session["Municipalities"])
                                    .Where(o => o.Name.ToLower() == locName.ToLower())
                                    .ToList()
                                    .Single();
            }
            catch
            {
                this.IncompleteData = true;
                this.ErrorReason = this.LocaleRes.GetString("ErrDataIncorrect");
                this.Subject = new Municipality("Lahti", 398);
            }
        }

        public string GetTrafficBarStyleString()
        {
            return String.Format(new System.Globalization.CultureInfo("en-US"), "width: {0:F2}%; background-color: {1};", this.Data.LevelDbl * 100, TrafficUtils.DoubleToColor(this.Data.LevelDbl));
        }

        public string GetTrafficLevelIdentifier()
        {
            string[] levelStrings = { "TrafficLvl0", "TrafficLvl1", "TrafficLvl2", "TrafficLvl3", "TrafficLvl4" };
            return levelStrings[(int)this.Data.Level];
        }

        public string GetVehiclesMeasurementsStr()
        {
            return String.Format(new System.Globalization.CultureInfo("en-US"), "[{0}, {1}]",
                this.Data.VehiclesPerHour, this.Data.VehiclesPerHourRecent);
        }

        public string GetAvgSpeedMeasurementsStr()
        {
            return String.Format(new System.Globalization.CultureInfo("en-US"), "[{0}, {1}]",
                this.Data.AvgSpeed, this.Data.AvgSpeedRecent);
        }

        public int GetVehiclesMinBound()
        {
            int bound = 0;
            double decBound = Math.Min(this.Data.VehiclesPerHour, this.Data.VehiclesPerHourRecent);
            bound = Convert.ToInt32(Math.Ceiling(decBound * 0.98));
            return bound;
        }

        public int GetVehiclesMaxBound()
        {
            int bound = 100000;
            double decBound = Math.Max(this.Data.VehiclesPerHour, this.Data.VehiclesPerHourRecent);
            bound = Convert.ToInt32(Math.Ceiling(decBound * 1.02));
            return bound;
        }

        public int GetAvgSpeedMinBound()
        {
            int bound = 0;
            double decBound = Math.Min(this.Data.AvgSpeed, this.Data.AvgSpeedRecent);
            bound = Convert.ToInt32(Math.Ceiling(decBound * 0.98));
            return bound;
        }

        public int GetAvgSpeedMaxBound()
        {
            int bound = 120;
            double decBound = Math.Max(this.Data.AvgSpeed, this.Data.AvgSpeedRecent);
            bound = Convert.ToInt32(Math.Ceiling(decBound * 1.02));
            return bound;
        }

        //public double GetRandomTraffic()
        //{
        //    return rnd.NextDouble();
        //}

        //public string GetRandomMeasurementsStr(int min, int max)
        //{
        //    if (max < min)
        //        throw new ArgumentException("min > max");

        //    int[] measurements = new int[5];

        //    measurements[0] = rnd.Next(min, max / 2);
        //    measurements[1] = rnd.Next(measurements[0], measurements[0] + (max - min) / 2);
        //    measurements[2] = rnd.Next(min, max);
        //    measurements[3] = rnd.Next(measurements[1], measurements[1] + (max - min) / 2);
        //    measurements[4] = rnd.Next(min, max);

        //    string measurementsStr = String.Format("[{0}, {1}, {2}, {3}, {4}]",
        //        measurements[0], measurements[1], measurements[2], measurements[3], measurements[4]);


        //    return measurementsStr;
        //}
    }
}