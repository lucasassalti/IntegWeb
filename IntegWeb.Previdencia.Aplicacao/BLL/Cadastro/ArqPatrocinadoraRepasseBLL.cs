using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Cadastro
{
    public class ArqPatrocinadoraRepasseBLL : ArqPatrocinadoraRepasseDAL
    {

        ArqPatrocinaCargaBLL ArqPatCargaBll = new ArqPatrocinaCargaBLL();

        public Resultado SaveData(PRE_TBL_ARQ_ENV_REPASSE_LINHA_View obj, PRE_TBL_ARQ_ENV_REPASSE_View objPai)
        {
            Resultado retorno = new Resultado(true);

            retorno = base.SaveDataRepasse(objPai,1);
            if (retorno.Ok && obj != null)
            {
                if (obj.COD_ARQ_ENV_REPASSE == 0)
                {
                    obj.COD_ARQ_ENV_REPASSE = base.getCodArquivoRepasse();
                    retorno = base.SaveDataLinha(obj);
                }
                else
                {
                    retorno = base.SaveDataLinha(obj);
                }
            }
            return retorno;
        }

        public Resultado Importar(PRE_TBL_ARQ_ENV_REPASSE_View obj)
        {
            Resultado retorno = new Resultado();

            retorno = base.SaveDataRepasse(obj,2);
            obj.COD_ARQ_ENV_REPASSE = Convert.ToInt32(retorno.CodigoCriado);

            if (retorno.Ok && obj.COD_ARQ_ENV_REPASSE != null)
            {
                retorno = new Resultado();
                retorno.Erro("Erro ao importar lançamentos");

                string grupo_portal = base.GetGrupoPortal(obj.COD_GRUPO_EMPRS);
                IEnumerable<FCESP_GRUPO_EMP_View> IEnum = m_DbContext.Database.SqlQuery<FCESP_GRUPO_EMP_View>("select EMPRESA from OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD " +
                                                                                                              "where GRUPO LIKE " + "'%" + grupo_portal + "' and EMPRESA is not null");
                List<Int32?> listEmpresas = IEnum.Select(l => l.EMPRESA).ToList();

                for (int i = 0; i < listEmpresas.Count; i++)
                {
                    retorno = base.Importar(obj, listEmpresas[i].Value);
                }

                if (retorno.CodigoCriado == 0)
                {
                    base.ExcluirRepasseVazio(obj);
                }
                else
                {
                    // Critica para os casos onde a verba do lançamento vem nulo.:
                    string strMensagem = "";
                    foreach (PRE_TBL_ARQ_ENV_REPASSE_LINHA_View linha in base.GetWhereDetalhes(obj.COD_ARQ_ENV_REPASSE, null, null, null, "0").ToList())
                    {
                        if (linha.COD_VERBA == null)
                        {
                            strMensagem += "VERBA NÃO LOCALIZADA LANÇ.: Empresa: " + linha.COD_EMPRS + "  Matrícula: " + linha.NUM_RGTRO_EMPRG;
                            if (linha.VLR_DESCONTO>0)
                            {
                                strMensagem += "  Valor Desconto: " + linha.VLR_DESCONTO;
                            } else {
                                strMensagem += "  Valor Percentual: " + linha.VLR_PERCENTUAL;
                            }
                            strMensagem += "\\n";
                        }
                    }
                    if (!String.IsNullOrEmpty(strMensagem))
                    {
                        retorno.Erro("Descontos importados com observações:<br><br>" + strMensagem);
                    }

                }
            }
            else
            {
                retorno.Erro("Erro ao importar lançamentos");
            }
            return retorno;
        }

        public Resultado Importar(DataTable dtCSV, 
                                  short pCOD_EMPRS,
                                  string pDCR_ARQ_ENV_REPASSE,
                                  short pANO_REF,
                                  short pMES_REF,
                                  short pCOD_GRUPO_EMPRS,
                                  short pCOD_ARQ_AREA,
                                  string LOG_INCLUSAO,
                                  int pCOD_ARQ_ENV_REPASSE = 0)
        {
            Resultado retorno = new Resultado();

            LAY_ENVIO lENVIO = new LAY_ENVIO(pCOD_EMPRS);

            PRE_TBL_ARQ_ENV_REPASSE_View ARQ_ENV_REPASSE = new PRE_TBL_ARQ_ENV_REPASSE_View();
            //ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();

            ARQ_ENV_REPASSE.COD_ARQ_ENV_REPASSE = pCOD_ARQ_ENV_REPASSE;
            ARQ_ENV_REPASSE.DCR_ARQ_ENV_REPASSE = pDCR_ARQ_ENV_REPASSE;
            ARQ_ENV_REPASSE.ANO_REF = pANO_REF;
            ARQ_ENV_REPASSE.MES_REF = pMES_REF;
            ARQ_ENV_REPASSE.COD_GRUPO_EMPRS = pCOD_GRUPO_EMPRS;
            ARQ_ENV_REPASSE.COD_ARQ_AREA = pCOD_ARQ_AREA;
            ARQ_ENV_REPASSE.DTH_INCLUSAO = System.DateTime.Now;
            ARQ_ENV_REPASSE.LOG_INCLUSAO = LOG_INCLUSAO;

            if (pCOD_ARQ_ENV_REPASSE == 0)
            {
                retorno = base.SaveDataRepasse(ARQ_ENV_REPASSE, 1);
            }
            else
            {
                retorno.Sucesso("Repasse aproveitado", pCOD_ARQ_ENV_REPASSE);
            }

            if (retorno.Ok)
            {

                foreach (DataRow dw in dtCSV.Rows)
                {
                    int pNUM_RGTRO_EMPRG = 0;
                    int? pCOD_VERBA = 0;
                    string pCOD_VERBA_PATROCINA = "";
                    decimal pVLR_PERCENTUAL = 0;
                    decimal pVLR_DESCONTO = 0;

                    string[] reg = dw[0].ToString().Split(';');

                    if (reg.Length > 1) // Arquivo CSV
                    {
                        pNUM_RGTRO_EMPRG = LAYOUT_UTIL.csv_GetInt(reg, lENVIO.LAY_NUM_RGTRO_EMPRG);
                        //pCOD_VERBA = LAYOUT_UTIL.csv_GetInt(reg, lENVIO.LAY_COD_VERBA);
                        pCOD_VERBA_PATROCINA = LAYOUT_UTIL.csv_GetStringNull(reg, lENVIO.LAY_COD_VERBA) + LAYOUT_UTIL.csv_GetStringNull(reg, lENVIO.LAY_COD_VERBA_2);
                        pVLR_PERCENTUAL = LAYOUT_UTIL.csv_GetDecimal(reg, lENVIO.LAY_VLR_PERCENTUAL);
                        pVLR_DESCONTO = LAYOUT_UTIL.csv_GetDecimal(reg, lENVIO.LAY_VLR_DESCONTO);
                    }
                    else // Arquivo tabulado
                    {
                        reg[0] = " " + reg[0];
                        pNUM_RGTRO_EMPRG = LAYOUT_UTIL.GetInt(reg[0], lENVIO.LAY_NUM_RGTRO_EMPRG);
                        //pCOD_VERBA = LAYOUT_UTIL.GetIntNull(reg[0], lENVIO.LAY_COD_VERBA);
                        pCOD_VERBA_PATROCINA = LAYOUT_UTIL.GetStringNull(reg[0], lENVIO.LAY_COD_VERBA) + LAYOUT_UTIL.GetStringNull(reg[0], lENVIO.LAY_COD_VERBA_2);
                        pVLR_PERCENTUAL = LAYOUT_UTIL.csv_GetDecimal(reg[0], lENVIO.LAY_VLR_PERCENTUAL);
                        pVLR_DESCONTO = LAYOUT_UTIL.csv_GetDecimal(reg[0], lENVIO.LAY_VLR_DESCONTO);
                    }
                    ArqPatrocinaDemonstrativoBLL DemonsBLL = new ArqPatrocinaDemonstrativoBLL();

                    if (pCOD_VERBA_PATROCINA != null)
                    {
                        PRE_TBL_ARQ_PAT_VERBA PAT_VERBA = DemonsBLL.GetVerbaPatrocinadora(pCOD_EMPRS, null, pCOD_VERBA_PATROCINA);
                        if (PAT_VERBA != null)
                        {
                            pCOD_VERBA = PAT_VERBA.COD_VERBA;
                        }
                    }

                    PRE_TBL_ARQ_ENV_REPASSE_LINHA_View ARQ_ENV_REPASSE_LINHA = new PRE_TBL_ARQ_ENV_REPASSE_LINHA_View();

                    ARQ_ENV_REPASSE_LINHA.COD_ARQ_ENV_REPASSE = int.Parse(retorno.CodigoCriado.ToString()); // ARQ_ENV_REPASSE.COD_ARQ_ENV_REPASSE;
                    ARQ_ENV_REPASSE_LINHA.COD_EMPRS = pCOD_EMPRS;
                    ARQ_ENV_REPASSE_LINHA.NUM_RGTRO_EMPRG = pNUM_RGTRO_EMPRG;
                    ARQ_ENV_REPASSE_LINHA.COD_VERBA = pCOD_VERBA;
                    ARQ_ENV_REPASSE_LINHA.COD_VERBA_PATROCINA = pCOD_VERBA_PATROCINA;
                    ARQ_ENV_REPASSE_LINHA.VLR_PERCENTUAL = pVLR_PERCENTUAL;
                    ARQ_ENV_REPASSE_LINHA.VLR_DESCONTO = pVLR_DESCONTO;
                    ARQ_ENV_REPASSE_LINHA.DTH_INCLUSAO = ARQ_ENV_REPASSE.DTH_INCLUSAO;
                    ARQ_ENV_REPASSE_LINHA.LOG_INCLUSAO = ARQ_ENV_REPASSE.LOG_INCLUSAO;
                    ARQ_ENV_REPASSE_LINHA.DTH_EXCLUSAO = null;
                    ARQ_ENV_REPASSE_LINHA.LOG_EXCLUSAO = null;

                    SaveDataLinha(ARQ_ENV_REPASSE_LINHA);

                    //LAY_COD_VERBA
                    //LAY_SEPARADOR
                    //LAY_COD_EMPRS_ADP
                    //LAY_NUM_RGTRO_EMPRG
                    //LAY_VLR_PERCENTUAL
                    //LAY_VLR_LANCAMENTO
                }
            }

            return retorno;
        }

        public int GetCountInsert()
        {
            return base.GetCountInsert(base.GetMaxPkRepasse());
        }

        public void GeraArquivoRepasse(string caminho_arquivo, short? COD_GRUPO_EMPRS, short? COD_EMPRS, short? ANO_REF, short? MES_REF, short? COD_ARQ_AREA, DateTime _current_date_time, string EXT_ARQUIVO = ".txt")
        {
            string strConteudo = "";
            List<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> lstLinhas = base.GetDataRepasseLinha(COD_GRUPO_EMPRS, COD_EMPRS, ANO_REF, MES_REF, COD_ARQ_AREA);
            int TotalLinhas = lstLinhas.Count();
            int count = 1;
            foreach (PRE_TBL_ARQ_ENV_REPASSE_LINHA_View linha_Repaase in lstLinhas)
            {
                strConteudo += Monta_Layout(linha_Repaase, COD_GRUPO_EMPRS, ANO_REF, MES_REF, _current_date_time, EXT_ARQUIVO, (count == TotalLinhas));
                count++;
            }
            if (!String.IsNullOrEmpty(strConteudo))
            {
                Util.String2File(strConteudo, caminho_arquivo, false);
            }
        }

        private string Monta_Layout(PRE_TBL_ARQ_ENV_REPASSE_LINHA_View linha_Repaase, short? COD_GRUPO_EMPRS, short? ANO_REF, short? MES_REF, DateTime _current_date_time, string EXT_ARQUIVO = ".txt", bool UltLinha = false)
        {
            string sLinha = "";

            short P_COD_EMPR = linha_Repaase.COD_EMPRS ?? 0;
            int P_NUM_RGTRO_EMPRG = linha_Repaase.NUM_RGTRO_EMPRG ?? 0;
            decimal P_PERCENTUAL = linha_Repaase.VLR_PERCENTUAL ?? 0;
            decimal P_VALORDESCONTO = linha_Repaase.VLR_DESCONTO ?? 0;
            short P_MES_REF = MES_REF ?? 0;
            short P_ANO_REF = ANO_REF ?? 0;
            if (P_MES_REF > 12)
            {
                P_MES_REF = 12;
            }
            DateTime ULT_DIA_MES = Util.UltimoDiaMes(new DateTime(P_ANO_REF, P_MES_REF, 1));

            switch (COD_GRUPO_EMPRS)
            {
                case 1: //CESP
                    //Print #2, "31" & "000000000" & Format(P_NUM_RGTRO_EMPRG, "00000") & Format(P_VDPATROCINADORA, "000") & Space(6) & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "00000000") & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), Format(Int(P_VALORDESCONTO), "00000000") & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2)) & Space(45)
                    if (P_VALORDESCONTO > 0)
                    {
                        sLinha = //"3100000000" + (Antes da alteração)
                                 "31000000000" + 
                                 P_NUM_RGTRO_EMPRG.ToString("00000") +
                                 (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(3, '0') +
                                 new String(' ', 6) +
                                 //FormataDecimals(P_PERCENTUAL).PadLeft(10, '0') +
                                 FormataDecimals(P_VALORDESCONTO).PadLeft(10,'0') +
                                 new String(' ', 45) +
                                 Environment.NewLine;
                    }
                    break;

                case 2: //CPFL
                    //Print #2, Format(P_NUM_RGTRO_EMPRG, "00000000") & Chr(9) & "01." & TxtMes.Text & "." & TxtAno.Text & Chr(9) & "31.12.9999" & Chr(9) & Format(P_VDPATROCINADORA, "0000") & Chr(9) & Format(P_PERCENTUAL, "0000.00000") & Chr(9) & IIf(P_VALORDESCONTO > 0, Format(P_VALORDESCONTO, "000000000.00"), "0")
                    //if (P_PERCENTUAL > 0)
                    //{

                        P_MES_REF = MES_REF ?? 0; // Regra para gerar verbas de 13o (MES=13)

                        if (P_COD_EMPR == 66) // CPFL - Piratininga: 'Para De' matriculas 
                        {
                            ATT_CHARGER_DEPARA DaPara = ArqPatCargaBll.GetEmpregadoMatricula_DE_PARA(P_COD_EMPR, null, P_NUM_RGTRO_EMPRG.ToString());
                            if (DaPara != null)
                            {
                                P_NUM_RGTRO_EMPRG = int.Parse(DaPara.CONTEUDODE);
                            }
                        }
                        else if (P_COD_EMPR == 72 && P_MES_REF > 12) // Cpfl Geração
                        {
                            P_MES_REF = 12;
                        }

                        sLinha = P_NUM_RGTRO_EMPRG.ToString("000000") +
                                 "\t" +
                                 "01." +
                                 P_MES_REF.ToString("00") +
                                 "." +
                                 P_ANO_REF.ToString("0000") +
                                 "\t31.12.9999\t" +
                                 (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                 "\t" +
                                 ((P_PERCENTUAL > 0) ? P_PERCENTUAL.ToString("0.00") : "0") +
                                 "\t" +
                                 ((P_VALORDESCONTO > 0) ? P_VALORDESCONTO.ToString("0.00") : "0") +
                                 Environment.NewLine;
                    //}
                    break;

                case 3: //Epte
                    //Print #2, "01" & Format(P_NUM_RGTRO_EMPRG, "0000000") & "12" & TxtAno.Text & Mid(P_VDPATROCINADORA, 1, 4) & IIf(P_VALORDESCONTO > 0, Format(Int(P_VALORDESCONTO), "00000000000") & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2), "0000000000000") & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "0000") & Right(Format(P_PERCENTUAL, "#,##0.000"), 3), "0000000") & Mid(P_VDPATROCINADORA, 5, 1)
                    break;

                case 4: //FUNCESP

                    LAY_EMPREGADO LAY = new LAY_EMPREGADO(P_COD_EMPR);
                    //Print #2, "004" & Format(P_NUM_RGTRO_EMPRG, "0000000") & P_DIGITO & Space(30) & Format(P_VDPATROCINADORA, "0000") & IIf(P_VALORDESCONTO > 0, Format(Int(P_VALORDESCONTO), "00000000") & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2), "0000000000") & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "00000000") & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), "0000000000") & Space(7)
                    sLinha = "004" +
                                P_NUM_RGTRO_EMPRG.ToString("0000000") +
                                LAY.Calc_Digito_Matricula(P_COD_EMPR.ToString("000"), P_NUM_RGTRO_EMPRG.ToString("0000000000")).ToString() +
                                new String(' ', 30) +
                                (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +                                
                                FormataDecimals(P_VALORDESCONTO).PadLeft(10, '0') +
                                FormataDecimals(P_PERCENTUAL).PadLeft(10, '0') +
                                new String(' ', 7) +
                                Environment.NewLine;
                    break;

                case 40: //Eletropaulo
                case 44: //Tiete

                    if (P_COD_EMPR == 40 && EXT_ARQUIVO == ".txt")  //Eletropaulo
                    {
                        //Print #2, Format(P_VDPATROCINADORA, "00000") & ";;" & "1;" & Format(P_NUM_RGTRO_EMPRG, "000000000") & ";" & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "0000000000000") & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), "000000000000000") & ";;" & IIf(P_VALORDESCONTO > 0, Format(Int(P_VALORDESCONTO), "0000000000000") & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2), "000000000000000")
                        sLinha = (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(5, '0') +
                                    ";;1;" +
                                    P_NUM_RGTRO_EMPRG.ToString("000000000") +
                                    ";" +
                                    FormataDecimals(P_PERCENTUAL).PadLeft(15, '0') +
                                    ";;" +
                                    FormataDecimals(P_VALORDESCONTO).PadLeft(15, '0') +
                                    Environment.NewLine;
                    }
                    else if (P_COD_EMPR == 44) //Tiete
                    {
                        if (EXT_ARQUIVO == "_valor.txt" && P_VALORDESCONTO > 0)
                        {
                            //Print #2, "8;" & Format(P_NUM_RGTRO_EMPRG, "000000") & ";" & Format(P_VDPATROCINADORA, "0000") & ";" & Format(P_VALORDESCONTO, "#,##0.00")
                            sLinha = "8;" +
                                        P_NUM_RGTRO_EMPRG.ToString("000000") +
                                        ";" +
                                        (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                        ";" +
                                        P_VALORDESCONTO.ToString("0.00") +
                                        Environment.NewLine;
                        }
                        else if (EXT_ARQUIVO == "_qtde.txt" && P_PERCENTUAL > 0)
                        {
                            //Print #3, "8;" & Format(P_NUM_RGTRO_EMPRG, "000000") & ";" & Format(P_VDPATROCINADORA, "0000") & ";" & Format(P_PERCENTUAL, "#,##0.00")
                            sLinha = "8;" +
                                        P_NUM_RGTRO_EMPRG.ToString("000000") +
                                        ";" +
                                        (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                        ";" +
                                        P_PERCENTUAL.ToString("0.00") +
                                        Environment.NewLine;
                        }
                    }
                    break;

                case 41: //BANDEIRANTE

                    //Print #2, "B" & Format(P_NUM_RGTRO_EMPRG, "00000000") & "0" & "0000000" & Format(P_VDPATROCINADORA, "00000") & "12" & Mid(TxtMes.Text & TxtAno.Text, 5, 2) & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "00000000") & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), Format(Int(P_VALORDESCONTO), "00000000") & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2))
                    sLinha = "B" +
                                P_NUM_RGTRO_EMPRG.ToString("00000000") +
                                "00000000" +
                                (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(5, '0') +
                                "12" +
                                P_ANO_REF.ToString("0000").Substring(5, 2) +
                                FormataDecimals(P_PERCENTUAL).PadLeft(10, '0') +
                                FormataDecimals(P_VALORDESCONTO).PadLeft(10, '0') +
                                Environment.NewLine;
                    break;

                case 42: //EMAE

                    //Print #2, TxtAno.Text & Format(TxtMes.Text, "00") & "0015" & Format(P_NUM_RGTRO_EMPRG, "00000000") & Format(P_VDPATROCINADORA, "0000") & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "0000000000") & "," & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), Format(Int(P_VALORDESCONTO), "0000000000") & "," & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2)) & Space(7) & Format(Day(DateSerial(Year(Date), Month(Date) + 1, 0)), "00") & Format(Month(Date), "00") & Year(Date) & "CARGA FUND. CESP    "                    
                    //Print #3, TxtAno.Text & Format(TxtMes.Text, "00") & "0015" & Format(P_NUM_RGTRO_EMPRG, "00000000") & Format(1528, "0000") &              IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "0000000000") & "," & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), Format(Int(v_VALORDESCONTO), "0000000000") & "," & Right(Format(v_VALORDESCONTO, "#,##0.00"), 2)) & Space(7) & Format(Day(DateSerial(Year(Date), Month(Date) + 1, 0)), "00") & Format(Month(Date), "00") & Year(Date) & "CARGA FUND. CESP    "
                    if (EXT_ARQUIVO == ".txt" && linha_Repaase.COD_VERBA_PATROCINA != "1528")
                    {
                        sLinha = P_ANO_REF.ToString("0000")                               +
                                 P_MES_REF.ToString("00")                                 +
                                 "0015"                                                   +
                                 P_NUM_RGTRO_EMPRG.ToString("00000000")                   +
                                (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                   // FormataDecimals(P_PERCENTUAL).PadLeft(12, '0') +
                                P_VALORDESCONTO.ToString("0.00").PadLeft(13, '0')         +
                                new String(' ', 7)                                        +
                                    // SR64144 - Ult. dia do mês:
                                    //_current_date_time.ToString("ddMMyyyy") +
                               ULT_DIA_MES.ToString("ddMMyyyy")                           +
                               "CARGA FUND. CESP    ";
                    }
                    else if (EXT_ARQUIVO == "_cota.txt" && linha_Repaase.COD_VERBA_PATROCINA == "1528")
                    {
                        //Print #3, TxtAno.Text & Format(TxtMes.Text, "00") & "0015" & Format(P_NUM_RGTRO_EMPRG, "00000000") & Format(1528, "0000") & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "0000000000") & "," & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), Format(Int(v_VALORDESCONTO), "0000000000") & "," & Right(Format(v_VALORDESCONTO, "#,##0.00"), 2)) & Space(7) & Format(Day(DateSerial(Year(Date), Month(Date) + 1, 0)), "00") & Format(Month(Date), "00") & Year(Date) & "CARGA FUND. CESP    "
                        sLinha = P_ANO_REF.ToString("0000") +
                                    P_MES_REF.ToString("00") +
                                    "0015" +
                                    P_NUM_RGTRO_EMPRG.ToString("00000000") +
                                    //linha_Repaase.COD_VERBA_PATROCINA.PadLeft(4, '0') +
                                    (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                    //FormataDecimals(P_PERCENTUAL).PadLeft(12, '0') +
                                    P_VALORDESCONTO.ToString("0.00").PadLeft(13, '0') +
                                    new String(' ', 7) +
                                    // SR64144 - Ult. dia do mês:
                                    //_current_date_time.ToString("ddMMyyyy") +
                                    ULT_DIA_MES.ToString("ddMMyyyy") +
                                    "CARGA FUND. CESP    ";
                    }
                    if (!UltLinha)
                    {
                        sLinha += Environment.NewLine;
                    }
                    break;

                case 43: //Ceetp

                    //Print #2, "01" & Format(P_NUM_RGTRO_EMPRG, "0000000") & TxtMes.Text & TxtAno.Text & Mid(P_VDPATROCINADORA, 1, 4) & IIf(P_VALORDESCONTO > 0, Format(Int(P_VALORDESCONTO), "00000000000") & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2), "0000000000000") & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "0000") & Right(Format(P_PERCENTUAL, "#,##0.000"), 3), "0000000") & Mid(P_VDPATROCINADORA, 5, 1)
                    sLinha = "01" +
                                P_NUM_RGTRO_EMPRG.ToString("0000000") +
                                P_MES_REF.ToString("00") +
                                P_ANO_REF.ToString("0000") +
                                (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0').Substring(0,4) +                                
                                FormataDecimals(P_VALORDESCONTO).PadLeft(13, '0') +
                                FormataDecimals(P_PERCENTUAL, 3).PadLeft(7, '0') +
                                (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(5, '0').Substring(4, 1) +
                                Environment.NewLine;
                    break;

                case 45: //Duke Paranapanema                    
                    if (P_VALORDESCONTO > 0)
                    {
                        //Print #2, Format(P_NUM_RGTRO_EMPRG, "000000") & Chr(9) & Format(P_VDPATROCINADORA, "0000") & Chr(9) & Chr(9) & Format(P_VALORDESCONTO, "#,##0.00")
                        sLinha = P_NUM_RGTRO_EMPRG.ToString("000000") +
                                    "\t" +
                                    (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                    "\t\t" +
                                    P_VALORDESCONTO.ToString("0.00") +
                                    Environment.NewLine;
                    }
                    else
                    {
                        //Print #2, Format(P_NUM_RGTRO_EMPRG, "000000") & Chr(9) & Format(P_VDPATROCINADORA, "0000") & Chr(9) & Format(P_PERCENTUAL, "#,##0.00")
                        sLinha = P_NUM_RGTRO_EMPRG.ToString("000000") +
                                    "\t" +
                                    (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                    "\t" +
                                    P_PERCENTUAL.ToString("0.00") +
                                    Environment.NewLine;
                    }
                    break;

                case 50: //Elektro
                case 88: //EKCE
                    //Print #2, Format(P_NUM_RGTRO_EMPRG, "00000000") & Mid(FimdoMes(TxtMes.Text & "/" & TxtAno.Text), 7, 2) & Mid(FimdoMes(TxtMes.Text & "/" & TxtAno.Text), 5, 2) & Mid(FimdoMes(TxtMes.Text & "/" & TxtAno.Text), 1, 4) & Format(P_VDPATROCINADORA, "0000") & IIf(P_VALORDESCONTO > 0, Format(Int(P_VALORDESCONTO), "000000000000") & "," & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2), "000000000000,00") & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "000000000000") & "," & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), "000000000000,00")
                    sLinha = P_NUM_RGTRO_EMPRG.ToString("00000000") +
                                ULT_DIA_MES.ToString("ddMMyyyy") +
                                (linha_Repaase.COD_VERBA_PATROCINA ?? "").PadLeft(4, '0') +
                                P_VALORDESCONTO.ToString("0.00").PadLeft(15, '0') +
                                P_PERCENTUAL.ToString("0.00").PadLeft(15,'0') +                                
                                //FormataDecimals(P_PERCENTUAL).PadLeft(14, '0') +
                                //FormataDecimals(P_VALORDESCONTO).PadLeft(14, '0') +                                
                                Environment.NewLine;
                    break;

                //case 88: //EKCE
                //    //Print #2, Format(P_NUM_RGTRO_EMPRG, "00000000") & Mid(FimdoMes(TxtMes.Text & "/" & TxtAno.Text), 7, 2) & Mid(FimdoMes(TxtMes.Text & "/" & TxtAno.Text), 5, 2) & Mid(FimdoMes(TxtMes.Text & "/" & TxtAno.Text), 1, 4) & Format(P_VDPATROCINADORA, "0000") & IIf(P_VALORDESCONTO > 0, Format(Int(P_VALORDESCONTO), "000000000000") & "," & Right(Format(P_VALORDESCONTO, "#,##0.00"), 2), "000000000000,00") & IIf(P_PERCENTUAL > 0, Format(Int(P_PERCENTUAL), "000000000000") & "," & Right(Format(P_PERCENTUAL, "#,##0.00"), 2), "000000000000,00")
                //    sLinha = P_NUM_RGTRO_EMPRG.ToString("00000000") +
                //                ULT_DIA_MES.ToString("ddMMyyyy") +         
                //                linha_Repaase.COD_VERBA_PATROCINA.PadLeft(4, '0') +
                //                P_VALORDESCONTO.ToString("0.00") +
                //                P_PERCENTUAL.ToString("0.00") +
                //        //FormataDecimals(P_PERCENTUAL).PadLeft(14, '0') +
                //        //FormataDecimals(P_VALORDESCONTO).PadLeft(14, '0') +                                
                //                Environment.NewLine;
                //    break;
            }
            return sLinha;


        }

        public List<string> getSchemaExts(short? COD_GRUPO_EMPRS)
        {
            List<string> ret_exts = new List<string>();
            switch (COD_GRUPO_EMPRS)
            {
                case 2:
                case 45:
                    ret_exts.Add(".xls");
                    break;
                case 40:
                case 44:
                    ret_exts.Add(".txt");
                    ret_exts.Add("_valor.txt");
                    ret_exts.Add("_qtde.txt");
                    break;
                case 42:
                    ret_exts.Add(".txt");
                    ret_exts.Add("_cota.txt");
                    break;
                case 4:                
                case 41:
                case 43:
                case 50:
                case 51:
                case 88:
                default:
                    ret_exts.Add(".txt");
                    break;
            }
            return ret_exts;
        }

        private string FormataDecimals(decimal DECIMAL, int CASAS = 2)
        {
            if (DECIMAL > 0)
            {
                decimal integral = Math.Truncate(DECIMAL);
                decimal fractional = Math.Round(DECIMAL - integral, CASAS);

                string s = fractional.ToString("0.000", CultureInfo.InvariantCulture);

                if (s.IndexOf('.') > -1)
                {
                    string[] aDec = s.Split('.');
                    string sDes = aDec[1].ToString().PadRight(50, '0');
                    return integral.ToString() + sDes.Substring(0,CASAS);
                }
                else
                {
                    return integral.ToString();
                }

            }
            else
            {
                return "0";
            }
        }

        public Resultado GerarArquivo(short? sMes, short? sAno, short? grupo, string grupo_nome,  short? area, DateTime DTH_INCLUSAO, string LOG_INCLUSAO)
        {
            Resultado res = new Resultado();
            PRE_TBL_ARQ_ENV_REPASSE_View ret = base.GetWhere(sMes, sAno, grupo, null, null, area).FirstOrDefault();
            if (ret == null)
            {

                //string horainicio = DateTime.Now.ToShortTimeString();
                //ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                PRE_TBL_ARQ_ENV_REPASSE_View newArqDesconto = new PRE_TBL_ARQ_ENV_REPASSE_View();

                newArqDesconto.COD_ARQ_ENV_REPASSE = 0;
                newArqDesconto.DCR_ARQ_ENV_REPASSE = "MovFin_" + (sMes ?? 0).ToString("00") + "_" + (sAno ?? 0).ToString("0000") + "_" + grupo_nome;
                newArqDesconto.MES_REF = sMes;
                newArqDesconto.ANO_REF = sAno;
                newArqDesconto.COD_ARQ_AREA = area;
                newArqDesconto.COD_GRUPO_EMPRS = grupo;
                newArqDesconto.DTH_INCLUSAO = DTH_INCLUSAO;
                newArqDesconto.LOG_INCLUSAO = LOG_INCLUSAO;

                res = Importar(newArqDesconto);

                //res.Sucesso("Arquivos de desconto gerados com sucesso! Total de descontos gerados para o grupo: " + res.CodigoCriado);
            }
            else
            {
                res.Erro("Atenção! Já existe um arquivos de desconto gerado para o grupo " + (grupo ?? 0).ToString());
            }
            return res;
        }

        public Resultado LiberarArquivo(short? sMes, short? sAno, short? grupo, short? area, DateTime DTH_INCLUSAO, string LOG_INCLUSAO)
        {
            Resultado res = new Resultado();
            ArqPatrocinadoraEnvioBLL EnvioBLL = new ArqPatrocinadoraEnvioBLL();
            List<PRE_TBL_ARQ_ENV_REPASSE_View> lstREPASSES = base.GetWhere(sMes, sAno, grupo, null, null, area).ToList();
            int iJaLancados = 0;

            foreach (PRE_TBL_ARQ_ENV_REPASSE_View rep in lstREPASSES)
            {
                res = EnvioBLL.GerarComboArqGrupos(rep, area, DTH_INCLUSAO, LOG_INCLUSAO);
                if (!res.Ok)
                {
                    if (res.Mensagem.ToUpper().IndexOf("JÁ EXISTE") > -1)
                    {
                        iJaLancados++;
                    }
                }
            }

            if (iJaLancados == 0)
            {
                if (res.Ok)
                {
                    res.Sucesso("Todos os arquivos disponíveis foram liberados com sucesso.");
                }
                else
                {
                    res.Erro("Não foram encontrados arquivos para serem liberados no grupo " + (grupo ?? 0).ToString());
                }
                //res.Sucesso("Todos os arquivos disponíveis foram liberados com sucesso.");
                //res.Erro("Não foram encontrados arquivos para serem liberados.");
            }
            else if (iJaLancados == lstREPASSES.Count)
            {                
                res.Erro("Não existem arquivos novos disponiveis para liberar no grupo " + (grupo ?? 0).ToString());
            }
            else
            {
                res.Erro("Alguns arquivos foram liberados para o grupo " + (grupo ?? 0).ToString());   
            }            

            //if (ret == null)
            //{

            //    //string horainicio = DateTime.Now.ToShortTimeString();
            //    //ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
            //    PRE_TBL_ARQ_ENV_REPASSE_View newArqDesconto = new PRE_TBL_ARQ_ENV_REPASSE_View();

            //    newArqDesconto.COD_ARQ_ENV_REPASSE = 0;
            //    newArqDesconto.DCR_ARQ_ENV_REPASSE = "MovFin_" + (sMes ?? 0).ToString("00") + "_" + (sAno ?? 0).ToString("0000");
            //    newArqDesconto.MES_REF = sMes;
            //    newArqDesconto.ANO_REF = sAno;
            //    newArqDesconto.COD_ARQ_AREA = area;
            //    newArqDesconto.COD_GRUPO_EMPRS = grupo;
            //    newArqDesconto.DTH_INCLUSAO = DTH_INCLUSAO;
            //    newArqDesconto.LOG_INCLUSAO = LOG_INCLUSAO;

            //    res = Importar(newArqDesconto);

            //    //res.Sucesso("Arquivos de desconto gerados com sucesso! Total de descontos gerados para o grupo: " + res.CodigoCriado);
            //}
            //else
            //{
            //    res.Erro("Atenção! Já existe um arquivos de desconto gerado para o grupo " + (grupo ?? 0).ToString());
            //}
            return res;
        }

        public List<PRE_TBL_ARQ_ENV_REPASSE_View> Listar_Repasses(short? mes, short? ano, short? grupo, int? status, string referencia, short? area)
        {
            return base.GetWhere(mes, ano, grupo, status, referencia, area).ToList();
        }
    }
}