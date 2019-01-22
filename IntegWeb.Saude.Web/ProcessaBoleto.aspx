<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ProcessaBoleto.aspx.cs" Inherits="IntegWeb.Saude.Web.ProcessaBoleto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="upHistBoleto" runat="server">
          <ContentTemplate>
             
            <div class="full_w  tabelaPagina">               

                 <div class="h_title">
                </div>
                 <h2>Processar Boletos de Cobrança</h2>  
                <br /><br />
                <div id="divProcessaBoeto" runat="server">                    
                    
                        <tr>
                            <td> 
                                <label class="txtCampoForm">
                                    Data de Vencimento:
                                     <asp:TextBox ID="dtProc" runat="server" Width="65"></asp:TextBox>
                                </label> 
                               
                            </td>
                            <td>
                                <asp:Button ID="btnEnvia" runat="server" CssClass="button" Text="Processar  >>" OnClick="btnEnvia_Click" OnClientClick = " return confirm('ATENÇÃO\n\nAo clicar em Ok, será iniciado o processo (Geração Dos Boletos) Acompanhe o Término do mesmo, na tela de Historico de Processamentos.');"/>                                        
                            </td>
                                    
                        </tr>
                            
                        </tr>
                    
                </div>
            </div>
        </ContentTemplate>
     </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="">
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
