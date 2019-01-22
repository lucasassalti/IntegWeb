<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CargaProtheus.aspx.cs" Inherits="IntegWeb.Saude.Web.CargaProtheus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Carga de Dados Protheus</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <div id="DivPrincipal" runat="server"  class="tabelaPagina">
                        <table>
                            <tr>
                                <td>Tipo de Carga:</td>
                                <td>
                                    <asp:DropDownList ID="ddlProtheus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProtheus_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                    </div>
                    <div id="DivData" runat="server" class="tabelaPagina">
                    <tr>
                        <td>
                            <asp:Label ID="lblDatPagaVenc" Text="Data de Pagamento/Vencimento:" runat="server"  />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDatPagaVenc" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)"  />
                        </td>
                    </tr>
                    </div>                    

                    <div id="Div20" runat="server" class="tabelaPagina">

                        <tr>
                            <td>
                                <asp:Label ID="lblDataComple" Text="Data Complementados:" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataComple" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDataSuple" Text="Data Suplementados:" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataSuple" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAssociado" Text="Codigo Associado:" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtAssociado" runat="server" CssClass="text" MaxLength="10" />
                            </td>
                        </tr>
                    </div>

                    <div id="Div27" runat="server" class="tabelaPagina">
                        <tr>
                            <td>
                                <asp:Label ID="lblLote" Text="Numero do Lote:" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtLote" runat="server" CssClass="date" MaxLength="10" />
                            </td>
                        </tr>
                    </div>

                    <div id="Div21" runat="server"  class="tabelaPagina">
                        <tr>
                            <td>
                                <asp:Label ID="lblDataInicial" Text="Data Documento Incial:" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataInicial" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDataFinal" Text="Data Documento Final:" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataFinal" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTxt" Text="Upload Txt:" runat="server" />
                            </td>

                            <td>
                                <asp:FileUpload ID="fuTxt" runat="server" CssClass="button" />
                            </td>

                        </tr>
                    </div>
                    <tr>
                        <td>
                            <asp:Button ID="btnOk" runat="server" Text="OK" OnClientClick="return postbackButtonClick();" CssClass="button" OnClick="btnOk_Click" />
                        </td>
                    </tr>
                    </table>

                    
                     <table>
                         <tr>
                             <td>
                                 <asp:Label ID="lblMsgCargaHorario" runat="server" Text="*Não é possível validar os processos no momento, carga do Protheus está em andamento" ForeColor="Red" Visible="false"></asp:Label>
                             </td>
                         </tr>
                                <tr>
                                    <td>
                                         <asp:GridView ID="grdFilaProcesso" 
                                            runat="server"
                                            CssClass="panelTable"
                                         OnRowCommand="grdFilaProcesso_RowCommand"
                                            OnPageIndexChanged="grdFilaProcesso_PageIndexChanged"
                                            OnRowDataBound="grdFilaProcesso_RowDataBound"
                                            AutoGenerateColumns="false"
                                             PageSize="5"
                                             >
                                             <Columns>
                                                 <asp:TemplateField HeaderText="Tipo do relatório">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("dcr_parametros") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Data de pagamento">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblDataPagamento" runat="server" Text='<%# Bind("DATA_PAGAMENTO") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Matrícula de inclusão">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblLogInclusao" runat="server" Text='<%# Bind("log_inclusao") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Data de inclusão">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblDataInclusao" runat="server" Text='<%# Bind("dth_inclusao") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblStatusProcesso" runat="server" Text='<%# Bind("Status_da_carga") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField>
                                                     <ItemTemplate>
                                                         <asp:Button ID="btnGeraRel" runat="server" CommandName="Gerar" Text="Gerar Relatório" CssClass="button"/>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnValidar" runat="server" CommandName="Validar" Text="Validar" CssClass="button" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField>
                                                     <ItemTemplate>
                                                         <asp:Button ID="btnExcluirLote" runat="server" CommandName="Excluir" Text="Excluir" CssClass="button" OnClientClick="return confirm('Deseja excluir esse lote?');" />
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                             </Columns>

                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
  
                </ContentTemplate>
                   <Triggers>
                   <asp:PostBackTrigger ControlID="btnOk" />
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
    </div>
</asp:Content>