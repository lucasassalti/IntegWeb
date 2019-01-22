using IntegWeb.Entidades;
using IntegWeb.Framework; 
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

namespace IntegWeb.Previdencia.Aplicacao.BLL
{

    public class ArqPatrocinaCargaBLL : ArqPatrocinaCargaDAL
    {

        internal List<ATT_CHARGER_DEPARA> cache_DePara_OrgaoLotacao_table = null;
        internal short cache_DePara_OrgaoLotacao_emprs = 0;

        internal List<ATT_CHARGER_DEPARA> cache_DePara_EmpregMat_table = null;
        internal short cache_DePara_EmpregMat_emprs = 0;

        //internal Resultado Cadastral(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS, List<PRE_TBL_ARQ_PATROCINA_CRITICA> lsCRITICAS)
        internal Resultado Cadastral(PRE_TBL_ARQ_PATROCINA_LINHA newCadastral, List<string> NAO_ATUALIZAR)
        {
            LAY_EMPREGADO LAY = new LAY_EMPREGADO(short.Parse(newCadastral.COD_EMPRS));
            EMPREGADO filEmpregado = LAY.DePara(newCadastral.COD_EMPRS, newCadastral.NUM_RGTRO_EMPRG, newCadastral.DADOS);
            EMPREGADO newEmpregado = null;

            if (filEmpregado.COD_EMPRS == 66) // CPFL - Piratininga: De Para de matriculas 
            {
                filEmpregado = DE_PARA_CPFL_66(filEmpregado);
            }

            EMPREGADO oldEmpregado = base.LoadEmpregado(filEmpregado.COD_EMPRS, filEmpregado.NUM_RGTRO_EMPRG);

            //List<string> NAO_ATUALIZAR = new List<string>();

            //foreach (PRE_TBL_ARQ_PATROCINA_CRITICA crit in lsCRITICAS.Where(c => c.COD_ARQ_PAT_LINHA == newCadastral.COD_ARQ_PAT_LINHA && c.TIP_CRITICA == 2))
            //{
            //    NAO_ATUALIZAR.Add(crit.NOM_CAMPO);
            //}

            if (oldEmpregado != null)
            {
                newEmpregado = MergeEmpregado(oldEmpregado, filEmpregado, NAO_ATUALIZAR);
            }
            else
            {
                newEmpregado = MergeEmpregado(newEmpregado ?? new EMPREGADO(), filEmpregado, NAO_ATUALIZAR);
            }

            //if (newEmpregado.NUM_DIGVR_EMPRG==null){
            //newEmpregado.NUM_DIGVR_EMPRG = LAY.Calc_Digito_Matricula(newCadastral.COD_EMPRS, newCadastral.NUM_RGTRO_EMPRG);
            newEmpregado.NUM_DIGVR_EMPRG = LAY.Calc_Digito_Matricula(newEmpregado.COD_EMPRS.ToString("000"), newEmpregado.NUM_RGTRO_EMPRG.ToString("0000000000"));
            //}

            ATT_CHARGER_DEPARA DaPara = GetOrgaoLotacao_DE_PARA(newEmpregado.COD_EMPRS, newEmpregado._NUM_ORGAO_ARQUIVO);
           
            //ATT_CHARGER_DEPARA DaPara = base.GetOrgaoLotacao_DE_PARA(newEmpregado.COD_EMPRS, newEmpregado._NUM_ORGAO_ARQUIVO);
            if (NAO_ATUALIZAR.IndexOf("NUM_ORGAO") == -1)
            {
                if (DaPara != null)
                {
                    newEmpregado.NUM_ORGAO = int.Parse(DaPara.CONTEUDOPARA);
                }
                else
                {
                    throw new Exception("Orgão não localizado. Favor carregar primeiro o arquivo de orgão. ORGAO = " + newEmpregado._NUM_ORGAO_ARQUIVO);
                }
            }

            if (oldEmpregado == null)
            {
                return base.InsertData(newEmpregado);
            }
            else
            {
                //return base.UpdateData(oldEmpregado);
                return new Resultado(true);
            }

            //return base.SaveData(newEmpregado);
        }

