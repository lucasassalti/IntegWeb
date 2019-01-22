using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data.Objects;
using System.Data;


namespace IntegWeb.Saude.Aplicacao.BLL
{
   public class CadAnaliseSusDAL
    {
       public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

       public Resultado gravaTabela(DataTable tabela)
        {
            Resultado res = new Resultado();
            DbSet<SAU_TBL_IMPUGNACAOSUS> tbIMPUGNACAOSUS = this.m_DbContext.SAU_TBL_IMPUGNACAOSUS;
            SAU_TBL_IMPUGNACAOSUS newRow = new SAU_TBL_IMPUGNACAOSUS();
            decimal maxChave = 0m;
            if (tbIMPUGNACAOSUS.ToList<SAU_TBL_IMPUGNACAOSUS>().Count<SAU_TBL_IMPUGNACAOSUS>() > 0)
            {
                maxChave = tbIMPUGNACAOSUS.Max((SAU_TBL_IMPUGNACAOSUS m) => m.CHAVE);
            }
                                  

           if (tbIMPUGNACAOSUS.ToList().Count() > 0) {
                       maxChave = tbIMPUGNACAOSUS.Max(m => m.CHAVE);
           }
           try
           {

               foreach (DataRow drNew in tabela.Rows)
                   {
                                              
                       newRow = new SAU_TBL_IMPUGNACAOSUS();
                       newRow.NUMEROOFICIO = Util.String2Decimal(drNew["numeroOficio"].ToString());
                       newRow.NUMEROPROCESSO = drNew["numeroProcesso"].ToString();
                       newRow.NUMEROABI = drNew["numeroABI"].ToString();
                       newRow.NUMERO    = drNew["numero"].ToString();
                       newRow.TIPO      = drNew["tipo"].ToString();
                       newRow.CODIGOBENEFICIARIO = Util.String2Int64(drNew["codigobeneficiario"].ToString().Trim());
                       newRow.CODIGOCCO = drNew["codigoCCO"].ToString();
                       newRow.COMPETENCIA = Util.String2Int32(drNew["competencia"].ToString());
                       newRow.DATAINICIOATENDIMENTO = DateTime.Parse(drNew["dataInicioAtendimento"].ToString());
                       newRow.DATAFIMATENDIMENTO = DateTime.Parse(drNew["dataFimAtendimento"].ToString());
                       newRow.DATANASCBENEFICIARIO = DateTime.Parse(drNew["dataNascBeneficiario"].ToString());
                       newRow.COD_EMP = Util.String2Short(drNew["Cod_Emp"].ToString());
                       newRow.MATRICULA = Util.String2Int32(drNew["matricula"].ToString());
                       newRow.SUB_MATRICULA = Util.String2Short(drNew["Sub_Matricula"].ToString());                       
                       newRow.NOMEBENEFICIARIO = drNew["nomebeneficiario"].ToString();                       
                       //newRow.DDD = Util.String2Short(drNew["DDD"].ToString()); // Linha Do Erro
                       newRow.TEL_BENEF = Util.String2Int32(drNew["Tel_Benef"].ToString());
                       newRow.DDD_CELULAR = Util.String2Short(drNew["DDD_Celular"].ToString());
                       newRow.TEL_CELULAR_BENEF = Util.String2Int32(drNew["Tel_Celular_Benef"].ToString());
                       newRow.COD_PLANO = Util.String2Short(drNew["Cod_Plano"].ToString());
                       newRow.DESC_PLANO = drNew["Desc_Plano"].ToString();
                       newRow.CODIGOPROCEDIMENTO = Util.String2Int32(drNew["codigoProcedimento"].ToString());
                       newRow.DESCRICAOPROCEDIMENTO = drNew["descricaoProcedimento"].ToString();
                       newRow.VALORPROCEDIMENTO = Util.String2Decimal(drNew["valorProcedimento"].ToString().Trim().Replace('.', ','));
                       newRow.NOMEUPS = drNew["nomeUPS"].ToString().Trim();
                       newRow.MUNICIPIO = drNew["municipio"].ToString().Trim();
                       newRow.CODIGOUF = drNew["codigoUF"].ToString().Trim();
                       newRow.CHAVE = maxChave +1;

                       maxChave++;
                       
                       tbIMPUGNACAOSUS.Add(newRow);
                                             
                   }
                   // Salva as Informações na Base de dados
                   m_DbContext.SaveChanges(); 

           }
           catch (Exception ex)
           {
               throw;
           }
            return res;
       }


