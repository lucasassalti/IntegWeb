using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Concessao
{

    public class CAD_DADOS_BANC
    {
        public short COD_EMPRS { get; set; }
        public int NUM_RGTRO_EMPRG { get; set; }
        public Nullable<short> COD_BANCO { get; set; }
        public Nullable<int> COD_AGENCIA { get; set; }
        public string TP_CONTA { get; set; }
        public string NUM_CONTA { get; set; }
        public string NOM_EMPRG { get; set; }
    }

    public class CAD_DADOS_MOV_CAD
    {
        public short COD_EMPRS { get; set; }
        public int NUM_RGTRO_EMPRG { get; set; }
        public string NOM_EMPRG { get; set; }
        public string CDEDESCOMPEMPRG { get; set; }
        public string DESC_TABELA { get; set; }
        public string DESC_CAMPO { get; set; }
        public string DESC_CONTEUDO { get; set; }
        public System.DateTime DTH_ATUALIZACAO { get; set; }
    }

    public class BateCadastralCargaDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<CAD_DADOS_BANC> GetDataBanc(short codEmprs, int numRgtroEmprg)
        {
            IQueryable<CAD_DADOS_BANC> query = (from b in m_DbContext.FC_CAD_TBL_MOV_DADOS_BANC
                                                from e in m_DbContext.EMPREGADO
                                                where (b.COD_EMPRS == e.COD_EMPRS)
                                                   && (b.NUM_RGTRO_EMPRG == e.NUM_RGTRO_EMPRG)
                                                   && (b.COD_EMPRS == codEmprs)
                                                   && (b.NUM_RGTRO_EMPRG == numRgtroEmprg)
                                                   && (e.DAT_DESLG_EMPRG == null)
                                                   && (b.DTH_ATUALIZACAO == (m_DbContext.FC_CAD_TBL_MOV_DADOS_BANC.Where(b1 => b1.COD_EMPRS == b.COD_EMPRS
                                                                                                                         && b1.NUM_RGTRO_EMPRG == b.NUM_RGTRO_EMPRG
                                                                                                                        ).Max(b2 => b2.DTH_ATUALIZACAO)))
                                                select new CAD_DADOS_BANC
                                                           {
                                                               COD_EMPRS = b.COD_EMPRS,
                                                               NUM_RGTRO_EMPRG = b.NUM_RGTRO_EMPRG,
                                                               NOM_EMPRG = e.NOM_EMPRG,
                                                               COD_BANCO = b.COD_BANCO,
                                                               COD_AGENCIA = b.COD_AGENCIA,
                                                               NUM_CONTA = b.NUM_CONTA,
                                                               TP_CONTA = b.TP_CONTA
                                                           }
                       );
            return query.ToList();

        }

        public List<CAD_DADOS_MOV_CAD> GetDataCad(short codEmprs, int numRgtroEmprg)
        {

            IQueryable<CAD_DADOS_MOV_CAD> query = (from c in m_DbContext.FC_CAD_TBL_MOV_DADOS_CAD
                                                   from e in m_DbContext.EMPREGADO
                                                   from x in m_DbContext.SCRTBLCDECOMPEMPREGADO
                                                   where (c.COD_EMPRS == e.COD_EMPRS)
                                                   && (c.NUM_RGTRO_EMPRG == e.NUM_RGTRO_EMPRG)
                                                   && (c.DESC_CAMPO == x.CDEDESCOLCOMPEMPRG.ToUpper())
                                                   && (e.DAT_DESLG_EMPRG == null)
                                                   && (c.COD_EMPRS == codEmprs)
                                                   && (c.NUM_RGTRO_EMPRG == numRgtroEmprg)
                                                   && (c.DTH_ATUALIZACAO == (m_DbContext.FC_CAD_TBL_MOV_DADOS_CAD.Where(c1 => c1.COD_EMPRS == c.COD_EMPRS
                                                                                                                         && c1.NUM_RGTRO_EMPRG == c.NUM_RGTRO_EMPRG
                                                                                                                         && c1.DESC_CAMPO == c.DESC_CAMPO
                                                                                                                        ).Max(c2 => c2.DTH_ATUALIZACAO)))
                                                   select new CAD_DADOS_MOV_CAD
                                                     {
                                                         COD_EMPRS = c.COD_EMPRS,
                                                         NUM_RGTRO_EMPRG = c.NUM_RGTRO_EMPRG,
                                                         NOM_EMPRG = e.NOM_EMPRG,
                                                         CDEDESCOMPEMPRG = x.CDEDESCOMPEMPRG,
                                                         DESC_TABELA = c.DESC_TABELA,
                                                         DESC_CAMPO = c.DESC_CAMPO,
                                                         DESC_CONTEUDO = c.DESC_CONTEUDO,
                                                         // DTH_ATUALIZACAO = c.DTH_ATUALIZACAO
                                                     });

            return query.ToList();
        }
    }

}
