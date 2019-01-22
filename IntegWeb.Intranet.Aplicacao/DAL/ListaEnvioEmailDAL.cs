using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data.OracleClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class ListaEnvioEmailDAL
    {
        protected DataTable GeraListBoletoEmail()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objCon = new ConexaoOracle();
            try
            {
                OracleDataAdapter adpt = objCon.ExecutarQueryAdapter("select * from own_portal.vw_boleto_saude_email");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a consulta referente aos boletos da saúde, favor entrar em contato com o administrador do sistema //n" + ex.Message);
            }

            return dt;
        }


        protected DataTable GetNomeArqSysdocs(string mes, string ano)
        {
            DataTable dt = new DataTable();

            String SQL = "";
          // Esse é o certo
            //SQL = SQL + " select idocs_arquivo, substr( identificacao, 1, 3) empresa, substr( identificacao, 4, 8 ) matricula, null, nomedoparticipante, anodacobranca, mesdacobranca, '' as NomePlano, idocs_path";
            SQL = SQL + " select idocs_arquivo, identificacao, null, nomedoparticipante, anodacobranca, mesdacobranca, '' as NomePlano, idocs_path";
            //  SQL = SQL + "select idocs_arquivo";
            SQL = SQL + " from tp_cobsaud";
            SQL = SQL + " where anodacobranca * 100 + mesdacobranca  between " + ano + mes + " and " + ano + mes + " ";
          //  SQL = SQL + " and identificacao =  00100056326 ";
            //SQL = SQL + " and identificacao like '" + filtro.CodigoEmpresa + filtro.Registro.Substring(3, 7) + "%' ";
            //SQL = SQL + " and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
            //SQL = SQL + " ) ";
            //Colocar ordem
          //  SQL = SQL + " order by 1, 6, 7";

            // Criar conexao para executar comando SQL
          //  MySqlConnection conn = new MySqlConnection("Persist Security Info=False;server=10.190.35.143;database=fcesp;uid=sys_crmged;pwd='teste092014';Connection Timeout=120");

            //PROD
           MySqlConnection conn = new MySqlConnection("Persist Security Info=False;server=10.190.35.57;database=fcesp;uid=sys_crmged;pwd='V37qcw11';Connection Timeout=120");

            MySqlCommand cmd = new MySqlCommand(SQL, conn);
            conn.Open();

            cmd.ExecuteNonQuery();

            MySqlDataAdapter mda = new MySqlDataAdapter(cmd);

            mda.Fill(dt);


            conn.Close();


            return dt;
        }

        protected void InsereTBLink(int mes, int ano, string idt, string link_docs, string link_short, string nome)
        {
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.ExecutarNonQuery("insert into OWN_PORTAL.TB_LINK_ENCURTADO VALUES ("+ mes +","+ ano +",'"+ idt +"','" + link_docs + "','"+ link_short +"','" + nome + "')");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);

            }

            obj.Dispose();
        }


    }
}
