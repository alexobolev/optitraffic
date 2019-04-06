using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace optitraffic
{
    public partial class Oops : System.Web.UI.Page
    {

        protected string ErrorText;
        protected int ErrorCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            this.ErrorText = lastError.Message;
            this.ErrorCode = lastError.HResult;
        }
    }
}