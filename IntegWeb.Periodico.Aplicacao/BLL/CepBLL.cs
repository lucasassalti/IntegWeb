using IntegWeb.Periodico.Aplicacao.DAL;
using IntegWeb.Saude.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntegWeb.Saude.Aplicacao
{
    public class CepBLL 
    {
        private DataTable _dt;
        private CepDAL objd;
        public DataTable ValidaCep(string cep, out string  mensagem)
        { 
            _dt = new DataTable();
            objd = new CepDAL();
            if (!string.IsNullOrEmpty(cep))
            {
                _dt = objd.SelectCep(cep);

                if (_dt.Rows.Count > 0)
                    mensagem = "";
                else
                    mensagem = "A consulta não retornou dados!";
            }
            else
            {
                mensagem = "Preencha o campo cep!";
            }
            return _dt;
        }
    }
}
