<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="DebitoConta.aspx.cs" Inherits="IntegWeb.Financeira.Web.DebitoConta" %>

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
            <h1>Débito automático</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="TbConsulta" HeaderText="Consulta Participantes c/ Débito Automática" runat="server" TabIndex="0">
                            <ContentTemplate>

                                <table>
                                    <tr>
                                        <td>Empresa</td>
                                        <td>
                                            <asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                                        </td>
                                        <td>Matrícula</td>
                                        <td>
                                            <asp:TextBox ID="txtPesqMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                                        </td>
                                        <td>Representante</td>
                                        <td>
                                            <asp:TextBox ID="txtPesqRepresentante" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CPF</td>
                                        <td>
                                            <asp:TextBox ID="txtPesqCpf" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                                        </td>
                                        <td>Nome</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtPesNome" Width="100%" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Consultar" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Limpar" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnExportar" OnClick="btnExportar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Exportar" />
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="TbConsulta_Mensagem" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <asp:ObjectDataSource runat="server" ID="odsDebitoConta"
                                    TypeName="IntegWeb.Financeira.Aplicacao.BLL.DebitoContaBLL"
                                    SelectMethod="GetData"
                                    SelectCountMethod="GetDataCount"
                                    EnablePaging="true"
                                    SortParameterName="sortParameter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtPesqEmpresa" Name="pEmpresa" PropertyName="Text" Type="Int16" />
                                        <asp:ControlParameter ControlID="txtPesqMatricula" Name="pMatricula" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtPesqRepresentante" Name="pRepresentante" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtPesqCpf" Name="pCpf" PropertyName="Text" Type="Int64" />
                                        <asp:ControlParameter ControlID="txtPesNome" Name="pNome" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdDebitoConta" runat="server"
                                    DataKeyNames="NUM_CPF, DTH_INCLUSAO, COD_PRODUTO"
                                    OnRowCancelingEdit="GridView_RowCancelingEdit"
                                    OnRowCreated="GridView_RowCreated"
                                    AllowPaging="true"
                                    AllowSorting="true"
                                    AutoGenerateColumns="False"
                                    EmptyDataText="A consulta não retornou registros"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    PageSize="50"
                                    DataSourceID="odsDebitoConta"
                                    style="font-size: smaller;"
                                    RowStyle-Height="8px">
                                    <PagerSettings
                                        Visible="true"
                                        PreviousPageText="Anterior"
                                        NextPageText="Próxima"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:BoundField DataField="NUM_CPF" HeaderText="CPF" SortExpression="NUM_CPF" />
                                        <asp:BoundField DataField="NOME" HeaderText="Nome" SortExpression="NOME" />
                                        <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" SortExpression="EMPRESA" />
                                        <asp:BoundField DataField="REGISTRO" HeaderText="Registro" SortExpression="REGISTRO" />
                                        <asp:BoundField DataField="REPRESENTANTE" HeaderText="Repres." SortExpression="REPRESENTANTE" />
                                        <asp:TemplateField HeaderText="Produto" SortExpression="COD_PRODUTO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPRODUTO" runat="server" Text='<%# Eval("COD_PRODUTO").ToString() + " - " + Eval("DESC_PRODUTO").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="COD_AGENCIA" HeaderText="Agência" SortExpression="COD_AGENCIA" />
                                        <asp:BoundField DataField="NUM_CONTA" HeaderText="Conta" SortExpression="NUM_CONTA" />
                                        <asp:BoundField DataField="TIP_CONTA" HeaderText="Tipo Conta" SortExpression="TIP_CONTA" />

                                        <asp:TemplateField HeaderText="Situação" SortExpression="IND_ATIVO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSITUACAO" runat="server" Text='<%# (Eval("IND_ATIVO").ToString().Equals("0")) ? "INATIVO" : "ATIVO" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DTH_INCLUSAO" HeaderText="Dt. Inclusão" SortExpression="DTH_INCLUSAO" DataFormatString="{0:d}" />
                                    </Columns>
                                </asp:GridView>

                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TbUpload" HeaderText="Importação de arquivo de retorno" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <h3>Entre com o arquivo recebido</h3>
                                            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                            <asp:Button ID="btnUpload" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" Text="Importar arquivo" OnClick="btnUpload_Click" />
                                            <asp:Button ID="btnConsultar2" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" Text="Consultar anteriores"  />
                                            <asp:Button ID="btnExportarRet" OnClick="btnExportarRet_Click" CssClass="button" CausesValidation="false" runat="server" Text="Exportar" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="TbUpload_Mensagem" runat="server" Visible="False"></asp:Label>
                                            <asp:LinkButton ID="lkYes" runat="server" OnClick="lkYes_Click" CommandArgument="" Text="SIM" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlGridRetDebitoConta" runat="server" Visible="False">
                                    <table>
                                        <tr>
                                            <td>Empresa</td>
                                            <td>
                                            <asp:TextBox ID="txtEmp" runat="server" CssClass="text" Width="100px" MaxLength="3" ></asp:TextBox>
                                            </td>

                                            <td>Matrícula</td>
                                            <td>
                                            <asp:TextBox ID="txtMatr" runat="server" CssClass="text" Width="100px" MaxLength="13" ></asp:TextBox>
                                            </td>

                                            <td>Representante</td>
                                            <td>
                                            <asp:TextBox ID="txtRepre" runat="server" CssClass="text" Width="100px" MaxLength="10" ></asp:TextBox>
                                            </td>
                                            <td><asp:Button ID="btnConsultar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar2_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td>Últimas importações</td>
                                            <td>
                                                <asp:DropDownList ID="ddlNomeArquivo" runat="server" AutoPostBack="True" Width="200px" OnTextChanged="ddlNomeArquivo_TextChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Tipo de Registro</td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoRegistro" runat="server" AutoPostBack="True" Width="200px" OnTextChanged="ddlNomeArquivo_TextChanged">
                                                    <asp:ListItem Text="<Todos>" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="B - CADASTRO DÉBIDO AUTOMÁTICO" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="F - RETORNO DE TRANSAÇÃO DÉBIDO AUTOMÁTICO" Value="F"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>Apenas com Crítica</td>
                                            <td>
                                                <asp:CheckBox ID="chkPesqComCritica" runat="server" AutoPostBack="True" OnCheckedChanged="ddlNomeArquivo_TextChanged"/>
                                            </td>
                                        </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td>Data do arquivo</td>
                                            <td>
                                                <asp:Label ID="lblData" runat="server" Text="-" Style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td>Total de linhas</td>
                                            <td>
                                                <asp:Label ID="lblTotalLinhas" runat="server" Text="-" Style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td>Qtd. linhas com erro</td>
                                            <td>
                                                <asp:Label ID="lblQtdErro" runat="server" Text="-" Style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:ObjectDataSource runat="server" ID="odsDebitoContaRetorno"
                                        TypeName="IntegWeb.Financeira.Aplicacao.DAL.DebitoContaRetornoDAL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtEmp" Name="pEmp" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtMatr" Name="pMatr" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="txtRepre" Name="pRepre" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="ddlNomeArquivo" Name="pDCR_NOM_ARQ" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="ddlTipoRegistro" Name="pID_TP_REGISTRO" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="chkPesqComCritica" Name="pComCritica" PropertyName="Checked" Type="Boolean" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdDebitoContaRetorno" runat="server"
                                        DataKeyNames="DCR_NOM_ARQ,NUM_SEQ_LINHA"
                                        OnRowCancelingEdit="GridView_RowCancelingEdit"
                                        OnRowCreated="GridView_RowCreated"
                                        OnRowDataBound="grdDebitoContaRetorno_RowDataBound"
                                        AllowPaging="True"
                                        AllowSorting="True"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="A consulta não retornou registros"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        PageSize="8"
                                        DataSourceID="odsDebitoContaRetorno">
                                        <PagerSettings
                                            PreviousPageText="Anterior"
                                            NextPageText="Próxima"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:BoundField DataField="NUM_SEQ_LINHA" HeaderText="Linha" SortExpression="NUM_SEQ_LINHA" />
                                            <asp:BoundField DataField="ID_TP_REGISTRO" HeaderText="Tipo" SortExpression="ID_TP_REGISTRO" />
                                            <asp:BoundField DataField="COD_EMPRESA" HeaderText="Empresa" SortExpression="COD_EMPRESA" />
                                            <asp:BoundField DataField="NUM_REGISTRO" HeaderText="Registro" SortExpression="NUM_REGISTRO" />
                                            <asp:BoundField DataField="NUM_REPRESENTANTE" HeaderText="Repres." SortExpression="NUM_REPRESENTANTE" />
                                            <asp:BoundField DataField="NUM_NOSSO_NUMERO" HeaderText="Nosso Número" SortExpression="NUM_NOSSO_NUMERO" />

                                            <asp:TemplateField HeaderText="Dt. Vencimento" SortExpression="DTA_VENCIMENTO" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDTA_VENCIMENTO" runat="server" Text='<%# (!String.IsNullOrEmpty(Eval("DTA_VENCIMENTO").ToString().Trim()) ? Eval("DTA_VENCIMENTO").ToString().Substring(6,2) + "/" + Eval("DTA_VENCIMENTO").ToString().Substring(4,2) + "/" + Eval("DTA_VENCIMENTO").ToString().Substring(0,4) : Eval("DTA_VENCIMENTO"))  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Valor" SortExpression="VLR_DEBITO" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVALOR" runat="server" Text='<%# (Eval("ID_TP_REGISTRO").Equals("F") ? decimal.Parse(Eval("VLR_DEBITO").ToString().Insert(13,",")).ToString() : Eval("VLR_DEBITO"))  %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--                                    <asp:TemplateField HeaderText="Retorno" SortExpression="COD_MOTIVO_RET">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRETORNO" runat="server" Text='<%# Eval("COD_MOTIVO_RET") + " - " + (Eval("AAT_TBL_RET_DEB_CONTA_MOTIVO.DESC_MOTIVO") ?? "").ToString() %>' ForeColor='<%# ((Eval("COD_MOTIVO_RET") ?? "00").Equals("00")) ? System.Drawing.Color.Black : System.Drawing.Color.Red %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Críticas">
                                                <ItemTemplate>
                                                    <div class="accordion" style='display: <%# (int.Parse(Eval("AAT_TBL_RET_DEB_CONTA_CRITICAS.Count").ToString()) > 1 ? "block" : "none") %>'>
                                                        <h3 style="color: red">Criticas encontradas</h3>
                                                        <div>
                                                            <asp:Label ID="lstCriticas" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div style='display: <%# (int.Parse(Eval("AAT_TBL_RET_DEB_CONTA_CRITICAS.Count").ToString()) == 1 ? "block" : "none") %>'>
                                                        <asp:Label ID="lblCritica" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$TbUpload$btnUpload" />
                    <asp:PostBackTrigger ControlID="TabContainer$TbUpload$btnConsultar" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
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
