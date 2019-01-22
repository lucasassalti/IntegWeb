<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="UsuarioTela.aspx.cs" Inherits="IntegWeb.Administracao.Web.UsuarioTela" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upUsuarioAd" runat="server">

        <ContentTemplate>
            <div class="full_w">

                <div class="h_title">
                    <asp:LinkButton ID="lnkHistorico" ForeColor="White" runat="server" OnClick="lnkHistorico_Click">Consultar Histórico do Usuário</asp:LinkButton>
                </div>
                <div id="divMovimentacao" runat="server">
                    <div id="divSelect" runat="server">
                        <h2>Importarção de Usuários Active Directory 
                        </h2>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="drpUsuario" runat="server">
                                        <asp:ListItem Text="Selecione" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Login" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Nome" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="E-mail" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Departamento" Value="4"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtUsuario" runat="server" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" />
                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />
                                    <asp:Button ID="btnImportar" runat="server" Text="Importar" CssClass="button" OnClick="btnImportar_Click" />
                                </td>
                            </tr>
                        </table>

                        <h3>Usuários Importados
                        </h3>
                        <br />
                        <asp:GridView OnRowCommand="grdUsuario_RowCommand" ID="grdUsuario" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" EmptyDataText="A consulta não retornou dados" OnPageIndexChanging="grdUsuario_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="login" HeaderText="Login" />
                                <asp:BoundField DataField="nome" HeaderText="Nome" />
                                <asp:BoundField DataField="email" HeaderText="E-mail" />
                                <asp:BoundField DataField="departamento" HeaderText="Departamento" />
                                <asp:BoundField DataField="dt_inclusao" HeaderText="Data" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="dt_inclusao" HeaderText="Horario" DataFormatString="{0:HH:mm:ss}" />
                                <asp:BoundField DataField="descricao_status" HeaderText="Status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                            Text="Alterar Status" CommandName="STATUS" CommandArgument='<%# Eval("descricao_status")+","+Eval("nome")+","+ Eval("login") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>
                    <div id="divAction" runat="server">
                        <h2>Alterar Status do Usuário</h2>
                        <br />
                        <table>
                            <tr>
                                <td>Usuário Selecionado:</td>
                                <td>
                                    <b>
                                        <asp:Label ID="lbUsuario" runat="server" Text="Label"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td>Status Atual:</td>
                                <td>
                                    <asp:HiddenField ID="hdUsuario" runat="server" />

                                    <b>
                                        <asp:Label ID="lbStatus" runat="server" Text="Label"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td>Justificativa (Ex:Afastado)</td>
                                <td>
                                    <asp:TextBox ID="txtJustificativa" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSalvar" runat="server"
                                        Text="Salvar" CssClass="button" OnClick="btnSalvar_Click" />
                                    <asp:Button ID="btnVoltar" runat="server"
                                        Text="Voltar" CssClass="button" OnClick="btnVoltar_Click" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="divHistorico" runat="server">
                    <table>
                        <tr>
                            <td>Selecione
                            </td>
                            <td>
                                <asp:DropDownList ID="drpHistUser" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpHistUser_SelectedIndexChanged"> </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnHistPes" runat="server" Text="Consultar" OnClick="btnHistPes_Click" CssClass="button" />
                                <asp:Button ID="btnHistVol" runat="server" Text="Voltar" OnClick="btnHistVol_Click" CssClass="button" />
                                <asp:Button ID="btnHistExcel" Visible="false" runat="server" Text="Exportar para Excel" OnClick="btnHistExcel_Click"  CssClass="button" />
                            </td>
                        </tr>

                    </table>
                    <asp:GridView ID="grdHistorico" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" EmptyDataText="A consulta não retornou dados" OnPageIndexChanging="grdHistorico_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="login" HeaderText="Login" />

                            <asp:BoundField DataField="ds_justitificativa" HeaderText="Justificativa" />
                            <asp:BoundField DataField="dt_inclusao" HeaderText="Data" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="dt_inclusao" HeaderText="Horario" DataFormatString="{0:HH:mm:ss}" />
                            <asp:BoundField DataField="descricao_status" HeaderText="Status" />
                            <asp:BoundField DataField="nome" HeaderText="Responsável" />

                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
         <Triggers>           
      <asp:PostBackTrigger ControlID="btnHistExcel" />        
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
</asp:Content>
