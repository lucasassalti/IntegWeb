using IntegWeb.Saude.Aplicacao.DAL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Processos
{
    public class RegistrosOficiaisBLL : RegistrosOficiaisDAL
    {
        public List<FC_SAU_TBL_RO_GERACAO_VIEW> GetDados()
        {
            return base.GetDados();
        }

        public List<FC_SAU_TBL_RO_GERACAO_VIEW> GetRel(decimal mes, decimal ano)
        {
            return base.GetRel(mes, ano);
        }

        public void ProcessarRO(int mes, int ano)
        {
            base.ProcessarRO(mes, ano);
        }

        public string GetStatus()
        {
            return base.GetStatus();
        }

        public void ProcessarRel(int idRel)
        {
            base.ProcessarRel(idRel);
        }

     

        }
    }

