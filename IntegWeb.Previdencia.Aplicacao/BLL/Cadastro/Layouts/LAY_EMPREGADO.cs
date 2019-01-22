using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    
    class LAY_EMPREGADO
    {
        //
        // Empresas: 001,002,004,043,062,066 e 071
        //
        public LAY_CAMPO LAY_COD_EMPRS = new LAY_CAMPO() { pos = 1, tam = 4, nome_amigavel = "CÓD. EMPRESA", nome = "COD_EMPRS" };
        public LAY_CAMPO LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 4, tam = 10, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
        public LAY_CAMPO LAY_NUM_DIGVR_EMPRG = new LAY_CAMPO() { pos = 14, tam = 1, nome_amigavel = "DÍGITO", nome = "NUM_DIGVR_EMPRG" };
        public LAY_CAMPO LAY_NOM_EMPRG = new LAY_CAMPO() { pos = 15, tam = 50, nome_amigavel = "NOME EMPREGADO", nome = "NOM_EMPRG" };
        public LAY_CAMPO LAY_DCR_ENDER_EMPRG = new LAY_CAMPO() { pos = 65, tam = 80, nome_amigavel = "ENDEREÇO", nome = "DCR_ENDER_EMPRG" }; 
        public LAY_CAMPO LAY_DCR_COMPL_EMPRG = new LAY_CAMPO() { pos = 145, tam = 50, nome_amigavel = "COMPL. ENDEREÇO", nome = "DCR_COMPL_EMPRG" };
        public LAY_CAMPO LAY_NOM_BAIRRO_EMPRG = new LAY_CAMPO() { pos = 195, tam = 25, nome_amigavel = "BAIRRO", nome = "NOM_BAIRRO_EMPRG" };
        public LAY_CAMPO LAY_NOM_CIDRS_EMPRG = new LAY_CAMPO() { pos = 220, tam = 30, nome_amigavel = "CIDADE", nome = "NOM_CIDRS_EMPRG" };
        public LAY_CAMPO LAY_COD_UNDFD_EMPRG = new LAY_CAMPO() { pos = 260, tam = 2, nome_amigavel = "ESTADO", nome = "COD_UNDFD_EMPRG" };
        public LAY_CAMPO LAY_COD_CEP_EMPRG = new LAY_CAMPO() { pos = 262, tam = 8, nome_amigavel = "CEP", nome = "COD_CEP_EMPRG" };
        public LAY_CAMPO LAY_NOM_PAIS_EMPRG = new LAY_CAMPO() { pos = 270, tam = 30, nome_amigavel = "PAÍS", nome = "NOM_PAIS_EMPRG" };
        public LAY_CAMPO LAY_NUM_CXPTL_EMPRG = new LAY_CAMPO() { pos = 300, tam = 5, nome_amigavel = "NÚM CX. POSTAL", nome = "NUM_CXPTL_EMPRG" };
        public LAY_CAMPO LAY_COD_DDI_EMPRG = new LAY_CAMPO() { pos = 305, tam = 7, nome_amigavel = "DDI", nome = "COD_DDI_EMPRG" };
        public LAY_CAMPO LAY_COD_DDD_EMPRG = new LAY_CAMPO() { pos = 312, tam = 4, nome_amigavel = "DDD", nome = "COD_DDD_EMPRG" };
        public LAY_CAMPO LAY_NUM_TELRES_EMPRG = new LAY_CAMPO() { pos = 316, tam = 8, nome_amigavel = "TEL. RESIDENCIAL", nome = "NUM_TELRES_EMPRG" };
        public LAY_CAMPO LAY_NUM_RAMAL_EMPRG = new LAY_CAMPO() { pos = 324, tam = 4, nome_amigavel = "NÚM. RAMAL", nome = "NUM_RAMAL_EMPRG" };
        public LAY_CAMPO LAY_COD_DDDCEL_EMPRG = new LAY_CAMPO() { pos = 14, tam = 1, nome_amigavel = "DDD CEL", nome = "COD_DDDCEL_EMPRG" };
        public LAY_CAMPO LAY_NUM_CELUL_EMPRG = new LAY_CAMPO() { pos = 328, tam = 8, nome_amigavel = "NÚM. CEL", nome = "NUM_CELUL_EMPRG" };
        public LAY_CAMPO LAY_COD_EMAIL_EMPRG = new LAY_CAMPO() { pos = 336, tam = 50, nome_amigavel = "E-MAIL", nome = "COD_EMAIL_EMPRG" };
        public LAY_CAMPO LAY_DAT_NASCM_EMPRG = new LAY_CAMPO() { pos = 386, tam = 8, nome_amigavel = "DT. NASCIMENTO", nome = "DAT_NASCM_EMPRG" };
        public LAY_CAMPO LAY_COD_SEXO_EMPRG = new LAY_CAMPO() { pos = 394, tam = 1, nome_amigavel = "SEXO", nome = "COD_SEXO_EMPRG" };
        public LAY_CAMPO LAY_COD_ESTCV_EMPRG = new LAY_CAMPO() { pos = 395, tam = 1, nome_amigavel = "ESTADO CIVIL", nome = "COD_ESTCV_EMPRG" };
        public LAY_CAMPO LAY_NUM_CPF_EMPRG = new LAY_CAMPO() { pos = 396, tam = 11, nome_amigavel = "CPF", nome = "NUM_CPF_EMPRG" };
        public LAY_CAMPO LAY_NUM_CI_EMPRG = new LAY_CAMPO() { pos = 407, tam = 15, nome_amigavel = "RG", nome = "NUM_CI_EMPRG" };
        public LAY_CAMPO LAY_COD_OREXCI_EMPRG = new LAY_CAMPO() { pos = 422, tam = 6, nome_amigavel = "CÓD. ÓRGÃO EXPEDIDOR", nome = "COD_OREXCI_EMPRG" };
        public LAY_CAMPO LAY_COD_UFCI_EMPRG = new LAY_CAMPO() { pos = 428, tam = 2, nome_amigavel = "EST. ÓRGÃO EXPEDIDOR", nome = "COD_UFCI_EMPRG" };
        public LAY_CAMPO LAY_DAT_EXPCI_EMPRG = new LAY_CAMPO() { pos = 430, tam = 8, nome_amigavel = "DAT. EXPEDIÇÃO RG", nome = "DAT_EXPCI_EMPRG" };
        public LAY_CAMPO LAY_NOM_MAE_EMPRG = new LAY_CAMPO() { pos = 438, tam = 255, nome_amigavel = "NOME MÃE", nome = "NOM_MAE_EMPRG" };
        public LAY_CAMPO LAY_NOM_PAI_EMPRG = new LAY_CAMPO() { pos = 693, tam = 255, nome_amigavel = "NOME PAI", nome = "NOM_PAI_EMPRG" };
        public LAY_CAMPO LAY_NUM_PISPAS_EMPRG = new LAY_CAMPO() { pos = 948, tam = 11, nome_amigavel = "PIS/PASEP", nome = "NUM_PISPAS_EMPRG" };
        public LAY_CAMPO LAY_DCR_NATURAL_EMPRG = new LAY_CAMPO() { pos = 959, tam = 30, nome_amigavel = "NATURALIDADE", nome = "DCR_NATURAL_EMPRG" };
        public LAY_CAMPO LAY_DCR_NACNL_EMPRG = new LAY_CAMPO() { pos = 989, tam = 30, nome_amigavel = "NACIONALIDADE", nome = "DCR_NACNL_EMPRG" };
        //public LAY_CAMPO  LAY_COD_UFNAT_EMPRG = new LAY_CAMPO() { pos = 989, tam = 30 }; 
        public LAY_CAMPO LAY_NUM_CTPRF_EMPRG = new LAY_CAMPO() { pos = 1019, tam = 15, nome_amigavel = "NÚM CART. TRABALHO", nome = "NUM_CTPRF_EMPRG" };
        public LAY_CAMPO LAY_NUM_SRCTP_EMPRG = new LAY_CAMPO() { pos = 1034, tam = 5, nome_amigavel = "NÚM SÉRIE CART. TRABALHO", nome = "NUM_SRCTP_EMPRG" };
        public LAY_CAMPO LAY_COD_BANCO = new LAY_CAMPO() { pos = 1039, tam = 3, nome_amigavel = "CÓD. BANCO", nome = "COD_BANCO" };
        public LAY_CAMPO LAY_COD_AGBCO = new LAY_CAMPO() { pos = 1042, tam = 7, nome_amigavel = "CÓD. AGENCIA", nome = "COD_AGBCO" };
        public LAY_CAMPO LAY_TIP_CTCOR_EMPRG = new LAY_CAMPO() { pos = 1049, tam = 5, nome_amigavel = "TIP. CONTA CORRENTE", nome = "TIP_CTCOR_EMPRG" };
        public LAY_CAMPO LAY_NUM_CTCOR_EMPRG = new LAY_CAMPO() { pos = 1054, tam = 15, nome_amigavel = "NÚM. CONTA CORRENTE", nome = "NUM_CTCOR_EMPRG" };
        public LAY_CAMPO LAY_DCR_OBSERVACAO = new LAY_CAMPO() { pos = 1069, tam = 100, nome_amigavel = "OBSERVAÇÃO", nome = "DCR_OBSERVACAO" };
        public LAY_CAMPO LAY_DAT_ADMSS_EMPRG = new LAY_CAMPO() { pos = 1169, tam = 8, nome_amigavel = "DT. ADMISSÃO", nome = "DAT_ADMSS_EMPRG" };
        public LAY_CAMPO LAY_COD_CTTRB_EMPRG = new LAY_CAMPO() { pos = 1177, tam = 1, nome_amigavel = "CÓD CONTR. TRABALHO", nome = "COD_CTTRB_EMPRG" };
        public LAY_CAMPO LAY_VLR_SALAR_EMPRG = new LAY_CAMPO() { pos = 1188, tam = 12, nome_amigavel = "VLR. SALÁRIO", nome = "VLR_SALAR_EMPRG" };
        public LAY_CAMPO LAY_NUM_CARGO = new LAY_CAMPO() { pos = 1200, tam = 8, nome_amigavel = "CÓD. CARGO", nome = "NUM_CARGO" };
        public LAY_CAMPO LAY_DCR_OCPPROF_EMPRG = new LAY_CAMPO() { pos = 1208, tam = 50, nome_amigavel = "DESC OCUP. PROFISSIONAL", nome = "DCR_OCPPROF_EMPRG" };
        public LAY_CAMPO LAY_NUM_FILIAL = new LAY_CAMPO() { pos = 1258, tam = 2, nome_amigavel = "NÚM FILIAL", nome = "NUM_FILIAL" };
        public LAY_CAMPO LAY_NUM_ORGAO = new LAY_CAMPO() { pos = 1260, tam = 15, nome_amigavel = "NÚM ORGÃO", nome = "NUM_ORGAO" };
        public LAY_CAMPO LAY_COD_MTDSL = new LAY_CAMPO() { pos = 1275, tam = 2, nome_amigavel = "CÓD MOTIVO DESLIGAMENTO", nome = "COD_MTDSL" };
        public LAY_CAMPO LAY_DAT_DESLG_EMPRG = new LAY_CAMPO() { pos = 1277, tam = 8, nome_amigavel = "DAT. DESLIGAMENTO", nome = "DAT_DESLG_EMPRG" };
        public LAY_CAMPO LAY_DAT_FALEC_EMPRG = new LAY_CAMPO() { pos = 1285, tam = 8, nome_amigavel = "DAT. FALECIMENTO", nome = "DAT_FALEC_EMPRG" };
        public LAY_CAMPO LAY_COD_MUNICI = new LAY_CAMPO() { pos = 1293, tam = 7, nome_amigavel = "CÓD. MUNICÍPIO", nome = "COD_MUNICI" };
        public LAY_CAMPO LAY_COD_ESTADO = new LAY_CAMPO() { pos = 1300, tam = 2, nome_amigavel = "ESTADO", nome = "COD_ESTADO" };
        public LAY_CAMPO LAY_COD_CONFL_EMPRG = new LAY_CAMPO() { pos = 1302, tam = 1, nome_amigavel = "CÓD CONFIDENCIALIDADE", nome = "COD_CONFL_EMPRG" };
        public LAY_CAMPO LAY_QTD_MESTRB_EMPRG = new LAY_CAMPO() { pos = 1303, tam = 4, nome_amigavel = "QTD. MÊS TRABALHADO", nome = "QTD_MESTRB_EMPRG" };
        public LAY_CAMPO LAY_QTD_INSS_EMPRG = new LAY_CAMPO() { pos = 1307, tam = 4, nome_amigavel = "QTD. MÊS TRABALHADO INSS", nome = "QTD_INSS_EMPRG" };
        public LAY_CAMPO LAY_NUM_GRSAL_EMPRG = new LAY_CAMPO() { pos = 1311, tam = 2, nome_amigavel = "NÚM. GRUPO SALARIAL", nome = "NUM_GRSAL_EMPRG" };
        public LAY_CAMPO LAY_NUM_CR = new LAY_CAMPO() { pos = 1313, tam = 12, nome_amigavel = "NÚM. CENTRO RESPONSABILIDADE", nome = "NUM_CR" };
        public LAY_CAMPO LAY_MRC_PLSAUD_EMPRG = new LAY_CAMPO() { pos = 1324, tam = 1, nome_amigavel = "MARCA PLANO SAÚDE", nome = "MRC_PLSAUD_EMPRG" };
        public LAY_CAMPO LAY_NUM_ENDER_EMPRG = new LAY_CAMPO() { pos = 1325, tam = 6, nome_amigavel = "NÚM. RESIDÊNCIA", nome = "NUM_ENDER_EMPRG" };

        //
        // G R U P O    4    -   040,045,050 e 060
        //        

        public void LAY_EMPREGADO_GRUPO_4()
        {
            //LAY_NUM_DIGVR_EMPRG = new LAY_CAMPO() { pos = 14, tam = 1 };
            //LAY_NOM_EMPRG = new LAY_CAMPO() { pos = 15, tam = 50 };
            //LAY_DCR_ENDER_EMPRG = new LAY_CAMPO() { pos = 65, tam = 80 };
            //LAY_DCR_COMPL_EMPRG = new LAY_CAMPO() { pos = 145, tam = 50 };
            //LAY_NOM_BAIRRO_EMPRG = new LAY_CAMPO() { pos = 195, tam = 25 };
            //LAY_NOM_CIDRS_EMPRG = new LAY_CAMPO() { pos = 220, tam = 30 };
            //LAY_COD_UNDFD_EMPRG = new LAY_CAMPO() { pos = 260, tam = 2 };
            //LAY_COD_CEP_EMPRG = new LAY_CAMPO() { pos = 262, tam = 8 };
            //LAY_NOM_PAIS_EMPRG = new LAY_CAMPO() { pos = 270, tam = 30 };
            //LAY_NUM_CXPTL_EMPRG = new LAY_CAMPO() { pos = 300, tam = 5 };
            //LAY_COD_DDI_EMPRG = new LAY_CAMPO() { pos = 305, tam = 7 };
            //LAY_COD_DDD_EMPRG = new LAY_CAMPO() { pos = 312, tam = 4 };
            //LAY_NUM_TELRES_EMPRG = new LAY_CAMPO() { pos = 316, tam = 8 };
            //LAY_NUM_RAMAL_EMPRG = new LAY_CAMPO() { pos = 324, tam = 4 };
            //LAY_COD_DDDCEL_EMPRG = new LAY_CAMPO() { pos = 14, tam = 1 };
            //LAY_NUM_CELUL_EMPRG = new LAY_CAMPO() { pos = 328, tam = 8 };
            //LAY_COD_EMAIL_EMPRG = new LAY_CAMPO() { pos = 336, tam = 50 };
            //LAY_DAT_NASCM_EMPRG = new LAY_CAMPO() { pos = 386, tam = 8 };
            //LAY_COD_SEXO_EMPRG = new LAY_CAMPO() { pos = 394, tam = 1 };
            //LAY_COD_ESTCV_EMPRG = new LAY_CAMPO() { pos = 395, tam = 1 };
            //LAY_NUM_CPF_EMPRG = new LAY_CAMPO() { pos = 396, tam = 11 };
            //LAY_NUM_CI_EMPRG = new LAY_CAMPO() { pos = 407, tam = 15 };
            //LAY_COD_OREXCI_EMPRG = new LAY_CAMPO() { pos = 422, tam = 6 };
            //LAY_COD_UFCI_EMPRG = new LAY_CAMPO() { pos = 428, tam = 2 };
            //LAY_DAT_EXPCI_EMPRG = new LAY_CAMPO() { pos = 430, tam = 8 };
            LAY_NOM_MAE_EMPRG.pos     = 438;   LAY_NOM_MAE_EMPRG.tam   = 50;
            LAY_NOM_PAI_EMPRG.pos     = 488;   LAY_NOM_PAI_EMPRG.tam   = 50;
            LAY_NUM_PISPAS_EMPRG.pos = 538;    LAY_NUM_PISPAS_EMPRG.tam = 11;
            LAY_DCR_NATURAL_EMPRG.pos = 549;   LAY_DCR_NATURAL_EMPRG.tam = 30;
            LAY_DCR_NACNL_EMPRG.pos   = 579;   LAY_DCR_NACNL_EMPRG.tam = 30;
            LAY_NUM_CTPRF_EMPRG.pos   = 609;   LAY_NUM_CTPRF_EMPRG.tam = 15;
            LAY_NUM_SRCTP_EMPRG.pos   = 624;   LAY_NUM_SRCTP_EMPRG.tam = 5;
            LAY_COD_BANCO.pos         = 629;   LAY_COD_BANCO.tam       = 3;
            LAY_COD_AGBCO.pos         = 632;   LAY_COD_AGBCO.tam       = 7;
            LAY_TIP_CTCOR_EMPRG.pos   = 639;   LAY_TIP_CTCOR_EMPRG.tam = 5;
            LAY_NUM_CTCOR_EMPRG.pos   = 644;   LAY_NUM_CTCOR_EMPRG.tam = 15;
            LAY_DCR_OBSERVACAO.pos    = 659;   LAY_DCR_OBSERVACAO.tam  = 100;
            LAY_DAT_ADMSS_EMPRG.pos   = 759;   LAY_DAT_ADMSS_EMPRG.tam = 8;
            LAY_COD_CTTRB_EMPRG.pos   = 767;   LAY_COD_CTTRB_EMPRG.tam = 1;
            LAY_VLR_SALAR_EMPRG.pos   = 778;   LAY_VLR_SALAR_EMPRG.tam = 12;
            LAY_NUM_CARGO.pos         = 790;   LAY_NUM_CARGO.tam       = 8;
            LAY_DCR_OCPPROF_EMPRG.pos = 798;   LAY_DCR_OCPPROF_EMPRG.tam= 50;
            LAY_NUM_FILIAL.pos        = 848;   LAY_NUM_FILIAL.tam       = 2;
            LAY_NUM_ORGAO.pos         = 850;   LAY_NUM_ORGAO.tam        = 15;
            LAY_COD_MTDSL.pos         = 865;   LAY_COD_MTDSL.tam        = 2;
            LAY_DAT_DESLG_EMPRG.pos   = 867;   LAY_DAT_DESLG_EMPRG.tam  = 8;
            LAY_DAT_FALEC_EMPRG.pos   = 875;   LAY_DAT_FALEC_EMPRG.tam  = 8;
            LAY_COD_MUNICI.pos        = 883;   LAY_COD_MUNICI.tam       = 7;
            LAY_COD_ESTADO.pos        = 890;   LAY_COD_ESTADO.tam       = 2;
            LAY_COD_CONFL_EMPRG.pos   = 892;   LAY_COD_CONFL_EMPRG.tam  = 1;
            LAY_QTD_MESTRB_EMPRG.pos  = 893;   LAY_QTD_MESTRB_EMPRG.tam = 4;
            LAY_QTD_INSS_EMPRG.pos    = 897;   LAY_QTD_INSS_EMPRG.tam   = 4;
            LAY_NUM_GRSAL_EMPRG.pos   = 901;   LAY_NUM_GRSAL_EMPRG.tam  = 2;
            LAY_NUM_CR.pos            = 903;   LAY_NUM_CR.tam           = 12;
            LAY_MRC_PLSAUD_EMPRG.pos  = 915;   LAY_MRC_PLSAUD_EMPRG.tam = 1;
            LAY_NUM_ENDER_EMPRG.pos   = 916;   LAY_NUM_ENDER_EMPRG.tam  = 6;
        }

        //
        // G R U P O    5    -   001
        //  

        public void LAY_EMPREGADO_GRUPO_5()
        {
            //LAY_NUM_DIGVR_EMPRG = new LAY_CAMPO() { pos = 14, tam = 1 };
            //LAY_NOM_EMPRG = new LAY_CAMPO() { pos = 15, tam = 50 };
            //LAY_DCR_ENDER_EMPRG = new LAY_CAMPO() { pos = 65, tam = 80 };
            //LAY_DCR_COMPL_EMPRG = new LAY_CAMPO() { pos = 145, tam = 50 };
            //LAY_NOM_BAIRRO_EMPRG = new LAY_CAMPO() { pos = 195, tam = 25 };
            //LAY_NOM_CIDRS_EMPRG = new LAY_CAMPO() { pos = 220, tam = 30 };
            //LAY_COD_UNDFD_EMPRG = new LAY_CAMPO() { pos = 260, tam = 2 };
            //LAY_COD_CEP_EMPRG = new LAY_CAMPO() { pos = 262, tam = 8 };
            //LAY_NOM_PAIS_EMPRG = new LAY_CAMPO() { pos = 270, tam = 30 };
            //LAY_NUM_CXPTL_EMPRG = new LAY_CAMPO() { pos = 300, tam = 5 };
            //LAY_COD_DDI_EMPRG = new LAY_CAMPO() { pos = 305, tam = 7 };
            //LAY_COD_DDD_EMPRG = new LAY_CAMPO() { pos = 312, tam = 4 };
            //LAY_NUM_TELRES_EMPRG = new LAY_CAMPO() { pos = 316, tam = 9 };
            //LAY_NUM_RAMAL_EMPRG = new LAY_CAMPO() { pos = 324, tam = 4 };
            //LAY_COD_DDDCEL_EMPRG = new LAY_CAMPO() { pos = 14, tam = 1 };
            LAY_NUM_CELUL_EMPRG.pos = 328; LAY_NUM_CELUL_EMPRG.tam = 9;
            LAY_COD_EMAIL_EMPRG.pos = 337; LAY_COD_EMAIL_EMPRG.tam = 50;
            LAY_DAT_NASCM_EMPRG.pos = 387; LAY_DAT_NASCM_EMPRG.tam = 8;
            LAY_COD_SEXO_EMPRG.pos = 395; LAY_COD_SEXO_EMPRG.tam = 1;
            LAY_COD_ESTCV_EMPRG.pos = 396; LAY_COD_ESTCV_EMPRG.tam = 1;
            LAY_NUM_CPF_EMPRG.pos = 397; LAY_NUM_CPF_EMPRG.tam = 11;
            LAY_NUM_CI_EMPRG.pos = 408; LAY_NUM_CI_EMPRG.tam = 15;
            LAY_COD_OREXCI_EMPRG.pos = 423; LAY_COD_OREXCI_EMPRG.tam = 6;
            LAY_COD_UFCI_EMPRG.pos = 429; LAY_COD_UFCI_EMPRG.tam = 2;
            LAY_DAT_EXPCI_EMPRG.pos = 431; LAY_DAT_EXPCI_EMPRG.tam = 8;
            LAY_NOM_MAE_EMPRG.pos = 439; LAY_NOM_MAE_EMPRG.tam = 255;
            LAY_NOM_PAI_EMPRG.pos = 694; LAY_NOM_PAI_EMPRG.tam = 255;
            LAY_NUM_PISPAS_EMPRG.pos = 949; LAY_NUM_PISPAS_EMPRG.tam = 11;

            LAY_DCR_NATURAL_EMPRG.pos = 960; LAY_DCR_NATURAL_EMPRG.tam = 30;
            LAY_DCR_NACNL_EMPRG.pos = 990; LAY_DCR_NACNL_EMPRG.tam = 30;
            LAY_NUM_CTPRF_EMPRG.pos = 1020; LAY_NUM_CTPRF_EMPRG.tam = 15;
            LAY_NUM_SRCTP_EMPRG.pos = 1035; LAY_NUM_SRCTP_EMPRG.tam = 5;
            LAY_COD_BANCO.pos = 1040; LAY_COD_BANCO.tam = 3;
            LAY_COD_AGBCO.pos = 1043; LAY_COD_AGBCO.tam = 7;
            LAY_TIP_CTCOR_EMPRG.pos = 1050; LAY_TIP_CTCOR_EMPRG.tam = 5;
            LAY_NUM_CTCOR_EMPRG.pos = 1055; LAY_NUM_CTCOR_EMPRG.tam = 15;
            LAY_DCR_OBSERVACAO.pos = 1070; LAY_DCR_OBSERVACAO.tam = 100;
            LAY_DAT_ADMSS_EMPRG.pos = 1170; LAY_DAT_ADMSS_EMPRG.tam = 8;
            LAY_COD_CTTRB_EMPRG.pos = 1178; LAY_COD_CTTRB_EMPRG.tam = 1;
            LAY_VLR_SALAR_EMPRG.pos = 1189; LAY_VLR_SALAR_EMPRG.tam = 12;
            LAY_NUM_CARGO.pos = 1201; LAY_NUM_CARGO.tam = 8;
            LAY_DCR_OCPPROF_EMPRG.pos = 1209; LAY_DCR_OCPPROF_EMPRG.tam = 50;
            LAY_NUM_FILIAL.pos = 1259; LAY_NUM_FILIAL.tam = 2;
            LAY_NUM_ORGAO.pos = 1261; LAY_NUM_ORGAO.tam = 15;
            LAY_COD_MTDSL.pos = 1276; LAY_COD_MTDSL.tam = 2;
            LAY_DAT_DESLG_EMPRG.pos = 1278; LAY_DAT_DESLG_EMPRG.tam = 8;
            LAY_DAT_FALEC_EMPRG.pos = 1286; LAY_DAT_FALEC_EMPRG.tam = 8;
            LAY_COD_MUNICI.pos = 1294; LAY_COD_MUNICI.tam = 7;
            LAY_COD_ESTADO.pos = 1301; LAY_COD_ESTADO.tam = 2;
            LAY_COD_CONFL_EMPRG.pos = 1303; LAY_COD_CONFL_EMPRG.tam = 1;
            LAY_QTD_MESTRB_EMPRG.pos = 1304; LAY_QTD_MESTRB_EMPRG.tam = 4;
            LAY_QTD_INSS_EMPRG.pos = 1308; LAY_QTD_INSS_EMPRG.tam = 4;
            LAY_NUM_GRSAL_EMPRG.pos = 1312; LAY_NUM_GRSAL_EMPRG.tam = 2;
            LAY_NUM_CR.pos = 1314; LAY_NUM_CR.tam = 12;
            LAY_MRC_PLSAUD_EMPRG.pos = 1325; LAY_MRC_PLSAUD_EMPRG.tam = 1;
            LAY_NUM_ENDER_EMPRG.pos = 1326; LAY_NUM_ENDER_EMPRG.tam = 6;
        }

        public LAY_EMPREGADO(short pCOD_EMPRS)
        {
            switch (pCOD_EMPRS)
            {
                case 1:
                    LAY_EMPREGADO_GRUPO_5();
                    break;
                case 2:
                case 4:
                case 43:
                case 62:
                case 66:
                case 71:
                case 87:
                case 89:
                default:
                    //LAY_EMPREGADO_GRUPO_1();
                    break;
                case 5:
                case 41:
                case 42:
                    //LAY_EMPREGADO_GRUPO_2();
                    break;
                case 44:
                    //LAY_EMPREGADO_GRUPO_2();
                    break;
                case 40:
                case 45:
                case 50:
                case 60:
                    LAY_EMPREGADO_GRUPO_4();
                    break;
            }

           
        }

        public EMPREGADO DePara(string pCOD_EMPRS, 
                                string pNUM_RGTRO_EMPRG,
                                string pDADOS)
        {
            EMPREGADO newEmpregado = new EMPREGADO();

            //newEmpregado._NAO_ATUALIZAR = new List<string>();

            //foreach(PRE_TBL_ARQ_PATROCINA_CRITICA crit in lsCRITICAS.Where(c => c.COD_ARQ_PAT_LINHA == newCadastral.COD_ARQ_PAT_LINHA && c.TIP_CRITICA == 2))
            //{
            //    newEmpregado._NAO_ATUALIZAR.Add(crit.NOM_CAMPO);
            //}

            newEmpregado.COD_EMPRS = short.Parse(pCOD_EMPRS);
            newEmpregado.NUM_RGTRO_EMPRG = int.Parse(pNUM_RGTRO_EMPRG);
            newEmpregado.NUM_DIGVR_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_DIGVR_EMPRG);
            newEmpregado.NOM_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_EMPRG);
            newEmpregado.DCR_ENDER_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_ENDER_EMPRG);
            newEmpregado.DCR_COMPL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_COMPL_EMPRG);
            newEmpregado.NOM_BAIRRO_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_BAIRRO_EMPRG);
            newEmpregado.NOM_CIDRS_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_CIDRS_EMPRG);
            newEmpregado.COD_UNDFD_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_UNDFD_EMPRG);            
            newEmpregado.COD_CEP_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_COD_CEP_EMPRG);
            newEmpregado.NOM_PAIS_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_PAIS_EMPRG);
            newEmpregado.NUM_CXPTL_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CXPTL_EMPRG);
            newEmpregado.COD_DDI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_DDI_EMPRG);
            newEmpregado.COD_DDD_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_DDD_EMPRG);
            //newEmpregado.NUM_TELRES_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_TELRES_EMPRG);
            newEmpregado.NUM_TELEF_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_TELRES_EMPRG);           
            newEmpregado.NUM_RAMAL_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_RAMAL_EMPRG);
            //newEmpregado.COD_DDDCEL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_DDDCEL_EMPRG);
            newEmpregado.NUM_CELUL_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CELUL_EMPRG);
            newEmpregado.COD_EMAIL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_EMAIL_EMPRG);
            newEmpregado.DAT_NASCM_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_NASCM_EMPRG);
            newEmpregado.COD_SEXO_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_SEXO_EMPRG);
            newEmpregado.COD_ESTCV_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_ESTCV_EMPRG);
            newEmpregado.NUM_CPF_EMPRG = LAYOUT_UTIL.GetLongNull(pDADOS, LAY_NUM_CPF_EMPRG);
            newEmpregado.NUM_CI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_CI_EMPRG);
            newEmpregado.COD_UFCI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_UFCI_EMPRG);
            newEmpregado.COD_OREXCI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_OREXCI_EMPRG);
            newEmpregado.DAT_EXPCI_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_EXPCI_EMPRG);
            newEmpregado.NOM_PAI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_PAI_EMPRG);
            newEmpregado.NOM_MAE_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_MAE_EMPRG);
            newEmpregado.NUM_PISPAS_EMPRG = LAYOUT_UTIL.GetLongNull(pDADOS, LAY_NUM_PISPAS_EMPRG);
            newEmpregado.NUM_CTPRF_EMPRG = LAYOUT_UTIL.GetLongNull(pDADOS, LAY_NUM_CTPRF_EMPRG);
            newEmpregado.NUM_SRCTP_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_SRCTP_EMPRG);
            newEmpregado.DCR_NATURAL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_NATURAL_EMPRG);
            newEmpregado.DCR_NACNL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_NACNL_EMPRG);
            //newEmpregado.COD_UFNAT_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_UFNAT_EMPRG);
            newEmpregado.COD_BANCO = LAYOUT_UTIL.GetShort(pDADOS, LAY_COD_BANCO);
            newEmpregado.COD_AGBCO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_COD_AGBCO);
            newEmpregado.TIP_CTCOR_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_TIP_CTCOR_EMPRG);
            newEmpregado.NUM_CTCOR_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_CTCOR_EMPRG);
            newEmpregado.DCR_OBSERVACAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_OBSERVACAO);
            newEmpregado.DAT_ADMSS_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_ADMSS_EMPRG);
            newEmpregado.COD_CTTRB_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_CTTRB_EMPRG);
            newEmpregado.VLR_SALAR_EMPRG = LAYOUT_UTIL.GetDecimalNull(pDADOS, LAY_VLR_SALAR_EMPRG, 4);
            newEmpregado.NUM_CARGO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CARGO);
            newEmpregado.DCR_OCPPROF_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_OCPPROF_EMPRG);
            newEmpregado.NUM_FILIAL = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_FILIAL);
            //newEmpregado.NUM_ORGAO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_ORGAO);
            newEmpregado._NUM_ORGAO_ARQUIVO = pDADOS.Substring(LAY_NUM_ORGAO.pos, LAY_NUM_ORGAO.tam).Trim();
            newEmpregado.COD_MTDSL = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_MTDSL);
            newEmpregado.COD_MTDSL = (newEmpregado.COD_MTDSL == 0) ? null : newEmpregado.COD_MTDSL;
            newEmpregado.DAT_DESLG_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_DESLG_EMPRG);
            newEmpregado.DAT_FALEC_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_FALEC_EMPRG);
            newEmpregado.COD_MUNICI = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_MUNICI);
            newEmpregado.COD_ESTADO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_ESTADO);
            newEmpregado.COD_CONFL_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_CONFL_EMPRG);
            newEmpregado.QTD_MESTRB_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_QTD_MESTRB_EMPRG);
            newEmpregado.QTD_INSS_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_QTD_INSS_EMPRG);
            newEmpregado.NUM_GRSAL_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_GRSAL_EMPRG);
            
            // ORA-02291: integrity constraint (ATT.FK_CR_EMPRG) violated - parent key not found
            // A TABELA ESTA VAZIA:  select * from att.centro_responsabilid
            
            //newEmpregado.NUM_CR = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CR);            
            newEmpregado.MRC_PLSAUD_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_MRC_PLSAUD_EMPRG);
            newEmpregado.NUM_ENDER_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_ENDER_EMPRG);

            //public Nullable<int> NUM_TELEF_EMPRG { get; set; }
            //public Nullable<int> NUM_MATR_PARTF { get; set; }            
            //public Nullable<short> COD_CTTRB_EMPRG { get; set; }        
            //public string NUM_CTCOR_EMPRG { get; set; }
            //public Nullable<long> NUM_CTPRF_EMPRG { get; set; }
            //public string NUM_SRCTP_EMPRG { get; set; }
            //public string MRC_PLSAUD_EMPRG { get; set; }
            //public string DCR_OCPPROF_EMPRG { get; set; }    
            //public string TIP_CTCOR_EMPRG { get; set; }
            //public string COD_UFNAT_EMPRG { get; set; }
            //public Nullable<decimal> VLR_01_EMPRG { get; set; }
            //public Nullable<short> QTD_DIATRB_EMPRG { get; set; }
            //public Nullable<short> QTD_ANOTRB_EMPRG { get; set; }
            //public Nullable<short> QTD_DIAANT_EMPRG { get; set; }
            //public Nullable<short> QTD_MESANT_EMPRG { get; set; }
            //public Nullable<short> QTD_ANOANT_EMPRG { get; set; }
            //public Nullable<long> NUM_TITULO_EMPRG { get; set; }
            //public Nullable<short> NUM_ZONA_EMPRG { get; set; }
            //public Nullable<short> NUM_SECAO_EMPRG { get; set; }
            //public string NUM_RGORIG_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_ULT_RCD_EMPRG { get; set; }
            //public Nullable<short> AGCCODCODIGODAAGENCIA { get; set; }
            //public Nullable<int> STECOD { get; set; }
            //public Nullable<long> NUM_CPFCTC_EMPRG { get; set; }
            //public string NOM_SEGTIT_EMPRG { get; set; }
            //public Nullable<short> COD_EMPRSRPTANT_EMPRG { get; set; }
            //public Nullable<int> NUM_RGTRORPTANT_EMPRG { get; set; }
            //public string NUM_DDDFAX_EMPRG { get; set; }
            //public Nullable<int> NUM_FAX_EMPRG { get; set; }
            //public string NUM_DDIFAX_EMPRG { get; set; }
            //public Nullable<short> ATECODATRIBUTOEMPRG { get; set; }
            //public Nullable<decimal> VLR_PATRIMONIAL_EMPRG { get; set; }
            //public Nullable<decimal> VLR_RENDIMENTO_EMPRG { get; set; }
            //public string DCR_SITPPE_EMPRG { get; set; }
            //public string DCR_RENDIMENTO_EMPRG { get; set; }
            //public string NAT_DOCIDNT_EMPRG { get; set; }
            //public Nullable<int> PAICOD { get; set; }
            //public string COD_ESTRUT_EMPRG { get; set; }
            //public string DCR_IDIOMA_EMPRG { get; set; }
            //public string MRC_NEGSEF_EMPRG { get; set; }
            //public string NUM_IP_EMPRG { get; set; }
            //public string MRC_NAOPART_EMPRG { get; set; }
            //public string NUM_CELUL2_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_OBTSRS_EMPRG { get; set; }
            //public string DCR_MOTOBT_EMPRG { get; set; }
            //public string EMPRGIDCTIPOMARCACAO { get; set; }
            //public Nullable<System.DateTime> DAT_PREVAPOS_EMPRG { get; set; }
            //public string EMPRGIDCGRPSANGUINEO { get; set; }
            //public string EMPRGIDCFATORRH { get; set; }
            //public string EMPRGNUMNATUREZA { get; set; }
            //public string EMPRGORGEMINATUREZA { get; set; }
            //public string EMPRGUNFORGEMINATUREZA { get; set; }
            //public Nullable<System.DateTime> EMPRGDTHEMINATUREZA { get; set; }
            //public Nullable<short> TLGCODTIPOLOGRAD { get; set; }
            //public string DCR_LOGRAD { get; set; }
            //public string NOM_EMPGR_ANS { get; set; }
            //public string EMPRGCODDDI2 { get; set; }
            //public string EMPRGCODDDD2 { get; set; }
            //public Nullable<int> EMPRGNUMTELEF2 { get; set; }
            //public string EMPRGDESEMAIL2 { get; set; }
            //public string NUM_SIAPE_EMPRG { get; set; }
            //public string NUM_MATFNC_EMPRG { get; set; }
            //public string NUM_CNS_EMPRG { get; set; }
            //public Nullable<int> PESCODPESSOA { get; set; }
            //public string DCR_APOS1_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_INIAPOS1_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_FIMAPOS1_EMPREG { get; set; }
            //public string DCR_APOS2_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_INIAPOS2_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_FIMAPOS2_EMPREG { get; set; }
            //public string DCR_APOS3_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_INIAPOS3_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_FIMAPOS3_EMPREG { get; set; }
            //public string DCR_APOS4_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_INIAPOS4_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_FIMAPOS4_EMPREG { get; set; }
            //public Nullable<System.DateTime> DAT_PRIEMP_EMPRG { get; set; }
            //public string MRC_ASSOC_EMPRG { get; set; }
            //public string MRC_DEFFIS_EMPRG { get; set; }
            //public Nullable<short> GDECOD { get; set; }
            //public Nullable<int> PAICODNASCIMENTO { get; set; }
            //public Nullable<int> PAICODRESIDENCIA { get; set; }
            //public Nullable<int> PAICODCIDADANIA1 { get; set; }
            //public Nullable<int> PAICODCIDADANIA2 { get; set; }
            //public string NUM_GRNCRD_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_VLDGRNCRD_EMPRG { get; set; }
            //public string NUM_DDICEL_EMPRG { get; set; }
            //public Nullable<System.DateTime> DAT_RECAD_EMPRG { get; set; }
            //public string MRC_RES_EXT_EMPRG { get; set; }
            //public string NOM_PAIS_EXT_1 { get; set; }
            //public string NOM_PAIS_EXT_2 { get; set; }
            //public string NOM_PAIS_EXT_3 { get; set; }
            //public Nullable<int> PAICODPAISEXT1 { get; set; }
            //public Nullable<int> PAICODPAISEXT2 { get; set; }
            //public Nullable<int> PAICODPAISEXT3 { get; set; }
            //public string MRC_DOC_GRNCRD { get; set; }
            //public string NOM_CJGE_EMPRG { get; set; }

           return newEmpregado;
        }

        public short Calc_Digito_Matricula (string pCOD_EMPRS, string pNUM_RGTRO_EMPRG)
        {
            //Public Function Mod11Mat(Emp As Integer, Mat As Long) As Byte
            //Dim Dig_calculado As Byte
            //Dim Tot_soma As Long
            //Dim Posicao As Byte
            //Dim Resto As Byte
            //Dim Ind As Byte
            //Dim cValor As String
            //ReDim aemp(3) As String
            //ReDim amat(7) As String

            //cValor = Format$(Emp, "000")
            //aemp(1) = Mid$(cValor, 1, 1)
            //aemp(2) = Mid$(cValor, 2, 1)
            //aemp(3) = Mid$(cValor, 3, 1)
 
            //cValor = Format$(Mat, "0000000")
            //amat(1) = Mid$(cValor, 1, 1)
            //amat(2) = Mid$(cValor, 2, 1)
            //amat(3) = Mid$(cValor, 3, 1)
            //amat(4) = Mid$(cValor, 4, 1)
            //amat(5) = Mid$(cValor, 5, 1)
            //amat(6) = Mid$(cValor, 6, 1)
            //amat(7) = Mid$(cValor, 7, 1)

            //Posicao = 7
            //For Ind = 2 To 8
            //  Tot_soma = Tot_soma + (Val(amat(Posicao)) * Ind)
            //  Posicao = Posicao - 1
            //Next
            //Posicao = 3
            //For Ind = 9 To 11
            //  Tot_soma = Tot_soma + (Val(aemp(Posicao)) * Ind)
            //  Posicao = Posicao - 1
            //Next
            //Resto = Int(Tot_soma / 11)
            //Resto = Tot_soma - (Resto * 11)
            //Dig_calculado = 11 - Resto
            //If Dig_calculado > 9 Then
            //   Dig_calculado = 0
            //End If
            //Mod11Mat = Dig_calculado
            //End Function

            char[] aemp = pCOD_EMPRS.ToCharArray();
            char[] amat = pNUM_RGTRO_EMPRG.ToCharArray();

            int Tot_soma = 0;
            int Posicao = 9;

            for (int Ind = 2; Ind < 9; Ind++)
            {
                Tot_soma = Tot_soma + (Convert.ToInt32(amat[Posicao].ToString()) * Ind);
                Posicao--;
            }

            Posicao = 2;
            for (int Ind = 9; Ind < 12; Ind++)
            {
                Tot_soma = Tot_soma + (Convert.ToInt32(aemp[Posicao].ToString()) * Ind);
                Posicao--;
            }

            int Resto = 0;
            short Dig_calculado = 0;

            //Resto = Convert.ToInt32(Tot_soma / 11);
            //Resto = Tot_soma - (Resto * 11);

            Resto = Convert.ToInt32(Tot_soma % 11);
            Dig_calculado = Convert.ToInt16(11 - Resto);
            if (Dig_calculado>9)
            {
               Dig_calculado = 0;
            }

            //Mod11Mat = Dig_calculado

            return Dig_calculado;
        }
        
    }
}
