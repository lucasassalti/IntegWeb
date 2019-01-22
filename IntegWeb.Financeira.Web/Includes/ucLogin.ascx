<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLogin.ascx.cs" Inherits="IntegWeb.Financeira.Web.Includes.ucLogin" %>
<div runat="server" id="divLogado">

    <div class="left">
        <asp:ImageButton ID="ImageButton1" OnClick="ImageButton1_Click" runat="server" CausesValidation="false" ImageUrl="~/img/bg_logo.png" style="float: left; height: 68px; width: 83px;" />
<%--        <img src="img/bg_logo.png" style="float: left; height: 68px; width: 83px;" />--%>
        <h1 class="tituloBranco">FinanceiraWeb
        </h1>
        <h3>
            <asp:Label ID="lblambiente" runat="server" CssClass="tituloVer" Text="Label" /></h3>
    </div>
    <div class="right">

        <div class="align-right">
            <p>
                BEM-VINDO, <strong>
                    <asp:Label ID="lbNome" runat="server" ForeColor="#FFFAFA"></asp:Label>
                </strong>[<asp:LinkButton ID="lnkSair" runat="server" OnClick="lnkSair_Click" CausesValidation="false" ForeColor="Red">Sair</asp:LinkButton>]
            </p>
            <asp:Label ID="lbDepartamento" runat="server"></asp:Label>

        </div>
    </div>

</div>

<%-- --%>



<div runat="server" id="divNLogado">


    <label for="login">Login:</label>
    <asp:TextBox ID="txtLogin" runat="server" CssClass="text uppercase"></asp:TextBox>
    <label for="pass">Senha:</label>
    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="text"></asp:TextBox>
    <div class="sep"></div>

    <asp:Button ID="btnAcessar" runat="server" Text="Efetuar Login" CssClass="button" OnClick="btnAcessar_Click" />

    <div class="footer">
        <a href="http://www.prevcesp.com.br/wps/portal">Portal</a> |  <a href="http://portalservicos/SMPortal/SitePages/Service%20Catalog.aspx">Esqueci minha senha</a>
    </div>

</div>


<a href="#divAction" class="fancybox" id="lnkErro" style="display: none"></a>

<div style="margin: 6px; text-align: center; display: none" id="divAction">
    <table>
        <tr>
            <td>
                <asp:Label ID="lbAviso" runat="server" Text="Atenção!" Font-Bold="true" Font-Size="Large" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbSenha" runat="server" Font-Size="Medium" Text="Senha Inválida" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsgAviso" runat="server" Font-Size="Small" /><br />
            </td>
        </tr>
    </table>
</div>

