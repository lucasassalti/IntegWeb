using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.DAL.Faturamento;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Faturamento
{
  public  class CargaMatMedPrestadoresBLL : CargaMatMedPrestadoresDAL
    {
        public List<TB_CONVENENTE_VIEW> GetConvenente()
        {
            return base.GetConvenente();
        }

        public List<TB_TAB_RECURSO_VIEW> GetTabMatMed(decimal cod_convenente)
        {
            return base.GetTabMatMed(cod_convenente);
        }

        public void ImportaDadosTemp(DataTable dt) 
        {
             base.ImportaDadosTemp(dt);
        }

        public DataTable VerificaProcNaoCadastradosIncor() 
        {
          return base.VerificaProcNaoCadastradosIncor();
        }

        public List<SAU_TBL_CARGA_TEMP> VerificaProcNaoCadastradosAlbert() 
        {
            return base.VerificaProcNaoCadastradosAlbert();
        }

        public void CadastrarCarga(decimal codConv, out int totalCad, out int totalAtu, out int qtdEspc, out int reCobertura, decimal cod_tab_recurso, System.DateTime datVigencia, decimal codTipCarga) 
        {

            base.CadastrarCarga(codConv, out  totalCad, out totalAtu, out qtdEspc , out reCobertura, cod_tab_recurso, datVigencia,codTipCarga);
        }

        public decimal getMaxCountCarga() 
        {
            return base.getMaxCountCarga();
        }
        
       public void InserirCargaRealizada(int qtdeProc,SAU_TBL_CARGA_REALIZADA tabela)
       {
           base.InserirCargaRealizada(qtdeProc,tabela);
       }

       public void InserirCargaProcedimento(DataTable dt)
       {
           base.InserirCargaProcedimento(dt);
       }

       public List<SAU_TBL_CARGA_PROCEDIMENTO> CarregarGridProc(decimal codCarga) 
       {
           return base.CarregarGridProc(codCarga);
       }

       public List<SAU_TBL_CARGA_REALIZADA> CarregarGridCarga(decimal codConv)
       {
           return base.CarregarGridCarga(codConv);
       }

       public void DeleteMatMed() 
       {
           base.DeleteMatMed();
       }


       public List<SAU_TBL_CARGA_TEMP> VerificaProcCadastradosAlbert(decimal codTabRec, DateTime datVig) 
       {
           return base.VerificaProcCadastradosAlbert(codTabRec, datVig);
       }

       public DataTable VerificaProcCadastradosIncor(decimal codTabRec, DateTime datVig) 
       {
          return base.VerificaProcCadastradosIncor(codTabRec, datVig);
       }

       public int GetProcNaoIncluido(decimal cod_carga) 
       {
           return base.GetProcNaoIncluido(cod_carga);
       }

       public void Savechanges() 
       {
            base.Savechanges();
       }

       public List<SAU_TBL_CARGA_TEMP> GetProcTemp() 
       {
           return base.GetProcTemp();
       }
  }
}
