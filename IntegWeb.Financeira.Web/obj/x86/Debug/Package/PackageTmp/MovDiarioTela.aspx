<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="MovDiarioTela.aspx.cs" Inherits="IntegWeb.Financeira.Web.MovDiarioTela" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
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
    <div class="MarginGrid">
        <asp:UpdatePanel runat="server" ID="upSys">
            <ContentTemplate>
                <div class="full_w">
                    <div class="tabelaPagina">

                        <h1>Movimentação Diária</h1>

                       <%-- <asp:UpdatePanel runat="server" ID="upSys">--%>
                                <%--<ajax:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="TabPanel1" HeaderText="Importar Arquivo" runat="server" TabIndex="1">
                            <ContentTemplate>--%>
                                    <table>
                                        <tr>
                                            <td>Selecione o arquivo para importar:</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Button ID="btnUpLoad" runat="server" OnClick="btnUpLoad_Click" Text="Importar" OnClientClick="return postbackButtonClick();" CssClass="button" />
                                                <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Exibir Relatório" OnClick="btnRelatorio_Click"  />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>

                                                <h4>
                                                    <asp:Label ID="contador" runat="server" Text="Número de Registros importados :"></asp:Label></h4>
                                                <h4>
                                                    <asp:Label runat="server" ID="StatusLabel" Text="Upload Status: " /></h4>
                                            </td>
                                        </tr>
                                    </table>
                        </div>
                                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Style="display: none;" />
                                <%--</ContentTemplate>
                     </ajax:TabPanel>
                        <ajax:TabPanel ID="TabPanel2" HeaderText="Histórico de Importação" runat="server" TabIndex="2">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td colspan="2">Preencha os campos abaixo para consultar:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Data Inicial Referência (MM/YYYY) </td>
                                        <td>
                                            <asp:TextBox CssClass="date" ID="txtdtIni" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Data Final Referência (MM/YYYY) </td>
                                        <td>
                                            <asp:TextBox CssClass="date" ID="txtdtFin" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <%--<asp:Button ID="btnConsultar" OnClick="btnConsultar_Click" runat="server" Text="Consultar" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="grdImportacao" OnRowCommand="grdImportacao_RowCommand" runat="server" AutoGenerateColumns="false" PageSize="31" EmptyDataText="A consulta não retornou dados">
                                    <Columns>
                                        <asp:BoundField DataField="dt_mov_ref" HeaderText="Data Movimentação" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="responsavel" HeaderText="Responsável" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DT_INCLUSAO" HeaderText="Data da Importação" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DT_INCLUSAO" HeaderText="Horário" DataFormatString="{0:HH:mm:ss}" ItemStyle-HorizontalAlign="Center" />

                                        <asp:BoundField DataField="QTD" HeaderText="QTD Linhas" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField>
                                            <ItemTemplate>

                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" CommandName="Exportar" CommandArgument='<%# Eval("DT_INCLUSAO") %>'>Gerar Excel</asp:LinkButton>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>

                                                <asp:LinkButton ID="LinkButton9" runat="server" CssClass="button" CommandName="Deletar" CommandArgument='<%# Eval("DT_INCLUSAO") %>' OnClientClick="if (!confirm('Atenção\n\nRelatório/Rotina podem não funcionar corretamente ao deletar esta importação.\nDeseja realmente deletar?')) return false;">Deletar</asp:LinkButton>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>--%>

                    </div>
            </ContentTemplate>
                             <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpLoad" />
                </Triggers>
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
    </div>
</asp:Content>
