using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Framework;
using IntegWeb.Entidades;
using System.Data.Entity.Validation;
using System.Data.Objects;

namespace IntegWeb.Saude.Aplicacao.DAL.Processos
{
    public class MemorialCalculoUnimedDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();


        public class CAD_TBL_CONTROLEUNIMED_view2
        {
            public decimal COD_CONTROLEUNIMED { get; set; }
            public System.DateTime DAT_GERACAO { get; set; }
            public string COD_IDENTIFICACAO { get; set; }
            public string NOM_PARTICIP { get; set; }
            public short COD_EMPRS { get; set; }
            public int NUM_MATRICULA { get; set; }
            public string SUB_MATRICULA { get; set; }
            public string COD_UNIMED { get; set; }
            public string DES_PLANO { get; set; }
            public string TIPO { get; set; }
            public string COD_PLANO_CESP { get; set; }
            public string COBRANCA_MEMORIAL { get; set; }
            public Nullable<decimal> VALOR { get; set; }
        }

        public IQueryable<CAD_TBL_CONTROLEUNIMED_view2> GetWhere(string codUnimed, DateTime? datIni, DateTime? datFim)
        {
            IQueryable<CAD_TBL_CONTROLEUNIMED_view2> query;

            query = from u in m_DbContext.CAD_TBL_CONTROLEUNIMED
                    from v in m_DbContext.SAU_TBL_VALORCARTUNIMED
                    where u.COD_UNIMED == v.COD_PLANO
                    && (u.COD_UNIMED == codUnimed || codUnimed == null)
                    && (u.DAT_GERACAO >= datIni || datIni == null)
                    && (u.DAT_GERACAO <= datFim || datFim == null)
                    && (u.TIPO.Contains("INCLUSÃO") ||
                    u.TIPO.Contains("INCLUSAO"))
                    && u.COBRANCA_MEMORIAL == "N"
                    && v.DAT_FIM_VIGENCIA == null
                    select new CAD_TBL_CONTROLEUNIMED_view2()
                    {
                        DAT_GERACAO = u.DAT_GERACAO,
                        COD_CONTROLEUNIMED = u.COD_CONTROLEUNIMED,
                        COD_IDENTIFICACAO = u.COD_IDENTIFICACAO,
                        NOM_PARTICIP = u.NOM_PARTICIP,
                        COD_EMPRS = u.COD_EMPRS,
                        NUM_MATRICULA = u.NUM_MATRICULA,
                        SUB_MATRICULA = u.SUB_MATRICULA,
                        COD_PLANO_CESP = u.COD_PLANO_CESP,
                        COD_UNIMED = u.COD_UNIMED,
                        DES_PLANO = u.DES_PLANO,
                        TIPO = u.TIPO,
                        COBRANCA_MEMORIAL = u.COBRANCA_MEMORIAL,
                        VALOR = v.INCLUSAO
                    };
            query = query.Concat(
                    from u in m_DbContext.CAD_TBL_CONTROLEUNIMED
                    from v in m_DbContext.SAU_TBL_VALORCARTUNIMED
                    where u.COD_UNIMED == v.COD_PLANO
                    && (u.COD_UNIMED == codUnimed || codUnimed == null)
                    && (u.DAT_GERACAO >= datIni || datIni == null)
                    && (u.DAT_GERACAO <= datFim || datFim == null)
                    && (u.TIPO.Contains("RENOVACAO"))
                    && u.COBRANCA_MEMORIAL == "N"
                    && v.DAT_FIM_VIGENCIA == null
                    select new CAD_TBL_CONTROLEUNIMED_view2()
                    {
                        DAT_GERACAO = u.DAT_GERACAO,
                        COD_CONTROLEUNIMED = u.COD_CONTROLEUNIMED,
                        COD_IDENTIFICACAO = u.COD_IDENTIFICACAO,
                        NOM_PARTICIP = u.NOM_PARTICIP,
                        COD_EMPRS = u.COD_EMPRS,
                        NUM_MATRICULA = u.NUM_MATRICULA,
                        SUB_MATRICULA = u.SUB_MATRICULA,
                        COD_PLANO_CESP = u.COD_PLANO_CESP,
                        COD_UNIMED = u.COD_UNIMED,
                        DES_PLANO = u.DES_PLANO,
                        TIPO = u.TIPO,
                        COBRANCA_MEMORIAL = u.COBRANCA_MEMORIAL,
                        VALOR = v.RENOVACAO
                    });

            query = query.Concat(from u in m_DbContext.CAD_TBL_CONTROLEUNIMED
                                 from v in m_DbContext.SAU_TBL_VALORCARTUNIMED
                                 where u.COD_UNIMED == v.COD_PLANO
                                 && (u.COD_UNIMED == codUnimed || codUnimed == null)
                                 && (u.DAT_GERACAO >= datIni || datIni == null)
                                 && (u.DAT_GERACAO <= datFim || datFim == null)
                                 && (u.TIPO.Contains("SEGUNDA_VIA"))
                                 && u.COBRANCA_MEMORIAL == "N"
                                 && v.DAT_FIM_VIGENCIA == null
                                 select new CAD_TBL_CONTROLEUNIMED_view2()
                                 {
                                     DAT_GERACAO = u.DAT_GERACAO,
                                     COD_CONTROLEUNIMED = u.COD_CONTROLEUNIMED,
                                     COD_IDENTIFICACAO = u.COD_IDENTIFICACAO,
                                     NOM_PARTICIP = u.NOM_PARTICIP,
                                     COD_EMPRS = u.COD_EMPRS,
                                     NUM_MATRICULA = u.NUM_MATRICULA,
                                     SUB_MATRICULA = u.SUB_MATRICULA,
                                     COD_PLANO_CESP = u.COD_PLANO_CESP,
                                     COD_UNIMED = u.COD_UNIMED,
                                     DES_PLANO = u.DES_PLANO,
                                     TIPO = u.TIPO,
                                     COBRANCA_MEMORIAL = u.COBRANCA_MEMORIAL,
                                     VALOR = v.SEGUNDA_VIA
                                 });

            return query;
        }

