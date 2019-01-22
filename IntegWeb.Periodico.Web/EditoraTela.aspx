<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="EditoraTela.aspx.cs" Inherits="IntegWeb.Periodico.Web.EditoraTela" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="full_w">
        <div class="h_title">
        </div>
        <h1>Editora</h1>
        <asp:UpdatePanel runat="server" ID="upEditora">
            <ContentTemplate>
                <asp:ValidationSummary ID="vsAssinatura" runat="server" ForeColor="Red" ShowMessageBox="true"
                            ShowSummary="false" />
                <div id="divSelect" class="tabelaPagina" runat="server">

                    <div style="text-align: left">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="drpEditora" runat="server">
                                        <asp:ListItem Text="Selecione" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="CPF/CNPJ" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Nome" Value="2"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtEditora" runat="server" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="False" OnClick="btnPesquisar_Click" />


                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />

                                    <asp:LinkButton ID="lnkInserirGrupo" runat="server" CausesValidation="False" OnClick="lnkInserirGrupo_Click" CssClass="button">Inserir Editora</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <asp:GridView AutoGenerateColumns="False" EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="gridEditora" OnRowCommand="gridEditora_RowCommand" runat="server" OnPageIndexChanging="gridEditora_PageIndexChanging" PageSize="10" AllowPaging="true">
                        <Columns>
                            <asp:BoundField HeaderText="Nome" DataField="NOME" />
                            <asp:BoundField HeaderText="Cpf/Cnpj" DataField="CGC_CPF" />
                            <asp:BoundField HeaderText="Fone" DataField="FONE" />
         
                            <asp:BoundField HeaderText="Contato" DataField="CONTATO" />
                            <asp:HyperLinkField
                                    DataNavigateUrlFields="SITE"
                                    DataTextField="SITE"
                                    HeaderText="Site"
                             />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CssClass="button" CommandArgument='<%#Eval("id_editora")%>' CausesValidation="false" runat="server" CommandName="Atualizar" ButtonType="Link" Text="Atualizar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button4" CssClass="button" CommandArgument='<%#Eval("id_editora")%>' CausesValidation="false" runat="server" CommandName="Deletar" ButtonType="Link" Text="Deletar" OnClientClick="if ( !confirm('Deseja realmente deletar?')) return false;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="divAction" runat="server" class="MarginGrid">
                    <div class="formTable">
                     <table>
                           <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Nome</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtNome" runat="server" Width="599px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>CPF/CNPJ</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCpfCnpj" runat="server" Width="190px" ></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>CEP</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCep" runat="server" Width="190px" onkeypress="mascara(this, cep)" MaxLength="9"></asp:TextBox>

                                            </td>
                                            <td>
                                                <div style="float: right; margin-right: 2px">
                                                    <asp:Button ID="btncEP" runat="server" Text="Buscar Cep" CssClass="button"  OnClick="btncEP_Click"/>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Rua</td>
                                            <td>Número</td>
                                             <td>Complemento</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtRua" runat="server" Width="332px"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtNumero" runat="server" Width="60px" onkeypress="mascara(this, soNumeros)"></asp:TextBox></td>
                                            <td> <asp:TextBox ID="txtComplemento" runat="server" Width="190px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                           <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Bairro</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtBairro" runat="server" Width="190px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                <table>
                                        <tr>
                                            <td>Cidade</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtcidade" runat="server" Width="190px" onkeypress="mascara(this, soLetras)"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>UF</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtEstado" runat="server" Width="190px" MaxLength="2" onkeypress="mascara(this, soLetras)"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>      
                           <tr>
                                <td>
                                    <table>
                                        <tr>

                                            <td>Telefone</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFone" runat="server" Width="190px" ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                                <td>

                                    <table>
                                        <tr>

                                            <td>Fax</td>
                                        </tr>
                                        <tr>

                                            <td>
                                                <asp:TextBox ID="txtFax"   runat="server" Width="190px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </td>                
                                <td>
                                    <table>
                                        <tr>
                                            <td>Contato</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtContato" runat="server" Width="190px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                </tr>
                           <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>E-mail</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" Width="190px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>Site</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSite" runat="server" Width="402px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                               </tr>
                           <tr>
                                <td colspan="2">
                                    <div">

                                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="button"  OnClick="btnSalvar_Click"/>
                                        <asp:Button ID="btnVoltar"  CssClass="button" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                    </div>
                                </td>
                            </tr>
                     </table>
                     </div>
                       <div id="divPeriodico" visible="false" runat="server"  class="tabelaPagina">
                        <h1>Periódico</h1>
                        <table>
                            <tr>
                                <td>Nome do Periódico:</td>
                                <td>
                                    <asp:TextBox ID="txtPeriodico" runat="server" Width ="500"></asp:TextBox>
                                 </td>
                                </tr>
                             <tr>
                                <td>Código do Produto</td>
                                <td>
                                    <asp:TextBox ID="txtCod" runat="server" Width ="200"></asp:TextBox>
                                 </td>
                                </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnInserirPeriodico" runat="server"  OnClick="btnInserirPeriodico_Click" Text="Inserir Periódico" CssClass="button" />
                                </td>
                            </tr>
                        </table>
                        <h3>Periódicos Vinculados</h3>
                        <asp:GridView  DataKeyNames="id_periodico" OnRowCancelingEdit="grdPeriodico_RowCancelingEdit" OnRowDeleting="grdPeriodico_RowDeleting"  OnRowEditing="grdPeriodico_RowEditing" OnRowUpdating="grdPeriodico_RowUpdating"
                             AutoGenerateColumns="False" EmptyDataText="A Editora não possui Periódicos Vinculados" CssClass="Table" ClientIDMode="Static" ID="grdPeriodico"  runat="server" OnPageIndexChanging="grdPeriodico_PageIndexChanging" PageSize="10">
                                <Columns>
                                   <asp:TemplateField HeaderText="Periódico" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPer" runat="server" Text='<%# Bind("NOME_PERIODICO") %>' Width="230px"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator18" ControlToValidate="txtPer" runat="server" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                                    </EditItemTemplate> 
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodico" runat="server" Text='<%# Bind("NOME_PERIODICO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Código do Produto" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Bind("CODIGO_PERIODICO") %>' Width="170px"></asp:TextBox>
                                    </EditItemTemplate> 
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CODIGO_PERIODICO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                       <asp:Button ID="btnSalvar" CssClass="button" runat="server" Text="Salvar" CommandName="Update"/>&nbsp;&nbsp;
                                       <asp:Button ID="btnCancelar" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="return confirm('Tem certeza que deseja abandonar a atualização do cadastro?');"/>
                                   </EditItemTemplate> 
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar"  CssClass="button" runat="server" Text="Editar" CommandName="Edit"/>
                                       </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnExcluir"  CssClass="button" runat="server" Text="Excluir" CommandName="Delete" OnClientClick="return confirm('Tem certeza que deseja excluir?');"></asp:Button>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
               </div>
                 </div>
                
             
            </ContentTemplate>
        
        </asp:UpdatePanel>
     </div>
</asp:Content>
