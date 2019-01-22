<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="OuvRelEstouros.aspx.cs" Inherits="IntegWeb.Intranet.Web.OuvRelEstouros" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $(".date").datepicker({
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }

        });
    </script>
    <asp:UpdatePanel ID="upRelEstouro" runat="server">
        <ContentTemplate>
            <div class="full_w">
                <div class="h_title">
                </div>

                <h1>Ouvidoria Relatório Estouro</h1>
                <div class="MarginGrid">
                    <br />
                    <table>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                <asp:Label runat ="server" CssClass="panelLabel" Text="Data Inicial" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                <asp:Label runat="server" CssClass="panelLabel" Text="Data Final" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDtInicioRelEstouro" runat="server" CssClass="date" onkeypress="mascara(this, data);" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" Text="*" ControlToValidate="txtDtInicioRelEstouro"
                                            ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data válida (mm/dd/yyyy)" />
                                        <asp:RegularExpressionValidator ID="RegularDateInicial" ForeColor="Red" runat="server"
                                            ControlToValidate="txtDtInicioRelEstouro" ErrorMessage="Informe uma Data Inicial válida (mm/dd/yyyy)"
                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                            Text="*"></asp:RegularExpressionValidator>
                        
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtFinalRelEstouro" runat="server" CssClass="date" onkeypress="mascara(this, data);" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDataFinal" runat="server" Text="*" ControlToValidate="txtDtFinalRelEstouro"
                                            ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data válida (mm/dd/yyyy)" />
                                        <asp:RegularExpressionValidator ID="RegularDataFinal" ForeColor="Red" runat="server"
                                            ControlToValidate="txtDtFinalRelEstouro" ErrorMessage="Informe uma Data Inicial válida (mm/dd/yyyy)"
                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                            Text="*"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnGeraRelExcel" OnClick="btnGeraRelExcel_Click" runat="server" Text="Gerar Relatório" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div id="carregando">
                <div class="carregandoTxt">
                    <img src="img/processando.gif" />
                    <br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

 </asp:Content>



