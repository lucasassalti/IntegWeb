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
    public class GrupoAdmPatrocinadoraDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public class PRE_VW_FCESP_GRUPO_EMP
        {
            public string GRUPO { get; set; }
            public short EMPRESA { get; set; }
            public short COD_EMPRS { get; set; }

        }


        public List<PRE_VW_FCESP_GRUPO_EMP> GetGrupo()
        {
            IEnumerable<PRE_VW_FCESP_GRUPO_EMP> IEnum = m_DbContext.Database.SqlQuery<PRE_VW_FCESP_GRUPO_EMP>("select distinct GRUPO from OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD order by GRUPO desc ");

            List<PRE_VW_FCESP_GRUPO_EMP> list = IEnum.ToList();

            return list;

        }

        public List<PRE_VW_FCESP_GRUPO_EMP> GetEmpresa(string pGrupo)
        {
            IEnumerable<PRE_VW_FCESP_GRUPO_EMP> IEnum = m_DbContext.Database.SqlQuery<PRE_VW_FCESP_GRUPO_EMP>("select EMPRESA from OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD " +
                                                                                                               "where upper(GRUPO) = " + "'" + pGrupo.ToUpper() + "'");
            List<PRE_VW_FCESP_GRUPO_EMP> list = IEnum.ToList();

            return list;
        }

        public List<PRE_VW_FCESP_GRUPO_EMP> GetEmpresaGeral()
        {
            IEnumerable<PRE_VW_FCESP_GRUPO_EMP> IEnum = m_DbContext.Database.SqlQuery<PRE_VW_FCESP_GRUPO_EMP>("select COD_EMPRS from OWN_PORTAL.EMPRESA@PPORTAL.WORLD");
            List<PRE_VW_FCESP_GRUPO_EMP> list = IEnum.ToList();

            return list;
        }

        public List<PRE_VW_FCESP_GRUPO_EMP> Search(string pGrupo, short? pEmpresa)
        {
            IEnumerable<PRE_VW_FCESP_GRUPO_EMP> IEnum = m_DbContext.Database.SqlQuery<PRE_VW_FCESP_GRUPO_EMP>("select distinct GRUPO from OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD where ((upper(GRUPO) like '%" + pGrupo.ToUpper() + "%') or ('" + pGrupo.ToUpper() + "' is null)) and ((EMPRESA = '" + pEmpresa + "') or ('" + pEmpresa + "' is null)) order by GRUPO desc", 0);
            List<PRE_VW_FCESP_GRUPO_EMP> list = IEnum.ToList();

            return list;
        }

        public Resultado Insert(string pGrupo, short pEmpresa)
        {
            Resultado res = new Resultado();

            try
            {
                int insere = m_DbContext.Database.ExecuteSqlCommand("insert into OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD (GRUPO,EMPRESA) values (" + " '" + pGrupo + "' " + " , " + pEmpresa + " )");

                if (insere != 0)
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

        public Resultado Delete(string pGrupo, short pEmpresa)
        {
            Resultado res = new Resultado();

            try
            {
                int delete = m_DbContext.Database.ExecuteSqlCommand("DELETE OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD WHERE GRUPO = " + "'" + pGrupo + "' " + "AND EMPRESA = " + pEmpresa);

                if (delete != 0)
                {
                        res.Sucesso("Registro deletado com sucesso.");
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
