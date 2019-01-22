using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Financeira.Aplicacao.DAL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL
{
    public class DebitoContaRetornoBLL : DebitoContaRetornoDAL
    {

        public Resultado DePara(DataTable dt, String NomeArquivo)
        {
            Resultado res = new Resultado();
            List<AAT_TBL_RET_DEB_CONTA> lsDebConta = new List<AAT_TBL_RET_DEB_CONTA>();
            int line_count = 1;

            base.DeleteData(NomeArquivo);

            foreach (DataRow row in dt.Rows)
            {
                String _data = row["DATA"].ToString();
                AAT_TBL_RET_DEB_CONTA newRetDebConta = new AAT_TBL_RET_DEB_CONTA();
                newRetDebConta.DCR_NOM_ARQ = NomeArquivo;
                newRetDebConta.ID_TP_REGISTRO = _data.Substring(0, 1);
                newRetDebConta.COD_EMPRESA = _data.Substring(79, 3);
                newRetDebConta.NUM_REGISTRO=_data.Substring(82, 9);
                newRetDebConta.NUM_REPRESENTANTE=_data.Substring(91, 8);
                newRetDebConta.ID_DEB_BANC=_data.Substring(1, 25);
                newRetDebConta.NUM_NOSSO_NUMERO=_data.Substring(69, 10);
                newRetDebConta.DTA_VENCIMENTO=_data.Substring(44, 8);
                newRetDebConta.VLR_DEBITO=_data.Substring(52, 15);
                newRetDebConta.AGENC_DEB_CONTA=_data.Substring(26, 4);
                newRetDebConta.ID_CLIENTE_BANCO=_data.Substring(30, 14);
                switch (newRetDebConta.ID_TP_REGISTRO)
                {
                    case "A":
                        newRetDebConta.DTA_VENCIMENTO = _data.Substring(65, 8);
                        break;
                    case "F":
                        newRetDebConta.COD_MOTIVO_RET = _data.Substring(67, 2);
                        break;
                }
                //newRetDebConta.COD_MOTIVO_RET=(newRetDebConta.ID_TP_REGISTRO=="F") ? _data.Substring(67, 2) : null;
                newRetDebConta.COD_MOVIMENTO=_data.Substring(149, 1);
                newRetDebConta.AAT_TBL_RET_DEB_CONTA_MOTIVO = GetMotivo(newRetDebConta.COD_MOTIVO_RET);
                newRetDebConta.NUM_SEQ_LINHA=line_count;
                line_count++;
                lsDebConta.Add(newRetDebConta);
            }

            Criticar(lsDebConta);

            if (lsDebConta.Count > 0)
            {
                base.Persistir(lsDebConta);
                res.Sucesso(lsDebConta.Count + " registro(s) importado(s) com sucesso.");
            }
            else
            {
                res.Erro("Nenhum registro localizado.");
            }         

            return res;

        }

        public Resultado Criticar(List<AAT_TBL_RET_DEB_CONTA> lsRetDebConta)
        {

            Resultado res = new Resultado();
            //List<AAT_TBL_RET_DEB_CONTA> lsDebConta = new List<AAT_TBL_RET_DEB_CONTA>();
            //DebitoContaRetornoDAL DebContaRetDAL = new DebitoContaRetornoDAL();
            //if (!String.IsNullOrEmpty(NomeArquivo))
            //{
            //    lsRetDebConta = GetWhere(NomeArquivo, null, "F").ToList();
            //}
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            DateTime currentDateTime = DateTime.Now;

            foreach (AAT_TBL_RET_DEB_CONTA retDebConta in lsRetDebConta.Where(x => x.ID_TP_REGISTRO=="F"))
            {
                AAT_TBL_DEB_CONTA debConta = new AAT_TBL_DEB_CONTA();

                short sTest;
                int iTest;
                if (!short.TryParse(retDebConta.COD_EMPRESA, out sTest)) retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
                                                                                new AAT_TBL_RET_DEB_CONTA_CRITICAS 
                                                                                    { COD_CRITICA = "200", 
                                                                                      DCR_CRITICA = "Cód. Empresa em branco." });

                if (!int.TryParse(retDebConta.NUM_REGISTRO, out iTest)) retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
                                                                                new AAT_TBL_RET_DEB_CONTA_CRITICAS 
                                                                                    { COD_CRITICA = "201", 
                                                                                      DCR_CRITICA = "Registro empregado em branco." });

                if (!int.TryParse(retDebConta.NUM_REPRESENTANTE, out iTest)) retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
                                                                                new AAT_TBL_RET_DEB_CONTA_CRITICAS
                                                                                    { COD_CRITICA = "202", 
                                                                                      DCR_CRITICA = "Núm. representante em branco"});

                if (retDebConta.COD_MOTIVO_RET != "00")
                {
                    retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(new AAT_TBL_RET_DEB_CONTA_CRITICAS 
                                                                       { COD_CRITICA = retDebConta.COD_MOTIVO_RET, 
                                                                         DCR_CRITICA = (retDebConta.AAT_TBL_RET_DEB_CONTA_MOTIVO!=null) ? retDebConta.AAT_TBL_RET_DEB_CONTA_MOTIVO.DESC_MOTIVO : "" });
                }

            }

            foreach (AAT_TBL_RET_DEB_CONTA retDebConta in lsRetDebConta.Where(x => x.ID_TP_REGISTRO == "B"))
            {
                AAT_TBL_DEB_CONTA debConta = new AAT_TBL_DEB_CONTA();

                if (!String.IsNullOrEmpty(retDebConta.ID_DEB_BANC))
                {

                    string strCPf = retDebConta.ID_DEB_BANC.Substring(3);
                    long lCPF;
                    long.TryParse(strCPf, out lCPF);

                    // Int16.MaxValue: 32767
                    // Int32.MaxValue: 2147483647
                    // Int64.MaxValue: 9223372036854775807

                    EmpregadoBLL EmpBLL = new EmpregadoBLL();
                    EMPREGADO Emp = EmpBLL.GetEmpregado(null, null, lCPF, null);
                    //REPRES_UNIAO_FSS

                    if (Emp == null)
                    {
                        //EmpregadoBLL RepresBLL = new EmpregadoBLL();
                        REPRES_UNIAO_FSS Repres = EmpBLL.GetRepresentante(null, null, lCPF, null);

                        if (Repres == null)
                        {
                            retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(new AAT_TBL_RET_DEB_CONTA_CRITICAS
                            {
                                COD_CRITICA = "203",
                                DCR_CRITICA = "Empregado não localizado (" + lCPF.ToString() + ")"
                            });
                        }
                        else
                        {
                            retDebConta.COD_EMPRESA = Repres.COD_EMPRS.ToString();
                            retDebConta.NUM_REGISTRO = Repres.NUM_RGTRO_EMPRG.ToString();
                            retDebConta.NUM_REPRESENTANTE = Repres.NUM_IDNTF_RPTANT.ToString();
                        }
                    }
                    else
                    {
                        retDebConta.COD_EMPRESA = Emp.COD_EMPRS.ToString();
                        retDebConta.NUM_REGISTRO = Emp.NUM_RGTRO_EMPRG.ToString();
                        retDebConta.NUM_REPRESENTANTE = "0";
                    }

                    short vCOD_PRODUTO;
                    short.TryParse(retDebConta.ID_DEB_BANC.Substring(0, 3), out vCOD_PRODUTO);
                    //if (vCOD_PRODUTO > 0)
                    //{
                    if (base.GetProduto(vCOD_PRODUTO) == null)
                    {
                        retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(new AAT_TBL_RET_DEB_CONTA_CRITICAS
                        {
                            COD_CRITICA = "205",
                            DCR_CRITICA = "Produto não localizado (" + vCOD_PRODUTO.ToString("000") + ")"
                        });
                    }
                    //}

                }

            }

            res.Sucesso(lsRetDebConta.Count + " registro(s) importado(s) com sucesso.");
            return res;

        }


        public Resultado ConsolidaListaDebitoConta(String NomeArquivo)
        {

            Resultado res = new Resultado();            
            List<AAT_TBL_RET_DEB_CONTA> lsDebConta = new List<AAT_TBL_RET_DEB_CONTA>();
            //DebitoContaRetornoDAL DebContaRetDAL = new DebitoContaRetornoDAL();
            DebitoContaDAL DebContaDAL = new DebitoContaDAL();
            //lsDebConta = GetWhere(NomeArquivo, null, "B", null).Where(d => d.AAT_TBL_RET_DEB_CONTA_CRITICAS.Count == 0).ToList();
            //lsDebConta = GetWhere(NomeArquivo, null, "B", null,0,"",false).Where(d => d.AAT_TBL_RET_DEB_CONTA_CRITICAS.Count == 0).ToList();
            lsDebConta = GetWhere(null, null, null, NomeArquivo, null, "B", false).Where(d => d.AAT_TBL_RET_DEB_CONTA_CRITICAS.Count == 0).ToList();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            DateTime currentDateTime = DateTime.Now;

            res.Sucesso(lsDebConta.Count + " registro(s) importado(s) com sucesso.");

            foreach (AAT_TBL_RET_DEB_CONTA retDebConta in lsDebConta)
            {
                AAT_TBL_DEB_CONTA debConta = new AAT_TBL_DEB_CONTA();
                debConta.COD_EMPRS = Util.String2Short(retDebConta.COD_EMPRESA);
                debConta.NUM_RGTRO_EMPRG = Util.String2Int32(retDebConta.NUM_REGISTRO);
                //debConta.NUM_IDNTF_RPTANT = Util.String2Int32(retDebConta.NUM_REPRESENTANTE); // linha comentada e adicionado a linha de baixo.
                debConta.NUM_IDNTF_RPTANT = Convert.ToInt32(retDebConta.NUM_REPRESENTANTE);
                debConta.COD_PRODUTO = short.Parse(retDebConta.ID_DEB_BANC.Substring(0, 3));
                debConta.NUM_CPF = Int64.Parse(retDebConta.ID_DEB_BANC.Substring(3));
                debConta.ID_DEB_BANC = retDebConta.ID_DEB_BANC;
                //debConta.IND_ATIVO = short.Parse((retDebConta.COD_MOVIMENTO == "0") ? "1" : "0"); // TIPO F: 0=Débito   / 1=Cancelamento
                debConta.IND_ATIVO = short.Parse((retDebConta.COD_MOVIMENTO == "2") ? "1" : "0");   // TIPO B: 1=Inclusão / 2=Exclusão                
                debConta.LOG_INCLUSAO = userName;
                debConta.DTH_INCLUSAO = currentDateTime;
                debConta.DCR_NOM_ARQ = retDebConta.DCR_NOM_ARQ;
                debConta.NUM_SEQ_LINHA = retDebConta.NUM_SEQ_LINHA;

                debConta.COD_BANCO = "033";
                debConta.COD_AGENCIA = retDebConta.AGENC_DEB_CONTA;
                debConta.TIP_CONTA = retDebConta.ID_CLIENTE_BANCO.Substring(0,2);
                debConta.NUM_CONTA = retDebConta.ID_CLIENTE_BANCO.Substring(2).Trim();        

                Resultado resSave = DebContaDAL.SaveData(debConta);
                if (!resSave.Ok)
                {
                    retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.Add(
                                            new AAT_TBL_RET_DEB_CONTA_CRITICAS
                                            {
                                                COD_CRITICA = "999",
                                                DCR_CRITICA = "Erro ao inserir o registro na tabela: " + resSave.Mensagem 
                                            });
                    m_DbContext.SaveChanges(); // Grava a critica de erro do banco
                    res.Erro("Occoreram erros na importação.");
                }
            }

            if (lsDebConta.Count == 0)
            {
                res.Erro("Nenhum registro do tipo 'B' foi importado.");
            }
            return res;

        }

        public DataTable ListarDadosParaExcel(string pDCR_NOM_ARQ, int? pNUM_SEQ_LINHA, string pID_TP_REGISTRO, bool? pComCritica)
        {
            DataTable dt = new DataTable();
            List<AAT_TBL_RET_DEB_CONTA_view> list = new List<AAT_TBL_RET_DEB_CONTA_view>();
            list = GetDataReport(pDCR_NOM_ARQ, pNUM_SEQ_LINHA, pID_TP_REGISTRO, pComCritica).ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            dt.Columns[0].ColumnName = "Arquivo";
            dt.Columns[1].ColumnName = "Linha";
            dt.Columns[2].ColumnName = "Tipo";
            dt.Columns[3].ColumnName = "Empresa";
            dt.Columns[4].ColumnName = "Registro";
            dt.Columns[5].ColumnName = "Repres.";
            dt.Columns[6].ColumnName = "Nosso Número";
            dt.Columns[7].ColumnName = "Dt. Vencimento";
            dt.Columns[8].ColumnName = "Valor Débito";            
            dt.Columns[9].ColumnName = "Cód. Critica";
            dt.Columns[10].ColumnName = "Critica";
            dt.Columns[11].ColumnName = "Cód. Motivo";
            dt.Columns[12].ColumnName = "Motivo";
            return dt;
        }

    }
}
