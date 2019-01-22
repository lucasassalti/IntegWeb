<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CargaEquipSimpro.aspx.cs" Inherits="IntegWeb.Saude.Web.CargaEquipSimpro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">


        var updateProgress = null;
        function postbackButtonClick() {
            if (Page_ClientValidate()) {
                updateProgress = $find("<%= UpdateProg1.ClientID %>");
                window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
                return true;
            }
        }

        function SubmitEnter(teclapres) {
            var tecla = teclapres.keyCode;
            console.log(teclapres.keyCode);
            var botaoPesq = '<%= btnPesquisar.ClientID %>';

            if (tecla == 13) {
                event.returnValue = false;
                event.cancel = true;
                document.getElementById(botaoPesq).click();
            }

            return false;
       }
    </script>
    <div class="full_w">

        <div class="h_title">
        </div>

        <div class="tabelaPagina">

            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">

                        <ajax:TabPanel ID="TabSimpro" HeaderText="Carga da Simpro" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <h1>Carga de Equipamentos da Simpro</h1>

                                <table id="tabDDL">
                                    <tr>
                                        <td>Selecione o Convenente:<br />
                                            <asp:DropDownList ID="ddlConvenente" runat="server" OnSelectedIndexChanged="ddlConvenente_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="reqDdlConvenente" ControlToValidate="ddlConvenente" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblContrat" runat="server" Text="Tipo de Condição Contradual:"></asp:Label><br />
                                            <asp:DropDownList ID="ddlContrat" runat="server"></asp:DropDownList>

                                        </td>
                                    </tr>
                                </table>

                                <div id="divInserir" runat="server">

                                    <table>
                                        <tr>    
                                            <td>Taxa a ser Aplicada:
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtTaxa" OnKeyPress="mascara(this,Simpro)" MaxLength="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqTxtTaxa" ControlToValidate="txtTaxa" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />

                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Tipo de Preço:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlTipoPreco">
                                                    <asp:ListItem Text="Fração" Value="FF"></asp:ListItem>
                                                    <asp:ListItem Text="Embalagem" Value="FE"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="reqDdlTipoPreco" ControlToValidate="ddlTipoPreco" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />


                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Validade :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtValidade" CssClass="date" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="ReqTxtValidade" ControlToValidate="txtValidade" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangTxtValidade"
                                                    Type="Date"
                                                    ControlToValidate="txtValidade"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic" ValidationGroup="grpParametros" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>Selecione o Arquivo Excel: xls*<br />
                                                <asp:FileUpload ID="fileExcel" runat="server" CssClass="button" />
                                                <asp:Button ID="btnProcessar" runat="server" Text="Processar" CssClass="button" OnClick="btnProcessar_Click" ValidationGroup="grpParametros" />
                                                <asp:Button ID="btnIncluir" runat="server" Text="Incluir Equipamentos" CssClass="button" OnClick="btnIncluir_Click" ValidationGroup="grpParametros" />
                                                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Processo_Mensagem" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                                <br />

                                <div id="divgGridview" runat="server" style="width: auto; height: 300px; overflow: scroll">

                                    <asp:GridView ID="GridEquip" runat="server"
                                        AutoGenerateColumns="false"
                                        ViewStateMode="Enabled"
                                        DataKeyNames="COD_TUSS">

                                        <Columns>

                                            <asp:BoundField DataField="COD_TUSS" HeaderText="Codigo TUSS" />
                                            <asp:BoundField DataField="DES_SIMPRO" HeaderText="Descrição Simpro" />
                                            <asp:BoundField DataField="taxa_aplicada" HeaderText="Taxa Aplicada" />
                                            <asp:BoundField DataField="tipo_preco" HeaderText="Tipo de Preço" />
                                            <asp:BoundField DataField="dat_validade" HeaderText="Validade" DataFormatString="{0:d}" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="TabTiras" HeaderText="Alteração de Qtde de Materiais" runat="server" TabIndex="0">
                            <ContentTemplate>

                                <h1>Alteração de Quantidade de Materiais</h1>
                                <table>
                                    <tr>
                                        <td>Código do material:
                                            <asp:TextBox runat="server" ID="txtMaterial" OnKeyDown="mascara(this,soNumeros); SubmitEnter(event);" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="ReqTxtMaterial" ControlToValidate="txtMaterial" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpMaterial" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnPesquisar" Text="Pesquisar" CssClass="button" OnClick="btnPesquisar_Click" ValidationGroup="grpMaterial" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnLimpar1" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click1" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />



                                <asp:GridView runat="server"
                                    ID="gridProcedimentos"
                                    AutoGenerateColumns="false"
                                    OnRowCommand="gridProcedimentos_RowCommand"
                                    EmptyDataText="Não há registros"
                                    DataKeyNames="COD_RECURSO,rcocodprocedimento">
                                    <Columns>
                                        <asp:BoundField DataField="rcocodprocedimento" HeaderText="Código do Material" />
                                        <asp:BoundField DataField="desRecurso" HeaderText="Descrição" />
                                        <asp:BoundField DataField="cod_recurso" HeaderText="Código Recurso" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelecionar" runat="server" CommandName="SelectAb" Text="Selecionar" CssClass="button" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                                <br />

                                <div id="divEquip" runat="server" visible="false">
                                    &nbsp &nbsp <b>Selecionado:</b>

                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblMaterial" Visible="false"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label runat="server" ID="lblDescri1" Text="<b>Descrição:</b>"></asp:Label><br />
                                                <asp:Label runat="server" ID="lblDescri"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnNovo" runat="server" Text="Incluir Nova Vigência" CssClass="button" CausesValidation="false" OnClick="btnNovo_Click" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblRecurso" Visible="false"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                    <br />

                                    &nbsp &nbsp
                                    <asp:Label runat="server" ID="lblTbEquip" Text='<%# tbEquip.Visible == true ? "<b>Cadastrar:</b>": "Cadastrado"%>'></asp:Label>
                                    <table runat="server" id="tbEquip" visible="false">
                                        <tr>
                                            <td>Qtde Embalagem:<br />
                                                <asp:TextBox runat="server" ID="txtQtde1" MaxLength="4" OnKeyPress="mascara(this,soNumeros)"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="ReqTxtQtde1" ControlToValidate="txtQtde1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEmb1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Data Início Vigência:<br />
                                                <asp:TextBox runat="server" ID="txtDtInicio1" CssClass="date" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="ReqTxtDtInicio1" ControlToValidate="txtDtInicio1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEmb1" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangTxtDtInicio"
                                                    Type="Date"
                                                    ControlToValidate="txtDtInicio1"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic" ValidationGroup="grpEmb1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Data Fim Vigência:<br />
                                                <asp:TextBox runat="server" ID="txtDtFim1" CssClass="date" MaxLength="10"></asp:TextBox>
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="RangTxtDtFim"
                                                    Type="Date"
                                                    ControlToValidate="txtDtFim1"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic" ValidationGroup="grpEmb1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="btnSalvarEquip" Text="Salvar" ValidationGroup="grpEmb1" OnClick="btnSalvarEquip_Click" CssClass="button" />
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:GridView
                                        runat="server"
                                        ID="gridEmb"
                                        AutoGenerateColumns="false"
                                        OnRowEditing="gridEmb_RowEditing"
                                        EmptyDataText="Não retornou Nenhum Registro"
                                        OnRowCancelingEdit="gridEmb_RowCancelingEdit"
                                        OnRowCommand="gridEmb_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qtde Embalagem:">

                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblEmbalagem" Text='<%# Bind("QTD") %>'></asp:Label><br />
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtQtde" Width="110px" Text='<%# Bind("QTD") %>' MaxLength="4" OnKeyPress="mascara(this,soNumeros)"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="ReqTxtQtde" ControlToValidate="txtQtde" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEmb" />
                                                </EditItemTemplate>

                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" ID="txtQtdeFooter" Width="110px" Text='<%# Bind("QTD") %>' MaxLength="5" OnKeyPress="mascara(this,soNumeros)"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="ReqTxtQtdeFooter" ControlToValidate="txtQtdeFooter" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpFooter" />
                                                </FooterTemplate>

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Data Início Vigência:">

                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDtInicio" Text='<%# Eval("DAT_INIVIG", "{0:d}") %>'></asp:Label><br />
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDtInicio" CssClass="date" Text='<%# Eval("DAT_INIVIG", "{0:d}") %>' MaxLength="10" Width="130px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="ReqTxtDtInicio" ControlToValidate="txtDtInicio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEmb" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangtxtDtInicio"
                                                        Type="Date"
                                                        ControlToValidate="txtDtInicio"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" ValidationGroup="grpEmb" />
                                                </EditItemTemplate>

                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" ID="txtDtInicioFooter" CssClass="date" Text='<%# Eval("DAT_INIVIG", "{0:d}") %>' MaxLength="10" Width="130px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="ReqtxtDtInicioFooter" ControlToValidate="txtDtInicioFooter" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpFooter" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangtxtDtInicio"
                                                        Type="Date"
                                                        ControlToValidate="txtDtInicioFooter"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" ValidationGroup="grpFooter" />

                                                </FooterTemplate>

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Data Fim Vigência:">

                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDtFim" Text='<%# Eval("DAT_FIMVIG", "{0:d}") %>'></asp:Label><br />
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDtFim" CssClass="date" MaxLength="10" Width="120px" Text='<%# Eval("DAT_FIMVIG", "{0:d}") %>'></asp:TextBox>
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="RangTxtDtFim"
                                                        Type="Date"
                                                        ControlToValidate="txtDtFim"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" ValidationGroup="grpEmb" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" ID="txtDtFimFooter" CssClass="date" MaxLength="10" Width="120px"></asp:TextBox>
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="RangTxtDtFim"
                                                        Type="Date"
                                                        ControlToValidate="txtDtFimFooter"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" ValidationGroup="grpFooter" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>

                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" CssClass="button" CausesValidation="false" />
                                                    <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CommandName="DeleteAb" CssClass="button" CausesValidation="false" />
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="UpdateAb" CssClass="button" ValidationGroup="grpEmb" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" OnClientClick="return confirm('Tem certeza que deseja abandonar a operação?');" CausesValidation="false" />

                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="btnSalvarNovo" runat="server" Text="Salvar" CommandName="UpdateNovo" CssClass="button" ValidationGroup="grpFooter" />
                                                    <asp:Button ID="btnCancelarNovo" runat="server" Text="Cancelar" CommandName="CancelNovo" CssClass="button" OnClientClick="return confirm('Tem certeza que deseja abandonar a operação?');" CausesValidation="false" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>



                                </div>


                            </ContentTemplate>
                        </ajax:TabPanel>

                    </ajax:TabContainer>

                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$tabSimpro$btnProcessar" />
                </Triggers>

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
</asp:Content>
