using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Cartas
{
    public  enum tipoDocumento
    {
        Devolucao=1,
        Proposta=2
    };
    public class PropostaAdesao
    {
        public int? id_pradprev { get; set; }
        public int? id_tpbeneficio { get; set; }
        public int? id_tpservico { get; set; }
        public int? tipo_doc { get; set; }
        public int? cod_emprs { get; set; }
        public string perfil { get; set; }
        public int? registro { get; set; }
        public string nome { get; set; }
        public int? sit_cadastral { get; set; }
        public string desc_motivo_sit { get; set; }
        public DateTime? dt_envio_kit { get; set; }
        public DateTime? dt_ar { get; set; }
        public DateTime? dt_metrofile { get; set; }
        public string cod_metrofile { get; set; }
        public DateTime? dt_devolucao { get; set; }
        public DateTime? dt_inclusao { get; set; }
        public string destinatario { get; set; }
        public string desc_motivo_dev { get; set; }
        public string matricula { get; set; }
        public string desc_indeferido { get; set; }
        public decimal? voluntaria { get; set; }
        public int id_status { get; set; }

   
        public PropostaAdesao() { 
        
                    id_pradprev =null;
                    id_tpbeneficio =null;
                    id_tpservico =null;
                    tipo_doc =null;
                    cod_emprs =null;
                    perfil =null;
                    registro =null;
                    nome =null;
                    desc_indeferido = null;
                    sit_cadastral =null;
                    desc_motivo_sit =null;
                    dt_envio_kit =null;
                    dt_ar =null;
                    dt_metrofile =null;
                    cod_metrofile =null;
                    dt_devolucao=null;
                    dt_inclusao=null;
                    destinatario =null;
                    desc_motivo_dev =null;
                    matricula =null;
                    voluntaria = null;
        
        
        
        }
    }
}
