using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.DAL;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class AtendimentosBLL : AtendimentosDAL
    {

        public List<QtdAtendimentos> GetQtdAtendimentos(int startRowIndex, int maximumRows, string paramCodEmpresa, string paramNumMatricula, string paramNumSubMatricula, string paramDtIni, string paramDtFim, string paramNumProcedimento, string sortParameter, string paramTipoPesquisa)
        {
            var qtdAtendimento = new List<QtdAtendimentos>();
            if (paramTipoPesquisa == "0")
            {
                qtdAtendimento = base.GetData(startRowIndex, maximumRows, paramCodEmpresa, paramNumMatricula, paramNumSubMatricula, paramDtIni, paramDtFim, paramNumProcedimento, sortParameter);
            }
            else
            {
                qtdAtendimento = base.GetDataDetalhado(startRowIndex, maximumRows, paramCodEmpresa, paramNumMatricula, paramNumSubMatricula, paramDtIni, paramDtFim, paramNumProcedimento, sortParameter);
            }
            return qtdAtendimento;
            
        }

        public int SelectCount(string paramCodEmpresa, string paramNumMatricula, string paramNumSubMatricula, string paramDtIni, string paramDtFim, string paramNumProcedimento, string paramTipoPesquisa)
        {
            return base.GetDataCount();
        }

    }
}
   