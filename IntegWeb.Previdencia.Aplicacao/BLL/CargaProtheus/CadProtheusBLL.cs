using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace IntegWeb.Previdencia.Aplicacao.BLL
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

        public DataTable GeraGridProcesso(string login)
        {
            DataTable dt = GetGridProcesso(login);

            DataTable rjt = new DataTable();


            dt.Columns.Add("Status_da_carga");
            int i = 0;

            foreach (DataRow r in dt.Rows)
            {
                rjt = GetRejeitada(Convert.ToInt32(r["num_lote"]));

                if (rjt.Rows.Count > 0)
                {
                    dt.Rows[i]["Status_da_carga"] = "Rejeitado";
                }
                else
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
                    
                }
                i++;
            }


            return dt;
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

        public DataSet GeraRelCredenc(int num_lote)
        {
            DataTable dt1 = GetMedctrAberta(num_lote);
            DataTable dt2 = GetProdutoCredenciada(num_lote);
            DataTable dt3 = GetPatrocinadorCredenciada(num_lote);
            DataTable dt4 = GetLiquidezCredenciada(num_lote);


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

        public void GeraProcessoProtheus(int tipo_carga, DateTime data_pagto, DateTime data_inclusao, int mes_ref, int ano_ref, int cod_emprs)
        {
            GetProcessosGerados(tipo_carga, data_pagto, data_inclusao, mes_ref, ano_ref, cod_emprs);

            // return dt;
        }


    }
}