        internal EMPREGADO MergeEmpregado(EMPREGADO leftEmpregado,
                                          EMPREGADO rightEmpregado,
                                          List<string> NAO_ATUALIZAR)
        {

            //List<string> NAO_ATUALIZAR = new List<string>();

            //m_DbContext.Entry(newEmpregado).CurrentValues.SetValues(newEmpregado);
            leftEmpregado.COD_EMPRS = rightEmpregado.COD_EMPRS;
            leftEmpregado.NUM_RGTRO_EMPRG = rightEmpregado.NUM_RGTRO_EMPRG;
            leftEmpregado.NUM_DIGVR_EMPRG = rightEmpregado.NUM_DIGVR_EMPRG;

            if (NAO_ATUALIZAR.IndexOf("NOM_EMPRG") == -1)
            {
                leftEmpregado.NOM_EMPRG = rightEmpregado.NOM_EMPRG;
            }

            if (NAO_ATUALIZAR.IndexOf("DCR_ENDER_EMPRG") == -1)   // Se não existir dados atualizados pelo Portal:
            {

                if (NAO_ATUALIZAR.IndexOf("COD_CEP_EMPRG") == -1)
                {
                    leftEmpregado.COD_CEP_EMPRG = rightEmpregado.COD_CEP_EMPRG;
                }

                if (NAO_ATUALIZAR.IndexOf("NUM_ENDER_EMPRG") == -1)
                {
                    leftEmpregado.NUM_ENDER_EMPRG = rightEmpregado.NUM_ENDER_EMPRG;
                }

                if (NAO_ATUALIZAR.IndexOf("NUM_TELRES_EMPRG") == -1)
                {
                    leftEmpregado.NUM_TELRES_EMPRG = rightEmpregado.NUM_TELRES_EMPRG;
                    //newEmpregado.NUM_TELEF_EMPRG = newEmpregado.NUM_TELEF_EMPRG;
                    //newEmpregado.NUM_RAMAL_EMPRG = newEmpregado.NUM_RAMAL_EMPRG;
                }

                if (NAO_ATUALIZAR.IndexOf("NUM_CELUL_EMPRG") == -1)
                {
                    leftEmpregado.NUM_CELUL_EMPRG = rightEmpregado.NUM_CELUL_EMPRG;
                }

                leftEmpregado.DCR_ENDER_EMPRG = rightEmpregado.DCR_ENDER_EMPRG;
                leftEmpregado.DCR_COMPL_EMPRG = rightEmpregado.DCR_COMPL_EMPRG;
                leftEmpregado.NOM_BAIRRO_EMPRG = rightEmpregado.NOM_BAIRRO_EMPRG;
                leftEmpregado.NOM_CIDRS_EMPRG = rightEmpregado.NOM_CIDRS_EMPRG;
                leftEmpregado.COD_UNDFD_EMPRG = rightEmpregado.COD_UNDFD_EMPRG;
                leftEmpregado.COD_EMAIL_EMPRG = rightEmpregado.COD_EMAIL_EMPRG;                

            }

            leftEmpregado.NOM_PAIS_EMPRG = rightEmpregado.NOM_PAIS_EMPRG;
            leftEmpregado.NUM_CXPTL_EMPRG = rightEmpregado.NUM_CXPTL_EMPRG;
            leftEmpregado.COD_DDI_EMPRG = rightEmpregado.COD_DDI_EMPRG;
            leftEmpregado.COD_DDD_EMPRG = rightEmpregado.COD_DDD_EMPRG;
            
            if (NAO_ATUALIZAR.IndexOf("DAT_NASCM_EMPRG") == -1)
            {
                leftEmpregado.DAT_NASCM_EMPRG = rightEmpregado.DAT_NASCM_EMPRG;
            }
            if (NAO_ATUALIZAR.IndexOf("COD_SEXO_EMPRG") == -1)
            {
                leftEmpregado.COD_SEXO_EMPRG = rightEmpregado.COD_SEXO_EMPRG;
            }
            if (NAO_ATUALIZAR.IndexOf("COD_ESTCV_EMPRG") == -1)
            {
                leftEmpregado.COD_ESTCV_EMPRG = rightEmpregado.COD_ESTCV_EMPRG;
            }
            if (NAO_ATUALIZAR.IndexOf("NUM_CPF_EMPRG") == -1)
            {
                leftEmpregado.NUM_CPF_EMPRG = rightEmpregado.NUM_CPF_EMPRG;
            }
            leftEmpregado.NUM_CI_EMPRG = rightEmpregado.NUM_CI_EMPRG;
            leftEmpregado.COD_UFCI_EMPRG = rightEmpregado.COD_UFCI_EMPRG;
            leftEmpregado.COD_OREXCI_EMPRG = rightEmpregado.COD_OREXCI_EMPRG;
            if (NAO_ATUALIZAR.IndexOf("DAT_EXPCI_EMPRG") == -1)
            {
                leftEmpregado.DAT_EXPCI_EMPRG = rightEmpregado.DAT_EXPCI_EMPRG;
            }
            leftEmpregado.NOM_MAE_EMPRG = rightEmpregado.NOM_MAE_EMPRG;
            leftEmpregado.NOM_PAI_EMPRG = rightEmpregado.NOM_PAI_EMPRG;
            leftEmpregado.NUM_PISPAS_EMPRG = rightEmpregado.NUM_PISPAS_EMPRG;
            //leftEmpregado.DCR_NATURAL_EMPR = rightEmpregado.DCR_NATURAL_EMPR;
            leftEmpregado.DCR_NACNL_EMPRG = rightEmpregado.DCR_NACNL_EMPRG;
            leftEmpregado.NUM_CTPRF_EMPRG = rightEmpregado.NUM_CTPRF_EMPRG;
            leftEmpregado.NUM_SRCTP_EMPRG = rightEmpregado.NUM_SRCTP_EMPRG;
            if (NAO_ATUALIZAR.IndexOf("COD_BANCO") == -1 &&
                NAO_ATUALIZAR.IndexOf("COD_AGBCO") == -1 &&
                NAO_ATUALIZAR.IndexOf("NUM_CTCOR_EMPRG") == -1 &&
                NAO_ATUALIZAR.IndexOf("TIP_CTCOR_EMPRG") == -1)
            {
                leftEmpregado.COD_BANCO = rightEmpregado.COD_BANCO;
                leftEmpregado.COD_AGBCO = rightEmpregado.COD_AGBCO;
                leftEmpregado.NUM_CTCOR_EMPRG = rightEmpregado.NUM_CTCOR_EMPRG;
                leftEmpregado.TIP_CTCOR_EMPRG = rightEmpregado.TIP_CTCOR_EMPRG;
            }
            else
            {

            }
            leftEmpregado.DCR_OBSERVACAO = rightEmpregado.DCR_OBSERVACAO;
            if (NAO_ATUALIZAR.IndexOf("DAT_ADMSS_EMPRG") == -1)
            {
                leftEmpregado.DAT_ADMSS_EMPRG = rightEmpregado.DAT_ADMSS_EMPRG;
            }
            if (NAO_ATUALIZAR.IndexOf("COD_CTTRB_EMPRG") == -1)
            {
                leftEmpregado.COD_CTTRB_EMPRG = rightEmpregado.COD_CTTRB_EMPRG;
            }
            leftEmpregado.VLR_SALAR_EMPRG = rightEmpregado.VLR_SALAR_EMPRG;
            if (NAO_ATUALIZAR.IndexOf("NUM_CARGO") == -1) {
                leftEmpregado.NUM_CARGO = rightEmpregado.NUM_CARGO;
            }
            //newEmpregado.DCR_OCPPROF_EMPR = rightEmpregado.DCR_OCPPROF_EMPR;
            leftEmpregado.NUM_FILIAL = rightEmpregado.NUM_FILIAL;
            if (NAO_ATUALIZAR.IndexOf("NUM_ORGAO") == -1)
            {
                leftEmpregado.NUM_ORGAO = rightEmpregado.NUM_ORGAO;                
            }
            leftEmpregado._NUM_ORGAO_ARQUIVO = rightEmpregado._NUM_ORGAO_ARQUIVO;

            //if (NAO_ATUALIZAR.IndexOf("COD_MTDSL") == -1)
            //{
            //    leftEmpregado.COD_MTDSL = rightEmpregado.COD_MTDSL;
            //}

            //leftEmpregado.DAT_DESLG_EMPRG = rightEmpregado.DAT_DESLG_EMPRG;
            leftEmpregado.DAT_FALEC_EMPRG = rightEmpregado.DAT_FALEC_EMPRG;

            if (rightEmpregado.COD_MUNICI != null && rightEmpregado.COD_ESTADO != null)
            {
                leftEmpregado.COD_MUNICI = rightEmpregado.COD_MUNICI;
                leftEmpregado.COD_ESTADO = rightEmpregado.COD_ESTADO;
            }
            
            leftEmpregado.COD_CONFL_EMPRG = rightEmpregado.COD_CONFL_EMPRG;
            leftEmpregado.QTD_MESTRB_EMPRG = rightEmpregado.QTD_MESTRB_EMPRG;
            leftEmpregado.QTD_INSS_EMPRG = rightEmpregado.QTD_INSS_EMPRG;
            leftEmpregado.NUM_GRSAL_EMPRG = rightEmpregado.NUM_GRSAL_EMPRG;
            leftEmpregado.NUM_CR = rightEmpregado.NUM_CR;
            leftEmpregado.MRC_PLSAUD_EMPRG = rightEmpregado.MRC_PLSAUD_EMPRG;

            //leftEmpregado.NAO_ATUALIZAR = new List<string>();

            //foreach(PRE_TBL_ARQ_PATROCINA_CRITICA crit in lsCRITICAS.Where(c => c.COD_ARQ_PAT_LINHA == newCadastral.COD_ARQ_PAT_LINHA && c.TIP_CRITICA == 2))
            //{
            //    leftEmpregado.NAO_ATUALIZAR.Add(crit.NOM_CAMPO);
            //}

            return leftEmpregado;
        }

