using IntegWeb.Entidades.Financeira.Tesouraria;
using IntegWeb.Financeira.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL
{
    public class MovDiarioBLL
    {

        public bool Inserir(List<MovDiario> obj)
        {
            return new MovDiarioDAL().Inserir(obj);
        }

        public DataTable BuscaImportacao(string dt_ini, string dt_fim)
        {
            return new MovDiarioDAL().ListaImportacao(dt_ini, dt_fim);
        }

        public DataTable BuscaDetalheImportacao(MovDiario obj)
        {
            return new MovDiarioDAL().ListaDetalheImportacao(obj);
        }

        public bool Deletar(MovDiario obj)
        {
            return new MovDiarioDAL().Deletar(obj);
        }

    }
}
