using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb;
using IntegWeb.Financeira;
using IntegWeb.Financeira.Aplicacao.DAL.Tesouraria;
using System.Data;

namespace IntegWeb.Financeira.Aplicacao.BLL.Tesouraria
{
    public class ControleFlagsBLL : ControleFlagsDAL
    {
        public DataTable geraConsultaGrid(int codEmpr)
        {
            DataTable dt = new DataTable();
            dt = consultaGrid(codEmpr);

            return dt;
        }

        public DataTable geraConsultaGridMatr(int codMatr)
        {
            DataTable dt = new DataTable();
            dt = consultaGrid(codMatr);

            return dt;
        }

        public DataTable mostrarGrid(String nome)
        {
            DataTable dt = new DataTable();
            dt = mostrarGridDal(nome);

            return dt;
        }

        public DataTable geraColunaFlag(DataTable dat)
        {
            DataTable dt = dat;

            dt.Columns.Add("Tipo_flag");
            int i = 0;

            foreach(DataRow r in dt.Rows)
            {
                if (r["flag_judicial"].ToString() == "S")
                {
                    dt.Rows[i]["Tipo_flag"] = "Judicial";
                }
                else if (r["flag_insucesso"].ToString() == "S")
                {
                    dt.Rows[i]["Tipo_flag"] = "Insucesso";
                }
                else
                {
                    dt.Rows[i]["Tipo_flag"] = "Flag";
                }

                i++;
            }

            return dt;
        }


    }
}
