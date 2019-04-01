using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using optitraffic.Models;

namespace optitraffic
{
    public partial class FrontPage : System.Web.UI.Page
    {
        protected List<TmsStation> TmsStations = new List<TmsStation>();
        protected List<string> Municipalities = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve TMS stations
            #region TMS stations retrieval
            string stationsJson = HttpHelper.Get(HttpHelper.TmsStationsUrl);
            JObject stationsDataObj = JObject.Parse(stationsJson);

            TmsStation tmsStation;
            int id_, munCode_;
            string munName_;

            IEnumerable<JToken> featureObjs = stationsDataObj.SelectTokens("$.features[*]");
            foreach (JToken featureObj in featureObjs)
            {
                if (!int.TryParse((string)featureObj.SelectToken("id"), out id_))
                    id_ = -1;

                if (!int.TryParse((string)featureObj.SelectToken("properties.municipalityCode"), out munCode_))
                    munCode_ = -1;

                munName_ = (string)(featureObj.SelectToken("properties.municipality") ?? "");

                if (-1 == this.Municipalities.IndexOf(munName_))
                    this.Municipalities.Add(munName_);

                tmsStation = new TmsStation(
                    id_, munCode_, munName_
                );

                this.TmsStations.Add(tmsStation);
            }
            #endregion

            this.Municipalities.Sort();
        }

        protected void SearchTextInputChanged(object sender, EventArgs e)
        {
            List<string> suggestionsList = new List<string>();
            foreach (var m in this.Municipalities)
                if (null != m && m.StartsWith(this.LocationName.Text))
                    suggestionsList.Add(m);

            string resultHtml = "";

            foreach (var suggesion in suggestionsList)
                resultHtml += String.Format("<li data-val=\"{0}\">{1}</li>\n", suggesion, suggesion);

            searchOptions.InnerHtml = "<ul>" + resultHtml + "</ul>";
        }
    }
}