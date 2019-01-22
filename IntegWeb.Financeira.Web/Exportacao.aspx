<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Exportacao.aspx.cs" Inherits="IntegWeb.Financeira.Web.Exportacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">

        <div class="h_title">
        </div>
        <div class="MarginGrid">
            <table style="border: 1px solid #FF0000;">
                <tr>
                    <td style="text-align: center;">

                        <h4>
                            <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red">ATENÇÃO</asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h4><asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red">Se ao término do processo não iniciar automaticamente o download do Excel verifique:</asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h4><asp:Label ID="Label3" runat="server" Text="Label" ForeColor="Red">1- A consulta retornou registros.</asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h4><asp:Label ID="Label4" runat="server" Text="Label" ForeColor="Red">2- O navegador esta bloqueando pop-up para este site.</asp:Label></h4>
                    </td>
                </tr>
            </table>
        </div>
        <h1>
            <asp:Label runat="server" ID="NomeRelatorio"></asp:Label>
        </h1>

        <div class="tabelaPagina">
            <table runat="server" id="table">
                <tr>
                    <td colspan="2"></td>

                </tr>
            </table>
            <div class="MarginGrid">
                <asp:UpdatePanel ID="upExcel" runat="server" >
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>                                    
                                    <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" OnClick="btnRelatorio_Click" Visible="true"/>
                                    <asp:Button ID="btnTxtSerasa" runat="server" CssClass="button" Text="Gerar arquivo Serasa" OnClick="btnTxtSerasa_Click" Visible="false" />
                                    <asp:Button ID="btnConsultarRemessas" CssClass="button" runat="server" Text="Consultar últimas Remessas exportadas" OnClick="btnConsultarRemessas_Click" Visible="false"/>
                            </tr>
                            <tr>
                                <td>
                                    <h3>
                                        <asp:Label Visible="false" ID="lblRegistros" runat="server" Text="Label" ForeColor="Red"></asp:Label></h3>
                                </td>
                            </tr>
                        </table>
                        </div>

                        <div id="divselect" runat="server" visible="false">
                            <h3>Últimas remessas exportadas</h3>
                            <asp:GridView ID="grdSex" 
                                DataKeyNames="COD_REMESSA_SERASA_PEFIN"
                                OnPageIndexChanging="grdSex_PageIndexChanging" 
                                runat="server" 
                                AllowPaging="True" 
                                PageSize="20" 
                                EmptyDataText="A consulta não retornou dados" 
                                AutoGenerateColumns="false" 
                                OnRowCancelingEdit="grdSex_RowCancelingEdit"
                                OnRowCommand="grdSex_RowCommand"
                                OnRowEditing="grdSex_RowEditing"
                                OnRowUpdating="grdSex_RowUpdating"
                                OnRowDeleting="grdSex_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="COD_REMESSA_SERASA_PEFIN" HeaderText="REMESSA" />
                                    <asp:BoundField DataField="DAT_EXPORTACAO" HeaderText="DATA EXPORTAÇÃO" ReadOnly="true" />
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                            <asp:Button ID="btCancelar" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btEditar" CausesValidation="false" CssClass="button" runat="server" Text="Editar" CommandName="Edit" />&nbsp;&nbsp;
                                            <asp:Button ID="btExcluir" CausesValidation="false" CssClass="button" runat="server" Text="Excluir" CommandName="Delete" OnClientClick="return confirm('Atenção!! \n\nDeseja realmente excluir este registro?');"  />&nbsp;&nbsp;
                                            <asp:Button ID="btExportar" CausesValidation="false" CssClass="button" runat="server" Text="Exportar" CommandName="Exportar" CommandArgument='<%# Eval("COD_REMESSA_SERASA_PEFIN")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
<%--                    <Triggers>
                        <asp:PostBackTrigger ControlID="ContentPlaceHolder1_btEditar" />
                        <asp:PostBackTrigger ControlID="ContentPlaceHolder1_btCancelar" />
                    </Triggers>--%>
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
    </div>

</asp:Content>
