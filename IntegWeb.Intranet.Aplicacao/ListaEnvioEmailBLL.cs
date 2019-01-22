using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IntegWeb.Intranet.Aplicacao.DAL;

namespace IntegWeb.Intranet.Aplicacao
{
    public class ListaEnvioEmailBLL : ListaEnvioEmailDAL
    {
        public DataTable GeraBoletoSaude()
        {
            

            DataTable dtFinal = new DataTable();

            dtFinal.Columns.Add("Nome Empregado");
            dtFinal.Columns.Add("Identificação");
            dtFinal.Columns.Add("CPF");
            dtFinal.Columns.Add("Email");
            dtFinal.Columns.Add("Data de Vencimento");
            dtFinal.Columns.Add("Link");

            DataTable dtSau = GeraListBoletoEmail();


            string ano = DateTime.Now.Year.ToString();
            string mes = DateTime.Now.Month.ToString("d2");

            DataTable dtDocs = GetNomeArqSysdocs(mes,ano);

            int i = 0;

            foreach (DataRow dr in dtSau.Rows)
            {
                dtFinal.Rows.Add();

                DataRow[] link = dtDocs.Select("identificacao = '" + dr[1].ToString() + "' AND nomedoparticipante = '" + dr[0].ToString().Replace("'","").Trim() + "'");

                if (link.Length > 0)
                {
                    dtFinal.Rows[i][0] = dr[0].ToString();
                    dtFinal.Rows[i][1] = dr[1].ToString();
                    dtFinal.Rows[i][2] = dr[3].ToString();
                    dtFinal.Rows[i][3] = dr[4].ToString();
                    dtFinal.Rows[i][4] = dr[5].ToString();
                    dtFinal.Rows[i][5] = "http://integweb.funcesp.com.br/Prod/DirBoletoSau.aspx?bCod=" + link[0][0].ToString();
                }
                else
                {
                    dtFinal.Rows[i][0] = dr[0].ToString();
                    dtFinal.Rows[i][1] = dr[1].ToString();
                    dtFinal.Rows[i][2] = dr[3].ToString();
                    dtFinal.Rows[i][3] = dr[4].ToString();
                    dtFinal.Rows[i][4] = dr[5].ToString();
                    dtFinal.Rows[i][5] = "Arquivo não encontrado no Sysdocs";
                }

                i++;
            }

            return dtFinal;

        }


        public void InsereLinks(int mes, int ano, string idt, string link_docs, string link_short, string nome)
        {
            InsereTBLink(mes, ano, idt, link_docs, link_short, nome);
        }

        public DataTable Gera()
        {
            DataTable dtFinal = new DataTable();

            dtFinal = GetNomeArqSysdocs("09", "2018");


            return dtFinal;

        }
    }
}
