using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class SimulaBDBLL
    {
        public bool ImportaDados(DataTable dt , string responsavel)
        {
            dt.Columns.Add("DT_INCLUSAO");
            dt.Columns.Add("RESPONSAVEL");

            foreach (DataRow dr in dt.Rows)
            {
                dr["DT_INCLUSAO"] =  DateTime.Now.ToString();
                dr["RESPONSAVEL"] = responsavel;
            }

            var obj = new SimulaBDDAL().ImportaDados(dt);

            if (obj)
            {
                Deletar();
            }

            return obj;

        }

        public DataTable ListaTodos()
        {
            return new SimulaBDDAL().SelectAll();
        }

        public bool Deletar()
        {

            return new SimulaBDDAL().Deletar();
        }
    }
}
