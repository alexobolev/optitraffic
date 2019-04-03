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
            this.Level = TrafficLevel.Medium;
            this.LevelPercentage = 0.34;

            // END OF DUMMY DATA

            //if (this.IncompleteData)
            //    return;
        }

        public static string GetTrafficLevelString(TrafficLevel level)
        {
            string[] levelStrings = { "No traffic", "Low traffic", "Medium traffic", "High traffic", "Take a bicycle" };
            return levelStrings[(int)level];
        }

        public static string GetColorByTraffic(double percentage)
        {
            // 20% - 30% - 50% - 60% - 80%
            string[] colors = { "#00F65A", "#00F65A", "#ffa100", "#f44336", "#f44336" };

            if (percentage >= 0.3)
                return colors[1];
            else if (percentage >= 0.5)
                return colors[2];
            else if (percentage >= 0.6)
                return colors[3];
            else if (percentage >= 0.8)
                return colors[4];
            else
                return colors[0];
        }
    }
}