<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BeneficiariosComIncentivo.aspx.cs" Inherits="IntegWeb.Intranet.Web.BeneficiariosComIncentivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Beneficiários com Incentivo</h1>

                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="PASTA_AMARELA_ESCURA" width="14%"><div align="center">Dados do Incentivado</div> </td>
                            <td width="86%"><img src="img/ico_direito_AMARELO_UP.jpg" width="30" height="20" /></td>
                        </tr>
                        <tr>
                            <td class="PASTA_AMARELA_ESCURA"><img src="images/separadores/ico_px_TRANSPARENTE.gif" width="150" height="1"></td>
	                         <td class="PASTA_AMARELA_ESCURA" width="86%"><img src="images/separadores/ico_px_TRANSPARENTE.gif" width="150" height="1"></td>
                        </tr>
                    </table>
                    <table width="96%" border="0" cellspacing="0" cellpadding="0" bordercolor="#FFCC33">
                        <tr> 
          
                             <td width="17%" class="FONT_BOLD"><font size="2">Empresa :</font></td>
                             <td width="26%" class="FONT_BOLD"><font size="2">Matricula :</font></td>
                             <td width="57%" class="FONT_BOLD"><font size="2">Nome :</font></td>
                        </tr>
                    </table>
                    <div>
                        <table width="100%" border="0">
                            <tr> 
	                             <td class="PASTA_AMARELA_ESCURA" width="14%"><div align="center">Dependentes</div></td>
	                             <td width="86%"><img src="img/ico_direito_AMARELO_UP.jpg" width="30" height="20"></td> 
                            </tr>
                            <tr> 
	                            <td class="PASTA_AMARELA_ESCURA"><img src="images/separadores/ico_px_TRANSPARENTE.gif" width="150" height="1"></td>
	                            <td class="PASTA_AMARELA_ESCURA" width="86%"><img src="images/separadores/ico_px_TRANSPARENTE.gif" width="150" height="1"></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
