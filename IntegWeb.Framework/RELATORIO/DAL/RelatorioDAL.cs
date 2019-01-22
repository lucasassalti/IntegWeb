using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Framework.Aplicacao
{
    internal class RelatorioDAL
    {
        public Relatorio Consultar(string relatorio)
        {
            Relatorio objRel = new Relatorio();
            Parametro objPar;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_RELATORIO", relatorio);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_RELATORIO.CONSULTAR");
                objRel.parametros = new List<Parametro>();

                while (leitor.Read())
                {
                    objPar = new Parametro();
                    objRel.titulo = leitor["TITULO"].ToString();
                    objRel.relatorio = leitor["RELATORIO"].ToString();
                    objRel.arquivo = leitor["ARQUIVO"].ToString();
                    objRel.relatorio_extensao = leitor["RELATORIO_EXTENSAO"].ToString();
                    if (leitor["ID_TIPO_RELATORIO"] != null)
                    {
                        objRel.tipo = Convert.ToInt32(leitor["ID_TIPO_RELATORIO"]);
                    }

                    if (!leitor["PARAMETRO"].ToString().Equals(""))
                    {
                        objPar.parametro = leitor["PARAMETRO"].ToString();
                        objPar.descricao = leitor["DESCRICAO"].ToString();
                        objPar.tipo = leitor["TIPO"].ToString();
                        objPar.componente_web = leitor["COMPONENTE_WEB"].ToString();
                        objPar.dropdowlist_consulta = leitor["DROPDOWLIST_CONSULTA"].ToString();
                        objPar.valor_inicial = leitor["VALOR_INICIAL"].ToString();
                        objPar.habilitado = leitor["HABILITADO"].ToString();
                        objPar.visivel = leitor["VISIVEL"].ToString();
                        objPar.permite_null = leitor["PERMITE_NULL"].ToString();
                        objPar.ordem = leitor["ORDEM"].ToString();
                        objRel.parametros.Add(objPar);
                    }
                 
                }

                leitor.NextResult();

                leitor.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return objRel;
        }

        public DataTable ConsultarDrop(string query)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("SQLTEXT", query);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_RELATORIO.CONSULTAR_DROP");

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

        public DataTable ConsultarPkg(Relatorio rel)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                foreach (var item in rel.parametros)
                {
                    objConexao.AdicionarParametro(item.parametro, item.valor);
                }
                
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter(rel.arquivo.Trim());

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
