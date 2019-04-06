using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
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
        protected ResourceManager LocaleRes = null;


        public string Locale
        {
            get
            {
                HttpCookie langInfo = Request.Cookies["lang"];
                if (null == langInfo)
                {
                    langInfo = new HttpCookie("lang");
                    langInfo.Value = "en";
                }

                return langInfo.Value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.LocaleRes = new ResourceManager(
                String.Format("optitraffic.assets.locals.{0}", this.Locale),
                Assembly.GetExecutingAssembly()
            );  // must be before anything else!
            LocationName.Attributes.Add("placeholder", this.LocaleRes.GetString("SearchBoxPlaceholder"));
        }

        [WebMethod]
        public static List<Municipality> GetMunicipalitiesByInput(string inputValue, int maxNum)
        {
            inputValue = inputValue.ToLower();

            try
            {
                return ((List<Municipality>)HttpContext.Current.Application["Municipalities"])
                    .Where(o => o.Name.ToLower().StartsWith(inputValue))
                    .ToList()
                    .Take(maxNum)
                    .ToList();
            } catch (Exception ex)
            {
                return null;
            }
        }
    }
}