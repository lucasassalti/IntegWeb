<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadParamSimulador.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CadParamSimulador" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <script type="text/javascript">
        function CarregaValores() {
            if ($('#ContentPlaceHolder1_TabContainer_TbTabua_txtDtReferencia').val() != "" && $('#ContentPlaceHolder1_TabContainer_TbTabua_ddlUnidMonetariaInc').val() > 0) {
                $('#ContentPlaceHolder1_TabContainer_TbTabua_btnCalcularValor').click();
                $('#ContentPlaceHolder1_TabContainer_TbTabua_btnCalcularValorMedio').click();
            }
        }
    </script>--%>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Manutenção Parâmetros Simulador</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbTabua" HeaderText="Tabua atuarial X Empresa e unidades de referência" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div id="divPesquisa" runat="server" class="tabelaPagina">
                                    <asp:Panel ID="pnlPesquisa" runat="server">
                                        <table>
                                            <tr>
                                                <td>Empresa:</td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpresa" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="2" />
                                                </td>
                                                <td>Unid.Monetária:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUnidMonetaria" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Plano:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPlano" runat="server"></asp:DropDownList>
                                                </td>
                                                <td>Dt.Referencia</td>
                                                <td>De:<asp:TextBox ID="txtdatIniRef" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)"></asp:TextBox>
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangDatIniRef"
                                                        Type="Date"
                                                        ControlToValidate="txtdatIniRef"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                </td>
                                                <td>Para:<asp:TextBox ID="txtdatFimRef" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)"></asp:TextBox>
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangeDatFimRef"
                                                        Type="Date"
                                                        ControlToValidate="txtdatFimRef"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" OnClick="btnNovo_Click" />
                                                    <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:ObjectDataSource runat="server" ID="odsTabuaAtuarial"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao.CadParamSimuladorBLL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtEmpresa" Name="emp" PropertyName="Text" Type="Int16" />
                                            <asp:ControlParameter ControlID="ddlUnidMonetaria" Name="codUm" PropertyName="SelectedValue" Type="Int16" />
                                            <asp:ControlParameter ControlID="ddlPlano" Name="plano" PropertyName="SelectedValue" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtdatIniRef" Name="datIni" PropertyName="Text" Type="DateTime" />
                                            <asp:ControlParameter ControlID="txtdatFimRef" Name="datFim" PropertyName="Text" Type="DateTime" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdTabuaAtuarial"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsTabuaAtuarial"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        AllowSorting="True"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        PageSize="8"
                                        OnSelectedIndexChanged="btnEditar_Click">
                                        <Columns>
                                            <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" SortExpression="EMPRESA" />
                                            <asp:TemplateField SortExpression="PLANO" HeaderText="Plano">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlano" runat="server" Text='<%# Eval("PLANO").ToString() + " - " + Eval("DESC_PLANO")%>'></asp:Label>
                                                    <asp:Label ID="lblPlano2" runat="server" Text='<%# Eval("PLANO")%>' Visible="false" Enabled="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TB_ATUARIAL" HeaderText="Tb.Atuarial" SortExpression="TB_ATUARIAL" />
                                            <asp:TemplateField SortExpression="SEXO" HeaderText="Sexo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSexo" runat="server" Text='<%# Eval("SEXO").ToString() == "1" ? "M" : "F" %>' />
                                                    <asp:Label ID="lblSexo2" runat="server" Text='<%# Eval("SEXO")%>' Visible="false" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="CODIGO_UM" HeaderText="Unid. Monetária">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnidMonetaria" runat="server" Text='<%# Eval("CODIGO_UM").ToString() + " - " + Eval("DESCRICAO_UM")%>'></asp:Label>
                                                    <asp:Label ID="lblUnidMonetaria2" runat="server" Text='<%# Eval("CODIGO_UM")%>' Visible="false" Enabled="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DT_REFERENCIA" HeaderText="Dt.Referencia" SortExpression="DT_REFERENCIA" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditar" runat="server" CssClass="button" Text="Editar" CommandName="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Bind("PLANO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnExcluir" runat="server" CssClass="button" Text="Excluir" CommandName="Excluir" OnClick="btnExcluir_Click" OnClientClick="return confirm('Tem certeza que deseja excluir?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div id="divInserir" runat="server" class="tabelaPagina" visible="False">
                                    <asp:Panel ID="pnlBotao" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSalvar" runat="server" CssClass="button" Text="Salvar" OnClick="btnSalvar_Click" ValidationGroup="grpInserirUref" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelar_Click" />
                                                </td>
                                                <td>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlInserir" runat="server">
                                        <table>
                                            <tr>
                                                <td>Empresa:</td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpresaInc" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="2" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqEmpresaInc" ControlToValidate="txtEmpresaInc" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>                                                
                                                <td>Unid.Monetária:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUnidMonetariaInc" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDdlUnidMonetariaInc" ControlToValidate="ddlUnidMonetariaInc" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Plano:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPlanoInc" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDdlPlano" ControlToValidate="ddlPlanoInc" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>                                                
                                                <td>Dt.Referencia:</td>
                                                <td>
                                                    <asp:TextBox ID="txtDtReferencia" runat="server" CssClass="date" MaxLength="10" OnTextChanged="txtDtReferencia_TextChanged" AutoPostBack="true"/>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDtReferencia" ControlToValidate="txtDtReferencia" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangDtReferencia"
                                                        Type="Date"
                                                        ControlToValidate="txtDtReferencia"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Tab.Atuarial:</td>
                                                <td>
                                                    <asp:TextBox ID="txtTabAtuarial" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="2" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTabAtuarial" ControlToValidate="txtTabAtuarial" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                                <td>Sexo:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSexoInc" runat="server">
                                                        <asp:ListItem Selected="True" />
                                                        <asp:ListItem Text="M" Value="1" />
                                                        <asp:ListItem Text="F" Value="2" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDdlSexoInc" ControlToValidate="ddlSexoInc" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>Valor:&nbsp&nbsp&nbsp
                                                    <asp:Button ID="btnCalcularValor" runat="server" CssClass="button" Text="Calcular" OnClick="btnCalcularValor_Click" Visible="true" Width="100px" Height="20px" Style="padding: 0px;"/>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValor" runat="server" onkeypress="mascara(this, moeda)" MaxLength="9" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtValor" ControlToValidate="txtValor" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                                <td>
                                                    Valor Médio:&nbsp&nbsp&nbsp
                                                    <asp:Button ID="btnCalcularValorMedio" runat="server" CssClass="button" Text="Calcular" OnClick="btnCalcularValorMedio_Click" Visible="true" Width="100px" Height="20px" Style="padding: 0px;"/>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValorMedio" runat="server" onkeypress="mascara(this, moeda)" MaxLength="9" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtValorMedio" ControlToValidate="txtValorMedio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Teto INSS:</td>
                                                <td>
                                                    <asp:TextBox ID="txtINSS" runat="server" onkeypress="mascara(this, moeda)" MaxLength="9" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtINSS" ControlToValidate="txtINSS" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                                <td>% Mínimo para BD:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPorcentBD" runat="server" onkeypress="mascara(this, moeda)" MaxLength="5" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtPorcentBD" ControlToValidate="txtPorcentBD" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>% Invalidez:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPorcentInvalid" runat="server" onkeypress="mascara(this, moeda)" MaxLength="5" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtPorcentInvalid" ControlToValidate="txtPorcentInvalid" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                                <td>Limite % Max.
                                                    Contribuição Empresa (CD):
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLimContribEmp" runat="server" onkeypress="mascara(this, moeda)" MaxLength="5" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtLimContribEmp" ControlToValidate="txtLimContribEmp" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Unid. Quadro
                                                    Próprio(UQP)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUQP" runat="server" onkeypress="mascara(this, moeda)" MaxLength="9" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtUQP" ControlToValidate="txtUQP" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                                <td>Juros Anuais</td>
                                                <td>
                                                    <asp:TextBox ID="txtJurosAnu" runat="server" MaxLength="7" onkeypress="mascara(this, soDecimais)" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtJurosAnu" ControlToValidate="txtJurosAnu" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Juros Padrão:</td>
                                                <td>
                                                    <asp:TextBox ID="txtJurosPadrao" runat="server" MaxLength="7" onkeypress="mascara(this, soDecimais)" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtJurosPadrao" ControlToValidate="txtJurosPadrao" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                                <td>Juros Máximo:</td>
                                                <td>
                                                    <asp:TextBox ID="txtJurosMax" runat="server" MaxLength="7" onkeypress="mascara(this, soDecimais)" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtJurosMax" ControlToValidate="txtJurosMax" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirUref" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>Usuário:</td>
                                                <td>
                                                    <asp:TextBox ID="txtUsuario" runat="server" ReadOnly="True" Enabled="False" />
                                                </td>
                                                <td>Data Inclusão:</td>
                                                <td>
                                                    <asp:TextBox ID="txtDtInclusao" runat="server" CssClass="date" MaxLength="10" ReadOnly="True" Enabled="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <%--ABA 2--%>
                        <ajax:TabPanel ID="TbTaxas" HeaderText="Taxas de contribuição" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Plano:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPlanoAB2" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisarAB2" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisarAB2_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnNovoAB2" runat="server" CssClass="button" Text="Novo" OnClick="btnNovoAB2_Click" />
                                            <asp:Button ID="btnLimparAB2" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparAB2_Click" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ObjectDataSource runat="server" ID="odsTaxaContribuicao"
                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao.CadParamSimuladorBLL"
                                    SelectMethod="GetDataTxContrib"
                                    SelectCountMethod="GetDataCountTxContrib"
                                    EnablePaging="True"
                                    SortParameterName="sortParameter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlPlanoAB2" Name="plano" PropertyName="SelectedValue" Type="Int16" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdTaxasContribucao"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataSourceID="odsTaxaContribuicao"
                                    EmptyDataText="Não retornou registros"
                                    AllowPaging="True"
                                    AllowSorting="True"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    PageSize="8"
                                    OnRowEditing="grdTaxasContribucao_RowEditing"
                                    DataKeyNames="PLANO"
                                    OnRowCommand="grdTaxasContribucao_RowCommand">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Plano" SortExpression="PLANO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlanoAb2" runat="server" Text='<%# Bind("PLANO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPlanoAb2" runat="server" Width="100%" MaxLength="2" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPlanoAb2" ControlToValidate="txtPlanoAb2" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirCont" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="% Faixa 1" SortExpression="PERC_F1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPercF1" runat="server" Text='<%# Bind("PERC_F1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPercf1" runat="server" Text='<%# Bind("PERC_F1") %>' Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercf1" ControlToValidate="txtPercf1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditCont" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPercF1f" runat="server" Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercF1f" ControlToValidate="txtPercF1f" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirCont" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="% Faixa 2" SortExpression="PERC_F2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPercF2" runat="server" Text='<%# Bind("PERC_F2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPercf2" runat="server" Text='<%# Bind("PERC_F2") %>' Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercf2" ControlToValidate="txtPercf2" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditCont" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPercF2f" runat="server" Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercF2f" ControlToValidate="txtPercF2f" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirCont" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="% Faixa 3" SortExpression="PERC_F3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPercF3" runat="server" Text='<%# Bind("PERC_F3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPercf3" runat="server" Text='<%# Bind("PERC_F3") %>' Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercf3" ControlToValidate="txtPercf3" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditCont" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPercF3f" runat="server" Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercF3f" ControlToValidate="txtPercF3f" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirCont" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="% Patrocinadora" SortExpression="PERC_EM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPercEm" runat="server" Text='<%# Bind("PERC_EM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPercem" runat="server" Text='<%# Bind("PERC_EM") %>' Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercem" ControlToValidate="txtPercem" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditCont" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPercEmf" runat="server" Width="100%" MaxLength="7" onkeypress="mascara(this, soDecimais)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtPercEmf" ControlToValidate="txtPercEmf" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirCont" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditarAB2" runat="server" Text="Editar" CommandName="Edit" CssClass="button" CausesValidation="false" />
                                                <asp:Button ID="btnExcluirAB2" runat="server" Text="Excluir" CommandName="DeleteAb2" CssClass="button" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja excluir?');" CommandArgument='<%# Bind("PLANO") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnSalvarAB2" runat="server" Text="Salvar" CommandName="UpdateAb2" CssClass="button" ValidationGroup="grpEditCont" />
                                                <asp:Button ID="btnCancelarAb2" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" OnClientClick="return confirm('Tem certeza que deseja abandonar a operação?');" CausesValidation="false" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btSalvarNovoAB2" CssClass="button" runat="server" Text="Salvar" CommandName="AddNew" ValidationGroup="grpInserirCont" />
                                                <asp:Button ID="btCancelarAB2" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="CancelAdd" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <%--ABA 3--%>
                        <ajax:TabPanel ID="TbParametros" HeaderText="Parâmetros" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Data Ref.Processamento:</td>
                                        <td>
                                            <asp:TextBox ID="txtDatRefProcessamento" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)"/>
                                            <asp:RequiredFieldValidator runat="server" ID="reqDatRefProcessamento" ControlToValidate="txtDatRefProcessamento" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangDatRefProcessamento"
                                                Type="Date"
                                                ControlToValidate="txtDatRefProcessamento"
                                                MaximumValue="31/12/9999"
                                                MinimumValue="31/12/1000"
                                                ErrorMessage="Data Inválida"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                ValidationGroup="grpParametros" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Data Ref.INSS:</td>
                                        <td>
                                            <asp:TextBox ID="txtDatRefInss" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)"/>
                                            <asp:RequiredFieldValidator runat="server" ID="reqDatRefInss" ControlToValidate="txtDatRefInss" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangDatRefInss"
                                                Type="Date"
                                                ControlToValidate="txtDatRefInss"
                                                MaximumValue="31/12/9999"
                                                MinimumValue="31/12/1000"
                                                ErrorMessage="Data Inválida"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                ValidationGroup="grpParametros" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Percentual INSS:</td>
                                        <td>
                                            <asp:TextBox ID="txtPercentualInss" runat="server" onkeypress="mascara(this, moeda)" MaxLength="5" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqPercentualInss" ControlToValidate="txtPercentualInss" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" onkeypress="mascara(this, soDecimais)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSalvarAB3" runat="server" CssClass="button" Text="Salvar" OnClick="btnSalvarAB3_Click" ValidationGroup="grpParametros" CausesValidation="true" />
                                            <asp:Button ID="btnLimparAB3" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparAB3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>

                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
