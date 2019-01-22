using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IntegWeb.Saude.Aplicacao.DAL.Cobranca
{
    public class AcertoPontualCoparticipacaoDAL
    {

        public DataTable ListarExtratoCoparticipacao(AcertoPontualCoparticipacao SelAtualizarAcertoCoparticipacao)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("pAnoFichaCaixa", SelAtualizarAcertoCoparticipacao.ano_Ficha_Caixa);
                objConexao.AdicionarParametro("pMesFichaCaixa", SelAtualizarAcertoCoparticipacao.mes_Ficha_Caixa);
                objConexao.AdicionarParametro("pCodEmpresa", SelAtualizarAcertoCoparticipacao.cod_Empresa);
                objConexao.AdicionarParametro("pNumMatricula", SelAtualizarAcertoCoparticipacao.num_Matricula);

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FC_SAU_PKG_ACERTO_COPARTICIP.Proc_Listar_Acerto_Copart");

                adpt.Fill(dt);
                adpt.Dispose();


            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um problema, //n Contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;

        }

        public Boolean atualizarExtratoCoparticipacao(AcertoPontualCoparticipacao objAcertoPontualCoparticip, AcertoPontualCoparticipacao objAcertoAlteracao)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("pAnoFatura", objAcertoPontualCoparticip.ano_Fatura);
                objConexao.AdicionarParametro("pNumSeqFatura", objAcertoPontualCoparticip.num_Seq_Fatura);
                objConexao.AdicionarParametro("pNumSeqAtend", objAcertoPontualCoparticip.num_Seq_Atend);
                objConexao.AdicionarParametro("pNumSeqItem", objAcertoPontualCoparticip.num_Seq_Item);
                objConexao.AdicionarParametro("pValpParticip", objAcertoAlteracao.val_p_Particip);
                objConexao.AdicionarParametro("pValParticip", objAcertoAlteracao.val_Particip);
                objConexao.AdicionarParametro("pIdcInternacao", objAcertoAlteracao.idc_Internacao);

                return objConexao.ExecutarNonQuery("FC_SAU_PKG_ACERTO_COPARTICIP.Proc_Atualizar_Acerto_Copart");
            }
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um problema, //n Contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public Boolean gravarLogExtratoCoparticipacao(AcertoPontualCoparticipacao objAcertoPontualCoparticip, DateTime dataAlteracao, String NomRespAlt, AcertoPontualCoparticipacao objAcertoAlteracao)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("pAnoFatura", objAcertoPontualCoparticip.ano_Fatura);
                objConexao.AdicionarParametro("pNumSeqFatura", objAcertoPontualCoparticip.num_Seq_Fatura);
                objConexao.AdicionarParametro("pNumSeqAtend", objAcertoPontualCoparticip.num_Seq_Atend);
                objConexao.AdicionarParametro("pNumSeqItem", objAcertoPontualCoparticip.num_Seq_Item);
                objConexao.AdicionarParametro("pDataAlteracao", dataAlteracao);
                objConexao.AdicionarParametro("pNomeRespAlter", NomRespAlt);

                objConexao.AdicionarParametro("pIdcInternacao", objAcertoPontualCoparticip.idc_Internacao);
                objConexao.AdicionarParametro("pNovoIdcInternacao", objAcertoAlteracao.idc_Internacao);

                objConexao.AdicionarParametro("pValpParticip", objAcertoPontualCoparticip.val_p_Particip);
                objConexao.AdicionarParametro("pNovoValpParticip", objAcertoAlteracao.val_p_Particip);

                objConexao.AdicionarParametro("pRcoCodProcedimento", objAcertoAlteracao.RCOCODPROCEDIMENTO);

                objConexao.AdicionarParametro("pValParticip", objAcertoPontualCoparticip.val_Particip);
                objConexao.AdicionarParametro("pNovoValParticip", objAcertoAlteracao.val_Particip);


                return objConexao.ExecutarNonQuery("FC_SAU_PKG_ACERTO_COPARTICIP.Proc_Insert_Log_Acerto_Copart");
            }
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um problema, //n Contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }
    }
}
