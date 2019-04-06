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


        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            try
            {
                this.ReqLocationName = HttpUtility.HtmlEncode(this.Request["LocationName"]);
            }
            catch (Exception ex)
            {
                this.ReqLocationName = null;
            }

            try
            {
                this.ReqLocationCode = int.Parse(HttpUtility.HtmlEncode(this.Request["LocationCode"]));
            } catch (Exception ex)
            {
                this.ReqLocationCode = -1;
            }


            if (null == this.ReqLocationName ||
                -1 == this.ReqLocationCode)
            {
                this.IncompleteData = true;
                this.ErrorReason = this.LocaleRes.GetString("ErrDataMissing");
                this.Subject = new Municipality("Lahti", 123);
            } else
            {
                try
                {
                    this.Subject = new Municipality(
                        this.ReqLocationName,
                        this.ReqLocationCode
                    );
                } catch (FormatException ex)
                {
                    this.IncompleteData = true;
                    this.ErrorReason = this.LocaleRes.GetString("ErrDataIncorrect");
                    this.Subject = new Municipality("Lahti", 123);
                }
            }

            this.Subject.Name = StringHelper.CapitalizeFirstChar(this.Subject.Name);

            // DUMMY DATA HERE
            this.LevelPercentage = GetRandomTraffic();
            this.Level = TrafficUtils.DoubleToLevel(this.LevelPercentage);
            // END OF DUMMY DATA
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