       public List<object> ConsultarABI()
       {

           SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

           IEnumerable<object> query = (from c in m_DbContext.SAU_TBL_IMPUGNACAOSUS
                       select c.NUMEROABI).Distinct();

           return query.ToList();
       }
       public List<object> ConsultarAIHPorUsuario(string pCodigoUsuario)
       {
           SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();
           decimal dCodigoUsuario = 0m;
           decimal.TryParse (pCodigoUsuario, out dCodigoUsuario);

           IEnumerable<object> query = (from c in m_DbContext.SAU_TBL_IMPUGNACAOSUS
                                        where c.CODIGOBENEFICIARIO == dCodigoUsuario
                                        select c.NUMERO).Distinct();

           return query.ToList();
       }

       
       public List<SAU_TBL_IMPUGNACAOSUS> GetData(int startRowIndex, int maximumRows, string paramCodigoUsuario, string paramBuscaAIHAPAC, string paramCompetencia, string sortParameter)
       {
           return this.GetWhere(paramCodigoUsuario, paramBuscaAIHAPAC, paramCompetencia).GetData(startRowIndex, maximumRows, sortParameter).ToList<SAU_TBL_IMPUGNACAOSUS>();
       }

       public IQueryable<SAU_TBL_IMPUGNACAOSUS> GetWhere(string paramCodigoUsuario, string paramBuscaAIHAPAC, string paramCompetencia)
       {
           decimal dNUM_ORGAO = 0m;
           decimal.TryParse(paramCodigoUsuario, out dNUM_ORGAO);
           string dAIHAPAC = paramBuscaAIHAPAC;
           decimal dCompetencia = 0m;
           decimal.TryParse(paramCompetencia, out dCompetencia);
           return from c in this.m_DbContext.SAU_TBL_IMPUGNACAOSUS
                  where ((c.CODIGOBENEFICIARIO == (decimal?)dNUM_ORGAO && paramCodigoUsuario != null) || paramCodigoUsuario == null) && ((c.NUMERO == dAIHAPAC && paramBuscaAIHAPAC != null) || paramBuscaAIHAPAC == null) && (((decimal?)c.COMPETENCIA == (decimal?)dCompetencia && paramCompetencia != null) || paramCompetencia == null)
                  select c;
       }


       public int GetDataCount(string paramCodigoUsuario, string paramBuscaAIHAPA, string paramCompetencia)
       {
           return this.GetWhere(paramCodigoUsuario, paramBuscaAIHAPA, paramCompetencia).SelectCount<SAU_TBL_IMPUGNACAOSUS>();
       }

        public Resultado SaveData(string upt_CCUSTO_DEB_UTIL, string upt_CCUSTO_CRE_UTIL, string upt_CCUSTO_DEB_GLOSA, string upt_CCUSTO_CRE_GLOSA, string upt_AUX_DEB_UTIL, string upt_AUX_CRE_UTIL, string upt_AUX_DEB_GLOSA, string upt_AUX_CRE_GLOSA, decimal key_NUM_ORGAO, string key_COD_PLANO)
        {
            Resultado res = new Resultado();

            var atualiza = m_DbContext.TB_CENTRO_CUSTO.FirstOrDefault(p => p.NUM_ORGAO == key_NUM_ORGAO && p.COD_PLANO == key_COD_PLANO);
            if (atualiza != null)
            {
                atualiza.CCUSTO_DEB_UTIL = upt_CCUSTO_DEB_UTIL;
                atualiza.CCUSTO_CRE_UTIL = upt_CCUSTO_CRE_UTIL;
                atualiza.CCUSTO_DEB_GLOSA = upt_CCUSTO_DEB_GLOSA;
                atualiza.CCUSTO_CRE_GLOSA = upt_CCUSTO_CRE_GLOSA;
                atualiza.AUX_DEB_UTIL = upt_AUX_DEB_UTIL;
                atualiza.AUX_CRE_UTIL = upt_AUX_CRE_UTIL;
                atualiza.AUX_DEB_GLOSA = upt_AUX_DEB_GLOSA;
                atualiza.AUX_CRE_GLOSA = upt_AUX_CRE_GLOSA;
                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    res.Sucesso(String.Format("{0} registros atualizados.", rows_updated));
                }
            }

            return res;
        }



    }
}
