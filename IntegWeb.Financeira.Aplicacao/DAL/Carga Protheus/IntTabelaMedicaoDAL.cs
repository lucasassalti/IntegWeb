using System;
using System.Linq;
using System.Text;
using System.Data;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Web.Services;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Financeira.Aplicacao.BLL.Carga_Protheus;


namespace IntegWeb.Financeira.Aplicacao.DAL.Carga_Protheus
{
    public class IntTabelaMedicaoDAL
    {
        EntitiesConn m_DbContext = new EntitiesConn();

        const string consultaPensaoAlimenticia = @"select distinct a.num_rgtro_emprg codigo,
                                                   b.nom_dpdte nomeRazaoSocial,
                                                   b.num_cpf_dpdte cpfCnpj,
                                                   a.num_ctcor_empdep codigoContaCorrente,
                                                   (select 'pFisica - Emprg_dpdte' from dual) tipoCadastro,
                                                   to_number(a.num_idntf_dpdte) codigo,
                                                   0 codigoEmpresa,
                                                   to_number(cod_banco) codigoBanco,
                                                   to_number(cod_agbco) codigoAgencia from EMPRG_DPDTE a                                     
                                                   left join att.dependente b on a.num_idntf_dpdte = b.num_idntf_dpdte"; // Pensões alimentícias


        const string exists = @"exists (select '' from att.desc_espec_partc_fss dep where dep.cod_emprs = a.cod_emprs
                                                                            and dep.num_rgtro_emprg = a.num_rgtro_emprg
                                                                            and dep.num_idntf_dpdte = a.num_idntf_dpdte
                                                                            and dep.dat_fimvig_dseppt is null)"; //armazena os padrões de cálculos das pensões alimentícias



        public List<SX5010> BuscaSx5010Liquidacao()
        {
            IQueryable<SX5010> query; 

            query = from x in m_DbContext.SX5010
                    where x.D_E_L_E_T_ != "*" && x.X5_TABELA == "58"
                    orderby x.X5_CHAVE
                    select x;

            return query.ToList();
        }

        public List<SX5010> BuscaSx5010Evento()
        {
            IQueryable<SX5010> query;

            query = from x in m_DbContext.SX5010
                    where x.D_E_L_E_T_ != "*" && x.X5_TABELA == "ZZ"
                    orderby x.X5_CHAVE
                    select x;

            return query.ToList();
        }

        public List<SB1010> BuscaSb1010()
        {
            IQueryable<SB1010> query;

            query = from x in m_DbContext.SB1010
                    select x;

            return query.ToList();
        }

        public List<CTD010> BuscaCtd010()
        {
            IQueryable<CTD010> query;

            query = from x in m_DbContext.CTD010
                    where x.D_E_L_E_T_ != "*" && x.CTD_CLASSE == "2"
                    orderby x.CTD_DESC01
                    select x;

            return query.ToList();
        }

        public List<CV0010> BuscaCv0010()
        {
            IQueryable<CV0010> query;

            query = from x in m_DbContext.CV0010
                    where x.D_E_L_E_T_ != "*" && x.CV0_CODIGO != " "
                    orderby x.CV0_DESC
                    select x;

            return query.ToList();
        }

        public List<CTT010> BuscaCtt010()
        {
            IQueryable<CTT010> query;

            query = from x in m_DbContext.CTT010
                    where x.D_E_L_E_T_ != "*" && x.CTT_CLASSE == "2"
                    orderby x.CTT_DESC01
                    select x;

            return query.ToList();
        }

        public List<CTH010> BuscaCth010()
        {
            IQueryable<CTH010> query;

            query = from x in m_DbContext.CTH010
                    where x.D_E_L_E_T_ != "*" && x.CTH_CLASSE == "2"
                    orderby x.CTH_DESC01
                    select x;

            return query.ToList();
        }

        public List<PRE_TBL_CARGA_PROTHEUS_TIPO> BuscaCargaProtheusTipo()
        {
            IQueryable<PRE_TBL_CARGA_PROTHEUS_TIPO> query;

            query = from x in m_DbContext.PRE_TBL_CARGA_PROTHEUS_TIPO
                    // where x.D_E_L_E_T_ != "*" && x.CTH_CLASSE == "2"
                    orderby x.COD_CARGA_TIPO
                    select x;

            return query.ToList();
        }

        #region Buscar Pessoa física/ jurídica por NOME


