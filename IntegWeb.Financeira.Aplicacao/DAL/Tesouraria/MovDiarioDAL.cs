using IntegWeb.Entidades.Financeira.Tesouraria;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL
{
    internal class MovDiarioDAL
    {
        public bool Inserir(List<MovDiario> list)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            bool ret = false;
            try
            {
                objConexao.ExecutarNonQuery("SAU_PKG_MOVDIARIA.DELETAR"); //Limpar a tabela para receber os novos registros
                foreach (var objM in list)
                {
                    objConexao.LimpaParametros();
                    objConexao.AdicionarParametro("P_TP_REGISTRO", objM.tp_registro);
                    objConexao.AdicionarParametro("P_EMPRESA", objM.empresa);
                    objConexao.AdicionarParametro("P_EMP_CADASTRO", objM.emp_cadastro);
                    objConexao.AdicionarParametro("P_REGISTRO", objM.registro);
                    objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);
                    objConexao.AdicionarParametro("P_REPRESENTANTE", objM.representante);

                    objConexao.AdicionarParametro("P_CPF", objM.cpf);
                    objConexao.AdicionarParametro("P_NOME", objM.nome);
                    objConexao.AdicionarParametro("P_VENCIMENTO_PARCELA", objM.vencimento_parcela);
                    objConexao.AdicionarParametro("P_CONTRATO", objM.contrato);
                    objConexao.AdicionarParametro("P_DT_EFET_MOV", objM.dt_efet_mov);
                    objConexao.AdicionarParametro("P_MES_ANO_REF", objM.mes_ano_ref);

                    objConexao.AdicionarParametro("P_MOV_TP", objM.mov_tp);
                    objConexao.AdicionarParametro("P_MOV_HIST", objM.mov_hist);

                    objConexao.AdicionarParametro("P_DESC_MOV_HIST", objM.desc_mov_hist);
                    objConexao.AdicionarParametro("P_VLR_MOV", objM.vlr_mov);

                    objConexao.AdicionarParametro("P_DT_MOV_REF", objM.dt_mov_ref);

                    objConexao.AdicionarParametro("P_CONTRATO_MOV", objM.contrato_mov);
                    objConexao.AdicionarParametro("P_SEQUENCIAL", objM.sequencial);

                    objConexao.AdicionarParametro("P_RESPONSAVEL", objM.responsavel);
                    objConexao.AdicionarParametro("P_DT_INCLUSAO", objM.dt_inclusao);

                    ret = objConexao.ExecutarNonQuery("SAU_PKG_MOVDIARIA.INSERIR");
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public DataTable ListaImportacao(string dt_ini, string dt_fin)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("DT_INI", dt_ini);
                objConexao.AdicionarParametro("DT_FIN", dt_fin);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_MOVDIARIA.LIST_IMPORT");


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

        public DataTable ListaDetalheImportacao(MovDiario obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_DT_INCLUSAO", obj.dt_inclusao);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_MOVDIARIA.LIST_DET_IMPORT");


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

        public bool Deletar(MovDiario obj)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_DT_INCLUSAO", obj.dt_inclusao);
                return objConexao.ExecutarNonQuery("SAU_PKG_MOVDIARIA.DELETAR");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }
    }
}
