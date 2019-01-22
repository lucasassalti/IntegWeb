using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Saude.Aplicacao.DAL.Cobranca;
using IntegWeb.Saude.Aplicacao.DAL.Saude;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Cobranca
{
    public class CisaoFusaoBLL : CisaoFusaoDAL 
    {

        public DataTable BuscaCisao(CisaoFusao obj)
        {
            return new CisaoFusaoDAL().ListarCisao(obj);
        }

        public DataTable BuscaCisaoCadastro(CisaoFusao obj)
        {
            return new CisaoFusaoDAL().ListarCisaoCadastro(obj);
        }
        public DataTable BuscaDigito(int emprs, int id)
        {
            return new CisaoFusaoDAL().ListarDigito(emprs ,id);
        }
        public DataTable BuscaEmpresa(int id)
        {
            return new CisaoFusaoDAL().ListarEmpresa(id);
        }
        public DataTable BuscaCisaoLog()
        {
            return new CisaoFusaoDAL().ListarCisaoLog();
        }

        public String buscaEmpresaMatricula(decimal cod_empresa, decimal matricula) 
        {
            return new CisaoFusaoDAL().getEmpresaMatricula(cod_empresa, matricula);
        }

        public bool ValidaCampos(out string mensagem, CisaoFusao objM, bool isUpdate)
        {

            bool ret = false;
            StringBuilder str = new StringBuilder();
            if (objM.Dat_Atualizacao == DateTime.MinValue)
            {
                str.Append("Informe a data atualização válida.\\n");
            }
            if (objM.Dat_Base_Cisao == DateTime.MinValue)
            {
                str.Append("Informe a data base cisão válida.\\n");

            }
            if (objM.Cod_Emprs_Ant == null)
            {
                str.Append("Informe o código da empresa anterior válida.\\n");
            }
            if (objM.Cod_Emprs_Atu == null)
            {
                str.Append("Informe o código da empresa atual válida.\\n");
            }

            if (objM.Num_Rgtro_Emprg_Ant == null)
            {
                str.Append("Informe a matrícula anterior válida.\\n");
            }
            if (objM.Num_Rgtro_Emprg_Atu == null)
            {
                str.Append("Informe a matrícula atual válida.\\n");
            }

            if (objM.Num_Digver_Atu ==null)
            {
                str.Append("Informe o dígito válido.\\n");
            }


            if (str != null && !str.ToString().Equals(""))
            {
                mensagem = str.ToString();
                return false;
            }


            if (isUpdate)
                ret = new CisaoFusaoDAL().Atualizar(out mensagem, objM);
            else

                ret = new CisaoFusaoDAL().Inserir(out mensagem, objM);

            return ret;

        }

        public bool Deletar (out string mensagem, CisaoFusao objM){
    
          return new CisaoFusaoDAL().Deletar(out mensagem, objM);
       }

        public bool Processar(string matricula)
        {

            return new CisaoFusaoDAL().Processar( matricula);
        }
    }
}
