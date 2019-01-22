<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RelCustoJudicial.aspx.cs" Inherits="IntegWeb.Previdencia.Web.RelCustoJudicial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tabelaPagina input[type="text"] {
            width: 100%;
        }

        .tabelaPagina select {
            width: 100%;
        }
    </style>
    <script type="text/javascript">

        //$(document).ready(function () {
        //function _client_side_script() {
        //    $('#ContentPlaceHolder1_chkIncidir').click(function () {
        //        if ($(this).prop('checked')) {
        //            $('#ContentPlaceHolder1_ddlUnMonetaria').removeAttr('disabled');
        //            $('#ContentPlaceHolder1_txtDtAtuMonetaria').removeAttr('disabled');
        //        } else {
        //            $('#ContentPlaceHolder1_ddlUnMonetaria').attr('disabled', 'disabled');
        //            $('#ContentPlaceHolder1_txtDtAtuMonetaria').attr('disabled', 'disabled');
        //        }
        //    });
        //}

    </script>
    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Emissão de Relatório de Custo Judicial</h1>
        <asp:UpdatePanel runat="server" ID="upCustoJudicial">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlGrid" class="tabelaPagina">

                    <table class="tabelaPagina">
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Gerar Novo" OnClick="btnNovo_Click" />
                                <asp:Button ID="btnRelatorio"  runat="server" CssClass="button" Text="Imprimir" Style="display: none;" />
                                <asp:Label  ID="pnlGrid_Mensagem" runat="server" visible="False"></asp:Label> 
                            </td>
                        </tr>
                        <tr>
                            <td>

                                <asp:ObjectDataSource runat="server" ID="odsCustoJudicial"
                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial.ValorReferenciaBLL"
                                    SelectMethod="ListarCustoJudicial"
                                    SortParameterName="sortParameter">
                                    <%-- <SelectParameters>
                                        <asp:ControlParameter ControlID="drpPesquisa" Name="filType" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="txtPesquisa" Name="filValue" PropertyName="Text"
                                            Type="String" ConvertEmptyStringToNull="true" />
                                        <asp:ControlParameter ControlID="txtCodEmpresa" Name="codEmpresa" PropertyName="Text"
                                            Type="Int32" ConvertEmptyStringToNull="true" />
                                        <asp:ControlParameter ControlID="txtCodMatricula" Name="codMatricula" PropertyName="Text"
                                            Type="Int32" ConvertEmptyStringToNull="true" />
                                    </SelectParameters>--%>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdCustoJudicial" runat="server"
                                    OnRowCreated="GridView_RowCreated"
                                    OnRowCommand="grdCustoJudicial_RowCommand"
                                    OnRowDeleting="grdCustoJudicial_RowDeleting"
                                    AllowPaging="true"
                                    AllowSorting="true"
                                    AutoGenerateColumns="False"
                                    EmptyDataText="A consulta não retornou registros"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    PageSize="8"
                                    DataSourceID="odsCustoJudicial">
                                    <%--OnRowCancelingEdit="grdCCusto_RowCancelingEdit" 
                        OnRowEditing="grdCCusto_RowEditing" 
                        OnRowUpdating="grdCCusto_RowUpdating"--%>
                                    <Columns>
                                        <asp:BoundField HeaderText="Dt. Geração" DataField="HDRDATHOR" SortExpression="HDRDATHOR" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Período De" DataField="DTA_SEL_DE" SortExpression="DTA_SEL_DE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}"/>
                                        <asp:BoundField HeaderText="Período Até" DataField="DTA_SEL_ATE" SortExpression="DTA_SEL_ATE" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}"/>
                                        <asp:BoundField HeaderText="Vl. Limitador" DataField="VLR_LIM_TETO" SortExpression="VLR_LIM_TETO" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:n}"/>
                                        <asp:BoundField HeaderText="Unid. Monetária" DataField="NOM_ABRVO_UM" SortExpression="NOM_ABRVO_UM" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Corrigido até" DataField="DTA_ULT_COR" SortExpression="DTA_ULT_COR" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}"/> 
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkVisualizar" runat="server" CssClass="button"
                                                    Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# Eval("HDRDATHOR") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                    Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("HDRDATHOR") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" />
                </asp:Panel>

                <asp:Panel class="tabelaPagina" ID="pnlGerarNovo" runat="server" Visible="false">
                    <table>
	                    <tr class="periodo">
                            <td>Período</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDtIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server" ControlToValidate="txtDtIni" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                            até&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtDtFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" runat="server" ControlToValidate="txtDtFim" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                            </td>
	                    </tr>
	                    <tr>
                            <td>Unidade Monetária: </td>
                            <td>
                                <asp:DropDownList ID="ddlUnMonetaria" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
	                    <tr>
                            <td>Valor Limitador: </td>
                            <td>
                                <asp:TextBox ID="txtVlrLimitador" runat="server" Style="width: 100px;" onkeypress="mascara(this, moeda)" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
	                    <tr>
                            <td>&nbsp</td>
                            <td>
                                <asp:CheckBox ID="chkIncidir" runat="server" Checked="true" Text=" Incidir Correção Monetária" />
                            </td>
                        </tr>
	                    <tr>
                            <td>Dt. Atualização Monetária: </td>
                            <td>
                                <asp:TextBox ID="txtDtAtuMonetaria" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
 
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnGerar" runat="server" CssClass="button" Text="Gerar" OnClick="btnGerar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelar_Click" />                                
                                <asp:Label id="pnlMensagem" runat="server" visible="False"></asp:Label> 
                                <asp:Label  ID="pnlGerarNovo_Mensagem" runat="server" visible="False"></asp:Label> 
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            </ContentTemplate>
            <%--            <Triggers>
                <asp:PostBackTrigger ControlID="btnImprimir" />
                <asp:PostBackTrigger ControlID="btnImportarVr" />
                <asp:PostBackTrigger ControlID="btnVoltarImportar" />
            </Triggers>--%>
        </asp:UpdatePanel>

    </div>

</asp:Content>
