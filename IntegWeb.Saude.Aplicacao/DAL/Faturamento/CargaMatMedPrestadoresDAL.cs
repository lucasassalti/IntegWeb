using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data.OracleClient;
using System.Collections;

namespace IntegWeb.Saude.Aplicacao.DAL.Faturamento
{
    public class CargaMatMedPrestadoresDAL
    {
        Resultado res = new Resultado();

        public SAUDE_EntityConn m_Db_Context = new SAUDE_EntityConn();

        #region .:VIEWS:.

        public class TB_CONVENENTE_VIEW
        {

            public decimal COD_CONVENENTE { get; set; }
            public string NOM_CONVENENTE { get; set; }

        }

        public class TB_TAB_RECURSO_VIEW
        {
            public decimal COD_TAB_MAT_MED { get; set; }
            public string DES_TAB_RECURSO { get; set; }

        }

        #endregion

        public decimal getMaxCountCarga() 
        {
            IQueryable<SAU_TBL_CARGA_REALIZADA> query;
            decimal resultado;

            try
            {
               resultado = m_Db_Context.SAU_TBL_CARGA_REALIZADA.Max(m => m.COD_CARGA);
            }
            catch (InvalidOperationException ex) 
            {
                resultado = 0;
            }

            return resultado;
        }
       
        public List<TB_CONVENENTE_VIEW> GetConvenente()
        {
            IQueryable<TB_CONVENENTE_VIEW> query;

            var convenentes = new decimal[] { 399020, 135329 };

            query = from conv in m_Db_Context.TB_CONVENENTE
                    where convenentes.Contains(conv.COD_CONVENENTE)
                    orderby conv.NOM_CONVENENTE

                    select new TB_CONVENENTE_VIEW
                    {
                        COD_CONVENENTE = conv.COD_CONVENENTE,
                        NOM_CONVENENTE = conv.NOM_CONVENENTE.Trim()
                    };

            List<TB_CONVENENTE_VIEW> lista = query.ToList();

            return lista;

        }

        public int GetProcNaoIncluido(decimal cod_carga) 
        {

            IQueryable<SAU_TBL_CARGA_REALIZADA> query;

            query = from carga in m_Db_Context.SAU_TBL_CARGA_REALIZADA
                    where carga.COD_CARGA == cod_carga
                    select carga;

            return Convert.ToInt32(query.SingleOrDefault().TOTAL_NAO_INCLUIDO);
        }

        public List<TB_TAB_RECURSO_VIEW> GetTabMatMed(decimal cod_convenente)
        {
            IQueryable<TB_TAB_RECURSO_VIEW> query;

            query = from conv in m_Db_Context.TB_TIPO_COND_CONV
                    from cont in m_Db_Context.TB_COND_CONTRAT
                    from tab in m_Db_Context.TB_TAB_RECURSO
                    where
                       conv.COD_CONVENENTE == cod_convenente
                    && conv.COD_TIPO_COND_CONT == cont.COD_TIPO_COND_CONT
                    && cont.COD_TAB_MAT_MED == tab.COD_TAB_RECURSO
                    && conv.DAT_VALIDADE == (m_Db_Context.TB_TIPO_COND_CONV.Where(cc => cc.COD_CONVENENTE == conv.COD_CONVENENTE).Max(data => data.DAT_VALIDADE))
                    orderby tab.COD_TAB_RECURSO

                    select new TB_TAB_RECURSO_VIEW
                    {
                        COD_TAB_MAT_MED = tab.COD_TAB_RECURSO,
                        DES_TAB_RECURSO = tab.DES_TAB_RECURSO.Trim()
                    };

            List<TB_TAB_RECURSO_VIEW> lista = query.ToList();
            lista.ForEach(tab => tab.DES_TAB_RECURSO = tab.COD_TAB_MAT_MED + " - " + tab.DES_TAB_RECURSO);

            return lista;

        }

        public DataTable VerificaProcNaoCadastradosIncor() 
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.PKG_CARGA_PROCEDIMENTOS.VERIFICA_PROC_N_CAD_INCOR");

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

        public DataTable VerificaProcCadastradosIncor(decimal codTabRec, DateTime datVig)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_COD_TAB_RECURSO", codTabRec);
                objConexao.AdicionarParametro("V_DAT_VIG", datVig);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PKG_CARGA_PROCEDIMENTOS.VERIFICA_PROC_CAD_INCOR");

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

