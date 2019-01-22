<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="EmailCancelamentoPlano.aspx.cs" Inherits="IntegWeb.Intranet.Web.EmailCancelamentoPlano" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script type="text/javascript">

        var updateProgress = null;
        var Alertado = false;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function Validar_Selecao(ref_out) {

            selecionados = GetCheckedRows();

            if (selecionados.length == 0) {
                alert("Atenção! Selecione ao menos um usuário para continuar");
                return false;
            }
            postbackButtonClick();
            return true;
        }

        
        function _client_side_script() {
            $('#chkSelectAll').change(function () {
                $('.span_checkbox input:checkbox').prop('checked', $('#chkSelectAll').prop('checked'));
                chkSelect_click();
            });
        };

        function chkSelect_click() {

            //Botão Validar:
            $('#ContentPlaceHolder1_btnValidar').attr('disabled', 'disabled');
            if (GetCheckedRows(1).attr('PLANO_DIGNA') != null) {
                if (!Alertado) {
                    Alertado = true;
                    if (!confirm('ATENÇÃO!  Para exclusão do Digna, a solicitação deverá ser efetuada pelo RH da empresa. O participante poderá nos solicitar se comprovar que pediu o cancelamento ao RH a mais de 30 dias.')) {
                        $('.span_checkbox input:checkbox').prop('checked', false);
                        $('#chkSelectAll').prop('checked', false);
                        Alertado = false;
                    }
                }
            } else {
                Alertado = false;
            }
            return false;
        }

        function GetCheckedRows(tipo) {
            filter = ""
            if (tipo != null) {
                filter = "[PLANO_DIGNA='" + tipo + "']";
            }
            return $('.span_checkbox input:checkbox:checked').closest('span' + filter);
        }

    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="full_w">
                <div class="tabelaPagina">
                    <h1>Cancelamento do Plano de Saúde – RN 412</h1>
                    <table>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hfEmpresa" runat="server" />
                                <asp:HiddenField ID="hfMatricula" runat="server" />
                                <asp:HiddenField ID="hfProtocolo" runat="server" />
                            </td>
                            <td>Email: 
                                <asp:TextBox ID="txtEmail" runat="server" Style="text-align: center" Width="230px" />
                                &nbsp;
                                <asp:Button ID="btnEnviarSelecao" runat="server" CssClass="button" Text="Enviar para o email" OnClick="btnEnviarSelecao_Click" OnClientClick="return Validar_Selecao();" />
                                &nbsp;
                                <asp:Button ID="btnImprimirSelecao" runat="server" Text="Visualizar / Imprimir" CssClass="button" OnClick="btnImprimirSelecao_Click" OnClientClick="return Validar_Selecao();" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Update" CssClass="button" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:GridView ID="grdCancelPlano"
                            runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="Não retornou registros"
                            AllowPaging="True"
                            CssClass="Table"
                            ClientIDMode="Static"
                            Font-Size="13px">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Size="12px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Text="" class="span_checkbox" onchange="return chkSelect_click();" PLANO_DIGNA='<%# Eval("PLANO_DIGNA").ToString() %>' />
                                        <asp:HiddenField ID="hidSubMatricula" runat="server" Value='<%# Bind("NUM_SUB_MATRIC") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Usuários" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomeParticipante" runat="server" Text='<%# Bind("NOM_PARTICIP") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plano" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomePlano" runat="server" Text='<%# Bind("des_plano") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" />
        </ContentTemplate>
<%--        <Triggers>
            <asp:PostBackTrigger ControlID="btnImprimirSelecao" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div id="carregando">
                <div class="carregandoTxt">
                    <img src="img/processando.gif" />
                    <br />
                    <br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
