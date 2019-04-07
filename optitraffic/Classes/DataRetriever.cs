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

        /// <summary>
        /// Gets a single TMS data by the station ID.
        /// </summary>
        /// <param name="data">TmsData instances to write into</param>
        /// <returns>True if all went well, false on error</returns>
        public static bool GetTmsData(ref TmsData data, int stationCode)
        {
            try
            {
                string dataJson = HttpHelper.Get(String.Format("{0}{1}", HttpHelper.TmsDataUrl, stationCode));
                JObject dataJsonObj = JObject.Parse(dataJson);

                TmsData tmsData = new TmsData(stationCode);
                //int overridesHourDir1_, overridesHourDir2_, overridesMinsDir1_, overridesMinsDir2_;
                //int avgSpeedHourDir1_, avgSpeedHourDir2_, avgSpeedMinsDir1_, avgSpeedMinsDir2_;
                int code, value;

                IEnumerable<JToken> sensorValObjs = dataJsonObj.SelectTokens("$.tmsStations[*].sensorValues[*]");
                foreach (JToken sensorValObj in sensorValObjs)
                {
                    if (!int.TryParse((string)sensorValObj.SelectToken("id"), out code))
                        continue;

                    if (!int.TryParse((string)sensorValObj.SelectToken("sensorValue"), out value))
                        continue;

                    switch (code)
                    {
                        case 5054:
                            tmsData.OverridesHourDirection1 = value;
                            break;
                        case 5055:
                            tmsData.OverridesHourDirection2 = value;
                            break;
                        case 5056:
                            tmsData.AvgSpeedHourDirection1 = value;
                            break;
                        case 5057:
                            tmsData.AvgSpeedHourDirection2 = value;
                            break;

                        case 5116:
                            tmsData.Overrides5MinDirection1 = value;
                            break;
                        case 5119:
                            tmsData.Overrides5MinDirection2 = value;
                            break;
                        case 5122:
                            tmsData.AvgSpeed5MinDirection1 = value;
                            break;
                        case 5125:
                            tmsData.AvgSpeed5MinDirection2 = value;
                            break;

                        default:
                            break;
                    }
                }

                data = tmsData ?? throw new Exception();

            } catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}