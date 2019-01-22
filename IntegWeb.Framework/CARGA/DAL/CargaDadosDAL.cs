using IntegWeb.Entidades.Carga;
using IntegWeb.Framework;
//using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Framework.Aplicacao
{
    internal class CargaDadosDAL
    {
        public CargaDados Consultar(string carga)
        {
            CargaDados objCarga = new CargaDados();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_CARGA", carga);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataReader leitor = objConexao.ObterLeitor("FUN_PKG_CARGA_DADOS.CONSULTAR");
                objCarga.de_para = new List<CargaDadosDePara>();

                while (leitor.Read())
                {
                    CargaDadosDePara objDePara = new CargaDadosDePara();
                    objCarga.id_carga = int.Parse(leitor["ID_CARGA"].ToString());
                    objCarga.carga  = leitor["NM_CARGA"].ToString();
                    objCarga.titulo = leitor["TITULO"].ToString();
                    objCarga.carga_extensao = leitor["EXTENSAO"].ToString();
                    objCarga.pkg_listar = leitor["NM_PKG_LISTAR"].ToString();
                    objCarga.pkg_deletar = leitor["NM_PKG_DELETAR"].ToString();
                    if (leitor["ID_CARGA_TIPO"] != null)
                    {
                        objCarga.tipo = Convert.ToInt32(leitor["ID_CARGA_TIPO"]);
                    }
                    if (!leitor["TABELA_DESTINO"].ToString().Equals(""))
                    {
                        objDePara.tabela_destino = leitor["TABELA_DESTINO"].ToString();
                        objDePara.origem_campo = leitor["ORIGEM_CAMPO"].ToString();
                        objDePara.destino_campo = leitor["DESTINO_CAMPO"].ToString();
                        if (leitor["ORIGEM_TIPO"] != null)
                        {
                            objDePara.origem_tipo = int.Parse(leitor["ORIGEM_TIPO"].ToString());
                        }
                        if (leitor["DESTINO_TIPO"] != null)
                        {
                            objDePara.destino_tipo = int.Parse(leitor["DESTINO_TIPO"].ToString());
                        }
                        if (leitor["VALOR_PADRAO"] != null)
                        {
                            objDePara.destino_valor_padrao = leitor["VALOR_PADRAO"].ToString();
                        }
                        if (leitor["NUM_ORDEM"] != null)
                        {
                            objDePara.ordem = int.Parse(leitor["NUM_ORDEM"].ToString());
                        }                        
                        objCarga.de_para.Add(objDePara);
                    }
                 
                }

                leitor.NextResult();

                leitor.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema [CargaDadosDAL.Consultar]: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return objCarga;
        }

        public bool ImportaDados(CargaDados carga, DataTable dt)
        {
            bool ret = false;
            using (Oracle.DataAccess.Client.OracleBulkCopy bulkCopy = new Oracle.DataAccess.Client.OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName =  carga.de_para[0].tabela_destino;

                string strColunas = "";
                foreach (CargaDadosDePara cddBulkCol in carga.de_para)
                {
                    bulkCopy.ColumnMappings.Add(cddBulkCol.origem_campo, cddBulkCol.destino_campo);
                    strColunas += cddBulkCol.origem_campo + ", ";
                }
                strColunas = strColunas.Substring(0, strColunas.Length - 2);

                try
                {
                    bulkCopy.WriteToServer(dt);
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    throw new Exception(ex.Message + "\\n\\nVerique se a planinha contém as colunas (" + strColunas + ")");
                }
                finally
                {
                    bulkCopy.Close();
                }

            }
            return ret;
        }

        public bool Deletar(CargaDados carga)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                return objConexao.ExecutarNonQuery(carga.pkg_deletar);
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema [CargaDadosDAL.Deletar]: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public DataTable ConsultarPkg(CargaDados carga)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = objConexao.ExecutarAdapter(carga.pkg_listar.Trim());
                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema [CargaDadosDAL.ConsultarPkg]: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

    }
}
