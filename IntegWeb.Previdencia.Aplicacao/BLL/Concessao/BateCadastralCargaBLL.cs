using IntegWeb.Previdencia.Aplicacao.DAL.Concessao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Concessao
{
    public class BateCadastralCargaBLL : BateCadastralCargaDAL
    {

        public DataTable ListarAlteracoesBanc(DataTable dt)
        {

            DataTable dtAlteradosBanc = new DataTable();
            dtAlteradosBanc.Columns.Add("Código Empresa", typeof(String));
            dtAlteradosBanc.Columns.Add("Número Registro", typeof(String));
            dtAlteradosBanc.Columns.Add("Nome Empregado", typeof(String));
            dtAlteradosBanc.Columns.Add("Código Banco Alterado", typeof(String));
            dtAlteradosBanc.Columns.Add("Código Agência Alterado", typeof(String));
            dtAlteradosBanc.Columns.Add("Número Conta Alterado", typeof(String));
            dtAlteradosBanc.Columns.Add("Tipo Conta Alterado", typeof(String));

            foreach (DataRow item in dt.Rows)
            {

                String linha = item[0].ToString();
                short codEmprs = Int16.Parse(linha.Substring(0, 3));
                int numRgtroEmprg = int.Parse(linha.Substring(4, 9));

                List<CAD_DADOS_BANC> listBanc = new List<CAD_DADOS_BANC>();

                listBanc = GetDataBanc(codEmprs, numRgtroEmprg);

                if (listBanc.Count > 0)
                {
                    for (int i = 0; i < listBanc.Count; i++)
                    {
                        dtAlteradosBanc.Rows.Add(listBanc[i].COD_EMPRS,
                                         listBanc[i].NUM_RGTRO_EMPRG,
                                         listBanc[i].NOM_EMPRG,
                                         listBanc[i].COD_BANCO,
                                         listBanc[i].COD_AGENCIA,
                                         listBanc[i].NUM_CONTA,
                                         listBanc[i].TP_CONTA
                                         );
                    }

                }

            }

            return dtAlteradosBanc;
        }

        public DataTable ListarAlteracoesCad(DataTable dt)
        {

            DataTable dtAlteradosCad = new DataTable();
            dtAlteradosCad.Columns.Add("Código Empresa", typeof(String));
            dtAlteradosCad.Columns.Add("Número Registro", typeof(String));
            dtAlteradosCad.Columns.Add("Nome Empregado", typeof(String));
            dtAlteradosCad.Columns.Add("Campo Alterado", typeof(String));
            dtAlteradosCad.Columns.Add("Conteúdo Alterado", typeof(String));
            //dtAlteradosCad.Columns.Add("Data Atualização", typeof(String));

            foreach (DataRow item in dt.Rows)
            {

                String linha = item[0].ToString();
                short codEmprs = Int16.Parse(linha.Substring(0, 3));
                int numRgtroEmprg = int.Parse(linha.Substring(4, 9));

                List<CAD_DADOS_MOV_CAD> listCad = new List<CAD_DADOS_MOV_CAD>();

                listCad = GetDataCad(codEmprs, numRgtroEmprg);

                if (listCad.Count > 0)
                {
                    for (int i = 0; i < listCad.Count; i++)
                    {
                        dtAlteradosCad.Rows.Add(listCad[i].COD_EMPRS,
                                           listCad[i].NUM_RGTRO_EMPRG,
                                           listCad[i].NOM_EMPRG,
                                           listCad[i].CDEDESCOMPEMPRG,
                                           listCad[i].DESC_CONTEUDO
                                          // listCad[i].DTH_ATUALIZACAO
                                           );
                    }

                }

            }

            return dtAlteradosCad;
        }
    }
}
