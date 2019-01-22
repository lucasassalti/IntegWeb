using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class Contribuicao : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdItem item1 = new grdItem("PSAP/CESP-B", @"10/03/2018", @"14/12/2018", "Unidade Referência - Reserva BPD - CESP", "Obrigatória");
                grdItem item2 = new grdItem("PSAP/CESP-B", @"10/03/2018", @"14/12/2018", "Unidade Referência - Reserva BPD - CESP", "Obrigatória");
                grdItem item3 = new grdItem("PSAP/CESP-B", @"10/03/2018", @"14/12/2018", "Unidade Referência - Reserva BPD - CESP", "Obrigatória");
                grdItem item4 = new grdItem("PSAP/CESP-B", @"10/03/2018", @"14/12/2018", "Unidade Referência - Reserva BPD - CESP", "Obrigatória");
                grdItem item5 = new grdItem("PSAP/CESP-B", @"10/03/2018", @"14/12/2018", "Unidade Referência - Reserva BPD - CESP", "Obrigatória");
                List<grdItem> gridFake = new List<grdItem>();
                gridFake.Add(item1);
                //gridFake.Add(item2);
                //gridFake.Add(item3);
                //gridFake.Add(item4);
                //gridFake.Add(item5);

                grdPesquisa.DataSource = gridFake;
                grdPesquisa.DataBind();
            }
        }
    }

    public class grdItem {
        public string Plano { get; set; }
        public string DtInicio { get; set; }
        public string DtFim { get; set; }
        public string Unidade { get; set; }
        public string Tipo { get; set; }

        public grdItem(string p_plano, string p_dtini, string p_dtfim, string p_unidade, string p_tipo)
        {
            this.Plano = p_plano;
            this.DtInicio = p_dtini;
            this.DtFim = p_dtfim;
            this.Unidade = p_unidade;
            this.Tipo = p_tipo; 
        }
    }
}