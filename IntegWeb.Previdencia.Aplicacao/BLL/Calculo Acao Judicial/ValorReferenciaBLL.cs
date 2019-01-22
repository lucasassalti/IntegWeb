//using IntegWeb.Framework;
using IntegWeb.Entidades;
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
    public class ValorReferenciaBLL : ValorReferenciaDAL
    {
        public Resultado ValidarVr()
        {
            Resultado retorno = new Resultado(true);
            StringBuilder str = new StringBuilder();
            if (num_matr_partf == null)
            {
                str.Append("Matrícula inválida.\\n");
            }

            if (num_sqncl_prc == null)
            {
                str.Append("Nº Seq. Processo inválido.\\n");
            }

            if (num_proc == null || String.IsNullOrEmpty(num_proc))
            {
                str.Append("Número do Processo obrigatório.\\n");
            }
            else
            {
                if (num_proc.IndexOf(",") > -1)
                {
                    str.Append("O campo Núm. Processo não pode conter VÍRGULA(S).\\n");
                }
            }

            if (cod_tip_atlz == null)
            {
                str.Append("Tipo Atualização obrigatório.\\n");
            }
            if (cpf_emprg == null)
            {
                str.Append("Inserir um Participante Válido.\\n");
            }
            if (dta_retr_atlz == null)
            {
                str.Append("Base Atualização Retr. obrigatória.\\n");
            }
            if (dta_status == null)
            {
                str.Append("Efetivação Revisão obrigatória.");
            }

            if (str.Length > 0)
            {
                retorno.Erro(str.ToString());
            }

            return retorno;

        }

        public void ImportacaoVr(out Resultado mensagem, bool ATLZ_IGPDI = false, bool ATLZ_TRAB = false, bool ATLZ_CIV = false)
        {
            mensagem = ImportaVr(ATLZ_IGPDI, ATLZ_TRAB, ATLZ_CIV);
        }

        public void DeletarVr(out Resultado mensagem)
        {
            mensagem = DesfazerImportacaoVr();
        }

        public bool VerificaVrDados()
        {
            return VerificarDadosVr();
        }

        public DataTable CarregaProcesso()
        {
            return ListarProcesso();
        }

        public DataTable ListarProcessosVr(int filType, string filValue, int? codEmpresa, int? codMatricula, string sortParameter)
        {
            DataTable dtReturn =
                      base.ListarProcessosVr(filType, filValue, codEmpresa, codMatricula);

            if (dtReturn.Rows.Count > 0)
            {
                return dtReturn.Select("", sortParameter).CopyToDataTable();
            }
            else
            {
                return dtReturn;
            }

        }

        public new DataTable CarregaProcessoVr()
        {
            return base.CarregaProcessoVr();
        }

        public new Resultado AtualizarProcessoVr()
        {
            Resultado ret = ValidarVr();
            return (ret.Ok) ? base.AtualizarProcessoVr() : ret;
        }

        public Resultado DuplicarVr()
        {
            Resultado ret = ValidarVr();
            cod_tip_atlz = null;
            return (ret.Ok) ? base.InserirProcessoVr() : ret;
        }

        public new Resultado InserirProcessoVr()
        {
            Resultado ret = ValidarVr();
            return (ret.Ok) ? base.InserirProcessoVr() : ret;
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

        public DataTable CarregaProcessosImportVr(int filType, string filValue, int? codEmpresa, int? codMatricula, string sortParameter)
        {

            DataTable dtReturn =
                      base.ListarProcessosImportVr(filType, filValue, codEmpresa, codMatricula);

            if (dtReturn.Rows.Count > 0)
            {
                return dtReturn.Select("", sortParameter).CopyToDataTable();
            }
            else
            {
                return dtReturn;
            }

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

        public DataTable CarregarDadosParticipante()
        {
            return base.CarregarDadosParticipante();
        }

        public DataTable ListarCustoJudicial(string sortParameter)
        {
            DataTable dtReturn =
                      base.ListarCustoJudicial();

            if (dtReturn.Rows.Count > 0)
            {
                return dtReturn.Select("", sortParameter).CopyToDataTable();
            }
            else
            {
                return dtReturn;
            }

        }

        public DataTable CarregaUnidadeMonetaria()
        {
            return base.CarregaUnidadeMonetaria().Select("COD_UM = 111", "NOM_ABRVO_UM").CopyToDataTable(); ;
        }

        //public DataTable CarregaCotacao(short p_cod_um)
        //{
        //    return base.CarregaCotacao(p_cod_um).Select("", "DAT_CMESUM").CopyToDataTable(); ;
        //}

        public Resultado GerarCustoJudicial(DateTime p_DAT_INI,
                                            DateTime p_DAT_FIN,
                                              Double p_LIM_DSP,
                                              string p_LIM_FLG,
                                               short p_COD_UM,
                                            DateTime p_DAT_ATLZ)
        {
            return base.GerarCustoJudicial(p_DAT_INI, p_DAT_FIN, p_LIM_DSP, p_LIM_FLG, p_COD_UM, p_DAT_ATLZ);
        }


        public Resultado DeletarCustoJudicial(DateTime p_HDRDATHOR)
        {
            return base.DeletarCustoJudicial(p_HDRDATHOR);
        }
    }
}
