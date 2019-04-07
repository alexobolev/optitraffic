using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

namespace optitraffic.Classes
{
    public class HttpHelper
    {
        /// <summary>
        /// URL address of DigiTraffic API endpoint for static data about TMS stations.
        /// </summary>
        public static string TmsStationsUrl = "https://tie.digitraffic.fi/api/v1/metadata/tms-stations";

        /// <summary>
        /// URL address of DigiTraffic API endpoint for dynamic TMS sensor data.
        /// </summary>
        public static string TmsDataUrl = "https://tie.digitraffic.fi/api/v1/data/tms-data/";

        /// <summary>
        /// URL address of DigiTraffic API endpoint for free flow speeds data.
        /// </summary>
        public static string FreeFlowSpeedsUrl = "https://tie.digitraffic.fi/api/v1/data/free-flow-speeds";

        /// <summary>
        /// URL address of DigiTraffic API endpoint for the free flow speeds data of a single station.
        /// </summary>
        public static string FreeFlowSpeedsIdUrl = "https://tie.digitraffic.fi/api/v1/data/free-flow-speeds/tms/";

        /// <summary>
        /// Sends a GET request to the given URL.
        /// </summary>
        /// <param name="url">URL to retrieve</param>
        /// <returns>A string of data</returns>
        /// <remarks>
        /// Basically, a carbon copy of code from MSDN:
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.webrequest?view=netframework-4.7.2
        /// </remarks>
        public static string Get(string url)
        {
            if (null == url)
                throw new ArgumentNullException();

            WebRequest req = WebRequest.Create(url);
            req.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            if (res.StatusCode != HttpStatusCode.OK)
                throw new Exception(((int)res.StatusCode).ToString());

            Stream data_stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(data_stream);
            string res_data = reader.ReadToEnd();

            reader.Close();
            data_stream.Close();
            res.Close();

            return res_data;
        }
    }
}