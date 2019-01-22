<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Popup.Master" CodeBehind="ExtratoUtilizacaoComponente.aspx.cs" Inherits="IntegWeb.Portal.Web.ExtUtilizacaoComponente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MarginGrid">
        <script type="text/javascript">
            function _client_side_script() {
                $('#chkSelectAll').click(function () {
                    $('.span_checkbox input:checkbox').prop('checked', $('#chkSelectAll').prop('checked'));
                });
            };
        </script>
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>

                <div class="full_w">

                    <div class="tabelaPagina">
                        <h1>Componente Utilização dos Serviços</h1>
                        <table>
                            <tr>
                                <td>Empresa</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" runat="server" Style="width: 50px;" OnTextChanged="txtCodMatricula_TextChanged" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                </td>
	                            <td style="text-align: end;">Matrícula</td>
                                <td>
                                    <asp:TextBox ID="txtCodMatricula" runat="server" Style="width: 70px;" OnTextChanged="txtCodMatricula_TextChanged" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Usuário</td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlRepresentante" runat="server" Width="300px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>E-Mail para envio</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEMail" runat="server" Width="220px"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnEmail" runat="server" CssClass="button" Text="Enviar" OnClick="btnEmail_Click" style="height: 20px; padding-top: 3px;" />
                                </td>
                            </tr>  
                            <tr>
                                <td>Ano</td>
                                <td>
                                    <%--<asp:TextBox ID="txtNumAno" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlNumAno" runat="server"> 
                                        <asp:ListItem Text="2016" Value="2016" Selected="True" />
                                    </asp:DropDownList>
                                </td>

                                <td>Período</td>
                                <td >
                                    <asp:DropDownList ID="ddlSemestre" runat="server" >
                                        <%--<asp:ListItem Text="1º Semestre" Value="1" Selected="True" />
                                        <asp:ListItem Text="2º Semestre" Value="2" />--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                
                            </tr>--%>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                </td>
                            </tr>
                        </table>

                        <asp:GridView ID="grdExtratoUtilizacao" runat="server"
                            AllowPaging="true"
                            AllowSorting="false"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            ClientIDMode="Static"
                            PageSize="8"
                            OnRowCommand="grdExtratoUtilizacao_RowCommand"
                            HeaderStyle-Height="31px">
                            <PagerSettings
                                Visible="true"
                                PreviousPageText="Anterior"
                                NextPageText="Próxima"
                                Mode="NumericFirstLast" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text="" class="span_checkbox"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="Periodo" HeaderText="Semestre" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="valorTotal" HeaderText="Total dos Serviços" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btDetalhes" CausesValidation="false" CssClass="button" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# txtCodEmpresa.Text + "," + txtCodMatricula.Text + "," + ddlRepresentante.SelectedValue + "," +  Eval("NUM_SUB_MATRIC") + "," +  ddlSemestre.SelectedValue + "," +  ddlNumAno.SelectedValue %>' />
                                        <%--<asp:Button ID="btEnviar" CausesValidation="false" CssClass="button" runat="server" Text="Enviar por e-mail" CommandName="Email" CommandArgument='<%# txtCodEmpresa.Text + "," + txtCodMatricula.Text + "," + ddlRepresentante.SelectedValue + "," + txtDtIni.Text + "," + txtDtFim.Text + "," + Eval("periodo_desconto") + "," + Eval("data_emissao") %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <uc1:ReportCrystal runat="server" ID="ReportCrystal" Visible="false" />

                    </div>
                </div>
            </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnPesquisar"/>
                </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div id="carregando">
                <div class="carregandoTxt">
                    <img src="img/processando.gif" />
                    <br /><br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
