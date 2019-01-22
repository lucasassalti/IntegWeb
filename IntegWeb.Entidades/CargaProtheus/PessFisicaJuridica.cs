using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class PessFisicaJuridica
    {
        public string nomeRazaoSocial { get; set; }
        public long? cpfCnpj { get; set; }
        public string tipoCadastro { get; set; }
        public int codigo { get; set; }
        public int codigoEmpresa { get; set; }
        public short? codigoBanco { get; set; }
        public int? codigoAgencia { get; set; }
        public int? codigoDigVerificadorAgencia { get; set; }
        public string codigoContaCorrente { get; set; }
        public short? codigoDvContaCorrente { get; set; }
        public string codigotipoConta { get ; set; }

        public short? cdTipoConta { get; set; }
    }



    
}
