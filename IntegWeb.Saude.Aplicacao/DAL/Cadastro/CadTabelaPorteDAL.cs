using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Cadastro
{
   public class CadTabelaPorteDAL 
    {
        public class ListaDataVig
        {
           public decimal NUM_SEQ { get; set; }
           public decimal COD_CONVENENTE {get;set;}
           public string NOM_CONVENENTE { get; set; }
           public DateTime? DT_VIG_PORTE { get; set; }
           public DateTime DT_INI_VIG { get; set; }
           public DateTime? DT_FIM_VIG { get; set; }
        }

        public class ListaDataVigClasse
        {
            public decimal  NUM_SEQ { get; set; }
            public decimal  COD_CLASSE { get; set; }
            public string  NOM_CLASSE { get; set; }
            public decimal?  COD_TAB_REC { get; set; }
            public DateTime? DT_VIG_PORTE { get; set; }
            public DateTime DT_INI_VIG { get; set; }
            public DateTime? DT_FIM_VIG { get; set; }
        }
        
       public SAUDE_EntityConn m_Db_Context = new SAUDE_EntityConn();

        public List<TB_CONVENENTE> GetConvenente()
        {
            IQueryable<TB_CONVENENTE> query;

            query = from conv in m_Db_Context.TB_CONVENENTE where conv.SIT_CONVENENTE == "1" orderby conv.COD_CONVENENTE
                    select conv;

             List<TB_CONVENENTE> lista = query.ToList();

             lista.ForEach(conv => conv.NOM_CONVENENTE = conv.COD_CONVENENTE + " - " + conv.NOM_CONVENENTE.Trim());

             return lista;

        }

        public List<DateTime> GetDatVig()
        {
            IQueryable<CADTBLVPRVALORPORTEREC> query = from dat in m_Db_Context.CADTBLVPRVALORPORTEREC orderby dat.VPRDATVALIDADE
                                                       select dat;

            return query.Select(s => s.VPRDATVALIDADE).Distinct().OrderBy( s => s).ToList();
            

        }

        public DataTable GetTabRec(decimal codConv)
        {
            DataTable dt = new DataTable();

            ConexaoOracle oracle = new ConexaoOracle();

            try
            {

            oracle.AdicionarParametroCursor("DADOS");
            oracle.AdicionarParametro("P_COD_CONVENENTE", codConv);
            OracleDataAdapter adpt = oracle.ExecutarAdapter("SAU_PKG_TABELA_PORTE.PROC_LISTAR_CONV_TAB_REC");

            adpt.Fill(dt);
            adpt.Dispose();

            DataColumn dc = dt.Columns.Add("COD_DESC_TAB_REC", typeof(string));
            dc.Expression = string.Format("{0}+' - '+{1}", "COD_TAB_RECURSO", "DES_TAB_RECURSO");


            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                oracle.Dispose();
            }

            return dt;

        }

        public List<TB_CLASSE_CONVENIADO> GetClasse() 
        {

            IQueryable<TB_CLASSE_CONVENIADO> query;

            query = from cc in m_Db_Context.TB_CLASSE_CONVENIADO
                    orderby cc.COD_CLASSE

                    select cc;

            return query.ToList();
        }

        public List<ListaDataVig> GetListaConvDatVig() 
        {
            IQueryable<ListaDataVig> query = from cv in m_Db_Context.SAU_TBL_CONV_PORTE_VIG
                                             from c in m_Db_Context.TB_CONVENENTE
                                             where c.COD_CONVENENTE == cv.COD_CONVENENTE
                                             orderby cv.COD_CONVENENTE, cv.DT_INI_VIG
                                             select new ListaDataVig
                        {
                          NUM_SEQ        =   cv.NUM_SEQ,     
                          COD_CONVENENTE =   cv.COD_CONVENENTE,
                          NOM_CONVENENTE =   c.NOM_CONVENENTE,
                          DT_VIG_PORTE   =   cv.DT_VIG_PORTE,
                          DT_INI_VIG     =   cv.DT_INI_VIG,
                          DT_FIM_VIG     =   cv.DT_FIM_VIG
                        };

           return query.ToList();
        }

        public List<ListaDataVig> GetConvDatVig(decimal codConv)
        {
            IQueryable<ListaDataVig> query = from cv in m_Db_Context.SAU_TBL_CONV_PORTE_VIG
                                             from c in m_Db_Context.TB_CONVENENTE
                                             where c.COD_CONVENENTE == cv.COD_CONVENENTE &&
                                             cv.COD_CONVENENTE == codConv
                                             orderby  cv.DT_INI_VIG
                                             select new ListaDataVig
                                             {  
                                                   NUM_SEQ       =  cv.NUM_SEQ,
                                                 COD_CONVENENTE  = cv.COD_CONVENENTE,
                                                 NOM_CONVENENTE  = c.NOM_CONVENENTE,
                                                 DT_VIG_PORTE    = cv.DT_VIG_PORTE,
                                                 DT_INI_VIG      = cv.DT_INI_VIG,
                                                 DT_FIM_VIG      = cv.DT_FIM_VIG
                                             };

            return query.ToList();
        }

        public List<ListaDataVigClasse> GetClasseDatVig(decimal codClasse) 
        {
            IQueryable<ListaDataVigClasse> query = from classe in m_Db_Context.SAU_TBL_CLASSE_PORT_VIG
                                                        from cc in m_Db_Context.TB_CLASSE_CONVENIADO
                                                        where cc.COD_CLASSE == classe.COD_CLASSE &&
                                                        classe.COD_CLASSE == codClasse
                                                        orderby classe.COD_CLASSE, classe.COD_TAB_RECURSO, classe.DT_INI_ATEND
                                                        select new ListaDataVigClasse
                                                       {
                                                           NUM_SEQ = classe.NUM_SEQ,
                                                           COD_CLASSE = classe.COD_CLASSE,
                                                           NOM_CLASSE = cc.DES_CLASSE.Trim(),
                                                           COD_TAB_REC = classe.COD_TAB_RECURSO,
                                                           DT_VIG_PORTE = classe.DT_VIG_PORTE,
                                                           DT_INI_VIG = classe.DT_INI_ATEND,
                                                           DT_FIM_VIG = classe.DT_FIM_ATEND,
                                                       };

            return query.ToList();
        }

        public List<ListaDataVigClasse> GetListaClasseDatVig()
        {
            IQueryable<ListaDataVigClasse> query = from classe in m_Db_Context.SAU_TBL_CLASSE_PORT_VIG
                                                        from cc in m_Db_Context.TB_CLASSE_CONVENIADO
                                                        where cc.COD_CLASSE == classe.COD_CLASSE
                                                        orderby classe.COD_CLASSE, classe.COD_TAB_RECURSO, classe.DT_INI_ATEND
                                                        select new ListaDataVigClasse
                                                        { 
                                                        NUM_SEQ     = classe.NUM_SEQ,
                                                        COD_CLASSE  = classe.COD_CLASSE,
                                                        NOM_CLASSE  = cc.DES_CLASSE.Trim(),
                                                        COD_TAB_REC = classe.COD_TAB_RECURSO,
                                                        DT_VIG_PORTE= classe.DT_VIG_PORTE,
                                                        DT_INI_VIG  = classe.DT_INI_ATEND,
                                                        DT_FIM_VIG  = classe.DT_FIM_ATEND,
                                                        };
            return query.ToList();
        }

        public List<CADTBLVPRVALORPORTEREC> GetPorte(DateTime DataVig) 
        {
            IQueryable<CADTBLVPRVALORPORTEREC> query = from c in m_Db_Context.CADTBLVPRVALORPORTEREC
                                                       where c.VPRDATVALIDADE == DataVig
                                                       orderby c.PRECODPORTEREC
                                                       select c;

            return query.ToList();
        }

        public decimal GetMaxNumSeqClasse() 
        {
           
            decimal resultado;

            try
            {
                resultado = m_Db_Context.SAU_TBL_CLASSE_PORT_VIG.Max(m => m.NUM_SEQ);
            }
            catch (InvalidOperationException ex)
            {
                resultado = 0;
            }

            return resultado;
        }

        public decimal GetMaxNumSeqConv()
        {

            decimal resultado;

            try
            {
                resultado = m_Db_Context.SAU_TBL_CONV_PORTE_VIG.Max(m => m.NUM_SEQ);
            }
            catch (InvalidOperationException ex)
            {
                resultado = 0;
            }

            return resultado;
        }

        public int VerificarVigFimConv(decimal codConv) 
        {
            IQueryable<SAU_TBL_CONV_PORTE_VIG> query;
            int i = 0;
            
            try
            {
                query = from c in m_Db_Context.SAU_TBL_CONV_PORTE_VIG
                        where c.COD_CONVENENTE == codConv &&
                        c.DT_INI_VIG == (m_Db_Context.SAU_TBL_CONV_PORTE_VIG.Where(s => s.COD_CONVENENTE == c.COD_CONVENENTE).Max(m => m.DT_INI_VIG))
                        select c;

                if (query.FirstOrDefault().DT_FIM_VIG == null) 
                {
                    i = 1;
                }

            }
            catch (NullReferenceException ex1)
            {
                i = 0;
            }
            catch (InvalidOperationException ex)
            {
                i = 1;
            }
            

            return i; 
            
        }

        public int VerificaInicioVigConv(decimal codConv, DateTime dataVig) 
        {
            IQueryable<SAU_TBL_CONV_PORTE_VIG> query = from conv in m_Db_Context.SAU_TBL_CONV_PORTE_VIG
                                                       where conv.COD_CONVENENTE == codConv && (dataVig >= conv.DT_INI_VIG && dataVig <= conv.DT_FIM_VIG)
                                                       select conv;
            return query.SelectCount();
        }

        public int VerificaInicioVigClasse(decimal codClasse, string codTabRec, DateTime dataVig)
        {
            if (codTabRec == "")
            {
                IQueryable<SAU_TBL_CLASSE_PORT_VIG> query = from classe in m_Db_Context.SAU_TBL_CLASSE_PORT_VIG
                                                            where classe.COD_CLASSE == codClasse && (dataVig >= classe.DT_INI_ATEND && dataVig <= classe.DT_FIM_ATEND)
                                                            select classe;
                return query.SelectCount();
            }
            else 
            {
                decimal codTab = Convert.ToDecimal(codTabRec);

                IQueryable<SAU_TBL_CLASSE_PORT_VIG> query = from classe in m_Db_Context.SAU_TBL_CLASSE_PORT_VIG
                                                            where 
                                                            classe.COD_CLASSE == codClasse
                                                            && classe.COD_TAB_RECURSO == codTab
                                                            && (dataVig >= classe.DT_INI_ATEND && dataVig <= classe.DT_FIM_ATEND)
                                                            select classe;
                return query.SelectCount();
            }
        }

        public int VerificaVigFimClasse(decimal codClasse, string codTabRec) 
        {
            IQueryable<SAU_TBL_CLASSE_PORT_VIG> query;
            int i = 0;
            
             try
            {
            if(codTabRec == "")
            {
                query = from cl in m_Db_Context.SAU_TBL_CLASSE_PORT_VIG
                        where
                            cl.COD_CLASSE == codClasse &&
                            cl.DT_INI_ATEND == (m_Db_Context.SAU_TBL_CLASSE_PORT_VIG.Where(w => w.COD_CLASSE == cl.COD_CLASSE).Max(m => m.DT_INI_ATEND))
                        select cl;
            }
            else
            {
                decimal codTab = Convert.ToDecimal(codTabRec);

                query = from cl in m_Db_Context.SAU_TBL_CLASSE_PORT_VIG
                        where
                            cl.COD_CLASSE == codClasse &&
                            cl.COD_TAB_RECURSO == codTab &&
                            cl.DT_INI_ATEND == (m_Db_Context.SAU_TBL_CLASSE_PORT_VIG.Where(w => w.COD_CLASSE == cl.COD_CLASSE).Max(m => m.DT_INI_ATEND))
                        select cl;
            }
           
           if (query.FirstOrDefault().DT_FIM_ATEND == null)
            {
                i = 1;
            }

            }
             catch (NullReferenceException ex1)
             {
                 i = 0;
             }
             catch (InvalidOperationException ex)
             {
                 i = 1;
             }


             return i; 
            
        }
       
        public void CadastrarConv( SAU_TBL_CONV_PORTE_VIG obj) 
        {
            try
            {
                m_Db_Context.SAU_TBL_CONV_PORTE_VIG.Add(obj);
                Savechanges();

               
            }
            catch (Exception ex)
            {
              throw new Exception ("Erro: " + ex.Message);
            }
        }

        public void CadastrarClasse(SAU_TBL_CLASSE_PORT_VIG obj)
        {
            try
            {
                m_Db_Context.SAU_TBL_CLASSE_PORT_VIG.Add(obj);
                Savechanges();


            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

        public void AtualizarConv(SAU_TBL_CONV_PORTE_VIG obj) 
        {
            var verifica = m_Db_Context.SAU_TBL_CONV_PORTE_VIG.Find(obj.NUM_SEQ, obj.COD_CONVENENTE);

            if (verifica != null) 
            {
                try
                {
                    verifica.DT_FIM_VIG = obj.DT_FIM_VIG;
                    verifica.DT_ATU = obj.DT_ATU;
                    verifica.USUARIO = obj.USUARIO;
                    Savechanges();


                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        public void AtualizarClasse(SAU_TBL_CLASSE_PORT_VIG obj)
        {

            var verifica = m_Db_Context.SAU_TBL_CLASSE_PORT_VIG.Find(obj.NUM_SEQ, obj.COD_CLASSE);

            if (verifica != null)
            {
                try
                {
                    verifica.DT_FIM_ATEND = obj.DT_FIM_ATEND;
                    verifica.DT_ATU = obj.DT_ATU;
                    verifica.USUARIO = obj.USUARIO;
                    Savechanges();


                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
            
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
   }
}
