
using IntegWeb.Entidades;
using IntegWeb.Periodico.Aplicacao.DAL;
using IntegWeb.Saude.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao
{
    public class EditoraBLL
    {
        private EditoraDAL _objd;
        public DataTable ListaTodos(Editora obj)
        {
            _objd = new EditoraDAL();
            return _objd.SelectAll(obj);
        }

        public bool ValidaCampos(out string mensagem, Editora objM, bool isUpdate, out int id)
        {

            bool ret = false;
            id = 0;
            _objd = new EditoraDAL();

            if (string.IsNullOrEmpty(objM.nome_editora))
            {
                mensagem = "Informe o Nome da Editora!";
                return false;
            }
            if (string.IsNullOrEmpty(objM.cgc_cpf_editora))
            {
                mensagem = "Informe o CPF/CNPJ da Ediora!";
                return false;
            }
           

            if (string.IsNullOrEmpty(objM.endereco_editora))
            {
                mensagem = "Informe o rua da Editora!";
                return false;
            }

            if (string.IsNullOrEmpty(objM.numero_editora))
            {
                mensagem = "Informe o número do Endereço!";
                return false;
            }

            if (string.IsNullOrEmpty(objM.bairro_editora))
            {
                mensagem = "Informe o bairro da Editora!";
                return false;
            }

            if (string.IsNullOrEmpty(objM.cidade_editora))
            {
                mensagem = "Informe a cidade da Editora!";
                return false;
            }
            if (string.IsNullOrEmpty(objM.uf_editora))
            {
                mensagem = "Informe o UF da Editora!";
                return false;
            }

            if (string.IsNullOrEmpty(objM.cep_editora))
            {
                mensagem = "Informe o CEP da Ediora!";
                return false;
            }


            if (string.IsNullOrEmpty(objM.fone_editora))
            {
                mensagem = "Informe o Telefone!";
                return false;
            }

          

            if (isUpdate)
                ret = _objd.Update(out mensagem, objM);
            else

                ret = _objd.Insert(out mensagem, objM, out id);

            return ret;

        }

        public bool Deletar(Editora objM, out string mensagem)
        {

            return new EditoraDAL().Delete(out mensagem, objM);
        }


    }
}