        internal Resultado Afastamento(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS, List<string> NAO_ATUALIZAR)
        {
            LAY_AFASTAMENTO LAY = new LAY_AFASTAMENTO();
            AFASTAMENTO filAfastamento = LAY.DePara(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS);
            AFASTAMENTO oldAfastamento = base.LoadAfastamento(filAfastamento.COD_EMPRS, filAfastamento.NUM_RGTRO_EMPRG, filAfastamento.DAT_INAFT_AFAST);
            AFASTAMENTO newAfastamento = null;

            if (oldAfastamento != null)
            {
                newAfastamento = MergeAfastamento(oldAfastamento, filAfastamento, NAO_ATUALIZAR);
            }
            else
            {
                newAfastamento = MergeAfastamento(newAfastamento ?? new AFASTAMENTO(), filAfastamento, NAO_ATUALIZAR);
            }

            //Trava para não inserir um novo afastamento para um empregado já afastado:
            if (oldAfastamento == null && NAO_ATUALIZAR.IndexOf("DAT_INAFT_AFAST") == -1)
            {
                return base.InsertData(newAfastamento);
            }
            else
            {
                //Não atualizar Afastamento:
                //return base.SaveData(LAY.DePara(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS));
                return new Resultado(true);
            }            
        }

        internal AFASTAMENTO MergeAfastamento(AFASTAMENTO leftAfastamento,
                                              AFASTAMENTO rightAfastamento,
                                              List<string> NAO_ATUALIZAR)
        {

            leftAfastamento.COD_EMPRS = rightAfastamento.COD_EMPRS;
            leftAfastamento.NUM_RGTRO_EMPRG = rightAfastamento.NUM_RGTRO_EMPRG;
            if (NAO_ATUALIZAR.IndexOf("DAT_INAFT_AFAST") == -1)
            {
                leftAfastamento.DAT_INAFT_AFAST = rightAfastamento.DAT_INAFT_AFAST;
            }
            leftAfastamento.DAT_PRVFA_AFAST = rightAfastamento.DAT_PRVFA_AFAST;
            leftAfastamento.DAT_FMAFT_AFAST = rightAfastamento.DAT_FMAFT_AFAST;
            if (NAO_ATUALIZAR.IndexOf("COD_TIPAFT") == -1)
            {
                leftAfastamento.COD_TIPAFT = rightAfastamento.COD_TIPAFT;
            }

            return leftAfastamento;
        }

