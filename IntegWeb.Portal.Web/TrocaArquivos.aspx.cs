using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{

    public partial class TrocaArquivos : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            init_SSO_Session();
            //TbUpload_Mensagem.Visible = false;            
        }
 
    }
}