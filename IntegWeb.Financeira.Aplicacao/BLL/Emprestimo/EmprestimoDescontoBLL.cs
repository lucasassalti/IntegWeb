using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Previdencia;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Financeira.Aplicacao.DAL;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using System.Globalization;

namespace IntegWeb.Financeira.Aplicacao.BLL
{
    public class EmprestimoDescontoBLL : EmprestimoDescontoDAL
    {

        public Resultado DePara(DataTable dt, String Mesref, String Anoref, String LOG_INCLUSAO)
        {
            Resultado res = new Resultado();
            List<AAT_TBL_EMPRESTIMO_DESCONTO> lsDescontos = new List<AAT_TBL_EMPRESTIMO_DESCONTO>();            

            DateTime currentDateTime = DateTime.Now;
            long PK = base.GetMaxPk();

            foreach (DataRow row in dt.Rows)
            {
                String _data = row["Empresa"].ToString();
                AAT_TBL_EMPRESTIMO_DESCONTO newRetDebConta = new AAT_TBL_EMPRESTIMO_DESCONTO();

                newRetDebConta.COD_EMPRESTIMO_DESCONTO = PK;                               
                newRetDebConta.COD_EMPRS = Util.String2Short(row["Empresa"].ToString()) ?? 0;
                newRetDebConta.NUM_RGTRO_EMPRG = Util.String2Int32(row["Registro"].ToString()) ?? 0;
                newRetDebConta.NUM_MATR_PARTF = Util.String2Int32(row["MATRICULA"].ToString()) ?? 0;
                newRetDebConta.NUM_IDNTF_RPTANT = Util.String2Int32(row["IDENTIFCAÇÃO REPRESENTANTE"].ToString()) ?? 0;
                newRetDebConta.MES_REF = Util.String2Short(Mesref) ?? 0;
                newRetDebConta.ANO_REF = Util.String2Short(Anoref) ?? 0;                
                newRetDebConta.COD_STATUS = 1;
                newRetDebConta.LOG_INCLUSAO = LOG_INCLUSAO;
                newRetDebConta.DTH_INCLUSAO = currentDateTime;

                switch (row["Perfil"].ToString().ToUpper())
                {
                    default:
                    case "ASSISTIDO":
                        newRetDebConta.COD_TIPO = "A";
                        break;
                    case "PENSIONISTA":
                        newRetDebConta.COD_TIPO = "P";
                        break;
                    case "DESLIGADO":
                        newRetDebConta.COD_TIPO = "D";
                        break;
                }
                
                newRetDebConta.NUM_CPF = Util.String2Int64(row["CPF"].ToString());
                newRetDebConta.VLR_DIVIDA = Util.String2Decimal(row["Saldo em  atraso"].ToString());                
                //newRetDebConta.LOG_INCLUSAO = ed.LOG_INCLUSAO
                //newRetDebConta.DTH_INCLUSAO = ed.DTH_INCLUSAO
                //newRetDebConta.DTH_EXCLUSAO = ed.DTH_EXCLUSAO
                //newRetDebConta.NOM_EMPRG = emp.NOM_EMPRG

                //newRetDebConta.AAT_TBL_RET_DEB_CONTA_MOTIVO = GetMotivo(newRetDebConta.COD_MOTIVO_RET);
                //newRetDebConta.NUM_SEQ_LINHA = line_count;
                PK++;
                lsDescontos.Add(newRetDebConta);
            }

            //Criticar(lsDebConta);

            if (lsDescontos.Count > 0)
            {
                base.DeleteData(Util.String2Short(Anoref) ?? 0, Util.String2Short(Mesref) ?? 0);
                res = base.Persistir(lsDescontos);
                //res.Sucesso(lsDescontos.Count + " registro(s) importado(s) com sucesso.");
            }
            else
            {
                res.Erro("Nenhum registro importado.");
            }
            return res;
        }

