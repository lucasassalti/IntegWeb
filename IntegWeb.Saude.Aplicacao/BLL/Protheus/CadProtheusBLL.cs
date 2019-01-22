using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class CadProtheusBLL : CadProtheusDal
    {
        public Resultado Validar(PRE_TBL_CARGA_PROTHEUS CargaProtheus)
        {
            Resultado retorno = new Resultado(true);

            if (CargaProtheus.COD_CARGA_TIPO == null)
            {
                retorno.Erro("O campo código é obrigatório");
                return retorno;
            }
            return retorno;

        }
        public DataTable GeraGridProcesso(string area)
        {
            //Gera as colunas de status 
            DataTable dt = GetGridProcesso(area);
            dt.Columns.Add("Status_da_carga");
            int i = 0;

            foreach (DataRow r in dt.Rows)
            {
                if (r["STATUS"].ToString() == "8")
                {
                    dt.Rows[i]["Status_da_carga"] = "Aguardando validação";
                }
                else if (r["STATUS"].ToString() == "1")
                {
                    dt.Rows[i]["Status_da_carga"] = "Aguardando carga no Protheus";
                }
                else if (r["STATUS"].ToString() == "2")
                {
                    dt.Rows[i]["Status_da_carga"] = "Carregado no Protheus";
                }
                i++;
            }

            return dt;
        }

        public DataSet GeraRelRedeCredenciada(int num_lote)
        {
            DataTable dt1 = GetMedctrAberta(num_lote);
            DataTable dt2 = GetProdutoRedeCred(num_lote);
            DataTable dt3 = GetPatrocinadorRedeCred(num_lote);
            DataTable dt4 = GetLiquidezRedeCred(num_lote);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            ds.Tables.Add(dt4);
            return ds;
        }

        public DataSet GeraRelGerais(int num_lote)
        {
            DataTable dt1 = GetMedctrAberta(num_lote);
            DataTable dt2 = GetResumoPrograma(num_lote);
            DataTable dt3 = GetResumoPatrocinador(num_lote);
            DataTable dt4 = new DataTable();

            if (dt1.Rows[0]["TP_PROC"].ToString() != "1" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "2" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "3" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "4" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "5" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "6" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "7" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "8" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "9" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "10" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "11" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "13" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "19" &&
                dt1.Rows[0]["TP_PROC"].ToString() != "22"
                )
            {
                dt4 = GetResumoLiquidez(num_lote);
            }
            else
            {
                dt4 = GetLiquidezPorTipo(num_lote);
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            ds.Tables.Add(dt4);
            return ds;
        }

        public DataTable RetornaNumeroLote(string login, DateTime dt_inclusao)
        {
            DataTable dt = GetNumeroLoteTipo(login, dt_inclusao);
            return dt;
        }

        public void GeraProcessoProtheus(int tipo_carga, DateTime data_pagto, DateTime data_inclusao)
        {
            GetProcessosGerados(tipo_carga, data_pagto, data_inclusao);

            // return dt;
        }

        public Resultado ValidarTxt(REEMB_FRMCIA CargaProtheus)
        {
            Resultado retorno = new Resultado(true);

            if (CargaProtheus.NUM_RGTRO_EMPRG == null)
            {
                retorno.Erro("O campo código é obrigatório");
                return retorno;
            }
            return retorno;

        }


    }
}