using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class Sistema
    {
        private byte m_codigo;
        private string m_nome;
        public int status { get; set; }
        public string descricao_status { get; set; }

        public byte Codigo
        {
            get { return m_codigo; }
            set { m_codigo = value; }
        }

        public string Nome
        {
            get { return m_nome; }
            set { m_nome = value; }
        }

        public Sistema()
        {
            m_codigo = byte.MinValue;
            m_nome = string.Empty;
        }
    }
}
