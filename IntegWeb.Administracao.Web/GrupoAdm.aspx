<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="GrupoAdm.aspx.cs" Inherits="IntegWeb.Administracao.Web.GrupoAdm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%@ Import Namespace="System.Data" %>

    <asp:UpdatePanel ID="upGrupos" runat="server">
        <ContentTemplate>
            <div class="full_w">

                <div class="h_title">
                    <asp:LinkButton ForeColor="White" ID="btnRastrear" runat="server" Text="Consultar Grupo" OnClick="btnRastrear_Click" CausesValidation="False" />&nbsp|
                    <asp:LinkButton ForeColor="White" ID="btnMovimentacao" runat="server" Text="Histórico de Movimentações" OnClick="btnMovimentacao_Click" CausesValidation="False" />
                </div>



                <div id="Administracao" runat="server">

                    <h3>
                        <asp:Label runat="server" ID="lblGrupo" Text="Nenhum Grupo Selecionado " ForeColor="Red"></asp:Label>

                    </h3>
                    <asp:HiddenField runat="server" ID="hdfGrupo" />
                    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <asp:TabPanel ID="tabGrupo" HeaderText="Grupos" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div id="DivSelectGrupo" runat="server">
                                    <div class="sep"></div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="drpGrupo" runat="server">
                                                        <asp:ListItem Text="Selecione" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Descrição do Grupo" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Área" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Status" Value="3"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrupo" runat="server" Width="300px"></asp:TextBox>
                                                    <asp:Button ID="btnGrupo" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="False" OnClick="btnGrupo_Click" />


                                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />

                                                    <asp:LinkButton ID="lnkInserirGrupo" runat="server" CausesValidation="False" OnClick="lnkInserirGrupo_Click" CssClass="button">Inserir Grupo</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--   </tr>
                                    <tr>
                                        <td colspan="2">--%>
                                    </div>

                                    <div class="sep"></div>
                                    <%--     </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">--%>

                                    <asp:GridView ID="grdGrupo" runat="server" AllowPaging="True" PageSize="15" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="False" OnPageIndexChanging="grdGrupo_PageIndexChanging" OnRowCommand="grdGrupo_RowCommand">

                                        <Columns>
                                            <asp:BoundField DataField="AREA" HeaderText="ÁREA" />
                                            <asp:BoundField DataField="NOME" HeaderText="DESCRIÇÃO" />
                                            <asp:BoundField DataField="descricao_status" HeaderText="STATUS" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Selecionar" Text="Selecionar" CommandName="Selecionar" CausesValidation="False"
                                                        CommandArgument=' <%# Eval("ID_GRUPO_ACESSOS") +","+ Eval("NOME")%>' runat="server" CssClass="button">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Alterar" Text="Editar" CommandName="Editar" CausesValidation="False"
                                                        CommandArgument=' <%# Eval("ID_GRUPO_ACESSOS") +","+ Eval("NOME")%>' runat="server" CssClass="button">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Status" Text="Alterar Status" CommandName="Status" CausesValidation="False"
                                                        CommandArgument=' <%# Eval("ID_GRUPO_ACESSOS") +","+ Eval("NOME")+","+ Eval("id_status")%>' runat="server" CssClass="button">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <%--                </td>
                                    </tr>
                                </table>--%>
                                </div>
                                <div id="DivActionGrupo" runat="server">
                                    <table>
                                        <tr>
                                            <td>Área</td>
                                            <td>
                                                <asp:TextBox ID="txtArea" runat="server" Width="60px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ControlToValidate="txtArea"
                                                    ForeColor="Red" Font-Bold="True" ErrorMessage="Informe a Área!" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nome do Grupo</td>
                                            <td>
                                                <asp:TextBox ID="txtNome" runat="server" Width="300px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" Text="*" ControlToValidate="txtNome"
                                                    ForeColor="Red" Font-Bold="True" ErrorMessage="Informe o Grupo!" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Descrição do Grupo</td>
                                            <td>
                                                <asp:TextBox ID="txtDescricao" runat="server" Width="300px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*" ControlToValidate="txtDescricao"
                                                    ForeColor="Red" Font-Bold="True" ErrorMessage="Informe a Descrição do Grupo!" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="btnVoltarGrupo" runat="server" Text="Voltar" CausesValidation="False" OnClick="btnVoltarGrupo_Click" CssClass="button" />
                                                <asp:Button ID="btnInserirGrupo" runat="server" Text="Salvar" OnClick="btnInserirGrupo_Click" CssClass="button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="vsGrupo" runat="server" ForeColor="Red" ShowMessageBox="True" ShowSummary="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="tabUsuario" HeaderText="Usuarios" runat="server" TabIndex="1">
                            <ContentTemplate>
                                <div id="DivActionUsuario" runat="server">
                                    <table>
                                        <tr>
                                            <td>Grupo</td>
                                            <td>
                                                <asp:DropDownList ID="drpGrupoUsuario" OnSelectedIndexChanged="drpGrupoUsuario_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                <asp:Button ID="BtnSalvarUsuario" CssClass="button" runat="server" Text="Salvar" CausesValidation="false" OnClick="BtnSalvarUsuario_Click" />
                                                <asp:Button ID="btnVoltarUsuario" CssClass="button" runat="server" Text="Voltar" CausesValidation="false" OnClick="btnVoltarUsuario_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">Usuário Vinculado ao Grupo Selecionado:</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <td colspan="2">
                                                    <asp:ListBox ID="lstUsuario" runat="server" Height="220px" Width="856px" SelectionMode="Multiple"></asp:ListBox>
                                                </td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="lbltxt" runat="server" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Lista de Usuários</b>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <b>Usuários no Grupo</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:ListBox ID="lstGrupoUsuario" runat="server" Height="500px" Width="400px" SelectionMode="Multiple"></asp:ListBox>

                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnEnvia" runat="server" Text=">" Width="45px" OnClick="btnEnvia_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnEnviaTodos" runat="server" Text=">>" Width="45px" OnClick="btnEnviaTodos_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnRemove" runat="server" Text="<" Width="45px" OnClick="btnRemove_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnRemoveTodos" runat="server" Text="<<" Width="45px" OnClick="btnRemoveTodos_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstUsuarioAcessos" runat="server" Height="500px" Width="400px" SelectionMode="Multiple"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="DivSelectUsuario" runat="server">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <div style="float: right">
                                                    <asp:LinkButton ID="lnkInserirUsuario" runat="server" CausesValidation="False" OnClick="lnkInserirUsuario_Click" CssClass="button">Inserir Usuário</asp:LinkButton>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grdUsuario" runat="server" AllowPaging="True" PageSize="15" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="False" OnPageIndexChanging="grdUsuario_PageIndexChanging" OnRowCommand="grdUsuario_RowCommand">

                                                    <Columns>
                                                        <asp:BoundField DataField="MATRICULA" HeaderText="IDENTIFICAÇÃO" />
                                                        <asp:BoundField DataField="NOME" HeaderText="NOME" />
                                                        <asp:BoundField DataField="descricao_status" HeaderText="STATUS" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton_Deletar" Text="Alterar Status" CommandName="Status" CssClass="button"
                                                                    CommandArgument=' <%# Eval("MATRICULA")+","+ Eval("ID_STATUS") %>' runat="server" OnClientClick="return confirm('Atenção!! \n\nDeseja Realmente alterar o status do usuário?');">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="tbPagina" HeaderText="Página" runat="server" TabIndex="3">
                            <ContentTemplate>
                                <div id="DivActionPagina" runat="server">
                                    <table>
                                        <tr>
                                            <td>Grupo</td>
                                            <td>
                                                <asp:DropDownList ID="drpGrupoPagina" AutoPostBack="true" OnSelectedIndexChanged="drpGrupoPagina_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                <asp:Button ID="btnSalvarPagina" CssClass="button" runat="server" Text="Salvar" CausesValidation="false" OnClick="btnSalvarPagina_Click" />
                                                <asp:Button ID="btnVoltarPagina" CssClass="button" runat="server" Text="Voltar" CausesValidation="false" OnClick="btnVoltarPagina_Click" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">Página Vinculada ao Grupo Selecionado:</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <td colspan="2">
                                                    <asp:ListBox ID="listVinPagina" runat="server" Height="220px" Width="856px" SelectionMode="Multiple"></asp:ListBox>
                                                </td>
                                            </td>
                                        </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblPagina" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">Filtrar por Sistema:
                                         <asp:DropDownList ID="drpSistema" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSistema_SelectedIndexChanged"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Lista de Páginas ( Menu Pai/ Menu Filho)</b>
                                            </td>
                                            <td></td>
                                            <td>
                                                <b>Páginas no Grupo</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstPagina" runat="server" Height="500px" Width="477px" SelectionMode="Multiple"></asp:ListBox>

                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnEnvio" runat="server" Text=">" Width="45px" OnClick="btnEnvio_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnEnvioTodos" runat="server" Text=">>" Width="45px" OnClick="btnEnvioTodos_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnRemovs" runat="server" Text="<" Width="45px" OnClick="btnRemovs_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnRemovsTodos" runat="server" Text="<<" Width="45px" OnClick="btnRemovsTodos_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lstPaginaAcesso" runat="server" Height="500px" Width="477px" SelectionMode="Multiple"></asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                                        </tr>

                                    </table>

                                  <%--  </td>
                                        </tr>--%>
                                </div>
                                <div id="DivSelectPagina" runat="server">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <div style="float: right">
                                                    <asp:LinkButton ID="lnkInserirPagina" runat="server" CausesValidation="False" OnClick="lnkInserirPagina_Click" CssClass="button">Inserir Página</asp:LinkButton>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView OnRowCommand="grdPagina_RowCommand" OnPageIndexChanging="grPagina_PageIndexChanging" ID="grdPagina" runat="server" AllowPaging="True" PageSize="15" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="False">

                                                    <Columns>
                                                        <asp:BoundField DataField="MENU_PAI" HeaderText="Menu Pai" />
                                                        <asp:BoundField DataField="MENU" HeaderText="Menu" />
                                                        <asp:BoundField DataField="AREA" HeaderText="Area" />
                                                        <asp:BoundField DataField="SISTEMA" HeaderText="Sistema" />
                                                        <asp:BoundField DataField="descricao_status" HeaderText="STATUS" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton_Se" Text="Alterar Status" CommandName="Status" runat="server" CommandArgument=' <%# Eval("ID_MENU")+","+ Eval("ID_STATUS") %>' CssClass="button" OnClientClick="return confirm('Deseja realmente deletar essa página do grupo');">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </div>
                <div id="ConsultarGrupo" runat="server">
                    <h1>Consultar Grupo</h1>
                    <table>
                        <tr>
                            <td>Pesquisar</td>
                            <td>
                                <asp:DropDownList ID="drpConsultaPesquisa" runat="server" OnSelectedIndexChanged="drpConsultaPesquisa_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Selecione" Enabled="true" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Usuário" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Página" Value="2"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="drpConsulta" AutoPostBack="true" runat="server" Enabled="false" OnSelectedIndexChanged="drpConsulta_SelectedIndexChanged">
                                    <asp:ListItem Text="Selecione" Enabled="true" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                            <td>
                                <asp:Button ID="btnConsulta" runat="server" Text="Consultar" OnClick="btnConsulta_Click" CssClass="button" />
                                <asp:Button ID="btnConsultaVoltar" runat="server" Text="Voltar" OnClick="btnConsultaVoltar_Click" CssClass="button" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="grdPesquisa" runat="server" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="False" OnRowCommand="grdPesquisa_RowCommand">

                                    <Columns>
                                        <asp:BoundField DataField="AREA" HeaderText="ÁREA" />
                                        <asp:BoundField DataField="NOME" HeaderText="GRUPO" />
                                        <asp:BoundField DataField="descricao_status" HeaderText="STATUS NO GRUPO" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_Status" Text="Alterar Status" CausesValidation="False"
                                                    CommandArgument=' <%#  Eval("ID_STATUS") +","+ Eval("ID_GRUPO_ACESSOS") %>' CommandName="Status" runat="server" CssClass="button">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>

                    </table>
                </div>

                <div id="Movimentacao" runat="server">
                    <h1>Histórico de Movimentações</h1>
                    <table>
                        <tr>
                            <td>Usuários
                            </td>
                            <td>
                                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="drpMovimentacaoUsuario_SelectedIndexChanged" ID="drpMovimentacaoUsuario" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnMovimentarSalvar" runat="server" Text="Buscar" CssClass="button" OnClick="btnMovimentarSalvar_Click" />
                                <asp:Button ID="btnMovimentarVoltar" runat="server" Text="Voltar" CssClass="button" OnClick="btnMovimentarVoltar_Click" />
                                <asp:Button ID="btnMovimentarExcel" runat="server" Text="Exportar para Excel" CssClass="button" OnClick="btnMovimentarExcel_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="grdMovimentacao" runat="server" OnPageIndexChanging="grdMovimentacao_PageIndexChanging" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="False" AllowPaging="True" PageSize="15">
                        <Columns>
                            <asp:BoundField DataField="area" HeaderText="Área" />
                            <asp:BoundField DataField="descricao_grupo" HeaderText="Grupo" />
                            <asp:BoundField DataField="DT_MOVIMENTACAO" HeaderText="Data" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="DT_MOVIMENTACAO" HeaderText="Horário" DataFormatString="{0:HH:mm:ss}" />
                            <asp:BoundField DataField="descricao_movimentacao" HeaderText="Tipo Movimentacao" />
                            <asp:BoundField DataField="descricao_usuario" HeaderText="Responsável" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnMovimentarExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
