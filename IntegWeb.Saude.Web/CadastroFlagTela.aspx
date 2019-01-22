<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CadastroFlagTela.aspx.cs" Inherits="IntegWeb.Saude.Web.Financeiro.CadastroFlagTela" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:MultiView id="mvwCadastroFlag" runat="server">

    <asp:View ID="vwConsultaFlag" runat="server">

        <h1> Consultar Flag </h1>
        <br />

        <label class="txtCampoForm"> 
                        Empresa: 
                        <asp:TextBox ID="txtCodEmprs" runat="server" Width="30" MaxLength="3"/>
             </label>
         &nbsp;&nbsp;&nbsp;

        <label class="txtCampoForm">
                        Matricula:
                        <asp:TextBox ID="txtNumRgtroEmprg" runat="server" MaxLength="7" Width="40"/>                
        </label>
        &nbsp;&nbsp;&nbsp;
        <label class="txtCampoForm">
                        Representante:
                        <asp:TextBox onfocus="javascript:limparPadrao(this);" onblur="javascript:escreverPadrao(this);" ID="txtNumRgtroRptant" runat="server" MaxLength="7" Text="0" Width="40"/>                
        </label>
        &nbsp;&nbsp;&nbsp;
                 
         <asp:Button ID="btnConsultarFlag" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultarFlag_Click" />
        
        </asp:View>

<script type="text/javascript">

function limparPadrao(campo) {
        if (campo.value == campo.defaultValue) {
        campo.value = "";
        }
}
function escreverPadrao(campo) {
        if (campo.value == "") {
        campo.value = campo.defaultValue;
        } else{
        campo.value = campo.value;
        }
}

</script>
        <asp:GridView 
            id="grvListaFlag" 
            runat="server"
            AutoGenerateColumns="false" 
            OnRowDataBound="grvListaFlag_RowDataBound" 
            OnRowCommand="grvListaFlag_RowCommand"
            EmptyDataText="A consulta não retornou dados">

            <Columns>
                <asp:BoundField HeaderText="Empresa"        DataField="cod_emprs" />
                <asp:BoundField HeaderText="Matrícula"      DataField="num_rgtro_emprg" />
                <asp:BoundField HeaderText="Representante"  DataField="num_idntf_rptant" />
                <asp:BoundField HeaderText="Nome"           DataField="nom_emprg_repres" />
                <asp:BoundField HeaderText="Data Inclusão"  DataField="dt_inclusao" />
                <asp:BoundField HeaderText="Nome Solicitante" DataField="nom_solic_inclusao" />
                <asp:BoundField HeaderText="Flag Judicial"  DataField="flag_judicial" />
            </Columns>
             
        </asp:GridView>
        <br />

          
        



</asp:MultiView>
</asp:Content>