        public List<SAU_TBL_CARGA_TEMP> VerificaProcNaoCadastradosAlbert()
        {
            IQueryable<SAU_TBL_CARGA_TEMP> query;

            query = from carga in m_Db_Context.SAU_TBL_CARGA_TEMP
                    where // not exists
                          !m_Db_Context.CADTBLRCORECURSOCODIGO.Any(cad => cad.RCOCODPROCEDIMENTO.Trim() == carga.COD_PROCEDIMENTO.Trim() && cad.TPACOD == "00" && cad.RCODATDESATIVACAO == null )
                    select carga;

            return query.ToList();

        }

        public List<SAU_TBL_CARGA_TEMP> VerificaProcCadastradosAlbert(decimal codTabRec, DateTime datVig) 
        {
            IQueryable<SAU_TBL_CARGA_TEMP> query;


               
                query = from proc in m_Db_Context.SAU_TBL_CARGA_TEMP
                        from cad in m_Db_Context.CADTBLRCORECURSOCODIGO
         
                        where
                        proc.COD_PROCEDIMENTO.Trim() == cad.RCOCODPROCEDIMENTO.Trim()
                     && cad.TPACOD == "00"
                     && m_Db_Context.TB_VAL_RECURSO.Any(exists => exists.COD_TAB_RECURSO == codTabRec && exists.DAT_VAL_RECURSO == datVig && exists.COD_RECURSO == cad.COD_RECURSO)
                        select proc;
       


            return query.ToList();
            
        }

        public  List<SAU_TBL_CARGA_PROCEDIMENTO> CarregarGridProc (decimal codCarga)
        {
            IQueryable<SAU_TBL_CARGA_PROCEDIMENTO> query;

            query = from proc in m_Db_Context.SAU_TBL_CARGA_PROCEDIMENTO
                    where proc.COD_CARGA == codCarga
                    select proc;
           
            return query.ToList();
        }

        public List<SAU_TBL_CARGA_REALIZADA> CarregarGridCarga(decimal codConv) 
        {
            IQueryable<SAU_TBL_CARGA_REALIZADA> query;

            query = from carga in m_Db_Context.SAU_TBL_CARGA_REALIZADA
                    where carga.COD_CONVENENTE == codConv
                    orderby  carga.COD_CARGA
                    select carga;

            return query.ToList();
        }

        public void ImportaDadosTemp(DataTable dt)
        {

            dt.Columns.Add("PREÇO_D", typeof(Decimal));

            foreach (DataRow dr in dt.Rows)
            {

                    dr["CÓDIGO"] = dr["CÓDIGO"].ToString().Trim();
                    dr["DESCRIÇÃO"] = dr["DESCRIÇÃO"].ToString().Trim();
                    dr["PREÇO_D"] = Math.Round(Convert.ToDecimal(dr["PREÇO"]), 2);

            }

            dt.Columns.Remove("PREÇO");

            ExcluirLinhasDuplicadas(dt, "CÓDIGO");


                using (Oracle.DataAccess.Client.OracleBulkCopy bulkCopy = new Oracle.DataAccess.Client.OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
                {

                    bulkCopy.DestinationTableName = "OWN_FUNCESP.SAU_TBL_CARGA_TEMP";
                    bulkCopy.ColumnMappings.Add("CÓDIGO", "cod_procedimento");
                    bulkCopy.ColumnMappings.Add("DESCRIÇÃO", "descricao");
                    bulkCopy.ColumnMappings.Add("PREÇO_D", "preco");


                    try
                    {
                        bulkCopy.WriteToServer(dt);

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("importação da planilha: " + ex.Message);
                    }
                    finally
                    {
                        bulkCopy.Dispose();
                        bulkCopy.Close();
                    }
                }

        }
           
        public void CadastrarCarga(decimal codConv, out int totalCad, out int totalAtu, out int qtdEspc, out int reCobertura, decimal cod_tab_recurso, System.DateTime datVigencia, decimal codTipCarga)
        {


            ConexaoOracle objConexao = new ConexaoOracle();
           

            objConexao.AdicionarParametro("V_COD_TAB_RECURSO",cod_tab_recurso);
            objConexao.AdicionarParametro("V_DAT_VIG",datVigencia );
            objConexao.AdicionarParametro("V_COD_TIP_CARGA", codTipCarga);
            objConexao.AdicionarParametroOut("V_TOTAL_CAD", OracleType.Number);
            objConexao.AdicionarParametroOut("V_TOTAL_ATU", OracleType.Number);
            objConexao.AdicionarParametroOut("V_QTDE_REG_REC_COB",OracleType.Number);
            objConexao.AdicionarParametroOut("V_QTDE_REG_PRD_ESPC", OracleType.Number);

            if (codConv == 399020) 
            {
              objConexao.ExecutarNonQuery("OWN_FUNCESP.PKG_CARGA_PROCEDIMENTOS.PRC_CARGA_ALBERT_EINSTEIN");
            }
            else if (codConv == 135329)
            {
              objConexao.ExecutarNonQuery("OWN_FUNCESP.PKG_CARGA_PROCEDIMENTOS.PRC_CARGA_INCOR");
            }
            
            

            List<OracleParameter> parametrosSaida = objConexao.ReturnParemeter();

            totalCad = Convert.ToInt32(parametrosSaida[0].Value.ToString());
            totalAtu = Convert.ToInt32(parametrosSaida[1].Value.ToString());
            reCobertura = Convert.ToInt32(parametrosSaida[2].Value.ToString());
            qtdEspc = Convert.ToInt32(parametrosSaida[3].Value.ToString());
        }
        
        public Resultado Savechanges()
        {

            Resultado res = new Entidades.Resultado();
            try
            {
                if (m_Db_Context.SaveChanges() > 0)
                {
                    res.Sucesso("Registro inserido com sucesso.");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));

            }

            return res;
        }