        public int GetDataCount(string codUnimed, DateTime? datIni, DateTime? datFim)
        {
            return GetWhere(codUnimed, datIni, datFim).Count();
        }

        public List<CAD_TBL_UNIMEDARQUIVO> GetUnimed()
        {
            IQueryable<CAD_TBL_UNIMEDARQUIVO> query;

            query = from un in m_DbContext.CAD_TBL_UNIMEDARQUIVO
                    select un;

            return query.ToList();
        }

        public Resultado Inserir(FUN_TBL_MEMORIAL_UNIMED obj)
        {
            Resultado res = new Resultado();
            FUN_TBL_MEMORIAL_UNIMED objNew = new FUN_TBL_MEMORIAL_UNIMED();

            objNew.ID_REG = GetMaxPk();
            objNew.DAT_GERACAO = obj.DAT_GERACAO;
            objNew.COD_EMPRS = obj.COD_EMPRS;
            objNew.NUM_MATRICULA = obj.NUM_MATRICULA;
            objNew.SUB_MATRICULA = obj.SUB_MATRICULA;
            objNew.COD_IDENTIFICACAO = obj.COD_IDENTIFICACAO;
            objNew.NOM_PARTICIP = obj.NOM_PARTICIP;
            objNew.COD_UNIMED = obj.COD_UNIMED;
            objNew.MOVIMENTACAO = obj.MOVIMENTACAO;
            objNew.COD_PLANO_CESP = obj.COD_PLANO_CESP;

            m_DbContext.FUN_TBL_MEMORIAL_UNIMED.Add(objNew);
            int rows_insert = m_DbContext.SaveChanges();

            if (rows_insert > 0)
            {
                res.Sucesso("Inclusão Feita com Sucesso");
            }


            return res;
        }

        public Resultado InserirHist(FUN_TBL_MEMORIAL_UNIMED_HIST objHist)
        {
            Resultado res = new Resultado();

            try
            {

                string sDAT_PAGAMENTO = "null";
                string sVALOR = "null";

                if (objHist.DAT_PAGAMENTO!=null)
                {
                    sDAT_PAGAMENTO = "to_date('" + (objHist.DAT_PAGAMENTO ?? DateTime.Now).ToString("dd/MM/yyyy") + "','DD/MM/YYYY') ";
                }

                if (objHist.VALOR != null)
                {
                    sVALOR = (objHist.VALOR ?? 0).ToString("0.00");
                }

                int insere = m_DbContext.Database.ExecuteSqlCommand("insert into own_funcesp.fun_tbl_memorial_unimed_hist(id_reg_hist,dat_geracao,cod_emprs,num_matricula,sub_matricula,cod_identificacao,nom_particip,movimentacao,cod_unimed,cod_plano_cesp,dat_pagamento,valor) values ("
                    + GetMaxPkHist() + " , to_date('" + objHist.DAT_GERACAO.ToString("dd/MM/yyyy") + "','DD/MM/YYYY'),"+ objHist.COD_EMPRS + " , " 
                    + objHist.NUM_MATRICULA + " , '" +  objHist.SUB_MATRICULA  + "' , '" + objHist.COD_IDENTIFICACAO
                    + "' , '" + objHist.NOM_PARTICIP + "' , '" + objHist.MOVIMENTACAO + "' , '"
                    + objHist.COD_UNIMED + "' , '" + objHist.COD_PLANO_CESP + "' , " + sDAT_PAGAMENTO + ","
                    + sVALOR.Replace(",",".") + ")");

                if (insere > 0)
                {

                    res.Sucesso("Registro inserido com sucesso.");

                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado Update(int valCodEmprs, int valNumMatricula, string valSubMatricula,string movimentacao, DateTime data)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.CAD_TBL_CONTROLEUNIMED.FirstOrDefault(obj => obj.COD_EMPRS == valCodEmprs && obj.NUM_MATRICULA == valNumMatricula 
                    && obj.SUB_MATRICULA == valSubMatricula && obj.TIPO == movimentacao
                    && EntityFunctions.TruncateTime(obj.DAT_GERACAO) == EntityFunctions.TruncateTime(data)
                    );

                if (atualiza != null)
                {
                    atualiza.COBRANCA_MEMORIAL = "S";
                }
                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated == 1)
                {
                    res.Sucesso("Registro atualizado com sucesso.");
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
            maxPK = m_DbContext.FUN_TBL_MEMORIAL_UNIMED.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public decimal GetMaxPkHist()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.FUN_TBL_MEMORIAL_UNIMED_HIST.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG_HIST) + 1;
            return maxPK;
        }

        public void Delete()
        {


            try
            {
                int delete = m_DbContext.Database.ExecuteSqlCommand("delete OWN_FUNCESP.FUN_TBL_MEMORIAL_UNIMED");

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

    }
}
