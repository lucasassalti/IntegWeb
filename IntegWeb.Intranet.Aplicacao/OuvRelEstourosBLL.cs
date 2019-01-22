using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Intranet.Aplicacao.DAL;
using System.Data;

namespace IntegWeb.Intranet.Aplicacao
{
    public class OuvRelEstourosBLL : OuvRelEstourosDAL
    {

        public DataSet GeraRelatorioGeral(DateTime dtIni, DateTime dtFim)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(AreasRelatorio());
            ds.Tables.Add(QtdEstouro(dtIni, dtFim));
            ds.Tables.Add(RespPeriodo(dtIni, dtFim));

            ds.Tables[0].TableName = "AREAS_REL";
            ds.Tables[1].TableName = "QTD_ESTOURO";
            ds.Tables[2].TableName = "RESP_PERIODO";

            return ds;
        }

        public DataTable GeraEstouro(DateTime? dtIni, DateTime? dtFim)
        {
            DataTable dt = new DataTable();

            dt = EstouroMensal(dtIni, dtFim);

            return dt;
        }

        public DataTable GeraResposta(DateTime? dtIni, DateTime? dtFim)
        {
            DataTable dt = new DataTable();

            dt = RespostaMensal(dtIni, dtFim);

            return dt;
        }

        public int GeraTotalEstouros(DateTime dtIni, DateTime dtFim)
        {
            int previsao = RelEstouro(dtIni, dtFim);

            return previsao;
        }
    }
}
