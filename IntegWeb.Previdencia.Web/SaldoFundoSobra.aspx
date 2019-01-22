<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="SaldoFundoSobra.aspx.cs" Inherits="IntegWeb.Previdencia.Web.SaldoFundoSobra" %>

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

        <h1>Saldo de Contas</h1>

        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>

                <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">

                    <ajax:TabPanel ID="tbMunicipio" HeaderText="Saldo" runat="server" TabIndex="0" class="tabelaPagina">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>Mês:
                                            <asp:TextBox ID="txtMes" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="2" />
                                    </td>
                                    <td>Ano:
                                        
                                            <asp:TextBox ID="txtAno" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="4" />
                                    </td>

                                    <td>Grupo Empresa:
                                            <asp:DropDownList ID="dropSaldo" runat="server"></asp:DropDownList></td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">

                                        <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                        <asp:Button ID="btnInserir" runat="server" CssClass="button" Text="Inserir Novo Registro" OnClick="btnInserir_Click" />

                                    </td>
                                </tr>
                            </table>

                            <ajax:ModalPopupExtender
                                ID="Popup"
                                runat="server"
                                DropShadow="true"
                                PopupControlID="panelPopup"
                                TargetControlID="btnInserir"
                                BackgroundCssClass="modalBackground">
                            </ajax:ModalPopupExtender>

                            <asp:Panel ID="panelPopup" runat="server" Style="display: none; background-color: white; border: 1px solid black">

                                <h3>Inserção Saldo de Contas:</h3>
                                <table>
                                    <tr>
                                        <td>Código Saldo Fundo:
                                                 <asp:TextBox ID="txtInserirCodSaldo" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="4" />
                                              <asp:RequiredFieldValidator
                                                    id="reqInserirCodSaldo"
                                                    ControlToValidate="txtInserirCodSaldo"
                                                    Text="Informe algum valor nos campos."
                                                    ValidationGroup="grpInserir"
                                                    ForeColor="Red"
                                                    Runat="server" />
                                           
                                            Ano Referência:
                                                 <asp:TextBox ID="txtInserirAno" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="4" />

                                            Mês Referência:
                                                <asp:TextBox ID="txtInserirMes" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Código Grupo:
                                                <asp:TextBox ID="txtInserirCod" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="4" />
                                            Plano:
                                                <asp:TextBox ID="txtInserirNumero" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="10" />
                                            Valor Saldo Fundo:
                                                <asp:TextBox ID="txtInserirValor" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="15" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSalvarInserir" runat="server" CssClass="button" Text="Inserir" OnClick="btnSalvar_Click"  ValidationGroup="grpInserir"   />
                                            <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" />
                                        </td>
                                    </tr>

                                </table>
                            </asp:Panel>
                            <tr>

                                <asp:ObjectDataSource runat="server" ID="odsSaldoFundo"
                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao.SaldoFundoSobraBLL"
                                    SelectMethod="GetData"
                                    SelectCountMethod="GetDataCount"
                                    EnablePaging="true"
                                    SortParameterName="sortParameter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtMes" Name="pMes" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtAno" Name="pAno" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="dropSaldo" Name="codEmp" PropertyName="SelectedValue" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdDadosSaldo" runat="server"
                                    AllowPaging="true"
                                    AllowSorting="true"
                                    DataSourceID="odsSaldoFundo"
                                    AutoGenerateColumns="False"
                                    OnRowCommand="grdDadosSaldo_RowCommand"
                                    EmptyDataText="A consulta não retornou registros"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    PageSize="8">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Código Saldo Fundo" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodSaldo" runat="server" Text='<%# Bind("COD_SALDO_FUNDO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Ano de referência" SortExpression="ANO_REF">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAnoRef" runat="server" Text='<%# Bind("ANO_REF") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Mês de referência" SortExpression="MES_REF">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMesRef" runat="server" Text='<%# Bind("MES_REF") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Grupo Empresa" SortExpression="COD_GRUPO_EMPRS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrupo" runat="server" Text='<%# Bind("COD_GRUPO_EMPRS") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Plano" SortExpression="NUM_PLBNF">

                                            <ItemTemplate>
                                                <asp:Label ID="lblNum" runat="server" Text='<%# Bind("NUM_PLBNF") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Valor Saldo Fundo" SortExpression="VLR_SALDO_FUNDO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaldo" runat="server" Text='<%# Bind("VLR_SALDO_FUNDO","{0:N2}") %>' />
                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtSaldo" runat="server" Text='<%# Bind("VLR_SALDO_FUNDO","{0:N2}") %>' Width="100%" MaxLength="150"  onkeypress="mascara(this, soNumeros)" />

                                                 <asp:RequiredFieldValidator
                                                    id="reqSaldo"
                                                    ControlToValidate="txtSaldo"
                                                    Text="Insira um valor no campo."
                                                    ValidationGroup="grpUpdateSaldo"
                                                    ForeColor="Red"
                                                    Runat="server"
                                                    MaxLength="15"  />

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnSalvarGrid" runat="server" Text="Salvar" CommandName="Gravar" CssClass="button" CausesValidation="true" ValidationGroup="grpUpdateSaldo" />
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </tr>

                        </ContentTemplate>
                    </ajax:TabPanel>
                </ajax:TabContainer>
            </ContentTemplate>

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
</asp:Content>
