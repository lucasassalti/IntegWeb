using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.DAL.Tesouraria;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL.Tesouraria
{
    public class ExportaRelIntFinContabBLL : ExportaRelIntFinContabDAL
    {
        public DataTable ListarDadosCC(DateTime datIni, DateTime datFim)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CD_EMPRESA");
            dt.Columns.Add("CD_PLANO");
            dt.Columns.Add("DT_GERACAO_ARQUIVO");
            dt.Columns.Add("NO_ARQUIVO");
            dt.Columns.Add("DT_CD_MOV");
            dt.Columns.Add("NR_CD_MOV");
            dt.Columns.Add("SQ_LANC_MOV");
            dt.Columns.Add("CD_ATIVIDADE");
            dt.Columns.Add("CD_CONTA");
            dt.Columns.Add("CD_AUXILIAR");
            dt.Columns.Add("CD_CCUSTO");
            dt.Columns.Add("TP_DOCUM");
            dt.Columns.Add("NR_DOCUM");
            dt.Columns.Add("DT_COMPET");
            dt.Columns.Add("VL_LANCAMENTO");
            dt.Columns.Add("IR_DEB_CRED");
            dt.Columns.Add("CD_HISTORICO");
            dt.Columns.Add("DS_HIST_DIARIO");
            dt.Columns.Add("IR_CRITICA_MOV");
            dt.Columns.Add("CD_REDUZIDA");

            List<CC_INTEGR_CT> list = new List<CC_INTEGR_CT>();

            list = GetData(datIni, datFim);

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    dt.Rows.Add(list[i].CD_EMPRESA,
                                list[i].CD_PLANO,
                                list[i].DT_GERACAO_ARQUIVO,
                                list[i].NO_ARQUIVO,
                                list[i].DT_CD_MOV,
                                list[i].NR_CD_MOV,
                                list[i].SQ_LANC_MOV,
                                list[i].CD_ATIVIDADE,
                                list[i].CD_CONTA,
                                list[i].CD_AUXILIAR,
                                list[i].CD_CCUSTO,
                                list[i].TP_DOCUM,
                                list[i].NR_DOCUM,
                                list[i].DT_COMPET,
                                list[i].VL_LANCAMENTO,
                                list[i].IR_DEB_CRED,
                                list[i].CD_HISTORICO,
                                list[i].DS_HIST_DIARIO,
                                list[i].IR_CRITICA_MOV,
                                list[i].CD_REDUZIDA);
                }
            }
            return dt;
        }

        public DataTable ListarDadosGB(DateTime datIni, DateTime datFim)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CD_EMPRESA");
            dt.Columns.Add("CD_PLANO");
            dt.Columns.Add("DT_GERACAO_ARQUIVO");
            dt.Columns.Add("NO_ARQUIVO");
            dt.Columns.Add("DT_CD_MOV");
            dt.Columns.Add("NR_CD_MOV");
            dt.Columns.Add("SQ_LANC_MOV");
            dt.Columns.Add("CD_ATIVIDADE");
            dt.Columns.Add("CD_CONTA");
            dt.Columns.Add("CD_AUXILIAR");
            dt.Columns.Add("CD_CCUSTO");
            dt.Columns.Add("TP_DOCUM");
            dt.Columns.Add("NR_DOCUM");
            dt.Columns.Add("DT_COMPET");
            dt.Columns.Add("VL_LANCAMENTO");
            dt.Columns.Add("IR_DEB_CRED");
            dt.Columns.Add("CD_HISTORICO");
            dt.Columns.Add("DS_HIST_DIARIO");
            dt.Columns.Add("IR_CRITICA_MOV");
            dt.Columns.Add("CD_REDUZIDA");
            List<GB_INTEGR_CT> list = new List<GB_INTEGR_CT>();

            list = GetDataGB(datIni, datFim);

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    dt.Rows.Add(list[i].CD_EMPRESA,
                                list[i].CD_PLANO,
                                list[i].DT_GERACAO_ARQUIVO,
                                list[i].NO_ARQUIVO,
                                list[i].DT_CD_MOV,
                                list[i].NR_CD_MOV,
                                list[i].SQ_LANC_MOV,
                                list[i].CD_ATIVIDADE,
                                list[i].CD_CONTA,
                                list[i].CD_AUXILIAR,
                                list[i].CD_CCUSTO,
                                list[i].TP_DOCUM,
                                list[i].NR_DOCUM,
                                list[i].DT_COMPET,
                                list[i].VL_LANCAMENTO,
                                list[i].IR_DEB_CRED,
                                list[i].CD_HISTORICO,
                                list[i].DS_HIST_DIARIO,
                                list[i].IR_CRITICA_MOV,
                                list[i].CD_REDUZIDA);
                }
            }
            return dt;
        }

        //public Resultado ValidaCampos(string datIni, string datFim)
        //{
        //    Resultado retorno = new Resultado();

        //    if (datIni == "" && datFim != "")
        //    {
        //        retorno.Erro("Campo Obrigatorio");
        //        return retorno;
        //    }

        //    return retorno;
        //}
    }
}
