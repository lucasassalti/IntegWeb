<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="CreditoReembolso.aspx.cs" Inherits="IntegWeb.Intranet.Web.CredReembolso" %>

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
                        <h1>Consulta Crédito de Reembolso</h1>
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
                                <td>Nome</td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlRepresentante" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlRepresentante_SelectedIndexChanged" ></asp:DropDownList>
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
                                <td>Usuário</td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlUsuario" runat="server" Width="300px"></asp:DropDownList>
                                </td>
                            </tr>                            
                            <tr>
                                <td>Período de crédito</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDtIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDtIni" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                até&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtDtFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server" ControlToValidate="txtDtFim" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPesquisarSysdocs" runat="server" CssClass="button" Text="Pesquisar SysDocs" OnClick="btnPesquisarSysdocs_Click" Visible="false"/>
                                </td>
                            </tr>
                        </table>

                        <iframe ID="ifCreditoReembolsoSysDocs" name="ifCreditoReembolsoSysDocs" height="600px" width="80%" src="" frameborder="0" runat="server" visible="false">
                        </iframe>

                        <asp:GridView ID="grdCreditoReembolso" runat="server"
                            AllowPaging="true"
                            AllowSorting="false"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            ClientIDMode="Static"
                            PageSize="8"
                            OnRowCommand="grdCreditoReembolso_RowCommand"
                            OnPageIndexChanging="grdCreditoReembolso_PageIndexChanging"
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
                                <asp:BoundField DataField="usuario" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="usuario" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="emissao" HeaderText="Emissão" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="previsao_credito" HeaderText="Previsão de Crédito" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="reembolsado" HeaderText="Reembolsado" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btDetalhes" CausesValidation="false" CssClass="button" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# txtCodEmpresa.Text + "," + txtCodMatricula.Text  + "," + ddlRepresentante.SelectedValue + "," + Eval("num_sub_matric") + "," + (String.IsNullOrEmpty(txtDtIni.Text) ? DateTime.Now.AddMonths(-2).ToString("dd/MM/yyyy") : txtDtIni.Text)  + "," + (String.IsNullOrEmpty(txtDtFim.Text) ? DateTime.Now.ToString("dd/MM/yyyy") : txtDtFim.Text)  + "," + Eval("previsao_credito", "{0:dd/MM/yyyy}") %>' />
                                        <%--<asp:Button ID="btEnviar" CausesValidation="false" CssClass="button" runat="server" Text="Enviar por e-mail" CommandName="Email" CommandArgument='<%# txtCodEmpresa.Text + "," + txtCodMatricula.Text + "," + Eval("num_sub_matric") + "," + txtDtIni.Text + "," + txtDtFim.Text  + "," + Eval("previsao_credito", "{0:dd/MM/yyyy}") %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <uc1:ReportCrystal runat="server" ID="ReportCrystal" Visible="false" />

                    </div>
                </div>
            </ContentTemplate>
            <%--                <Triggers>
                    <asp:PostBackTrigger ControlID="btnPesquisar"/>
                </Triggers>--%>
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
