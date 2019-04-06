using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using optitraffic.Classes;

namespace optitraffic
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            List<TmsStation> tmsStations = new List<TmsStation>();
            List<Municipality> municipalities = new List<Municipality>();

            if (!DataRetriever.GetStationsAndMunicipalities(ref tmsStations, ref municipalities))
            {
                Application["InitialLoadFailed"] = true;
                Application["TmsStations"] = null;
                Application["Municipalities"] = null;
            } else
            {
                Application["InitialLoadFailed"] = false;
                Application["TmsStations"] = tmsStations;
                Application["Municipalities"] = municipalities;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://stackoverflow.com/questions/5028206/how-to-catch-httprequestvalidationexception-in-production/31808854
        /// https://support.microsoft.com/en-us/help/312629/prb-threadabortexception-occurs-if-you-use-response-end-response-redir
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            Exception ex = ctx.Server.GetLastError();
            if (ex is HttpRequestValidationException)
            {
                ctx.Server.ClearError();
                this.Response.Redirect("FrontPage.aspx", false);
                return;
            }

        }
    }
}