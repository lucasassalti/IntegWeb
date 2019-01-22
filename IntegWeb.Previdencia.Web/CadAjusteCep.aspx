<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadAjusteCep.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CadAjusteCep" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--o Codigo abaixo serve para sempre ativar o Jquery, pois quando a pagina dá post o jquery se perde.--%>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            $(function () {
                //$('input#txtFiltraGrid').quicksearch('table#grdDadosMunicipio tbody tr');
                $('input#txtFiltraGridCep').quicksearch('table#grdDadosCEP tbody tr');
            })
        }

        function EndRequestHandler(sender, args) {
            $(function () {
               // $('input#txtFiltraGrid').quicksearch('table#grdDadosMunicipio tbody tr');
                $('input#txtFiltraGridCep').quicksearch('table#grdDadosCEP tbody tr');
            })
        }
    </script>

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
        <h1>Manutenção de CEPs e Municípios</h1>
        <div class="MarginGrid"> 
            <asp:UpdatePanel runat="server" ID="UpdatePanel">
                <ContentTemplate>

                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="tbMunicipio" HeaderText="Municipio" runat="server" TabIndex="0" class="tabelaPagina">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>UF</td>                                            
                                        <td><asp:TextBox ID="txtPesqUF" runat="server" Width="80px"  MaxLength="2" /><td>
                                        </td>
                                        <td>Município</td>                                            
                                        <td><asp:TextBox ID="txtPesqCidade" runat="server" Width="230px" /><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cód. Município</td>                                            
                                        <td><asp:TextBox ID="txtPesqCodMunic" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="7" /><td>
                                        </td>
                                        <td>Cód. IBGE</td>                                            
                                        <td><asp:TextBox ID="txtPesqCodIBGE" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="7" /><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan ="5">
                                            <asp:Button ID="btnPesquisarCodMunicDescMunic" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisarCodMunicDescMunic_Click" />
                                            <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                            <asp:Button ID="btnInserirMunicipio" runat="server" CssClass="button" Text="Inserir Município" OnClick="btnInserirMunicipio_Click" Width="135px" />
                                        </td>
                                    </tr>

                                    <asp:Panel ID="pnlInserirMunic" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAddNewMunicipio" runat="server" CssClass="button" Text="Inserir" OnClick="btnAddNewMunicipio_Click" CausesValidation="true" ValidationGroup="grpInserirMunic" />
                                                    <asp:Button ID="btnCancelarMunicipio" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarMunicipio_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCodEstado" runat="server" Text="Código Estado" Width="120px"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblCodMunicipio" runat="server" Text="Código Municipio" Width="150px"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblDescrMunicipio" runat="server" Text="Descrição do Municipio" Width="170px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDescrResumidaMunicipio" runat="server" Text="Descrição Resumida do Municipio" Width="150px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlCodEstadoMunicInsert" runat="server"></asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator runat="server" ID="revCodEstadoMunicInsert" ControlToValidate="ddlCodEstadoMunicInsert"
                                                        ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirMunic" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCodMunicipioInserir" runat="server" Width="150px" onkeypress="mascara(this, soNumeros)" MaxLength="7" />
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ID="revTxtCodMunicipioInserir" ControlToValidate="txtCodMunicipioInserir" ValidationExpression="^[\s\S]{7,7}$"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Esse campo é obrigatorio e tem que conter 7 digitos numericos." ValidationGroup="grpInserirMunic" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescrMunicipio" runat="server" Width="150px" MaxLength="50" />
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ID="revtxtDescrMunicipio" ControlToValidate="txtDescrMunicipio"
                                                        ErrorMessage="Esse campo é obrigatorio e só pode conter até 50 Caracteres" ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,50}$"
                                                        ValidationGroup="grpInserirMunic" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescrResumidaMunicipio" runat="server" Width="150px" MaxLength="15" />
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ID="revtxtDescrResumidaMunicipio" ControlToValidate="txtDescrResumidaMunicipio"
                                                        ErrorMessage="Esse campo só é obrigatorio e pode conter até 15 Caracteres" ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,15}$"
                                                        ValidationGroup="grpInserirMunic" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <tr>

                                    <asp:ObjectDataSource runat="server" ID="odsMunicipio"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Cadastro.CadAjusteCepBLL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="true"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>                                                
                                            <asp:ControlParameter ControlID="txtPesqCidade"    Name="pCidade"   PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                            <asp:ControlParameter ControlID="txtPesqUF"        Name="pUF"       PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                            <asp:ControlParameter ControlID="txtPesqCodMunic"  Name="pCodMunic" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                            <asp:ControlParameter ControlID="txtPesqCodIBGE"   Name="pCodIBGE"  PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                        <asp:GridView ID="grdDadosMunicipio" runat="server"
                                            DataKeyNames="COD_ESTADO,COD_MUNICI,PAICOD"        
                                            OnRowEditing="grdDadosMunicipio_RowEditing" 
                                            OnRowCreated="GridView_RowCreated"     
                                            OnRowDeleted="GridView_RowDeleted"                   
                                            OnRowCancelingEdit="grdDadosMunicipio_RowCancelingEdit"
                                            OnRowCommand="grdDadosMunicipio_RowCommand"
                                            AllowPaging="true"
                                            AllowSorting="true"
                                            AutoGenerateColumns="False"
                                            EmptyDataText="A consulta não retornou registros"
                                            CssClass="Table"
                                            ClientIDMode="Static"
                                            PageSize="8"
                                            DataSourceID="odsMunicipio">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Estado" SortExpression="COD_ESTADO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoEstado" runat="server" Text='<%# Bind("COD_ESTADO") %>' />
                                                    </ItemTemplate>
                                                     <EditItemTemplate>
                                                        <asp:TextBox ID="txtCodEstado" runat="server" Text='<%# Bind("COD_ESTADO") %>' Width="100%" MaxLength="7" />
                                                        </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cod. Município" SortExpression="COD_MUNICI">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoMunicipio" runat="server" Text='<%# Bind("COD_MUNICI") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCodigoMunicipio" runat="server" Text='<%# Bind("COD_MUNICI") %>' Width="100%" MaxLength="7" onkeypress="mascara(this, soNumeros)" />
                                                        <br />
                                                        <asp:RegularExpressionValidator runat="server" ID="revtxtCodigoMunicipio" ControlToValidate="txtCodigoMunicipio" ValidationExpression="^[\s\S]{7,7}$"
                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Esse campo é obrigatorio e tem que conter 7 digitos numericos."
                                                            ValidationGroup="grpUpdateMunic" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Município" SortExpression="DCR_MUNICI">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDcrMunicipio" runat="server" Text='<%# Bind("DCR_MUNICI") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDcrMunicipio" runat="server" Text='<%# Bind("DCR_MUNICI") %>' Width="100%" MaxLength="50" onkeypress="mascara(this, soLetras)" />
                                                        <asp:RegularExpressionValidator runat="server" ID="revTxtDcrMunicipio" ControlToValidate="txtDcrMunicipio"
                                                            ErrorMessage="Esse Campo só pode conter até 50 Caracteres" ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,50}$"
                                                            ValidationGroup="grpUpdateMunic" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Município Resumido" SortExpression="DCR_RSUMD_MUNICI">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDcrResumida" runat="server" Text='<%# Bind("DCR_RSUMD_MUNICI") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDcrResumida" runat="server" Text='<%# Bind("DCR_RSUMD_MUNICI") %>' Width="100%" MaxLength="15" onkeypress="mascara(this, soLetras)" />
                                                        <asp:RegularExpressionValidator runat="server" ID="revTxtDcrResumida" ControlToValidate="txtDcrResumida"
                                                            ErrorMessage="Esse Campo só pode conter até 15 Caracteres" ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,15}$"
                                                            ValidationGroup="grpUpdateMunic" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CEP" SortExpression="COD_LOC_CEP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoCEP" runat="server" Text='<%# Bind("COD_LOC_CEP") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cód. IBGE" SortExpression="COD_MUNICI_IBGE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoIBGE" runat="server" Text='<%# Bind("COD_MUNICI_IBGE") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Código Pais" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoPais" runat="server" Text='<%# Bind("PAICOD") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEditarMunicipio" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                        <asp:Button ID="btnExcluir" runat="server" CssClass="button" Text="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("COD_ESTADO").ToString() + "," + Eval("COD_MUNICI").ToString() + "," + Eval("COD_MUNICI_IBGE").ToString() %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnSalvarMunicipio" runat="server" Text="Salvar" CommandName="Gravar" CssClass="button" CausesValidation="true" ValidationGroup="grpUpdateMunic" />
                                                        <asp:Button ID="btnCancelarMunicipio" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </tr>

                                </table>

                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="TbCep" HeaderText="Cep" runat="server" TabIndex="1" class="tabelaPagina">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Estado</td>                                            
                                        <td><asp:TextBox ID="txtCEP_PesqUF" runat="server" Width="80px" MaxLength="2" /><td>
                                        </td>
                                        <td>Município</td>                                            
                                        <td><asp:TextBox ID="txtCEP_PesqMunicipio" runat="server" Width="230px" /><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Bairro</td>                                            
                                        <td><asp:TextBox ID="txtCEP_PesqBairro" runat="server" Width="80px" /><td>
                                        </td>
                                        <td>Logradouro</td>                                            
                                        <td><asp:TextBox ID="txtCEP_PesqLogradouro" runat="server" Width="230px" /><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CEP</td>                                            
                                        <td><asp:TextBox ID="txtCEP_PesqCEP" runat="server" Width="80px" onkeypress="mascara(this, soNumeros)" MaxLength="8" /><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Button ID="btnPesquisarCepLogradouro" runat="server" CssClass="button" Text="Pesquisar Por" OnClick="btnPesquisarCepLogradouro_Click" />
                                            <asp:Button ID="btnLimparCep" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparCep_Click" />
                                            <asp:Button ID="btnInserirCEP" runat="server" CssClass="button" Text="Inserir CEP" OnClick="btnInserirCEP_Click" Width="135px" />
                                        </td>

                                    </tr>
                                    <asp:Panel ID="pnlInserirCEP" runat="server" Visible="false">
                                        <table>
                                 
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumeroCEP" runat="server" Text="CEP" Width="120px"></asp:Label></br>
                                                    <asp:TextBox ID="txtNumeroCEP" runat="server" Width="150px" MaxLength="8" onkeypress="mascara(this, soNumeros)" />
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ID="reqTxtNumeroCEP" ControlToValidate="txtNumeroCEP"
                                                        ErrorMessage="Esse campo é obrigatorio e só pode conter até 8 Caracteres"
                                                        ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,8}$" ValidationGroup="grpInserirCEP" />
                                                </td>
                                           </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCodEstadoCEP" runat="server" Text="Estado" Width="120px"></asp:Label></br>
                                                    <asp:DropDownList ID="ddlCodEstadoCEP" runat="server"></asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDdlCodEstadoCEP" ControlToValidate="ddlCodEstadoCEP"
                                                        ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpInserirCEP" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                     <asp:Label ID="lblCodMunicipioCEP" runat="server" Text="Código Municipio" Width="120px"></asp:Label></br>
                                                    <asp:TextBox ID="txtCodMunicipioCEP" runat="server" Width="150px" MaxLength="7" onkeypress="mascara(this, soNumeros)" />
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ID="reqtxtCodMunicipioCEP" ControlToValidate="txtCodMunicipioCEP"
                                                        ErrorMessage="Esse campo é obrigatorio e tem que conter 7 digitos numericos."
                                                        ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,60}$" ValidationGroup="grpInserirCEP" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescMuniciCEP" runat="server" Text="Nome Municipio" Width="120px"></asp:Label></br>
                                                    <asp:TextBox ID="txtDescMunicipioCEP" runat="server" Width="150px" MaxLength="50"/>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:Label ID="lblTipoLogradouroCEP" runat="server" Text="Tipo Logradouro" Width="120px"></asp:Label></br>
                                                    <asp:DropDownList ID="ddlTipoLogradouroInsertCEP" runat="server">
                                                        <asp:ListItem Selected="True" />
                                                        <asp:ListItem Text="AVE" Value="AVE" />
                                                        <asp:ListItem Text="ROD" Value="ROD" />
                                                        <asp:ListItem Text="TRA" Value="TRA" />
                                                        <asp:ListItem Text="VIL" Value="VIL" />
                                                        <asp:ListItem Text="RUA" Value="RUA" />
                                                        <asp:ListItem Text="ALA" Value="ALA" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescLogradouroCEP" runat="server" Text="Nome Logradouro" Width="120px"></asp:Label></br>
                                                    <asp:TextBox ID="txtDescLogradouroCEP" runat="server" Width="150px" MaxLength="50" />
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ID="reqtxtDescLogradouroCEP" ControlToValidate="txtDescLogradouroCEP"
                                                        ErrorMessage="Esse campo é obrigatorio e só pode conter até 60 Caracteres"
                                                        ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,60}$" ValidationGroup="grpInserirCEP" />
                                                </td>
                                           </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescBairroCEP" runat="server" Text="Bairro" Width="120px"></asp:Label></br>
                                                    <asp:TextBox ID="txtBairroCEP" runat="server" Width="150px" MaxLength="50" />
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ID="reqtxtBairroCEP" ControlToValidate="txtBairroCEP"
                                                        ErrorMessage="Esse campo é obrigatorio e só pode conter até 60 Caracteres"
                                                        ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,60}$" ValidationGroup="grpInserirCEP" />
                                                </td>
                                          
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:Button ID="btnAddNewCEP" runat="server" CssClass="button" Text="Inserir" OnClick="btnAddNewCEP_Click" CausesValidation="true" ValidationGroup="grpInserirCEP" />
                                                    <asp:Button ID="btnCancelarCEP" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarCEP_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <tr>

                                    <asp:ObjectDataSource runat="server" ID="odsCep"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Cadastro.CadAjusteCepBLL"
                                        SelectMethod="GetDataCep"
                                        SelectCountMethod="GetDataCepCount"
                                        EnablePaging="true"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>                
                                            <asp:ControlParameter ControlID="txtCEP_PesqUF"         Name="pUF"     PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                            <asp:ControlParameter ControlID="txtCEP_PesqMunicipio"  Name="pMunicipio"  PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                            <asp:ControlParameter ControlID="txtCEP_PesqBairro"     Name="pBairro" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                            <asp:ControlParameter ControlID="txtCEP_PesqLogradouro" Name="pLogradouro" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                            <asp:ControlParameter ControlID="txtCEP_PesqCEP"        Name="pCEP"    PropertyName="Text" Type="String" ConvertEmptyStringToNull="true"/>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                        <asp:GridView ID="grdDadosCEP" runat="server"
                                            DataKeyNames="NUM_CEP"
                                            OnRowEditing="grdDadosCEP_RowEditing" 
                                            OnRowCreated="GridView_RowCreated"     
                                            OnRowDeleted="GridView_RowDeleted"                   
                                            OnRowCancelingEdit="grdDadosCEP_RowCancelingEdit"
                                            OnRowCommand="grdDadosCEP_RowCommand"
                                            OnRowDataBound="grdDadosCEP_RowDataBound"
                                            AllowPaging="true"
                                            AllowSorting="true"
                                            AutoGenerateColumns="False"
                                            EmptyDataText="A consulta não retornou registros"
                                            CssClass="Table"
                                            ClientIDMode="Static"
                                            PageSize="8"
                                            DataSourceID="odsCep">
                                            <Columns>
                                                <asp:TemplateField HeaderText="CEP" SortExpression="NUM_CEP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumCEP" runat="server" Text='<%# Bind("NUM_CEP") %>' />
                                                         </ItemTemplate>
                                                         <EditItemTemplate>
                                                        <asp:TextBox ID="txtNumCEP" runat="server" Text='<%# Bind("NUM_CEP") %>' MaxLength="15" Width="100%"  onkeypress="mascara(this, soNumeros)" />
                                                    </EditItemTemplate>
                                                 
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Estado" SortExpression="COD_ESTADO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoEstado" runat="server" Text='<%# Bind("COD_ESTADO")  %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCodigoEstado" runat="server" Text='<%# Bind("COD_ESTADO") %>' MaxLength="3"  onkeypress="mascara(this, soLetras)" Width="100%" />
                                                    </EditItemTemplate>
                                                    <%--    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCodigoEstado" runat="server" Text='<%# Bind("COD_ESTADO") %>' onkeypress="mascara(this, soLetras)" MaxLength="2" Width="100%" />
                                                        <br />
                                                        <asp:RegularExpressionValidator runat="server" ID="revtxtCodigoEstado" ControlToValidate="txtCodigoEstado"
                                                            ErrorMessage="Esse Campo tem que conter 2 Caracteres" ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{2,2}$"
                                                            ValidationGroup="grpUpdateCEP" />
                                                    </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cód. Município" SortExpression="COD_MUNICI">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoMunicipio" runat="server" Text='<%# Bind("COD_MUNICI") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCodigoMunicipio" runat="server" Text='<%# Bind("COD_MUNICI") %>' MaxLength="15" onkeypress="mascara(this, soNumeros)" Width="100%" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tipo" SortExpression="TIP_LOGRADOURO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoLogradouro" runat="server" Text='<%# Bind("TIP_LOGRADOURO") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:HiddenField ID="hidTipoLog" runat="server" Value='<%# (Eval("TIP_LOGRADOURO")==null) ? "" : Eval("TIP_LOGRADOURO").ToString().Trim() %>' />
                                                        <asp:DropDownList ID="ddlTipoLogradouroCEP" runat="server" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Logradouro" SortExpression="NOM_LOGRADOURO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNomeLogradouro" runat="server" Text='<%# Bind("NOM_LOGRADOURO") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtNomeLogradouro" runat="server" Text='<%# Bind("NOM_LOGRADOURO") %>' MaxLength="60"  onkeypress="mascara(this, soLetras)" Width="100%" />
                                                        <br />
                                                        <asp:RegularExpressionValidator runat="server" ID="revtxtNomeLogradouro" ControlToValidate="txtNomeLogradouro"
                                                            ErrorMessage="Esse Campo só pode conter até 60 Caracteres" ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,60}$"
                                                            ValidationGroup="grpUpdateCEP" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bairro" SortExpression="DES_BAIRRO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDcrBairro" runat="server" Text='<%# Bind("DES_BAIRRO") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDcrBairro" runat="server" Text='<%# Bind("DES_BAIRRO") %>' MaxLength="60"  onkeypress="mascara(this, soLetras)" Width="100%" />
                                                        <br />
                                                        <asp:RegularExpressionValidator runat="server" ID="revtxtDcrBairro" ControlToValidate="txtDcrBairro"
                                                            ErrorMessage="Esse Campo só pode conter até 60 Caracteres" ForeColor="Red" Display="Dynamic" ValidationExpression="^[\s\S]{0,60}$"
                                                            ValidationGroup="grpUpdateCEP" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Município" SortExpression="DES_MUNICI">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDcrMunicipio" runat="server" Text='<%# Bind("DES_MUNICI") %>' />
                                                    </ItemTemplate>
                                                      <EditItemTemplate>
                                                        <asp:TextBox ID="txtDcrMunicipio" runat="server" Text='<%# Bind("DES_MUNICI") %>' MaxLength="60"  onkeypress="mascara(this, soLetras)" Width="100%" />
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cód. Pai" Visible="false" SortExpression="PAICOD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoPai" runat="server" Text='<%# Bind("PAICOD") %>' />
                                                    </ItemTemplate>
                                                     <EditItemTemplate>
                                                        <asp:TextBox ID="txtCodigoPai" runat="server" Text='<%# Bind("PAICOD") %>' MaxLength="60" Width="100%" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEditarCEP" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                        <asp:Button ID="btnExcluirCep" runat="server" CssClass="button" Text="Excluir" CommandName="Excluir" OnClick="btnExcluirCep_Click" OnClientClick="return confirm('Tem certeza que deseja excluir?');" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnSalvarCEP" runat="server" Text="Salvar" CommandName="Gravar" CssClass="button" CausesValidation="true" ValidationGroup="grpUpdateCEP" />
                                                        <asp:Button ID="btnCancelarCEP" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </tr>
                                </table>
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
    </div>
</asp:Content>
