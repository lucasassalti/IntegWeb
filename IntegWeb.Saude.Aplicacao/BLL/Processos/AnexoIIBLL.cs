using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IntegWeb.Saude.Aplicacao.DAL.Processos;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System.IO;

namespace IntegWeb.Saude.Aplicacao.BLL.Processos
{
    public class AnexoIIBLL : AnexoIIDAL
    {

        public SAU_TB_SERV_X_HOSP_AND changeListaDeServicos(String codigoHospital, String codigoServico)
        {
            AnexoIIBLL anexoBLL = new AnexoIIBLL();
            SAU_TB_SERV_X_HOSP_AND serv = anexoBLL.CarregarServicosxHospital(codigoHospital, codigoServico).FirstOrDefault();

            return serv;
        }

        public List<CargaServicos> retornaEmpresaServico(string numConvenente)
        {
            List<CargaServicos> servicos = CargaServicosPorHospital(numConvenente).ToList();

            List<CargaServicos> servicosHosp = new List<CargaServicos>();
            foreach (var item in servicos.ToList())
            {
                servicosHosp.Add(new CargaServicos
                {
                    descServico = item.codServico + "      -      " + item.descServico,
                    codServico = Convert.ToInt32(item.codServico),
                    valor = item.valor,
                    vProposto = item.vProposto,
                    pProposta = item.pProposta,
                    dtReajusteProp = item.dtReajusteProp
                });
            }

            return servicosHosp;
        }

        public void TrataExportacaoScan(List<string> lstCodHospitais, SAU_TB_LOG_AND log)
        {

            List<ExportaArquivoScan> arquivoScan = new List<ExportaArquivoScan>();
            List<SAU_TB_SERV_X_HOSP_AND> listaServHosp = new List<SAU_TB_SERV_X_HOSP_AND>();

            // Sobe para a memória as informações dos prestadores relevantes
            if (lstCodHospitais.Count > 0)
            {
                String contratos = null;
                foreach (var item in lstCodHospitais)
                {
                    contratos += item.ToString() + ",";
                }

                //Carrega a lista de Serviços do Anexo2
                listaServHosp = CarregaServicoScan(contratos.Remove(contratos.Length - 1));
                //Carrega informações que devem ser importadas ao SCAN
                arquivoScan = ExportaArquivoScan(contratos.Remove(contratos.Length - 1));
            }
            // Preenche o objeto "Arquivo Scan" que representa o objeto que será enviado ao scan
            foreach (var arqScan in arquivoScan)
            {
                TB_VAL_RECURSO tValRecurso = new TB_VAL_RECURSO
                {
                    COD_RECURSO = arqScan.Cod_Recurso,
                    RCOSEQ = arqScan.RCOSEQ,
                    COD_TAB_RECURSO = arqScan.Cod_Tab_Servicos,
                    RCOCODPROCEDIMENTO = arqScan.RCOCODPROCEDIMENTO,
                    VAL_RECURSO = (Decimal?)listaServHosp.Find(x => x.COD_HOSP == arqScan.CodHosp && arqScan.RCOCODPROCEDIMENTO == x.COD_SERV.ToString()).VALOR,
                    DAT_VAL_RECURSO = (DateTime)listaServHosp.Find(x => x.COD_HOSP == arqScan.CodHosp).DAT_INI_VIGENCIA,
                };

                AtualizaTb_Val_Recurso(tValRecurso, arqScan, log);
            }

        }