        public Resultado Processar(String Mes_ref, String Ano_ref, DateTime DtComplementados, DateTime DtSuplementados)
        {
            Resultado res = new Resultado();

            int ret = base.Processar(Mes_ref.PadLeft(2, '0'), Ano_ref.PadLeft(4, '0'), DtComplementados, DtSuplementados);

            if (ret > 0)
            {
                res.Sucesso(ret + " registro(s) processados(s) com sucesso.");
            } else {
                res.Erro("Erro! Nenhum registro atualizado.");
            }

            return res;
        }

        public DataTable GerarDataTable(String Mesref, String Anoref, short? pCod_Status)
        {
            Resultado res = new Resultado();
            DataTable dt = new DataTable();
            List<AAT_TBL_EMPRESTIMO_DESCONTO_view> lst_emp_des = base.GetWhere(Util.String2Short(Anoref), Util.String2Short(Mesref), null, null, null, null, null, pCod_Status).ToList();
            lst_emp_des = lst_emp_des
                .Where(e => e.VLR_DIVIDA_POSS > 0 && (e.COD_STATUS == 2 || e.COD_STATUS == 4))
                .ToList();

            if (lst_emp_des != null)
            {
                dt = lst_emp_des.ToDataTable();
            }

            dt.Columns[0].ColumnName = "Digito";
            dt.Columns[1].ColumnName = "Status";
            dt.Columns[2].ColumnName = "Perfil";
            dt.Columns[3].ColumnName = "Nome";
            dt.Columns[4].ColumnName = "PK";
            dt.Columns[5].ColumnName = "Empresa";
            dt.Columns[6].ColumnName = "Registro";
            dt.Columns[7].ColumnName = "Matrícula";
            dt.Columns[8].ColumnName = "Representante";
            dt.Columns[9].ColumnName = "Ano";
            dt.Columns[10].ColumnName = "Mês";            
            dt.Columns[11].ColumnName = "Cod. Status";
            dt.Columns[12].ColumnName = "Tipo";
            dt.Columns[13].ColumnName = "Cpf";
            dt.Columns[14].ColumnName = "Vlr. Divida";
            dt.Columns[15].ColumnName = "Vlr. Aprovado";
            dt.Columns[16].ColumnName = "Vlr. Desconto";
            dt.Columns[17].ColumnName = "Vlr. Líquido";
            dt.Columns[18].ColumnName = "Limite";
            dt.Columns[19].ColumnName = "Parc. Empréstimo";
            dt.Columns[20].ColumnName = "Vlr. Desc. Folha";
            dt.Columns.RemoveAt(0);
            dt.Columns.RemoveAt(3);
            dt.Columns.RemoveAt(9);
            dt.Columns.RemoveAt(18);
            dt.Columns.RemoveAt(18);
            dt.Columns.RemoveAt(18);
            dt.Columns.RemoveAt(18);
            dt.Columns.RemoveAt(18);
            dt.Columns.RemoveAt(18);
            dt.Columns.RemoveAt(18);
            dt.Columns.RemoveAt(18);
            //dt.Columns.RemoveAt(18);
            //dt.Columns.RemoveAt(25);
            //dt.Columns[12].ColumnName = "Cpf";
            //dt.Columns[13].ColumnName = "Desconto";
            //dt.Columns[14].ColumnName = "Líquido";
            //dt.Columns[15].ColumnName = "Parc. Empréstimo";
            //dt.Columns[16].ColumnName = "Vlr. Desc. Folha";

            return dt;
        }

