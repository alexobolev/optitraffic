using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using optitraffic.Classes;


namespace optitraffic.Classes
{
    public class DataRetriever
    {
        /// <summary>
        /// Gets a list of TMS stations and a list of municipalities from the online resource
        /// </summary>
        /// <param name="stations">List of TMS stations to write into</param>
        /// <param name="municipalities">List of municipalities to write into</param>
        /// <returns>True if all went well, false on error</returns>
        public static bool GetStationsAndMunicipalities(ref List<TmsStation> stations, ref List<Municipality> municipalities)
        {
            try
            {
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

                    if (!string.IsNullOrWhiteSpace(munName_) && municipalities.FindIndex(x => x.Name == munName_) == -1)
                        municipalities.Add(new Municipality(munName_, munCode_));

                    tmsStation = new TmsStation(
                        id_, munCode_, munName_
                    );

                    stations.Add(tmsStation);
                }

            } catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}