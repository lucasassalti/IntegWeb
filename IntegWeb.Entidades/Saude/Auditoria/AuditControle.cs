using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Auditoria
{
    public class AuditControle
    {
      public string tipserv { get; set; }
        public string nome { get; set; }
        public string matricula { get; set; }
        public string hospital { get; set; }
        public DateTime? dt_inter { get; set; }
        public DateTime? dt_alta { get; set; }
        public decimal? custo { get; set; }
        public decimal? glosa { get; set; }
        public decimal? valor_cobrado { get; set; }
        public decimal? valor_pago { get; set; }
        public DateTime? dt_inclusao { get; set; }
        public string responsavel { get; set; }
        public string mesano { get; set; }
        public int?  id_empaudit { get; set; }


        public AuditControle()
        {
            id_empaudit = null;
            tipserv = null;
            nome = null;
            matricula = null;
            hospital = null;
            dt_inter = null;
            dt_alta = null;
            custo = null;
            glosa = null;
            valor_cobrado = null;
            valor_pago = null;
            dt_inclusao = null;
            responsavel = null;
            mesano = null;

        }
    }


}
