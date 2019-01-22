using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Entidades;

namespace IntegWeb.Saude.Aplicacao
{
    public class CadastroFlagBLL
    {       
        public Resultado Incluir(CadastroFlag objCadastroFlag)
        {
            Resultado retorno = new Resultado();
                       
            retorno = new CadastroFlagDAL().Incluir(objCadastroFlag);

            return retorno;
        }

        public List<CadastroFlag> Listar()
        {
            return new CadastroFlagDAL().Listar();
            
        }

        public List<CadastroFlag> ConsultarFlag(CadastroFlag cons)
        {

            return new CadastroFlagDAL().ConsultarFlag(cons);
        }


    }


}
