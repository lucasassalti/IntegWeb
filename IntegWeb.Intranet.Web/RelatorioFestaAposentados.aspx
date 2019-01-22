<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="RelatorioFestaAposentados.aspx.cs" Inherits="IntegWeb.Intranet.Web.RelatorioFestaAposentados" %>

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
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>



    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="tabelaPagina">
                <div class="full_w">
                    <table>
                        <tr>
                            <td>
                                <h1>Relatório da Festa dos Aposentados</h1>
                            </td>
                        </tr>
                    </table>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblDataInicial" runat="server" Text="Data Inicial:" CssClass="panelLabel"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtDataInicial" runat="server" CssClass="date" onkeypress="mascara(this, data);" MaxLength="10"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" Text="*" ControlToValidate="txtDataInicial"
                                    ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data válida (mm/dd/yyyy)" />
                                <asp:RegularExpressionValidator ID="RegularDateInicial" ForeColor="Red" runat="server"
                                    ControlToValidate="txtDataInicial" ErrorMessage="Informe uma Data Inicial válida (mm/dd/yyyy)"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                    Text="*"></asp:RegularExpressionValidator>--%>
                            </asp:TableCell><asp:TableCell>
                                <asp:Label ID="lblDataFinal" runat="server" Text="Data Final:" CssClass="panelLabel"></asp:Label>
                            </asp:TableCell><asp:TableCell>
                                <asp:TextBox ID="txtDataFinal" runat="server" CssClass="date" onkeypress="mascara(this, data);" MaxLength="10"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ControlToValidate="txtDataFinal"
                                    ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data válida (mm/dd/yyyy)" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server"
                                    ControlToValidate="txtDataFinal" ErrorMessage="Informe uma Data Inicial válida (mm/dd/yyyy)"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                    Text="*"></asp:RegularExpressionValidator>--%>
                            </asp:TableCell><asp:TableCell>
                                <asp:Label ID="lblTipoRel" runat="server" Text="Forma de pagamento:" CssClass="panelLabel"></asp:Label>
                            </asp:TableCell><asp:TableCell>
                                <asp:DropDownList ID="ddlTipoRel" runat="server" Width="150px"></asp:DropDownList>
                            </asp:TableCell><asp:TableCell>
                                <asp:Button ID="btnGerarRel" runat="server" Text="Gerar relatório" CssClass="button" OnClick="btnGerarRel_Click"/>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:GridView ID="grdRelFesta" runat="server"></asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    
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
