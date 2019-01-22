<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="ExtratoUtilizacao.aspx.cs" Inherits="IntegWeb.Intranet.Web.ExtUtilizacao" %>

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
                        <h1>Consulta Extrato de Utilização</h1>
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
                                    <asp:DropDownList ID="ddlRepresentante" runat="server" Width="300px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>E-Mail para envio</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEMail" runat="server" Width="220px"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnEmail" runat="server" CssClass="button" Text="Enviar" OnClick="btnEmail_Click" Style="height: 20px; padding-top: 3px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>Período de emissão</td>
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
                                    <asp:Button ID="btnPesquisarSysdocs" runat="server" CssClass="button" Text="Pesquisar SysDocs" OnClick="btnPesquisarSysdocs_Click" />
                                </td>
                            </tr>
                        </table>

                        <iframe id="ifExtratoUtilSysDocs_0" name="ifExtratoUtilSysDocs_0" height="400px" width="80%" src="" frameborder="0" runat="server" visible="false"></iframe>

                        <iframe id="ifExtratoUtilSysDocs_1" name="ifExtratoUtilSysDocs_1" height="400px" width="80%" src="" frameborder="0" runat="server" visible="false"></iframe>

                        <asp:GridView ID="grdExtratoUtilizacao" runat="server"
                            AllowSorting="true"
                            OnSorting="grdExtratoUtilizacao_Sorting"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            ClientIDMode="Static"
                            OnRowCommand="grdExtratoUtilizacao_RowCommand"
                            OnRowCreated="GridView_RowCreated"
                            HeaderStyle-Height="31px">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text="" class="span_checkbox" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Participante/Representante" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRepresentante" runat="server" Text='<%# (ddlRepresentante.SelectedItem!=null) ? ddlRepresentante.SelectedItem.Text : "" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="periodo_desconto" HeaderText="Período Desconto" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="data_emissao" HeaderText="Data de Emissão" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" SortExpression="DATA_EMISSAO" />
                                <asp:BoundField DataField="total_servicos" HeaderText="Total dos Serviços" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="total_pagar" HeaderText="Total a Pagar" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btDetalhes" CausesValidation="false" CssClass="button" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# txtCodEmpresa.Text + "," + txtCodMatricula.Text + "," + ddlRepresentante.SelectedValue + "," + (String.IsNullOrEmpty(txtDtIni.Text) ? DateTime.Now.AddMonths(-2).ToString("dd/MM/yyyy") : txtDtIni.Text)  + "," + (String.IsNullOrEmpty(txtDtFim.Text) ? DateTime.Now.ToString("dd/MM/yyyy") : txtDtFim.Text) + "," + Eval("periodo_desconto") + "," + Eval("data_emissao") %>' />
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
                <asp:PostBackTrigger ControlID="btnPesquisar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div id="carregando">
                <div class="carregandoTxt">
                    <img src="img/processando.gif" />
                    <br />
                    <br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
