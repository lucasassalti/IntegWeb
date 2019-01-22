using IntegWeb.Entidades.Cartas;
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
    public class PropostaAdesaoDAL : PropostaAdesao
    {
        protected DataTable ListarControles(int? codEmpresa, int? codMatricula, DateTime? dtIni, DateTime? dtFim, int? intStatus, string sortParameter)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);                
                objConexao.AdicionarParametro("P_COD_EMPRS", codEmpresa);
                objConexao.AdicionarParametro("P_REGISTRO", codMatricula);
                objConexao.AdicionarParametro("P_DT_INI", dtIni);
                objConexao.AdicionarParametro("P_DT_FIM", dtFim);
                objConexao.AdicionarParametro("P_ID_STATUS", (intStatus == 0) ? null : intStatus);

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_CONTROLE");

                adpt.Fill(dt);
                adpt.Dispose();

                DataRow[] arDR = dt.Select("", sortParameter);
                dt = (arDR.Length > 0) ? arDR.CopyToDataTable() : new DataTable();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable SelecionaRelatorioCartas(Int32 cdPes, Int32 cdEmp, DateTime dtIni, DateTime dtFim)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.LimpaParametros();
                objConexao.AdicionarParametro("P_REGISTRO", cdPes);
                objConexao.AdicionarParametro("P_EMPRESA", cdEmp);
                objConexao.AdicionarParametro("P_DTINICIAL", dtIni);
                objConexao.AdicionarParametro("P_DTFINAL", dtFim);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_RELATORIOS_CARTAS");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable SelecionarProposta()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_PERFIL", perfil);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_PROPOSTA");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable SelecionarPropostaPorParticipante()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_PROPOSTA_POR_PART");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable SelecionarControle()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_CONTROLE");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable SelecionarParticipante()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_EMPRESA", cod_emprs);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_PARTICIP");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable SelecionaRelatorio()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_EMPRESA", cod_emprs);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_RELATORIOS");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable ListarStatus()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTAR_STATUS");
                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected bool InserirProposta(out string mensagem, out int id)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_ID_BENEFICIO", id_tpbeneficio);
                objConexao.AdicionarParametro("P_ID_TPSERVICO", id_tpservico);
                objConexao.AdicionarParametro("P_PERFIL", perfil);
                objConexao.AdicionarParametro("P_DTINCLUSAO", dt_inclusao);
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("P_VOLUNTARIA", voluntaria);
                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_NOME", nome);
                objConexao.AdicionarParametro("P_SIT_CADASTRAL", sit_cadastral);
                objConexao.AdicionarParametro("P_DESC_MOTIVO_SIT", desc_motivo_sit);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
                objConexao.AdicionarParametro("P_TIPO_DOC", tipo_doc);
                objConexao.AdicionarParametroOut("P_RETORNO");

                mensagem = "Registro Inserido com Sucesso";
                objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.INSERIR");
                id = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                mensagem = "Registro inserido com sucesso";
                return id > 0;
               

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

        protected bool AlterarProposta(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_ID_BENEFICIO", id_tpbeneficio);
                objConexao.AdicionarParametro("P_ID_TPSERVICO", id_tpservico);
                objConexao.AdicionarParametro("P_DTINCLUSAO", dt_inclusao);
                objConexao.AdicionarParametro("P_VOLUNTARIA", voluntaria);
                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("P_NOME", nome);
                objConexao.AdicionarParametro("P_SIT_CADASTRAL", sit_cadastral);
                objConexao.AdicionarParametro("P_DESC_MOTIVO_SIT", desc_motivo_sit);
                objConexao.AdicionarParametro("P_PERFIL", perfil);
                objConexao.AdicionarParametro("P_DESC_INDEFERIDO", desc_indeferido);              

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.ALTERAR_PROPOSTA");

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

        protected bool DeletarProposta(out string mensagem)
        {
            bool bRet = false;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.DELETAR");
                int ret = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                switch (ret)
                {
                    case 1:
                        mensagem = "Não é possível excluir um registro com status \\n[Aguardando AR] ou [Arquivado]!";                        
                        break;
                    case 2:
                        mensagem = "ATENÇÃO! Não é possível excluir um registro com status [Enviado], \\n[Enviar KIT] ou [Indeferido]";
                        mensagem += "\\n\\nO status do registro foi alterado para [Criado]";
                        bRet = true;
                        break;
                    case 0:
                    default:
                        mensagem = "Registro Deletado com Sucesso";
                        bRet = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

            return bRet;

        }

        protected bool EnviarCapitalizacao(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);


                mensagem = "Enviado para a Capitalização";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.ENVIAR_CAPITALIZACAO");

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

        protected bool EnviarKit(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
                objConexao.AdicionarParametro("P_DT_ENVIO_KIT", dt_envio_kit);
                objConexao.AdicionarParametro("P_DT_AR", dt_ar);
                objConexao.AdicionarParametro("P_DT_METROFILE", dt_metrofile);
                objConexao.AdicionarParametro("P_COD_METROFILE", cod_metrofile);


                mensagem = "Dados do Kit atualizados com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.ENVIAR_KIT");

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

        protected bool ArquivarProposta(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);


                mensagem = "Proposta arquivada";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.ARQUIVAR_PROPOSTA");

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

        protected bool InserirDeferimento(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
                objConexao.AdicionarParametro("P_MOTIVO", desc_indeferido);


                mensagem = "Proposta Deferida com Sucesso!";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.DEFERIR_DOCUMENTO");

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

        protected bool InserirIndeferimento(out string mensagem)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_MOTIVO", desc_indeferido);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);


                mensagem = "Proposta Indeferida com Sucesso!";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.INDEFERIR_DOCUMENTO");

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
