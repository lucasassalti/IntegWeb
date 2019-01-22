using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.DAL.Tesouraria;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL.Tesouraria
{
    public class TravaDataCorteBLL : TravaDataCorteDAL
    {

        public FC_TBL_PARAMETRO GetParameter()
        {
            return new TravaDataCorteDAL().GetParameter();
        }


        public Resultado Update(string valParametro)
        {
            return new TravaDataCorteDAL().Update(valParametro);
        }
    }
}