        public IEnumerable<PessFisicaJuridica> BuscaEmpregado(string pBusca)
        {

            int buscaLong;
            int.TryParse(pBusca, out buscaLong);


            if (buscaLong > 0)
            {
                IEnumerable<PessFisicaJuridica> empregadoPorMatricula;
                empregadoPorMatricula = m_DbContext.Database.SqlQuery<PessFisicaJuridica>(@"SELECT nom_emprg nomeRazaoSocial,
                                                                                                   to_number(num_cpf_emprg) cpfCnpj,
                                                                                                   (select 'pFisica - Empregado' from dual) tipoCadastro,
                                                                                                   cod_emprs codigoEmpresa,
                                                                                                   to_number(num_rgtro_emprg) codigo,
                                                                                                   num_ctcor_emprg codigoContaCorrente,
                                                                                                   to_number(cod_banco) codigoBanco,
                                                                                                   to_number(cod_agbco) codigoAgencia,
                                                                                                   Num_Digvr_Emprg codigoDvContaCorrente,
                                                                                                   TIP_CTCOR_EMPRG codigotipoConta
                                                                                              from EMPREGADO  where NUM_RGTRO_EMPRG = " + buscaLong);
                 
                empregadoPorMatricula.AsQueryable();

                return empregadoPorMatricula;
            }


            IEnumerable<PessFisicaJuridica> queryConvenente =
            m_DbContext.Database.SqlQuery<PessFisicaJuridica>(@"select to_number(num_cgc_cpf) cpfCnpj,
                                                                       (trim(nom_convenente) || ' - ' || COD_CONVENENTE) nomeRazaoSocial,
                                                                       num_conta_corr codigoContaCorrente,
                                                                       to_number(dgv_convenente) codigoDvContaCorrente,
                                                                       (select 'pJuridica - Convenente' from dual) tipoCadastro,
                                                                       to_number(0) codigoEmpresa,
                                                                       cod_convenente codigo,
                                                                       tip_ctcor codigotipoConta,
                                                                       to_number(cod_banco) codigoBanco,
                                                                       to_number(cod_agbco) codigoAgencia
                                                                  from tb_convenente where nom_convenente LIKE '%" + pBusca.ToUpper() + "%'");
            queryConvenente.AsQueryable();


            IQueryable<PessFisicaJuridica> queryEmpregado;
            queryEmpregado = from emp in m_DbContext.EMPREGADO
                             where emp.NOM_EMPRG.Contains(pBusca.ToUpper())
                             select new PessFisicaJuridica
                             {
                                 cpfCnpj = emp.NUM_CPF_EMPRG,
                                 nomeRazaoSocial = emp.NOM_EMPRG,
                                 tipoCadastro = "pFisica - Empregado",
                                 codigo = emp.NUM_RGTRO_EMPRG,
                                 codigoEmpresa = emp.COD_EMPRS, // Somente a consulta de empregado deve retornar código de empresa.
                                 codigoAgencia = emp.COD_AGBCO,
                                 codigoBanco = emp.COD_BANCO,
                                 codigoContaCorrente = emp.NUM_CTCOR_EMPRG,
                                 codigoDvContaCorrente = emp.NUM_DIGVR_EMPRG,
                                 codigotipoConta = emp.TIP_CTCOR_EMPRG
                             };

            IQueryable<PessFisicaJuridica> queryDependente;
            queryDependente = from dpt in m_DbContext.DEPENDENTE
                              where dpt.NOM_DPDTE.Contains(pBusca.ToUpper())
                              select new PessFisicaJuridica
                              {
                                  cpfCnpj = dpt.NUM_CPF_DPDTE,
                                  nomeRazaoSocial = dpt.NOM_DPDTE,
                                  tipoCadastro = "pFisica - Dependente",
                                  codigo = dpt.NUM_IDNTF_DPDTE,
                                  codigoEmpresa = 0,
                                  codigoAgencia = 0,
                                  codigoBanco = 0,
                                  codigoDvContaCorrente = 0,
                                  codigoContaCorrente = "0",
                                  codigotipoConta = "0"
                              };

            IQueryable<PessFisicaJuridica> queryRpUniaoFss;

            queryRpUniaoFss = from ruf in m_DbContext.REPRES_UNIAO_FSS
                              where ruf.NOM_REPRES.Contains(pBusca.ToUpper())

                              select new PessFisicaJuridica()
                              {
                                  cpfCnpj = ruf.NUM_CPF_REPRES,
                                  nomeRazaoSocial = ruf.NOM_REPRES,
                                  tipoCadastro = "pFisica - Repres_uniao_fss",
                                  codigo = ruf.NUM_IDNTF_RPTANT,
                                  codigoEmpresa = 0,
                                  codigoAgencia = ruf.COD_AGBCO,
                                  codigoBanco = ruf.COD_BANCO,
                                  codigoContaCorrente = ruf.NUM_CTCOR_REPRES,
                                  cdTipoConta = ruf.TIP_CTCOR_REPRES,
                                  codigoDvContaCorrente = 0
                              };

            foreach (var item in queryRpUniaoFss)
            {
                if (item.cdTipoConta >= 0)
                {
                    item.codigotipoConta = item.cdTipoConta.ToString();
                }
            }


            // pensões alimentícias
            IEnumerable<PessFisicaJuridica> queryEmpDpte =
            m_DbContext.Database.SqlQuery<PessFisicaJuridica>(consultaPensaoAlimenticia + " where  b.nom_dpdte like '%" + pBusca.ToUpper() + "%'" + " and " + exists);
            queryEmpDpte.AsQueryable();


            var queryFinal = queryConvenente.Concat(queryEmpregado).Concat(queryDependente).Concat(queryRpUniaoFss).Concat(queryEmpDpte).Distinct();



            return queryFinal.Take(50).Distinct();
        }

        #endregion

        #region Buscar Pessoa física/ jurídica por CPF ou CNPJ
        public IEnumerable<PessFisicaJuridica> BuscaCpfCnpj(string pBusca)
        {
            Int64 buscaInt;
            Int64.TryParse(pBusca, out buscaInt);

            long buscaLong;
            long.TryParse(pBusca, out buscaLong);

            IEnumerable<PessFisicaJuridica> queryEmpregado =
            m_DbContext.Database.SqlQuery<PessFisicaJuridica>(@"SELECT nom_emprg nomeRazaoSocial,
                                                                       to_number(num_cpf_emprg) cpfCnpj,
                                                                       (select 'pFisica - Empregado' from dual),
                                                                       num_ctcor_emprg codigoContaCorrente,
                                                                       Num_Digvr_Emprg codigoDvContaCorrente,
                                                                       'pFisica - Empregado' tipoCadastro,
                                                                       cod_emprs codigoEmpresa,
                                                                       num_rgtro_emprg codigo,
                                                                       to_number(cod_banco) codigoBanco,
                                                                       to_number(cod_agbco) codigoAgencia,
                                                                       TIP_CTCOR_EMPRG codigotipoConta
                                                                  FROM ATT.EMPREGADO WHERE NUM_CPF_EMPRG LIKE '%" + pBusca + "%'");
            queryEmpregado.AsQueryable();


            IQueryable<PessFisicaJuridica> queryDependente;
            queryDependente = from dpt in m_DbContext.DEPENDENTE
                              where dpt.NUM_CPF_DPDTE == buscaLong
                              select new PessFisicaJuridica
                              {
                                  cpfCnpj = dpt.NUM_CPF_DPDTE,
                                  nomeRazaoSocial = dpt.NOM_DPDTE,
                                  tipoCadastro = "pFisica - Dependente",
                                  codigo = dpt.NUM_IDNTF_DPDTE,
                                  codigoEmpresa = 0,
                                  codigoAgencia = 0,
                                  codigoBanco = 0,
                                  codigotipoConta = "0",
                                  codigoContaCorrente = "0",
                                  codigoDvContaCorrente = 0
                              };

            IQueryable<PessFisicaJuridica> queryRpUniaoFss;
            queryRpUniaoFss = from ruf in m_DbContext.REPRES_UNIAO_FSS
                              where ruf.NUM_CPF_REPRES == buscaLong

                              select new PessFisicaJuridica()
                              {
                                  cpfCnpj = ruf.NUM_CPF_REPRES,
                                  nomeRazaoSocial = ruf.NOM_REPRES,
                                  tipoCadastro = "pFisica - Repres_uniao_fss",
                                  codigo = ruf.NUM_IDNTF_RPTANT,
                                  codigoEmpresa = 0,
                                  codigoAgencia = ruf.COD_AGBCO,
                                  codigoBanco = ruf.COD_BANCO,
                                  codigoContaCorrente = ruf.NUM_CTCOR_REPRES,
                                  cdTipoConta = ruf.TIP_CTCOR_REPRES,
                                  codigoDvContaCorrente = 0
                              };

            foreach (var item in queryRpUniaoFss)
            {
                if (item.cdTipoConta >= 0)
                {
                    item.codigotipoConta = item.cdTipoConta.ToString();
                }
            }


            // pensões alimentícias
            IEnumerable<PessFisicaJuridica> queryEmpDpte =
            m_DbContext.Database.SqlQuery<PessFisicaJuridica>(consultaPensaoAlimenticia + " where b.num_cpf_dpdte like '%" + pBusca.ToUpper() + "%'" + " and " + exists);
            queryEmpDpte.AsQueryable();

            IEnumerable<PessFisicaJuridica> queryConvenente =
           m_DbContext.Database.SqlQuery<PessFisicaJuridica>(@"select to_number(num_cgc_cpf) cpfCnpj,
                                                                      (trim(nom_convenente) || ' - ' || COD_CONVENENTE) nomeRazaoSocial,
                                                                      num_conta_corr codigoContaCorrente,
                                                                      dgv_convenente codigoDvContaCorrente,
                                                                      (select 'pJuridica - Convenente' from dual) tipoCadastro,
                                                                      '0' codigoEmpresa,
                                                                      cod_convenente codigo,
                                                                      to_number(cod_banco) codigoBanco,
                                                                      to_number(cod_agbco) codigoAgencia,
                                                                      tip_ctcor codigotipoConta
                                                                 from tb_convenente where num_cgc_cpf LIKE '%" + pBusca + "%'");

            queryConvenente.AsQueryable();


            var queryFinal = queryEmpregado.
            Concat(queryDependente).
            Concat(queryRpUniaoFss).
            Concat(queryEmpDpte).
            Concat(queryConvenente);


            return queryFinal.Take(50).Distinct();
        }

        #endregion

        #region Geração de grid

        public DataTable PesquisarGrid()
        {
            IEnumerable<MEDCTR> IEnum = m_DbContext.Database.SqlQuery<MEDCTR>("SELECT * FROM OWN_INTPROTHEUS.MEDCTR WHERE ROWNUM <= 100");
            DataTable retorno = Util.ToDataTable<MEDCTR>(IEnum.ToList());

            return retorno;
        }

        public DataTable PesquisaGridParametrizada(int? codEmpresa, int? codMatricula, int? codCovenente, string DataInclusao, string Banco, string Agencia)
        {

            IEnumerable<MEDCTR> Ienum = from med in m_DbContext.MEDCTR
                                        where 1 == 1
                                        && (med.COD_EMPRS == codEmpresa || codEmpresa == 0)
                                        && (med.NUM_RGTRO_EMPRG == codMatricula || codMatricula == 0)
                                        && (med.COD_CONVENENTE == codCovenente || codCovenente == 0)
                                        && (med.BANCO == Banco || Banco == null)
                                        && (med.AGENCIA == Agencia || Agencia == null)
                                        && (med.DTINCL == DataInclusao || DataInclusao == null)
                                        orderby med.DTINCL descending
                                        select med;

            DataTable retorno = Util.ToDataTable<MEDCTR>(Ienum.ToList());

            return retorno;
        }
        #endregion

        public long chaveSequencial()
        {
            long chaveSequencial = 0;

            chaveSequencial = m_DbContext.MEDCTR.Max(s => s.SEQ_MEDCTR);

            return chaveSequencial;
        }

        public Int32 chaveSequencialLote()
        {
            Int32 chaveSequenciaLote = 0;

            chaveSequenciaLote = m_DbContext.MEDCTR.Max(s => s.NUM_LOTE);

            return chaveSequenciaLote;
        }

        public Resultado GravarMedicao(MEDCTR med)
        {
            Resultado res = new Resultado();
            try
            {
                m_DbContext.MEDCTR.Add(med);

                int linha = m_DbContext.SaveChanges();

                if (linha > 0)
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

        public PessFisicaJuridica TratarDadosBancarios(string cdBanco, string cdTpConta, string numConta)
        {
            PessFisicaJuridica pes = new PessFisicaJuridica();

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("v_cod_bco", cdBanco);
                objConexao.AdicionarParametro("v_tip_cta", cdTpConta);
                objConexao.AdicionarParametro("v_num_cta", numConta);
                objConexao.AdicionarParametroOut("vv_num_cta", OracleType.NVarChar);
                objConexao.AdicionarParametroOut("vv_dv_cta", OracleType.NVarChar);
                objConexao.AdicionarParametroOut("vv_dv_ag", OracleType.NVarChar);

                objConexao.ExecutarNonQuery("own_intprotheus.PKG_CARGA_PROTHEUS.STP_TRATA_CONTA_CORRENTE");

                List<OracleParameter> param = objConexao.ReturnParemeter();

                string contaCorrente = param[0].Value.ToString().Trim();
                string dvContaCorrente = param[1].Value.ToString().Trim();
                string dvAgencia = param[2].Value.ToString().Trim();

                Int16 banco = 0;
                Int16.TryParse(cdBanco, out banco);

                pes.codigoDvContaCorrente = (!String.IsNullOrEmpty(dvContaCorrente) ? Convert.ToInt16(dvContaCorrente) : Convert.ToInt16(0));

                pes.codigoDigVerificadorAgencia = (!String.IsNullOrEmpty(dvAgencia) ? Convert.ToInt32(dvAgencia) : 0);

                pes.codigoContaCorrente = contaCorrente;
                pes.codigoBanco = banco;

                return pes;
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
    }
}
