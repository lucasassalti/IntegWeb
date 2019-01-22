using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao
{
    public class CORPORATIVO
    {
        public short COD_EMPRS { get; set; }
        public string NOM_RZSOC_EMPRS { get; set; }
        public Nullable<int> NUM_MATR_PARTF { get; set; }
        public string NOM_EMPRG { get; set; }
    }

    public class CAPITALIZACAO
    {
        public short COD_EMPRS { get; set; }
        public short NUM_PATROC { get; set; }
        public string NOM_PATROC { get; set; }
        public Nullable<int> NUM_MATR_PARTF { get; set; }
        public string NOM_EMPRG { get; set; }
    }

    public class TrocaEmpresaDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public CORPORATIVO GetDataCorporativo(int num_matr_partf)
        {
            IQueryable<CORPORATIVO> query;

            query = from e in m_DbContext.EMPREGADO
                    from emp in m_DbContext.EMPRESA
                    where (e.COD_EMPRS == emp.COD_EMPRS)
                    && (e.DAT_DESLG_EMPRG == null)
                    && (e.NUM_MATR_PARTF == num_matr_partf)
                    select new CORPORATIVO
                    {
                        COD_EMPRS = e.COD_EMPRS,
                        NOM_RZSOC_EMPRS = emp.NOM_RZSOC_EMPRS,
                        NUM_MATR_PARTF = e.NUM_MATR_PARTF,
                        NOM_EMPRG = e.NOM_EMPRG

                    };


            return query.FirstOrDefault();
        }

        public CAPITALIZACAO GetDataCapitalizacao(int num_matr_partf)
        {
            IQueryable<CAPITALIZACAO> query;

            query = from p in m_DbContext.PARTICIPANTE_FSS
                    from patr in m_DbContext.PATROCINADORA_FSS
                    from emp in m_DbContext.EMPREGADO
                    where (p.COD_EMPRS == patr.COD_EMPRS)
                    && (p.NUM_PATROC == patr.NUM_PATROC)
                    && (p.NUM_MATR_PARTF == emp.NUM_MATR_PARTF)
                    && (p.NUM_MATR_PARTF == num_matr_partf)
                    select new CAPITALIZACAO
                    {
                        COD_EMPRS = p.COD_EMPRS,
                        NUM_PATROC = p.NUM_PATROC,
                        NOM_PATROC = patr.NOM_PATROC,
                        NUM_MATR_PARTF = p.NUM_MATR_PARTF,
                        NOM_EMPRG = emp.NOM_EMPRG
                    };



            return query.FirstOrDefault();

        }

        public Entidades.Resultado Update(int num_matr_partf,short cod_emprs, short num_patroc)
        {
            Entidades.Resultado res = new Entidades.Resultado();

            try
            {
                var atualiza = m_DbContext.PARTICIPANTE_FSS.FirstOrDefault(p =>p.NUM_MATR_PARTF == num_matr_partf );

                if (atualiza != null)
                {
                    if (atualiza.COD_EMPRS == cod_emprs)
                    {
                        res.Erro("Não foi possível realizar a troca: Empresas iguais");
                        return res;
                    }

                    atualiza.COD_EMPRS = cod_emprs;
                    atualiza.NUM_PATROC = num_patroc;
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

    }
}
