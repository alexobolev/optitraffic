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

using optitraffic.Classes;

namespace optitraffic
{

    public partial class FrontPage : System.Web.UI.Page
    {
        protected List<TmsStation> TmsStations = new List<TmsStation>();
        protected List<Municipality> Municipalities = new List<Municipality>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve TMS stations and municipalities
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

                if (!string.IsNullOrWhiteSpace(munName_) && this.Municipalities.FindIndex(x => x.Name == munName_) == -1)
                    this.Municipalities.Add(new Municipality(munName_, munCode_));

                tmsStation = new TmsStation(
                    id_, munCode_, munName_
                );

                this.TmsStations.Add(tmsStation);
            }
            #endregion

            this.Municipalities = this.Municipalities.OrderBy(o => o.Name).ToList();
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            //int munIdx = this.Municipalities.FindIndex(m => m.Name == this.LocationName.Text);

            //if (munIdx > -1)
            //    this.LocationCode.Text = this.Municipalities[munIdx].Code.ToString();
        }
    }
}