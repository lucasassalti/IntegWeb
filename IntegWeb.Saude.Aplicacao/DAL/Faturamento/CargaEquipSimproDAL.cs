using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Faturamento
{
    public class CargaEquipSimproDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public class TB_CONVENENTE_view
        {

            public decimal COD_CONVENENTE { get; set; }
            public string NOM_CONVENENTE { get; set; }
        }

        public class TB_TIPO_COND_CONV_view
        {

            public decimal COD_TIPO_COND_CONT { get; set; }
            public string DES_TIPO_COND_CONT { get; set; }
            public System.DateTime DAT_VALIDADE { get; set; }
            public string COD_DES_TIPO_COND_CONT
            {
                get
                {
                    return String.Format("{0} - {1}", DAT_VALIDADE.ToString("dd.MM.yyyy"), DES_TIPO_COND_CONT);

                }
            }

            public string COD_TIPO_COND_CONT_VALUE
            {
                get
                {
                    return String.Format("{0} - {1}", DAT_VALIDADE.ToString("dd.MM.yyyy"), COD_TIPO_COND_CONT.ToString());
                }
            }

        }

        public class CADTBLRCORECURSOCODIGO_view
        {
            public decimal COD_RECURSO { get; set; }
            public string desRecurso { get; set; }
            public string RCOCODPROCEDIMENTO { get; set; }
            public string TPACOD { get; set; }

        }

        public class SAU_TBL_QTDE_MATMED_view
        {
            public decimal COD_RECURSO { get; set; }
            public DateTime DAT_INIVIG { get; set; }
            public DateTime DTH_ACAO { get; set; }

        }

        public class SAU_TBL_QTDE_MATMED_view1
        {
            public decimal COD_RECURSO { get; set; }
            public DateTime DAT_INIVIG { get; set; }
            public DateTime? DAT_FIMVIG { get; set; }
            public int QTD { get; set; }
            public DateTime DTH_ACAO { get; set; }

        }

        public class SAU_TBL_QTDE_MATMED_view2
        {
            public decimal COD_RECURSO { get; set; }
            public DateTime DAT_INIVIG { get; set; }
            public DateTime DAT_FIMVIG { get; set; }
            public int QTD { get; set; }
            public DateTime DTH_ACAO { get; set; }

        }

        public class TB_EQUIP_SIMPRO_view
        {
            public string COD_SIMPRO { get; set; }
            public string DES_SIMPRO { get; set; }
            public decimal TAXA_APLICADA { get; set; }
            public string TIPO_PRECO { get; set; }
            public System.DateTime DAT_VALIDADE { get; set; }

        }

        public List<TB_CONVENENTE_view> GetConvenente()
        {
            IQueryable<TB_CONVENENTE_view> query;

            //subquery
            query = from conv in m_DbContext.TB_CONVENENTE
                    from tipo in m_DbContext.TB_TIPO_COND_CONV
                    from cond in m_DbContext.TB_COND_CONTRAT
                    where
                    tipo.COD_CONVENENTE == conv.COD_CONVENENTE
                    && (conv.SIT_CONVENENTE == "1")
                    && (tipo.DAT_VALIDADE == (m_DbContext.TB_TIPO_COND_CONV.Where(tipo1 => tipo1.COD_CONVENENTE == tipo.COD_CONVENENTE).Max(data => data.DAT_VALIDADE)))
                    && (tipo.COD_TIPO_COND_CONT == cond.COD_TIPO_COND_CONT)
                    && (cond.IDC_UTILIZ_SIMPRO == "S")
                    orderby conv.COD_CONVENENTE
                    select new TB_CONVENENTE_view
                    {

                        COD_CONVENENTE = conv.COD_CONVENENTE,
                        NOM_CONVENENTE = conv.NOM_CONVENENTE
                    };

            List<TB_CONVENENTE_view> lista = query.ToList();
            lista.ForEach(conv => conv.NOM_CONVENENTE = conv.COD_CONVENENTE + " - " + conv.NOM_CONVENENTE);

            return lista;
        }

        public List<TB_TIPO_COND_CONV_view> GetCondicaoContratual(decimal codigoConv)
        {

            IQueryable<TB_TIPO_COND_CONV_view> query;

            query = from tipo in m_DbContext.TB_TIPO_COND_CONV
                    from cont in m_DbContext.TB_TIPO_COND_CONTRAT
                    where tipo.COD_CONVENENTE == codigoConv
                    && (tipo.COD_TIPO_COND_CONT == cont.COD_TIPO_COND_CONT)
                    && (tipo.DAT_VALIDADE == (m_DbContext.TB_TIPO_COND_CONV.Where(tipo1 => tipo1.COD_CONVENENTE == tipo.COD_CONVENENTE).Max(data => data.DAT_VALIDADE)))
                    orderby tipo.COD_TIPO_COND_CONT
                    select new TB_TIPO_COND_CONV_view
                    {
                        COD_TIPO_COND_CONT = tipo.COD_TIPO_COND_CONT,
                        DAT_VALIDADE = tipo.DAT_VALIDADE,
                        DES_TIPO_COND_CONT = cont.DES_TIPO_COND_CONT
                    };

            List<TB_TIPO_COND_CONV_view> lista = query.ToList();

          

            return lista;
        }

        public List<TB_EQUIP_SIMPRO_view> GetEquipSimproList()
        {
            return GetEquipSimpro().ToList();
        }

        public IQueryable<TB_EQUIP_SIMPRO_view> GetEquipSimpro()
        {

            IQueryable<TB_EQUIP_SIMPRO_view> query;
            var tpo = new string[] { "19", "00" };

            query =

                    from es in m_DbContext.TB_EQUIP_SIMPRO
                    join rco in m_DbContext.CADTBLRCORECURSOCODIGO
                    on es.COD_SIMPRO.Trim() equals rco.RCOCODPROCEDIMENTO into g
                    from rco in g.DefaultIfEmpty()
                    where rco.RCODATDESATIVACAO == null
                    && tpo.Contains(rco.TPACOD)
                    select new TB_EQUIP_SIMPRO_view
                    {

                        COD_SIMPRO = rco.RCOCODPROCEDIMENTO,
                        DAT_VALIDADE = es.DAT_VALIDADE,
                        TIPO_PRECO = es.TIPO_PRECO,
                        TAXA_APLICADA = es.TAXA_APLICADA,

                    };


            return query.Distinct();
        }

        public List<CADTBLRCORECURSOCODIGO_view> GetProcedimento(string procedimento)
        {
            IQueryable<CADTBLRCORECURSOCODIGO_view> query;


            query =
                    from rco in m_DbContext.CADTBLRCORECURSOCODIGO
                    from r in m_DbContext.TB_RECURSO
                    where (rco.COD_RECURSO == r.COD_RECURSO)
                    && (rco.RCOCODPROCEDIMENTO == procedimento)
                    && (rco.RCODATDESATIVACAO == null)
                    && (m_DbContext.CADTBLSMO.Any(p => p.COD_RECURSO == rco.COD_RECURSO))
                    select new CADTBLRCORECURSOCODIGO_view
                    {
                        COD_RECURSO = rco.COD_RECURSO,
                        desRecurso = r.DES_RECURSO,
                        RCOCODPROCEDIMENTO = rco.RCOCODPROCEDIMENTO

                    };
                   
                    

          

            return query.Distinct().ToList();
        }

        public List<SAU_TBL_QTDE_MATMED_view1> GetMatMed(decimal recurso)
        {
            IQueryable<SAU_TBL_QTDE_MATMED_view1> query;


            query = from mat in m_DbContext.SAU_TBL_QTDE_MATMED
                    join r in
                        (from mat1 in m_DbContext.SAU_TBL_QTDE_MATMED
                         where mat1.COD_RECURSO == recurso
                         group mat1 by new { mat1.COD_RECURSO, mat1.DAT_INIVIG } into g
                         select new SAU_TBL_QTDE_MATMED_view()
                             {
                                 COD_RECURSO = g.Key.COD_RECURSO,
                                 DAT_INIVIG = g.Key.DAT_INIVIG,
                                 DTH_ACAO = g.Max(x => x.DTH_ACAO)
                             }
                        )
                       on new { mat.COD_RECURSO, mat.DTH_ACAO } equals new { r.COD_RECURSO, r.DTH_ACAO }
                    where mat.COD_ACAO != "D" orderby mat.DAT_INIVIG
                    select new SAU_TBL_QTDE_MATMED_view1()
                    {
                        COD_RECURSO = mat.COD_RECURSO,
                        DAT_FIMVIG = mat.DAT_FIMVIG,
                        DAT_INIVIG = mat.DAT_INIVIG,
                        DTH_ACAO = mat.DTH_ACAO,
                        QTD = mat.QTD
                    };



            return query.ToList();

        }

        public DateTime? GetLastFimMatMed(decimal recurso)
        {
            IQueryable<SAU_TBL_QTDE_MATMED_view1> query;

            query = from mat in m_DbContext.SAU_TBL_QTDE_MATMED
                    where mat.COD_RECURSO == recurso
                    && mat.DTH_ACAO == (m_DbContext.SAU_TBL_QTDE_MATMED.Where(x => x.COD_RECURSO == recurso && x.COD_ACAO != "D").Max(max => max.DTH_ACAO))
                    select new SAU_TBL_QTDE_MATMED_view1
                    {
                        DAT_FIMVIG = mat.DAT_FIMVIG
                    };      
            
               return query.FirstOrDefault().DAT_FIMVIG;

        }

        public DateTime GetLastInicioMatMed(decimal recurso)
        {
            IQueryable<SAU_TBL_QTDE_MATMED_view2> query;


            query = from mat in m_DbContext.SAU_TBL_QTDE_MATMED
                    join r in
                        (from mat1 in m_DbContext.SAU_TBL_QTDE_MATMED
                         where mat1.COD_RECURSO == recurso
                         group mat1 by new { mat1.COD_RECURSO, mat1.DAT_INIVIG } into g
                         select new SAU_TBL_QTDE_MATMED_view()
                         {
                             COD_RECURSO = g.Key.COD_RECURSO,
                             DAT_INIVIG = g.Key.DAT_INIVIG,
                             DTH_ACAO = g.Max(x => x.DTH_ACAO)
                         }
                        )
                       on new { mat.COD_RECURSO, mat.DTH_ACAO } equals new { r.COD_RECURSO, r.DTH_ACAO }
                    where mat.COD_ACAO != "D"
                    group mat by new { mat.DAT_INIVIG } into g
                    select new SAU_TBL_QTDE_MATMED_view2()
                    {
                       
                        
                        DAT_INIVIG = g.Max(x => x.DAT_INIVIG)
                        
                    };



            return query.FirstOrDefault().DAT_INIVIG;
        }

        public DateTime GetDatValidade(decimal codTipoConvenente, decimal codConvenente)
        {

            IQueryable<TB_TIPO_COND_CONV_view> query;

            query = from conv in m_DbContext.TB_TIPO_COND_CONV
                    where conv.COD_TIPO_COND_CONT == codTipoConvenente
                    && conv.COD_CONVENENTE == codConvenente
                    group conv by new { conv.DAT_VALIDADE } into g
                    select new TB_TIPO_COND_CONV_view
                    {

                        DAT_VALIDADE = g.Max(m => m.DAT_VALIDADE)
                    };


            return query.FirstOrDefault().DAT_VALIDADE;


        }

        public Resultado ImportaDados(DataTable dt)
        {
            Resultado res = new Resultado();


            try
            {


                foreach (DataRow dr in dt.Rows)
                {

                    TB_EQUIP_SIMPRO obj = new TB_EQUIP_SIMPRO();

                    obj.COD_SIMPRO = dr["COD_TUSS"].ToString();
                    obj.DES_SIMPRO = dr["DES_SIMPRO"].ToString();
                    obj.TAXA_APLICADA = Convert.ToDecimal(dr["taxa_aplicada"]);
                    obj.TIPO_PRECO = dr["tipo_preco"].ToString();
                    obj.DAT_VALIDADE = Convert.ToDateTime(dr["dat_validade"]);


                    var insere = m_DbContext.TB_EQUIP_SIMPRO.Find(obj.COD_SIMPRO);

                    if (insere == null)
                    {

                        m_DbContext.TB_EQUIP_SIMPRO.Add(obj);
                        Savechanges();

                    }
                    else
                    {
                        res.Erro("O equipamento ja foi cadastrado");
                    }
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            return res;

            /*using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
          {

              bulkCopy.DestinationTableName = "OWN_FUNCESP.TB_EQUIP_SIMPRO";
              bulkCopy.ColumnMappings.Add("COD_TUSS", "cod_simpro");
              bulkCopy.ColumnMappings.Add("DES_SIMPRO", "des_simpro");
              bulkCopy.ColumnMappings.Add("taxa_aplicada", "taxa_aplicada");
              bulkCopy.ColumnMappings.Add("tipo_preco", "tipo_preco");
              bulkCopy.ColumnMappings.Add("dat_validade", "dat_validade");

              try
              {
                  bulkCopy.WriteToServer(dt);
                  ret = true;
              }
              catch (Exception ex)
              {
                  ret = false;
                  throw new Exception(ex.Message + "\\n\\nVerique se a planinha contém as colunas");
              }
              finally
              {
                  bulkCopy.Close();
              }
          }
          return ret;*/

        }

        public decimal GetMaxPrioridade(FATTBLRSP obj) 
        {
            
            decimal retorno = 0;
            try
            {
                var verifica = m_DbContext.FATTBLRSP.Where(m => m.COD_TIPO_COND_CONT == obj.COD_TIPO_COND_CONT
                                                            & m.DAT_VALIDADE == obj.DAT_VALIDADE
                                                             & m.RSPDATVALIDINI == obj.RSPDATVALIDINI ).Max(n => n.RSPVALPRIORIDADE);
                
                retorno = verifica;
            }
            catch (NullReferenceException e)
            {

                retorno = 0;

            }
            catch(Exception ex)
            {
                retorno = 0;
            }

            return retorno ;
        }

        public Boolean VerificarFAT(FATTBLRSP obj) 
        {
            Boolean bo = false;

            try 
            {
                var verifica = 0;
                //IQueryable<FATTBLRSP> query;
                IQueryable<string> query;
                if (obj.RSPCODSIMPROINI == null && obj.RSPCODSIMPROFIM == null) //está inserindo taxa padrão
                {
                    //verifica = m_DbContext.FATTBLRSP.Where(m => m.COD_TIPO_COND_CONT == obj.COD_TIPO_COND_CONT
                    //                                         & m.DAT_VALIDADE == obj.DAT_VALIDADE.Date
                    //                                         & m.RSPDATVALIDINI == obj.RSPDATVALIDINI.Date
                    //                                         & m.RSPCODSIMPROINI == null
                    //                                         & m.RSPCODSIMPROFIM == null).Count();
                    query = from RSP in m_DbContext.FATTBLRSP
                            where RSP.COD_TIPO_COND_CONT == obj.COD_TIPO_COND_CONT
                                                             & RSP.DAT_VALIDADE == obj.DAT_VALIDADE.Date
                                                             & RSP.RSPDATVALIDINI == obj.RSPDATVALIDINI.Date
                                                             & RSP.RSPTIPPRECO == obj.RSPTIPPRECO
                                                             & RSP.RSPCODSIMPROINI.Trim() == null
                                                             & RSP.RSPCODSIMPROFIM.Trim() == null
                            select RSP.RSPCODSIMPROINI;

                    verifica = query.Count();
                }
                else
                {
                    //verifica = m_DbContext.FATTBLRSP.Where(m => m.COD_TIPO_COND_CONT == obj.COD_TIPO_COND_CONT
                    //                                         & m.DAT_VALIDADE == obj.DAT_VALIDADE.Date
                    //                                         & m.RSPDATVALIDINI == obj.RSPDATVALIDINI.Date
                    //                                         & m.RSPCODSIMPROINI == obj.RSPCODSIMPROINI
                    //                                         & m.RSPCODSIMPROFIM == obj.RSPCODSIMPROFIM).Count();

                    query = from RSP in m_DbContext.FATTBLRSP
                            where RSP.COD_TIPO_COND_CONT == obj.COD_TIPO_COND_CONT
                                                             & RSP.DAT_VALIDADE == obj.DAT_VALIDADE.Date
                                                             & RSP.RSPDATVALIDINI == obj.RSPDATVALIDINI.Date
                                                             & RSP.RSPCODSIMPROINI.Trim() == obj.RSPCODSIMPROINI.Trim()
                                                             & RSP.RSPCODSIMPROFIM.Trim() == obj.RSPCODSIMPROFIM.Trim()
                            select RSP.RSPCODSIMPROINI;

                    verifica = query.Count();
                }
                
                
                if (verifica == 0)
                {
                    bo = true;
                }
                else 
                {
                   bo = false;
                }

            }
            catch(Exception e)
            {
            
            }

            return bo;
        }

        public Resultado InserirFattblrsp(FATTBLRSP obj)
        {
            Resultado res = new Resultado();

            try
            {
                

                //var atualiza = m_DbContext.FATTBLRSP.Find(obj.COD_TIPO_COND_CONT, obj.DAT_VALIDADE, obj.RSPDATVALIDINI, obj.RSPVALPRIORIDADE);////////////// alterar este select

                if (VerificarFAT(obj) == true)
                {
                    // inclui novo registro na tabela

                    //atualiza = obj;

                    m_DbContext.FATTBLRSP.Add(obj);

                    res.Sucesso("ok");

                }

                else
                {

                    res.Erro("Os equipamentos ja foram cadastrados");

                }


            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado InserirMatMed(SAU_TBL_QTDE_MATMED obj)
        {
            Resultado res = new Resultado();

            try
            {
                var verifica = m_DbContext.SAU_TBL_QTDE_MATMED.Find(obj.COD_RECURSO, obj.QTD, obj.DAT_INIVIG, obj.COD_ACAO, obj.DTH_ACAO);

                if (verifica == null)
                {
                    m_DbContext.SAU_TBL_QTDE_MATMED.Add(obj);
                    res = Savechanges();
                    return res;
                }
                else
                {
                    res.Erro("Ocorreu um erro ao cadastrar o equipamento");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public Resultado AtualizarMatMed(SAU_TBL_QTDE_MATMED obj)
        {
            Resultado res = new Resultado();

            try
            {
                var verifica = m_DbContext.SAU_TBL_QTDE_MATMED.FirstOrDefault(x => x.COD_RECURSO == obj.COD_RECURSO
                                                                                 && x.DAT_INIVIG == obj.DAT_INIVIG);
                if (verifica != null)
                {
                    m_DbContext.SAU_TBL_QTDE_MATMED.Add(obj);
                    res = Savechanges();
                    return res;
                }
                else
                {
                    res.Erro("A data de vigência não pode ser alterada");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public Resultado Savechanges()
        {

            Resultado res = new Entidades.Resultado();
            try
            {
                if (m_DbContext.SaveChanges() > 0)
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

        public Resultado DeleteEquipSimpro()
        {

            Resultado res = new Resultado();


            try
            {
                int rows_delete = m_DbContext.Database.ExecuteSqlCommand("delete from own_funcesp.tb_equip_simpro");



                if (rows_delete > 0)
                {
                    res.Sucesso(String.Format("{0} registros excluido.", rows_delete));
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

    }
}



