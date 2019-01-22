using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL.Concessao;
using IntegWeb.Previdencia.Aplicacao.DAL.Concessao;
//using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using IntegWeb.Entidades;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{

    public class CriticasBLL : CriticasDAL
    {

        internal List<ATT_CHARGER_DEPARA> cache_DePara_EmpregMat_table = null;
        internal short cache_DePara_EmpregMat_emprs = 0;

        internal List<ATT_CHARGER_DEPARA> cache_DePara_OrgaoLotacao_table = null;
        internal short cache_DePara_OrgaoLotacao_emprs = 0;

        #region .: Geral :.

        internal void Empresa(PRE_TBL_ARQ_PATROCINA ARQUIVO,
                              PRE_TBL_ARQ_PATROCINA_LINHA ALVO,
                              List<PRE_TBL_ARQ_PATROCINA_LINHA> LINHAS,
                              LAY_CAMPO LAY_COD_EMPRS,
                              short[] lst_Emprs)
        {

            if (NumeroZerado(ALVO.COD_EMPRS))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(1, LAY_COD_EMPRS));
            }
            else if (!NumeroValido(ALVO.COD_EMPRS))
            //else if (!short.TryParse(ALVO.COD_EMPRS, out sTest))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(2, LAY_COD_EMPRS, ALVO.COD_EMPRS));
                //ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(NovaCritica(2,
                //                                       "Campo inválido; Valor = " + ALVO.COD_EMPRS,
                //                                       LAY_COD_EMPRS));
            }
            else
            {
                short sCOD_EMPRS;
                short.TryParse(ALVO.COD_EMPRS, out sCOD_EMPRS);
                EmpresaBLL EmpBLL = new EmpresaBLL();
                EMPRESA emprs = EmpBLL.GetEMPRESA(sCOD_EMPRS);

                if (emprs==null)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(3,
                                                           "Cód. Empresa não localizada; Valor = " + sCOD_EMPRS.ToString(), 
                                                           LAY_COD_EMPRS));
                }

                // if (Array.IndexOf(lst_Emprs, sCOD_EMPRS) == -1) <-- Metodo padrão mais lento que customizado:
                if (lst_Emprs!=null && Util.IndexOf(lst_Emprs, sCOD_EMPRS) == -1 && Util.IndexOf(lst_Emprs, 9999) == -1)
                {
                    string critica_4 = "Usuário logado não tem permissão para enviar de dados da empresa " + sCOD_EMPRS.ToString();                    
                    //if (ARQUIVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Where(c => c.COD_CRITICA == 4).Count() < 10)
                    //{
                          ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(4, critica_4, LAY_COD_EMPRS, ALVO.NUM_LINHA.ToString()));
                    //    ARQUIVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(4, critica_4, LAY_COD_EMPRS, ALVO.NUM_LINHA.ToString()));
                    //}
                }

            }

        }
        #endregion

        #region .: Afastamento :.

        internal void Afastamento(PRE_TBL_ARQ_PATROCINA ARQUIVO,
                                  PRE_TBL_ARQ_PATROCINA_LINHA ALVO,
                                  List<PRE_TBL_ARQ_PATROCINA_LINHA> LINHAS,
                                  LAY_AFASTAMENTO lay)
        {            

            PRE_TBL_ARQ_PATROCINA_LINHA LINHA_DUPLICADA = LINHAS.FirstOrDefault(l => l.COD_EMPRS == ALVO.COD_EMPRS &&
                                                                                l.NUM_RGTRO_EMPRG == ALVO.NUM_RGTRO_EMPRG &&
                                                                                l.NUM_LINHA != ALVO.NUM_LINHA);

            if (LINHA_DUPLICADA != null &&
                LINHA_DUPLICADA.DADOS.Equals(ALVO.DADOS) &&
                LINHA_DUPLICADA.PRE_TBL_ARQ_PATROCINA_CRITICA.FirstOrDefault(lc => lc.COD_CRITICA == 23) == null)
            {

                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(23, "Registro duplicado. (ln. " + LINHA_DUPLICADA.NUM_LINHA + ")"));
                //ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                //{
                //    COD_CRITICA = 23,
                //    DCR_CRITICA = "Registro duplicado. (ln. " + LINHA_DUPLICADA.NUM_LINHA + ")",
                //    NOM_CAMPO = "",
                //    NUM_POSICAO = 1
                //});
            }

            //LAY_AFASTAMENTO lay = new LAY_AFASTAMENTO();

            //Criticas_Empresa(ARQUIVO, ALVO, LINHAS, lay.LAY_COD_EMPRS);

            short COD_EMPRS;
            short.TryParse(ALVO.COD_EMPRS, out COD_EMPRS);

            int NUM_RGTRO_EMPRG = 0;
            int.TryParse(ALVO.NUM_RGTRO_EMPRG, out NUM_RGTRO_EMPRG);

            string DAT_INAFT_AFAST = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DAT_INAFT_AFAST);
            string DAT_FMAFT_AFAST = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DAT_FMAFT_AFAST);

            short sTIP_AFASTAMENTO = 0;
            string TIP_AFASTAMENTO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_TIPAFT);
            short.TryParse(TIP_AFASTAMENTO, out sTIP_AFASTAMENTO);


            if (DataEmBranco(DAT_INAFT_AFAST))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(201, lay.LAY_DAT_INAFT_AFAST));
            }
            else if (LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_INAFT_AFAST) > DateTime.Now)
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(202,
                                                                   "Data de Início de Afastamento posterior a data de hoje. Dt. Início Afastamento = " + LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_INAFT_AFAST).ToString("dd/MM/yyyy"),
                                                                   lay.LAY_DAT_INAFT_AFAST));
            }

            if (!DataEmBranco(DAT_FMAFT_AFAST) && !DataValida(DAT_FMAFT_AFAST))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(210,
                                                                   "Data do Final de Afastamento inválida. Final Afastamento = " + DAT_FMAFT_AFAST + " Formato correto = YYYYMMDD",
                                                                   lay.LAY_DAT_FMAFT_AFAST));
            }
            else 
            if (!DataEmBranco(DAT_FMAFT_AFAST) && LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_FMAFT_AFAST) > DateTime.Now)
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(203,
                                                                   "Data do Final de Afastamento posterior a data de hoje. Dt. Final Afastamento = " + LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_FMAFT_AFAST).ToString("dd/MM/yyyy"),
                                                                   lay.LAY_DAT_FMAFT_AFAST));
            }

            int qtd_afastamentos_em_aberto = 0;
            long num_linha_duplicada = 0;
            foreach(PRE_TBL_ARQ_PATROCINA_LINHA LN_AFAST in LINHAS.Where(l => l.COD_EMPRS == ALVO.COD_EMPRS && l.NUM_RGTRO_EMPRG == ALVO.NUM_RGTRO_EMPRG && ALVO.NUM_LINHA != l.NUM_LINHA))
            {
                string LN_DAT_FMAFT_AFAST = LAYOUT_UTIL.GetStringNull(LN_AFAST.DADOS, lay.LAY_DAT_FMAFT_AFAST);

                if (DataEmBranco(LN_DAT_FMAFT_AFAST)){
                    qtd_afastamentos_em_aberto += 1;
                    num_linha_duplicada = LN_AFAST.NUM_LINHA;
                    break;
                }
            }

            if (DataEmBranco(DAT_FMAFT_AFAST) && (qtd_afastamentos_em_aberto > 0))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(204,
                                                                   "Foram encontrados afastamentos em aberto para mesma matrícula. LINHA = " + num_linha_duplicada.ToString(),
                                                                   lay.LAY_COD_TIPAFT));
            }

            AfastamentoBLL AfastaBLL = new AfastamentoBLL();

            if (AfastaBLL.GetTipoAfastamento(sTIP_AFASTAMENTO) == null)
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(205,
                                                                   "Tipo de Afastamento não localizado. Tipo de Afastamento = " + sTIP_AFASTAMENTO.ToString(),
                                                                   lay.LAY_COD_TIPAFT));
            }

            AFASTAMENTO Afastamento = AfastaBLL.GetAfastamento(COD_EMPRS, NUM_RGTRO_EMPRG, LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_INAFT_AFAST));


            
            if (Afastamento != null &&
                !DataEmBranco(DAT_FMAFT_AFAST) && DataValida(DAT_FMAFT_AFAST) && !DataEmBranco(DAT_INAFT_AFAST) && DataValida(DAT_INAFT_AFAST) &&
                Afastamento.DAT_FMAFT_AFAST == LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_FMAFT_AFAST) && 
                Afastamento.DAT_INAFT_AFAST == LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_INAFT_AFAST) && 
                Afastamento.DAT_PRVFA_AFAST == LAYOUT_UTIL.GetData(ALVO.DADOS, lay.LAY_DAT_PRVFA_AFAST))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(209,
                                                                   "Já existe um afastamento idêntico no banco de dados. Data de Afastamento = " + Util.Date2String(Afastamento.DAT_INAFT_AFAST),
                                                                   lay.LAY_DAT_INAFT_AFAST));
            }
            else
            {
                Afastamento = AfastaBLL.GetAfastamentoEmAberto(COD_EMPRS, NUM_RGTRO_EMPRG);

                if (Afastamento != null)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(206,
                                                                       "Empregado já afastado. Data de Afastamento = " + Util.Date2String(Afastamento.DAT_INAFT_AFAST),
                                                                       lay.LAY_DAT_INAFT_AFAST));
                }

                ParticipanteBLL ParticBLL = new ParticipanteBLL();
                PARTICIPANTE Empregado =
                                ParticBLL.GetParticipanteBy(COD_EMPRS, NUM_RGTRO_EMPRG, 0, false);

                //
                // Erros especificos da tabela EMPREGADO
                //
                if (Empregado == null)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(207, 
                                                                        "Empregado não encontrado. A base de empregados deve ser importada primeiro.", 
                                                                        lay.LAY_NUM_RGTRO_EMPRG));
                } else if (Empregado.DAT_DESLG_EMPRG != null)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(208,
                                                                       "Alteração não permitida. Não permitido afastar empregado com data de desligamento. VALOR =" + Util.Date2String(Empregado.DAT_DESLG_EMPRG),
                                                                       lay.LAY_NUM_RGTRO_EMPRG));
                }
            }


        }
        #endregion

        #region .: OrgaoLotacao :.

        internal void OrgaoLotacao(PRE_TBL_ARQ_PATROCINA ARQUIVO,
                                   PRE_TBL_ARQ_PATROCINA_LINHA ALVO,
                                   List<PRE_TBL_ARQ_PATROCINA_LINHA> LINHAS,
                                   LAY_ORGAO lay)
        {
            //LAY_ORGAO lay = new LAY_ORGAO();
            //Criticas_Empresa(ARQUIVO, ALVO, LINHAS, lay.LAY_COD_EMPRS);

            string NUM_ORGAO = String.Empty;
            string NOM_ORGAO = String.Empty;
            string NUM_FILIAL = String.Empty;
            string DCR_ENDER_ORGAO = String.Empty;
            string NUM_ENDER_ORGAO = String.Empty;
            string DCR_COMPL_ORGAO = String.Empty;
            string NOM_CIDRS_ORGAO = String.Empty;
            string COD_UNDFD_ORGAO = String.Empty;
            string COD_CEP_ORGAO = String.Empty;
            string COD_DDD_ORGAO = String.Empty;
            string NUM_TELEF_ORGAO = String.Empty;

            NUM_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_ORGAO);
            NOM_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NOM_ORGAO);
            NUM_FILIAL = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_FILIAL);
            DCR_ENDER_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DCR_ENDER_ORGAO);
            NUM_ENDER_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_ENDER_ORGAO);
            DCR_COMPL_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DCR_COMPL_ORGAO);
            NOM_CIDRS_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NOM_CIDRS_ORGAO);
            COD_UNDFD_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_UNDFD_ORGAO);
            COD_CEP_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_CEP_ORGAO);
            COD_DDD_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_DDD_ORGAO);
            NUM_TELEF_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_TELEF_ORGAO);

            if (String.IsNullOrEmpty(NUM_ORGAO)) ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(301, lay.LAY_NUM_ORGAO));
            if (String.IsNullOrEmpty(NOM_ORGAO)) ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(302, lay.LAY_NOM_ORGAO));
            if (NumeroZerado(NUM_FILIAL))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(303, lay.LAY_NUM_FILIAL));
            }
            else if (!NumeroValido(NUM_FILIAL))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(304, lay.LAY_NUM_FILIAL, NUM_FILIAL));
            }
            if (String.IsNullOrEmpty(DCR_ENDER_ORGAO)) ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(305, lay.LAY_DCR_ENDER_ORGAO));
            if (NumeroZerado(NUM_ENDER_ORGAO))          
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(306, lay.LAY_NUM_ENDER_ORGAO));
            }
            else if (!NumeroValido(NUM_ENDER_ORGAO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(307, lay.LAY_NUM_ENDER_ORGAO, NUM_ENDER_ORGAO));
            }
            if (String.IsNullOrEmpty(DCR_COMPL_ORGAO)) ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(308, lay.LAY_DCR_COMPL_ORGAO));
            if (String.IsNullOrEmpty(NOM_CIDRS_ORGAO)) ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(309, lay.LAY_NOM_CIDRS_ORGAO));
            if (String.IsNullOrEmpty(COD_UNDFD_ORGAO)) ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(310, lay.LAY_COD_UNDFD_ORGAO));
            if (NumeroZerado(COD_CEP_ORGAO))            
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(311, lay.LAY_COD_CEP_ORGAO));
            }
            else if (!NumeroValido(COD_CEP_ORGAO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(312, lay.LAY_COD_CEP_ORGAO, COD_CEP_ORGAO));
            }
            if (String.IsNullOrEmpty(COD_DDD_ORGAO)) ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(313, lay.LAY_COD_DDD_ORGAO));
            if (NumeroZerado(NUM_TELEF_ORGAO))      
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(314, lay.LAY_NUM_TELEF_ORGAO));
            }
            else if (!NumeroValido(COD_DDD_ORGAO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(315, lay.LAY_NUM_TELEF_ORGAO, NUM_TELEF_ORGAO));
            }

        }
        #endregion

        #region .: Financeiro :.

        public partial class CONTROLE_ARQ_FINANCEIRO
        {
            public string COD_EMPRS { get; set; }
            public string NUM_RGTRO_EMPRG { get; set; }
            public bool DADOS_CARREGADOS { get; set; }
            public bool PARTIC_PREVIDENCIA { get; set; }
            public bool PARTIC_EMPRESTIMO { get; set; }
            public bool PARTIC_SAUDE { get; set; }
            public short? NUM_PLBNF_ATIVO { get; set; }
            public List<int> VERBAS { get; set; }
        }

        internal void Financeiro(PRE_TBL_ARQ_PATROCINA ARQUIVO,
                                 PRE_TBL_ARQ_PATROCINA_LINHA ALVO,
                                 List<PRE_TBL_ARQ_PATROCINA_LINHA> LINHAS,
                                 LAY_FICHA_FINANCEIRA lay,
                                 ref List<CONTROLE_ARQ_FINANCEIRO> CTRL_FINANCEIRO,
                                 string DAT_REPASSE_TELA,
                                 FinanceiroBLL FinBLL)
        {
            //int iTest;

            //LAY_FICHA_FINANCEIRA lay = new LAY_FICHA_FINANCEIRA();
            //Criticas_Empresa(ARQUIVO, ALVO, LINHAS, lay.LAY_COD_EMPRS);
            //
            // LINHAS DUPLICADAS
            //
            PRE_TBL_ARQ_PATROCINA_LINHA LINHA_DUPLICADA = LINHAS.FirstOrDefault(l => l.COD_EMPRS == ALVO.COD_EMPRS && l.NUM_RGTRO_EMPRG == ALVO.NUM_RGTRO_EMPRG && l.NUM_LINHA != ALVO.NUM_LINHA && l.NUM_HASH_LINHA == ALVO.NUM_HASH_LINHA);

            if (LINHA_DUPLICADA != null &&
                LINHA_DUPLICADA.DADOS.Equals(ALVO.DADOS))
            //LINHA_DUPLICADA.PRE_TBL_ARQ_PATROCINA_CRITICA.FirstOrDefault(lc => lc.COD_CRITICA == 43) == null)
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                {
                    COD_CRITICA = 43,
                    DCR_CRITICA = "Registro duplicado. (ln. " + LINHA_DUPLICADA.NUM_LINHA + ")",
                    NOM_CAMPO = "",
                    NUM_POSICAO = 1
                });
            }

            if (ALVO.TIP_LINHA != 2)
            {
                ARQUIVO._LINHA_HEADER = ALVO;

                string DT_CREDITO = String.Empty;
                string DT_REPASSE = String.Empty;
                string VLR_TOTAL_REPASSADO = String.Empty;
                string NUM_TOTAL_REGISTROS = String.Empty;

                if (ALVO.TIP_LINHA == 1) //Cabeçalho
                {
                    DT_CREDITO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.CABECALHO.LAY_DT_CREDITO);
                    DT_REPASSE = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.CABECALHO.LAY_DT_REPASSE);
                    VLR_TOTAL_REPASSADO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.CABECALHO.LAY_VLR_TOTAL_REPASSADO);
                    NUM_TOTAL_REGISTROS = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.CABECALHO.LAY_NUM_TOTAL_REGISTROS);
                }
                else if (ALVO.TIP_LINHA == 3) //Rodapé
                {
                    DT_CREDITO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.RODAPE.LAY_DT_CREDITO);
                    DT_REPASSE = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.RODAPE.LAY_DT_REPASSE);
                    VLR_TOTAL_REPASSADO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.RODAPE.LAY_VLR_TOTAL_REPASSADO);
                    NUM_TOTAL_REGISTROS = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.RODAPE.LAY_NUM_TOTAL_REGISTROS);
                }

                if (DataEmBranco(DT_CREDITO))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(401, ALVO.TIP_LINHA == 1 ? lay.CABECALHO.LAY_DT_CREDITO : lay.RODAPE.LAY_DT_REPASSE));
                }
                else if (!DataValida(DT_CREDITO))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(402, "Campo inválido. Data de Crédito inválida; Valor=" + DT_CREDITO, ALVO.TIP_LINHA == 1 ? lay.CABECALHO.LAY_DT_CREDITO : lay.RODAPE.LAY_DT_REPASSE));
                }

                if (DataEmBranco(DT_REPASSE))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(403, ALVO.TIP_LINHA == 1 ? lay.CABECALHO.LAY_DT_CREDITO : lay.RODAPE.LAY_DT_REPASSE));
                }
                else if (!DataValida(DT_REPASSE))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(404, "Campo inválido. Data de Repasse inválida; Valor=" + DataValidaFormatada(DT_REPASSE),
                                                                       ALVO.TIP_LINHA == 1 ? lay.CABECALHO.LAY_DT_CREDITO : lay.RODAPE.LAY_DT_REPASSE));
                }

                string sDT_REPASSE = Util.Date2String(Util.String2Date(DAT_REPASSE_TELA), "yyyyMMdd");

                if (DT_REPASSE != sDT_REPASSE)
                {
                    ARQUIVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(405, "Campo inválido. Data de Repasse do arquivo não confere com a data da tela; Data do arq.: " + DataValidaFormatada(DT_REPASSE) + " Tela= " + Util.Date2String(Util.String2Date(DAT_REPASSE_TELA), "dd/MM/yyyy"),
                                                                          ALVO.TIP_LINHA == 1 ? lay.CABECALHO.LAY_DT_CREDITO : lay.RODAPE.LAY_DT_REPASSE));
                }


            }
            else
            {
                string COD_VERBA = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_VERBA);
                string ANO_REF = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_ANO_REFER_VERFIN);
                string MES_REF = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_MES_REFER_VERFIN);
                string VLR_VERBA = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_VLR_VERFIN);
                string ANO_COMP = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_ANO_PAGTO_VERFIN);
                string MES_COMP = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_MES_PAGTO_VERFIN);

                if (String.IsNullOrEmpty(COD_VERBA))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(406, lay.LAY_COD_VERBA));
                }                 

                if (NumeroZerado(ANO_REF))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(407, lay.LAY_ANO_REFER_VERFIN));
                }
                else if (!NumeroValido(ANO_REF))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(408, lay.LAY_ANO_REFER_VERFIN, ANO_REF));
                }
                else
                {
                    if (!AnoValido(ANO_REF))
                    {
                        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(409, lay.LAY_ANO_REFER_VERFIN, ANO_REF));
                    }
                }

                if (NumeroZerado(MES_REF))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(410, lay.LAY_MES_REFER_VERFIN));
                }
                else if (!NumeroValido(MES_REF))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(411, lay.LAY_MES_REFER_VERFIN, MES_REF));
                }
                else 
                {
                    if (!MesValido(MES_REF))
                    {
                        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(412, lay.LAY_MES_REFER_VERFIN, MES_REF));
                    }
                }

                if (NumeroZerado(VLR_VERBA))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(413, lay.LAY_VLR_VERFIN));
                }
                else if (!NumeroValido(VLR_VERBA))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(414, lay.LAY_VLR_VERFIN, VLR_VERBA));
                }

                if (NumeroZerado(ANO_COMP))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(415, lay.LAY_ANO_PAGTO_VERFIN));
                }
                else if (!NumeroValido(ANO_COMP))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(416, lay.LAY_ANO_PAGTO_VERFIN, ANO_COMP));
                }
                else
                {
                    if (!AnoValido(ANO_COMP))
                    {
                        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(417, lay.LAY_ANO_PAGTO_VERFIN, ANO_COMP));
                    }
                }

                if (NumeroZerado(MES_COMP))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(418, lay.LAY_MES_PAGTO_VERFIN));
                }
                else if (!NumeroValido(MES_COMP))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(419, lay.LAY_MES_PAGTO_VERFIN, MES_COMP));
                }
                else
                {
                    if (!MesValido(MES_COMP))
                    {
                        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(420, lay.LAY_MES_PAGTO_VERFIN, MES_COMP));
                    }
                }

                // 1 - Salário
                // 2 - Salário 13o
                // 3 - Previdência
                // 4 - Previdência 13o
                // 5 - Saúde
                // 6 - Seguros 
                // 7 - Empréstimo

                short COD_EMPRS;
                short.TryParse(ALVO.COD_EMPRS, out COD_EMPRS);

                int NUM_RGTRO_EMPRG;
                int.TryParse(ALVO.NUM_RGTRO_EMPRG, out NUM_RGTRO_EMPRG);

                if (NumeroZerado(ALVO.NUM_RGTRO_EMPRG))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(421, lay.LAY_NUM_RGTRO_EMPRG));
                }
                else if (!NumeroValido(ALVO.NUM_RGTRO_EMPRG))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(422, lay.LAY_NUM_RGTRO_EMPRG, ALVO.NUM_RGTRO_EMPRG));
                }

                int iCOD_VERBA;
                int.TryParse(COD_VERBA, out iCOD_VERBA);

                ParticipanteBLL ParticBLL = new ParticipanteBLL();

                if (COD_EMPRS == 66) // CPFL - Piratininga: De Para de matriculas 
                {
                    ATT_CHARGER_DEPARA DaPara = GetEmpregadoMatricula_DE_PARA(COD_EMPRS, NUM_RGTRO_EMPRG.ToString());
                    if (DaPara != null)
                    {
                        //System.Diagnostics.Debug.WriteLine(NUM_RGTRO_EMPRG.ToString() + " -> " + DaPara.CONTEUDOPARA);
                        NUM_RGTRO_EMPRG = int.Parse(DaPara.CONTEUDOPARA);
                    }
                }

                PARTICIPANTE Empregado = ParticBLL.GetParticipanteBy(COD_EMPRS, NUM_RGTRO_EMPRG, 0, false);
                //FinanceiroBLL FinBLL = new FinanceiroBLL();

                if (Empregado != null)
                {

                    CONTROLE_ARQ_FINANCEIRO CTRL_PARTICIPANTE = Ini_Controle_Participante(ref CTRL_FINANCEIRO, ALVO.COD_EMPRS, ALVO.NUM_RGTRO_EMPRG);                    
                    CTRL_PARTICIPANTE.VERBAS.Add(iCOD_VERBA);    
                
                    //Trava para não repetir a consulta varias vezes para o mesmo participante:
                    if (!CTRL_PARTICIPANTE.DADOS_CARREGADOS)
                    {
                        //ADESAO_PLANO_PARTIC_FSS aPrevidencia = FinBLL.GetPlanoPartic_ATIVO(NUM_RGTRO_EMPRG, null);
                        short PARTIC_NUM_PLBNF = FinBLL.GetPlanoPartic_ATIVO(NUM_RGTRO_EMPRG, null);

                        // Participante da PREVIDENCIA:
                        //if (aPrevidencia != null)
                        if (PARTIC_NUM_PLBNF > 0)
                        {
                            CTRL_PARTICIPANTE.PARTIC_PREVIDENCIA = true;
                            //CTRL_PARTICIPANTE.NUM_PLBNF_ATIVO = aPrevidencia.NUM_PLBNF;
                            CTRL_PARTICIPANTE.NUM_PLBNF_ATIVO = PARTIC_NUM_PLBNF;
                        }

                        //TB_PARTICIP_PLANO aSaude = FinBLL.GetPlanoSaudePartic_ATIVO(COD_EMPRS, NUM_RGTRO_EMPRG.ToString());
                        CTRL_PARTICIPANTE.PARTIC_SAUDE = FinBLL.Tem_PLANO_SAUDE_Ativo(COD_EMPRS, NUM_RGTRO_EMPRG.ToString());
                        //IRE_EMPREST_RECEBE aEmprestimo = FinBLL.GetEmpretimoPartic_ATIVO(COD_EMPRS, NUM_RGTRO_EMPRG, ANO_REF + MES_REF);
                        CTRL_PARTICIPANTE.PARTIC_EMPRESTIMO = FinBLL.Tem_EMPRESTIMO_Ativo(COD_EMPRS, NUM_RGTRO_EMPRG, ANO_REF + MES_REF);

                        CTRL_PARTICIPANTE.DADOS_CARREGADOS = true;
                    }

                    PRE_TBL_ARQ_PAT_VERBA gVerba = FinBLL.GetGrupoVerba(COD_EMPRS, iCOD_VERBA, 4); // Previdência 13o                
                    //bool VerbaPrevidencia13 = FinBLL.GetGrupoVerba2(COD_EMPRS, iCOD_VERBA, 4); // Previdência 13o

                    if (gVerba != null && MES_REF != "12")
                    //if (VerbaPrevidencia13 && MES_REF != "12")
                    {

                        if (Empregado.DAT_DESLG_EMPRG == null)
                        {
                            ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(423,
                                                                    "Verbas de Contribuição previdenciária para 13º não permitida. Esta verba é permitida apenas no mês de dezembro.",
                                                                    lay.LAY_COD_VERBA,
                                                                    COD_EMPRS.ToString("000"),
                                                                    NUM_RGTRO_EMPRG.ToString("0000"),
                                                                    iCOD_VERBA.ToString("00000")));
                        }
                        //else if (DateTime.Parse(Util.Date2String(Empregado.DAT_DESLG_EMPRG)).Month.ToString("00") != MES_REF)
                        //{
                        //    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(424,
                        //                                            "Verbas de Contribuição previdenciária para 13º não permitida. Esta verba é permitida apenas no mês de desligamento do empregado; MÊS DESLIG. = " + Util.Date2String(Empregado.DAT_DESLG_EMPRG),
                        //                                            lay.LAY_COD_VERBA,
                        //                                            COD_EMPRS.ToString("000"),
                        //                                            NUM_RGTRO_EMPRG.ToString("0000"),
                        //                                            iCOD_VERBA.ToString("00000")));
                        //}

                    }                    


                }
                else
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(425,
                                                                   "Empregado informado não está cadastrado",
                                                                   lay.LAY_NUM_RGTRO_EMPRG,
                                                                   COD_EMPRS.ToString("000"),
                                                                   NUM_RGTRO_EMPRG.ToString("0000"),
                                                                   iCOD_VERBA.ToString("00000")));
                }
            }
        }

        internal void Financeiro_2(PRE_TBL_ARQ_PATROCINA ARQUIVO, ref List<CONTROLE_ARQ_FINANCEIRO> CTRL_FINANCEIRO, string sDAT_REPASSE)
        {
            LAY_FICHA_FINANCEIRA lay = new LAY_FICHA_FINANCEIRA();            

            foreach (CONTROLE_ARQ_FINANCEIRO CTRL_PARTICIPANTE in CTRL_FINANCEIRO)
            {

                short COD_EMPRS;
                short.TryParse(CTRL_PARTICIPANTE.COD_EMPRS, out COD_EMPRS);

                long NUM_RGTRO_EMPRG;
                long.TryParse(CTRL_PARTICIPANTE.NUM_RGTRO_EMPRG, out NUM_RGTRO_EMPRG);

                if (ARQUIVO.MES_REF != 13)
                {

                    if (CTRL_PARTICIPANTE.PARTIC_PREVIDENCIA)
                    {
                        int COD_VERBA_OBRIGATORIA = GetVerbaObrigatoria(COD_EMPRS, CTRL_PARTICIPANTE.NUM_PLBNF_ATIVO, 1);
                        if (COD_VERBA_OBRIGATORIA > 0 && CTRL_PARTICIPANTE.VERBAS.IndexOf(COD_VERBA_OBRIGATORIA) == -1)
                        {
                            ARQUIVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(450,
                                                                                  "Verba obrigatória para Participante com plano de previdencia não foi informada.",
                                                                                  lay.LAY_COD_VERBA,
                                                                                  COD_EMPRS.ToString("000"),
                                                                                  NUM_RGTRO_EMPRG.ToString("0000"),
                                                                                  COD_VERBA_OBRIGATORIA.ToString("00000")));
                        }
                    }

                    if (CTRL_PARTICIPANTE.PARTIC_SAUDE)
                    {
                        int COD_VERBA_OBRIGATORIA = GetVerbaObrigatoria(0, 0, 5); // Todas empresas
                        if (COD_VERBA_OBRIGATORIA > 0 && CTRL_PARTICIPANTE.VERBAS.IndexOf(COD_VERBA_OBRIGATORIA) == -1)
                        {
                            ARQUIVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(451,
                                //String.Format("Verba não informada. Verba obrigatória para Participante com plano de saúde não foi informada: COD_VERBA = {0}; PARTICIPANTE = {1}", COD_VERBA_OBRIGATORIA, CTRL_PARTICIPANTE.NUM_RGTRO_EMPRG),
                                                                                  "Verba obrigatória para Participante com plano de saúde não foi informada.",
                                                                                  lay.LAY_COD_VERBA,
                                                                                  COD_EMPRS.ToString("000"),
                                                                                  NUM_RGTRO_EMPRG.ToString("0000"),
                                                                                  COD_VERBA_OBRIGATORIA.ToString("00000")));
                        }
                    }

                    if (CTRL_PARTICIPANTE.PARTIC_EMPRESTIMO)
                    {

                        int COD_VERBA_OBRIGATORIA = GetVerbaObrigatoria(COD_EMPRS, 0, 7); // Todas empresas
                        if (CTRL_PARTICIPANTE.PARTIC_PREVIDENCIA &&
                            COD_VERBA_OBRIGATORIA > 0 &&
                            CTRL_PARTICIPANTE.VERBAS.IndexOf(COD_VERBA_OBRIGATORIA) == -1)
                        {
                            ARQUIVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(452,
                                //String.Format("Verba não informada. Verba obrigatória para Participante com emprestimo e plano de previdência não foi informada: COD_VERBA = {0}; PARTICIPANTE = {1}", COD_VERBA_OBRIGATORIA, CTRL_PARTICIPANTE.NUM_RGTRO_EMPRG),
                                                                                  "Verba obrigatória para Participante com emprestimo e plano de previdência não foi informada.",
                                                                                  lay.LAY_COD_VERBA,
                                                                                  COD_EMPRS.ToString("000"),
                                                                                  NUM_RGTRO_EMPRG.ToString("0000"),
                                                                                  COD_VERBA_OBRIGATORIA.ToString("00000")));
                        }
                    }
                }

            }
        }

        private int GetVerbaObrigatoria(short pCOD_EMPRS, short? NUM_PLBNF, short pTIP_GRUPO_VERBA)
        {
            if (pTIP_GRUPO_VERBA == 1) //Salário
            {
                switch (pCOD_EMPRS)
                {
                    case 1: //CESP
                        if (NUM_PLBNF == 15 || NUM_PLBNF == null) // SRC VERBAS FIXAS CESP
                        {
                            return 82000;
                        }
                        else if (NUM_PLBNF == 40) // SRC VERBAS FIXAS CESP PL 40
                        {
                            return 82020;
                        }
                        break;

                    case 4: // FUNCESP 
                        return 81004; // SRC FUNDAÇÃO CESP

                    case 40: // ELETROPAULO 
                        return 82010; // SRC VERBAS FIXAS ELETROPAULO

                    case 42: // EMAE
                        return 82014; // SRC VERBAS FIXAS EMAE

                    case 43: // CTEEP
                        if (NUM_PLBNF == 30 || NUM_PLBNF == null) // SRC VERBAS FIXAS CTEEP
                        {
                            return 82002;
                        }
                        else if (NUM_PLBNF == 18) // SRC VERBAS FIXAS EPTE
                        {
                            return 82012;
                        }
                        break;

                    case 44: // TIETÊ
                        return 82004; // SRC VERBAS FIXAS TIETÊ

                    case 45: // DUKE
                        return 82006; // SRC VERBAS FIXAS DUKE

                    case 50: // ELEKTRO
                        return 82008; // SRC VERBAS FIXAS ELEKTRO

                    case 2: // CPFL
                    case 62: // CPFL GERACAO
                    case 66: // CPFL PIRATININGA
                    case 71: // CPFL BRASIL
                        if (NUM_PLBNF == 16 || NUM_PLBNF == null) // SRC CPFL
                        {
                            return 81000;
                        }
                        else if (NUM_PLBNF == 39) // SRC VERBAS FIXAS PIRATININGA
                        {
                            return 82018;
                        }
                        break;

                }
            }


            if (pTIP_GRUPO_VERBA == 5) //Saúde
            {
                return 80010;
            }

            if (pTIP_GRUPO_VERBA == 7) //Empréstimo
            {
                switch (pCOD_EMPRS)
                {
                    case 40: //ELETROPAULO
                    case 45: //DUKE
                        return 0;
                    default:
                        return 80300;
                }
            }

            return 0;
        }

        private CONTROLE_ARQ_FINANCEIRO Ini_Controle_Participante(ref List<CONTROLE_ARQ_FINANCEIRO> CTRL_FINANCEIRO, string pCOD_EMPRS, string pNUM_RGTRO_EMPRG)
        {
            CONTROLE_ARQ_FINANCEIRO CTRL_PARTICIPANTE = CTRL_FINANCEIRO.FirstOrDefault(f => f.COD_EMPRS == pCOD_EMPRS && f.NUM_RGTRO_EMPRG == pNUM_RGTRO_EMPRG);

            if (CTRL_PARTICIPANTE == null)
            {
                CTRL_PARTICIPANTE = new CONTROLE_ARQ_FINANCEIRO();
                CTRL_PARTICIPANTE.COD_EMPRS = pCOD_EMPRS;
                CTRL_PARTICIPANTE.NUM_RGTRO_EMPRG = pNUM_RGTRO_EMPRG;
                CTRL_PARTICIPANTE.DADOS_CARREGADOS = false;
                CTRL_PARTICIPANTE.VERBAS = new List<int>();
                CTRL_FINANCEIRO.Add(CTRL_PARTICIPANTE);
            }

            return CTRL_PARTICIPANTE;
        }

        #endregion

        #region .: Cadastral :.

        internal void Cadastral(PRE_TBL_ARQ_PATROCINA ARQUIVO,
                                PRE_TBL_ARQ_PATROCINA_LINHA ALVO,
                                List<PRE_TBL_ARQ_PATROCINA_LINHA> LINHAS,
                                LAY_EMPREGADO lay,
                                FinanceiroBLL FinBLL)
        {

            //LAY_EMPREGADO lay = new LAY_EMPREGADO(short.Parse(ALVO.COD_EMPRS));

            //Criticas_Empresa(ARQUIVO, ALVO, LINHAS, lay.LAY_COD_EMPRS);

            //
            // LINHAS DUPLICADAS
            //
            PRE_TBL_ARQ_PATROCINA_LINHA LINHA_DUPLICADA = LINHAS.FirstOrDefault(l => l.COD_EMPRS == ALVO.COD_EMPRS && l.NUM_RGTRO_EMPRG == ALVO.NUM_RGTRO_EMPRG && l.NUM_LINHA != ALVO.NUM_LINHA);
            //PRE_TBL_ARQ_PATROCINA_LINHA LINHA_DUPLICADA = ARQUIVO.PRE_TBL_ARQ_PATROCINA_LINHA.FirstOrDefault(l => l.NUM_HASH_LINHA == LINHA.NUM_HASH_LINHA && l.DADOS.Equals(LINHA.DADOS) && l.NUM_LINHA != LINHA.NUM_LINHA);
            //IEnumerable<PRE_TBL_ARQ_PATROCINA_LINHA> bloco_LINHAS = ARQUIVO.PRE_TBL_ARQ_PATROCINA_LINHA.Where(l => l.NUM_HASH_LINHA == LINHA.NUM_HASH_LINHA && l.NUM_LINHA != LINHA.NUM_LINHA);
            //System.Diagnostics.Debug.WriteLine("Linha " + LINHA.NUM_LINHA + ": " + bloco_LINHAS.Count().ToString());
            //foreach(PRE_TBL_ARQ_PATROCINA_LINHA LINHA_DUPLICADA in bloco_LINHAS)
            //{

            //PRE_TBL_ARQ_PATROCINA_LINHA LINHA_DUPLICADA = ARQUIVO.PRE_TBL_ARQ_PATROCINA_LINHA
            //    .Where(l => l.NUM_HASH_LINHA == LINHA.NUM_HASH_LINHA)
            //    .FirstOrDefault(lh => lh.DADOS.Equals(LINHA.DADOS) && lh.NUM_LINHA != LINHA.NUM_LINHA);

            if (LINHA_DUPLICADA != null &&
                LINHA_DUPLICADA.DADOS.Equals(ALVO.DADOS) &&
                LINHA_DUPLICADA.PRE_TBL_ARQ_PATROCINA_CRITICA.FirstOrDefault(lc => lc.COD_CRITICA == 13) == null)
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                {
                    COD_CRITICA = 13,
                    DCR_CRITICA = "Registro duplicado. (ln. " + LINHA_DUPLICADA.NUM_LINHA + ")",
                    NOM_CAMPO = "",
                    NUM_POSICAO = 1
                });
            }

            //int iTest;
            short sTest;

            short COD_EMPRS;
            short.TryParse(ALVO.COD_EMPRS, out COD_EMPRS);

            int NUM_RGTRO_EMPRG = 0;
            int.TryParse(ALVO.NUM_RGTRO_EMPRG, out NUM_RGTRO_EMPRG);

            /////////////////////////////////////////////////////
            // CAMPO REGISTRO DO EMPREGADO:
            //

            if (NumeroZerado(ALVO.NUM_RGTRO_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(101, lay.LAY_NUM_RGTRO_EMPRG));
            }
            else if (!NumeroValido(ALVO.NUM_RGTRO_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(102, lay.LAY_NUM_RGTRO_EMPRG, ALVO.NUM_RGTRO_EMPRG));
            }

            /////////////////////////////////////////////////////
            // CAMPO DIGITO VERIFICADOR DO REGISTRO DO EMPREGADO:
            //

            //string DIG_VERIFICADOR = ALVO.DADOS.Substring(0, 1).Trim();
            string DIG_VERIFICADOR = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_DIGVR_EMPRG);

            if (!String.IsNullOrEmpty(DIG_VERIFICADOR) && (!short.TryParse(DIG_VERIFICADOR, out sTest)))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(104, lay.LAY_NUM_DIGVR_EMPRG, DIG_VERIFICADOR));
            }

            /////////////////////////////////////////////////////
            // CAMPO NOME DO EMPREGADO:
            //

            //string NOME_EMPREGADO = ALVO.DADOS.Substring(1, 50).Trim();
            string NOME_EMPREGADO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NOM_EMPRG);

            if (String.IsNullOrEmpty(NOME_EMPREGADO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(105, lay.LAY_NOM_EMPRG));
            } else {

                if (NOME_EMPREGADO.IndexOf('.') > -1)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(
                        new PRE_TBL_ARQ_PATROCINA_CRITICA
                    {
                        TIP_CRITICA = 2,
                        COD_CRITICA = 106,
                        DCR_CRITICA = "Caractere '.' não permitidos no campo [NOME EMPREGADO]; Valor=" + NOME_EMPREGADO,
                        NOM_CAMPO = "NOME EMPREGADO",
                        NUM_POSICAO = Convert.ToInt16(15 + NOME_EMPREGADO.IndexOf('.')) //15
                    });
                }

                if (NOME_EMPREGADO.IndexOf("  ") > -1)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                    {
                        TIP_CRITICA = 2,
                        COD_CRITICA = 107,
                        DCR_CRITICA = "Campo com excesso de espaços entre nomes; [NOME EMPREGADO] Valor=" + NOME_EMPREGADO,
                        NOM_CAMPO = "NOME EMPREGADO",
                        NUM_POSICAO = Convert.ToInt16(15 + NOME_EMPREGADO.IndexOf("  ").ToString()) //15
                    });
                }

                string[] _nomes = NOME_EMPREGADO.Split(' ');

                for (int i = 0; i < _nomes.Length; i++)
                {
                    if (_nomes[i].Length < 2 && !String.IsNullOrEmpty(_nomes[i]))
                    {
                        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                       {
                           TIP_CRITICA = 2,
                           COD_CRITICA = 108,
                           DCR_CRITICA = "Nome ou sobrenome com menos de dois caracteres; [NOME EMPREGADO] Valor=" + NOME_EMPREGADO,
                           NOM_CAMPO = "NOME EMPREGADO",
                           NUM_POSICAO = 15
                       });
                        break;
                    }
                }
            }

            /////////////////////////////////////////////////////
            // CAMPO SEXO, CPF, DT. NASCIMENTO E DT. ADMISSÃO
            //

            //EmpregadoBLL ParticBLL = new EmpregadoBLL();
            //EMPREGADO Empregado = ParticBLL.GetEmpregado(null, null, Convert.ToInt64(CPF), null);
            ParticipanteBLL ParticBLL = new ParticipanteBLL();
            BateCadastralCargaBLL BateBLL = new BateCadastralCargaBLL();

            string SEXO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_SEXO_EMPRG);
            string CPF = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_CPF_EMPRG);
            string DT_NASCIMENTO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DAT_NASCM_EMPRG);
            string DT_ADMISSAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DAT_ADMSS_EMPRG);
            string DT_DESLIGAMENTO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DAT_DESLG_EMPRG);
            string MOTIVO_DESLIGAMENTO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_MTDSL);
            string DT_EXP_RG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DAT_EXPCI_EMPRG);            
            string COD_CEP_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_CEP_EMPRG);
            string NUM_TELRES_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_TELRES_EMPRG);            
            string NUM_CELULAR = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_CELUL_EMPRG);
            string EMAIL = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_EMAIL_EMPRG);
            string NUM_CARGO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_CARGO);

            string NUM_ORGAO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_ORGAO);
            string COD_BANCO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_BANCO);
            string COD_AGBCO = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_AGBCO);
            string NUM_CTCOR_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_CTCOR_EMPRG);
            string TIP_CTCOR_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_TIP_CTCOR_EMPRG);

            string COD_ESTCV_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_ESTCV_EMPRG);
            string COD_CTTRB_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_CTTRB_EMPRG);

            string DCR_ENDER_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DCR_ENDER_EMPRG);
            string NUM_ENDER_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NUM_ENDER_EMPRG);  
            string DCR_COMPL_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_DCR_COMPL_EMPRG);  
            string NOM_BAIRRO_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NOM_BAIRRO_EMPRG);  
            string NOM_CIDRS_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_NOM_CIDRS_EMPRG);  
            string COD_UNDFD_EMPRG = LAYOUT_UTIL.GetStringNull(ALVO.DADOS, lay.LAY_COD_UNDFD_EMPRG);

            if (String.IsNullOrEmpty(SEXO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(109, lay.LAY_COD_SEXO_EMPRG));
            }

            if (DataEmBranco(DT_NASCIMENTO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(110, lay.LAY_DAT_NASCM_EMPRG));
            }
            else if (!DataValida(DT_NASCIMENTO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(111, lay.LAY_DAT_NASCM_EMPRG, DT_NASCIMENTO));
            }

            if (String.IsNullOrEmpty(DT_ADMISSAO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(112, lay.LAY_DAT_ADMSS_EMPRG));
            }


            if (NumeroZerado(COD_CEP_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(113,
                                                                                     lay.LAY_COD_CEP_EMPRG));
            }
            else if (!NumeroValido(COD_CEP_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(114, lay.LAY_COD_CEP_EMPRG, COD_CEP_EMPRG));
            }

            //if (NumeroZerado(NUM_TELRES_EMPRG))
            //{
            //    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(115,
            //                                                                         lay.LAY_NUM_TELRES_EMPRG));
            //}
            //else if (!NumeroValido(NUM_TELRES_EMPRG))
            //{
            //    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(116, lay.LAY_NUM_TELRES_EMPRG, NUM_TELRES_EMPRG));
            //}

            if (!NumeroZerado(NUM_TELRES_EMPRG) && !NumeroValido(NUM_TELRES_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(116, lay.LAY_NUM_TELRES_EMPRG, NUM_TELRES_EMPRG));
            }

            if (NumeroZerado(COD_ESTCV_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(117,
                                                                                     lay.LAY_COD_ESTCV_EMPRG));
            }
            else if (!NumeroValido(COD_ESTCV_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(118, lay.LAY_COD_ESTCV_EMPRG, COD_ESTCV_EMPRG));
            }

            if (NumeroZerado(COD_CTTRB_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(119,
                                                                                     lay.LAY_COD_CTTRB_EMPRG));
            }
            else if (!NumeroValido(COD_CTTRB_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(120, lay.LAY_COD_CTTRB_EMPRG, COD_CTTRB_EMPRG));
            }

            if (NumeroZerado(NUM_ENDER_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(121,
                                                                                     lay.LAY_NUM_ENDER_EMPRG));
            }
            else if (!NumeroValido(NUM_ENDER_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(122, lay.LAY_NUM_ENDER_EMPRG, NUM_ENDER_EMPRG));
            }

            if (String.IsNullOrEmpty(NUM_ORGAO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(123,
                                                                                     lay.LAY_NUM_ORGAO));
            }
            else
            {
                //COD_EMPRS, NUM_RGTRO_EMPRG
                ATT_CHARGER_DEPARA DaPara = GetOrgaoLotacao_DE_PARA(COD_EMPRS, NUM_ORGAO);
                //ATT_CHARGER_DEPARA DaPara = base.GetOrgaoLotacao_DE_PARA(newEmpregado.COD_EMPRS, newEmpregado._NUM_ORGAO_ARQUIVO);
                if (DaPara == null)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(152,
                                                            "Orgão não localizado. Carregue primeiro o arquivo de orgão de lotação. Núm. Orgão=" + NUM_ORGAO,
                                                            lay.LAY_NUM_ORGAO));
                }
            }

            if (DataEmBranco(DT_EXP_RG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(124, lay.LAY_DAT_EXPCI_EMPRG));
            }
            else if (!DataValida(DT_EXP_RG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(125, lay.LAY_DAT_EXPCI_EMPRG, DT_EXP_RG));
            }
            else if (!DataEmBranco(DT_NASCIMENTO) && DataValida(DT_NASCIMENTO))
            {
                if (int.Parse(DT_NASCIMENTO) >= int.Parse(DT_EXP_RG))
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(126,
                                                           "Campo inválido. Dt. da nascimento maior que a Dt. de emissão do RG. DT.NASC=" + DT_NASCIMENTO + ". DT.RG=" + DT_EXP_RG,
                                                           lay.LAY_DAT_EXPCI_EMPRG));
                }
            }

            if (NumeroZerado(COD_BANCO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(127,
                                                                                     lay.LAY_COD_BANCO));
            }
            else if (!NumeroValido(COD_BANCO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(128, lay.LAY_COD_BANCO, COD_BANCO));
            }

            if (NumeroZerado(COD_AGBCO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(129,
                                                                                     lay.LAY_COD_AGBCO));
            }
            else if (!NumeroValido(COD_AGBCO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(130, lay.LAY_COD_AGBCO, COD_AGBCO));
            }

            if (String.IsNullOrEmpty(NUM_CTCOR_EMPRG))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(150,
                                                                                     lay.LAY_NUM_CTCOR_EMPRG));
            }

            if (NumeroZerado(NUM_CARGO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(159,
                                                                                     lay.LAY_NUM_CARGO));
            }
            else if (!NumeroValido(NUM_CARGO))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_INVALIDO(160, lay.LAY_NUM_CARGO, NUM_CARGO));
            }
            else
            {
                CargosBLL CargosBLL = new CargosBLL();
                int iNUM_CARGO = 0;
                int.TryParse(NUM_CARGO, out iNUM_CARGO);
                CARGOS Cargo = CargosBLL.GetCargo(iNUM_CARGO);
                if (Cargo == null)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(163,
                                                                       "Cód. do Cargo não localizado.  CARGO = " + iNUM_CARGO.ToString(),
                                                                       lay.LAY_NUM_CARGO));
                }
            }

            //if (String.IsNullOrEmpty(TIP_CTCOR_EMPRG))
            //{
            //    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alert_CAMPO_OBRIGATORIO(151,
            //                                                                         lay.LAY_TIP_CTCOR_EMPRG));
            //}

            short sCOD_BANCO = 0;
            int iCOD_AGBCO = 0;

            short.TryParse(COD_BANCO, out sCOD_BANCO);
            int.TryParse(COD_AGBCO, out iCOD_AGBCO);

            if (sCOD_BANCO > 0 && iCOD_AGBCO > 0 && (ParticBLL.GetAgencia(sCOD_BANCO, iCOD_AGBCO) == null))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(131,
                                                                   "Banco/Agência não localizado. BANCO = " + sCOD_BANCO.ToString() + " AGÊNCIA = " + iCOD_AGBCO.ToString(),
                                                                   lay.LAY_COD_AGBCO));
            }

            if (DataEmBranco(DT_DESLIGAMENTO))
            {
                int iMOTIVO_DESLIGAMENTO;
                int.TryParse(MOTIVO_DESLIGAMENTO, out iMOTIVO_DESLIGAMENTO);
                if (iMOTIVO_DESLIGAMENTO > 0)
                {
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(153,
                                                            "Campo inválido. Foi atribuido um motivo de desligamento sem a Dt. Desligamento preenchida.",
                                                            lay.LAY_COD_MTDSL));
                }
            }
            else
            {
                //int iMOTIVO_DESLIGAMENTO;
                //int.TryParse(MOTIVO_DESLIGAMENTO, out iMOTIVO_DESLIGAMENTO);
                //if (iMOTIVO_DESLIGAMENTO == 0)
                //{
                //    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(154,
                //                                            "Campo inválido. Foi atribuida uma Dt. Desligamento sem o motivo de desligamento preenchido.",
                //                                            lay.LAY_DAT_DESLG_EMPRG));
                //}
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(155,
                                                           "Campo inválido. O campo Dt. Desligamento não pode ser preenchido/enviado via arquivo cadastral.",
                                                           lay.LAY_DAT_DESLG_EMPRG));
            }


            if (COD_EMPRS == 66) // CPFL - Piratininga: De Para de matriculas 
            {
                ATT_CHARGER_DEPARA DaPara_66 = GetEmpregadoMatricula_DE_PARA(COD_EMPRS, NUM_RGTRO_EMPRG.ToString());
                if (DaPara_66 == null)
                {
                    //System.Diagnostics.Debug.WriteLine(NUM_RGTRO_EMPRG.ToString() + " -> " + DaPara.CONTEUDOPARA);
                    //NUM_RGTRO_EMPRG = int.Parse(DaPara_66.CONTEUDOPARA);
                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(161,
                                                            "Empregado não localizado no DE-PARA de Matrículas CPFL-Piratininga-066. MATRICULA = " + NUM_RGTRO_EMPRG.ToString(),
                                                            lay.LAY_NUM_RGTRO_EMPRG));
                }
            }

            if (NumeroZerado(CPF))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_OBRIGATORIO(132, lay.LAY_NUM_CPF_EMPRG));
            }
            else if (!NumeroValido(CPF))
            {
                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_crit_CAMPO_INVALIDO(133, lay.LAY_NUM_CPF_EMPRG, CPF));
            }
            else
            {
                //EMPREGADO Empregado = ParticBLL.GetEmpregado(null, null, Convert.ToInt64(CPF), null);
                PARTICIPANTE Empregado = ParticBLL.GetParticipanteBy(Convert.ToInt64(CPF), null, false, false);

                if (Empregado != null)
                {

                    if (Empregado.DAT_DESLG_EMPRG == null)
                    {
                        //
                        // Erros especificos da tabela EMPREGADO
                        //
                        //if (Empregado.ORIGEM_TABELA == "E")
                        //{
                        //    if (Empregado.COD_EMPRS != COD_EMPRS || Empregado.NUM_RGTRO_EMPRG != NUM_RGTRO_EMPRG)
                        //    {
                        //        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(134, "Campo inválido. CPF já existe para outra empresa/matrícula; Valor=" + CPF, lay.LAY_NUM_CPF_EMPRG));
                        //    }
                        //}

                        //
                        // Erros especificos da tabela REPRES_UNIAO_FSS
                        //
                        //if (Empregado.ORIGEM_TABELA == "R")
                        //{
                        //    if (Empregado.COD_EMPRS != COD_EMPRS || Empregado.NUM_RGTRO_EMPRG != NUM_RGTRO_EMPRG)
                        //    {
                        //        if (DateTime.Parse(Empregado.DAT_NASCM_EMPRG.ToString()).ToString("yyyyMMdd") != DT_NASCIMENTO)
                        //        {
                        //            ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(135, "Campo inválido. CPF já existe para outra empresa/matrícula; Valor=" + CPF, lay.LAY_NUM_CPF_EMPRG));
                        //        }
                        //    }
                        //}


                        //
                        //  -> Validações para empregados que NÃO mudaram de empresa (Guilherme Provenzano - 20/12/2017):
                        //

                        if (Empregado.COD_EMPRS == COD_EMPRS && Empregado.NUM_RGTRO_EMPRG == NUM_RGTRO_EMPRG)
                        {
                            if (Empregado.NOM_EMPRG != NOME_EMPREGADO)
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(136, "Alteração não permitida. Campo [NOME EMPREGADO] alterado: De=" + Empregado.NOM_EMPRG + " Para=" + NOME_EMPREGADO, lay.LAY_NOM_EMPRG));
                            }

                            if (Empregado.COD_SEXO_EMPRG != SEXO)
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(137, "Alteração não permitida. Campo [Sexo] alterado: De=" + Empregado.COD_SEXO_EMPRG + " Para=" + SEXO, lay.LAY_NOM_EMPRG));
                            }

                            if (Util.Date2String(Empregado.DAT_NASCM_EMPRG, "yyyyMMdd") != DT_NASCIMENTO) //19920324
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(138, "Alteração não permitida. Dt. Nascimento alterada: De=" + Util.Date2String(Empregado.DAT_NASCM_EMPRG, "yyyyMMdd") + " Para=" + DT_NASCIMENTO, lay.LAY_DAT_NASCM_EMPRG));
                            }

                            if (Util.Date2String(Empregado.DAT_ADMSS_EMPRG, "yyyyMMdd") != DT_ADMISSAO) //19920324
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(139,
                                                                       "Alteração não permitida. Dt. Admissão alterada: De=" + Util.Date2String(Empregado.DAT_ADMSS_EMPRG, "yyyyMMdd") + " Para=" + DT_ADMISSAO,
                                                                       lay.LAY_DAT_ADMSS_EMPRG));
                            }

                            //if (!DataEmBranco(DT_DESLIGAMENTO))
                            //{

                            //    if (Util.Date2String(Empregado.DAT_DESLG_EMPRG, "yyyyMMdd") != DT_DESLIGAMENTO)                        
                            //    {
                            //        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(140,
                            //                                               "Alteração não permitida. Dt. Desligamento alterada: De=" + Util.Date2String(Empregado.DAT_DESLG_EMPRG, "yyyyMMdd") + " Para=" + DT_DESLIGAMENTO,
                            //                                               lay.LAY_DAT_DESLG_EMPRG));
                            //    }

                            //    if (ParticBLL.GetMotivoDesligamento(Convert.ToInt16(MOTIVO_DESLIGAMENTO))==null)
                            //    {
                            //        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(146,
                            //                   "Cód. do motivo de desligamento não localizado. COD_MTDSL = " + MOTIVO_DESLIGAMENTO,
                            //                   lay.LAY_COD_MTDSL));
                            //    }

                            //    if (Empregado.DAT_DESLG_EMPRG == null &&  DT_DESLIGAMENTO != null)
                            //    {
                            //        //FinanceiroBLL FinBLL = new FinanceiroBLL();
                            //        //TB_PARTICIP_PLANO aSaude = FinBLL.GetPlanoSaudePartic_ATIVO(COD_EMPRS, NUM_RGTRO_EMPRG.ToString());
                            //        bool Tem_Plano_Saude = FinBLL.Tem_PLANO_SAUDE_Ativo(COD_EMPRS, NUM_RGTRO_EMPRG.ToString());

                            //        if (Tem_Plano_Saude)
                            //        {
                            //            ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(149,
                            //                                                   "Alteração não permitida. Não é possível desligar empregado com plano de saúde ativo.",
                            //                                                   lay.LAY_DAT_DESLG_EMPRG));
                            //        }
                            //    }

                            //}

                            int iNUM_CELULAR;
                            int.TryParse(NUM_CELULAR, out iNUM_CELULAR);

                            if (Empregado.NUM_CELUL_EMPRG != null && Empregado.NUM_CELUL_EMPRG != iNUM_CELULAR && (iNUM_CELULAR == 0)) //19920324
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(141, "Alteração não permitida. Número Celular alterado: De=" + Empregado.NUM_CELUL_EMPRG.ToString() + " Para=NULO", lay.LAY_NUM_CELUL_EMPRG));
                            }

                            if (iNUM_CELULAR > 0 && iNUM_CELULAR.ToString().Length < 8) //19920324
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(142, "Campo inválido. O campo Núm. Celular deve conter no minimo 8 digitos Valor=" + iNUM_CELULAR.ToString(), lay.LAY_NUM_CELUL_EMPRG));
                            }

                            if (Empregado.COD_EMAIL_EMPRG != EMAIL && String.IsNullOrEmpty(EMAIL)) //19920324
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(143, "Alteração não permitida. E-Mail alterado: De=" + Empregado.COD_EMAIL_EMPRG + " Para=NULO", lay.LAY_COD_EMAIL_EMPRG));
                            }

                            if ((Empregado.COD_BANCO ?? 0) != sCOD_BANCO)
                            {
                                //List<CAD_DADOS_BANC> DADOS_BANC = BateBLL.GetDataBanc(COD_EMPRS, NUM_RGTRO_EMPRG);
                                if (ParticBLL.AlterouDadosBancarios(COD_EMPRS, NUM_RGTRO_EMPRG))
                                {
                                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(147,
                                                                                       "Banco não pode ser alterado devido a atualização recente efetuada no Portal Funcesp. BANCO = " + COD_BANCO,
                                                                                       lay.LAY_COD_BANCO));
                                }
                            }

                            if ((Empregado.COD_AGBCO ?? 0) != iCOD_AGBCO ||
                                (Empregado.NUM_CTCOR_EMPRG ?? "").Trim() != NUM_CTCOR_EMPRG ||
                                (Empregado.TIP_CTCOR_EMPRG ?? "").Trim() != TIP_CTCOR_EMPRG)
                            {
                                // List<CAD_DADOS_BANC> DADOS_BANC = BateBLL.GetDataBanc(COD_EMPRS, NUM_RGTRO_EMPRG);
                                if (ParticBLL.AlterouDadosBancarios(COD_EMPRS, NUM_RGTRO_EMPRG))
                                {
                                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(148,
                                                                                       "Agência, Conta bancária ou Tipo não podem ser alterados devido a atualização recente efetuada no Portal Funcesp. AGÊNCIA = " + COD_AGBCO + " CONTA = " + NUM_CTCOR_EMPRG + " TIPO = " + TIP_CTCOR_EMPRG,
                                                                                       lay.LAY_COD_AGBCO));
                                }
                            }

                            //short sCOD_BANCO = 0;
                            int iCOD_CEP_EMPRG = 0;
                            int iNUM_ENDER_EMPRG = 0;
                            int iNUM_TELEF_EMPRG = 0;
                            int iNUM_CELUL_EMPRG = 0;

                            //short.TryParse(COD_BANCO, out sCOD_BANCO);
                            int.TryParse(COD_CEP_EMPRG, out iCOD_CEP_EMPRG);
                            int.TryParse(NUM_ENDER_EMPRG, out iNUM_ENDER_EMPRG);
                            int.TryParse(NUM_TELRES_EMPRG, out iNUM_TELEF_EMPRG);
                            int.TryParse(NUM_CELULAR, out iNUM_CELUL_EMPRG);

                            string CAMPOS_ALTERADOS = "";

                            if ((Empregado.COD_CEP_EMPRG ?? 0) != iCOD_CEP_EMPRG)
                            {
                                CAMPOS_ALTERADOS += ",CEP";
                            }

                            if ((Empregado.DCR_ENDER_EMPRG ?? "").Trim() != (DCR_ENDER_EMPRG ?? "").Trim())
                            {
                                CAMPOS_ALTERADOS += ",ENDEREÇO";
                            }

                            if ((Empregado.NUM_ENDER_EMPRG ?? 0) != iNUM_ENDER_EMPRG)
                            {
                                CAMPOS_ALTERADOS += ",NÚM.";
                            }

                            if ((Empregado.DCR_COMPL_EMPRG ?? "").Trim() != (DCR_COMPL_EMPRG ?? "").Trim())
                            {
                                CAMPOS_ALTERADOS += ",COMPL.";
                            }

                            if ((Empregado.NOM_BAIRRO_EMPRG ?? "").Trim() != (NOM_BAIRRO_EMPRG ?? "").Trim())
                            {
                                CAMPOS_ALTERADOS += ",BAIRRO";
                            }

                            if ((Empregado.NOM_CIDRS_EMPRG ?? "").Trim() != (NOM_CIDRS_EMPRG ?? "").Trim())
                            {
                                CAMPOS_ALTERADOS += ",CIDADE";
                            }

                            if ((Empregado.COD_UNDFD_EMPRG ?? "").Trim() != (COD_UNDFD_EMPRG ?? "").Trim())
                            {
                                CAMPOS_ALTERADOS += ",ESTADO";
                            }

                            if ((Empregado.NUM_TELEF_EMPRG ?? 0) != iNUM_TELEF_EMPRG)
                            {
                                CAMPOS_ALTERADOS += ",TEL.";
                            }

                            if ((Empregado.NUM_CELUL_EMPRG ?? 0) != iNUM_CELUL_EMPRG)
                            {
                                CAMPOS_ALTERADOS += ",CELULAR";
                            }

                            if ((Empregado.COD_EMAIL_EMPRG ?? "").Trim() != (EMAIL ?? "").Trim())
                            {
                                CAMPOS_ALTERADOS += ",E-MAIL";
                            }

                            if (!String.IsNullOrEmpty(CAMPOS_ALTERADOS))
                            {
                                CAMPOS_ALTERADOS = CAMPOS_ALTERADOS.Substring(1, CAMPOS_ALTERADOS.Length - 1);
                                //List<CAD_DADOS_MOV_CAD> DADOS_CADAS = BateBLL.GetDataCad(COD_EMPRS, NUM_RGTRO_EMPRG);                        
                                if (ParticBLL.AlterouDadosCadastrais(COD_EMPRS, NUM_RGTRO_EMPRG))
                                {
                                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(149,
                                                                                       "Dados cadastrais não podem ser alterados devido a atualização recente efetuada no Portal Funcesp. (" + CAMPOS_ALTERADOS + ")",
                                                                                       lay.LAY_DCR_ENDER_EMPRG));
                                }
                            }

                            short sCOD_ESTCV_EMPRG = 0;
                            short.TryParse(COD_ESTCV_EMPRG, out sCOD_ESTCV_EMPRG);

                            if (Empregado.COD_ESTCV_EMPRG != null && Empregado.COD_ESTCV_EMPRG != sCOD_ESTCV_EMPRG)
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(157,
                                                                                  "Alteração não permitida. Estado civil alterado: De=" + Empregado.COD_ESTCV_EMPRG.ToString() + " Para=" + COD_ESTCV_EMPRG.ToString(),
                                                                                  lay.LAY_COD_ESTCV_EMPRG));
                            }
                            else if (sCOD_ESTCV_EMPRG < 1 || sCOD_ESTCV_EMPRG > 8)
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(158,
                                                                                  "Valor inválido. Código Estado civil deve estar entre 1 e 8. Valor = " + sCOD_ESTCV_EMPRG.ToString(),
                                                                                  lay.LAY_COD_ESTCV_EMPRG));
                            }

                            // Regra especifica para 43 - CTEEP:
                            // Bloquear alteração de ag. / conta corrente para participantes com beneficio requerido:
                            // COD_TPPCP = 05 (Assistido)   &     COD_SITPAR = 09 (Benefício requerido)
                            if (COD_EMPRS == 43) 
                            {
                                ADESAO_PLANO_PARTIC_FSS situacao_part = ParticBLL.GetSituacaoPlanoPrevidencia(Empregado.NUM_MATR_PARTF ?? 0, null);
                                if (situacao_part != null && situacao_part.COD_TPPCP == 5 && situacao_part.COD_SITPAR == 9)
                                {
                                    ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(162,
                                                                                        "Banco, agência, Conta bancária e Tipo não podem ser alterados devido a situação do participante na Funcesp:  Assistido c/ Benefício requerido",
                                                                                        lay.LAY_COD_AGBCO));
                                }
                            }

                        }
                    }
                    //else (Empregado.DAT_DESLG_EMPRG != null):
                    else if (Empregado.COD_EMPRS == COD_EMPRS && Empregado.NUM_RGTRO_EMPRG == NUM_RGTRO_EMPRG)
                    {
                        ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_critica(156,
                                                                "Alteração não permitida. Alteração cadastral não permitida para funcionário inativo na base de dados da Funcesp. Dt. Desligamento = " + Util.Date2String(Empregado.DAT_DESLG_EMPRG, "dd/MM/yyyy"),
                                                                lay.LAY_DAT_DESLG_EMPRG));
                    }
                }
                else
                {

                    Empregado = ParticBLL.GetParticipanteBy(COD_EMPRS, NUM_RGTRO_EMPRG, 0, false);
                    //
                    // Erros especificos da tabela EMPREGADO
                    //
                    if (Empregado != null)
                    {
                        if (Empregado.ORIGEM_TABELA == "E")
                        {
                            if (Empregado.NUM_CPF_EMPRG.ToString() != CPF)
                            {
                                ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(144, "Alteração não permitida. CPF alterado: De=" + Empregado.NUM_CPF_EMPRG + " Para=" + CPF, lay.LAY_NUM_CPF_EMPRG));
                            }
                        }

                        //
                        // Erros especificos da tabela REPRES_UNIAO_FSS
                        //
                        //if (Empregado.ORIGEM_TABELA == "R")
                        //{
                        //    if (Empregado.NUM_CPF_EMPRG.ToString() != CPF)
                        //    {
                        //        if (DateTime.Parse(Empregado.DAT_NASCM_EMPRG.ToString()).ToString("yyyyMMdd") != DT_NASCIMENTO)
                        //        {
                        //            ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new_alerta(145, "Alteração não permitida. CPF alterado: De=" + Empregado.NUM_CPF_EMPRG + " Para=" + CPF, lay.LAY_NUM_CPF_EMPRG));
                        //        }
                        //    }
                        //}
                    }
                }

                //REPRES_UNIAO_FSS Repres = buscaBLL.GetRepresentante(null, null, Convert.ToInt64(CPF), null);

                //if (Repres != null)
                //{
                //    if (Repres.COD_EMPRS != COD_EMPRS || Repres.NUM_RGTRO_EMPRG != NUM_RGTRO_EMPRG)
                //    {
                //        if (DateTime.Parse(Repres.DAT_NASCM_REPRES.ToString()).ToString("yyyyMMdd") != DT_NASCIMENTO)
                //        {
                //            ALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                //            {
                //                COD_CRITICA = 221,
                //                DCR_CRITICA = "Campo inválido. CPF já existe como representante; Valor=" + CPF,
                //                NOM_CAMPO = "CPF EMPREGADO",
                //                NUM_POSICAO = 396
                //            });
                //        }
                //    }
                //}
            }
        }

        internal ATT_CHARGER_DEPARA GetEmpregadoMatricula_DE_PARA(short pCOD_EMPRS, string pNUM_ORGAO_DE = null)
        {
            Resultado res = new Resultado();
            if (cache_DePara_EmpregMat_table == null || cache_DePara_EmpregMat_emprs != pCOD_EMPRS)
            {
                ParticipanteBLL ParticBLL = new ParticipanteBLL();
                cache_DePara_EmpregMat_table = ParticBLL.GetEmpregado_DE_PARA2(pCOD_EMPRS);
                cache_DePara_EmpregMat_emprs = pCOD_EMPRS;
            }

            return cache_DePara_EmpregMat_table.FirstOrDefault(o => o.CONTEUDODE == pNUM_ORGAO_DE);

        }

        internal ATT_CHARGER_DEPARA GetOrgaoLotacao_DE_PARA(short pCOD_EMPRS, string pNUM_ORGAO_DE = null)
        {
            Resultado res = new Resultado();
            if (cache_DePara_OrgaoLotacao_table == null || cache_DePara_OrgaoLotacao_emprs != pCOD_EMPRS)
            {
                cache_DePara_OrgaoLotacao_table = base.Carrega_cache_DePara_OrgaoLotacao_table(pCOD_EMPRS);
                cache_DePara_OrgaoLotacao_emprs = pCOD_EMPRS;
            }

            return cache_DePara_OrgaoLotacao_table.FirstOrDefault(o => o.CONTEUDODE == pNUM_ORGAO_DE);

        }

        #endregion

        #region .: Métodos :.

        internal bool NumeroZerado(string numerico)
        {
            if (numerico == null || 
                numerico!= null &&
                numerico == "0".PadLeft(numerico.Length, '0'))
            {
                return true;
            } else {
                return false;
            }
        }
        internal bool NumeroValido(string numerico)
        {
            long lNum;
            return long.TryParse(numerico, out lNum);
        }
        internal bool MesValido(string mes)
        {
            short sMes;
            if (short.TryParse(mes, out sMes))
            {
                return (sMes >= 1 && sMes <= 12);
            }
            else
            {
                return false;
            }
        }
        internal bool AnoValido(string ano)
        {
            int sAno;
            if (int.TryParse(ano, out sAno))
            {
                return (sAno >= 2000 && sAno <= 2050);
            }
            else
            {
                return false;
            }
        }
        internal bool DataEmBranco(string data_YYYYMMDD)
        {
            long lData;
            if (long.TryParse(data_YYYYMMDD, out lData))
            {
                return (lData == 0);
            }
            else
            {
                return true;
            }
        }
        internal bool DataValida(string data_YYYYMMDD)
        {
            DateTime dData;
            data_YYYYMMDD = data_YYYYMMDD.PadLeft(8, '0');
            return
                DateTime.TryParse(data_YYYYMMDD.Substring(6, 2) + "/" + data_YYYYMMDD.Substring(4, 2) + "/" + data_YYYYMMDD.Substring(0, 4), out dData);
        }

        internal string DataValidaFormatada(string data_YYYYMMDD)
        {
            if (DataValida(data_YYYYMMDD))
            {
                return data_YYYYMMDD.Substring(6, 2) + "/" + data_YYYYMMDD.Substring(4, 2) + "/" + data_YYYYMMDD.Substring(0, 4);
            }
            return "";
        }

        internal PRE_TBL_ARQ_PATROCINA_CRITICA new_crit_CAMPO_OBRIGATORIO(short COD_CRITICA, LAY_CAMPO CAMPO)
        {
            string dcrCritica = "Campo obrigatório [" + CAMPO.nome_amigavel + "]";
            return (new_critica(COD_CRITICA, dcrCritica, CAMPO));
        }

        internal PRE_TBL_ARQ_PATROCINA_CRITICA new_alert_CAMPO_OBRIGATORIO(short COD_CRITICA, LAY_CAMPO CAMPO)
        {
            string dcrCritica = "Campo obrigatório [" + CAMPO.nome_amigavel + "]";
            return (new_alerta(COD_CRITICA, dcrCritica, CAMPO));
        }

        internal PRE_TBL_ARQ_PATROCINA_CRITICA new_crit_CAMPO_INVALIDO(short COD_CRITICA, LAY_CAMPO CAMPO, string Valor)
        {
            string dcrCritica = "Campo inválido [" + CAMPO.nome_amigavel + "] Valor = " + Valor;
            return (new_critica(COD_CRITICA, dcrCritica, CAMPO));
        }

        internal PRE_TBL_ARQ_PATROCINA_CRITICA new_alert_CAMPO_INVALIDO(short COD_CRITICA, LAY_CAMPO CAMPO, string Valor)
        {
            string dcrCritica = "Campo inválido [" + CAMPO.nome_amigavel + "] Valor = " + Valor;
            return (new_alerta(COD_CRITICA, dcrCritica, CAMPO));
        }

        internal PRE_TBL_ARQ_PATROCINA_CRITICA new_critica(short COD_CRITICA, string DCR_CRITICA, LAY_CAMPO CAMPO, string Ref_1 = null, string Ref_2 = null, string Ref_3 = null)
        {
            return (new PRE_TBL_ARQ_PATROCINA_CRITICA
                {
                    COD_CRITICA = COD_CRITICA,
                    DCR_CRITICA = DCR_CRITICA,
                    NOM_CAMPO = CAMPO.nome,
                    NUM_POSICAO = CAMPO.pos,
                    REF_1 = Ref_1,
                    REF_2 = Ref_2,
                    REF_3 = Ref_3
                });
        }
        internal PRE_TBL_ARQ_PATROCINA_CRITICA new_critica(short COD_CRITICA, string DCR_CRITICA)
        {
            return (new PRE_TBL_ARQ_PATROCINA_CRITICA
            {
                COD_CRITICA = COD_CRITICA,
                DCR_CRITICA = DCR_CRITICA,
                NOM_CAMPO = "",
                NUM_POSICAO = null
            });
        }
        internal PRE_TBL_ARQ_PATROCINA_CRITICA new_alerta(short COD_CRITICA, string DCR_CRITICA, LAY_CAMPO CAMPO, string Ref_1 = null, string Ref_2 = null, string Ref_3 = null)
        {
            return (new PRE_TBL_ARQ_PATROCINA_CRITICA
                {
                    TIP_CRITICA = 2,
                    COD_CRITICA = COD_CRITICA,
                    DCR_CRITICA = DCR_CRITICA,
                    NOM_CAMPO = CAMPO.nome,
                    NUM_POSICAO = CAMPO.pos,
                    REF_1 = Ref_1,
                    REF_2 = Ref_2,
                    REF_3 = Ref_3
                });
        }

        #endregion

    }
}
