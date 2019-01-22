using IntegWeb.Entidades.Relatorio;
using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class AcaoJudicialBLL : AcaoJudicialDAL
    {
        StringBuilder str = new StringBuilder();

        public bool ProcessaSrcContribuicoes(out string mensagem, out int id)
        {

            id = 0;
            if (string.IsNullOrEmpty(flag_abn))
            {
                str.Append("Escolha a opção de Abono!\\n");
            }

            if (string.IsNullOrEmpty(flag_acao_Jud))
            {
                str.Append("Escolha a opção de Polo da Ação Judicional!\\n");
            }


            if (cod_tiplto == 0)
            {
                str.Append("Escolha um tipo de pleito\\n");
            }

            if (string.IsNullOrEmpty(cod_vara))
            {
                str.Append("Preencha o código da Vara\\n");
            }


            if (num_matr_partf == null)
            {
                str.Append("Digite uma matrícula válida.\\n");
            }

            //if (string.IsNullOrEmpty(obs_src))
            //{
            //    str.Append("Digite a Observação\\n");
            //}

            if (string.IsNullOrEmpty(num_pasta))
            {
                str.Append("Escolha o número da pasata!\\n");
            }

            if (string.IsNullOrEmpty(num_processo))
            {
                str.Append("Escolha o número do processo!\\n");
            }

            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return ProcessaSrc(out mensagem, out id);

        }

        public bool ProcessaCtrContribuicoes(out string mensagem)
        {


            if (num_matr_partf == null)
            {
                str.Append("num_matr_partf inválido.\\n");
            }

            if (num_seq_prc == null)
            {
                str.Append("num_seq_prc inválido.\\n");
            }


            if (data_atualiz == null)
            {
                str.Append("Digite uma data válida.\\n");

            }

            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return ProcessaCtr(out mensagem);

        }

        public bool DeletarSrcContribuicoes(out string mensagem)
        {
            bool ret = DeletaSrc();

            if (ret)
                mensagem = "Deletado com sucesso";
            else
                mensagem = "Não é possível deletar, pois o processo foi enviado para o VR.";

            return ret;
        }

        public bool ProcessoDadosSemPleito(out string mensagem)
        {

            return ProcessaDadosSPleito(out mensagem);
        }

        public bool ProcessaDadosComPleito(out string mensagem)
        {

            return ProcessaDadosCPleito(out mensagem);
        }

        public decimal RetornaSrb(int matr, int num_seq)
        {
            return RetornaValorSrb(matr, num_seq);
        }

        public bool ProcessaSalReal(out string mensagem, bool ismsg)
        {
            if (id_acao_processo == 0)
            {

                str.Append("Escolha uma opção de Tipo de Benefício!\\n");
            }

            if (ismsg)
            {
                if (string.IsNullOrEmpty(desc_mensagem))
                {
                    str.Append("Digite uma mensagem válida!\\n");
                }
            }
            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return ProcessaSalarioReal(out  mensagem);
        }

        public bool ProcessaPar(out string mensagem)
        {
            //if (string.IsNullOrEmpty(nome_benficiario))
            //{
            //    str.Append("Informe o nome do beneficiário!\\n");
            //}

            if (id_acao_processo == 0)
            {

                str.Append("Escolha uma opção de Tipo de Benefício!\\n");
            }


            if (num_matr_partf == null)
            {
                str.Append("num_matr_partf inválido.\\n");
            }

            if (num_seq_prc == null)
            {
                str.Append("num_seq_prc inválido.\\n");
            }

            if (vl_inicial == null && mrc_cad_bnf == "S")
            {
                str.Append("Informe um Valor Inicial válido.\\n");
            }

            if (vl_compleito == null && mrc_cad_rsv_cplto == "S")
            {
                str.Append("Informe uma Reserva com Pleito válido.\\n");
            }

            if (vl_sempleito == null && mrc_cad_rsv_splto == "S")
            {
                str.Append("Informe uma Reserva sem Pleito válido.\\n");
            }

            if (dt_ajuizamento  == null)
            {
                str.Append("Informe a Data de Prescrição.\\n");
            }

            if (dt_ini_pgto == null)
            {
                str.Append("Informe a Data Início de Pagamento.\\n");
            }

            if (dt_fin_pgto == null)
            {
                str.Append("Informe a Data Fim de Pagamento.\\n");
            }



            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return ProcessaParametro(out mensagem);
        }

        public bool ProcessaCalculoReal(out string mensagem)
        {
            if (id_acao_processo == 0)
            {

                str.Append("Escolha uma opção de Tipo de Benefício!\\n");
            }

            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return ProcessaCalcReal(out  mensagem);
        }

        public bool ProcessaProvisionamentoIR(int? anoRef){
            return base.ProcessaProvisionamentoIR(anoRef);
        }

        public bool ImportacaoVr(out string mensagem)
        {
            return ImportaVr(out mensagem);
        }

        public bool DeletarVr(out string mensagem)
        {
            return DesfazerImportacaoVr(out mensagem);
        }

        public bool VerificaVrDados()
        {
            return ValidaVr();
        }

        public DataTable CarregaParticipante()
        {
            return ListarParticipante();
        }

        public DataTable CarregaProcesso()
        {
            return ListarProcesso();
        }

        public DataTable ListarProcessosVr(int filType, string filValue, string sortParameter)
        {
            return base.ListarProcessosVr(filType, filValue).Select("", sortParameter).CopyToDataTable();
        }

        public int SelectCount(int filType, string filValue)
        {
            return base.ListarProcessosVr(filType, filValue).Rows.Count;
        }

        public DataTable CarregaProcessoVr()
        {
            return base.CarregaProcessoVr();
        }

        public DataTable CarregaTipoPleito()
        {
            return ListarTipoPleito();
        }

        public DataTable CarregaTipoAtualizacao()
        {
            return ListarTipoAtualizacao();
        }

        public DataTable CarregaAssunto()
        {
            return ListarAssunto();
        }

        public DataTable CarregaHistorico()
        {
            return ListarHistorico();
        }

        public DataSet CarregarRelatoriosDisp()
        {
            return ListarRelatoriosDisp();
        }

        public List<Relatorio> CarregaRelatorio()
        {

            DataSet dts = ListarRelatorio();
            List<Relatorio> list = ConverteListaRelatorio(dts);
            return list;
        }

        public List<Relatorio> CarregaRelatorioParam(string ids)
        {

            DataSet dts = ListarRelatorioParametro(ids);
            List<Relatorio> list = ConverteListaRelatorio(dts);
            return list;
        }

        private List<Relatorio> ConverteListaRelatorio(DataSet dts)
        {

            Relatorio rel; Parametro param;
            List<Relatorio> list = new List<Relatorio>();

            if (!string.IsNullOrEmpty(dts.Tables[0].Rows[0]["arquivo"].ToString()) ||
                !string.IsNullOrEmpty(dts.Tables[0].Rows[0]["id_relatorio"].ToString()) ||
                !string.IsNullOrEmpty(dts.Tables[0].Rows[0]["relatorio"].ToString()) ||
                !string.IsNullOrEmpty(dts.Tables[0].Rows[0]["titulo"].ToString()) ||
                !string.IsNullOrEmpty(dts.Tables[1].Rows[0]["parametro"].ToString())
                )
            {


                foreach (DataRow row in dts.Tables[0].Rows)
                {

                    rel = new Relatorio();
                    rel.id_relatorio = int.Parse(row["id_relatorio"].ToString());
                    rel.arquivo = row["arquivo"].ToString();
                    rel.relatorio = row["relatorio"].ToString();
                    rel.titulo = row["titulo"].ToString();
                    list.Add(rel);
                }

                foreach (var item in list)
                {
                    foreach (DataRow row in dts.Tables[1].Rows)
                    {
                        if (item.id_relatorio == int.Parse(row["id_relatorio_param"].ToString()))
                        {
                            param = new Parametro();
                            param.parametro = row["parametro"].ToString();
                            item.parametros.Add(param);
                        }

                    }
                }
            }

            return list;
        }

        public DataTable CarregaParametro()
        {
            return ListarParametro();
        }

        public DateTime RetornaDIB(int matr, int num_seq)
        {
            return ValidaDIB(matr, num_seq);
        }

        public bool DeletarParametro(out string mensagem)
        {
            bool ret = DeletaParametro();

            if (ret)
                mensagem = "Parâmetro excluído com sucesso.\\nReprocesse os calculos retroativos.";
            else
                mensagem = "Não é possível excluir, pois o processo foi enviado para o VR.";

            return ret;
        }   

    }
}
