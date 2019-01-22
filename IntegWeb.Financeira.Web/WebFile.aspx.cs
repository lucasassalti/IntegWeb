using IntegWeb.Framework.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace IntegWeb.Financeira.Web
{
    public partial class WebFile : BaseWebFile
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["dwFile"] != null)
            {
                ExportToFile(Request.QueryString["dwFile"].ToString());
            }
            else
            {
                ExportToFile(); //Compatibilidade versão antiga
            }
        }
    }
}