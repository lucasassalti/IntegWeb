using IntegWeb.Entidades.Carga;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IntegWeb.Framework.Aplicacao
{
    public class CargaDadosBLL
    {

        public CargaDados Listar(string relatorio)
        {
            return new CargaDadosDAL().Consultar(relatorio);
        }

        public bool ImportarDados(CargaDados carga, DataTable dt)
        {
            DataTable dtCloned = dt.Clone();
            // Tipa as colunas da origem ou insere colunas que não existem na origem:
            foreach (CargaDadosDePara cddDe in carga.de_para)
            {
                if (dtCloned.Columns.IndexOf(cddDe.origem_campo) == -1)
                {
                    dtCloned.Columns.Add(cddDe.origem_campo, GetFieldType(cddDe.origem_tipo));
                    dtCloned.Columns[cddDe.origem_campo].DefaultValue = GetDefaultValue(cddDe.destino_valor_padrao);
                }
                else
                {
                    if (dtCloned.Columns[cddDe.origem_campo].DataType != GetFieldType(cddDe.origem_tipo))
                    {
                        dtCloned.Columns[cddDe.origem_campo].DataType = GetFieldType(cddDe.origem_tipo);
                    }
                }
            }

            //Remove colunas na origem que não fazem parte da carga:
            for (int i=0; i < dtCloned.Columns.Count; i++)
            {
                DataColumn dcRemover = dtCloned.Columns[i]; 
                if (carga.GetCampoOrigemDePara(dcRemover.ColumnName) == -1)
                {
                    dtCloned.Columns.Remove(dcRemover);
                    i--;
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }

            Deletar(carga);

            var obj = new CargaDadosDAL().ImportaDados(carga, dtCloned);

            return obj;

        }

        private Type GetFieldType(int? field_type)
        {
            switch (field_type)
            {
                case 1:
                default:
                    return typeof(string);
                case 2:
                    return typeof(Int64);
                case 3:
                    return typeof(decimal);
                case 4:
                    return typeof(DateTime);
            }
        }

        private object GetDefaultValue(string default_value)
        {
            switch (default_value.ToUpper())
            {
                case "SYSUSER":
                    string userName;
                    if (HttpContext.Current.Session["objUser"] != null)
                    {
                        ConectaAD cAD = (ConectaAD)HttpContext.Current.Session["objUser"];
                        userName = cAD.login;
                    }
                    else
                    {
                        userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    }
                    return userName.ToUpper();
                case "SYSDATE":
                    return DateTime.Now;
                default:
                    return null;
            }
        }

        public bool Deletar(CargaDados carga)
        {
            return new CargaDadosDAL().Deletar(carga);
        }

        public DataTable ListarDinamico(CargaDados carga)
        {
            return new CargaDadosDAL().ConsultarPkg(carga);
        }

    }
}
