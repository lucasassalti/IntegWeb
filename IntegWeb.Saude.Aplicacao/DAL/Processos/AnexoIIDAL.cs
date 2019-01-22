using System;
using System.Collections.Generic;
using System.Linq;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Faturamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace IntegWeb.Saude.Aplicacao.DAL.Processos
{
    public class AnexoIIDAL
    {

        // ConectaAD teste = (ConectaAD)Session["objUser"];

        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        /************************ FUNÇÕES E BUSCAS *********************************************************************************/

        #region CARREGAR SERVIÇOS / BUSCAS / FUNÇÕES DE BUSCAS GENÉRICAS

        public IQueryable<SAU_TB_SERVICO_AND> CarregarServicos(string cod_Serv)
        {
            // Tratamento
            decimal cod_servico = Convert.ToDecimal(cod_Serv);

            IQueryable<SAU_TB_SERVICO_AND> query = from s in m_DbContext.SAU_TB_SERVICO_AND
                                                   where (s.COD_SERV == cod_servico || cod_servico == null || cod_servico == 0)
                                                   select s;
            return query;
        }

        public String CarregarDescricaoServicos(string cod_Serv)
        {
            // Tratamento
            decimal cod_servico = Convert.ToDecimal(cod_Serv);

            IQueryable<String> query = from s in m_DbContext.SAU_TB_SERVICO_AND
                                       where (s.COD_SERV == cod_servico || cod_servico == null || cod_servico == 0)
                                       select s.DESCRICAO;
            return query.ToString();
        }

        public IQueryable<CargaServicos> CargaServicosPorHospital(string Cod_Empresa)
        {
            decimal CodEmpresa = Convert.ToDecimal(Cod_Empresa);

            IQueryable<CargaServicos> query = from serv in m_DbContext.SAU_TB_SERV_X_HOSP_AND
                                              where serv.COD_HOSP == CodEmpresa
                                              select new CargaServicos
                                              {
                                                  //descServico = serv.DESCRICAO,
                                                  codServico = serv.COD_SERV,
                                                  valor = serv.VALOR
                                                 // vProposto = serv.VPROPOSTO,
                                                 // pProposta = serv.PPROPOSTA,
                                                 // dtReajusteProp = serv.DTREAJUSTEPROP
                                              };
            return query;
        }

        public SAU_TB_HOSP_AND retornaHospital(Int32 cod)
        {
            SAU_TB_HOSP_AND hospital = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == cod);

            return hospital;
        }



        public Tuple<DateTime?, DateTime?> RetornarDataMaior(String CodigoEmpresa)
        {
            decimal cdHospital = Convert.ToDecimal(CodigoEmpresa);

            var maiorData = from servicos in m_DbContext.SAU_TB_SERV_X_HOSP_AND
                            join hospitais in m_DbContext.SAU_TB_HOSP_AND
                            on servicos.COD_HOSP equals hospitais.COD_HOSP
                            where servicos.COD_HOSP == cdHospital// && servicos.DTREAJUSTEPROP != null

                            select servicos.DAT_INI_VIGENCIA;

            var vigencia = from servicos in m_DbContext.SAU_TB_SERV_X_HOSP_AND
                           join hospitais in m_DbContext.SAU_TB_HOSP_AND
                           on servicos.COD_HOSP equals hospitais.COD_HOSP
                           where servicos.COD_HOSP == cdHospital
                           select hospitais.DAT_FIM_VIGENCIA;


            return Tuple.Create(maiorData.Max(), vigencia.FirstOrDefault());
        }

        #endregion

        #region CARREGAR SERVIÇOS x HOSPITAL

        public IQueryable<SAU_TB_SERV_X_HOSP_AND> CarregarServicosxHospital(string cod_Hosp, string cod_Serv)
        {
            // Tratamento
            decimal cd_hospital;
            Decimal.TryParse(cod_Hosp, out cd_hospital);

            decimal cd_servico;
            Decimal.TryParse(cod_Serv, out cd_servico);

            IQueryable<SAU_TB_SERV_X_HOSP_AND> query = from s in m_DbContext.SAU_TB_SERV_X_HOSP_AND
                                                       where (s.COD_HOSP == cd_hospital || cd_hospital == null || cd_hospital == 0)
                                                          && (s.COD_SERV == cd_servico || cd_servico == null || cd_servico == 0)
                                                       select s;
            return query;
        }

        #endregion

        #region CARREGAR HOSPITAL

        public IQueryable<SAU_TB_HOSP_AND> carregarHospital(string nomeConvenente, int? codCovenente)
        {
            IQueryable<SAU_TB_HOSP_AND> query = from h in m_DbContext.SAU_TB_HOSP_AND
                                                where (h.COD_HOSP == codCovenente || codCovenente == null)
                                                   && (h.NOME_FANTASIA == nomeConvenente || nomeConvenente == null)
                                                   && (h.NOME_FANTASIA != "" || h.NOME_FANTASIA != null)
                                                   && (h.COD_HOSP >= 0 || h.COD_HOSP != null)
                                                select h;

            foreach (var item in query)
            {
                item.NOME_FANTASIA = item.COD_HOSP + " - " + item.NOME_FANTASIA.Replace(item.COD_HOSP.ToString(), "").Replace(" - ", "");
            }

            return query.OrderBy(x => x.COD_HOSP);
        }
        #endregion

        #region CARREGAR OBSERVACAO CONTRATUAL

        public String CarregarObservacaoContratual(string CodigoHosp)
        {
            // Tratamento
            decimal cod_Hospital = Convert.ToDecimal(CodigoHosp);

            IQueryable<String> query = from s in m_DbContext.SAU_TB_HOSP_AND
                                       where (s.COD_HOSP == cod_Hospital || CodigoHosp == null)
                                       select s.OBSERVACAOCONTRATUAL;
            return query.FirstOrDefault();
        }

        #endregion

        /************************ FUNÇÕES PRINCIPAIS ******************************************************************************/

        #region ATUALIZAR VIGÊNCIA DE SERVIÇOS

        public Resultado AtualizaVigenciaServico(string CdEmpresa, string DataVigencia, string DataMaior)
        {
            Resultado resultado = new Resultado();
            Decimal CodigoEmpresa;
            Decimal.TryParse(CdEmpresa, out CodigoEmpresa);
            try
            {
                SAU_TB_HOSP_AND hospitais = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == CodigoEmpresa);
                if (hospitais != null)
                {
                    hospitais.DAT_FIM_VIGENCIA = Convert.ToDateTime(DataMaior);
                    int res = m_DbContext.SaveChanges();
                    if (res > 0)
                    {
                        resultado.Sucesso("Atualização de vigência ocorreu com sucesso!");
                    }
                    else
                    {
                        resultado.Erro("Ocorreu um erro na tentativa de atualizar a vigência.");

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            return resultado;
        }

        #endregion

        #region ATUALIZAR O VALOR DOS SERVIÇOS X HOSPITAL

        public Resultado AtualizaValorServicosHosp(decimal codigoEmpresa, SAU_TB_LOG_AND log)
        {
            AnexoIIBLL anexoBll = new AnexoIIBLL();
            Resultado resultado = new Resultado();

            SAU_TB_SERV_X_HOSP_AND servico = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == codigoEmpresa);

            SAU_TB_SERV_X_HOSP_AND atualiza = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == codigoEmpresa);

          //  if (atualiza.VPROPOSTO > 0)
          //  {
                /*atualiza.ADTREAJUSTE = servico.PDTREAJUSTE;
                atualiza.PDTREAJUSTE = servico.DTREAJUSTE;
                atualiza.DTREAJUSTE = servico.DTREAJUSTEPROP;
                atualiza.DTREAJUSTEPROP = null;
                atualiza.AVALOR = servico.PVALOR;
                atualiza.PVALOR = servico.VALOR;
                atualiza.VALOR = servico.VPROPOSTO;
                atualiza.VPROPOSTO = 0;
                atualiza.APORCENTAGEM = servico.PPORCENTAGEM;
                atualiza.PPORCENTAGEM = servico.PORCENTAGEM;
                atualiza.PORCENTAGEM = servico.PPROPOSTA;
                atualiza.PPROPOSTA = 0; */

                try
                {
                    int cont = m_DbContext.SaveChanges();
                    if (cont > 0)
                    {
                        anexoBll.GeraLog(servico, atualiza, log, "CONFIRMAAUMENTO");
                        resultado.Sucesso("Atualização de vigência ocorreu com sucesso!");
                    }
                }
                catch (Exception e)
                {
                    resultado.Erro("Ocorreu um erro na tentativa de atualizar a vigência." + e);
                }
          //  }
            return resultado;
        }

        #endregion

        #region ATUALIZAR PORCENTAGEM SERVIÇO X HOSPITAL

        public Boolean atualizaServicoHospital(SAU_TB_SERV_X_HOSP_AND servico, SAU_TB_LOG_AND log)
        {
            AnexoIIBLL anexoBll = new AnexoIIBLL();

            bool ok = false;
            try
            {
                SAU_TB_SERV_X_HOSP_AND atualiza = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(x => x.COD_SERV == servico.COD_SERV && x.COD_HOSP == servico.COD_HOSP);
                if (atualiza != null)
                {
                    // Geração de LOG
                    anexoBll.GeraLog(servico, atualiza, log, "PLANILHADEAPROVACAO");

                    atualiza.COD_HOSP = servico.COD_HOSP;
                    atualiza.COD_SERV = servico.COD_SERV;
                    /*atualiza.VPROPOSTO = servico.VPROPOSTO;
                    atualiza.PPROPOSTA = servico.PPROPOSTA;
                    atualiza.DESCONTO = servico.DESCONTO;
                    atualiza.DTREAJUSTEPROP = servico.DTREAJUSTEPROP;*/
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro, //n Contate o administrador do sistema: //n" + ex.Message);
            }

            return ok;
        }

        #endregion

        /************************ CRUD *******************************************************************************************/

        #region CRUD - SERVIÇOS

        public Boolean IncluirServico(SAU_TB_SERVICO_AND service)
        {
            bool ok = false;
            SAU_TB_SERVICO_AND servico = m_DbContext.SAU_TB_SERVICO_AND.FirstOrDefault(x => x.COD_SERV == service.COD_SERV);

            if (servico == null)
            {
                SAU_TB_SERVICO_AND incluir = new SAU_TB_SERVICO_AND();
                incluir.COD_SERV = service.COD_SERV;
                incluir.DESCRICAO = service.DESCRICAO;

                m_DbContext.SAU_TB_SERVICO_AND.Add(incluir);

                int inclusao = m_DbContext.SaveChanges();

                if (inclusao > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok;
                }

            }
            else
            {
                return ok;
            }
        }

        public Boolean AlterarServico(SAU_TB_SERVICO_AND service)
        {
            bool ok = false;
            SAU_TB_SERVICO_AND servico = m_DbContext.SAU_TB_SERVICO_AND.FirstOrDefault(x => x.COD_SERV == service.COD_SERV);

            if (servico != null)
            {
                servico.COD_SERV = service.COD_SERV;
                servico.DESCRICAO = service.DESCRICAO;

                int update = m_DbContext.SaveChanges();

                if (update > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok;
                }
            }
            else
            {
                return ok;
            }
        }

        public Boolean ExcluirServico(SAU_TB_SERVICO_AND service)
        {
            bool ok = false;
            SAU_TB_SERVICO_AND servico = m_DbContext.SAU_TB_SERVICO_AND.FirstOrDefault(x => x.COD_SERV == service.COD_SERV);

            if (servico != null)
            {
                m_DbContext.SAU_TB_SERVICO_AND.Remove(servico);

                int inclusao = m_DbContext.SaveChanges();

                if (inclusao > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok;
                }

            }
            else
            {
                return ok;
            }
        }

        #region CARREGAR SERVIÇO (TISS)

        public Boolean VerificaExistenciaServico(string CodigoServico)
        {
            bool existe = false;
            decimal codServico;
            decimal.TryParse(CodigoServico, out codServico);

            IQueryable<Decimal> query = from s in m_DbContext.SAU_TB_SERVICO_AND
                                        where (s.COD_SERV == codServico || codServico == null)
                                        select s.COD_SERV;
            if (query.ToList().Count > 0)
            {
                return existe = true;
            }
            return existe;
        }

        public List<SAU_TB_SERVICO_AND> RetornarServico(string CodigoServico)
        {
            decimal codServico;
            decimal.TryParse(CodigoServico, out codServico);

            IQueryable<SAU_TB_SERVICO_AND> query = from s in m_DbContext.SAU_TB_SERVICO_AND
                                                   where (s.COD_SERV == codServico || codServico == null)
                                                   select s;
            return query.ToList();
        }

        public IList<SAU_TB_SERVICO_AND> RetornarServicoTop(string CodigoServico)
        {
            decimal codServico;
            decimal.TryParse(CodigoServico, out codServico);

            IQueryable<SAU_TB_SERVICO_AND> query = from s in m_DbContext.SAU_TB_SERVICO_AND
                                                   where (s.COD_SERV == codServico || codServico == null)
                                                   select s;
            return query.Take(100).ToList();
        }

        #endregion



        #endregion

        #region CRUD - SERVIÇOS X HOSPITAL

        public Boolean IncluirServicoxHospital(SAU_TB_SERV_X_HOSP_AND servHosp)
        {
            bool ok = false;
            SAU_TB_SERV_X_HOSP_AND servicoHospital = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(x => x.COD_SERV == servHosp.COD_SERV && x.COD_HOSP == servHosp.COD_HOSP);

            if (servicoHospital == null)
            {
                SAU_TB_SERV_X_HOSP_AND incluir = new SAU_TB_SERV_X_HOSP_AND();
                incluir = servHosp;

                m_DbContext.SAU_TB_SERV_X_HOSP_AND.Add(incluir);

                int inclusao = m_DbContext.SaveChanges();

                if (inclusao > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok;
                }
            }
            else
            {
                return ok;
            }
        }

        public Boolean AlterarServicoxHospital(SAU_TB_SERV_X_HOSP_AND servHosp)
        {
            bool ok = false;
            SAU_TB_SERV_X_HOSP_AND servicoHospital = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(x => x.COD_SERV == servHosp.COD_SERV && x.COD_HOSP == servHosp.COD_HOSP);

            if (servicoHospital != null)
            {
               // servicoHospital.DESCRICAO = servHosp.DESCRICAO;
              //  servicoHospital.DTREAJUSTE = servHosp.DTREAJUSTE;
                servicoHospital.VALOR = servHosp.VALOR;

                SAU_TB_HOSP_AND hospt = new SAU_TB_HOSP_AND();
                hospt.DAT_INI_VIGENCIA = carregarHospital(null, Convert.ToInt32(servHosp.COD_HOSP)).FirstOrDefault().DAT_FIM_VIGENCIA;
                hospt.COD_HOSP = servHosp.COD_HOSP;
                if (hospt.DAT_INI_VIGENCIA == null)
                {
                    AtualizarPrestador(hospt);
                }

                int atualizado = m_DbContext.SaveChanges();

                if (atualizado > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok;
                }
            }
            else
            {
                return ok;
            }
        }

        public Boolean ExcluirServicoxHospital(SAU_TB_SERV_X_HOSP_AND servHosp)
        {
            bool ok = false;
            SAU_TB_SERV_X_HOSP_AND servicoHospital = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(x => x.COD_SERV == servHosp.COD_SERV && x.COD_HOSP == servHosp.COD_HOSP);

            if (servicoHospital != null)
            {
                m_DbContext.SAU_TB_SERV_X_HOSP_AND.Remove(servicoHospital);

                int exclusao = m_DbContext.SaveChanges();

                if (exclusao > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok;
                }
            }
            else
            {
                return ok;
            }
        }


        #endregion

        #region CRUD - PRESTADOR

        public SAU_TB_HOSP_AND buscaPrestador(SAU_TB_HOSP_AND hosp)
        {
            SAU_TB_HOSP_AND busca = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == hosp.COD_HOSP);

            return busca;
        }

        public Boolean incluirPrestador(SAU_TB_HOSP_AND hosp)
        {
            bool ok = false;

            SAU_TB_HOSP_AND busca = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == hosp.COD_HOSP);

            if (busca == null)
            {
                m_DbContext.SAU_TB_HOSP_AND.Add(hosp);
                int inclusao = m_DbContext.SaveChanges();

                if (inclusao > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok = false;
                }
            }
            else
            {
                return ok = false;
            }
        }

        public Boolean ExcluirPrestador(SAU_TB_HOSP_AND hosp)
        {
            bool ok = false;

            var hospital = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == hosp.COD_HOSP);

            if (hospital != null)
            {
                m_DbContext.SAU_TB_HOSP_AND.Remove(hospital);

                int excluir = m_DbContext.SaveChanges();
                if (excluir > 0)
                {
                    return ok = true;
                }
                else
                {
                    return ok;
                }
            }
            else
            {
                return ok;
            }
        }

        public Boolean AtualizarPrestador(SAU_TB_HOSP_AND hosp)
        {
            bool ok = false;

            var atualiza = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == hosp.COD_HOSP);

            if (atualiza != null)
            {
                // Atualizar o hospital. 
                atualiza.NOME_FANTASIA = hosp.NOME_FANTASIA;
                atualiza.CONTATO = hosp.CONTATO;
                atualiza.DAT_INICIO_CONTRATO = hosp.DAT_INICIO_CONTRATO;
                atualiza.CIDADE = hosp.CIDADE;
                atualiza.REGIONAL = hosp.REGIONAL;
                atualiza.CREDENCIADOR = hosp.CREDENCIADOR;

                if (hosp.DAT_FIM_VIGENCIA != null)
                {
                    atualiza.DAT_FIM_VIGENCIA = hosp.DAT_FIM_VIGENCIA;
                }

                int atualizar = m_DbContext.SaveChanges();

                if (atualizar > 0)
                {
                    return ok = true;
                }
            }
            return ok;
        }

        #endregion

        #region CRUD - OBSERVAÇÕES CONTRATUAIS

        public Boolean incluirObservacaoContratual(SAU_TB_HOSP_AND hosp)
        {
            bool ok = false;

            var incluir = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == hosp.COD_HOSP);

            if (incluir.OBSERVACAOCONTRATUAL == null)
            {
                // Incluir OBSERVAÇÃO CONTRATUAL
                incluir.OBSERVACAOCONTRATUAL = hosp.OBSERVACAOCONTRATUAL;

                int retorno = m_DbContext.SaveChanges();

                if (retorno > 0)
                {
                    return ok = true;
                }
            }
            return ok;
        }

        public Boolean AlterarObservacaoContratual(SAU_TB_HOSP_AND hosp)
        {
            bool ok = false;

            var atualiza = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == hosp.COD_HOSP);

            if (atualiza.OBSERVACAOCONTRATUAL != null)
            {
                // Atualizar OBSERVAÇÃO CONTRATUAL
                atualiza.OBSERVACAOCONTRATUAL = hosp.OBSERVACAOCONTRATUAL;

                int retorno = m_DbContext.SaveChanges();

                if (retorno > 0)
                {
                    return ok = true;
                }
            }
            return ok;
        }

        public Boolean ExcluirObservacaoContratual(SAU_TB_HOSP_AND hosp)
        {
            bool ok = false;

            var atualiza = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(x => x.COD_HOSP == hosp.COD_HOSP);

            if (atualiza.OBSERVACAOCONTRATUAL != null)
            {
                // Excluir OBSERVAÇÃO CONTRATUAL
                atualiza.OBSERVACAOCONTRATUAL = "";

                int excluir = m_DbContext.SaveChanges();

                if (excluir > 0)
                {
                    return ok = true;
                }
            }
            return ok;
        }

        #endregion

        #region CRUD - TB_VAL_RECURSO

        public Boolean AtualizaTb_Val_Recurso(TB_VAL_RECURSO tValRecurso, ExportaArquivoScan arquivoScan, SAU_TB_LOG_AND log)
        {
            bool ok = false;

            var Recurso = m_DbContext.TB_VAL_RECURSO.FirstOrDefault(x => x.COD_TAB_RECURSO == tValRecurso.COD_TAB_RECURSO
                                                                    && x.COD_RECURSO == tValRecurso.COD_RECURSO
                                                                    && x.DAT_VAL_RECURSO == tValRecurso.DAT_VAL_RECURSO);

            if (Recurso == null)
            {
                AnexoIIBLL anexoBll = new AnexoIIBLL();

                var incluir = m_DbContext.TB_VAL_RECURSO.Add(tValRecurso);

                int retorno = m_DbContext.SaveChanges();
                if (retorno > 0)
                {
                    decimal procedimento;
                    decimal.TryParse(tValRecurso.RCOCODPROCEDIMENTO, out procedimento);

                    log.COD_TAB_SERV_TB_VAL_RECURSO = tValRecurso.COD_TAB_RECURSO;
                    log.DT_REAJUSTE_TB_VAL_RECURSO = tValRecurso.DAT_VAL_RECURSO;
                    log.COD_RECURSO_TB_VAL_RECURSO = tValRecurso.COD_RECURSO;
                    log.VAL_RECURSO_TB_VAL_RECURSO = tValRecurso.VAL_RECURSO;
                    log.COD_SERV = procedimento;
                    log.COD_HOSP = arquivoScan.CodHosp;
                    anexoBll.GeraLog(null, null, log, "EXPORTASCAN");

                    return ok = true;
                }
            }

            return ok;
        }

        public List<SAU_TB_LOG_AND> retornaLog(DateTime? inicio, DateTime? fim, Decimal? prestador)
        {

            IQueryable<SAU_TB_LOG_AND> query = from log in m_DbContext.SAU_TB_LOG_AND
                                               where
                                               log.OPERACAO == "EXPORTASCAN"
                                               && (log.COD_HOSP == prestador || prestador == 0)
                                               && (log.DATAALTERACAO >= inicio || inicio == DateTime.MinValue)
                                               && (log.DATAALTERACAO <= fim || fim == DateTime.MinValue)
                                               select log;

            return query.Take(500).OrderBy(x => x.DT_REAJUSTE_TB_VAL_RECURSO).OrderByDescending(x => x.DATAALTERACAO).ToList();
        }



        #endregion


        #region CRUD - LOG


        public Boolean incluirLog(SAU_TB_LOG_AND log)
        {
            bool ok = false;

            if (log != null)
            {
                // INCLUIR LOG
                m_DbContext.SAU_TB_LOG_AND.Add(log);

                int retorno = m_DbContext.SaveChanges();

                if (retorno > 0)
                {
                    return ok = true;
                }
            }
            return ok;
        }

        #endregion

        /************************ EXPORTAÇÃO DE ARQUIVOS SCAN ********************************************************************/

        #region :: EXPORTAÇÃO DE ARQUIVOS - SCAN ::

        public List<ExportaArquivoScan> ExportaArquivoScan(String contratos)
        {
            IEnumerable<ExportaArquivoScan> exportaArquivoScan = new List<ExportaArquivoScan>();
            IList<ExportaArquivoScan> repExptArquivoScan = new List<ExportaArquivoScan>();

            exportaArquivoScan =
                m_DbContext.Database.SqlQuery<ExportaArquivoScan>(@"SELECT DISTINCT  Tb2.Cod_Tab_Servicos As Cod_Tab_Servicos, 
                                                                                     Tb3.Cod_Recurso AS Cod_Recurso,   
                                                                                     to_date(sha.dtreajuste) As DtReajuste,
                                                                                     sha.valor As Valor,  Tb3.RCOSEQ AS RCOSEQ,  
                                                                                     Tb3.Rcocodprocedimento as RCOCODPROCEDIMENTO, 
                                                                                     sha.cod_hosp CodHosp
                                                                     FROM att.TB_TAB_RECURSO  TR,  att.Tb_Cond_Contrat  Tb2,  att.Tb_Tipo_Cond_Conv  Tb1,  att.cadtblrcorecursocodigo  Tb3,  att.tb_recurso  r, 
                                                                      att.tb_tipo_recurso  tip,
                                                                      own_funcesp.sau_tb_serv_x_hosp_and sha
                                                                     WHERE ((TR.COD_TAB_RECURSO = Tb2.COD_TAB_SERVICOS)
                                                                     OR  (Tb2.COD_TAB_SERVICOS IS NULL and TR.COD_TAB_RECURSO = Tb2.Cod_Tab_Mat_Med))
                                                                     AND Tb1.COD_TIPO_COND_CONT = Tb2.COD_TIPO_COND_CONT AND r.COD_RECURSO = Tb3.COD_RECURSO AND r.COD_TIP_RECURSO = tip.COD_TIP_RECURSO  
                                                                     AND Tb3.rcodatdesativacao Is Null  AND sha.cod_serv = tb3.rcocodprocedimento  AND tip.TIP_RECURSO in ('M','T')
                                                                     AND Tb1.DAT_VALIDADE = (select MAX(DAT_VALIDADE) from att.Tb_Tipo_Cond_Conv TCC where TCC.COD_CONVENENTE = Tb1.Cod_Convenente)
                                                                     AND Tb3.RCOCODPROCEDIMENTO in (Select to_char(s1.cod_serv) from own_funcesp.sau_tb_serv_x_hosp_and  s1 
                                                                     where  s1.DtReajuste = (select Max(s2.DtReajuste) from own_funcesp.sau_tb_serv_x_hosp_and  s2 
                                                                     where s2.cod_hosp = s1.cod_hosp and s2.cod_serv = s1.cod_serv) and s1.cod_hosp in (" + contratos + ")) AND to_number(Tb1.COD_CONVENENTE)  in (" + contratos + ") and sha.cod_hosp = (" + contratos + ") ");

            return exportaArquivoScan.ToList();
        }

        public List<SAU_TB_SERV_X_HOSP_AND> CarregaServicoScan(String contratos)
        {
            IEnumerable<SAU_TB_SERV_X_HOSP_AND> exportaArquivoScan = new List<SAU_TB_SERV_X_HOSP_AND>();
            IList<SAU_TB_SERV_X_HOSP_AND> repExptArquivoScan = new List<SAU_TB_SERV_X_HOSP_AND>();

            exportaArquivoScan =
                m_DbContext.Database.SqlQuery<SAU_TB_SERV_X_HOSP_AND>(@"Select * from own_funcesp.sau_tb_serv_x_hosp_and  s1  where s1.DtReajuste = (select Max(s2.DtReajuste)
                                                                      from own_funcesp.sau_tb_serv_x_hosp_and  s2  where s2.cod_hosp = s1.cod_hosp and s2.cod_serv = s1.cod_serv) and s1.cod_hosp in (" + contratos + ")");

            return exportaArquivoScan.ToList();
        }


        #endregion

    }
}