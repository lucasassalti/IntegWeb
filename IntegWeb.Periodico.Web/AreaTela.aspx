<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AreaTela.aspx.cs" Inherits="IntegWeb.Periodico.Web.AreaTela" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">

        <div class="h_title">
        </div>
        <h1>Área</h1>
        <asp:UpdatePanel runat="server" ID="upArea">
            <ContentTemplate>
                <div id="divSelect" class="tabelaPagina" runat="server">
                    <div style="text-align: left">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="drpArea" runat="server">
                                        <asp:ListItem Text="Selecione" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Sigla" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Descrição" Value="2"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtArea" runat="server" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="False" OnClick="btnPesquisar_Click" />


                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />

                                    <asp:LinkButton ID="lnkInserirGrupo" runat="server" CausesValidation="False" OnClick="lnkInserirGrupo_Click" CssClass="button">Inserir Área</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <asp:GridView AutoGenerateColumns="False" EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="gridArea" OnRowCommand="gridArea_RowCommand" runat="server" OnPageIndexChanging="gridArea_PageIndexChanging" PageSize="10" AllowPaging="true">
                        <Columns>
         <%--                   <asp:BoundField HeaderText="Código" DataField="CODIGO" />--%>
                            <asp:BoundField HeaderText="Sigla" DataField="SIGLA" />
                            <asp:BoundField HeaderText="Descrição" DataField="DESCRICAO" />
                            <asp:BoundField HeaderText="Responsável" DataField="RESPONSAVEL" />
                            <asp:BoundField HeaderText="Edifício" DataField="EDIFICIO" />
                            <asp:BoundField HeaderText="Andar" DataField="ANDAR" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CssClass="button" CommandArgument='<%#Eval("ID_AREA")%>' CausesValidation="false" runat="server" CommandName="Atualizar" ButtonType="Link" Text="Atualizar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button4" CssClass="button" CommandArgument='<%#Eval("ID_AREA")%>' CausesValidation="false" runat="server" CommandName="Deletar" ButtonType="Link" Text="Deletar" OnClientClick="if ( !confirm('Deseja realmente deletar?')) return false;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="divAction" runat="server" class="MarginGrid">
                    <div class="formTable">
                        <table>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Sigla</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSigla" runat="server" Width="190px"></asp:TextBox>

                                            </td>
                                            <td>
                                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="button" runat="server" Text="Buscar" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--<td>
                                    <table>
                                        <tr>
                                            <td>Código</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCodigo" runat="server" Width="190px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>--%>
                            </tr>
                            <tr>
                                <td colspan="3">

                                    <table>
                                        <tr>
                                            <td>Descrição</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescricao" runat="server" Width="470px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>


                            </tr>
                            <tr>
                                <td colspan="3">

                                    <table>
                                        <tr>
                                            <td>Responsável</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtResponsavel" runat="server" Width="470px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>


                            </tr>
                            <tr>

                                <td>

                                    <table>
                                        <tr>
                                            <td>Andar</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAndar" runat="server" Width="190px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                                <td>

                                    <table>
                                        <tr>
                                            <td>Edifício</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtEdificio" runat="server" Width="190px"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="3">


                                    <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" CssClass="button" runat="server" Text="Salvar" />
                                    <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CssClass="button" runat="server" Text="Voltar" />

                                </td>


                            </tr>
                        </table>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
