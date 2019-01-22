using IntegWeb.Saude.Aplicacao.DAL.Controladoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Controladoria
{
    public class ItemOrcInssBLL: ItemOrcInssDAL
    {
        public DataTable ListaDados()
        {

            return SelectData();
        }


        //public string ValidaCampos()
        //{

        //    StringBuilder str = new StringBuilder();

        //    if (cod_emprs < 1)
        //    {
        //        str.Append("Informe a cod_emprs.\\n");
        //    }

        //    if (cod_emprs_ct < 1)
        //    {
        //        str.Append("Informe o cod_emprs_ct.\\n");
        //    }

        //    if (string.IsNullOrEmpty(cod_natureza_ct))
        //    {
        //        str.Append("Informe a cod_natureza_ct.\\n");
        //    }

        //    if (cod_plano_ct < 1)
        //    {
        //        str.Append("Informe a cod_plano_ct.\\n");
        //    }

        //    if (string.IsNullOrEmpty(consolida_plano))
        //    {
        //        str.Append("Informe o consolida_plano.\\n");
        //    }

       
        //    if (string.IsNullOrEmpty(desc_natureza))
        //    {
        //        str.Append("Informe a desc_natureza.\\n");
        //    }

        //    if (string.IsNullOrEmpty(desc_plano))
        //    {
        //        str.Append("Informe a desc_plano.\\n");
        //    }

        //    if (string.IsNullOrEmpty(item_orcamentario))
        //    {
        //        str.Append("Informe a item_orcamentario.\\n");
        //    }

        //    if (string.IsNullOrEmpty(nom_abrvo_emprs))
        //    {
        //        str.Append("Informe a nom_abrvo_emprs.\\n");
        //    }
        //    if (string.IsNullOrEmpty(nom_rzsoc_emprs))
        //    {
        //        str.Append("Informe a nom_abrvo_emprs.\\n");
        //    }
        //    if (string.IsNullOrEmpty(tipo_item_orc))
        //    {
        //        str.Append("Informe a tipo_item_orc.\\n");
        //    }
        //    return str.ToString();

        //}

        public bool Atualizar(out string msg)
        {

            //msg = ValidaCampos();

            //if (msg.Equals(""))
            //{
                return UpdateData(out msg);
            //}
            //else
            //    return false;


        }
    }
}