        internal ATT_CHARGER_DEPARA GetOrgaoLotacao_DE_PARA(short pCOD_EMPRS, string pNUM_ORGAO_DE = null)
        {
            Resultado res = new Resultado();
            if (cache_DePara_OrgaoLotacao_table == null || cache_DePara_OrgaoLotacao_emprs != pCOD_EMPRS)
            {
                CriticasDAL critDAL = new CriticasDAL();
                cache_DePara_OrgaoLotacao_table = critDAL.Carrega_cache_DePara_OrgaoLotacao_table(pCOD_EMPRS);
                cache_DePara_OrgaoLotacao_emprs = pCOD_EMPRS;
            }

            return cache_DePara_OrgaoLotacao_table.FirstOrDefault(o => o.CONTEUDODE == pNUM_ORGAO_DE);

        }

        internal Resultado OrgaoLotacao(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS, List<string> NAO_ATUALIZAR)
        {
            LAY_ORGAO LAY = new LAY_ORGAO();
            ORGAO filOrgao = LAY.DePara(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS);

            ATT_CHARGER_DEPARA DaPara = GetOrgaoLotacao_DE_PARA(filOrgao.COD_EMPRS, filOrgao._NUM_ORGAO_ARQUIVO);
            if (DaPara != null)
            {
                filOrgao.NUM_ORGAO = int.Parse(DaPara.CONTEUDOPARA);
            }
            else
            {
                //Testar
                int iMaxPk_ORGAO = base.GetMaxPk_ORGAO();
                filOrgao.NUM_ORGAO = iMaxPk_ORGAO+1;
                DaPara = new ATT_CHARGER_DEPARA();
                DaPara.CODAPLICACAO = 1;
                DaPara.CODEMPRESA = filOrgao.COD_EMPRS;
                DaPara.CODTABELA = 181;
                DaPara.CODCOLUNA = 1;
                DaPara.CONTEUDODE = filOrgao._NUM_ORGAO_ARQUIVO;
                DaPara.CONTEUDOPARA = filOrgao.NUM_ORGAO.ToString();
                base.InsertData(DaPara);
            }

            //ORGAO oldOrgao = base.LoadOrgao(filOrgao.COD_EMPRS, filOrgao.NUM_ORGAO);
            ORGAO oldOrgao = base.LoadOrgao(filOrgao.NUM_ORGAO);
            ORGAO newOrgao = null;

            if (oldOrgao != null)
            {
                newOrgao = MergeOrgao(oldOrgao, filOrgao, NAO_ATUALIZAR);
            }
            else
            {
                newOrgao = MergeOrgao(newOrgao ?? new ORGAO(), filOrgao, NAO_ATUALIZAR);
            }
            

            if (oldOrgao == null)
            {
                return base.InsertData(newOrgao);
            }
            else
            {
                //return base.SaveData(newOrgao);
                return new Resultado(true);
            }               
        }

