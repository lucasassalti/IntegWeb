using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Framework.Aplicacao
{
    internal class UsuariosPortalDAL
    {

        public List<UsuarioPortal> ListarUsuariosPortal(int? CodEmpresa, int? CodMatricula, int? NumIdntfRptant)
        {
            DataTable dt = new DataTable();
            List<UsuarioPortal> lstUsuariosPortal = new List<UsuarioPortal>();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_EMPRS", CodEmpresa);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG", CodMatricula);
                objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", NumIdntfRptant);

                objConexao.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_USUARIOS_PORTAL.LISTAR_USUARIOS_PORTAL");
                adpt.Fill(dt);
                adpt.Dispose();

                foreach(DataRow drUser in dt.Rows)
                {
                    UsuarioPortal newUser = new UsuarioPortal();
                    newUser.CPF = Util.String2Short(drUser["UID_LDAP"].ToString());
                    newUser.Descricao = drUser["DESCRIPTION"].ToString();
                    newUser.NomeCompleto = drUser["CN"].ToString();
                    newUser.Sobrenome = drUser["SN"].ToString();
                    newUser.Nome = drUser["DISPLAYNAME"].ToString();
                    newUser.Apelido = drUser["GIVENNAME"].ToString();
                    newUser.COD_EMPRS = short.Parse(drUser["COD_EMPRS"].ToString());
                    newUser.NUM_RGTRO_EMPRG = int.Parse(drUser["NUM_RGTRO_EMPRG"].ToString());
                    newUser.NUM_DIGVR_EMPRG = Util.String2Short(drUser["NUM_DIGVR_EMPRG"].ToString());
                    newUser.NUM_IDNTF_DPDTE = Util.String2Int32(drUser["NUM_IDNTF_DPDTE"].ToString());
                    newUser.NUM_IDNTF_RPTANT = int.Parse(drUser["NUM_IDNTF_RPTANT"].ToString());
                    newUser.ENDERECO = drUser["ENDERECO"].ToString();
                    newUser.BAIRRO = drUser["BAIRRO"].ToString();
                    newUser.CEP = drUser["CEP"].ToString();
                    newUser.CIDADE = drUser["CIDADE"].ToString();
                    newUser.ESTADO = drUser["ESTADO"].ToString();
                    lstUsuariosPortal.Add(newUser);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema [CargaDadosDAL.ConsultarPkg]: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return lstUsuariosPortal;
        }

    }
}
