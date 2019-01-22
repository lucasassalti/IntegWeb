using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class Menu
    {
        private int m_codigo;
        private string m_nome;
        private string m_link;
        private Sistema m_sistema;
        private short m_nivel;
        private Menu m_menuPai;
        private List<Menu> m_menusFilhos;
        private int m_status;
        private string m_descricao_status;


        public int Codigo
        {
            get { return m_codigo; }
            set { m_codigo = value; }
        }

        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public string Nome
        {
            get { return m_nome; }
            set { m_nome = value; }
        }

        public string DescricaoStatus
        {
            get { return m_descricao_status; }
            set { m_descricao_status = value; }
        }

        public string Link
        {
            get { return m_link; }
            set { m_link = value; }
        }

        public Sistema Sistema
        {
            get { return m_sistema; }
            set { m_sistema = value; }
        }

        public short Nivel
        {
            get { return m_nivel; }
            set { m_nivel = value; }
        }

        public Menu MenuPai
        {
            get { return m_menuPai; }
            set { m_menuPai = value; }
        }

        public List<Menu> MenusFilhos
        {
            get { return m_menusFilhos; }
            set { m_menusFilhos = value; }
        }



        public Menu()
        {
            m_codigo = int.MinValue;
            m_status= int.MinValue;
            m_descricao_status = string.Empty;
            m_nome = string.Empty;
            m_link = string.Empty;
            m_sistema = new Sistema();
            m_nivel = short.MinValue;
            m_menusFilhos = new List<Menu>();


        }


     
    }
}
