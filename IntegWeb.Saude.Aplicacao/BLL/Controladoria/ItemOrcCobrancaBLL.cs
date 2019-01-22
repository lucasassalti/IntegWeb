using IntegWeb.Entidades.Saude.Controladoria;
using IntegWeb.Saude.Aplicacao.DAL.Controladoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Controladoria
{
    public class ItemOrcCobrancaBLL
    {
        public DataTable ListaTodos(ItemOrcCobranca obj)
        {
            return new ItemOrcCobrancaDAL().SelectAll(obj); ;
        }

        //public string ValidaCampos(ItemOrcCobranca objM)
        //{

        //    StringBuilder str = new StringBuilder();

        //    if (objM.Cod_Emprs <1)
        //    {
        //        str.Append("Informe a empresa.\\n");
        //    }

        //    if (objM.Cod_Plano < 1)
        //    {
        //        str.Append("Informe o plano.\\n");
        //    }

        //    if (objM.Cod_Tipo_Comp < 1)
        //    {
        //        str.Append("Informe o componente.\\n");
        //    }

        //    if (objM.Cod_Grupo < 1)
        //    {
        //        str.Append("Informe o grupo.\\n");
        //    }

        //    if (string.IsNullOrEmpty(objM.Fcesp_Natureza))
        //    {
        //        str.Append("Informe a natureza.\\n");
        //    }

        //    if (string.IsNullOrEmpty(objM.Suplem_Natureza))
        //    {
        //        str.Append("Informe o suplemento natureza.\\n");
        //    }

        //    if (string.IsNullOrEmpty(objM.Patroc_Natureza))
        //    {
        //        str.Append("Informe o patrocínio natureza.\\n");
        //    }

        //    if (string.IsNullOrEmpty(objM.Compl_Natureza))
        //    {
        //        str.Append("Informe o complemento natureza.\\n");
        //    }
        //    return str.ToString();

        //}

        public bool Atualizar(ItemOrcCobranca objM, out string msg) {

            //msg=ValidaCampos(objM);

            //if (msg.Equals(""))
            //{
                return new ItemOrcCobrancaDAL().Atualizar(out msg, objM);
            //}
            //else
            //    return false;

        
        }
    }
}
