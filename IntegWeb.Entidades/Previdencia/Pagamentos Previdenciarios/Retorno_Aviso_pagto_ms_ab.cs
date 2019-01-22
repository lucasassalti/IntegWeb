using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades.Previdencia.Pagamentos;

namespace IntegWeb.Entidades.Previdencia.Pagamentos
{
    public class Retorno_Aviso_pagto_ms_ab
    {
        private List<pagamentos> _pagamentos;
        private List<pagamentosBloco2> _pagamentosbloco2;
        private List<pagamentosBloco3> _pagamentosbloco3;

        public int anqtdeaviso
        {
            get;
            set;
        }
        public string astipoaviso
        {
            get;
            set;
        }

        public List<pagamentos> pagamentos
        {
            get { return _pagamentos; }
            set { _pagamentos = value; }
        }

        public List<pagamentosBloco2> pagamentosbloco2
        {
            get { return _pagamentosbloco2; }
            set { _pagamentosbloco2 = value; }
        }
        public List<pagamentosBloco3> pagamentosbloco3
        {
            get { return _pagamentosbloco3; }
            set { _pagamentosbloco3 = value; }
        }
    }
}
