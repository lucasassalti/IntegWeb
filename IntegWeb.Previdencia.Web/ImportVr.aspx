<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ImportVr.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ImportVr" %>

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
    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Importar para o VR</h1>
        <div class="MarginGrid">
            <asp:UpdatePanel runat="server" ID="upImport">
                <ContentTemplate>
                    <div class="tabelaPagina" id="divSelect" runat="server">

                        <table>

                            <tr>
                                <td>Digite o Nº Empresa</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Digite o Nº Matrícula</td>
                                <td>
                                    <asp:TextBox ID="txtCodMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" runat="server" CssClass="button" Text="Pesquisar   " />

                                    <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" runat="server" CssClass="button" Text="Limpar" />


                                </td>
                            </tr>
                        </table>

                        <div id="divParticip" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td>Nome do Participante</td>
                                    <td>
                                        <asp:TextBox ID="TxtNom" Width="500px" Enabled="false" runat="server"></asp:TextBox></td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hdNumPartif" Value="0" runat="server" />
                            <asp:HiddenField ID="hdSeq" Value="0" runat="server" />
                            <asp:HiddenField ID="hdMatricula" runat="server" />
                            <asp:HiddenField ID="HdEmpresa" runat="server" />
                            <div class="formTable">

                                <asp:GridView AllowPaging="true" AutoGenerateColumns="false" PageSize="10" OnRowCommand="grdSrc_RowCommand"
                                    EmptyDataText="A consulta não retornou dados" CssClass="Table" OnPageIndexChanging="grdSrc_PageIndexChanging" ClientIDMode="Static" ID="grdSrc" runat="server">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sequência" DataField="num_sqncl_prc" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Num Matri" DataField="num_matr_partf" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Data do Cálculo" DataField="dth_processamento" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                        <asp:BoundField HeaderText="Nº Processo" DataField="nro_processo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Nº da Pasta" DataField="nro_pasta" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Situação" DataField="DESC_MR" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Último Processo Executado" DataField="desc_processo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkImportar" runat="server" CssClass="button"
                                                    Text="Importar para o VR" CommandName="Importar" CommandArgument='<%# Eval("num_sqncl_prc") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                    Text="Desfazer Importação" CommandName="Desfazer" CommandArgument='<%# Eval("num_sqncl_prc") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>


                    </div>
                    <div class="tabelaPagina" id="divInsert" runat="server">
                        <table>
                            <tr>
                                <td>Escolha abaixo o tipo de Importação:
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="ckTipImportacao" RepeatDirection="Vertical" runat="server">
                                        <asp:ListItem Text="Cível" Value="C" />
                                        <asp:ListItem Text="IGPDI" Value="I" />
                                        <asp:ListItem Text="Trabalhista" Value="T" />
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnImportar" runat="server" CssClass="button" OnClick="btnImportar_Click" Text="Importar para o VR" />
                                    <asp:Button ID="btnVoltar" runat="server" CssClass="button" OnClick="btnVoltar_Click" Text="Voltar á Tela Inicial" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
