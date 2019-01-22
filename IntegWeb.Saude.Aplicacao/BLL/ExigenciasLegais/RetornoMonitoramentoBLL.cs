using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Entidades.Saude.ExigenciasLegais.MonitoramentoTISS;
using IntegWeb.Saude.Aplicacao.DAL.ExigenciasLegais;
using IntegWeb.Entidades;
using System.Xml.Linq;

namespace IntegWeb.Saude.Aplicacao.BLL.ExigenciasLegais
{
    public class RetornoMonitoramentoBLL
    {
        #region Métodos
        public Resultado Inserir(mensagemEnvioANS msgenvio)
        {
            return new RetornoMonitoramentoMensagemEnvioANSDAL().Inserir(msgenvio);
        }

        //public Resultado Inserir(cabecalhoTransacao cabecalho)
        //{
        //    Resultado resultado = new RetornoMonitoramentoCabecalhoDAL().Inserir(cabecalho);
        //    if (resultado.Ok)
        //    {
        //        resultado = Inserir(cabecalho.identificacaoTransacao);
        //    }
        //    return resultado;
        //}

        //public Resultado Inserir(cabecalhoTransacaoIdentificacaoTransacao identificacaoTransacao)
        //{
        //    return new RetornoMonitoramentoIdentificacaoTransacaoDAL().Inserir(identificacaoTransacao);
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagem mensagem)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagemAnsParaOperadora ansParaOperadora)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento resumoProcessamento)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados registrosRejeitados)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(ct_monitoramentoGuiaDadosContratadoExecutante contratadoExecutante)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosGuia errosGuia)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuia errosItensGuia)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(ct_monitoramentoGuiaProcedimentosIdentProcedimento identProcedimento)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(ct_monitoramentoGuiaProcedimentosIdentProcedimentoProcedimento procedimento)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuiaRelacaoErros relacaoErros)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoResumoProcessamentoTotais resumoProcessamentoTotais)
        //{
        //    throw new NotImplementedException();
        //}

        //public Resultado Inserir(epilogo epilogo)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
