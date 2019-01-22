using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Saude.Aplicacao.DAL.Saude;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Saude
{
    public class CotaBLL
    {
        public List<Cota> ConsultarCota(string cota)
        {
            return new CotaDAL().ConsultarCota(cota);
        }
        public int GeraCota(string login, string dat_mov) {

            return new CotaDAL().GeraCota(login,dat_mov);
        }

        public DataSet ListaExcel(int id)
        {
            return new CotaDAL().ListaExcel(id);
        }
    }
}