        public void InserirCargaRealizada(int qtdeProc,SAU_TBL_CARGA_REALIZADA tabela) 
        {
            int verifica = 0;

                 verifica = m_Db_Context.SAU_TBL_CARGA_REALIZADA.Where( t => t.COD_CARGA == tabela.COD_CARGA && t.DAT_VIG == tabela.DAT_VIG && t.COD_CONVENENTE == tabela.COD_CONVENENTE).Count();

                if (verifica == 0) 
                {
                    try 
                    {
                        m_Db_Context.SAU_TBL_CARGA_REALIZADA.Add(tabela);
                        //m_Db_Context.SaveChanges();
                    }
                    catch(Exception ex)
                    {

                        res.Erro(Util.GetInnerException(ex));
                    }
                  
                }
            

        }

        public void InserirCargaProcedimento(DataTable dt)
        {
            
            using (Oracle.DataAccess.Client.OracleBulkCopy bulkCopy = new Oracle.DataAccess.Client.OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {

                bulkCopy.DestinationTableName = "SAU_TBL_CARGA_PROCEDIMENTO";
                bulkCopy.ColumnMappings.Add("cod_carga"       , "cod_carga");
                bulkCopy.ColumnMappings.Add("cod_procedimento", "cod_procedimento");
                bulkCopy.ColumnMappings.Add("descricao"       , "descricao");
                bulkCopy.ColumnMappings.Add("preco"           , "preco");
                bulkCopy.ColumnMappings.Add("obs_erro"        , "obs_erro");


                try
                {
                    bulkCopy.WriteToServer(dt);

                }
                catch (Exception ex)
                {

                    throw new Exception("Salvando procedimentos: " + ex.Message);
                }
                finally
                {
                    bulkCopy.Dispose();
                    bulkCopy.Close();
                }
            }
                }
            
        public void DeleteMatMed()
        {

            try
            {
               m_Db_Context.Database.ExecuteSqlCommand("delete from own_funcesp.sau_tbl_carga_temp");

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));

            }
            
        }

        public List<SAU_TBL_CARGA_TEMP> GetProcTemp()
        {

            IQueryable<SAU_TBL_CARGA_TEMP> query;

            query = from carga in m_Db_Context.SAU_TBL_CARGA_TEMP
                    select carga;

            return query.ToList();
        }

        public DataTable ExcluirLinhasDuplicadas(DataTable dt, string coluna) 
        {
            ArrayList RegistroUnico = new ArrayList();
            ArrayList RegistroDuplicado = new ArrayList();

            foreach(DataRow linha in dt.Rows)
            {
                if (RegistroUnico.Contains(linha[coluna]))
                {
                    RegistroDuplicado.Add(linha);
                }
                else 
                {
                    RegistroUnico.Add(linha[coluna]);
                }
            }

            foreach(DataRow linha in RegistroDuplicado)
            {
                dt.Rows.Remove(linha);
            }

            return dt;
        }
    }
    }