        public Resultado GerarTxt(String Mesref, String Anoref, String caminho_arquivo)
        {
            Resultado res = new Resultado();

            List<AAT_TBL_EMPRESTIMO_DESCONTO_view> lst_emp_des = base.GetWhere(Util.String2Short(Anoref), Util.String2Short(Mesref), null, null, null, null, null, null).ToList();

            String strConteudo = "";

            if (lst_emp_des.Count > 0)
            {
                foreach(AAT_TBL_EMPRESTIMO_DESCONTO_view emp_dec in lst_emp_des)
                {
                    if (emp_dec.VLR_DIVIDA_POSS > 0 &&
                       (emp_dec.COD_STATUS == 2 || emp_dec.COD_STATUS == 4))
                    {
                        switch (emp_dec.NUM_IDNTF_RPTANT)
                        {
                            case 0:
                                strConteudo += "020";
                                strConteudo += "00000000000";
                                strConteudo += "A";
                                strConteudo += emp_dec.COD_EMPRS.ToString().PadLeft(3, '0');
                                strConteudo += emp_dec.NUM_RGTRO_EMPRG.ToString().PadLeft(10, '0');
                                strConteudo += emp_dec.NUM_DIGVR_EMPRG.ToString().PadLeft(1, '0');
                                strConteudo += "000000000000";
                                strConteudo += Anoref.PadLeft(4, '0') + Mesref.PadLeft(2, '0');
                                strConteudo += "20301";
                                strConteudo += FormataDecimals(emp_dec.VLR_CARGA ?? 0).PadLeft(13, '0');
                                strConteudo += "00000000000000000000000000000000000";
                                break;
                            default:
                                strConteudo += "020";
                                strConteudo += (emp_dec.NUM_CPF ?? 0).ToString().PadLeft(11, '0');
                                strConteudo += "P";
                                strConteudo += "000";
                                strConteudo += "0000000000";
                                strConteudo += "0";
                                strConteudo += "000000000000"; // filler
                                strConteudo += Anoref.PadLeft(4, '0') + Mesref.PadLeft(2, '0');
                                strConteudo += "20301";
                                strConteudo += FormataDecimals(emp_dec.VLR_CARGA ?? 0).PadLeft(13, '0');
                                strConteudo += "00000000000000000000000000000000000"; // filler
                                break;
                        }
                        strConteudo += Environment.NewLine;
                    }
                }
            }

            if (!String.IsNullOrEmpty(strConteudo))
            {
                Util.String2File(strConteudo, caminho_arquivo);
                foreach (AAT_TBL_EMPRESTIMO_DESCONTO_view emp_dec in lst_emp_des.Where(e => e.COD_STATUS == 2))
                {
                    emp_dec.COD_STATUS = 4; // TXT Gerado
                    base.SaveData(emp_dec);
                }
                res.Sucesso("Arquivo gerado com sucesso.");
            }
            else if (lst_emp_des.Count == 0)
            {
                res.Alert("Não foram encontrados registros para exportar.");
            }
            else
            {
                res.Erro("Erro ao gerar o arquivo.");
            }

            return res;
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
                    return integral.ToString() + sDes.Substring(0, CASAS);
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

        //public Resultado Criticar(List<AAT_TBL_RET_DEB_CONTA> lsRetDebConta)
        //{

        //    Resultado res = new Resultado();
        //    //List<AAT_TBL_RET_DEB_CONTA> lsDebConta = new List<AAT_TBL_RET_DEB_CONTA>();
        //    //DebitoContaRetornoDAL DebContaRetDAL = new DebitoContaRetornoDAL();
        //    //if (!String.IsNullOrEmpty(NomeArquivo))
        //    //{
        //    //    lsRetDebConta = GetWhere(NomeArquivo, null, "F").ToList();
        //    //}
        //    string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        //    DateTime currentDateTime = DateTime.Now;

        //    foreach (AAT_TBL_RET_DEB_CONTA retDebConta in lsRetDebConta.Where(x => x.ID_TP_REGISTRO == "F"))
        //    {
        //        AAT_TBL_DEB_CONTA debConta = new AAT_TBL_DEB_CONTA();

        //        short sTest;
        //        int iTest;
        //        if (!short.TryParse(retDebConta.COD_EMPRESA, out sTest)) retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
        //                                                                        new AAT_TBL_RET_DEB_CONTA_CRITICAS
        //                                                                        {
        //                                                                            COD_CRITICA = "200",
        //                                                                            DCR_CRITICA = "Cód. Empresa em branco."
        //                                                                        });

        //        if (!int.TryParse(retDebConta.NUM_REGISTRO, out iTest)) retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
        //                                                                        new AAT_TBL_RET_DEB_CONTA_CRITICAS
        //                                                                        {
        //                                                                            COD_CRITICA = "201",
        //                                                                            DCR_CRITICA = "Registro empregado em branco."
        //                                                                        });

        //        if (!int.TryParse(retDebConta.NUM_REPRESENTANTE, out iTest)) retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
        //                                                                        new AAT_TBL_RET_DEB_CONTA_CRITICAS
        //                                                                        {
        //                                                                            COD_CRITICA = "202",
        //                                                                            DCR_CRITICA = "Núm. representante em branco"
        //                                                                        });

        //        if (retDebConta.COD_MOTIVO_RET != "00")
        //        {
        //            retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(new AAT_TBL_RET_DEB_CONTA_CRITICAS
        //            {
        //                COD_CRITICA = retDebConta.COD_MOTIVO_RET,
        //                DCR_CRITICA = (retDebConta.AAT_TBL_RET_DEB_CONTA_MOTIVO != null) ? retDebConta.AAT_TBL_RET_DEB_CONTA_MOTIVO.DESC_MOTIVO : ""
        //            });
        //        }

        //    }

        //    foreach (AAT_TBL_RET_DEB_CONTA retDebConta in lsRetDebConta.Where(x => x.ID_TP_REGISTRO == "B"))
        //    {
        //        AAT_TBL_DEB_CONTA debConta = new AAT_TBL_DEB_CONTA();

        //        if (!String.IsNullOrEmpty(retDebConta.ID_DEB_BANC))
        //        {

        //            string strCPf = retDebConta.ID_DEB_BANC.Substring(3);
        //            long lCPF;
        //            long.TryParse(strCPf, out lCPF);

        //            // Int16.MaxValue: 32767
        //            // Int32.MaxValue: 2147483647
        //            // Int64.MaxValue: 9223372036854775807

        //            EmpregadoBLL EmpBLL = new EmpregadoBLL();
        //            EMPREGADO Emp = EmpBLL.GetEmpregado(null, null, lCPF, null);
        //            //REPRES_UNIAO_FSS

        //            if (Emp == null)
        //            {
        //                //EmpregadoBLL RepresBLL = new EmpregadoBLL();
        //                REPRES_UNIAO_FSS Repres = EmpBLL.GetRepresentante(null, null, lCPF, null);

        //                if (Repres == null)
        //                {
        //                    retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(new AAT_TBL_RET_DEB_CONTA_CRITICAS
        //                    {
        //                        COD_CRITICA = "203",
        //                        DCR_CRITICA = "Empregado não localizado (" + lCPF.ToString() + ")"
        //                    });
        //                }
        //                else
        //                {
        //                    retDebConta.COD_EMPRESA = Repres.COD_EMPRS.ToString();
        //                    retDebConta.NUM_REGISTRO = Repres.NUM_RGTRO_EMPRG.ToString();
        //                    retDebConta.NUM_REPRESENTANTE = Repres.NUM_IDNTF_RPTANT.ToString();
        //                }
        //            }
        //            else
        //            {
        //                retDebConta.COD_EMPRESA = Emp.COD_EMPRS.ToString();
        //                retDebConta.NUM_REGISTRO = Emp.NUM_RGTRO_EMPRG.ToString();
        //                retDebConta.NUM_REPRESENTANTE = "0";
        //            }

        //            short vCOD_PRODUTO;
        //            short.TryParse(retDebConta.ID_DEB_BANC.Substring(0, 3), out vCOD_PRODUTO);
        //            //if (vCOD_PRODUTO > 0)
        //            //{
        //            if (base.GetProduto(vCOD_PRODUTO) == null)
        //            {
        //                retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(new AAT_TBL_RET_DEB_CONTA_CRITICAS
        //                {
        //                    COD_CRITICA = "205",
        //                    DCR_CRITICA = "Produto não localizado (" + vCOD_PRODUTO.ToString("000") + ")"
        //                });
        //            }
        //            //}

        //        }

        //    }

        //    res.Sucesso(lsRetDebConta.Count + " registro(s) importado(s) com sucesso.");
        //    return res;

        //}


        //public Resultado ConsolidaListaDebitoConta(String NomeArquivo)
        //{

        //    Resultado res = new Resultado();
        //    List<AAT_TBL_RET_DEB_CONTA> lsDebConta = new List<AAT_TBL_RET_DEB_CONTA>();
        //    //DebitoContaRetornoDAL DebContaRetDAL = new DebitoContaRetornoDAL();
        //    DebitoContaDAL DebContaDAL = new DebitoContaDAL();
        //    lsDebConta = GetWhere(NomeArquivo, null, "B").Where(d => d.AAT_TBL_RET_DEB_CONTA_CRITICAS.Count == 0).ToList();
        //    string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        //    DateTime currentDateTime = DateTime.Now;

        //    res.Sucesso(lsDebConta.Count + " registro(s) importado(s) com sucesso.");

        //    foreach (AAT_TBL_RET_DEB_CONTA retDebConta in lsDebConta)
        //    {
        //        AAT_TBL_DEB_CONTA debConta = new AAT_TBL_DEB_CONTA();
        //        debConta.COD_EMPRS = Util.String2Short(retDebConta.COD_EMPRESA);
        //        debConta.NUM_RGTRO_EMPRG = Util.String2Int32(retDebConta.NUM_REGISTRO);
        //        //debConta.NUM_IDNTF_RPTANT = Util.String2Int32(retDebConta.NUM_REPRESENTANTE); // linha comentada e adicionado a linha de baixo.
        //        debConta.NUM_IDNTF_RPTANT = Convert.ToInt32(retDebConta.NUM_REPRESENTANTE);
        //        debConta.COD_PRODUTO = short.Parse(retDebConta.ID_DEB_BANC.Substring(0, 3));
        //        debConta.NUM_CPF = Int64.Parse(retDebConta.ID_DEB_BANC.Substring(3));
        //        debConta.ID_DEB_BANC = retDebConta.ID_DEB_BANC;
        //        //debConta.IND_ATIVO = short.Parse((retDebConta.COD_MOVIMENTO == "0") ? "1" : "0"); // TIPO F: 0=Débito   / 1=Cancelamento
        //        debConta.IND_ATIVO = short.Parse((retDebConta.COD_MOVIMENTO == "2") ? "1" : "0");   // TIPO B: 1=Inclusão / 2=Exclusão                
        //        debConta.LOG_INCLUSAO = userName;
        //        debConta.DTH_INCLUSAO = currentDateTime;
        //        debConta.DCR_NOM_ARQ = retDebConta.DCR_NOM_ARQ;
        //        debConta.NUM_SEQ_LINHA = retDebConta.NUM_SEQ_LINHA;

        //        debConta.COD_BANCO = "033";
        //        debConta.COD_AGENCIA = retDebConta.AGENC_DEB_CONTA;
        //        debConta.TIP_CONTA = retDebConta.ID_CLIENTE_BANCO.Substring(0, 2);
        //        debConta.NUM_CONTA = retDebConta.ID_CLIENTE_BANCO.Substring(2).Trim();

        //        Resultado resSave = DebContaDAL.SaveData(debConta);
        //        if (!resSave.Ok)
        //        {
        //            retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
        //                                    new AAT_TBL_RET_DEB_CONTA_CRITICAS
        //                                    {
        //                                        COD_CRITICA = "999",
        //                                        DCR_CRITICA = "Erro ao inserir o registro na tabela: " + resSave.Mensagem
        //                                    });
        //            m_DbContext.SaveChanges(); // Grava a critica de erro do banco
        //            res.Erro("Occoreram erros na importação.");
        //        }
        //    }

        //    if (lsDebConta.Count == 0)
        //    {
        //        res.Erro("Nenhum registro do tipo 'B' foi importado.");
        //    }
        //    return res;

        //}

    }
}
