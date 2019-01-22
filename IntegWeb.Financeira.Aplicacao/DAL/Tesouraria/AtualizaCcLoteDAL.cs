using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL.Tesouraria
{
    public class AtualizaCcLoteDAL
    {
        public class FUN_VW_PERFIL
        {
            public string PERFIL { get; set; }

        }

        public class FUN_TBL_ATU_CC_HIST_view
        {
            public Nullable<System.DateTime> DAT_PROCESSAMENTO { get; set; }
            public short COD_EMPRS { get; set; }
            public int NUM_RGTRO_EMPRG { get; set; }
            public Nullable<int> NUM_IDNTF_RPTANT { get; set; }
            public string NOME { get; set; }
            public Nullable<long> NUM_CPF_EMPRG { get; set; }
            public Nullable<short> COD_BANCO { get; set; }
            public Nullable<int> COD_AGBCO { get; set; }
            public string TIP_CTCOR_EMPRG { get; set; }
            public string NUM_CTCOR_EMPRG { get; set; }
            public string CRITICA { get; set; }
        }

        public EntitiesConn m_DbContext = new EntitiesConn();

        public List<FUN_TBL_ATU_CC_HIST> GetData(int startRowIndex, int maximumRows, short? emp, DateTime? datProcessamentoIni, DateTime? datProcessamentoFim, int? matricula, int? representante, string nome, Int64? cpf, short? codBanco, int? codAgencia, string tipConta, string numConta, string sortParameter)
        {
            return GetWhere(emp, datProcessamentoIni, datProcessamentoFim, matricula, representante, nome, cpf, codBanco, codAgencia, tipConta, numConta)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<FUN_TBL_ATU_CC_HIST> GetWhere(short? emp, DateTime? datProcessamentoIni, DateTime? datProcessamentoFim, int? matricula, int? representante, string nome, Int64? cpf, short? codBanco, int? codAgencia, string tipConta, string numConta)
        {
            IQueryable<FUN_TBL_ATU_CC_HIST> query;

            query = from u in m_DbContext.FUN_TBL_ATU_CC_HIST
                    where (u.COD_EMPRS == emp || emp == null)
                     && (u.DAT_PROCESSAMENTO >= datProcessamentoIni || datProcessamentoIni == null)
                     && (u.DAT_PROCESSAMENTO <= datProcessamentoFim || datProcessamentoFim == null)
                    && (u.NUM_RGTRO_EMPRG == matricula || matricula == null)
                    && (u.NUM_IDNTF_RPTANT == representante || representante == null)
                    && (u.NOME.ToUpper().Contains(nome.ToUpper()) || nome == null)
                    && (u.NUM_CPF_EMPRG == cpf || cpf == null)
                    && (u.COD_BANCO == codBanco || codBanco == null)
                    && (u.COD_AGBCO == codAgencia || codAgencia == null)
                    && (u.TIP_CTCOR_EMPRG.ToUpper().Contains(tipConta.ToUpper()) || tipConta == null)
                    && (u.NUM_CTCOR_EMPRG.ToUpper().Contains(numConta.ToUpper()) || numConta == null)
                    orderby u.DAT_PROCESSAMENTO descending
                    select u;

            return query;
        }

        public List<FUN_TBL_ATU_CC_HIST> GetDataHist(int startRowIndex, int maximumRows, string sortParameter, string pDT_ATU_CONTAS, bool pCHK_CRITICA)
        {
            return GetWhereHist(pDT_ATU_CONTAS, pCHK_CRITICA).GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<FUN_TBL_ATU_CC_HIST> GetWhereHist(string pDT_ATU_CONTAS, bool pCHK_CRITICA)
        {
            IQueryable<FUN_TBL_ATU_CC_HIST> query;

            //query = from u in m_DbContext.FUN_TBL_ATU_CC_HIST
            //        where (u.DAT_PROCESSAMENTO == dtProcess && dtProcess != null || dtProcess == null)
            //        select u;
            if (pCHK_CRITICA == false)
            {
                if (pDT_ATU_CONTAS != "TODOS")
                {
                    query = m_DbContext.FUN_TBL_ATU_CC_HIST.Where(u => u.NOME_ARQ == pDT_ATU_CONTAS);
                }
                else
                {
                    query = m_DbContext.FUN_TBL_ATU_CC_HIST.Where(u => u.NOME_ARQ != null);
                }
            }
            else
            {
                if (pDT_ATU_CONTAS != "TODOS")
                {
                    query = m_DbContext.FUN_TBL_ATU_CC_HIST.Where(u => u.NOME_ARQ == pDT_ATU_CONTAS
                                                         && u.CRITICA.Contains("Erro"));
                }
                else
                {
                    query = m_DbContext.FUN_TBL_ATU_CC_HIST.Where(u => u.NOME_ARQ != null
                                                                  && u.CRITICA.Contains("Erro"));
                }
               
            }

            return query;
        }

        public int GetDataCountHist(string pDT_ATU_CONTAS, bool pCHK_CRITICA)
        {
            return GetWhereHist(pDT_ATU_CONTAS,pCHK_CRITICA).Count();
        }

        public int GetCriticasHist(string pDT_ATU_CONTAS, bool pCHK_CRITICA)
        {
            return GetWhereHist(pDT_ATU_CONTAS,pCHK_CRITICA).Where(r => r.CRITICA.Contains("Erro")).SelectCount();
        }

        public IQueryable<FUN_TBL_ATU_CC_HIST_view> GetWhereExcel(short? emp, DateTime? datProcessamentoIni, DateTime? datProcessamentoFim, int? matricula, int? representante, string nome, Int64? cpf, short? codBanco, int? codAgencia, string tipConta, string numConta)
        {
            IQueryable<FUN_TBL_ATU_CC_HIST_view> query;

            query = from h in m_DbContext.FUN_TBL_ATU_CC_HIST
                    where (h.COD_EMPRS == emp || emp == null)
                     && (h.DAT_PROCESSAMENTO >= datProcessamentoIni || datProcessamentoIni == null)
                     && (h.DAT_PROCESSAMENTO <= datProcessamentoFim || datProcessamentoFim == null)
                    && (h.NUM_RGTRO_EMPRG == matricula || matricula == null)
                    && (h.NUM_IDNTF_RPTANT == representante || representante == null)
                    && (h.NOME.ToUpper().Contains(nome.ToUpper()) || nome == null)
                    && (h.NUM_CPF_EMPRG == cpf || cpf == null)
                    && (h.COD_BANCO == codBanco || codBanco == null)
                    && (h.COD_AGBCO == codAgencia || codAgencia == null)
                    && (h.TIP_CTCOR_EMPRG.ToUpper().Contains(tipConta.ToUpper()) || tipConta == null)
                    && (h.NUM_CTCOR_EMPRG.ToUpper().Contains(numConta.ToUpper()) || numConta == null)
                    select new FUN_TBL_ATU_CC_HIST_view()
                    {
                        DAT_PROCESSAMENTO = h.DAT_PROCESSAMENTO,
                        COD_EMPRS = h.COD_EMPRS,
                        NUM_RGTRO_EMPRG = h.NUM_RGTRO_EMPRG,
                        NUM_IDNTF_RPTANT = h.NUM_IDNTF_RPTANT,
                        NOME = h.NOME,
                        NUM_CPF_EMPRG = h.NUM_CPF_EMPRG,
                        COD_BANCO = h.COD_BANCO,
                        COD_AGBCO = h.COD_AGBCO,
                        TIP_CTCOR_EMPRG = h.TIP_CTCOR_EMPRG,
                        NUM_CTCOR_EMPRG = h.NUM_CTCOR_EMPRG,
                        CRITICA = h.CRITICA
                    };

            return query;
        }

        public int GetDataCount(short? emp, DateTime? datProcessamentoIni, DateTime? datProcessamentoFim, int? matricula, int? representante, string nome, Int64? cpf, short? codBanco, int? codAgencia, string tipConta, string numConta)
        {
            return GetWhere(emp, datProcessamentoIni, datProcessamentoFim, matricula, representante, nome, cpf, codBanco, codAgencia, tipConta, numConta).Count();
        }

        public Resultado AtualizaCcLote()
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Resultado res = new Resultado();
            try
            {

                bool result = objConexao.ExecutarNonQuery("OWN_FUNCESP.FUN_PKG_ATUALIZA_CC_LOTE.PRC_ATUALIZA_CC");

                if (result == true)
                {
                    res.Sucesso("Processamento Feito com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            finally
            {
                objConexao.Dispose();
            }

            return res;
        }

        public void Inserir(FUN_TBL_ATU_CC obj)
        {
            obj.ID_REG = GetMaxPk();
            m_DbContext.FUN_TBL_ATU_CC.Add(obj);
            m_DbContext.SaveChanges();
        }

        public void InserirHist(FUN_TBL_ATU_CC_HIST obj)
        {
            obj.ID_REG_HIST = GetMaxPkHist();
            obj.DAT_PROCESSAMENTO = DateTime.Now;
            m_DbContext.FUN_TBL_ATU_CC_HIST.Add(obj);
            m_DbContext.SaveChanges();
        }

        public void Delete()
        {


            try
            {
                int delete = m_DbContext.Database.ExecuteSqlCommand("delete own_funcesp.fun_tbl_atu_cc");

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public string VerificaPerfil(short emp, int matricula)
        {
            IEnumerable<FUN_VW_PERFIL> IEnum = m_DbContext.Database.SqlQuery<FUN_VW_PERFIL>("select att.fcesp_perfil_participante.fcesp_des_perfil(null," + emp + "," + matricula + ") PERFIL from dual ", 0);

            string perfil = IEnum.FirstOrDefault().PERFIL;

            return perfil;
        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.FUN_TBL_ATU_CC.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public decimal GetMaxPkHist()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.FUN_TBL_ATU_CC_HIST.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG_HIST) + 1;
            return maxPK;
        }

        public AGENCIA ValidaAgencia(short codBanco, int codAgencia)
        {
            IQueryable<AGENCIA> query;
            query = from e in m_DbContext.AGENCIA
                    where (e.COD_BANCO == codBanco || codBanco == null)
                       && (e.COD_AGBCO == codAgencia || codAgencia == null)
                    select e;

            return query.FirstOrDefault();
        }

        public bool ValidaBanco(short codBanco)
        {
            bool banco = false;

            IQueryable<short> query;
            query = from e in m_DbContext.AGENCIA
                    where (e.COD_BANCO == codBanco || codBanco == null)
                    select e.COD_BANCO;

            if (query.Count() > 0)
                banco = true;


            return banco;
        }

        public EMPRESA ValidaEmpresa(short codEmpresa)
        {
            IQueryable<EMPRESA> query;
            query = from e in m_DbContext.EMPRESA
                    where (e.COD_EMPRS == codEmpresa || codEmpresa == null)
                    select e;

            return query.FirstOrDefault();
        }

        public string GetNomeParticipante(short emp, int matricula, Int64 cpf)
        {
            IQueryable<string> query = from e in m_DbContext.EMPREGADO
                                       where (e.COD_EMPRS == emp)
                                       && (e.NUM_RGTRO_EMPRG == matricula)
                                       && (e.NUM_CPF_EMPRG == cpf)
                                       select e.NOM_EMPRG;

            if (query.FirstOrDefault() != null)
                return query.FirstOrDefault().ToString();
            else
                return "";
        }

        public string GetNomeParticipanteDep(short emp, int matricula, Int64 cpf)
        {
            IQueryable<string> query = from d in m_DbContext.DEPENDENTE
                                       from empd in m_DbContext.EMPRG_DPDTE
                                       where d.NUM_IDNTF_DPDTE == empd.NUM_IDNTF_DPDTE
                                       && empd.COD_EMPRS == emp
                                       && empd.NUM_RGTRO_EMPRG == matricula
                                       && d.NUM_CPF_DPDTE == cpf
                                       select d.NOM_DPDTE;

            if (query.FirstOrDefault() != null)
                return query.FirstOrDefault().ToString();
            else
                return "";
        }

        public string GetNomeRepresentanteExt(int nRep, Int64 cpf)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();
            string numRep = "";

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select r.num_idntf_rptant from representante_fss r, repres_depend_fss d where r.num_idntf_rptant = d.num_idntf_rptant and d.num_idntf_rptant = " + nRep + " and r.num_cpf_rptant = " + cpf );
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }

            if (dt.Rows.Count > 0)
                numRep = dt.Rows[0][0].ToString();

            return numRep;
        }

        public string GetContaAtivaTit(short emp, int matricula, short codBanco, int codAgencia, string numeroConta, string tipoConta)
        {
            string dataContaAtiva = "";

            if (String.IsNullOrEmpty(tipoConta))
            {
                IQueryable<DateTime?> query = from z in m_DbContext.HIST_CADASTRO_FSS
                                              where (z.COD_EMPRS == emp)
                                              && (z.COD_BANCO == codBanco)
                                              && (z.COD_AGBCO == codAgencia)
                                              && (z.NUM_RGTRO_EMPRG == matricula)
                                              && (z.NUM_CTCOR_HISCAD == numeroConta)
                                              && (z.TIP_CTCOR_HISCAD == null)
                                              && (z.DAT_FIMVIG_HISCAD == null)
                                              select z.DAT_INIVIG_HISCAD;

                dataContaAtiva = query.FirstOrDefault().ToString();
            }
            else
            {
                IQueryable<DateTime?> query = from z in m_DbContext.HIST_CADASTRO_FSS
                                              where (z.COD_EMPRS == emp)
                                              && (z.COD_BANCO == codBanco)
                                              && (z.COD_AGBCO == codAgencia)
                                              && (z.NUM_RGTRO_EMPRG == matricula)
                                              && (z.NUM_CTCOR_HISCAD == numeroConta)
                                              && (z.TIP_CTCOR_HISCAD.Trim() == null && tipoConta == null || z.TIP_CTCOR_HISCAD.Trim() == tipoConta.Trim())
                                              && (z.DAT_FIMVIG_HISCAD == null)
                                              select z.DAT_INIVIG_HISCAD;

                dataContaAtiva = query.FirstOrDefault().ToString();
            }


            return dataContaAtiva;
        }

        public string GetContaComplemenTit(short emp, int matricula, short codBanco, int codAgencia, string numeroConta, string tipoConta)
        {
            IQueryable<DateTime?> query = from z in m_DbContext.HIST_CADASTRO_COMPLEM_FSS
                                          where (z.COD_EMPRS == emp)
                                          && (z.COD_BANCO == codBanco)
                                          && (z.COD_AGBCO == codAgencia)
                                          && (z.NUM_RGTRO_EMPRG == matricula)
                                          && (z.NUM_CTCOR_HSCDCP == numeroConta)
                                          && (z.TIP_CTCOR_HSCDCP.Trim() == null && tipoConta == null || z.TIP_CTCOR_HSCDCP.Trim() == tipoConta.Trim())
                                          && (z.DAT_FIMVIG_HSCDCP == null)
                                          select z.DAT_INIVIG_HSCDCP;

            return query.FirstOrDefault().ToString();
        }

        public string GetContaAtivaCorporativo(short emp,int? numRepresentante,int? numDependente , int matricula, short codBanco, int codAgencia, string numeroConta, string tipoConta)
        {

            string dataConta = "";

            // numero representante

            if (String.IsNullOrEmpty(tipoConta))
            {
                tipoConta = null;
            }

            if (numRepresentante == 0)
            {
                numRepresentante = null;
            }

            if(numDependente == 0)
            {
                numDependente = null;
            }

            if (numDependente == null)
            {
                IQueryable<DateTime?> query = from z in m_DbContext.HIST_CADASTRO_FSS
                                              where (z.COD_EMPRS == emp)
                                              && (z.COD_BANCO == codBanco)
                                              && (z.COD_AGBCO == codAgencia)
                                              && (z.NUM_RGTRO_EMPRG == matricula)
                                              && (z.NUM_CTCOR_HISCAD == numeroConta)
                                              && (z.TIP_CTCOR_HISCAD.Trim() == null && tipoConta == null || z.TIP_CTCOR_HISCAD.Trim() == tipoConta.Trim())
                                              && (z.NUM_IDNTF_RPTANT == null && numRepresentante == null || z.NUM_IDNTF_RPTANT == numRepresentante)
                                              && (z.NUM_IDNTF_DPDTE == null)
                                              && (z.DAT_FIMVIG_HISCAD == null)
                                              select z.DAT_INIVIG_HISCAD;

                dataConta = query.FirstOrDefault().ToString();
            }
            else
            {
                IQueryable<DateTime?> query = from z in m_DbContext.HIST_CADASTRO_FSS
                                              where (z.COD_EMPRS == emp)
                                              && (z.COD_BANCO == codBanco)
                                              && (z.COD_AGBCO == codAgencia)
                                              && (z.NUM_RGTRO_EMPRG == matricula)
                                              && (z.NUM_CTCOR_HISCAD == numeroConta)
                                              && (z.TIP_CTCOR_HISCAD.Trim() == null && tipoConta == null || z.TIP_CTCOR_HISCAD.Trim() == tipoConta.Trim())
                                              && (z.NUM_IDNTF_DPDTE == null && numDependente == null || z.NUM_IDNTF_DPDTE == numDependente)
                                              && (z.DAT_FIMVIG_HISCAD == null)
                                              select z.DAT_INIVIG_HISCAD;
                dataConta = query.FirstOrDefault().ToString();
            }



            return dataConta;

        }


        public string GetContaAtivaComplementar(short emp, int? numRepresentante, int? numDepend, int matricula, short codBanco, int codAgencia, string numeroConta, string tipoConta)
        {
            string dataConta = "";

            if (String.IsNullOrEmpty(tipoConta))
            {
                tipoConta = null;
            }

            if (numDepend == 0)
            {

                IQueryable<DateTime?> query = from x in m_DbContext.HIST_CADASTRO_COMPLEM_FSS
                                              where (x.COD_BANCO == codBanco)            //(x.COD_EMPRS == emp)
                                              && (x.COD_AGBCO == codAgencia)
                                                  // && (x.NUM_RGTRO_EMPRG == matricula)
                                              && (x.NUM_CTCOR_HSCDCP == numeroConta)
                                              && (x.NUM_IDNTF_RPTANT == null && numRepresentante == null || x.NUM_IDNTF_RPTANT == numRepresentante)
                                              && (x.TIP_CTCOR_HSCDCP.Trim() == null && tipoConta == null || x.TIP_CTCOR_HSCDCP.Trim() == tipoConta.Trim())
                                              && (x.DAT_FIMVIG_HSCDCP == null)
                                              select x.DAT_INIVIG_HSCDCP;

                dataConta = query.FirstOrDefault().ToString();    

            }
            else
            {
                IQueryable<DateTime?> query = from x in m_DbContext.HIST_CADASTRO_COMPLEM_FSS
                                              where (x.COD_BANCO == codBanco)            //(x.COD_EMPRS == emp)
                                              && (x.COD_AGBCO == codAgencia)
                                                  // && (x.NUM_RGTRO_EMPRG == matricula)
                                              && (x.NUM_CTCOR_HSCDCP == numeroConta)
                                              && (x.NUM_IDNTF_DPDTE == null && numDepend == null || x.NUM_IDNTF_DPDTE == numDepend)
                                              && (x.TIP_CTCOR_HSCDCP.Trim() == null && tipoConta == null || x.TIP_CTCOR_HSCDCP.Trim() == tipoConta.Trim())
                                              && (x.DAT_FIMVIG_HSCDCP == null)
                                              select x.DAT_INIVIG_HSCDCP;
                dataConta = query.FirstOrDefault().ToString();    
            }




            return dataConta;        
        }

        public Int32 GetNumDepend(short emp, int matricula, Int64 cpf)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();
            Int32 numDepend = 0;

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select DEP.NUM_IDNTF_DPDTE from emprg_dpdte dep, dependente dd where dep.num_idntf_dpdte = dd.num_idntf_dpdte and dep.cod_emprs = " + emp + " and dep.num_rgtro_emprg = " + matricula + " and dd.num_cpf_dpdte = " + cpf);
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }

            if (dt.Rows.Count > 0)
               numDepend = Convert.ToInt32(dt.Rows[0][0].ToString());


            return numDepend;

        }

        public Int32 GetNumRepr(short emp, int matricula, Int32 numRep)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();
            Int32 numRepr = 0;

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select distinct r.num_idntf_rptant from representante_fss r, repres_depend_fss d where r.num_idntf_rptant = d.num_idntf_rptant and d.cod_emprs = " + emp + " and d.num_rgtro_emprg = " + matricula + " and d.num_idntf_rptant = " + numRep);
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }

            if (dt.Rows.Count > 0)
                numRepr = Convert.ToInt32(dt.Rows[0][0].ToString());


            return numRepr;
        }


        public Resultado AtualizaCcComplementar()
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Resultado res = new Resultado();
            try
            {

                bool result = objConexao.ExecutarNonQuery("OWN_FUNCESP.FUN_PKG_ATUALIZA_CC_LOTE.PRC_ATUALIZA_CC_COMPLEMEN");

                if (result == true)
                {
                    res.Sucesso("Processamento Feito com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            finally
            {
                objConexao.Dispose();
            }

            return res;
        }

        public DataTable GetListaDataDT_ATU_CONTAS()
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select distinct h.nome_arq from own_funcesp.fun_tbl_atu_cc_hist h order by h.nome_arq");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }

           


            return dt;
   
                  

        }

    }
}
