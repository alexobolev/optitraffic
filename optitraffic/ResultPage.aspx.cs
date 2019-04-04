using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using optitraffic.Classes;

namespace optitraffic
{
    public partial class ResultPage : System.Web.UI.Page
    {
        protected Municipality Subject;
        protected TrafficLevel Level;
        protected double LevelPercentage = 0;

        protected bool IncompleteData = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == this.Request["LocationName"] ||
                false)
            {
                this.IncompleteData = true;
            } else
            {
                this.Subject.Name = this.Request["LocationName"];
            }

            // DUMMY DATA HERE

            this.Subject = new Municipality("Lahti", 123);
            this.LevelPercentage = GetRandomTraffic();
            this.Level = TrafficUtils.DoubleToLevel(this.LevelPercentage);



            // END OF DUMMY DATA

            //if (this.IncompleteData)
            //    return;
        }

        public string GetTrafficBarStyleString()
        {
            return String.Format("width: {0:F2}%; background-color: {1};", this.LevelPercentage * 100, TrafficUtils.DoubleToColor(this.LevelPercentage));
        }

        public string GetTrafficLevelString()
        {
            string[] levelStrings = { "No traffic", "Low traffic", "Medium traffic", "High traffic", "Take a bloody bicycle" };
            return levelStrings[(int)this.Level];
        }

        public static double GetRandomTraffic()
        {
            Random rnd = new Random();
            return rnd.NextDouble();
        }
    }
}