<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="ControleMedctr.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ControleMedctr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function isNumber(evt) {

            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Controle Medctr</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroDoLote" runat="server" Text="Número do Lote: "></asp:Label>
                                <asp:TextBox ID="txtNumeroDoLote" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblDtPagamento" runat="server" Text="Data de pagamento: "></asp:Label>
                                <asp:TextBox ID="txtDtPagamento" runat="server" Width="100px" onkeypress="mascara(this, data)"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblTipMed" runat="server" Text="Tipo de medição: "></asp:Label>
                                <asp:DropDownList ID="ddlTipoMed" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" OnClick="btnPesquisar_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click" />
                                <asp:HiddenField ID="hdTarget" runat="server" />
                            </td>

                        </tr>
                    </table>


                    <asp:GridView
                        ID="grdPrincipal"
                        runat="server"
                        AllowPaging="true"
                        AutoGenerateColumns="false"
                        PageSize="10"
                        Width="1000px"
                        ClientIDMode="Static"
                        CssClass="Table"
                        OnPageIndexChanging="grdPrincipal_PageIndexChanging"
                        OnRowCommand="grdPrincipal_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Lote: 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNumeroLote" runat="server" Text='<%# Bind("num_lote") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    Tipo de medição:
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTpMedic" runat="server" Text='<%# Bind("dcr_parametros") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Data de pagamento: 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDtPagto" runat="server" Text='<%# Bind("dt_pagto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Status: 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <center><asp:LinkButton ID="btnAlterar" runat="server" CommandName="Gravar" CommandArgument="Gravar" Text="Alterar" CssClass="button"></asp:LinkButton></center>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <center><asp:LinkButton ID="btnExcluirLote" runat="server" CommandName="Excluir" CommandArgument="Excluir" Text="Excluir Lote" CssClass="button" OnClientClick="return confirm('Deseja excluir o lote?');" /></center>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <center><asp:LinkButton ID="btnDuplicarLote" runat="server" CommandName="Duplicar" Text="Duplicar Lote" CssClass="button" OnClientClick="return confirm('Deseja duplicar o lote?');" /></center>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>

                    <asp:GridView
                        ID="grdDetalheLote"
                        runat="server"
                        CssClass="panelLabel"
                        AllowPaging="true"
                        AutoGenerateColumns="false"
                        PageSize="20" Width="100px">
                        <Columns>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    IDREC
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdrec" runat="server" Text='<%# Bind("idrec") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Xnumct
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblXnumct" runat="server" Text='<%# Bind("xnumct") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Valor medição
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValmed" runat="server" Text='<%# Bind("valmed") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>


               <asp:Panel ID="ModalPanel" runat="server" Width="450px" BackColor="White" BorderWidth="2px" BorderColor="Black" BorderStyle="Solid">
                   <br />
 &nbsp;&nbsp;<b>Alteração do lote: <asp:Label ID="lblPopNumLote" runat="server" Text=""></asp:Label> </b><br /><br />

               &nbsp;&nbsp;  Nova data de pagamento: <asp:TextBox ID="txtDtPagtoNova" runat="server" MaxLength="10" onkeypress="mascara(this, data)"></asp:TextBox>
                   <br /><br />
                    &nbsp;&nbsp; Status: <asp:TextBox ID="txtPopStatus" runat="server" Width="30px" MaxLength="1"></asp:TextBox>
                   <br /><br />
                 &nbsp;&nbsp;  <asp:Button ID="btnConfirmaAlterData" runat="server" Text="Concluir" OnClick="btnConfirmaAlterData_Click" OnClientClick="return confirm('Deseja alterar o lote?');" />
 <asp:Button ID="OKButton" runat="server" Text="Cancelar" />
                   <asp:Label ID="lblStatusAnt" runat="server" Visible="false"></asp:Label>
                   <asp:Label ID="lblDtAnt" runat="server" Visible="false"></asp:Label>
                   <asp:Label ID="lblLog" runat="server" Visible="false"></asp:Label>
                   <br />
                   <br />
</asp:Panel>


                    <ajax:ModalPopupExtender ID="mpe" runat="server" TargetControlID="hdTarget"
 PopupControlID="ModalPanel" OkControlID="OKButton" />

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
    </div>
</asp:Content>
