using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Framework;
using IntegWeb.Entidades;
using System.Data;
using System.Data.OracleClient;
using System.Data.Entity.Validation;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class CAD_TBL_CONTROLEUNIMED_view
    {
        public System.DateTime DAT_MOVIMENTACAO { get; set; }
        public string COD_IDENTIFICACAO { get; set; }
        public string NOM_PARTICIP { get; set; }
        //    public string NOM_MAE_EMPRG { get; set; }
        //  public string TITULAR { get; set; }
        //   public Nullable<System.DateTime> DAT_NASCIMENTO { get; set; }
        //   public string COD_SEXO { get; set; }
        //   public string DCR_ESTCV { get; set; }
        //   public string DCR_GRADPC { get; set; }
        //  public Nullable<long> CPF { get; set; }
        // public string ACOMODACAO { get; set; }
        public string DES_PLANO { get; set; }
        //  public string COD_UNDFD_EMPRG { get; set; }
        // public Nullable<System.DateTime> DAT_INI_PERIODO { get; set; }
        // public Nullable<System.DateTime> DAT_FIN_PERIDO { get; set; }
        public Nullable<System.DateTime> DAT_SAIDA { get; set; }
        public string TIPO { get; set; }
        //    public string USU_GERACAO { get; set; }
        // public Nullable<System.DateTime> DT_HIST { get; set; }
        // public string NUM_CI_EMPRG { get; set; }
        //  public string COD_UNIMED { get; set; }
        //  public Nullable<System.DateTime> DAT_ADESAO { get; set; }
        //   public string COD_PLANO_CESP { get; set; }
        public short COD_EMPRS { get; set; }
        public int NUM_MATRICULA { get; set; }
        public string SUB_MATRICULA { get; set; }
        //  public Nullable<System.DateTime> DAT_CANCELAMENTO { get; set; }
        public string COBRANCA_MEMORIAL { get; set; }
        public string NUMERO_UNIMED { get; set; }
        public Nullable<int> NUMERO_VIA { get; set; }
    }

    public class ControleUnimedDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public List<CAD_TBL_CONTROLEUNIMED> GetData(int startRowIndex, int maximumRows, short? emp, int? matricula, string sub, string nome, string codIdentificacao, string codUnimed, string tipMovimentacao, DateTime? datMovimentacaoIni, DateTime? datMovimentacaoFim, DateTime? datSaidaIni, DateTime? datSaidaFim, string sortParameter)
        {
            return GetWhere(emp, matricula, sub, nome, codIdentificacao, codUnimed, tipMovimentacao, datMovimentacaoIni, datMovimentacaoFim, datSaidaIni, datSaidaFim)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<CAD_TBL_CONTROLEUNIMED> GetWhere(short? emp, int? matricula, string sub, string nome, string codIdentificacao, string codUnimed, string tipMovimentacao, DateTime? datMovimentacaoIni, DateTime? datMovimentacaoFim, DateTime? datSaidaIni, DateTime? datSaidaFim)
        {
            IQueryable<CAD_TBL_CONTROLEUNIMED> query;

            query = from u in m_DbContext.CAD_TBL_CONTROLEUNIMED
                    where (u.COD_EMPRS == emp || emp == null)
                    && (u.NUM_MATRICULA == matricula || matricula == null)
                    && (u.SUB_MATRICULA == sub || sub == null)
                    && (u.NOM_PARTICIP.ToUpper().Contains(nome.ToUpper()) || nome == null)
                    && (u.COD_IDENTIFICACAO.Trim().Contains(codIdentificacao.Trim()) || codIdentificacao == null)
                    && (u.COD_UNIMED == codUnimed || codUnimed == null)
                    && (u.TIPO.ToLower().Contains(tipMovimentacao.ToLower()) || tipMovimentacao == null)
                    && (u.DAT_GERACAO >= datMovimentacaoIni || datMovimentacaoIni == null)
                    && (u.DAT_GERACAO <= datMovimentacaoFim || datMovimentacaoFim == null)
                    && (u.ENVIO_UNIMED >= datSaidaIni || datSaidaIni == null)
                    && (u.ENVIO_UNIMED <= datSaidaFim || datSaidaFim == null)
                    select u;

            return query;
        }

        public Resultado AtualizaTabelaControleUnimed(CAD_TBL_CONTROLEUNIMED obj)
        {
            Resultado res = new Resultado();

            try
            {

                var atualiza = m_DbContext.CAD_TBL_CONTROLEUNIMED.Find(obj.COD_CONTROLEUNIMED);

                if (atualiza != null)
                {

                    atualiza.ENVIO_UNIMED = obj.ENVIO_UNIMED;
                    atualiza.COBRANCA_MEMORIAL = obj.COBRANCA_MEMORIAL;
                    atualiza.NUMERO_UNIMED = obj.NUMERO_UNIMED;
                    atualiza.NUMERO_VIA = obj.NUMERO_VIA;

                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso");
                    }
                }

            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public int GetDataCount(short? emp, int? matricula, string sub, string nome, string codIdentificacao, string codUnimed, string tipMovimentacao, DateTime? datMovimentacaoIni, DateTime? datMovimentacaoFim, DateTime? datSaidaIni, DateTime? datSaidaFim)
        {
            return GetWhere(emp, matricula, sub, nome, codIdentificacao, codUnimed, tipMovimentacao, datMovimentacaoIni, datMovimentacaoFim, datSaidaIni, datSaidaFim).Count();
        }

        public List<CAD_TBL_UNIMEDARQUIVO> GetUnimed()
        {
            IQueryable<CAD_TBL_UNIMEDARQUIVO> query;

            query = from un in m_DbContext.CAD_TBL_UNIMEDARQUIVO
                    select un;

            return query.ToList();
        }

        public List<SAU_TBL_VALORCARTUNIMED> GetValorCI()
        {
            IQueryable<SAU_TBL_VALORCARTUNIMED> query;

            query = from v in m_DbContext.SAU_TBL_VALORCARTUNIMED
                    where (v.DAT_FIM_VIGENCIA == null)
                    && (v.DAT_INICIO_VIGENCIA == (m_DbContext.SAU_TBL_VALORCARTUNIMED.Where(v1 => v1.ID_REG == v.ID_REG).Max(v2 => v2.DAT_INICIO_VIGENCIA)))
                    select v;

            return query.ToList();
        }

        public Resultado AtualizaTabelaCI(SAU_TBL_VALORCARTUNIMED obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.SAU_TBL_VALORCARTUNIMED.Find(obj.ID_REG);

                if (atualiza != null)
                {
                    atualiza.DAT_FIM_VIGENCIA = DateTime.Now;

                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado InsereTabelaUnimed(CAD_TBL_UNIMEDARQUIVO obj)
        {
            Resultado res = new Resultado();

            try
            {
                m_DbContext.CAD_TBL_UNIMEDARQUIVO.Add(obj);
                int rows_insert = m_DbContext.SaveChanges();

                if (rows_insert > 0)
                {
                    res.Sucesso("Inclusão Feita com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            return res;

        }

        public Resultado InserirTabelaCI(SAU_TBL_VALORCARTUNIMED obj)
        {
            Resultado res = new Resultado();

            try
            {
                obj.ID_REG = GetMaxPkValorCI();
                obj.DAT_INICIO_VIGENCIA = DateTime.Now;
                obj.DAT_FIM_VIGENCIA = null; // Valor ativo

                m_DbContext.SAU_TBL_VALORCARTUNIMED.Add(obj);
                int rows_insert = m_DbContext.SaveChanges();

                if (rows_insert > 0)
                {
                    res.Sucesso("Inclusão Feita com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        public Resultado Inserir(CAD_TBL_CONTROLEUNIMED obj)
        {
            Resultado res = new Resultado();

            try
            {
                obj.COD_CONTROLEUNIMED = GetMaxPk();
                m_DbContext.CAD_TBL_CONTROLEUNIMED.Add(obj);
                int rows_insert = m_DbContext.SaveChanges();

                if (rows_insert > 0)
                {
                    res.Sucesso("Inclusão Feita com Sucesso");
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

        public Resultado DeleteUnimedExistente(CAD_TBL_UNIMEDARQUIVO obj)
        {
            Resultado res = new Resultado();

            try
            {
                var delete = m_DbContext.CAD_TBL_UNIMEDARQUIVO.Find(obj.COD_PLANO);

                if (delete != null)
                {
                    m_DbContext.CAD_TBL_UNIMEDARQUIVO.Remove(delete);
                    int rows_delete = m_DbContext.SaveChanges();

                    if (rows_delete > 0)
                    {
                        res.Sucesso("Registro Deletado com Sucesso");
                    }
                }

            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado DeleteTabelaCI(SAU_TBL_VALORCARTUNIMED obj)
        {
            Resultado res = new Resultado();

            try
            {
                List<SAU_TBL_VALORCARTUNIMED> delete = m_DbContext.SAU_TBL_VALORCARTUNIMED.Where(vl => vl.COD_PLANO == obj.COD_PLANO).ToList();

                if (delete.Count > 0)
                {
                    for (int i = 0; i < delete.Count; i++)
                    {
                        m_DbContext.SAU_TBL_VALORCARTUNIMED.Remove(delete[i]);
                        int rows_delete = m_DbContext.SaveChanges();

                        if (rows_delete > 0)
                        {
                            res.Sucesso("Registro Deletado com Sucesso");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.CAD_TBL_CONTROLEUNIMED.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_CONTROLEUNIMED) + 1;
            return maxPK;
        }

        public decimal GetMaxPkValorCI()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.SAU_TBL_VALORCARTUNIMED.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public IQueryable<CAD_TBL_CONTROLEUNIMED_view> GetDataExportar(short? emp, int? matricula, string sub, string nome, string codIdentificacao, string codUnimed, string tipMovimentacao, DateTime? datMovimentacaoIni, DateTime? datMovimentacaoFim, DateTime? datSaidaIni, DateTime? datSaidaFim)
        {
            IQueryable<CAD_TBL_CONTROLEUNIMED_view> query;

            query = from u in m_DbContext.CAD_TBL_CONTROLEUNIMED
                    where (u.COD_EMPRS == emp || emp == null)
                    && (u.NUM_MATRICULA == matricula || matricula == null)
                    && (u.SUB_MATRICULA == sub || sub == null)
                    && (u.NOM_PARTICIP.ToUpper().Contains(nome.ToUpper()) || nome == null)
                    && (u.COD_IDENTIFICACAO.Trim().Contains(codIdentificacao.Trim()) || codIdentificacao == null)
                    && (u.COD_UNIMED == codUnimed || codUnimed == null)
                    && (u.TIPO.ToLower().Contains(tipMovimentacao.ToLower()) || tipMovimentacao == null)
                    && (u.DAT_GERACAO >= datMovimentacaoIni || datMovimentacaoIni == null)
                    && (u.DAT_GERACAO <= datMovimentacaoFim || datMovimentacaoFim == null)
                    && (u.ENVIO_UNIMED >= datSaidaIni || datSaidaIni == null)
                    && (u.ENVIO_UNIMED <= datSaidaFim || datSaidaFim == null)
                    select new CAD_TBL_CONTROLEUNIMED_view()
                    {

                        COD_EMPRS = u.COD_EMPRS,
                        NUM_MATRICULA = u.NUM_MATRICULA,
                        SUB_MATRICULA = u.SUB_MATRICULA,
                        COD_IDENTIFICACAO = u.COD_IDENTIFICACAO,
                        NOM_PARTICIP = u.NOM_PARTICIP,
                        DES_PLANO = u.DES_PLANO,
                        TIPO = u.TIPO,
                        DAT_MOVIMENTACAO = u.DAT_GERACAO,
                        DAT_SAIDA = u.ENVIO_UNIMED,
                        COBRANCA_MEMORIAL = u.COBRANCA_MEMORIAL,
                        NUMERO_UNIMED = u.NUMERO_UNIMED,
                        NUMERO_VIA = u.NUMERO_VIA
                    };

            return query;
        }

        public DataTable ExtRelInclusao(int plano, DateTime vDataFim, DateTime vDataIni)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();


            try
            {

                objConexao.AdicionarParametro("Plano", plano);
                objConexao.AdicionarParametro("vDataFim", vDataFim);
                objConexao.AdicionarParametro("vDataIni", vDataIni);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_CONTROLE_UNIMED.LISTAR_UNIMED_INCLUSAO");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ExtRelTroca(int plano, DateTime vDataFim, DateTime vDataIni)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();


            try
            {

                objConexao.AdicionarParametro("Plano", plano);
                objConexao.AdicionarParametro("vDataFim", vDataFim);
                objConexao.AdicionarParametro("vDataIni", vDataIni);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_CONTROLE_UNIMED.LISTAR_UNIMED_TROCA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ExtRelSegVia(int plano, DateTime vDataFim, DateTime vDataIni)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {

                objConexao.AdicionarParametro("Plano", plano);
                objConexao.AdicionarParametro("vDataFim", vDataFim);
                objConexao.AdicionarParametro("vDataIni", vDataIni);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_CONTROLE_UNIMED.LISTAR_UNIMED_2_VIA");

                leitor.Fill(dt);
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
            return dt;
        }

        public DataTable ExtRelCancelamento(int plano, DateTime vDataFim, DateTime vDataIni)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            DataTable dt = new DataTable();

            try
            {

                objConexao.AdicionarParametro("Plano", plano);
                objConexao.AdicionarParametro("vDataFim", vDataFim);
                objConexao.AdicionarParametro("vDataIni", vDataIni);
                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter leitor = objConexao.ExecutarAdapter("OWN_FUNCESP.FUN_PKG_CONTROLE_UNIMED.LISTAR_UNIMED_CANCELAMENTO");

                leitor.Fill(dt);
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
            return dt;
        }

        public Resultado InserirTabelaEtq(CAD_TBL_CONTROLEUNIMED_ETQ obj)
        {

            Resultado res = new Resultado();
            try
            {
                obj.ID_REG = GetMaxPkEtq();
                m_DbContext.CAD_TBL_CONTROLEUNIMED_ETQ.Add(obj);
                int rows_insert = m_DbContext.SaveChanges();

                if (rows_insert > 0)
                {
                    res.Sucesso("Inclusão Feita com Sucesso");
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

        public decimal GetMaxPkEtq()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.CAD_TBL_CONTROLEUNIMED_ETQ.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }


        public void DeleteTabelaEtq(string user)
        {
            try
            {
                int delete = m_DbContext.Database.ExecuteSqlCommand("delete OWN_FUNCESP.CAD_TBL_CONTROLEUNIMED_ETQ where USU_GERACAO = " + "'" + user + "'");

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }



        public String GetPlanoFcesp(short? emp, string matricula, string sub)
        {
            String plano;

            ConexaoOracle obj = new ConexaoOracle();


            try
            {
                plano = obj.ExecutarScalar("select p.cod_plano from tb_particip_plano h,tb_plano p where h.cod_plano = p.cod_plano and h.cod_emprs = '" + emp + "'and h.num_matricula ='" + matricula + "' and h.num_sub_matric ='" + sub + "'  and h.dat_cancelamento is null AND P.COD_PROGRAMA IN (1,2,3,8) AND P.COD_MOD_PLANO IN (5,6) AND P.COD_PLANO NOT IN (9,11,58,61) AND P.NUM_REGISTRO_PLANO IS NOT NULL ").ToString();
                
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
                      
            return plano;
        }

        
    }

   
}





