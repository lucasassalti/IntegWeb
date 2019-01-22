using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class Assinatura
    {


        public int? id_assinatura { get; set; }
        public string cod_assinatura { get; set; }
        public int? id_periodico { get; set; }
        public int? id_periodo { get; set; }
        public int? dist_assinat { get; set; }
        public int? qtde_assinat { get; set; }
        public decimal? valor_assinat { get; set; }
        public DateTime? dt_pagto_assinat { get; set; }
        public DateTime? dt_inicio_assinat { get; set; }
        public DateTime? dt_vecto_assinat { get; set; }
        public DateTime? dt_vigencia { get; set; }
        public string matricula { get; set; }
        public string listarea { get; set; }
        public string ano_mes { get; set; }
        public String obs { get; set; }


        public Assinatura()
        {

            ano_mes = null;
            id_assinatura = null;
            cod_assinatura = null;
            id_periodico = null;
            id_periodo = null;
            dist_assinat = null;
            qtde_assinat = null;
            valor_assinat = null;
            dt_pagto_assinat = null;
            dt_inicio_assinat = null;
            dt_vecto_assinat = null;
            dt_vigencia=null;
            matricula = null;
            obs = null;

        }
    }
}
