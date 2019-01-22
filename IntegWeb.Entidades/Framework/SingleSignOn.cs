using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Framework
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Singlesignon
    {

        private int codErroField;
        private string msgErroField;
        private long cPFField;
        private short empresaField;
        private int prontuarioField;
        private int matric_previdenciaField;
        private int representanteField;
        private string targetField;
        private short[] listaEmpresasField;
        private string[] listaGruposField;

        /// <remarks/>
        public int CodErro
        {
            get
            {
                return this.codErroField;
            }
            set
            {
                this.codErroField = value;
            }
        }

        /// <remarks/>
        public string MsgErro
        {
            get
            {
                return this.msgErroField;
            }
            set
            {
                this.msgErroField = value;
            }
        }

        /// <remarks/>
        public long CPF
        {
            get
            {
                return this.cPFField;
            }
            set
            {
                this.cPFField = value;
            }
        }

        /// <remarks/>
        public short Empresa
        {
            get
            {
                return this.empresaField;
            }
            set
            {
                this.empresaField = value;
            }
        }

        /// <remarks/>
        public int Prontuario
        {
            get
            {
                return this.prontuarioField;
            }
            set
            {
                this.prontuarioField = value;
            }
        }

        /// <remarks/>
        public int Matric_previdencia
        {
            get
            {
                return this.matric_previdenciaField;
            }
            set
            {
                this.matric_previdenciaField = value;
            }
        }

        /// <remarks/>
        public int Representante
        {
            get
            {
                return this.representanteField;
            }
            set
            {
                this.representanteField = value;
            }
        }

        /// <remarks/>
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute()]
        [System.Xml.Serialization.XmlArrayItemAttribute("Empresa", IsNullable = false)]
        public short[] ListaEmpresas
        {
            get
            {
                return this.listaEmpresasField;
            }
            set
            {
                this.listaEmpresasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Grupo", IsNullable = false)]
        public string[] ListaGrupos
        {
            get
            {
                return this.listaGruposField;
            }
            set
            {
                this.listaGruposField = value;
            }
        }
    }
}
