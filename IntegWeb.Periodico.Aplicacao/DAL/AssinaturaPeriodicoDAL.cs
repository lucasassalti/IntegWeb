using  IntegWeb.Entidades;
using  IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    internal class AssinaturaPeriodicoDAL
    {
        public DataTable SelectAll(Assinatura objM)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_ASSINATURA", objM.id_assinatura);
                objConexao.AdicionarParametro("P_COD_ASSINATURA", objM.cod_assinatura);
                objConexao.AdicionarParametro("P_MES_ANO", objM.ano_mes);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ASSINATURA.LISTAR");

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

        public DataTable SelectJoin(Assinatura obj)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_ASSINATURA", obj.id_assinatura);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ASSINATURA.LISTAR_AREA_ASSINATURA");

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
               
        public bool Insert(out string mensagem, Assinatura objM, out int id)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
     
                objConexao.AdicionarParametro("P_ID_PERIODICO", objM.id_periodico);
                objConexao.AdicionarParametro("P_PERIODO", objM.id_periodo);
                objConexao.AdicionarParametro("P_DISTRIBUICAO", objM.dist_assinat);
                objConexao.AdicionarParametro("P_QUANTIDADE", objM.qtde_assinat);
                objConexao.AdicionarParametro("P_VALOR", objM.valor_assinat);
                objConexao.AdicionarParametro("P_CODIGO", objM.cod_assinatura);
                objConexao.AdicionarParametro("P_DT_PAGTO", objM.dt_pagto_assinat);
                objConexao.AdicionarParametro("P_DT_INICIO", objM.dt_inicio_assinat);
                objConexao.AdicionarParametro("P_DT_VECTO", objM.dt_vecto_assinat);

                objConexao.AdicionarParametro("P_DT_VIGENCIA", objM.dt_vigencia);
                objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);
                objConexao.AdicionarParametroOut("P_RETORNO");
                mensagem = "Registro inserido com sucesso";
                objConexao.ExecutarNonQuery("SAU_PKG_ASSINATURA.INSERIR");
                id=int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                return id > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

            
        }

        public bool Update(out string mensagem, Assinatura objM)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_ASSINATURA", objM.id_assinatura);
                objConexao.AdicionarParametro("P_ID_PERIODICO", objM.id_periodico);
                objConexao.AdicionarParametro("P_PERIODO", objM.id_periodo);
                objConexao.AdicionarParametro("P_DISTRIBUICAO", objM.dist_assinat);
                objConexao.AdicionarParametro("P_QUANTIDADE", objM.qtde_assinat);
                objConexao.AdicionarParametro("P_CODIGO", objM.cod_assinatura);
                objConexao.AdicionarParametro("P_DT_PAGTO", objM.dt_pagto_assinat);
                objConexao.AdicionarParametro("P_DT_INICIO", objM.dt_inicio_assinat);
                objConexao.AdicionarParametro("P_DT_VECTO", objM.dt_vecto_assinat);
                mensagem = "Registro atualizado com sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_ASSINATURA.ALTERAR");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

           

        }

        public bool Delete(out string mensagem, Assinatura obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_ASSINATURA", obj.id_assinatura);
                objConexao.AdicionarParametro("P_OBS", obj.obs);
                objConexao.AdicionarParametro("P_MATRICULA", obj.matricula);

                mensagem = "Assinatura cancelada com sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_ASSINATURA.DELETAR");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }

        public bool InserAreaAssinatura(Assinatura objM, out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_AREA",objM.listarea );
                objConexao.AdicionarParametro("P_ID_ASSINATURA", objM.id_assinatura);
                
                bool ret= objConexao.ExecutarNonQuery("SAU_PKG_ASSINATURA.INSERIR_ASSINATURA_AREA");
                mensagem="Registro inserido com sucesso";
                return ret;

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public bool InsertVigencia(out string mensagem, Assinatura objM)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);
                objConexao.AdicionarParametro("P_DT_VIGENCIA", objM.dt_vigencia);
                objConexao.AdicionarParametro("P_ID_ASSINATURA", objM.id_assinatura);
                objConexao.AdicionarParametro("P_VALOR", objM.valor_assinat);

                mensagem = "Registro atualizado com sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_ASSINATURA.INSERIR_VALOR_VIGENCIA");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }

        public DataTable SelectVal(Assinatura objM)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_ASSINATURA", objM.id_assinatura);

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ASSINATURA.LISTAR_VALORES");

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
