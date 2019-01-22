using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Saude.Aplicacao.DAL.Cobranca;
using IntegWeb.Saude.Aplicacao.DAL.Saude;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntegWeb.Saude.Aplicacao.BLL.Cobranca
{
    public class AcertoPontualCoparticipacaoBLL
    {
        //Select das informações de acerto pontual

        //AcertoPontualCoparticipacao listParamAcerto = new AcertoPontualCoparticipacao();

        public DataTable ListarAcertoPontualCoparticipacao(AcertoPontualCoparticipacao SelAtualizarAcertoCoparticipacao)
        {
            return new AcertoPontualCoparticipacaoDAL().ListarExtratoCoparticipacao(SelAtualizarAcertoCoparticipacao);

        }


        public Boolean AtualizarAcertoCoparticipacao(AcertoPontualCoparticipacao updAtualizarAcertoCoparticipacao, AcertoPontualCoparticipacao objAcertoAlteracao)
        {
            return new AcertoPontualCoparticipacaoDAL().atualizarExtratoCoparticipacao(updAtualizarAcertoCoparticipacao, objAcertoAlteracao);
        }



        public Boolean InserirLogAcertoCoparticipacao(AcertoPontualCoparticipacao InsLogAtualizarAcertoCoparticipacao, DateTime dataAlteracao, String NomRespAlt, AcertoPontualCoparticipacao objAcertoAlteracao)
        {   
            return new AcertoPontualCoparticipacaoDAL().gravarLogExtratoCoparticipacao(InsLogAtualizarAcertoCoparticipacao, dataAlteracao, NomRespAlt, objAcertoAlteracao);
        }
        
    }
}
