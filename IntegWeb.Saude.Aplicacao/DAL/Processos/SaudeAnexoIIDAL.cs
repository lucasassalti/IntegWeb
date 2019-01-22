using IntegWeb.Entidades;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Processos
{
    public class SAU_TB_SERV_X_HOSP_AND_view
    {
        public decimal ID_REG { get; set; }
        public decimal? COD_HOSP { get; set; }
        public decimal? COD_SERV { get; set; }
        public string DESC_SERV { get; set; }
    }


    public class SAU_TB_LOG_AND_view
    {
        public decimal QTD_SERV { get; set; }
        public decimal? COD_HOSP { get; set; }
        public Nullable<System.DateTime> DAT_PROCESSAMENTO { get; set; }
        public string USU_PROCESSAMENTO { get; set; }

    }


    public class SaudeAnexoIIDAL
    {

        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public IQueryable<SAU_TB_HOSP_AND> carregarHospital(string nomeConvenente, int? codCovenente)
        {
            IQueryable<SAU_TB_HOSP_AND> query = from h in m_DbContext.SAU_TB_HOSP_AND
                                                where (h.COD_HOSP == codCovenente || codCovenente == null)
                                                   && (h.NOME_FANTASIA == nomeConvenente || nomeConvenente == null)
                                                   && (h.NOME_FANTASIA != "" || h.NOME_FANTASIA != null)
                                                   && (h.COD_HOSP >= 0 || h.COD_HOSP != null)
                                                   && (h.DAT_FIM_VIGENCIA == null)
                                                select h;

            foreach (var item in query)
            {
                item.NOME_FANTASIA = item.COD_HOSP + " - " + item.NOME_FANTASIA.Replace(item.COD_HOSP.ToString(), "").Replace(" - ", "");
            }

            return query.OrderBy(x => x.COD_HOSP);
        }

        public IQueryable<SAU_TB_SERVICO_AND> CarregarServicos(int? codServ)
        {
            IQueryable<SAU_TB_SERVICO_AND> query = from s in m_DbContext.SAU_TB_SERVICO_AND
                                                   where (s.COD_SERV == codServ || s.COD_SERV == null || s.COD_SERV >= 0)
                                                   && (s.DAT_FIM_ALTERACAO == null)
                                                   select s;

            foreach (var item in query)
            {
                item.DESCRICAO = item.COD_SERV + " - " + item.DESCRICAO.Replace(item.COD_SERV.ToString(), "").Replace(" - ", "");
            }


            return query.OrderBy(se => se.COD_SERV);
        }

        #region .: Aba 1 :.

        public IQueryable<SAU_TB_SERV_X_HOSP_AND> GetServHospAnt(int codHosp)
        {
            IQueryable<SAU_TB_SERV_X_HOSP_AND> query = from sh in m_DbContext.SAU_TB_SERV_X_HOSP_AND
                                                       where (sh.COD_HOSP == codHosp)
                                                        && (sh.DAT_FIM_VIGENCIA != null)
                                                       select sh;

            return query;

        }

        #endregion

        #region .: Aba 2 :.

        public IQueryable<SAU_TB_SERV_X_HOSP_AND_view> GetServPrest(int? codHosp)
        {
            IQueryable<SAU_TB_SERV_X_HOSP_AND_view> query = from sp in m_DbContext.SAU_TB_SERV_X_HOSP_AND
                                                            from serv in m_DbContext.SAU_TB_SERVICO_AND
                                                            where (sp.COD_SERV == serv.COD_SERV)
                                                            && (sp.COD_HOSP == codHosp || codHosp == null)
                                                            && (sp.DAT_FIM_VIGENCIA == null)
                                                            select new SAU_TB_SERV_X_HOSP_AND_view
                                                            {
                                                                COD_HOSP = sp.COD_HOSP,
                                                                COD_SERV = sp.COD_SERV,
                                                                DESC_SERV = serv.DESCRICAO,
                                                                ID_REG = sp.ID_REG
                                                            };

            return query.OrderBy(q => q.COD_SERV);
        }

        public Resultado AplicarPorcentagemProposta(SAU_TB_SERV_X_HOSP_AND obj)
        {
            Resultado res = new Resultado();

            try
            {
                //var atualiza = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(a => a.COD_HOSP == obj.COD_HOSP
                //                                                               && a.COD_SERV == obj.COD_SERV
                //                                                              && a.DAT_FIM_VIGENCIA == null);

                //if (atualiza != null)
                //{
                //    atualiza.PORC_PROPOSTA = obj.PORC_PROPOSTA;
                //    atualiza.PORC_DESC_PROPOSTO = obj.PORC_DESC_PROPOSTO;
                //    atualiza.VALOR_PROPOSTO = obj.VALOR_PROPOSTO;
                //    atualiza.DAT_PROPOSTA = obj.DAT_PROPOSTA;
                //    atualiza.USU_ALTERACAO = obj.USU_ALTERACAO;

                //  


                int rows_update = m_DbContext.Database.ExecuteSqlCommand("UPDATE OWN_FUNCESP.SAU_TB_SERV_X_HOSP_AND  SET PORC_PROPOSTA = " + obj.PORC_PROPOSTA.ToString().Replace(",", ".") + ", PORC_DESC_PROPOSTO = " + obj.PORC_DESC_PROPOSTO.ToString().Replace(",", ".") + ", VALOR_PROPOSTO = " + obj.VALOR_PROPOSTO.ToString().Replace(",", ".") + ", DAT_PROPOSTA = to_date('" + Util.FormatarData(Convert.ToDateTime(obj.DAT_PROPOSTA)) + "','dd/mm/rrrr') " + ", USU_ALTERACAO = " + "'" + obj.USU_ALTERACAO + "'" + " WHERE COD_HOSP = " + obj.COD_HOSP + " AND COD_SERV = " + obj.COD_SERV + " AND DAT_FIM_VIGENCIA IS NULL");
               

                if (rows_update > 0)
                {
                    res.Sucesso("Serviço Atualizado com Sucesso");
                }

            }

            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public Resultado ConfirmarAumento(SAU_TB_SERV_X_HOSP_AND obj)
        {
            Resultado res = new Resultado();
            SAU_TB_SERV_X_HOSP_AND objNew = new SAU_TB_SERV_X_HOSP_AND();


            try
            {
                var atualiza = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(a => a.COD_HOSP == obj.COD_HOSP
                                                                               && a.COD_SERV == obj.COD_SERV
                                                                               && a.DAT_FIM_VIGENCIA == null);
                if (atualiza != null)
                {
                    atualiza.DAT_FIM_VIGENCIA = DateTime.Today;
                    atualiza.USU_ALTERACAO = obj.USU_ALTERACAO;

                    objNew.ID_REG = GetMaxPkServPrest();
                    objNew.COD_HOSP = obj.COD_HOSP;
                    objNew.COD_SERV = obj.COD_SERV;
                    objNew.VALOR = atualiza.VALOR_PROPOSTO;
                    objNew.VALOR_PROPOSTO = atualiza.VALOR_PROPOSTO;
                    objNew.PORCENTAGEM = atualiza.PORC_PROPOSTA;
                    objNew.PORC_PROPOSTA = atualiza.PORC_PROPOSTA;
                    objNew.PORC_DESCONTO = atualiza.PORC_DESC_PROPOSTO;
                    objNew.DAT_INI_VIGENCIA = atualiza.DAT_PROPOSTA;
                    objNew.DAT_FIM_VIGENCIA = null;
                    objNew.DAT_PROPOSTA = atualiza.DAT_PROPOSTA;
                    objNew.USU_ALTERACAO = obj.USU_ALTERACAO;
                    objNew.PORC_DESC_PROPOSTO = atualiza.PORC_DESC_PROPOSTO;

                    m_DbContext.SAU_TB_SERV_X_HOSP_AND.Add(objNew);

                    int row_update = m_DbContext.SaveChanges();

                    if (row_update > 0)
                    {
                        res.Sucesso("Registro Alterado com Sucesso");
                    }

                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }



            return res;


        }

        #endregion

        #region .: Aba 3 :.

        public List<SAU_TB_HOSP_AND> GetHospital(int? codHosp)
        {
            IQueryable<SAU_TB_HOSP_AND> query = from h in m_DbContext.SAU_TB_HOSP_AND
                                                where (h.COD_HOSP == codHosp || h.COD_HOSP == null)
                                                   && (h.DAT_FIM_VIGENCIA == null)
                                                select h;


            return query.ToList();
        }

        public Resultado AtualizaHospital(SAU_TB_HOSP_AND obj)
        {
            Resultado res = new Resultado();
            SAU_TB_HOSP_AND objNew = new SAU_TB_HOSP_AND();

            try
            {

                var atualiza = m_DbContext.SAU_TB_HOSP_AND.Find(obj.ID_REG);

                atualiza.DAT_FIM_VIGENCIA = DateTime.Today;
                atualiza.USU_ALTERACAO = obj.USU_ALTERACAO;

                objNew.ID_REG = GetMaxPkHospital();
                objNew.COD_HOSP = obj.COD_HOSP;
                objNew.NOME_FANTASIA = obj.NOME_FANTASIA;
                objNew.DAT_INICIO_CONTRATO = obj.DAT_INICIO_CONTRATO;
                objNew.CREDENCIADOR = obj.CREDENCIADOR;
                objNew.CIDADE = obj.CIDADE;
                objNew.REGIONAL = obj.REGIONAL;
                objNew.CONTATO = obj.CONTATO;
                objNew.OBSERVACAOCONTRATUAL = atualiza.OBSERVACAOCONTRATUAL;
                objNew.DAT_INI_VIGENCIA = DateTime.Today;
                objNew.DAT_FIM_VIGENCIA = null;
                objNew.USU_ALTERACAO = obj.USU_ALTERACAO;

                m_DbContext.SAU_TB_HOSP_AND.Add(objNew);

                int row_update = m_DbContext.SaveChanges();

                if (row_update > 0)
                {
                    res.Sucesso("Registro Alterado com Sucesso");
                }


            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public decimal GetMaxPkHospital()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.SAU_TB_HOSP_AND.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public Resultado DeleteHospital(SAU_TB_HOSP_AND obj)
        {

            // DELETE LÓGICO

            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.SAU_TB_HOSP_AND.Find(obj.ID_REG);
                atualiza.DAT_FIM_VIGENCIA = DateTime.Today;
                atualiza.USU_ALTERACAO = obj.USU_ALTERACAO;

                int row_update = m_DbContext.SaveChanges();

                if (row_update > 0)
                {
                    res.Sucesso("Registro Deletado com Sucesso");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public Resultado DeleteAllServicos(int? codHosp, string user)
        {
            Resultado res = new Resultado();

            try
            {
                List<SAU_TB_SERV_X_HOSP_AND> atualiza = m_DbContext.SAU_TB_SERV_X_HOSP_AND.Where(s => s.COD_HOSP == codHosp).ToList();

                atualiza.ForEach(s =>
                {
                    s.DAT_FIM_VIGENCIA = DateTime.Today;
                    s.USU_ALTERACAO = user;
                });

                int rows_update = m_DbContext.SaveChanges();

                if (rows_update > 0)
                {
                    res.Sucesso("Contrato Excluído com Sucesso");
                }
                else
                {
                    res.Erro("Não foram encontrados Serviços para esse Contrato, Contrato Excluído");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public Resultado InserirHospital(SAU_TB_HOSP_AND obj)
        {
            Resultado res = new Resultado();

            try
            {
                var vInserir = m_DbContext.SAU_TB_HOSP_AND.FirstOrDefault(h => h.COD_HOSP == obj.COD_HOSP);

                if (vInserir == null)
                {
                    obj.ID_REG = GetMaxPkHospital();

                    m_DbContext.SAU_TB_HOSP_AND.Add(obj);

                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Registro Inserido com Sucesso");
                    }

                }
                else
                {
                    res.Erro("Contrato Existente, Favor Alterar o Código Contrato");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        #endregion

        #region .: Aba 4 :.

        public List<SAU_TB_SERVICO_AND> GetServico(int? codServ)
        {
            IQueryable<SAU_TB_SERVICO_AND> query = from s in m_DbContext.SAU_TB_SERVICO_AND
                                                   where (s.COD_SERV == codServ || s.COD_SERV == null)
                                                   && (s.DAT_FIM_ALTERACAO == null)
                                                   select s;

            return query.ToList();

        }

        public decimal GetMaxPkServico()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.SAU_TB_SERVICO_AND.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public Resultado AtualizaServico(SAU_TB_SERVICO_AND obj)
        {
            Resultado res = new Resultado();
            SAU_TB_SERVICO_AND objNew = new SAU_TB_SERVICO_AND();

            try
            {
                var atualiza = m_DbContext.SAU_TB_SERVICO_AND.Find(obj.ID_REG);
                atualiza.DAT_FIM_ALTERACAO = DateTime.Today;
                atualiza.USU_ALTERACAO = obj.USU_ALTERACAO;

                objNew.ID_REG = GetMaxPkServico();
                objNew.COD_SERV = obj.COD_SERV;
                objNew.DESCRICAO = obj.DESCRICAO;
                objNew.DAT_INI_ALTERACAO = DateTime.Today;
                objNew.DAT_FIM_ALTERACAO = null;
                objNew.USU_ALTERACAO = obj.USU_ALTERACAO;

                m_DbContext.SAU_TB_SERVICO_AND.Add(objNew);

                int rows_update = m_DbContext.SaveChanges();


                if (rows_update > 0)
                {
                    res.Sucesso("Serviço Atualizado com Sucesso");
                }


            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado DeleteServico(SAU_TB_SERVICO_AND obj)
        {
            //DELETE LÓGICO
            Resultado res = new Resultado();

            try
            {
                var delete = m_DbContext.SAU_TB_SERVICO_AND.Find(obj.ID_REG);

                delete.DAT_FIM_ALTERACAO = DateTime.Today;
                delete.USU_ALTERACAO = obj.USU_ALTERACAO;

                int row_delete = m_DbContext.SaveChanges();

                if (row_delete > 0)
                {
                    res.Sucesso("Serviço Deletado com Sucesso");
                }


            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado DeleteAllServPrest(int? codServ, string user)
        {
            Resultado res = new Resultado();

            try
            {
                List<SAU_TB_SERV_X_HOSP_AND> atualiza = m_DbContext.SAU_TB_SERV_X_HOSP_AND.Where(s => s.COD_SERV == codServ).ToList();

                atualiza.ForEach(s =>
                {
                    s.DAT_FIM_VIGENCIA = DateTime.Today;
                    s.USU_ALTERACAO = user;
                });

                int rows_update = m_DbContext.SaveChanges();

                if (rows_update > 0)
                {
                    res.Sucesso("Registro Deletado com Sucesso");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public Resultado InserirServico(SAU_TB_SERVICO_AND obj)
        {
            Resultado res = new Resultado();

            try
            {
                var vInserir = m_DbContext.SAU_TB_SERVICO_AND.FirstOrDefault(s => s.COD_SERV == obj.COD_SERV);

                if (vInserir == null)
                {
                    obj.ID_REG = GetMaxPkServico();
                    m_DbContext.SAU_TB_SERVICO_AND.Add(obj);

                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Registro Inserido com Sucesso");
                    }

                }
                else
                {
                    res.Erro("Serviço Existente, Favor Alterar o Código Serviço");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        #endregion

        #region .: Aba 5 :.

        public IQueryable<SAU_TB_SERV_X_HOSP_AND> GetServHosp(int codHosp, int? codServ)
        {
            IQueryable<SAU_TB_SERV_X_HOSP_AND> query = from sh in m_DbContext.SAU_TB_SERV_X_HOSP_AND
                                                       where (sh.COD_HOSP == codHosp)
                                                       && (sh.COD_SERV == codServ || codServ == null)
                                                       && (sh.DAT_FIM_VIGENCIA == null)
                                                       select sh;

            return query;

        }

        public decimal GetMaxPkServPrest()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.SAU_TB_SERV_X_HOSP_AND.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public Resultado DeleteServPrest(SAU_TB_SERV_X_HOSP_AND obj)
        {

            Resultado res = new Resultado();

            try
            {
                var delete = m_DbContext.SAU_TB_SERV_X_HOSP_AND.Find(obj.ID_REG);
                delete.DAT_FIM_VIGENCIA = DateTime.Today;
                delete.USU_ALTERACAO = obj.USU_ALTERACAO;

                int rows_delete = m_DbContext.SaveChanges();

                if (rows_delete > 0)
                {
                    res.Sucesso("Serviço Deletado com Sucesso");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }


            return res;


        }

        public Resultado AtualizaServPrest(SAU_TB_SERV_X_HOSP_AND obj)
        {
            Resultado res = new Resultado();
            SAU_TB_SERV_X_HOSP_AND newObj = new SAU_TB_SERV_X_HOSP_AND();

            try
            {
                var atualiza = m_DbContext.SAU_TB_SERV_X_HOSP_AND.Find(obj.ID_REG);
                atualiza.DAT_FIM_VIGENCIA = DateTime.Today;
                atualiza.USU_ALTERACAO = obj.USU_ALTERACAO;

                newObj.ID_REG = GetMaxPkServPrest();
                newObj.COD_HOSP = obj.COD_HOSP;
                newObj.COD_SERV = obj.COD_SERV;
                newObj.VALOR = obj.VALOR;
                newObj.VALOR_PROPOSTO = 0;
                newObj.PORCENTAGEM = 0;
                newObj.PORC_PROPOSTA = 0;
                newObj.PORC_DESCONTO = 0;
                newObj.PORC_DESC_PROPOSTO = 0;
                newObj.DAT_INI_VIGENCIA = obj.DAT_INI_VIGENCIA;
                newObj.DAT_FIM_VIGENCIA = null;
                newObj.DAT_PROPOSTA = obj.DAT_PROPOSTA;
                newObj.USU_ALTERACAO = obj.USU_ALTERACAO;

                m_DbContext.SAU_TB_SERV_X_HOSP_AND.Add(newObj);

                int rows_update = m_DbContext.SaveChanges();

                if (rows_update > 0)
                {
                    res.Sucesso("Serviço Atualizado com Sucesso");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado InserirServPrest(SAU_TB_SERV_X_HOSP_AND obj)
        {
            Resultado res = new Resultado();

            try
            {
                int rows_insert = m_DbContext.SaveChanges();

                var vInserir = m_DbContext.SAU_TB_SERV_X_HOSP_AND.FirstOrDefault(sp => sp.COD_HOSP == obj.COD_HOSP
                                                                                    && sp.COD_SERV == obj.COD_SERV
                                                                                    && sp.DAT_FIM_VIGENCIA == null);
                if (vInserir == null)
                {
                    obj.ID_REG = GetMaxPkServPrest();
                    obj.VALOR_PROPOSTO = 0;
                    obj.PORCENTAGEM = 0;
                    obj.PORC_PROPOSTA = 0;
                    obj.PORC_DESCONTO = 0;
                    obj.PORC_DESC_PROPOSTO = 0;
                    obj.DAT_FIM_VIGENCIA = null;
                    obj.DAT_PROPOSTA = null;

                    m_DbContext.SAU_TB_SERV_X_HOSP_AND.Add(obj);

                    int row_insert = m_DbContext.SaveChanges();


                    if (row_insert > 0)
                    {
                        res.Sucesso("Serviço Inserido com Sucesso");
                    }
                }
                else
                {
                    res.Erro("O Contrato já possui esse Serviço, Favor verificar");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }


        #endregion

        #region .: Aba 6 :.

        public Resultado AtualizaObservacaoContratual(SAU_TB_HOSP_AND obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualizaObs = m_DbContext.SAU_TB_HOSP_AND.Find(obj.ID_REG);
                atualizaObs.OBSERVACAOCONTRATUAL = obj.OBSERVACAOCONTRATUAL;

                int rows_update = m_DbContext.SaveChanges();

                if (rows_update > 0)
                {
                    res.Sucesso("Registro Alterado com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        #endregion

        #region .: Aba 7 :.

        public IQueryable<SAU_TB_HOSP_AND> CarregaGeralHospital()
        {
            IQueryable<SAU_TB_HOSP_AND> query = from h in m_DbContext.SAU_TB_HOSP_AND
                                                where (h.DAT_FIM_VIGENCIA == null)
                                                select h;

            foreach (var item in query)
            {
                item.NOME_FANTASIA = item.COD_HOSP + " - " + item.NOME_FANTASIA.Replace(item.COD_HOSP.ToString(), "").Replace(" - ", "");
            }

            return query.OrderBy(x => x.COD_HOSP);

        }

        public List<ExportaArquivoScam> ExportaArquivoScam(int codHosp)
        {
            IEnumerable<ExportaArquivoScam> exportaArquivoScam = new List<ExportaArquivoScam>();

            exportaArquivoScam =
                m_DbContext.Database.SqlQuery<ExportaArquivoScam>(@"SELECT DISTINCT  Tb2.Cod_Tab_Servicos As Cod_Tab_Servicos, 
                                                                                     Tb3.Cod_Recurso AS Cod_Recurso,   
                                                                                     to_date(sha.dat_ini_vigencia) As DtReajuste,
                                                                                     sha.valor As Valor,  Tb3.RCOSEQ AS RCOSEQ,  
                                                                                     Tb3.Rcocodprocedimento as RCOCODPROCEDIMENTO, 
                                                                                     sha.cod_hosp CodHosp
                                                                     FROM att.TB_TAB_RECURSO  TR,  att.Tb_Cond_Contrat  Tb2,  att.Tb_Tipo_Cond_Conv  Tb1,  att.cadtblrcorecursocodigo  Tb3,  att.tb_recurso  r, 
                                                                      att.tb_tipo_recurso  tip,
                                                                      own_funcesp.sau_tb_serv_x_hosp_and sha
                                                                     WHERE ((TR.COD_TAB_RECURSO = Tb2.COD_TAB_SERVICOS)
                                                                     OR  (Tb2.COD_TAB_SERVICOS IS NULL and TR.COD_TAB_RECURSO = Tb2.Cod_Tab_Mat_Med))
                                                                     AND Tb1.COD_TIPO_COND_CONT = Tb2.COD_TIPO_COND_CONT AND r.COD_RECURSO = Tb3.COD_RECURSO AND r.COD_TIP_RECURSO = tip.COD_TIP_RECURSO  
                                                                     AND Tb3.rcodatdesativacao Is Null  AND sha.cod_serv = tb3.rcocodprocedimento  AND tip.TIP_RECURSO in ('M','T') AND R.COD_TIP_RECURSO not IN (3,7) 
                                                                     AND R.COD_TIP_RECURSO not IN (3,7,23,24,25,27,29)
                                                                     AND Tb1.DAT_VALIDADE = (select MAX(DAT_VALIDADE) from att.Tb_Tipo_Cond_Conv TCC where TCC.COD_CONVENENTE = Tb1.Cod_Convenente)
                                                                     AND Tb3.RCOCODPROCEDIMENTO in (Select to_char(s1.cod_serv) from own_funcesp.sau_tb_serv_x_hosp_and  s1 
                                                                     where  s1.dat_ini_vigencia = (select Max(s2.dat_ini_vigencia) from own_funcesp.sau_tb_serv_x_hosp_and  s2 
                                                                     where s2.cod_hosp = s1.cod_hosp and s2.cod_serv = s1.cod_serv) and s1.cod_hosp in (" + codHosp + ")) AND to_number(Tb1.COD_CONVENENTE)  in (" + codHosp + ") and sha.cod_hosp = (" + codHosp + ") and sha.dat_fim_vigencia is null");

            return exportaArquivoScam.ToList();
        }

        public Resultado AtualizaTabelaValoresSCAM(TB_VAL_RECURSO tValRecurso, ExportaArquivoScam arquivoScam)
        {
            Resultado res = new Resultado();

            try
            {
                var Recurso = m_DbContext.TB_VAL_RECURSO.FirstOrDefault(x => x.COD_TAB_RECURSO == tValRecurso.COD_TAB_RECURSO
                                                                 && x.COD_RECURSO == tValRecurso.COD_RECURSO
                                                                 && x.DAT_VAL_RECURSO == tValRecurso.DAT_VAL_RECURSO);

                if (Recurso == null)
                {

                    m_DbContext.TB_VAL_RECURSO.Add(tValRecurso);
                    int retorno = m_DbContext.SaveChanges();

                    if (retorno > 0)
                    {
                        res.Sucesso("Registro Inserido com Sucesso");
                    }

                }
                else
                {
                    res.Erro("Registro Existente na Base");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }



            return res;
        }

        public Resultado InserirLog(SAU_TB_LOG_AND obj)
        {
            Resultado res = new Resultado();

            try
            {
                obj.ID_REG = GetMaxPKLog();

                m_DbContext.SAU_TB_LOG_AND.Add(obj);

                int rows_insert = m_DbContext.SaveChanges();

                if (rows_insert > 0)
                {
                    res.Sucesso("Registro Inserido com Sucesso");

                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public decimal GetMaxPKLog()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.SAU_TB_LOG_AND.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public List<SAU_TB_LOG_AND_view> GetData(int startRowIndex, int maximumRows, int? codHosp, DateTime? datExportacao, string sortParameter)
        {
            return GetWhere(codHosp, datExportacao).GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<SAU_TB_LOG_AND_view> GetWhere(int? codHosp, DateTime? datExportacao)
        {

            IQueryable<SAU_TB_LOG_AND_view> query = from l in m_DbContext.SAU_TB_LOG_AND
                                                    where (l.COD_HOSP == codHosp || codHosp == null)
                                                       && (l.DAT_PROCESSAMENTO == datExportacao || datExportacao == null)
                                                    group l by new { l.COD_HOSP, l.DAT_PROCESSAMENTO, l.USU_PROCESSAMENTO } into log
                                                    select new SAU_TB_LOG_AND_view
                                                    {
                                                        COD_HOSP = log.Key.COD_HOSP ?? 0,
                                                        QTD_SERV = log.Count(),
                                                        DAT_PROCESSAMENTO = log.Key.DAT_PROCESSAMENTO,
                                                        USU_PROCESSAMENTO = log.Key.USU_PROCESSAMENTO

                                                    };


            return query;

        }

        public int GetDataCount(int? codHosp, DateTime? datExportacao)
        {
            return GetWhere(codHosp, datExportacao).Count();
        }


        #endregion


    }
}
