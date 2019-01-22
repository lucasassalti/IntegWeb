using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Cobranca
{
    public class BoletoCobrancaSaudeDAL
    {
        public SAUDE_EntityConn m_Db_Context = new SAUDE_EntityConn();
        ConexaoOracle objConexao = new ConexaoOracle(); 

        public void ProcessarCobrancaSaude(DateTime dtVenc,DateTime dtTolerancia, decimal numLote, out decimal numLoteOut)
        {

            try
            {
                objConexao.AdicionarParametro("p_dt_vencimento", dtVenc);
                objConexao.AdicionarParametro("p_dt_tolerancia", dtTolerancia);
                objConexao.AdicionarParametro("p_num_lote", numLote);
                objConexao.AdicionarParametroOut("out_num_n_consolidado", OracleType.Number);
                objConexao.ExecutarNonQuery("opportunity.sau_pkg_geracao_boletos_saude.prc_rotina_cobranca_saude");

                List<OracleParameter> parametrosSaida = objConexao.ReturnParemeter();

                numLoteOut = Convert.ToInt32(parametrosSaida[0].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Erro : " + ex.Message);
            }
            finally 
            {
                objConexao.Dispose();
            }

        }

        public void ProcessarFlagInsucesso(decimal numLote) 
        {
           

            try
            {
                objConexao.AdicionarParametro("p_num_n_consolidado", numLote);
                objConexao.ExecutarNonQuery("opportunity.sau_pkg_geracao_boletos_saude.prc_rotina_flag_insucesso");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro : " + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public void ProcessarInadimplentes(decimal tipoRotina,DateTime dtVenc, decimal numLote) 
        {
            try
            {
                

                if (tipoRotina == 1)
                {
                    objConexao.AdicionarParametro("numlote", numLote);
                    objConexao.AdicionarParametro("datvencimento", dtVenc);
                    objConexao.ExecutarNonQuery("opportunity.sp_gera_inadimplentes");
                }
                else if (tipoRotina == 2)
                {
                    objConexao.AdicionarParametro("p_num_n_consolidado", numLote);
                    objConexao.AdicionarParametro("p_dt_vencimento", dtVenc);
                    objConexao.ExecutarNonQuery("opportunity.sau_pkg_geracao_boletos_saude.prc_rotina_inadimplentes");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro : " + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public void ProcessarExtraJudicial(string codemprs,string matricula, decimal numRepres, decimal numlote, DateTime dtVenc,DateTime dtVencAnt, out decimal contador) 
        {
            string empresa = codemprs.PadLeft(3,'0') ;
            string matricula1 = matricula.PadLeft(7,'0');

            var buscaLote = m_Db_Context.OPP_TB_INADIPLENTES.Where(x => x.LOTE == numlote && x.EMPRESA.Trim() == empresa && x.REGISTRO.Trim() == matricula1 && x.NUM_IDNTF_RPTANT == numRepres);
            var buscaParticipante = m_Db_Context.OPP_TB_INADIPLENTES.Where(x => x.VENCIMENTO == dtVencAnt && x.EMPRESA.Trim() == empresa && x.REGISTRO.Trim() == matricula1 && x.NUM_IDNTF_RPTANT == numRepres);

           

            if (buscaParticipante.Count() > 0 )
            {
                
                if (buscaLote.Count() == 0)
                {

                    objConexao.AdicionarParametro("p_num_empresa", codemprs);
                    objConexao.AdicionarParametro("p_num_matricula", matricula);
                    objConexao.AdicionarParametro("p_num_repres", numRepres);
                    objConexao.AdicionarParametro("p_num_n_consolidado", numlote);
                    objConexao.AdicionarParametro("p_dt_vencimento", dtVenc);
                    objConexao.AdicionarParametro("p_dt_vencimento_ant", dtVencAnt);
                    objConexao.AdicionarParametroOut("p_contador");
                    objConexao.ExecutarNonQuery("opportunity.sau_pkg_geracao_boletos_saude.prc_cobranca_extra_judicial");

                    List<OracleParameter> parametrosSaida = objConexao.ReturnParemeter();
                    contador = Convert.ToDecimal(parametrosSaida[0].Value.ToString());
                }
                else
                {
                    throw new Exception(" O registro já existe na Tabela");
                }
            }
            else
            {
                throw new Exception(" Não há registro de inadimplência para este participante no vencimento " + dtVencAnt.ToShortDateString());
            }
        }

        public void ProcessarAvisoDeCancelamento(DateTime dtVenc, decimal numLote)
        {
            
            try
            {
                objConexao.AdicionarParametro("p_dt_vencimento", dtVenc);
                objConexao.AdicionarParametro("p_num_n_consolidado", numLote);
                objConexao.ExecutarNonQuery("opportunity.sau_pkg_geracao_boletos_saude.prc_rotina_aviso_cancela");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro : " + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public DataTable ProcessarRelatorios(decimal tipoRel, DateTime dtVenc, decimal numLote) 
        {
            DataTable dt = new DataTable();

            try
            {
                objConexao.AdicionarParametro("p_dt_vencimento", dtVenc);
                objConexao.AdicionarParametro("p_num_n_consolidado", numLote);
                objConexao.AdicionarParametro("p_num_tipo_rel", tipoRel);
                objConexao.AdicionarParametroCursor("dados");
                
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("opportunity.sau_pkg_geracao_boletos_saude.prc_rotina_geracao_rel");

                adpt.Fill(dt);
                adpt.Dispose();

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro : " + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public void ProcessarArquivosTxts(decimal tipoTxt, decimal numLote, out string nomeArquivo)//
        {
            string teste = " ";

             try
             {
                 objConexao.AdicionarParametro("intNumLote", numLote);
                 objConexao.AdicionarParametro("teste", teste);
                 objConexao.AdicionarParametroOut("intCodErro",OracleType.Number);
                 objConexao.AdicionarParametroOut("strMsgRetorno",OracleType.VarChar);
                 objConexao.AdicionarParametroOut("nomeArquivo",OracleType.VarChar);

                 if (tipoTxt == 2 )//CARTAS DE AVISO de inadimplencia
                 {
                     objConexao.ExecutarNonQuery("opportunity.sp_print_ext_cob_saude_AV");
                 }
                 else if (tipoTxt == 5 )//CARTAS DE AVISO de Cancelamento
                 {
                     objConexao.ExecutarNonQuery("opportunity.sp_print_ext_cob_saude_ej");
                 }
                 else if (tipoTxt == 4 )//CARTAS DE Inadimplentes Digna
                 {
                     objConexao.ExecutarNonQuery("opportunity.sp_print_ext_cob_saude_ativo");
                 }
                 else if (tipoTxt == 3)//DOS BOLETOS JUDICIAIS = COBSAUDAR
                 {
                     objConexao.ExecutarNonQuery("opportunity.sp_print_ext_cob_saude_jud");
                 }
                 else if (tipoTxt == 1)//DOS BOLETOS PRINCIPAIS
                 {
                     objConexao.ExecutarNonQuery("opportunity.sp_print_ext_cob_saude");
                 }

                 List<OracleParameter> parametrosSaida = objConexao.ReturnParemeter();
                 nomeArquivo = parametrosSaida[2].Value.ToString();
             }
             catch (Exception ex)
             {
                 throw new Exception("Erro : " + ex.Message);
             }
             finally
             {
                 objConexao.Dispose();
             }

        }

      
        }

       
}
