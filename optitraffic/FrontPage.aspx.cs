using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        protected void Page_Load(object sender, EventArgs e)
        {

            // Retrieve TMS stations
            #region TMS stations retrieval
            string stationsJson = HttpHelper.Get(HttpHelper.TmsStationsUrl);
            JObject stationsDataObj = JObject.Parse(stationsJson);

            TmsStation tmsStation;
            int id_, munCode_;

            IEnumerable<JToken> featureObjs = stationsDataObj.SelectTokens("$.features[*]");
            foreach (JToken featureObj in featureObjs)
            {
                if (!int.TryParse((string)featureObj.SelectToken("id"), out id_))
                    id_ = -1;

                if (!int.TryParse((string)featureObj.SelectToken("properties.municipalityCode"), out munCode_))
                    munCode_ = -1;

                tmsStation = new TmsStation(
                    id_, munCode_,
                    (string)(featureObj.SelectToken("properties.municipality") ?? "")
                );

                this.TmsStations.Add(tmsStation);
            }
            #endregion
        }

        protected void LocationName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}