﻿using IntegWeb.Entidades.Cartas;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class AvisoRepresentanteDAL
    {

        public int? cod_emprs { get; set; }
        public int? num_registro { get; set; }
        public int? num_idntf_rptant { get; set; }


        protected DataTable SelecionarAvisoPgtoRepres()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("V_NUM_REGTRO", num_registro);
                objConexao.AdicionarParametro("V_num_idntf_rptant", num_idntf_rptant);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_ACAO_AVISO_PAGAMENTO.PRE_PRC_LISTAR_REPRESETANTE");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }



                   
    }
}
