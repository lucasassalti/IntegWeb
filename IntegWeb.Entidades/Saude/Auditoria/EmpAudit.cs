using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Auditoria
{
    public class EmpAudit
    {
        public int?  id_empid {get;set;}
        public string  descricao { get; set; }

        public EmpAudit() {

            id_empid = null;
            descricao = null;
        
        }
    }
}
