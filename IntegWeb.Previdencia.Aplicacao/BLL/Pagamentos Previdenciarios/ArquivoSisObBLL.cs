using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class ArquivoSisObBLL
    {
        public bool Deletar(string mesano)
        {
            return new ArquivoSisObDAL().Deletar(mesano);
        }

        public bool Inserir(List<ArquivoSisOb> obj)
        {
            return new ArquivoSisObDAL().Inserir(obj);
        }
        public DataTable ListaTodos(ArquivoSisOb obj)
        {
            return new ArquivoSisObDAL().SelectAll(obj);
        }

        public DataTable ListaSysOb (ArquivoSisOb obj)
        {
            return new ArquivoSisObDAL().SelectSysOb(obj);
        }

    }
}
