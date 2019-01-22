using IntegWeb.Financeira.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL
{
    public class ImportaBaseSerasaBLL
    {
        public bool ImportaDados(DataTable dt , string responsavel)
        {

            DataTable dtCloned = dt.Clone();
            dtCloned.Columns.Add("DESC_USER", typeof(string));            
            dtCloned.Columns.Add("DAT_IMPORTACAO", typeof(DateTime));
            dtCloned.Columns[5].DataType = typeof(DateTime);
            dtCloned.Columns[6].DataType = typeof(Decimal);

            DateTime DataHora = DateTime.Now;

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
          

            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }

            foreach (DataRow row in dtCloned.Rows)
            {
                row["DESC_USER"]        = userName;
                row["DAT_IMPORTACAO"]   = DataHora;
                //row["VALOR"] = row["VALOR"].ToString().Replace(",", ".");                
            }

            Deletar();

            var obj = new ImportaBaseSerasaDAL().ImportaDados(dtCloned);

            return obj;

        }

        public int GeraRemessa()
        {

            int CodRemessa = new ImportaBaseSerasaDAL().GeraRemessa();

            return CodRemessa;

        }

        public DataTable ListaTodos()
        {
            return new ImportaBaseSerasaDAL().SelectAll();
        }

        public DataTable ListaRemessas()
        {
            return new ImportaBaseSerasaDAL().SelectRemessas();
        }

        public bool Deletar()
        {
            return new ImportaBaseSerasaDAL().Deletar();
        }

        public bool Atualizar(int NumRemessa, int NumRemessaNova)
        {
            return new ImportaBaseSerasaDAL().Atualizar(NumRemessa, NumRemessaNova);
        }


        public bool Deletar_Remessa(int NumRemessa)
        {
            return new ImportaBaseSerasaDAL().Deletar_Remessa(NumRemessa);
        }
    }
}
