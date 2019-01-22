using IntegWeb.Framework;
using IntegWeb.Entidades.Saude.Financeiro;
using System.Data.OracleClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IntegWeb.Saude.Aplicacao.DAL.Financeiro
{
    internal class HistoricoBoletoDAL
    {

        public List<HistProcessaBoleto> ConsultarProcessamento()
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            List<HistProcessaBoleto> list = new List<HistProcessaBoleto>();
            HistProcessaBoleto histi;
            try
            {
                
                objConexao.AdicionarParametroCursor("dados");

                System.Data.OracleClient.OracleDataReader leitor = objConexao.ObterLeitor("OPPORTUNITY.FIN_PKG_BOLETOS_SAUDE.LISTAR");
                //DateTime dt;
                while (leitor.Read())
                {


                    histi = new HistProcessaBoleto();
                    histi.execucao_id = int.Parse(leitor["execucao_id"].ToString());
                    //hist.inicio = DateTime.Parse(leitor["inicio"].ToString());
                    //histi.dat_vencimento = DateTime.Parse(leitor["dat_vencimento"].ToString());

                    //if (leitor["dat_vencimento"].ToString() == null) 
                    //{
                    //    hist.dat_vencimento = (DateTime?)null; 
                    //}
                    //else
                    //{
                    //    hist.dat_vencimento = DateTime.Parse(leitor["dat_vencimento"].ToString());
                    //}

                    histi.dat_vencimento = leitor["dat_vencimento"].ToString().Equals("") ? (DateTime?)null : DateTime.Parse(leitor["dat_vencimento"].ToString());


                    histi.inicio = leitor["inicio"].ToString() == "" ? (DateTime?)null : DateTime.Parse(leitor["inicio"].ToString());
                    //histi.inicio = DateTime.Parse(leitor["inicio"].ToString());
                    histi.fim = leitor["fim"].ToString() == "" ? (DateTime?)null : DateTime.Parse(leitor["fim"].ToString());
                    //histi.fim = DateTime.Parse(leitor["fim"].ToString());
                    histi.lote_nao_consolidado = leitor["lote_nao_consolidado"].ToString() == "" ? 0 : int.Parse(leitor["lote_nao_consolidado"].ToString());
                    //histi.lote_nao_consolidado = int.Parse(leitor["lote_nao_consolidado"].ToString());
                    histi.mensagem = leitor["mensagem"].ToString();
                   // hist.dat_vencimento = DateTime.Parse(leitor["dat_vencimento"].ToString());
                    list.Add(histi);
                    
                }

                leitor.Dispose();
            }
            catch  (Exception ex)
            {
               throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return list;
        }

        public DataTable ListaCobrancas()
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OPPORTUNITY.FIN_pkg_boletos_saude.gera_rel_todas_cobrancas");

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

        public DataTable ListaFlagAtivo()
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OPPORTUNITY.FIN_pkg_boletos_saude.GERA_REL_FLAG_ATIVO");

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

        public DataTable ListaInadimplentes()
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OPPORTUNITY.FIN_pkg_boletos_saude.gera_rel_inadimplentes");

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

        public DataTable ListaEnderecoNulo()
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OPPORTUNITY.FIN_pkg_boletos_saude.gera_rel_endereco_nulo");

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