        internal ORGAO MergeOrgao(ORGAO leftOrgao,
                                  ORGAO rightOrgao,
                                  List<string> NAO_ATUALIZAR)
        {

            leftOrgao.NUM_ORGAO = rightOrgao.NUM_ORGAO;
            leftOrgao.COD_EMPRS = rightOrgao.COD_EMPRS;
            if (NAO_ATUALIZAR.IndexOf("NOM_ORGAO") == -1)
            {
                leftOrgao.NOM_ORGAO = rightOrgao.NOM_ORGAO;
            }
            leftOrgao.COD_ORGAO = rightOrgao.COD_ORGAO;            
            leftOrgao.NUM_FILIAL = rightOrgao.NUM_FILIAL;
            leftOrgao.COD_ESTADO = rightOrgao.COD_ESTADO;
            leftOrgao.COD_MUNICI = rightOrgao.COD_MUNICI;
            if (NAO_ATUALIZAR.IndexOf("DCR_ENDER_ORGAO") == -1)
            {
                leftOrgao.DCR_ENDER_ORGAO = rightOrgao.DCR_ENDER_ORGAO;
            }
            if (NAO_ATUALIZAR.IndexOf("NUM_ENDER_ORGAO") == -1)
            {
                leftOrgao.NUM_ENDER_ORGAO = rightOrgao.NUM_ENDER_ORGAO;
            }
            if (NAO_ATUALIZAR.IndexOf("DCR_COMPL_ORGAO") == -1)
            {
                leftOrgao.DCR_COMPL_ORGAO = rightOrgao.DCR_COMPL_ORGAO;
            }
            if (NAO_ATUALIZAR.IndexOf("NOM_CIDRS_ORGAO") == -1)
            {
                leftOrgao.NOM_CIDRS_ORGAO = rightOrgao.NOM_CIDRS_ORGAO;
            }
            if (NAO_ATUALIZAR.IndexOf("COD_UNDFD_ORGAO") == -1)
            {
                leftOrgao.COD_UNDFD_ORGAO = rightOrgao.COD_UNDFD_ORGAO;
            }
            if (NAO_ATUALIZAR.IndexOf("COD_CEP_ORGAO") == -1)
            {
                leftOrgao.COD_CEP_ORGAO = rightOrgao.COD_CEP_ORGAO;
            }
            if (NAO_ATUALIZAR.IndexOf("COD_DDD_ORGAO") == -1)
            {
                leftOrgao.COD_DDD_ORGAO = rightOrgao.COD_DDD_ORGAO;
            }
            if (NAO_ATUALIZAR.IndexOf("NUM_TELEF_ORGAO") == -1)
            {
                leftOrgao.NUM_TELEF_ORGAO = rightOrgao.NUM_TELEF_ORGAO;
            }
            leftOrgao.NOM_BAIRRO_ORGAO = rightOrgao.NOM_BAIRRO_ORGAO;

            return leftOrgao;
        }

