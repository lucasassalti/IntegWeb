using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Entidades
{
    public class Resultado
    {
        private bool m_ok;
        private bool m_alerta;
        private string m_mensagem;
        private long m_codigoCriado;

        public bool Ok
        {
            get { return m_ok; }
        }

        public bool Alerta
        {
            get { return m_alerta; }
        }

        public string Mensagem
        {
            get { return m_mensagem; }
        }

        public long CodigoCriado
        {
            get { return m_codigoCriado; }
        }

        public Resultado()
        {
            m_ok = false;
            m_alerta = false;
            m_mensagem = string.Empty;
            m_codigoCriado = 0;
        }

        public Resultado(bool ok)
        {
            m_ok = ok;
            m_alerta = false;
            m_mensagem = string.Empty;
            m_codigoCriado = 0;
        }

        public void Alert(string mensagemAlerta)
        {
            m_ok = false;
            m_alerta = true;
            m_mensagem = mensagemAlerta;
        }

        public void Erro(string mensagemErro)
        {
            m_ok = false;
            m_alerta = false;
            m_mensagem = mensagemErro;
        }

        public void Sucesso()
        {
            m_ok = true;
            m_alerta = false;
            m_mensagem = "";
        }

        public void Sucesso(string mensagemSucesso)
        {
            m_ok = true;
            m_alerta = false;
            m_mensagem = mensagemSucesso;
        }

        public void Sucesso(string mensagemSucesso, long codigo)
        {
            m_ok = true;
            m_alerta = false;
            m_mensagem = mensagemSucesso;
            m_codigoCriado = codigo;
        }
    }
}
