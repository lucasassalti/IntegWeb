using IntegWeb.Entidades.Previdencia.Pagamentos;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace Intranet.Aplicacao.DAL
{
    public class pagamentosDAL
    {
        private void MapearAviso(OracleDataReader leitor, ref pagamentos obj)
        {
            obj.AVISO_COD_EMPRS = leitor["COD_EMPRS"].ToString();
            obj.AVISO_NOM_RZSOC_EMPRS = leitor["NOM_RZSOC_EMPRS"].ToString();
            obj.AVISO_NUM_RGTRO_EMPRG = leitor["NUM_RGTRO_EMPRG"].ToString();
            obj.AVISO_NUM_DIGVR_EMPRG = leitor["NUM_DIGVR_EMPRG"].ToString();
            obj.AVISO_NUM_IDNTF_DPDTE = leitor["NUM_IDNTF_DPDTE"].ToString();
            obj.AVISO_NUM_IDNTF_RPTANT = leitor["NUM_IDNTF_RPTANT"].ToString();
            obj.AVISO_NOM_EMPRG = leitor["NOM_EMPRG"].ToString();
            obj.AVISO_MES_REFERENCIA = leitor["MES_REFERENCIA"].ToString();
            obj.AVISO_ANO_REFERENCIA = leitor["ANO_REFERENCIA"].ToString();
            obj.AVISO_NOM_RZSOC_BANCO = leitor["NOM_RZSOC_BANCO"].ToString();
            obj.AVISO_NOM_AGBCO = leitor["NOM_AGBCO"].ToString();
            obj.AVISO_TIP_CTCOR_HISCAD = leitor["TIP_CTCOR_HISCAD"].ToString();
            obj.AVISO_NUM_CTCOR_HISCAD = leitor["NUM_CTCOR_HISCAD"].ToString();
            obj.AVISO_DAT_PAGTO_PCPGBF = ((DateTime)leitor["DAT_PAGTO_PCPGBF"]).ToString("dd/MM/yyyy");

            //obj.AVISO_ADIANT_PREVIST = Convert.ToInt32(leitor["ADIANT_PREVIST"]);
            obj.AVISO_ADIANT_PREVIST = decimal.Parse(leitor["ADIANT_PREVIST"].ToString());

            obj.AVISO_DCR_PLBNF = leitor["DCR_PLBNF"].ToString();
            obj.AVISO_TXTFIXO31 = leitor["TXTFIXO31"].ToString();
            obj.AVISO_TXTFIXO24 = leitor["TXTFIXO24"].ToString();
            obj.AVISO_TXTFIXO25 = leitor["TXTFIXO25"].ToString();
            obj.AVISO_RODAPE1 = leitor["RODAPE1"].ToString();
            obj.AVISO_RODAPE2 = leitor["RODAPE2"].ToString();
            obj.AVISO_RODAPE3 = leitor["RODAPE3"].ToString();

        }

        private void MapearAviso2(OracleDataReader leitor, ref pagamentosBloco2 obj)
        {
            obj.AVISO_HISTORICO = leitor["HISTORICO"].ToString();
            //string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
            //obj.AVISO_VENCIMENTO = Convert.ToInt32(Convert.DBNull.Equals(leitor["VENCIMENTO"]));

            if (leitor["VENCIMENTO"] != DBNull.Value)
            {
                obj.AVISO_VENCIMENTO = decimal.Parse(leitor["VENCIMENTO"].ToString());
                obj.AVISO_DESCONTO = decimal.Parse(leitor["DESCONTO"].ToString());
            }
            else
            {
                obj.AVISO_VENCIMENTO = 0;
                obj.AVISO_DESCONTO = 0;

            }

            obj.AVISO_TOT_VENCIMENTO = decimal.Parse(leitor["TOT_VENCIMENTO"].ToString());
            obj.AVISO_TOT_DESCONTO = decimal.Parse(leitor["TOT_DESCONTO"].ToString());
            obj.AVISO_TOT_LIQUIDO = Convert.ToDecimal(leitor["TOT_LIQUIDO"]);

        }

        private void MapearAviso3(OracleDataReader leitor, ref pagamentosBloco3 obj)
        {
            obj.AVISO_DRC_VRBFSS = leitor["dcr_vrbfss"].ToString();
            //obj.AVISO_SLD_ANTERIOR = Convert.ToInt32(leitor["sld_anterior"]);
            obj.AVISO_SLD_ANTERIOR = decimal.Parse(leitor["sld_anterior"].ToString());
            //obj.AVISO_MOVTO_MES = Convert.ToInt32(leitor["movto_mes"]);
            obj.AVISO_MOVTO_MES = decimal.Parse(leitor["movto_mes"].ToString());
            //obj.AVISO_SLD_ATUAL = Convert.ToInt32(leitor["sld_atual"]);
            obj.AVISO_SLD_ATUAL = decimal.Parse(leitor["sld_atual"].ToString());
        }


        public Retorno_Aviso_pagto_ms_ab ConsultaAbono(out string mensagem,
                                                       string AVISO_COD_EMPRS,
                                                       string AVISO_NUM_RGTRO_EMPRG,
                                                       string AVISO_NUM_IDNTF_RPTANT,
                                                       string AVISO_NUM_IDNTF_DPDTE,
                                                       string AVISO_ANO_REFERENCIA,
                                                       string Aviso_asabono,
                                                       string Aviso_asquadro)
        {
            Retorno_Aviso_pagto_ms_ab ret = new Retorno_Aviso_pagto_ms_ab();
            ret.pagamentos = new List<pagamentos>();
            ret.pagamentosbloco2 = new List<pagamentosBloco2>();
            ret.pagamentosbloco3 = new List<pagamentosBloco3>();

            mensagem = "";

            //Conexao objConexao = new Conexao();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("ancodemprs", AVISO_COD_EMPRS);
                objConexao.AdicionarParametro("annumrgtroemprg", AVISO_NUM_RGTRO_EMPRG);
                objConexao.AdicionarParametro("annumidntfrptant", AVISO_NUM_IDNTF_RPTANT);
                objConexao.AdicionarParametro("annumidntfdpdte", AVISO_NUM_IDNTF_DPDTE);
                objConexao.AdicionarParametro("ananomesrefer", AVISO_ANO_REFERENCIA);
                objConexao.AdicionarParametro("asabono", Aviso_asabono);
                objConexao.AdicionarParametro("asquadro", Aviso_asquadro);
                objConexao.AdicionarParametroCursor("srcreturn");
                objConexao.AdicionarParametroOut("anqtdeaviso", OracleType.Float);
                objConexao.AdicionarParametroOut("astipoaviso", OracleType.VarChar);

                objConexao.ExecutarDML("opportunity.pre_sp_dados_aviso_pagto_ms_ab");

                ret.anqtdeaviso = int.Parse(objConexao.ParametrosOut()["anqtdeaviso"].Value.ToString());
                ret.astipoaviso = objConexao.ParametrosOut()["astipoaviso"].Value.ToString();


                using (OracleDataReader leitor = (OracleDataReader)objConexao.ParametrosOut()["srcreturn"].Value)
                {
                    while (leitor.Read())
                    {
                        switch (Aviso_asquadro)
                        {
                            case "1":
                                pagamentos qp = new pagamentos();
                                MapearAviso(leitor, ref qp);
                                ret.pagamentos.Add(qp);
                                break;
                            case "2":
                                pagamentosBloco2 qp2 = new pagamentosBloco2();
                                MapearAviso2(leitor, ref qp2);
                                ret.pagamentosbloco2.Add(qp2);
                                break;
                            case "3":
                                pagamentosBloco3 qp3 = new pagamentosBloco3();
                                MapearAviso3(leitor, ref qp3);
                                ret.pagamentosbloco3.Add(qp3);
                                break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                mensagem = "Problemas contate o administrador do sistema: //n" + ex.Message;
            }
            finally
            {
                objConexao.Dispose();
            }

            return ret;

        }

        public string GetDataMax(string nempr, string nreg, string repres)
        {
            //Consultar tabela de Aviso de Pagamento do Oracle para pegar a ultima data gerada
            String retorno = "";
            String queryString = "";
            queryString = queryString + "select nvl(max(to_char(avp.dat_pagto_pcpgbf,'rrrrmm')),0) as referencia from opportunity.tb_avisopagto avp ";
            queryString = queryString + " where avp.cod_emprs      = " + nempr + " "; //  --Parâmetro de Empresa
            queryString = queryString + " and avp.num_rgtro_emprg  = " + nreg + " ";  //--Parâmetro de Registro
            queryString = queryString + " and avp.num_idntf_rptant  = " + repres + " ";  //--Parâmetro de Representante (Pensão Previdenciária)
            queryString = queryString + " and avp.num_idntf_dpdte = 0"; //--Parâmetro de Representante (Pensão Alimentícia)"
            queryString = queryString + " and avp.CODBARRAS_ECT = 'PAGSUPL'"; //--Parâmetro para verificar se a pessoa é PAGSUPL"

            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                retorno = objConexao.ExecutarScalar(queryString).ToString();
            }

            return retorno;

        }

        public DataTable RetornarPgtos(string nempr, string nreg, string repres)
        {
            //Consultar tabela de Aviso de Pagamento do Oracle para pegar a ultima data gerada
            String queryString = "";
            queryString = queryString + "select * from ( ";
            queryString = queryString + "select distinct avp.ano_referencia, Upper(avp.mes_referencia) as mes_referencia, nvl(to_char(avp.dat_pagto_pcpgbf, 'rrrrmm'), 0)";
            queryString = queryString + " || case when to_char(avp.dat_pagto_pcpgbf, 'dd') < 25 then 'S' ELSE 'N' END as referencia, ";
            //queryString = queryString + " Upper(avp.mes_referencia) || case when to_char(avp.dat_pagto_pcpgbf, 'dd') < 25 then  ";
            queryString = queryString + " Upper(avp.mes_referencia) || case when to_char(avp.dat_pagto_pcpgbf, 'dd') < 23 then  ";
            queryString = queryString + " case when to_char(avp.dat_pagto_pcpgbf, 'mm') = 11 then ' - ADIANTAMENTO ABONO'   else ' - ABONO'  end  ";
            queryString = queryString + " end  as mesAnoref  ";
            queryString = queryString + " from opportunity.tb_avisopagto avp ";
            queryString = queryString + " where avp.cod_emprs      = " + nempr + " "; //  --Parâmetro de Empresa
            queryString = queryString + " and avp.num_rgtro_emprg  = " + nreg + " ";  //--Parâmetro de Registro
            queryString = queryString + " and avp.num_idntf_rptant  = " + repres + " ";  //--Parâmetro de Representante (Pensão Previdenciária)
            queryString = queryString + " and avp.num_idntf_dpdte = 0"; //--Parâmetro de Representante (Pensão Alimentícia)"
            queryString = queryString + " and to_char(avp.dat_pagto_pcpgbf,'rrrrmm') > 201501";
            //queryString = queryString + " order by referencia";
            queryString = queryString + " ) order by ano_referencia desc, referencia asc";

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(queryString);
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

        public DataTable RetornarPgtos(string nempr, string nreg, string repres, string mesIni, string mesFim, string anoIni, string anoFim)
        {
            DataTable dt = new DataTable();
            var periodoIni = string.Concat("'",mesIni, "/", anoIni, "'");
            var periodoFim = string.Concat("'",mesFim, "/", anoFim, "'");

            if (periodoIni.Length != 9)
            {
                return dt;
            }

            if (periodoFim.Length != 9)
            {
                return dt;
            }

            //Consultar tabela de Aviso de Pagamento do Oracle para pegar a ultima data gerada
            String queryString = "";
            queryString = queryString + "select * from ( ";
            queryString = queryString + "select distinct avp.ano_referencia, Upper(avp.mes_referencia) as mes_referencia, nvl(to_char(avp.dat_pagto_pcpgbf, 'rrrrmm'), 0)";
            queryString = queryString + " || case when to_char(avp.dat_pagto_pcpgbf, 'dd') < 25 then 'S' ELSE 'N' END as referencia, ";
            //queryString = queryString + " Upper(avp.mes_referencia) || case when to_char(avp.dat_pagto_pcpgbf, 'dd') < 25 then  ";
            queryString = queryString + " Upper(avp.mes_referencia) || case when to_char(avp.dat_pagto_pcpgbf, 'dd') < 23 then  ";
            queryString = queryString + " case when to_char(avp.dat_pagto_pcpgbf, 'mm') = 11 then ' - ADIANTAMENTO ABONO'   else ' - ABONO'  end  ";
            queryString = queryString + " end  as mesAnoref  ";
            queryString = queryString + " from opportunity.tb_avisopagto avp ";
            queryString = queryString + " where avp.cod_emprs      = " + nempr + " "; //  --Parâmetro de Empresa
            queryString = queryString + " and avp.num_rgtro_emprg  = " + nreg + " ";  //--Parâmetro de Registro
            queryString = queryString + " and avp.num_idntf_rptant  = " + repres + " ";  //--Parâmetro de Representante (Pensão Previdenciária)
            queryString = queryString + " and avp.num_idntf_dpdte = 0"; //--Parâmetro de Representante (Pensão Alimentícia)"
            queryString = queryString + " AND to_date(to_char(avp.dat_pagto_pcpgbf, 'MM/rrrr'),'MM/rrrr') BETWEEN  to_date(" + periodoIni + ", 'MM/rrrr') ";
            queryString = queryString + "AND to_date(" + periodoFim + ", 'MM/rrrr' )" ;
            queryString = queryString + " and to_char(avp.dat_pagto_pcpgbf,'rrrrmm') > 201501";
            //queryString = queryString + " order by referencia";
            queryString = queryString + " ) order by ano_referencia desc, referencia asc";

            
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(queryString);
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