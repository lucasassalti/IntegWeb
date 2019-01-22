using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    
    class LAY_ENVIO
    {
        //
        // Empresa: 001
        //        
        public LAY_CAMPO LAY_COD_EMPRS_ADP = new LAY_CAMPO() { pos = 1, tam = 2, nome_amigavel = "CÓD. EMPRESA ADP", nome = "COD_EMPRS_ADP" };
        public LAY_CAMPO LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 11, tam = 5, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
        public LAY_CAMPO LAY_COD_VERBA = new LAY_CAMPO() { pos = 16, tam = 4, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
        public LAY_CAMPO LAY_COD_VERBA_2 = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "CÓD. VERBA CONTINUAÇÃO", nome = "COD_VERBA_2" };
        public LAY_CAMPO LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };
        public LAY_CAMPO LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 26, tam = 10, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };
        public LAY_CAMPO LAY_SEPARADOR = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "SEPARADOR", nome = "SEPARADOR" };        

        //
        // G R U P O 02
        //

        public void LAY_GRUPO_02()
        {
            LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 1, tam = 6, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
            LAY_COD_VERBA = new LAY_CAMPO() { pos = 4, tam = 0, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
            LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };
            LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 6, tam = 0, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };
        }

        //
        // G R U P O    40
        //        
        public void LAY_GRUPO_40()
        {
            LAY_COD_VERBA = new LAY_CAMPO() { pos = 1, tam = 5, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
            LAY_SEPARADOR = new LAY_CAMPO() { pos = 2, tam = 1, nome_amigavel = "SEPARADOR", nome = "SEPARADOR" };
            LAY_COD_EMPRS_ADP = new LAY_CAMPO() { pos = 3, tam = 1, nome_amigavel = "CÓD. EMPRESA ADP", nome = "COD_EMPRS_ADP" };
            LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 4, tam = 9, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
            LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 5, tam = 15, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };
            LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 7, tam = 15, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };
        }

        //
        // G R U P O    42
        //        
        public void LAY_GRUPO_42()
        {
            LAY_COD_EMPRS_ADP = new LAY_CAMPO() { pos = 7, tam = 4, nome_amigavel = "CÓD. EMPRESA ADP", nome = "COD_EMPRS_ADP" };
            LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 11, tam = 8, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
            LAY_COD_VERBA = new LAY_CAMPO() { pos = 19, tam = 4, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
            LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };
            LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 23, tam = 13, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };
            LAY_SEPARADOR = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "SEPARADOR", nome = "SEPARADOR" };
        }

        //
        // G R U P O    43
        //    
        public void LAY_GRUPO_43()
        {
            LAY_COD_EMPRS_ADP = new LAY_CAMPO() { pos = 1, tam = 2, nome_amigavel = "CÓD. EMPRESA ADP", nome = "COD_EMPRS_ADP" };
            LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 3, tam = 7, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
            LAY_COD_VERBA = new LAY_CAMPO() { pos = 16, tam = 4, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
            LAY_COD_VERBA_2 = new LAY_CAMPO() { pos = 40, tam = 1, nome_amigavel = "CÓD. VERBA CONTINUAÇÃO", nome = "COD_VERBA_2" };
            //LAY_SEPARADOR = new LAY_CAMPO() { pos = 2, tam = 1, nome_amigavel = "SEPARADOR", nome = "SEPARADOR" };         
            LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 20, tam = 13, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };   
            LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 33, tam = 7, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };            
        }

        //
        // G R U P O    44
        //    
        public void LAY_GRUPO_44()
        {
            LAY_COD_EMPRS_ADP = new LAY_CAMPO() { pos = 1, tam = 1, nome_amigavel = "CÓD. EMPRESA ADP", nome = "COD_EMPRS_ADP" };
            LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 2, tam = 6, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
            LAY_COD_VERBA = new LAY_CAMPO() { pos = 3, tam = 4, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
            //LAY_SEPARADOR = new LAY_CAMPO() { pos = 2, tam = 1, nome_amigavel = "SEPARADOR", nome = "SEPARADOR" };                        
            LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };
            LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 4, tam = 0, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };
        }

        //
        //G R U P O 45
        //
        public void LAY_GRUPO_45()
        {
            LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 1, tam = 6, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
            LAY_COD_VERBA = new LAY_CAMPO() { pos = 2, tam = 4, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
            LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 3, tam = 0, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };
            LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 4, tam = 0, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };
        }

        //
        // G R U P O    50  -  050 e 088
        //        
        public void LAY_GRUPO_50()
        {
            LAY_COD_EMPRS_ADP = new LAY_CAMPO() { pos = 0, tam = 0, nome_amigavel = "CÓD. EMPRESA ADP", nome = "COD_EMPRS_ADP" };            
            LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 1, tam = 8, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };            
            LAY_COD_VERBA = new LAY_CAMPO() { pos = 17, tam = 4, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
            //LAY_SEPARADOR = new LAY_CAMPO() { pos = 2, tam = 1, nome_amigavel = "SEPARADOR", nome = "SEPARADOR" };                                    
            LAY_VLR_DESCONTO = new LAY_CAMPO() { pos = 21, tam = 15, nome_amigavel = "Vlr. Desc.", nome = "LAY_VLR_DESCONTO" };
            LAY_VLR_PERCENTUAL = new LAY_CAMPO() { pos = 36, tam = 15, nome_amigavel = "Vlr. Perc.", nome = "VLR_PERCENTUAL" };
        }

        public LAY_ENVIO(short pCOD_EMPRS)
        {
            switch (pCOD_EMPRS)
            {
                case 1:
                case 2:
                case 62:
                case 66:
                case 71:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                case 89:
                case 91:
                case 92:
                case 95:
                case 97:
                case 98:
                case 99:
                case 100:
                case 104:
                    LAY_GRUPO_02();
                    break;
                case 4:
                default:
                    //Padrão();
                    break;
                case 42:
                    LAY_GRUPO_42();
                    break;
                case 43:
                    LAY_GRUPO_43();
                    break;
                case 44:
                    LAY_GRUPO_44();
                    break;
                case 40:
                    LAY_GRUPO_40();
                    break;
                case 45:
                    LAY_GRUPO_45();
                    break;
                case 50:
                case 88:
                    LAY_GRUPO_50();
                    break;
            }

           
        }

        //public EMPREGADO DePara(string pCOD_EMPRS, 
        //                        string pNUM_RGTRO_EMPRG,
        //                        string pDADOS)
        //{
        //    EMPREGADO newEmpregado = new EMPREGADO();

        //    //newEmpregado._NAO_ATUALIZAR = new List<string>();

        //    //foreach(PRE_TBL_ARQ_PATROCINA_CRITICA crit in lsCRITICAS.Where(c => c.COD_ARQ_PAT_LINHA == newCadastral.COD_ARQ_PAT_LINHA && c.TIP_CRITICA == 2))
        //    //{
        //    //    newEmpregado._NAO_ATUALIZAR.Add(crit.NOM_CAMPO);
        //    //}

        //    newEmpregado.COD_EMPRS = short.Parse(pCOD_EMPRS);
        //    newEmpregado.NUM_RGTRO_EMPRG = int.Parse(pNUM_RGTRO_EMPRG);
        //    newEmpregado.NUM_DIGVR_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_DIGVR_EMPRG);
        //    newEmpregado.NOM_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_EMPRG);
        //    newEmpregado.DCR_ENDER_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_ENDER_EMPRG);
        //    newEmpregado.DCR_COMPL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_COMPL_EMPRG);
        //    newEmpregado.NOM_BAIRRO_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_BAIRRO_EMPRG);
        //    newEmpregado.NOM_CIDRS_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_CIDRS_EMPRG);
        //    newEmpregado.COD_UNDFD_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_UNDFD_EMPRG);            
        //    newEmpregado.COD_CEP_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_COD_CEP_EMPRG);
        //    newEmpregado.NOM_PAIS_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_PAIS_EMPRG);
        //    newEmpregado.NUM_CXPTL_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CXPTL_EMPRG);
        //    newEmpregado.COD_DDI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_DDI_EMPRG);
        //    newEmpregado.COD_DDD_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_DDD_EMPRG);
        //    //newEmpregado.NUM_TELRES_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_TELRES_EMPRG);
        //    newEmpregado.NUM_TELEF_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_TELRES_EMPRG);           
        //    newEmpregado.NUM_RAMAL_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_RAMAL_EMPRG);
        //    //newEmpregado.COD_DDDCEL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_DDDCEL_EMPRG);
        //    newEmpregado.NUM_CELUL_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CELUL_EMPRG);
        //    newEmpregado.COD_EMAIL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_EMAIL_EMPRG);
        //    newEmpregado.DAT_NASCM_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_NASCM_EMPRG);
        //    newEmpregado.COD_SEXO_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_SEXO_EMPRG);
        //    newEmpregado.COD_ESTCV_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_ESTCV_EMPRG);
        //    newEmpregado.NUM_CPF_EMPRG = LAYOUT_UTIL.GetLongNull(pDADOS, LAY_NUM_CPF_EMPRG);
        //    newEmpregado.NUM_CI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_CI_EMPRG);
        //    newEmpregado.COD_UFCI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_UFCI_EMPRG);
        //    newEmpregado.COD_OREXCI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_OREXCI_EMPRG);
        //    newEmpregado.DAT_EXPCI_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_EXPCI_EMPRG);
        //    newEmpregado.NOM_PAI_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_PAI_EMPRG);
        //    newEmpregado.NOM_MAE_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_MAE_EMPRG);
        //    newEmpregado.NUM_PISPAS_EMPRG = LAYOUT_UTIL.GetLongNull(pDADOS, LAY_NUM_PISPAS_EMPRG);
        //    newEmpregado.NUM_CTPRF_EMPRG = LAYOUT_UTIL.GetLongNull(pDADOS, LAY_NUM_CTPRF_EMPRG);
        //    newEmpregado.NUM_SRCTP_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_SRCTP_EMPRG);
        //    newEmpregado.DCR_NATURAL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_NATURAL_EMPRG);
        //    newEmpregado.DCR_NACNL_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_NACNL_EMPRG);
        //    //newEmpregado.COD_UFNAT_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_UFNAT_EMPRG);
        //    newEmpregado.COD_BANCO = LAYOUT_UTIL.GetShort(pDADOS, LAY_COD_BANCO);
        //    newEmpregado.COD_AGBCO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_COD_AGBCO);
        //    newEmpregado.TIP_CTCOR_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_TIP_CTCOR_EMPRG);
        //    newEmpregado.NUM_CTCOR_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NUM_CTCOR_EMPRG);
        //    newEmpregado.DCR_OBSERVACAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_OBSERVACAO);
        //    newEmpregado.DAT_ADMSS_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_ADMSS_EMPRG);
        //    newEmpregado.COD_CTTRB_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_CTTRB_EMPRG);
        //    newEmpregado.VLR_SALAR_EMPRG = LAYOUT_UTIL.GetDecimalNull(pDADOS, LAY_VLR_SALAR_EMPRG, 4);
        //    newEmpregado.NUM_CARGO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CARGO);
        //    newEmpregado.DCR_OCPPROF_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_OCPPROF_EMPRG);
        //    newEmpregado.NUM_FILIAL = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_FILIAL);
        //    //newEmpregado.NUM_ORGAO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_ORGAO);
        //    newEmpregado._NUM_ORGAO_ARQUIVO = pDADOS.Substring(LAY_NUM_ORGAO.pos, LAY_NUM_ORGAO.tam).Trim();
        //    newEmpregado.COD_MTDSL = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_MTDSL);
        //    newEmpregado.COD_MTDSL = (newEmpregado.COD_MTDSL == 0) ? null : newEmpregado.COD_MTDSL;
        //    newEmpregado.DAT_DESLG_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_DESLG_EMPRG);
        //    newEmpregado.DAT_FALEC_EMPRG = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_FALEC_EMPRG);
        //    newEmpregado.COD_MUNICI = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_MUNICI);
        //    newEmpregado.COD_ESTADO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_ESTADO);
        //    newEmpregado.COD_CONFL_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_COD_CONFL_EMPRG);
        //    newEmpregado.QTD_MESTRB_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_QTD_MESTRB_EMPRG);
        //    newEmpregado.QTD_INSS_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_QTD_INSS_EMPRG);
        //    newEmpregado.NUM_GRSAL_EMPRG = LAYOUT_UTIL.GetShortNull(pDADOS, LAY_NUM_GRSAL_EMPRG);
            
        //    // ORA-02291: integrity constraint (ATT.FK_CR_EMPRG) violated - parent key not found
        //    // A TABELA ESTA VAZIA:  select * from att.centro_responsabilid
            
        //    //newEmpregado.NUM_CR = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_CR);            
        //    newEmpregado.MRC_PLSAUD_EMPRG = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_MRC_PLSAUD_EMPRG);
        //    newEmpregado.NUM_ENDER_EMPRG = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_ENDER_EMPRG);

        //   return newEmpregado;
        //}

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
