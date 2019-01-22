using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class GrupoAdmPatrocinadoraBLL : GrupoAdmPatrocinadoraDAL
    {
        public bool ValidaEmpresa(short emp)
        {
            if (emp == 0)
            {
                return false;
            }
            else
            {
                return true;            
            }
        
        }

        public bool ValidaGrupoEmp(string grupo, short emp)
        {
            int valEmp = Search(grupo, emp).Count();

            if (valEmp >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        
        }
    }
}
