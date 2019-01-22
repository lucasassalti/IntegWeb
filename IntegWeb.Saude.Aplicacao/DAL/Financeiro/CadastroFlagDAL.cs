using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Framework;
using IntegWeb.Entidades;
using System.Data.OracleClient;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    internal class CadastroFlagDAL
    {
        public Resultado Incluir(CadastroFlag objCadastroFlag)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {

                db.AdicionarParametro("P_COD_EMPRS", objCadastroFlag.Cod_emprs);
                db.AdicionarParametro("P_NUM_RGTRO_EMPRG", objCadastroFlag.Num_rgtro_emprg);
                db.AdicionarParametro("P_NUM_IDNTF_RPTANT", objCadastroFlag.Num_idntf_rptant);
                db.AdicionarParametro("P_NOM_EMPRG_REPRES", objCadastroFlag.Nom_emprg_repres);
                db.AdicionarParametro("P_DT_INCLUSAO", objCadastroFlag.Dt_inclusao);
                db.AdicionarParametro("P_NOM_SOLIC_INCLUSAO", objCadastroFlag.Nom_solic_inclusao);
                db.AdicionarParametro("P_FLAG_JUDICIAL", objCadastroFlag.Flag_judicial);
                
                //se não for necessário capturar o código de saída
                db.ExecutarDML("PKG_CLASSE_PRESTADOR.INSERIR");

               
            }

            return retorno;
        }


        public List<CadastroFlag> Listar()
        {
            List<CadastroFlag> lista = new List<CadastroFlag>();
            CadastroFlag objItem;

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametroCursor("DADOS");

                using (OracleDataReader leitor = db.ObterLeitor("PKG_CADASTRO_FLAG.LISTAR"))
                {
                    while (leitor.Read())
                    {
                        objItem = new CadastroFlag();

                        objItem.Cod_emprs = Convert.ToInt32(leitor["COD_EMPRS"]);
                        objItem.Num_rgtro_emprg = Convert.ToInt32(leitor["Num_rgtro_emprg"]);
                        objItem.Num_idntf_rptant = Convert.ToInt32(leitor["Num_idntf_rptant"]);
                        objItem.Nom_emprg_repres = leitor["nom_emprg_repres"].ToString();
                        objItem.Dt_inclusao = Convert.ToDateTime(leitor["Dt_inclusao"]);

                        lista.Add(objItem);
                    }
                }
            }
            return lista;
        }

        public List<CadastroFlag> ConsultarFlag(CadastroFlag cons)
        {
            //Preparado o Objeto de Retorno
            //CadastroFlag objItem = new CadastroFlag();

            //Instanciado a classe padrão de conexão Oracle
            ConexaoOracle objConexao = new ConexaoOracle();

            List<CadastroFlag> list = new List<CadastroFlag>();

            //Adicionado todos os parâmetros da package/procedure
            objConexao.AdicionarParametro("P_COD_EMPRS", cons.Cod_emprs);
            objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG", cons.Num_rgtro_emprg);
            objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", cons.Num_idntf_rptant);
            objConexao.AdicionarParametroCursor("DADOS");

            //Executa a procedure e retorna CURSOR dentro de LEITOR
            OracleDataReader leitor = objConexao.ObterLeitor("PKG_CADASTRO_FLAG.CONSULTAR");

            if (leitor.Read())
            {
                //Mapeamento Objeto-Relacional (ORM)
                cons.Cod_emprs = Convert.ToInt32(leitor["COD_EMPRS"]);
                cons.Num_rgtro_emprg = Convert.ToInt32(leitor["NUM_RGTRO_EMPRG"]);
                cons.Num_idntf_rptant = Convert.ToInt32(leitor["NUM_IDNTF_RPTANT"]);
                cons.Nom_emprg_repres = Convert.ToString(leitor["NOM_EMPRG_REPRES"]);
                cons.Dt_inclusao = Convert.ToDateTime(leitor["DT_INCLUSAO"]);
                cons.Flag_judicial = Convert.ToString(leitor["FLAG_JUDICIAL"]);

                list.Add(cons);
            }

            //Fecha conexão e faz dispose dos objetos envolvidos.
            //Lembrar do Garbage Collector
            leitor.Dispose();
            objConexao.Dispose();

            return list;
        }


    }
}
