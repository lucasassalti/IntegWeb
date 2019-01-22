using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Processos
{

    public class CargaServicos
    {
        public string descServico { get; set; }
        public decimal? codServico { get; set; }
        public decimal? valor { get; set; }
        public decimal? vProposto { get; set; }
        public decimal? pProposta { get; set; }
        public DateTime? dtReajusteProp { get; set; }
       
    }

    public class ExportaArquivoScam
    {
        public Decimal Cod_Tab_Servicos { get; set; }
        public Decimal Cod_Recurso { get; set; }
        public DateTime DtReajuste { get; set; }
        public Decimal Valor { get; set; }
        public Int32? RCOSEQ { get; set; }
        public String RCOCODPROCEDIMENTO { get; set; }
        public Decimal CodHosp { get; set; }
    }

    public class RETAJAXSERVHOSP
    {
        public string PPROPOSTA { get; set; }
        public string VPROPOSTO { get; set; }
        public string DESCONTO { get; set; }
        public string COD_HOSP { get; set; }
        public string COD_SERV { get; set; }
        public string DTREAJUSTEPROP { get; set; }
    }
}
