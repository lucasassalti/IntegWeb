using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Entidades.Cartas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public  class PropostaAdesaoBLL : PropostaAdesaoDAL
    {
        public TipoBeneficioBLL ObjTipoBeneficio { get; set; }
        public TipoServicoBLL ObTipoServico { get; set; }
        public BenefRecusadoBLL ObjBenefRecusado { get; set; }
        public TempoRecusadoBLL ObjTempRecusado { get; set; }

        public new DataTable ListarControles()
        {
            return base.ListarControles(null, null, null, null, 0, "ID_PRADPREV");
        }

        public new DataTable ListarControles(int? codEmpresa, int? codMatricula, DateTime? dtIni, DateTime? dtFim, int? intStatus, string sortParameter)
        {
            return base.ListarControles(codEmpresa, codMatricula, dtIni, dtFim, intStatus, sortParameter);
        }

        public int SelectCount(int codEmpresa, int codMatricula, DateTime dtIni, DateTime dtFim, int intStatus, string sortParameter)
        {
            return base.ListarControles(codEmpresa, codMatricula, dtIni, dtFim, intStatus, sortParameter).Rows.Count;
        }

        public DataTable ListarParticipante()
        {            
            return SelecionarParticipante();
        }

        public DataTable ListarProposta()
        {
            return base.SelecionarProposta();
        }

        public DataTable ListarControle()
        {
            return SelecionarControle();
        }

        public DataTable ListarRelatorio()
        {        
            return SelecionaRelatorio();
        }

        public DataTable ListarStatus()
        {
            return base.ListarStatus();
        }

        public bool ValidaCampos(out string msg, bool tpserv_obrigatorio)
        {

            StringBuilder mensagem = new StringBuilder();
            msg = "";
            bool ret = false;

            if (registro == null)
            {
                mensagem.Append( "Informe o Registro Empregado!\\n");
          
            }

            if (cod_emprs == null)
            {
                mensagem.Append("Informe o Código da Empresa!\\n");

            }

            DataTable dt = SelecionarPropostaPorParticipante();
            if (dt.Rows.Count > 0)
            {
                mensagem.Append("Já existe uma proposta cadastrada para esta empresa e matricula.\\n");
            }
            else
            {

                if (string.IsNullOrEmpty(nome))
                {
                    mensagem.Append("Informe o Nome!\\n");

                }

                if (string.IsNullOrEmpty(perfil))
                {
                    mensagem.Append("Informe o Perfil!\\n");

                }

                if (dt_inclusao == null)
                {
                    mensagem.Append("Informe a Data de Inclusão!\\n");

                }

                if (voluntaria == null)
                {
                    mensagem.Append("Informe % Voluntária!\\n");

                }

                if (tpserv_obrigatorio && id_tpservico == null)
                {
                    mensagem.Append("Informe o Tempo de Serviço!\\n");

                }

                if (id_tpbeneficio == null)
                {
                    mensagem.Append("Informe Tipo de Beneficiário!\\n");

                }

                if (sit_cadastral == 1)
                {
                    if (string.IsNullOrEmpty(desc_motivo_sit))
                    {
                        mensagem.Append("Informe Qual(is) dado(s)!\\n");

                    }
                }
            }
            if (mensagem.Length>0)
            {
                msg = mensagem.ToString();
                ret= true;
            }

            return ret;

        }


        public bool Inserir(out string  msg, out int id, bool tpserv_obrigatorio) {

         if (ValidaCampos(out  msg, tpserv_obrigatorio))
         {
             id = 0;
             return false;
         }
         return InserirProposta(out msg, out  id);
        
        }


        public bool Alterar(out string msg, bool tpserv_obrigatorio)
        {

            if (ValidaCampos(out  msg, tpserv_obrigatorio))
            {
                return false;
            }
            return AlterarProposta(out msg);

        }

        public bool Deletar(out string msg)
        {

            return DeletarProposta(out msg);
        }


        public bool EnviarCap(out string msg)
        {

            return EnviarCapitalizacao(out msg);
        }

        public bool Salvarkit(out string msg)
        {

            return EnviarKit(out msg);
        }

        public new bool ArquivarProposta(out string msg)
        {

            return base.ArquivarProposta(out msg);
        }

        public bool Deferir(out string msg)
        {

            return InserirDeferimento (out msg);
        }
        public bool Indeferir(out string msg)
        {

            return InserirIndeferimento(out msg);
        }

    }
}