        //internal Resultado OrgaoLotacao_DE_PARA(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS)
        //{
        //    LAY_ORGAO LAY = new LAY_ORGAO();
        //    return base.SaveData(LAY.DePara(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS));
        //}

        internal EMPREGADO DE_PARA_CPFL_66(EMPREGADO _empregado)
        {
            EMPREGADO ret = new EMPREGADO();
            ret = _empregado;

            ATT_CHARGER_DEPARA DaPara = GetEmpregadoMatricula_DE_PARA(_empregado.COD_EMPRS, _empregado.NUM_RGTRO_EMPRG.ToString());
            if (DaPara != null)
            {
                //System.Diagnostics.Debug.WriteLine(pNUM_RGTRO_EMPRG + " -> " + DaPara.CONTEUDOPARA);
                ret.NUM_RGTRO_EMPRG = int.Parse(DaPara.CONTEUDOPARA);
            }
            else
            {
                throw new Exception("Empregado não localizado no DE-PARA de Matrículas CPFL-Piratininga-066. MATRICULA = " + _empregado.NUM_RGTRO_EMPRG.ToString());
            }

            return _empregado;
        }

        internal ATT_CHARGER_DEPARA GetEmpregadoMatricula_DE_PARA(short pCOD_EMPRS, string pNUM_RGTRO_EMPRG_DE = null, string pNUM_RGTRO_EMPRG_PARA = null)
        {
            Resultado res = new Resultado();
            if (cache_DePara_EmpregMat_table == null || cache_DePara_EmpregMat_emprs != pCOD_EMPRS)
            {
                ParticipanteBLL ParticBLL = new ParticipanteBLL();
                cache_DePara_EmpregMat_table = ParticBLL.GetEmpregado_DE_PARA2(pCOD_EMPRS);
                cache_DePara_EmpregMat_emprs = pCOD_EMPRS;
            }

            if (pNUM_RGTRO_EMPRG_DE != null)
            {
                return cache_DePara_EmpregMat_table.FirstOrDefault(o => o.CONTEUDODE == pNUM_RGTRO_EMPRG_DE);
            }
            else // if (pNUM_ORGAO_PARA != null)
            {
                return cache_DePara_EmpregMat_table.FirstOrDefault(o => o.CONTEUDOPARA == pNUM_RGTRO_EMPRG_PARA);
            }

        }

