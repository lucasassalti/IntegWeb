using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data.Objects;
using System.Data;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class ConvenenteDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public List<TB_CONVENENTE> Listar()
        {
            return GetWhere().ToList();
        }

        public TB_CONVENENTE Consultar(decimal COD_CONVENENTE)
        {
            return GetWhere(COD_CONVENENTE).ToList().FirstOrDefault();
        }


        public IQueryable<TB_CONVENENTE> GetWhere(decimal? COD_CONVENENTE = null)
        {

            IQueryable<TB_CONVENENTE> query;

            decimal dCOD_CONVENENTE = 0;
            decimal.TryParse(COD_CONVENENTE.ToString(), out dCOD_CONVENENTE);

            query = from c in m_DbContext.TB_CONVENENTE
                    where (c.COD_CONVENENTE == dCOD_CONVENENTE && COD_CONVENENTE != null || COD_CONVENENTE == null)
                    select c;

            return query;

        }

    }
}
