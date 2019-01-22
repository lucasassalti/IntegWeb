<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="StatusExtratoUtilizacaoSaude.aspx.cs" Inherits="IntegWeb.Saude.Web.StatusExtratoUtilizacaoSaude" %>

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
    </script>
       
    <asp:UpdatePanel runat="server" ID="upUpdatePanel">
        <ContentTemplate>
            <div class="full_w">

                <div class="h_title">
                </div>

                <div class="tabelaPagina">

                    <h1>Status de Extratos de Utilização da Saúde</h1>

                    <table id="tblDados">
                        <tr>
                            <td>Nº da Empresa:<br />
                                <asp:TextBox ID="txtEmpresa" runat="Server" MaxLength="3" OnKeyPress="mascara(this,soNumeros)"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="reqtxtEmpresa" ControlToValidate="txtEmpresa" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                            </td>
                            <td>Nº da Matricula:<br />
                                <asp:TextBox ID="txtMatricula" runat="Server" MaxLength="10"  OnKeyPress="mascara(this,soNumeros)"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqtxtMatricula" ControlToValidate="txtMatricula" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                            </td>
                        </tr>
                     
                        <tr>
                            <td>Nº do Representante:<br />
                                <asp:TextBox ID="txtRepresentante" runat="Server" MaxLength="6"  OnKeyPress="mascara(this,soNumeros)"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqtxtRepresentante" ControlToValidate="txtRepresentante" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                            </td>
                             <td>Data de Movimentação:<br />
                                <asp:TextBox runat="server" ID="txtDataMovimentacao" CssClass="date" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqtxtDataMovimentacao" ControlToValidate="txtDataMovimentacao" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros" />
                                <asp:RangeValidator
                                    runat="server"
                                    ID="rangtxtDataMovimentacao"
                                    Type="Date"
                                    ControlToValidate="txtDataMovimentacao"
                                    MaximumValue="31/12/9999"
                                    MinimumValue="31/12/1000"
                                    ErrorMessage="Data Inválida"
                                    ForeColor="Red"
                                    Display="Dynamic" ValidationGroup="grpParametros" />
                            </td>
                        </tr>
         
                        <tr>
                            <td>
                                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" ValidationGroup="grpParametros" OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click"/>
                            </td>
                        </tr>
                    </table>
                    <asp:ObjectDataSource runat="server" ID="odsExtrato"
                          TypeName="IntegWeb.Saude.Aplicacao.DAL.Processos.StatusExtratoUtilizacaoSaudeDAL"
                          SelectMethod="GetExtrato">
                          <SelectParameters>
                               <asp:ControlParameter ControlID="txtEmpresa" Name="codEmpresa" PropertyName="Text" type="Int16" />
                               <asp:ControlParameter ControlID="txtMatricula" Name="matricula" PropertyName="Text" type="Int32"/>
                               <asp:ControlParameter ControlID="txtRepresentante" Name="numRepresen" PropertyName="Text" type="Int32"/>
                               <asp:ControlParameter ControlID="txtDataMovimentacao" Name="dataMotiv" PropertyName="Text" type="DateTime" />
                          </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="gridExtrato"
                        runat="server"
                        AutoGenerateColumns="false"
                        DataSourceID="odsExtrato"
                        EmptyDataText="Não retornou registros"
                        OnRowEditing="gridExtrato_RowEditing"
                        OnRowCommand="gridExtrato_RowCommand"
                        
                         >

                        <Columns>
                            <asp:BoundField DataField="NOM_RESP" HeaderText="Responsável" ReadOnly="true" />
                            <asp:BoundField DataField="COD_EMPRS" HeaderText="Nº Empresa" ReadOnly="true" />
                            <asp:BoundField DataField="NUM_RGTRO_EMPRG" HeaderText="Matricula" ReadOnly="true" />
                            <asp:BoundField DataField="DAT_REF" HeaderText="Data Ref." ReadOnly="true"/>
                            <asp:BoundField DataField="DAT_MOVIMENTO" HeaderText="Data Movimentação" dataformatstring="{0:d}" ReadOnly="true" />
                            <asp:BoundField DataField="NUM_IDNTF_RPTANT" HeaderText="Nº Representante" ReadOnly="true" />
                                
                            
                            <asp:TemplateField HeaderText="Inibir Extrato">
                                <ItemTemplate>
                                    <asp:Label ID="lblInibir" runat="server" Text='<%# Bind("IDC_INIBE_EXT") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlInibicao" SelectedValue='<%# Eval("IDC_INIBE_EXT") %>'>  
                                        <asp:ListItem Text="Não" Value="N" ></asp:ListItem>
                                        <asp:ListItem Text="Sim"  Value="S" ></asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator runat="server" ID="reqDdlInibicao" ControlToValidate="ddlInibicao" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditCont" />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server"  Text="Editar" CommandName="Edit" CssClass="button" CausesValidation="false"/>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="UpdateAb" CssClass="button" ValidationGroup="grpEditCont" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" OnClientClick="return confirm('Tem certeza que deseja abandonar a operação?');" CausesValidation="false" />
                                    <asp:Label ID="lblInibir2" runat="server" Text='<%# Bind("IDC_INIBE_EXT") %>' Visible="false" Enabled="false"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnPesquisar" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                <ProgressTemplate>
                    <div id="carregando">
                        <div class="carregandoTxt">
                            <img src="img/processando.gif" />
                            <br />
                            <h2>Carregando. Aguarde...</h2>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
</asp:Content>
