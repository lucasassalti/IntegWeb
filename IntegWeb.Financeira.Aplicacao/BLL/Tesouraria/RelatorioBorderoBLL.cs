using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IntegWeb;
using IntegWeb.Financeira;
using IntegWeb.Financeira.Aplicacao.DAL.Tesouraria;

namespace IntegWeb.Financeira.Aplicacao.BLL.Tesouraria
{
    public class RelatorioBorderoBLL : RelatorioBorderoDAL
    {
        public DataSet geraRelBorderos(DateTime dtInicio, DateTime dtFinal)
        {
            DataSet ds = new DataSet();

            String[] mes = new String[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


            string dtIniFormatada = dtInicio.Day.ToString() + "-" + mes[Convert.ToInt16(dtInicio.Month.ToString())] + "-" + dtInicio.Year.ToString();
            string dtFimFormatada = dtFinal.Day.ToString() + "-" + mes[Convert.ToInt16(dtFinal.Month.ToString())] + "-" + dtFinal.Year.ToString();

            //ds.Tables.Add(geraRelSemRateio(dtIniFormatada, dtFimFormatada));
            //ds.Tables.Add(geraRelComRateio(dtIniFormatada, dtFimFormatada));

            DataTable dtSemRat = geraRelSemRateio(dtIniFormatada, dtFimFormatada);
            DataTable dtComRat = geraRelComRateio(dtIniFormatada, dtFimFormatada);

            //DataTable dtSemRatFormat = geraColunas();
            //DataTable dtComRatFormat = geraColunas();

            //for (int i = 0; i < dtSemRat.Rows.Count; i++)
            //{
            //    dtSemRatFormat.ImportRow(dtSemRat.Rows[i]);
            //}

            //for (int i = 0; i < dtComRat.Rows.Count; i++)
            //{
            //    dtComRatFormat.ImportRow(dtComRat.Rows[i]);
            //}


            ds.Tables.Add(dtSemRat);
            ds.Tables.Add(dtComRat);
            return ds;
        }

        public DataSet geraRelBorderosNumero(string bordInicial, string bordFinal)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(geraRelSemRateioNumero(bordInicial,bordFinal));
            ds.Tables.Add(geraRelComRateioNumero(bordInicial,bordFinal));

            return ds;
        }

        public DataTable geraColunas()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("NUM_BORDERO", typeof(string)));
            dt.Columns.Add(new DataColumn("DT_BORDERO", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("TIPO", typeof(string)));
            dt.Columns.Add(new DataColumn("BANCO_BORD", typeof(string)));
            dt.Columns.Add(new DataColumn("AGENCIA_BORD", typeof(string)));
            dt.Columns.Add(new DataColumn("NUMCONTA_BORD", typeof(string)));
            dt.Columns.Add(new DataColumn("PREFIXO", typeof(string)));
            dt.Columns.Add(new DataColumn("NUM_TITULO", typeof(string)));
            dt.Columns.Add(new DataColumn("PARCELA", typeof(string)));
            dt.Columns.Add(new DataColumn("FORNECEDOR", typeof(string)));
            dt.Columns.Add(new DataColumn("NOME", typeof(string)));
            dt.Columns.Add(new DataColumn("CNPJ", typeof(string)));
            dt.Columns.Add(new DataColumn("DT_EMISSAO", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DT_VENCIMENTO", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("VLR_BRUTO", typeof(decimal)));
            dt.Columns.Add(new DataColumn("VLR_ISS", typeof(decimal)));
            dt.Columns.Add(new DataColumn("VLR_IRRF", typeof(decimal)));
            dt.Columns.Add(new DataColumn("VLR_PIS", typeof(decimal)));
            dt.Columns.Add(new DataColumn("VLR_COFINS", typeof(decimal)));
            dt.Columns.Add(new DataColumn("VLR_CSLL", typeof(decimal)));
            dt.Columns.Add(new DataColumn("VLR_LIQ", typeof(decimal)));
            dt.Columns.Add(new DataColumn("COD_NATUREZA", typeof(string)));
            dt.Columns.Add(new DataColumn("DESC_NATUREZA", typeof(string)));
            dt.Columns.Add(new DataColumn("CONTA_CONTABIL", typeof(string)));
            dt.Columns.Add(new DataColumn("CENTRO_CUSTO", typeof(string)));
            dt.Columns.Add(new DataColumn("PATROCINADOR", typeof(string)));
            dt.Columns.Add(new DataColumn("PLANO", typeof(string)));
            dt.Columns.Add(new DataColumn("SUBMASSA", typeof(string)));
            dt.Columns.Add(new DataColumn("VALOR_RATEADO", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PERCENTUAL_RATEADO", typeof(decimal)));
            dt.Columns.Add(new DataColumn("COD_FORMA_PAG", typeof(string)));
            dt.Columns.Add(new DataColumn("DESC_FORMA_LIQUID", typeof(string)));
            dt.Columns.Add(new DataColumn("COD_BARRAS", typeof(string)));
            dt.Columns.Add(new DataColumn("PROJETO", typeof(string)));



            return dt;

        }

    }
}

