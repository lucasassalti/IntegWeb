<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="DevolucaoProposta.aspx.cs" Inherits="IntegWeb.Previdencia.Web.DevolucaoProposta" %>

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
        <h1>Devolução</h1>
        <asp:UpdatePanel runat="server" ID="upDevolucao">
            <ContentTemplate>

                <div class="MarginGrid">
                    <div id="divSelect" class="tabelaPagina" runat="server">
                        <asp:HiddenField ID="hdId" runat="server" Value="0" />
                        <div style="text-align: left">
                            <table>
                                <tr>
                                    <td>Filtrar por Registro Empregado:  
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPesquisa" runat="server" Width="300px"></asp:TextBox>

                                        <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="false" />


                                        <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" />

                                        <asp:LinkButton ID="lnkInserir" OnClick="lnkInserir_Click" runat="server" CausesValidation="False" CssClass="button">Inserir Devolução</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="tabelaPagina">
                            <asp:GridView AllowPaging="true" AutoGenerateColumns="false"
                                EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdDevolucao" runat="server" OnPageIndexChanging="grdDevolucao_PageIndexChanging" PageSize="10" OnRowCommand="grdDevolucao_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Código da Empresa" DataField="cod_emprs" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Registro Empregado" DataField="Registro" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Nome" DataField="NOM_PARTICIP"  />
                                    <asp:BoundField HeaderText="Data da Devolução" DataField="DT_DEVOLUCAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                    <asp:BoundField HeaderText="Destinatário" DataField="DESTINATARIO" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                                Text="Alterar" CommandName="Alterar" CommandArgument='<%# Eval("ID_PRADPREV")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                Text="Deletar" CommandName="Deletar" CommandArgument='<%# Eval("ID_PRADPREV") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div id="divAction" runat="server" visible="false">
                        <table>


                            <tr>

                                <td>
                                    <table>
                                        <tr>
                                            <td>Registro Empregado</td>

                                            <td>Código da Empresa</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtRegistro" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmpresa" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Nome</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtNome" runat="server" Width="526px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </table>
                        <table>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Data de Devolução</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDevolucao" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Destinatário</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDestinatario" runat="server" Width="526px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Motivo da Devolução</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtMotDevolucao" TextMode="MultiLine" runat="server" Width="526px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>


                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnSalvarCad" OnClick="btnSalvarCad_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" />
                                    <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
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
    </div>
</asp:Content>
