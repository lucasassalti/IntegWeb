using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
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

    public class ArqPatrocinaDemonstrativoBLL : ArqPatrocinaDemonstrativoDAL
    {

        //public Resultado GeraDemonstrativo(PRE_TBL_ARQ_PATROCINA arqCARGA,                                                                              
        //                                   DateTime sDAT_REPASSE,
        //                                   DateTime sDAT_CREDITO,
        //                                   string   pLOG_INCLUSAO,
        //                                   DateTime? pDTH_INCLUSAO = null)
        //{
        //    Resultado res = new Resultado();
        //    try
        //    {
        //        string analytics = "";
        //        long LINHAS_ERRO = 0;
        //        long LINHAS_INSERIDAS = 0;
        //        long LINHAS_ATUALIZADAS = 0;
        //        DateTime tempo_ini, tempo_fim;
        //        tempo_ini = DateTime.Now;
        //        DateTime DAT_DEMONSTRATIVO = DateTime.Now;
        //        base.m_DbContext.Configuration.LazyLoadingEnabled = false;
        //        base.m_DbContext.Configuration.ProxyCreationEnabled = false;
        //        ArqPatrocinadoraBLL CargaBLL = new ArqPatrocinadoraBLL();
        //        List<PRE_TBL_ARQ_PATROCINA_LINHA> lsLINHAS = CargaBLL.LINHA_GetAllByCOD_ARQ_PAT(arqCARGA.COD_ARQ_PAT);

        //        PRE_TBL_ARQ_PATROCINA_CARGA Carga = new PRE_TBL_ARQ_PATROCINA_CARGA();
        //        //Crit.DeleteCritica(arqPatrocinadora.COD_ARQ_PAT);

        //        //if (ArqPatro.CRITICA_GetDataCountGroup(arqCARGA.COD_ARQ_PAT, 1) > 0)
        //        //{
        //        //    res.Erro("Foram encontradas críticas que impedem o carregamento do arquivo.");
        //        //}
        //        //else
        //        //{
        //            foreach (PRE_TBL_ARQ_PATROCINA_LINHA lALVO in lsLINHAS)
        //            {

        //                if (lALVO.TIP_LINHA != 2) continue;
        //                res = new Resultado();

        //                try
        //                {
        //                    res = DeParaDemonstrativo(lALVO.COD_EMPRS, 
        //                                              lALVO.NUM_RGTRO_EMPRG, 
        //                                              lALVO.DADOS, 
        //                                              pLOG_INCLUSAO, 
        //                                              pDTH_INCLUSAO ?? DAT_DEMONSTRATIVO,
        //                                              lALVO.COD_ARQ_PAT_LINHA);
        //                    switch (res.CodigoCriado)
        //                    {
        //                        case 1: //INSERT
        //                            LINHAS_INSERIDAS++;
        //                            break;
        //                        case 2: //UPDATE
        //                            LINHAS_ATUALIZADAS++;
        //                            break;
        //                    }

        //                    if (res.Ok)
        //                    {
        //                        res = base.SaveChanges();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    res.Erro(Util.GetInnerException(ex));
        //                }

        //                if (!res.Ok)
        //                {
        //                    LINHAS_ERRO++;
        //                    Carga.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
        //                    {
        //                        COD_CRITICA = 903,
        //                        DCR_CRITICA = "Atenção! Ocorreu um erro na tentativa de carregar a linha " + lALVO.NUM_LINHA + " do arquivo.\\nMotivo:" + res.Mensagem,
        //                        NOM_CAMPO = "",
        //                        NUM_POSICAO = 1
        //                    });
        //                }
        //            }
        //        //}

        //        tempo_fim = DateTime.Now;
        //        //analytics = (String.Format("{0:n}", (tempo_fim - tempo_ini).TotalSeconds)) + " sec.";
        //        analytics = "";

        //        if (LINHAS_ERRO == 0)
        //        {
        //            CargaBLL.Registra_LOG(6, arqCARGA.COD_ARQ_PAT, pLOG_INCLUSAO);
        //            res.Sucesso("Demonstrativo de Repasse gerado. " + analytics);
        //        }
        //        else
        //        {
        //            res.Sucesso("Atenção! Não foi possivel gerar o Demonstrativo de Repasse. Erro(s) encontrado(s): " + LINHAS_ERRO + "\\n" + analytics);
        //        }                

        //    }
        //    catch (Exception ex)
        //    {

        //        res.Erro("Atenção! Ocorreu um erro na tentativa de carregar o arquivo.\\nMotivo: " + Util.GetInnerException(ex));

        //        arqCARGA.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
        //        {
        //            COD_CRITICA = 902,
        //            DCR_CRITICA = "Atenção! Ocorreu um erro na tentativa de carregar o arquivo.\\nMotivo: " + Util.GetInnerException(ex),
        //            NOM_CAMPO = "",
        //            NUM_POSICAO = 1
        //        });
        //    }

        //    return res;
        //}

        //internal Resultado DeParaDemonstrativo2(DateTime? DAT_REPASSE, DateTime? DAT_CREDITO, string pDADOS, string pGRUPO_PORTAL, string pLOG_INCLUSAO, DateTime pDTH_INCLUSAO)
        //{
        //    Resultado Res = new Resultado();
        //    LAY_FICHA_FINANCEIRA LAY = new LAY_FICHA_FINANCEIRA();
        //    PRE_TBL_ARQ_PAT_DEMONSTRA newDemons = LAY.DePara_Demo(pGRUPO_PORTAL);
        //    //TB_SCR_SUBGRUPO_FINANC_VERBA SubGrupoVerba = base.GetTipoLancamento(newDemons.COD_VERBA);
        //    //if (SubGrupoVerba != null)
        //    //{
        //        //newDemons.COD_ARQ_PAT_DEMO
        //        newDemons.DAT_REPASSE = DAT_REPASSE;
        //        newDemons.DAT_CREDITO = DAT_CREDITO;
        //        newDemons.GRUPO_PORTAL = pGRUPO_PORTAL;
        //        newDemons.LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO, 0, 30);
        //        newDemons.DTH_INCLUSAO = pDTH_INCLUSAO;
        //        Res = base.SaveData(newDemons);
        //    //}
        //    //else
        //    //{
        //    //    Res.Sucesso("Verba ignorada");
        //   // }

        //    return Res;

        //}

        //internal Resultado DeParaDemonstrativo_detalhe2(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS, string pLOG_INCLUSAO, DateTime pDTH_INCLUSAO, long? pCOD_ARQ_PAT_LINHA = null, int? pCOD_ARQ_PAT_DEMO = 0)
        //{
        //    Resultado Res = new Resultado();
        //    LAY_FICHA_FINANCEIRA LAY = new LAY_FICHA_FINANCEIRA();
        //    PRE_TBL_ARQ_PAT_DEMONSTRA_DET newDemons = LAY.DePara_Demo_Detalhe(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS);
        //    TB_SCR_SUBGRUPO_FINANC_VERBA SubGrupoVerba = base.GetTipoLancamento(newDemons.COD_VERBA);
        //    if (SubGrupoVerba != null)
        //    {
        //        newDemons.LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO, 0, 30);
        //        newDemons.DTH_INCLUSAO = pDTH_INCLUSAO;
        //        newDemons.COD_ARQ_PAT_LINHA = pCOD_ARQ_PAT_LINHA;
        //        newDemons.TIP_CRED_DEB = SubGrupoVerba.CRED_DEB;
        //        newDemons.COD_ARQ_PAT_DEMO = Convert.ToInt32(pCOD_ARQ_PAT_DEMO);
        //        Res = base.SaveData(newDemons);
        //    }
        //    else
        //    {
        //        Res.Sucesso("Verba ignorada");
        //    }

        //    return Res;

        //}

        internal Resultado DeParaDemonstrativo(string pCOD_EMPRS, 
                                               string pNUM_RGTRO_EMPRG, 
                                               string pDADOS,
                                               string pDAT_REPASSE,
                                               string pDAT_CREDITO,
                                               string pGRUPO_PORTAL,
                                               string pLOG_INCLUSAO, 
                                               DateTime pDTH_INCLUSAO, 
                                               FinanceiroBLL FinBLL,
                                               long? pCOD_ARQ_PAT_LINHA = null, 
                                               int? pCOD_ARQ_PAT_DEMO = 0)
        {
            Resultado Res = new Resultado();
            LAY_FICHA_FINANCEIRA LAY = new LAY_FICHA_FINANCEIRA();
            //PRE_TBL_ARQ_PAT_DEMONSTRA_DET newLancamento = LAY.DePara_Demo_Detalhe(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS);
            PRE_TBL_ARQ_PAT_DEMONSTRA de_para_Demonstrativo = LAY.DePara_Demonstrativo(pDADOS);
            PRE_TBL_ARQ_PAT_DEMONSTRA_DET de_para_Lancamento = de_para_Demonstrativo.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.FirstOrDefault();
            TB_SCR_SUBGRUPO_FINANC_VERBA SubGrupoVerba = base.GetTipoLancamento(de_para_Lancamento.COD_VERBA);
            if (SubGrupoVerba != null)
            {
                PRE_TBL_ARQ_PAT_DEMONSTRA newDemonstrativo = new PRE_TBL_ARQ_PAT_DEMONSTRA();
                newDemonstrativo.COD_ARQ_PAT_DEMO = Convert.ToInt32(pCOD_ARQ_PAT_DEMO);
                newDemonstrativo.ANO_REF = de_para_Demonstrativo.ANO_REF;
                newDemonstrativo.MES_REF = de_para_Demonstrativo.MES_REF;
                newDemonstrativo.DAT_REPASSE = Util.String2Date(pDAT_REPASSE);
                newDemonstrativo.DAT_CREDITO = Util.String2Date(pDAT_CREDITO);
                newDemonstrativo.GRUPO_PORTAL = pGRUPO_PORTAL;
                newDemonstrativo.LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO, 0, 30);
                newDemonstrativo.DTH_INCLUSAO = pDTH_INCLUSAO;

                PRE_TBL_ARQ_PAT_DEMONSTRA_DET newLancamento = new PRE_TBL_ARQ_PAT_DEMONSTRA_DET();                            
                newLancamento.VLR_LANCAMENTO = de_para_Lancamento.VLR_LANCAMENTO;
                newLancamento.TIP_LANCAMENTO = "P";
                //newLancamento.TIP_CRED_DEB = "C";
                newLancamento.TIP_CRED_DEB = SubGrupoVerba.CRED_DEB;
                newLancamento.COD_EMPRS = Util.String2Short(pCOD_EMPRS);
                newLancamento.NUM_RGTRO_EMPRG = Util.String2Int64(pNUM_RGTRO_EMPRG);
                //newLancamento.ANO_REF = Util.String2Short(txtAnoRef.Text);
                //newLancamento.MES_REF = Util.String2Short(txtMesRef.Text);
                //newLancamento.COD_VERBA = Util.String2Int32(txtCodVerba.Text);        
                newLancamento.COD_VERBA = de_para_Lancamento.COD_VERBA;
                //FinanceiroBLL FinBLL = new FinanceiroBLL();
                PRE_TBL_ARQ_PAT_VERBA gVerba = FinBLL.GetGrupoVerba(Convert.ToInt16(pCOD_EMPRS), Convert.ToInt32(newLancamento.COD_VERBA)); // Previdência 13o   
                if (gVerba!=null) {
                    newLancamento.COD_VERBA_PATROCINA = gVerba.COD_VERBA_PATROCINA;
                    newLancamento.DCR_LANCAMENTO = "";
                }
                //newLancamento.VLR_LANCAMENTO = Util.String2Decimal(txtVlrLancamento.Text) ?? 0;
                newLancamento.COD_ARQ_PAT_LINHA = pCOD_ARQ_PAT_LINHA;
                newLancamento.LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO, 0, 30);
                newLancamento.DTH_INCLUSAO = pDTH_INCLUSAO;

                if (pCOD_ARQ_PAT_DEMO == 0)
                {
                    Res = base.SaveData(newDemonstrativo);
                    if (Res.Ok)
                    {
                        newLancamento.COD_ARQ_PAT_DEMO = Convert.ToInt32(Res.CodigoCriado);
                        base.InsertData(newLancamento);
                    }
                } 
                else 
                {
                    newLancamento.COD_ARQ_PAT_DEMO = Convert.ToInt32(pCOD_ARQ_PAT_DEMO);
                    Res = base.InsertData(newLancamento);
                }

            }
            else
            {
                Res.Sucesso("Verba ignorada");
            }

            return Res;

        }

        //public new Resultado SaveData(PRE_TBL_ARQ_PAT_DEMONSTRA_DET newLancamento, decimal pVLR_ACERTO)
        //{
        //    Resultado res = new Resultado();

        //    res = base.SaveData(newLancamento);

        //    if (res.Ok)
        //    {
        //        PRE_TBL_ARQ_PAT_DEMONSTRA_DET newAcerto = newLancamento.Clone();
        //        newAcerto.TIP_LANCAMENTO = "A";
        //        newAcerto.VLR_LANCAMENTO = pVLR_ACERTO;
        //        res = base.SaveData(newAcerto);
        //    }

        //    return res;
        //}

    }
}
