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
        protected Municipality Subject;
        protected TrafficLevel Level;
        protected double LevelPercentage = 0;

        protected bool IncompleteData = false;
        protected string ErrorReason = "";

        protected Random rnd = new Random();

        protected string ReqLocationName = null;
        protected int ReqLocationCode = -1;

        protected List<TmsStation> LocalStations;


        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.ParseRequestData();

            this.LocalStations = ((List<TmsStation>)HttpContext.Current.Application["TmsStations"])
                                    .Where(o => o.MunicipalityCode == this.ReqLocationCode && o.MunicipalityCode != -1)
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

            DataView data = new DataView(this.ReqLocationCode, stationDataList, ffsDataList);


            // DUMMY DATA HERE
            this.LevelPercentage = GetRandomTraffic();
            this.Level = TrafficUtils.DoubleToLevel(this.LevelPercentage);
            // END OF DUMMY DATA
        }

        protected void ParseRequestData()
        {
            try
            {
                this.ReqLocationName = this.Request["LocationName"];
                this.ReqLocationCode = int.Parse(this.Request["LocationCode"]);

                if (null == this.ReqLocationName ||
                    -1 == this.ReqLocationCode)
                    throw new Exception();

                this.Subject = new Municipality(
                    ((List<Municipality>)HttpContext.Current.Application["Municipalities"])
                        .Where(o => o.Code == this.ReqLocationCode)
                        .ToList()
                        .Single()
                        .Name,
                    this.ReqLocationCode
                );

            }
            catch (Exception ex)
            {
                this.IncompleteData = true;
                this.ErrorReason = this.LocaleRes.GetString("ErrDataIncorrect");
                this.Subject = new Municipality("Lahti", 398);
            }
        }

        public string GetTrafficBarStyleString()
        {
            return String.Format(new System.Globalization.CultureInfo("en-US"), "width: {0:F2}%; background-color: {1};", this.LevelPercentage * 100, TrafficUtils.DoubleToColor(this.LevelPercentage));
        }

        public string GetTrafficLevelIdentifier()
        {
            string[] levelStrings = { "TrafficLvl0", "TrafficLvl1", "TrafficLvl2", "TrafficLvl3", "TrafficLvl4" };
            return levelStrings[(int)this.Level];
        }

        public double GetRandomTraffic()
        {
            return rnd.NextDouble();
        }

        public string GetRandomMeasurementsStr(int min, int max)
        {
            if (max < min)
                throw new ArgumentException("min > max");

            int[] measurements = new int[5];

            measurements[0] = rnd.Next(min, max / 2);
            measurements[1] = rnd.Next(measurements[0], measurements[0] + (max - min) / 2);
            measurements[2] = rnd.Next(min, max);
            measurements[3] = rnd.Next(measurements[1], measurements[1] + (max - min) / 2);
            measurements[4] = rnd.Next(min, max);

            string measurementsStr = String.Format("[{0}, {1}, {2}, {3}, {4}]",
                measurements[0], measurements[1], measurements[2], measurements[3], measurements[4]);


            return measurementsStr;
        }
    }
}