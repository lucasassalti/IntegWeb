using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Intranet.Aplicacao.DAL;
using IntegWeb.Entidades.Previdencia.Pagamentos;
using IntegWeb.Framework;

namespace Intranet.Aplicacao.BLL

{
    public class pagamentosBLL
    {

        public Retorno_Aviso_pagto_ms_ab ConsultarQtde(out string mensagem,
                                                       string AVISO_COD_EMPRS,
                                                       string AVISO_NUM_RGTRO_EMPRG,
                                                       string AVISO_NUM_IDNTF_RPTANT,
                                                       string AVISO_NUM_IDNTF_DPDTE,
                                                       string AVISO_ANO_REFERENCIA,
                                                       string Aviso_asabono,
                                                       string Aviso_asquadro)
        {
            var retorno = new pagamentosDAL().ConsultaAbono(out mensagem,
                                                            AVISO_COD_EMPRS,
                                                            AVISO_NUM_RGTRO_EMPRG,
                                                            AVISO_NUM_IDNTF_RPTANT,
                                                            AVISO_NUM_IDNTF_DPDTE,
                                                            AVISO_ANO_REFERENCIA,
                                                            Aviso_asabono,
                                                            Aviso_asquadro);
            return retorno;
        }

        public string DataMax(string nempr, string nreg, string repres)
        {
            return new pagamentosDAL().GetDataMax(nempr, nreg, repres);
        }

        public DataTable RetornarPgtos(string nempr, string nreg, string repres)
        {
            return new pagamentosDAL().RetornarPgtos(nempr, nreg, repres);
        }

        public DataTable RetornarPgtos(string nempr, string nreg, string repres,string mesInicial,string mesFim,string anoInicial,string anoFim)
        {
            return new pagamentosDAL().RetornarPgtos(nempr, nreg, repres,mesInicial,mesFim,anoInicial,anoFim);
        }
    }
}