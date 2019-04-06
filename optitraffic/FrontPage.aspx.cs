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

    public partial class FrontPage : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
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