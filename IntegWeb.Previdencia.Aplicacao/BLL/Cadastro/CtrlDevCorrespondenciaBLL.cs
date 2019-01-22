using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Cadastro
{
    public class CtrlDevCorrespondenciaBLL : CtrlDevCorrespondenciaDAL
    {
        public DataTable buscarControleCorrespondencia()
        {
            DataTable dt = new DataTable();
            List<CAD_TBL_CTRLDEV_CORRESP> list = new List<CAD_TBL_CTRLDEV_CORRESP>();
            list = GetControleCorrespondencia().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }

        public class ControleCorrespondencia
        {
            public decimal ID_REG { get; set; }
            public Nullable<decimal> NUM_CONTRATO { get; set; }
            public Nullable<decimal> COD_EMPRS { get; set; }
            public Nullable<decimal> MATRICULA { get; set; }
            public Nullable<decimal> NUM_REP { get; set; }
            public string NOME { get; set; }
            public string ENDERECO { get; set; }
            public string COMPLEMENTO { get; set; }
            public string NUMERO { get; set; }
            public string BAIRRO { get; set; }
            public string MUNICIPIO { get; set; }
            public string UF { get; set; }
            public string CEP { get; set; }
            public Nullable<decimal> ID_TIPODOCUMENTO { get; set; }
            public Nullable<System.DateTime> DATAPOSTAGEM { get; set; }
            public Nullable<System.DateTime> DATADEVOLUCAO { get; set; }
            public Nullable<decimal> ID_TIPOMOTIVODEVOLUCAO { get; set; }
            public Nullable<System.DateTime> REENVIO { get; set; }
            public string TEMPO_PRAZO { get; set; }
        }

    }
}
