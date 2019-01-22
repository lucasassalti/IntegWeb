using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL
{
    public class EmpregadoDAL
    {

        public EntitiesConn m_DbContext = new EntitiesConn();

        public List<EMPREGADO> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pCpf, pNome)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<EMPREGADO> GetWhere(short? pEmpresa, int? pMatricula, long? pCpf, string pNome)
        {
            IQueryable<EMPREGADO> query;
            query = from e in m_DbContext.EMPREGADO
                    where (e.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (e.NUM_CPF_EMPRG == pCpf || pCpf == null)
                       && (e.NOM_EMPRG.ToLower().Contains(pNome.ToLower()) || pNome == null)
                    select e;
            
            return query;
        }

        public List<EMPREGADO> GetList(short? pEmpresa, int? pMatricula, long? pCpf, string pNome)
        {
            return GetWhere(pEmpresa, pMatricula, pCpf, pNome).ToList();
        }

        public int GetDataCount(short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome)
        {
            return GetWhere(pEmpresa, pMatricula, pCpf, pNome).SelectCount();
        }

        public EMPREGADO GetEmpregado(short? pEmpresa, int? pMatricula, long pCpf, string pNome)
        {

            var Emp = GetWhere(pEmpresa, pMatricula, pCpf, pNome).ToList()                     
                        .Select(s => { s.DAT_DESLG_EMPRG = (s.DAT_DESLG_EMPRG ?? DateTime.Now); return s; })
                        .OrderByDescending(s => s.DAT_DESLG_EMPRG); //Sempre o mais atual

            return Emp.FirstOrDefault();
        }

        public REPRES_UNIAO_FSS GetRepresentante(short? pEmpresa, int? pMatricula, long pCpf, string pNome)
        {
            IQueryable<REPRES_UNIAO_FSS> query;
            query = from e in m_DbContext.REPRES_UNIAO_FSS
                    where (e.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (e.NUM_CPF_REPRES == pCpf || pCpf == null)
                       && (e.NOM_REPRES.ToLower().Contains(pNome.ToLower()) || pNome == null)
                    orderby e.NUM_IDNTF_RPTANT descending //Sempre o mais atual
                    select e;

            return query.FirstOrDefault(); 
        }

    }
}
