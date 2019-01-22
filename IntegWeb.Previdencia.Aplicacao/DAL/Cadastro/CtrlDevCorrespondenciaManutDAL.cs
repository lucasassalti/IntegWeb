using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Cadastro
{
    public class CtrlDevCorrespondenciaManutDAL
    {
        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        #region "TipoDocumento"

        public IQueryable<CAD_TBL_CTRLDEV_TIPODOCUMENTO> GetTipoDocumento()
        {
            IQueryable<CAD_TBL_CTRLDEV_TIPODOCUMENTO> query;

            query = from u in m_DbContext.CAD_TBL_CTRLDEV_TIPODOCUMENTO
                    select u;

            return query;
        }

        public Resultado AtualizaTipoDocumento(CAD_TBL_CTRLDEV_TIPODOCUMENTO obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_TIPODOCUMENTO.FirstOrDefault(p => p.ID_REG == obj.ID_REG);

                if (atualiza != null)
                {
                    atualiza.DESCRICAO = obj.DESCRICAO;

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


        public Resultado InseriTipoDocumento(CAD_TBL_CTRLDEV_TIPODOCUMENTO obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida o tipo de Documento
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_TIPODOCUMENTO.FirstOrDefault(p => p.DESCRICAO == obj.DESCRICAO);

                if (atualiza == null)
                {
                    obj.ID_REG = GetMaxPk_TipoDocumento() + 1;
                    m_DbContext.CAD_TBL_CTRLDEV_TIPODOCUMENTO.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Esse motivo devolução já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public int GetMaxPk_TipoDocumento()
        {
            int maxPK = 0;
            maxPK = Convert.ToInt16(m_DbContext.CAD_TBL_CTRLDEV_TIPODOCUMENTO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG));
            return maxPK;
        }

        #endregion

        #region "TipoMotivoDevolucao"

        public IQueryable<CAD_TBL_CTRLDEV_TIPOMOTDEV> GetTipoMotivoDevolucao()
        {
            IQueryable<CAD_TBL_CTRLDEV_TIPOMOTDEV> query;

            query = from u in m_DbContext.CAD_TBL_CTRLDEV_TIPOMOTDEV
                    select u;

            return query;
        }

        public Resultado AtualizaTipoMotivoDevolucao(CAD_TBL_CTRLDEV_TIPOMOTDEV obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_TIPOMOTDEV.FirstOrDefault(p => p.ID_REG == obj.ID_REG);

                if (atualiza != null)
                {
                    atualiza.DESCRICAO = obj.DESCRICAO;

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

        public Resultado InseriTipoMotivoDevolucao(CAD_TBL_CTRLDEV_TIPOMOTDEV obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida o tipo de Motivo Devolucao
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_TIPOMOTDEV.FirstOrDefault(p => p.DESCRICAO == obj.DESCRICAO);

                if (atualiza == null)
                {
                    obj.ID_REG = GetMaxPk_TipoMotivoDevolucao() + 1;
                    m_DbContext.CAD_TBL_CTRLDEV_TIPOMOTDEV.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Esse motivo devolução já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public int GetMaxPk_TipoMotivoDevolucao()
        {
            int maxPK = 0;
            maxPK = Convert.ToInt16(m_DbContext.CAD_TBL_CTRLDEV_TIPOMOTDEV.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG));
            return maxPK;
        }

        #endregion

        #region "TipoAcao"

        public IQueryable<CAD_TBL_CTRLDEV_TIPOACAO> GetTipoAcao()
        {
            IQueryable<CAD_TBL_CTRLDEV_TIPOACAO> query;
            query = from u in m_DbContext.CAD_TBL_CTRLDEV_TIPOACAO
                    select u;

            return query;
        }

        public Resultado AtualizaTipoAcao(CAD_TBL_CTRLDEV_TIPOACAO obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_TIPOACAO.FirstOrDefault(p => p.ID_REG == obj.ID_REG);

                if (atualiza != null)
                {
                    atualiza.DESCRICAO = obj.DESCRICAO;

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

        public Resultado InseriTipoAcao(CAD_TBL_CTRLDEV_TIPOACAO obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida o tipo de ação
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_TIPOACAO.FirstOrDefault(p => p.DESCRICAO == obj.DESCRICAO);

                if (atualiza == null)
                {
                    obj.ID_REG = GetMaxPk_TipoAcao() + 1;
                    m_DbContext.CAD_TBL_CTRLDEV_TIPOACAO.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Essa ação já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }


        public int GetMaxPk_TipoAcao()
        {
            int maxPK = 0;
            maxPK = Convert.ToInt16(m_DbContext.CAD_TBL_CTRLDEV_TIPOACAO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG));
            return maxPK;
        }

        #endregion

        #region "FluxoAcao"

        //View local
        public class CAD_VIEW_CTRLDEV_FLUXOACAO
        {
            public Nullable<decimal> ID_REG { get; set; }
            public string DescricaoDoc { get; set; }
            public string DescricaoAcao { get; set; }
            public string TempoPrazo { get; set; }
            public decimal IdDescricaoDoc { get; set; }
            public decimal IdDescricaoAcao { get; set; }

        }

        public IQueryable<CAD_VIEW_CTRLDEV_FLUXOACAO> GetFluxoAcao()
        {
            IQueryable<CAD_VIEW_CTRLDEV_FLUXOACAO> query;

            query = (from u in m_DbContext.CAD_TBL_CTRLDEV_FLUXOACAO
                     join doc in m_DbContext.CAD_TBL_CTRLDEV_TIPODOCUMENTO on u.ID_TIPODOCUMENTO equals doc.ID_REG
                     join aca in m_DbContext.CAD_TBL_CTRLDEV_TIPOACAO on u.ID_TIPOPLANOACAO equals aca.ID_REG
                     select new CAD_VIEW_CTRLDEV_FLUXOACAO
                     {
                         ID_REG = u.ID_REG,
                         IdDescricaoDoc = doc.ID_REG,
                         DescricaoDoc = doc.DESCRICAO,
                         IdDescricaoAcao = aca.ID_REG,
                         DescricaoAcao = aca.DESCRICAO,
                         TempoPrazo = u.TEMPO_PRAZO
                     });


            return query;
        }

        public Resultado AtualizaFluxoAcao(CAD_TBL_CTRLDEV_FLUXOACAO obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida a combinação de Fluxo e ação;
                //var atualiza = m_DbContext.CAD_TBL_CTRLDEV_FLUXOACAO.FirstOrDefault(p => p.ID_REG == obj.ID_REG);

                //Valida a combinação de Fluxo e ação;
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_FLUXOACAO.FirstOrDefault(p => p.ID_TIPOPLANOACAO == obj.ID_TIPOPLANOACAO
                    && p.ID_TIPODOCUMENTO == obj.ID_TIPODOCUMENTO && p.TEMPO_PRAZO == obj.TEMPO_PRAZO );

                if (atualiza != null)
                {
                    atualiza.ID_TIPODOCUMENTO = obj.ID_TIPODOCUMENTO;
                    atualiza.ID_TIPOPLANOACAO = obj.ID_TIPOPLANOACAO;
                    atualiza.TEMPO_PRAZO = obj.TEMPO_PRAZO;

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


        public Resultado InseriFluxoAcao(CAD_TBL_CTRLDEV_FLUXOACAO obj)
        {
            Resultado res = new Resultado();

            try
            {  
                //Valida a combinação de Fluxo e ação;
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_FLUXOACAO.FirstOrDefault(p => p.ID_TIPOPLANOACAO == obj.ID_TIPOPLANOACAO
                     && p.ID_TIPODOCUMENTO == obj.ID_TIPODOCUMENTO && (p.TEMPO_PRAZO == obj.TEMPO_PRAZO || obj.TEMPO_PRAZO == null));

                if (atualiza == null)
                {
                    obj.ID_REG = GetMaxPk_FluxoAcao() + 1;
                    m_DbContext.CAD_TBL_CTRLDEV_FLUXOACAO.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Essa Combinação de Fluxo e ação já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }


        public int GetMaxPk_FluxoAcao()
        {
            int maxPK = 0;
            maxPK = Convert.ToInt16(m_DbContext.CAD_TBL_CTRLDEV_FLUXOACAO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG));
            return maxPK;
        }

        #endregion

    }
}
