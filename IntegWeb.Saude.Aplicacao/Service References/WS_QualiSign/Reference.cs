﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegWeb.Saude.Aplicacao.WS_QualiSign {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WS_QualiSign.qswsdeSoap")]
    public interface qswsdeSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Login(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login", ReplyAction="*")]
        System.Threading.Tasks.Task<string> LoginAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarDocumento", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarDocumento(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarDocumento", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ListarDocumentoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarDocPorReferencia", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarDocPorReferencia(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarDocPorReferencia", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ListarDocPorReferenciaAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoCadastroDOCUMENTO", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AutoCadastroDOCUMENTO(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoCadastroDOCUMENTO", ReplyAction="*")]
        System.Threading.Tasks.Task<string> AutoCadastroDOCUMENTOAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LoginServico", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LoginServico(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LoginServico", ReplyAction="*")]
        System.Threading.Tasks.Task<string> LoginServicoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoCadastroContrateJa", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AutoCadastroContrateJa(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoCadastroContrateJa", ReplyAction="*")]
        System.Threading.Tasks.Task<string> AutoCadastroContrateJaAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultarIDUsuario", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ConsultarIDUsuario(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultarIDUsuario", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ConsultarIDUsuarioAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DocumentoCancelar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DocumentoCancelar(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DocumentoCancelar", ReplyAction="*")]
        System.Threading.Tasks.Task<string> DocumentoCancelarAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DocumentoExcluir", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DocumentoExcluir(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DocumentoExcluir", ReplyAction="*")]
        System.Threading.Tasks.Task<string> DocumentoExcluirAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvListarEmailValido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EvListarEmailValido(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvListarEmailValido", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EvListarEmailValidoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObterP7SDoc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ObterP7SDoc(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObterP7SDoc", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ObterP7SDocAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultarWorkflowDoc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ConsultarWorkflowDoc(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultarWorkflowDoc", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ConsultarWorkflowDocAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GerarCarimboTempo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GerarCarimboTempo(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GerarCarimboTempo", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GerarCarimboTempoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultarCarimboTempo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ConsultarCarimboTempo(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultarCarimboTempo", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ConsultarCarimboTempoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoCadastroRelacionamentos", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AutoCadastroRelacionamentos(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoCadastroRelacionamentos", ReplyAction="*")]
        System.Threading.Tasks.Task<string> AutoCadastroRelacionamentosAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarLogAuditoria", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarLogAuditoria(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarLogAuditoria", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ListarLogAuditoriaAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarAtributosRelacionamento", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarAtributosRelacionamento(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarAtributosRelacionamento", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ListarAtributosRelacionamentoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarAtributosRepresentante", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarAtributosRepresentante(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarAtributosRepresentante", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ListarAtributosRepresentanteAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarRepresentantesRelacionamento", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListarRepresentantesRelacionamento(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarRepresentantesRelacionamento", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ListarRepresentantesRelacionamentoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvExcluirEmailValido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EvExcluirEmailValido(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvExcluirEmailValido", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EvExcluirEmailValidoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvListarLogsEmailValido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EvListarLogsEmailValido(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvListarLogsEmailValido", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EvListarLogsEmailValidoAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EV_DetalhesProcessamento_Listar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet EV_DetalhesProcessamento_Listar(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EV_DetalhesProcessamento_Listar", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> EV_DetalhesProcessamento_ListarAsync(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvEnviarEmailValido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EvEnviarEmailValido(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EvEnviarEmailValido", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EvEnviarEmailValidoAsync(string xml);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface qswsdeSoapChannel : IntegWeb.Saude.Aplicacao.WS_QualiSign.qswsdeSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class qswsdeSoapClient : System.ServiceModel.ClientBase<IntegWeb.Saude.Aplicacao.WS_QualiSign.qswsdeSoap>, IntegWeb.Saude.Aplicacao.WS_QualiSign.qswsdeSoap {
        
        public qswsdeSoapClient() {
        }
        
        public qswsdeSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public qswsdeSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public qswsdeSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public qswsdeSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Login(string xml) {
            return base.Channel.Login(xml);
        }
        
        public System.Threading.Tasks.Task<string> LoginAsync(string xml) {
            return base.Channel.LoginAsync(xml);
        }
        
        public string ListarDocumento(string xml) {
            return base.Channel.ListarDocumento(xml);
        }
        
        public System.Threading.Tasks.Task<string> ListarDocumentoAsync(string xml) {
            return base.Channel.ListarDocumentoAsync(xml);
        }
        
        public string ListarDocPorReferencia(string xml) {
            return base.Channel.ListarDocPorReferencia(xml);
        }
        
        public System.Threading.Tasks.Task<string> ListarDocPorReferenciaAsync(string xml) {
            return base.Channel.ListarDocPorReferenciaAsync(xml);
        }
        
        public string AutoCadastroDOCUMENTO(string xml) {
            return base.Channel.AutoCadastroDOCUMENTO(xml);
        }
        
        public System.Threading.Tasks.Task<string> AutoCadastroDOCUMENTOAsync(string xml) {
            return base.Channel.AutoCadastroDOCUMENTOAsync(xml);
        }
        
        public string LoginServico(string xml) {
            return base.Channel.LoginServico(xml);
        }
        
        public System.Threading.Tasks.Task<string> LoginServicoAsync(string xml) {
            return base.Channel.LoginServicoAsync(xml);
        }
        
        public string AutoCadastroContrateJa(string xml) {
            return base.Channel.AutoCadastroContrateJa(xml);
        }
        
        public System.Threading.Tasks.Task<string> AutoCadastroContrateJaAsync(string xml) {
            return base.Channel.AutoCadastroContrateJaAsync(xml);
        }
        
        public string ConsultarIDUsuario(string xml) {
            return base.Channel.ConsultarIDUsuario(xml);
        }
        
        public System.Threading.Tasks.Task<string> ConsultarIDUsuarioAsync(string xml) {
            return base.Channel.ConsultarIDUsuarioAsync(xml);
        }
        
        public string DocumentoCancelar(string xml) {
            return base.Channel.DocumentoCancelar(xml);
        }
        
        public System.Threading.Tasks.Task<string> DocumentoCancelarAsync(string xml) {
            return base.Channel.DocumentoCancelarAsync(xml);
        }
        
        public string DocumentoExcluir(string xml) {
            return base.Channel.DocumentoExcluir(xml);
        }
        
        public System.Threading.Tasks.Task<string> DocumentoExcluirAsync(string xml) {
            return base.Channel.DocumentoExcluirAsync(xml);
        }
        
        public string EvListarEmailValido(string xml) {
            return base.Channel.EvListarEmailValido(xml);
        }
        
        public System.Threading.Tasks.Task<string> EvListarEmailValidoAsync(string xml) {
            return base.Channel.EvListarEmailValidoAsync(xml);
        }
        
        public string ObterP7SDoc(string xml) {
            return base.Channel.ObterP7SDoc(xml);
        }
        
        public System.Threading.Tasks.Task<string> ObterP7SDocAsync(string xml) {
            return base.Channel.ObterP7SDocAsync(xml);
        }
        
        public string ConsultarWorkflowDoc(string xml) {
            return base.Channel.ConsultarWorkflowDoc(xml);
        }
        
        public System.Threading.Tasks.Task<string> ConsultarWorkflowDocAsync(string xml) {
            return base.Channel.ConsultarWorkflowDocAsync(xml);
        }
        
        public string GerarCarimboTempo(string xml) {
            return base.Channel.GerarCarimboTempo(xml);
        }
        
        public System.Threading.Tasks.Task<string> GerarCarimboTempoAsync(string xml) {
            return base.Channel.GerarCarimboTempoAsync(xml);
        }
        
        public string ConsultarCarimboTempo(string xml) {
            return base.Channel.ConsultarCarimboTempo(xml);
        }
        
        public System.Threading.Tasks.Task<string> ConsultarCarimboTempoAsync(string xml) {
            return base.Channel.ConsultarCarimboTempoAsync(xml);
        }
        
        public string AutoCadastroRelacionamentos(string xml) {
            return base.Channel.AutoCadastroRelacionamentos(xml);
        }
        
        public System.Threading.Tasks.Task<string> AutoCadastroRelacionamentosAsync(string xml) {
            return base.Channel.AutoCadastroRelacionamentosAsync(xml);
        }
        
        public string ListarLogAuditoria(string xml) {
            return base.Channel.ListarLogAuditoria(xml);
        }
        
        public System.Threading.Tasks.Task<string> ListarLogAuditoriaAsync(string xml) {
            return base.Channel.ListarLogAuditoriaAsync(xml);
        }
        
        public string ListarAtributosRelacionamento(string xml) {
            return base.Channel.ListarAtributosRelacionamento(xml);
        }
        
        public System.Threading.Tasks.Task<string> ListarAtributosRelacionamentoAsync(string xml) {
            return base.Channel.ListarAtributosRelacionamentoAsync(xml);
        }
        
        public string ListarAtributosRepresentante(string xml) {
            return base.Channel.ListarAtributosRepresentante(xml);
        }
        
        public System.Threading.Tasks.Task<string> ListarAtributosRepresentanteAsync(string xml) {
            return base.Channel.ListarAtributosRepresentanteAsync(xml);
        }
        
        public string ListarRepresentantesRelacionamento(string xml) {
            return base.Channel.ListarRepresentantesRelacionamento(xml);
        }
        
        public System.Threading.Tasks.Task<string> ListarRepresentantesRelacionamentoAsync(string xml) {
            return base.Channel.ListarRepresentantesRelacionamentoAsync(xml);
        }
        
        public string EvExcluirEmailValido(string xml) {
            return base.Channel.EvExcluirEmailValido(xml);
        }
        
        public System.Threading.Tasks.Task<string> EvExcluirEmailValidoAsync(string xml) {
            return base.Channel.EvExcluirEmailValidoAsync(xml);
        }
        
        public string EvListarLogsEmailValido(string xml) {
            return base.Channel.EvListarLogsEmailValido(xml);
        }
        
        public System.Threading.Tasks.Task<string> EvListarLogsEmailValidoAsync(string xml) {
            return base.Channel.EvListarLogsEmailValidoAsync(xml);
        }
        
        public System.Data.DataSet EV_DetalhesProcessamento_Listar(string xml) {
            return base.Channel.EV_DetalhesProcessamento_Listar(xml);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> EV_DetalhesProcessamento_ListarAsync(string xml) {
            return base.Channel.EV_DetalhesProcessamento_ListarAsync(xml);
        }
        
        public string EvEnviarEmailValido(string xml) {
            return base.Channel.EvEnviarEmailValido(xml);
        }
        
        public System.Threading.Tasks.Task<string> EvEnviarEmailValidoAsync(string xml) {
            return base.Channel.EvEnviarEmailValidoAsync(xml);
        }
    }
}
