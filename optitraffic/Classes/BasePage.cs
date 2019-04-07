using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;

namespace optitraffic.Classes
{
    public class BasePage : System.Web.UI.Page
    {
        protected ResourceManager LocaleRes = null;

        public string Locale
        {
            get
            {
                List<String> localesList = (List<String>)HttpContext.Current.Application["Locales"];

                HttpCookie langInfo = Request.Cookies["lang"];
                if (null == langInfo ||
                    null == localesList ||
                    localesList.FindIndex(t => t == langInfo.Value) < 0)
                {
                    langInfo = new HttpCookie("lang");
                    langInfo.Value = "en";
                }

                return langInfo.Value;
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            this.LocaleRes = new ResourceManager(
                String.Format("optitraffic.assets.locals.{0}", this.Locale),
                Assembly.GetExecutingAssembly()
            );
        }
    }
}