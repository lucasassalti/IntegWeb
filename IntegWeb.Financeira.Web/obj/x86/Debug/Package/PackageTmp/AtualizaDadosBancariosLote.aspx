<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AtualizaDadosBancariosLote.aspx.cs" Inherits="IntegWeb.Financeira.Web.AtualizaDadosBancariosLote" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
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
            <h1>Atualização Dados Bancários em Lote </h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbConsultaDados" HeaderText="Consulta Dados Atualizados" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Empresa:
                                                    <asp:TextBox ID="txtEmpresa" runat="server" onkeypress="mascara(this, soNumeros)" Width="141px" MaxLength="3" />
                                        </td>
                                        <td>Matrícula:
                                                    <asp:TextBox ID="txtMatricula" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" MaxLength="10" />
                                        </td>
                                        <td>Nome:<asp:TextBox ID="txtNome" runat="server" MaxLength="70" Width="400px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Banco:
                                                    <asp:TextBox ID="txtBanco" runat="server" onkeypress="mascara(this, soNumeros)" Width="157px" MaxLength="3" />
                                        </td>
                                        <td>Agência:
                                                    <asp:TextBox ID="txtAgencia" runat="server" onkeypress="mascara(this, soNumeros)" Width="109px" MaxLength="7" />
                                        </td>
                                        <td>Tipo Conta:
                                            <asp:TextBox ID="txtTipConta" runat="server" Width="180px" MaxLength="5" />
                                            Num. Conta:
                                            <asp:TextBox ID="txtNumConta" runat="server" Width="80px" MaxLength="15" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Representante:  
                                            <asp:TextBox ID="txtRepresentante" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" MaxLength="10" />
                                        </td>
                                        <td>CPF:  
                                            <asp:TextBox ID="txtCpf" runat="server" onkeypress="mascara(this, soNumeros)" Width="138px" MaxLength="11" />
                                        </td>
                                        <td>Data Movimentação
                                                    De:<asp:TextBox ID="txtProcessamentoIni" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangProcessamentoIni"
                                                Type="Date"
                                                ControlToValidate="txtProcessamentoIni"
                                                MaximumValue="31/12/9999"
                                                MinimumValue="31/12/1000"
                                                ErrorMessage="Data Inválida"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                ValidationGroup="ValitiAB1" />
                                            Até:<asp:TextBox ID="txtProcessamentoFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="146px" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangProcessamentoFim"
                                                Type="Date"
                                                ControlToValidate="txtProcessamentoFim"
                                                MaximumValue="31/12/9999"
                                                MinimumValue="31/12/1000"
                                                ErrorMessage="Data Inválida"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                ValidationGroup="ValitiAB1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" ValidationGroup="ValitiAB1" OnClick="btnPesquisar_Click" />

                                            <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnExportar" runat="server" Text="Exportar Excel" CssClass="button" OnClick="btnExportar_Click" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ObjectDataSource ID="odsHistoricoAtualizacao"
                                    runat="server"
                                    TypeName="IntegWeb.Financeira.Aplicacao.BLL.Tesouraria.AtualizaCcLoteBLL"
                                    SelectMethod="GetData"
                                    SelectCountMethod="GetDataCount"
                                    EnablePaging="True"
                                    SortParameterName="sortParameter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtEmpresa" Name="emp" PropertyName="Text" Type="Int16" />
                                        <asp:ControlParameter ControlID="txtMatricula" Name="matricula" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtRepresentante" Name="representante" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtNome" Name="nome" PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="txtCpf" Name="cpf" PropertyName="Text" Type="Int64" />
                                        <asp:ControlParameter ControlID="txtProcessamentoIni" Name="datProcessamentoIni" PropertyName="Text" Type="DateTime" />
                                        <asp:ControlParameter ControlID="txtProcessamentoFim" Name="datProcessamentoFim" PropertyName="Text" Type="DateTime" />
                                        <asp:ControlParameter ControlID="txtBanco" Name="codBanco" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtAgencia" Name="codAgencia" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtTipConta" Name="tipConta" PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="txtNumConta" Name="numConta" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>


                                <asp:GridView ID="grdHistAtualizacao"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataSourceID="odsHistoricoAtualizacao"
                                    EmptyDataText="Não retornou registros"
                                    AllowPaging="True"
                                    AllowSorting="True"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    Font-Size="9px">
                                    <Columns>
                                        <asp:BoundField DataField="ID_REG_HIST" HeaderText="ID" SortExpression="ID_REG_HIST" Visible="False" />
                                        <asp:BoundField DataField="DAT_PROCESSAMENTO" HeaderText="Dt.Processo" SortExpression="DAT_PROCESSAMENTO" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="COD_EMPRS" HeaderText="Empresa" SortExpression="COD_EMPRS" />
                                        <asp:BoundField DataField="NUM_RGTRO_EMPRG" HeaderText="Matricula" SortExpression="NUM_RGTRO_EMPRG" />
                                        <asp:BoundField DataField="NUM_IDNTF_RPTANT" HeaderText="Representante" SortExpression="NUM_IDNTF_RPTANT" />
                                        <asp:BoundField DataField="NOME" HeaderText="Nome" SortExpression="NOME" />
                                        <asp:BoundField DataField="NUM_CPF_EMPRG" HeaderText="CPF" SortExpression="NUM_CPF_EMPRG" />
                                        <asp:BoundField DataField="COD_BANCO" HeaderText="Banco" SortExpression="COD_BANCO" />
                                        <asp:BoundField DataField="COD_AGBCO" HeaderText="Agência" SortExpression="COD_AGBCO" />
                                        <asp:BoundField DataField="TIP_CTCOR_EMPRG" HeaderText="Tp.Conta" SortExpression="TIP_CTCOR_EMPRG" />
                                        <asp:BoundField DataField="NUM_CTCOR_EMPRG" HeaderText="Nº Conta" SortExpression="NUM_CTCOR_EMPRG" />
                                        <asp:TemplateField HeaderText="Critica" SortExpression="CRITICA">
                                            <ItemTemplate>
                                            <%--    <asp:Label ID="lblCritica" runat="server" Text='<%# Eval("CRITICA") %>' ></asp:Label>--%>
                                                <asp:Label ID="lblCritica" runat="server" Text='<%# Eval("CRITICA").ToString() %>' ForeColor='<%# (Eval("CRITICA").ToString().Contains("com sucesso")) ? System.Drawing.Color.Green : System.Drawing.Color.Red  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <%--ABA 2--%>
                        <ajax:TabPanel ID="TbImportaArquivo" HeaderText="Importação de Arquivo" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Entre com o arquivo de carga:</td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnProcessar" runat="server" Text="Processar" CssClass="button" OnClick="btnProcessar_Click" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:Panel ID="pnlGridHistAtu" runat="server" Visible="true">
                                    <table>
                                        <tr>
                                            <td>Últimas importações</td>
                                            <td>
                                                <asp:DropDownList ID="ddlAtuContas" runat="server" AutoPostBack="true" Width="300px" OnTextChanged="ddlAtuContas_TextChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                               Apenas críticas:  <asp:CheckBox ID="chkPesqComCritica" runat="server" AutoPostBack="true" OnCheckedChanged="ddlAtuContas_TextChanged" />
                                            </td>
                                        </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td>Total de linhas</td>
                                            <td>
                                                <asp:Label ID="lblTotalLinhas" runat="server" Text="-" Style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td>Qtd de críticas</td>
                                            <td>
                                                <asp:Label ID="lblQtdErro" runat="server" Text="-" Style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:ObjectDataSource ID="odsConsultaHistorico"
                                    runat="server"
                                    TypeName="IntegWeb.Financeira.Aplicacao.BLL.Tesouraria.AtualizaCcLoteBLL"
                                    SelectMethod="GetDataHist"
                                    SelectCountMethod="GetDataCountHist"
                                    EnablePaging="True"
                                    SortParameterName="sortParameter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlAtuContas" Name="pDT_ATU_CONTAS" PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="chkPesqComCritica" Name="pCHK_CRITICA" PropertyName="Checked" Type="Boolean" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                     <asp:GridView ID="grdConsultaHistorico"
                                    runat="server"
                                    DataKeysNames="pDT_ATU_CONTAS"
                                    AutoGenerateColumns="False"
                                    DataSourceID="odsConsultaHistorico"
                                    EmptyDataText="Não retornou registros"
                                    AllowPaging="True"
                                    AllowSorting="True"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    Font-Size="9px">
                                    <Columns>
                                        <asp:BoundField DataField="ID_REG_HIST" HeaderText="ID" SortExpression="ID_REG_HIST" Visible="False" />
                                        <asp:BoundField DataField="DAT_PROCESSAMENTO" HeaderText="Dt.Processo" SortExpression="DAT_PROCESSAMENTO" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="COD_EMPRS" HeaderText="Empresa" SortExpression="COD_EMPRS" />
                                        <asp:BoundField DataField="NUM_RGTRO_EMPRG" HeaderText="Matricula" SortExpression="NUM_RGTRO_EMPRG" />
                                        <asp:BoundField DataField="NUM_IDNTF_RPTANT" HeaderText="Representante" SortExpression="NUM_IDNTF_RPTANT" />
                                        <asp:BoundField DataField="NOME" HeaderText="Nome" SortExpression="NOME" />
                                        <asp:BoundField DataField="NUM_CPF_EMPRG" HeaderText="CPF" SortExpression="NUM_CPF_EMPRG" />
                                        <asp:BoundField DataField="COD_BANCO" HeaderText="Banco" SortExpression="COD_BANCO" />
                                        <asp:BoundField DataField="COD_AGBCO" HeaderText="Agência" SortExpression="COD_AGBCO" />
                                        <asp:BoundField DataField="TIP_CTCOR_EMPRG" HeaderText="Tp.Conta" SortExpression="TIP_CTCOR_EMPRG" />
                                        <asp:BoundField DataField="NUM_CTCOR_EMPRG" HeaderText="Nº Conta" SortExpression="NUM_CTCOR_EMPRG" />
                                        <asp:TemplateField HeaderText="Critica" SortExpression="CRITICA">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblCriticaHist" runat="server" Text='<%# Eval("CRITICA") %>' ></asp:Label>--%>
                                                <asp:Label ID="lblCriticaHist" runat="server" Text='<%# Eval("CRITICA").ToString() %>' ForeColor='<%# (Eval("CRITICA").ToString().Contains("com sucesso")) ? System.Drawing.Color.Green : System.Drawing.Color.Red  %>'></asp:Label>
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
                    <asp:PostBackTrigger ControlID="TabContainer$TbImportaArquivo$btnProcessar" />
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
