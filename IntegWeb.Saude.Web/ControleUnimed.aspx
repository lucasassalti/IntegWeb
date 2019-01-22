<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ControleUnimed.aspx.cs" Inherits="IntegWeb.Saude.Web.ControleUnimed" %>

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

        function show() {

            if ($('#<%= ddlTipoInc.ClientID %>').val() == 'CANCELAMENTO') {
                $('#<%= lblDatCancInc.ClientID %>').show();
                $('#<%= txtDatCancInc.ClientID %>').show();
            }
            else {
                $('#<%= lblDatCancInc.ClientID %>').hide();
                $('#<%= txtDatCancInc.ClientID %>').hide();
            }
        }


    </script>



    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Controle Unimed </h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">





                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbControleUnimed" HeaderText="Controle Relatórios Unimed" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div id="divPesquisar" runat="server" class="tabelaPagina">
                                    <asp:Panel ID="pnlControleUnimed" runat="server">
                                        <table>
                                            <tr>
                                                <td>Empresa:
                                                    <asp:TextBox ID="txtEmpresa" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="3" />
                                                </td>

                                                <td>Identificação:
                                                    <asp:TextBox ID="txtCodIdentificacao" runat="server" MaxLength="25" onkeypress="mascara(this, soNumeros)" Width="150px" />
                                                </td>
                                                <td>Nome:<asp:TextBox ID="txtNome" runat="server" MaxLength="70" Width="300px" />
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>Matrícula:
                                                    <asp:TextBox ID="txtMatricula" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" MaxLength="10" />
                                                </td>


                                                <td>Nome Unimed:
                                                    <asp:DropDownList ID="ddlNomeUnimed" runat="server" />
                                                </td>
                                                <td>Movimentação:
                                                    <asp:DropDownList ID="ddlMovimentacao" runat="server">
                                                        <asp:ListItem Text="Inclusão" Value="INCLUSÃO" />
                                                        <asp:ListItem Text="Cancelamento" Value="CANCELAMENTO" />
                                                        <asp:ListItem Text="Troca" Value="TROCA" />
                                                        <asp:ListItem Text="2º Via" Value="SEGUNDA_VIA" />
                                                        <asp:ListItem Text="Renovação" Value="RENOVACAO" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Sub:
                                                    <asp:TextBox ID="txtSubMatricula" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="2" />
                                                </td>

                                                <td>Data Movimentação
                                                    De:<asp:TextBox ID="txtDatMovimentacaoIni" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangDatMovimentacaoIni"
                                                        Type="Date"
                                                        ControlToValidate="txtDatMovimentacaoIni"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        ValidationGroup="ValitiAB1" />
                                                    Até:<asp:TextBox ID="txtDatMovimentacaoFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangDatMovimentacaoFim"
                                                        Type="Date"
                                                        ControlToValidate="txtDatMovimentacaoFim"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        ValidationGroup="ValitiAB1" />
                                                </td>
                                                <td>Data Saída
                                                    De:<asp:TextBox ID="txtDatSaidaIni" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangDatSaidaIni"
                                                        Type="Date"
                                                        ControlToValidate="txtDatSaidaIni"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        ValidationGroup="ValitiAB1" />

                                                    Até:<asp:TextBox ID="txtDatSaidaFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="RangeDatSaidaFim"
                                                        Type="Date"
                                                        ControlToValidate="txtDatSaidaFim"
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
                                                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" ValidationGroup="ValitiAB1" OnClick="btnPesquisar_Click" CausesValidation="true" />

                                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnEdicaoLote" runat="server" Text="Edição em Lote" CssClass="button" OnClick="btnEdicaoLote_Click" />
                                                    <asp:Button ID="btnExportar" OnClick="btnExportar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Exportar Excel" />
                                                    <asp:Button ID="btnInclusaoLote" runat="server" Text="Inclusão em Lote" CssClass="button" OnClick="btnInclusaoLote_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnInclusao" runat="server" Text="Inclusão Individual" CssClass="button" OnClick="btnInclusao_Click" />
                                                    <asp:Button ID="btnEtq" runat="server" Text="Emissão Etiquetas" CssClass="button" OnClick="btnEtq_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <%--  <asp:ObjectDataSource ID="odsControleUnimed"
                                        runat="server"
                                        TypeName="IntegWeb.Saude.Aplicacao.BLL.ControleUnimedBLL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtEmpresa" Name="emp" PropertyName="Text" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtMatricula" Name="matricula" PropertyName="Text" Type="Int32" />
                                            <asp:ControlParameter ControlID="txtSubMatricula" Name="sub" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtNome" Name="nome" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtCodIdentificacao" Name="codIdentificacao" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="ddlNomeUnimed" Name="codUnimed" PropertyName="SelectedValue" Type="String" />
                                            <asp:ControlParameter ControlID="ddlMovimentacao" Name="tipMovimentacao" PropertyName="SelectedValue" Type="String" />
                                            <asp:ControlParameter ControlID="txtDatMovimentacaoIni" Name="datMovimentacaoIni" PropertyName="Text" Type="DateTime" />
                                            <asp:ControlParameter ControlID="txtDatMovimentacaoFim" Name="datMovimentacaoFim" PropertyName="Text" Type="DateTime" />
                                            <asp:ControlParameter ControlID="txtDatSaidaIni" Name="datSaidaIni" PropertyName="Text" Type="DateTime" />
                                            <asp:ControlParameter ControlID="txtDatSaidaFim" Name="datSaidaFim" PropertyName="Text" Type="DateTime" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>--%>

                                    <div style="overflow: scroll;">
                                        <asp:GridView ID="grdControleUnimed"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            EmptyDataText="Não retornou registros"
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            CssClass="Table"
                                            ClientIDMode="Static"
                                            PageSize="10"
                                            OnRowEditing="grdControleUnimed_RowEditing"
                                            OnRowCommand="grdControleUnimed_RowCommand"
                                            OnRowCancelingEdit="grdControleUnimed_RowCancelingEdit"
                                            OnPageIndexChanging="grdControleUnimed_PageIndexChanging"
                                            OnSorting="grdControleUnimed_Sorting"
                                            Font-Size="9px">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Text="" class="span_checkbox" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Código" SortExpression="COD_CONTROLEUNIMED" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodControleUnimed" runat="server" Text='<%# Bind("COD_CONTROLEUNIMED") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Empresa" SortExpression="COD_EMPRS" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Matrícula" SortExpression="NUM_MATRICULA" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumMatricula" runat="server" Text='<%# Bind("NUM_MATRICULA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sub" SortExpression="SUB_MATRICULA" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSub" runat="server" Text='<%# Bind("SUB_MATRICULA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Identificação" SortExpression="COD_IDENTIFICACAO" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodIdentificacao" runat="server" Text='<%# Bind("COD_IDENTIFICACAO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nome" SortExpression="NOM_PARTICIP" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNome" runat="server" Text='<%# Bind("NOM_PARTICIP") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unimed" SortExpression="DES_PLANO" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesUnimed" runat="server" Text='<%# Bind("DES_PLANO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Movimentação" SortExpression="TIPO" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMovimentacao" runat="server" Text='<%# Bind("TIPO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dt_Mov" SortExpression="DAT_GERACAO" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDatMovimentacao" runat="server" Text='<%# Bind("DAT_GERACAO","{0:dd/MM/yyyy}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dt_Saída" SortExpression="ENVIO_UNIMED" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDatSaida" runat="server" Text='<%# Bind("ENVIO_UNIMED","{0:dd/MM/yyyy}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDatSaida" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Text='<%# Bind("ENVIO_UNIMED","{0:dd/MM/yyyy}") %>' Width="70px" />
                                                        <asp:RangeValidator
                                                            runat="server"
                                                            ID="rangDatSaida"
                                                            Type="Date"
                                                            ControlToValidate="txtDatSaida"
                                                            MaximumValue="31/12/9999"
                                                            MinimumValue="31/12/1000"
                                                            ErrorMessage="Data Inválida"
                                                            ForeColor="Red"
                                                            Display="Dynamic"
                                                            ValidationGroup="ValitiGDR1" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Memorial" SortExpression="COBRANCA_MEMORIAL" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMemorial" runat="server" Text='<%# Bind("COBRANCA_MEMORIAL") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlMemorial" runat="server" SelectedValue='<%# Eval("COBRANCA_MEMORIAL") %>'>
                                                            <asp:ListItem Text="N" Value="N" />
                                                            <asp:ListItem Text="S" Value="S" />
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="N_Unimed" SortExpression="NUMERO_UNIMED" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumeroUnimed" runat="server" Text='<%# Bind("NUMERO_UNIMED") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtNumeroUnimed" runat="server" Text='<%# Bind("NUMERO_UNIMED") %>' onkeypress="mascara(this, soNumeros)" MaxLength="17" Width="130px" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Via" SortExpression="NUMERO_VIA" HeaderStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumeroVia" runat="server" Text='<%# Bind("NUMERO_VIA") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtNumeroVia" runat="server" Text='<%# Bind("NUMERO_VIA") %>' onkeypress="mascara(this, soNumeros)" MaxLength="10" Width="20px" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" CssClass="button" Font-Size="9px" Width="50px" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="UpdateAB1" CssClass="button" ValidationGroup="ValitiGDR1" Font-Size="7px" Width="50px" />
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" CausesValidation="false" Font-Size="7px" Width="50px" OnClick="btnCancelarInc_Click" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                </div>


                                <ajax:ModalPopupExtender
                                    ID="ModalPopupExtender1"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopup"
                                    TargetControlID="btnEdicaoLote"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="panelPopup" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Atualização em Lote:</h3>
                                    <table>
                                        <tr>
                                            <td>Data Saída:
                                                <asp:TextBox ID="txtDatSaidaPopUp" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangDatSaidaPopUp"
                                                    Type="Date"
                                                    ControlToValidate="txtDatSaidaPopUp"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValidaPopUp" />
                                            </td>
                                            <td>Memorial:
                                                  <asp:DropDownList ID="ddlMemorialPopUp" runat="server">
                                                      <asp:ListItem Text="N" Value="N" />
                                                      <asp:ListItem Text="S" Value="S" />
                                                  </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <%--                            <tr>
                                            <td>Número Unimed:
                                                 <asp:TextBox ID="txtNumeroUnimedPopUp" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="17" Width="150px" />
                                            </td>
                                            <td>Nº Via:
                                                  <asp:TextBox ID="txtNumeroViaPopUp" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="10" Width="50px" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAtualizarLote" runat="server" Text="Atualizar" CssClass="button" OnClick="btnAtualizarLote_Click" CausesValidation="true" ValidationGroup="ValidaPopUp" />
                                                <asp:Button ID="btnCancelarPopUp" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarPopUp_Click" />
                                            </td>

                                        </tr>
                                    </table>

                                </asp:Panel>

                                <div id="divInclusaoLote" runat="server" class="tabelaPagina" visible="False">

                                    <asp:Panel ID="pnlInclusaoLote" runat="server" class="tabelaPagina">
                                        <table>
                                            <tr>
                                                <td>Entre com o arquivo de carga:</td>
                                                <td>
                                                    <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnProcessar" OnClientClick="return postbackButtonClick();" Text="Processar" OnClick="btnProcessar_Click" CssClass="button" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnVoltar" CssClass="button" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>

                                <asp:ObjectDataSource ID="odsPlanoAutomatico"
                                    runat="server"
                                    TypeName="IntegWeb.Saude.Aplicacao.BLL.ControleUnimedBLL"
                                    SelectMethod="GetData"
                                    SelectCountMethod="GetDataCount"
                                    EnablePaging="True"
                                    SortParameterName="sortParameter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtEmpresaInc" Name="emp" PropertyName="Text" Type="Int16" />
                                        <asp:ControlParameter ControlID="txtMatriculaInc" Name="matricula" PropertyName="Text" Type="string" />
                                        <asp:ControlParameter ControlID="txtSubInc" Name="sub" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>


                                <ajax:ModalPopupExtender
                                    ID="ModalPopupExtender2"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopUpIn"
                                    TargetControlID="btnInclusao"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="panelPopUpIn" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Inserir Participante Individual:</h3>
                                    <table>
                                        <tr>
                                            <td>Empresa:
                                                 <asp:TextBox ID="txtEmpresaInc" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="3" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqEmpresaInc" ControlToValidate="txtEmpresaInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />

                                                Matrícula:
                                                    <asp:TextBox ID="txtMatriculaInc" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqMatriculaInc" ControlToValidate="txtMatriculaInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />
                                                Sub:
                                                    <asp:TextBox ID="txtSubInc" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="2" AutoPostBack="true" OnTextChanged="txtSubInc_TextChanged" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqSubInc" ControlToValidate="txtSubInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />
                                                Data Adesão:
                                                <asp:TextBox ID="txtDataAdesaoInc" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqDataAdesaoInc" ControlToValidate="txtDataAdesaoInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangDataAdesaoInc"
                                                    Type="Date"
                                                    ControlToValidate="txtDataAdesaoInc"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValidaInc" />

                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Identificação:
                                                    <asp:TextBox ID="txtIdenticacaoInc" runat="server" MaxLength="25" onkeypress="mascara(this, soNumeros)" Width="150px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqIdenticacaoInc" ControlToValidate="txtIdenticacaoInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />

                                                Nome:<asp:TextBox ID="txtNomeInc" runat="server" MaxLength="70" Width="300px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqNomeInc" ControlToValidate="txtNomeInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>CPF:
                                                <asp:TextBox ID="txtCPFInc" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" MaxLength="11" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqCPFInc" ControlToValidate="txtCPFInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />
                                                Sexo:
                                                    <asp:DropDownList ID="ddlSexoInc" runat="server">
                                                        <asp:ListItem Text="F" Value="F" />
                                                        <asp:ListItem Text="M" Value="M" />
                                                    </asp:DropDownList>

                                                Código Plano Saúde:
                                                 <asp:Label ID="lblCodPlanSaude" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="3" OnTextChanged="txtSubInc_TextChanged" />


                                                Data Nascimento:
                                                <asp:TextBox ID="txtDatNascInc" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqDatNascInc" ControlToValidate="txtDatNascInc" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaInc" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangDatNascInc"
                                                    Type="Date"
                                                    ControlToValidate="txtDatNascInc"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValidaInc" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Unimed:
                                                    <asp:DropDownList ID="ddlUnimedInc" runat="server" />
                                                Acomodação:
                                                    <asp:DropDownList ID="ddlAcomodInc" runat="server">
                                                        <asp:ListItem Text="Apartamento" Value="APTO" />
                                                        <asp:ListItem Text="Quarto" Value="QUARTO" />
                                                    </asp:DropDownList>
                                                Movimentação:
                                                    <asp:DropDownList ID="ddlTipoInc" runat="server" onchange="show()">
                                                        <asp:ListItem Text="Inclusão" Value="INCLUSÃO" />
                                                        <asp:ListItem Text="Cancelamento" Value="CANCELAMENTO" />
                                                        <asp:ListItem Text="Troca" Value="TROCA" />
                                                        <asp:ListItem Text="2º Via" Value="SEGUNDA_VIA" />
                                                        <asp:ListItem Text="Renovação" Value="RENOVACAO" />
                                                    </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Número Unimed:
                                                 <asp:TextBox ID="txtNumeroUnimedInc" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="17" Width="150px" />
                                                Nº Via:
                                                  <asp:TextBox ID="txtNumeroViaInc" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="10" Width="50px" />

                                                <asp:Label ID="lblDatCancInc" runat="server" Text="Data Cancelamento:" Style="display: none" />
                                                <asp:TextBox ID="txtDatCancInc" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" Style="display: none" />

                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangDatCancInc"
                                                    Type="Date"
                                                    ControlToValidate="txtDatCancInc"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValidaInc" />

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInserirIn" runat="server" Text="Inserir" CssClass="button" ValidationGroup="ValidaInc" CausesValidation="true" OnClick="btnInserirIn_Click" />
                                                <asp:Button ID="btnCancelarInc" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarInc_Click" />
                                            </td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <%--ABA 2--%>
                        <ajax:TabPanel ID="TbExtracaoRelatorio" HeaderText="Extração Relatórios Unimed" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Data Inicio:</td>
                                        <td>
                                            <asp:TextBox ID="txtDatIni" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqDatIni" ControlToValidate="txtDatIni" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiAB2" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangDatIni"
                                                Type="Date"
                                                ControlToValidate="txtDatIni"
                                                MaximumValue="31/12/9999"
                                                MinimumValue="31/12/1000"
                                                ErrorMessage="Data Inválida"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                ValidationGroup="ValitiAB2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Data Fim:</td>
                                        <td>
                                            <asp:TextBox ID="txtDatFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqDatFim" ControlToValidate="txtDatFim" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiAB2" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="RangeDatFim"
                                                Type="Date"
                                                ControlToValidate="txtDatFim"
                                                MaximumValue="31/12/9999"
                                                MinimumValue="31/12/1000"
                                                ErrorMessage="Data Inválida"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                ValidationGroup="ValitiAB2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nome Unimed:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlNomeUnimedAB2" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>Movimentação:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlMovimentacaoAB2" runat="server">
                                                <asp:ListItem Text="Inclusão" Value="INCLUSÃO" />
                                                <asp:ListItem Text="Cancelamento" Value="CANCELAMENTO" />
                                                <asp:ListItem Text="Troca" Value="TROCA" />
                                                <asp:ListItem Text="2º Via" Value="SEGUNDA_VIA" />
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnRelatorio" runat="server" Text="Carregar Relatório" CssClass="button" ValidationGroup="ValitiAB2" OnClick="btnRelatorio_Click" CausesValidation="true" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <%--ABA 3--%>
                        <ajax:TabPanel ID="TbAtualizacaoTabela" HeaderText="Atualização Tabela CI Unimed" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <asp:Panel ID="pnlAtualizaTabela" runat="server">

                                    <h3>Atualização Tabela CI Unimed</h3>
                                    &nbsp
                                       <asp:ObjectDataSource ID="odsAtualizaTabela"
                                           runat="server"
                                           TypeName="IntegWeb.Saude.Aplicacao.BLL.ControleUnimedBLL"
                                           SelectMethod="GetValorCI"></asp:ObjectDataSource>

                                    <asp:GridView ID="grdAtualizaTabela"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        DataSourceID="odsAtualizaTabela"
                                        EmptyDataText="Não retornou registros"
                                        CssClass="Table"
                                        OnRowCommand="grdAtualizaTabela_RowCommand">
                                        <Columns>

                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cód. Plano">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodPlanoAB3" runat="server" Text='<%# Bind("COD_PLANO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Plano">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlanoAB3" runat="server" Text='<%# Bind("DES_PLANO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Inclusão">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInclusaoAB3" runat="server" Text='<%# Bind("INCLUSAO","{0:N2}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtInclusaoAB3" runat="server" Text='<%# Bind("INCLUSAO") %>' onkeypress="mascara(this, moeda)" Width="70px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Segunda Via">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSegViaAB3" runat="server" Text='<%# Bind("SEGUNDA_VIA","{0:N2}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSegViaAB3" runat="server" Text='<%# Bind("SEGUNDA_VIA") %>' onkeypress="mascara(this, moeda)" Width="70px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Renovação">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRenovacaoAB3" runat="server" Text='<%# Bind("RENOVACAO","{0:N2}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRenovacaoAB3" runat="server" Text='<%# Bind("RENOVACAO") %>' onkeypress="mascara(this, moeda)" Width="70px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Início">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatInicioVigenciaAB3" runat="server" Text='<%# Bind("DAT_INICIO_VIGENCIA","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDatInicioVigenciaAB3" runat="server" Text='<%# Bind("DAT_INICIO_VIGENCIA","{0:dd/MM/yyyy}") %>' Width="70px" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" ValidationGroup="ValitiGDR3" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangDatInicioVigenciaAB3"
                                                        Type="Date"
                                                        ControlToValidate="txtDatInicioVigenciaAB3"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <%--  <asp:TemplateField HeaderText="Data Fim">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatFimVigenciaAB3" runat="server" Text='<%# Bind("DAT_FIM_VIGENCIA","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDatFimVigenciaAB3" runat="server" Text='<%# Bind("DAT_FIM_VIGENCIA","{0:dd/MM/yyyy}") %>' Width="70px" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" ValidationGroup="ValitiGDR3"/>
                                                      <asp:RangeValidator
                                                            runat="server"
                                                            ID="rangtxtDatFimVigenciaAB3"
                                                            Type="Date"
                                                            ControlToValidate="txtDatFimVigenciaAB3"
                                                            MaximumValue="31/12/9999"
                                                            MinimumValue="31/12/1000"
                                                            ErrorMessage="Data Inválida"
                                                            ForeColor="Red"
                                                            Display="Dynamic" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarAB3" runat="server" Text="Editar" CommandName="Edit" CssClass="button" CausesValidation="false" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvarAB3" runat="server" Text="Salvar" CommandName="UpdateAB3" CssClass="button" ValidationGroup="ValitiGDR3" />
                                                    <asp:Button ID="btnCancelarAB3" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" CausesValidation="false" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </ajax:TabPanel>
                         <%--ABA 4--%>
                               <ajax:TabPanel ID="TbBateCadastral" HeaderText="Bate Cadastral" runat="server" TabIndex="0">
                            <ContentTemplate>
                                  <table>
                                            <tr>
                                                <td>Entre com o arquivo de carga:</td>
                                                <td>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="button" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="Button1"  Text="Processar" CssClass="button" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button2" CssClass="button" runat="server" Text="Voltar" />
                                                </td>
                                            </tr>
                                        </table>

                            </ContentTemplate>
                        </ajax:TabPanel>

                        <%--ABA 5--%>
                        <ajax:TabPanel ID="TbUnimedsExistentes" HeaderText="Unimeds Existentes" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <h3>Parametrização Unimed</h3>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnParametrizacaoUnimed" runat="server" Text="Inserir Unimed" CssClass="button" OnClick="btnParametrizacaoUnimed_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:ObjectDataSource ID="odsUnimedExistente"
                                    runat="server"
                                    TypeName="IntegWeb.Saude.Aplicacao.BLL.ControleUnimedBLL"
                                    SelectMethod="GetUnimed"></asp:ObjectDataSource>

                                <asp:GridView ID="grdUnimedExistente"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    DataSourceID="odsUnimedExistente"
                                    DataKeyNames="COD_PLANO"
                                    EmptyDataText="Não retornou registros"
                                    CssClass="Table"
                                    OnRowCommand="grdUnimedExistente_RowCommand">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Cód. Plano">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodPlanoAB4" runat="server" Text='<%# Bind("COD_PLANO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descrição Plano">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescPlanoAB4" runat="server" Text='<%# Bind("DES_PLANO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnExcluirAB4" runat="server" Text="Excluir" CommandName="DeleteAB4" CssClass="button" CausesValidation="false" CommandArgument='<%# Bind("COD_PLANO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <ajax:ModalPopupExtender
                                    ID="ModalPopupExtender3"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="pnlPopUpIncUnimed"
                                    TargetControlID="btnParametrizacaoUnimed"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="pnlPopUpIncUnimed" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Parametrização Unimed:</h3>
                                    <table>
                                        <tr>
                                            <td>Código Unimed:
                                                 <asp:TextBox ID="txtCodUnimedAB4" runat="server" onkeypress="mascara(this, soNumeros)" Width="50px" MaxLength="3" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqCodUnimedAB4" ControlToValidate="txtCodUnimedAB4" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB4" />

                                                Descrição Unimed:
                                                    <asp:TextBox ID="txtDesUnimedAB4" runat="server" MaxLength="70" Width="300px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqDesUnimedAB4" ControlToValidate="txtDesUnimedAB4" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB4" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Valor CI Inclusão:
                                                 <asp:TextBox ID="txtVlInclusaoAB4" runat="server" onkeypress="mascara(this, moeda)" Width="70px" />
                                                Valor CI Segunda Via:
                                                 <asp:TextBox ID="txtVlSegViaAB4" runat="server" onkeypress="mascara(this, moeda)" Width="70px" />
                                                Valor CI Renovação:
                                                <asp:TextBox ID="txtVlRenovacaoAB4" runat="server" onkeypress="mascara(this, moeda)" Width="70px" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInserirAB4" runat="server" Text="Inserir" CssClass="button" ValidationGroup="ValidaAB4" CausesValidation="true" OnClick="btnInserirAB4_Click" />
                                                <asp:Button ID="btnCancelarAB4" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarAB4_Click" />
                                            </td>
                                        </tr>
                                </asp:Panel>
                            </ContentTemplate>
                        </ajax:TabPanel>

                 

                    </ajax:TabContainer>

                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="false" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer" />
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
