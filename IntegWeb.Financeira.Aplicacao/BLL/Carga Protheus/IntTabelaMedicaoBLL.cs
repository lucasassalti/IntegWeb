using System;
using System.Linq;
using System.Text;
using IntegWeb.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Financeira.Aplicacao.DAL.Carga_Protheus;


namespace IntegWeb.Financeira.Aplicacao.BLL.Carga_Protheus
{
    public class IntTabelaMedicaoBLL : IntTabelaMedicaoDAL
    {      
        public Entidades.Resultado SalverMedicao(MEDCTR md)
        {
            Entidades.Resultado res = new Entidades.Resultado();

            // Caso posteriormente existam tratamentos no objeto, fazer na bll.
            var medctrDal = new IntTabelaMedicaoDAL();
            res = medctrDal.GravarMedicao(md);
            
            return res;
        }

        public PessFisicaJuridica BuscarInformacaoBancaria(string cdBanco, string cdTipoConta, string cdContaCorrente)
        {
            var medctrDal = new IntTabelaMedicaoDAL();
            return medctrDal.TratarDadosBancarios(cdBanco, cdTipoConta, cdContaCorrente);            
        }    
    }
}
