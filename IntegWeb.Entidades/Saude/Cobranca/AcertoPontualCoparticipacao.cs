using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Cobranca
{
    public class AcertoPontualCoparticipacao
    {
        public Int32    cod_Covenente               { get; set; }
        public String   nome_Covenente              { get; set; }
        public Int32    cod_Empresa                 { get; set; }
        public String   num_Matricula               { get; set; }
        public String   num_Sub_Matricula           { get; set; }
        public String   nom_Participante            { get; set; }
        public String   idc_Internacao              { get; set; }
        public String   ano_Receb_Participante      { get; set; }
        public Int32    num_Seq_receb_Participante  { get; set; }
        public String   ano_Fatura                  { get; set; }
        public Int32    num_Seq_Fatura              { get; set; }
        public Int32    num_Seq_Atend               { get; set; }
        public Int32    num_Seq_Item                { get; set; }
        public Int32    cod_Recurso                 { get; set; }
        public String   des_Recurso                 { get; set; }
        public DateTime dat_Realiz                  { get; set; }
        public DateTime hor_Realiz                  { get; set; }
        public Double    val_p_Particip              { get; set; }
        public Double   val_Cobrado                 { get; set; }
        public Double   val_Calculado               { get; set; }
        public Double   val_Pagar                   { get; set; }
        public Int32    cod_Plano_Pagto             { get; set; }
        public Int32    fat_Acomodacao              { get; set; }
        public String   RCOCODPROCEDIMENTO          { get; set; }
        public Double   val_Particip                { get; set; }
        public String   ano_Ficha_Caixa             { get; set; }
        public String   mes_Ficha_Caixa             { get; set; }


    }
}