        internal Resultado Financeiro(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDAT_REPASSE, string pDADOS, List<string> NAO_ATUALIZAR)
        {
            LAY_FICHA_FINANCEIRA LAY = new LAY_FICHA_FINANCEIRA();
            FICHA_FINANCEIRA newFicha = LAY.DePara(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS);
            newFicha.DAT_PAGTO_VERFIN = Util.String2Date(pDAT_REPASSE);

            if (newFicha.COD_EMPRS == 66) // CPFL - Piratininga: De Para de matriculas 
            {
                ATT_CHARGER_DEPARA DaPara = GetEmpregadoMatricula_DE_PARA(newFicha.COD_EMPRS, newFicha.NUM_RGTRO_EMPRG.ToString());
                if (DaPara != null)
                {
                    newFicha.NUM_RGTRO_EMPRG = int.Parse(DaPara.CONTEUDOPARA);
                }
                else
                {
                    throw new Exception("Empregado não localizado. Favor carregar primeiro o arquivo de empregados. MATRICULA = " + newFicha.NUM_RGTRO_EMPRG.ToString());
                }
            }

            return base.InsertData(newFicha);
        }

        internal Resultado Financeiro(short pCOD_EMPRS, int pNUM_RGTRO_EMPRG, FICHA_FINANCEIRA newFicha)
        {
            LAY_FICHA_FINANCEIRA LAY = new LAY_FICHA_FINANCEIRA();
            //FICHA_FINANCEIRA newFicha = LAY.DePara(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDADOS);
            //newFicha.DAT_PAGTO_VERFIN = Util.String2Date(pDAT_REPASSE);

            if (newFicha.COD_EMPRS == 66) // CPFL - Piratininga: De Para de matriculas 
            {
                ATT_CHARGER_DEPARA DaPara = GetEmpregadoMatricula_DE_PARA(newFicha.COD_EMPRS, newFicha.NUM_RGTRO_EMPRG.ToString());
                if (DaPara != null)
                {
                    newFicha.NUM_RGTRO_EMPRG = int.Parse(DaPara.CONTEUDOPARA);
                }
                else
                {
                    throw new Exception("Empregado não localizado. Favor carregar primeiro o arquivo de empregados. MATRICULA = " + newFicha.NUM_RGTRO_EMPRG.ToString());
                }
            }

            return base.InsertData(newFicha);
        }

        public Resultado SaveData(PRE_TBL_ARQ_PAT_DEMONSTRA_DET newLancamento)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                PRE_TBL_ARQ_PAT_DEMONSTRA_DET atualiza = null;

                atualiza = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.Find(newLancamento.COD_DEMO_DET, newLancamento.TIP_LANCAMENTO);

                if (atualiza != null)  //Novo registro
                {
                    newLancamento.COD_ARQ_PAT_DEMO = atualiza.COD_ARQ_PAT_DEMO;
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newLancamento);
                    //res.Sucesso("UPDATE", newLancamento.COD_ARQ_PAT_DEMO);
                }
                res = SaveChanges();
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

    }
}
