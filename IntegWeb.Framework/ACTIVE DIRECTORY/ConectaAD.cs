using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using System.DirectoryServices;
using System.Data;
using System.Web.Hosting;

namespace IntegWeb.Framework
{
    public class ConectaAD : UsuarioSistema
    {
        private string LDAP = "LDAP://funcesp.com.br/dc=funcesp,dc=com,dc=br";

        public bool AutenticaUsuarioAD()
        {

            // Criar objeto para conectar no LDAP
            DirectoryEntry directoryEntry = new DirectoryEntry(LDAP, "FCESP\\" + login, senha);
            // Criar objeto de consulta no LDAP
            DirectorySearcher deSearcher = new DirectorySearcher(directoryEntry);
            // Define a condicao de filtro
            deSearcher.Filter = "(&(sAMAccountName=" + login + "))";
            try
            {
                // Executar a consulta
                DirectoryEntry userDE = deSearcher.FindOne().GetDirectoryEntry();

                nome = userDE.Properties["cn"].Value.ToString();
                departamento = (userDE.Properties["department"].Value ?? String.Empty).ToString();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable ListarTodosUsuariosAD()
        {

            DataTable table = new DataTable("Results");

            table.Columns.Add("Nome");

            table.Columns.Add("Usuario");

            table.Columns.Add("Email");

            table.Columns.Add("Departamento");

            table.Columns.Add("Data");

            DataRow row;

            try
            {
              
                    DirectoryEntry deRoot = new DirectoryEntry(LDAP);

                    DirectorySearcher deSrch = new DirectorySearcher(deRoot, "(&(objectClass=user)(objectCategory=person))");

                    deSrch.PropertiesToLoad.Add("cn");

                    deSrch.PropertiesToLoad.Add("userPrincipalName");

                    deSrch.PropertiesToLoad.Add("sAMAccountName");

                    deSrch.PropertiesToLoad.Add("department");

                    deSrch.PropertiesToLoad.Add("mail");

                    deSrch.PropertiesToLoad.Add("memberOf");

                    deSrch.Sort.PropertyName = "sAMAccountName";

                    foreach (SearchResult oRes in deSrch.FindAll())
                    {
                        if (oRes.Properties.Contains("memberOf"))
                        {

                            if (oRes.Properties["memberOf"][0].ToString().Contains("DC=funcesp,DC=com,DC=br"))
                            {
                                row = table.NewRow();

                                if (oRes.Properties.Contains("cn"))
                                {
                                    row["Nome"] = oRes.Properties["cn"][0].ToString();
                                }
                                if (oRes.Properties.Contains("sAMAccountName"))
                                {
                                    row["usuario"] = oRes.Properties["sAMAccountName"][0].ToString();
                                }
                                if (oRes.Properties.Contains("department"))
                                {
                                    row["Departamento"] = oRes.Properties["department"][0].ToString();
                                }

                                if (oRes.Properties.Contains("mail"))
                                {

                                    row["Email"] = oRes.Properties["mail"][0].ToString();

                                }

                                row["Data"] = DateTime.Now.ToString();

                                table.Rows.Add(row);
                            }
                        }

                    }
                
            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }


            return table;
        }
    }
}