        public Boolean GeraLog(SAU_TB_SERV_X_HOSP_AND servicoAtual, SAU_TB_SERV_X_HOSP_AND ServicoAtualizado, SAU_TB_LOG_AND recurso, String Metodo)
        {
            // Caso a variável seja nulo, faço a instancia da mesma para não null exception.
            if (recurso == null)
            {
                recurso = new SAU_TB_LOG_AND();
            }
            if (servicoAtual == null)
            {
                servicoAtual = new SAU_TB_SERV_X_HOSP_AND();
            }
            if (ServicoAtualizado == null)
            {
                ServicoAtualizado = new SAU_TB_SERV_X_HOSP_AND();
            }

            //var usuario = (ConectaAD)Session["objUser"];

            SAU_TB_LOG_AND log = new SAU_TB_LOG_AND();
            log = new SAU_TB_LOG_AND
            {

                USUARIO = recurso.USUARIO,
                DATAALTERACAO = DateTime.Now,
                COD_HOSP = (servicoAtual.COD_SERV != 0 && servicoAtual.COD_HOSP != 0) ? servicoAtual.COD_HOSP : recurso.COD_HOSP,
                COD_SERV = (servicoAtual.COD_SERV != 0 && servicoAtual.COD_HOSP != 0) ? servicoAtual.COD_SERV : recurso.COD_SERV,
               /* ADTREAJUSTEATUAL = servicoAtual.ADTREAJUSTE,
                ADTREAJUSTEANTERIOR = ServicoAtualizado.ADTREAJUSTE,
                PDTREAJUSTEATUAL = servicoAtual.PDTREAJUSTE,
                PDTREAJUSTEANTERIOR = ServicoAtualizado.PDTREAJUSTE,
                DTREAJUSTEATUAL = servicoAtual.DTREAJUSTE,
                DTREAJUSTEANTERIOR = ServicoAtualizado.DTREAJUSTE,
                DTREAJUSTEPROPATUAL = servicoAtual.DTREAJUSTEPROP,
                DTREAJUSTEPROPANTERIOR = ServicoAtualizado.DTREAJUSTEPROP,
                AVALORATUAL = servicoAtual.AVALOR,
                AVALORANTERIOR = ServicoAtualizado.AVALOR,
                PVALORATUAL = servicoAtual.PVALOR,
                PVALORANTERIOR = ServicoAtualizado.PVALOR,
                VALORATUAL = servicoAtual.VALOR,
                VALORANTERIOR = ServicoAtualizado.VALOR,
                APORCENTAGEMATUAL = servicoAtual.APORCENTAGEM,
                APORCENTAGEMANTERIOR = ServicoAtualizado.APORCENTAGEM,
                PPORCENTAGEMATUAL = servicoAtual.PPORCENTAGEM,
                PPORCENTAGEMANTERIOR = ServicoAtualizado.PPORCENTAGEM,
                PORCENTAGEMATUAL = servicoAtual.PORCENTAGEM,
                PORCENTAGEMANTERIOR = ServicoAtualizado.PORCENTAGEM,
                VPROPOSTOANTERIOR = ServicoAtualizado.VPROPOSTO,
                VPROPOSTOATUAL = servicoAtual.VPROPOSTO,
                DESCONTOANTERIOR = ServicoAtualizado.DESCONTO,
                DESCONTOATUAL = servicoAtual.DESCONTO,
                PPROPOSTAANTERIOR = ServicoAtualizado.PPROPOSTA,
                PPROPOSTAATUAL = servicoAtual.PPROPOSTA,*/
                VAL_RECURSO_TB_VAL_RECURSO = recurso.VAL_RECURSO_TB_VAL_RECURSO,
                COD_TAB_SERV_TB_VAL_RECURSO = recurso.COD_TAB_SERV_TB_VAL_RECURSO,
                DT_REAJUSTE_TB_VAL_RECURSO = recurso.DT_REAJUSTE_TB_VAL_RECURSO,
                COD_RECURSO_TB_VAL_RECURSO = recurso.COD_RECURSO_TB_VAL_RECURSO,

                OPERACAO = Metodo
            };

            return incluirLog(log);

        }

        public List<SAU_TB_LOG_AND> returnLog(DateTime inicio, DateTime fim, Decimal prestador, Decimal Servico)
        {

            if (inicio == DateTime.MinValue || fim == DateTime.MinValue)
            {
                inicio = DateTime.MinValue;
                fim = DateTime.MinValue;
            }
            else
            {
                inicio = inicio.Add(new TimeSpan(0, 0, 0));
                fim = fim.Date.Add(new TimeSpan(23, 59, 0));
            }

            return retornaLog(inicio, fim, prestador);
        }

        public String ConfirmacaoAumento(string CodigoHospital)
        {
            var datas = RetornarDataMaior(CodigoHospital);

            String mensagem = @"Deseja atualiza data de Vigencia do Anexo II, de <b>"
                               + Convert.ToDateTime(datas.Item2).ToString("dd/MM/yyyy") +
                               "</b> para <b>" + Convert.ToDateTime(datas.Item1).ToString("dd/MM/yyyy") + "</b>  ?";
            return mensagem;

        }

    }
}
