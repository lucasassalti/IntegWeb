<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="IntegWeb.Administracao.Web.Relatorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function ScrollRight() {
            window.scrollTo(200, 0);
        }

        function ScrollLeft() {
            window.scrollTo(0, 0);
        }
    </script>    
     <asp:UpdatePanel runat="server" ID="upRelatorio">
        <ContentTemplate>
        <div class="full_w">
            <div class="tabelaPagina">
            <asp:Panel class="tabelaPagina" ID="pnlLista" runat="server">
                <h1>Manutenção de Relatórios</h1>
                <table>
                    <tr>
                        <td>Título do Relatório:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPesqTitulo" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Relatório(Nome):
                        </td>
                        <td>
                            <asp:TextBox ID="txtPesqRelatorio" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="button" runat="server" Text="Consultar" />
                            <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="button" runat="server" Text="Limpar" />
                            <asp:Button ID="btnNovo" OnClick="btnNovo_Click" CssClass="button" runat="server" Text="Novo" />
                        </td>
                        <td>
                            <asp:Label id="spanMensagem" runat="server" visible="false" ></asp:Label> 
                        </td>
                    </tr>
                </table>

                <asp:ObjectDataSource runat="server" ID="odsRelatorio"
                    TypeName="IntegWeb.Administracao.Aplicacao.BLL.RelatorioBLL"
                    SelectMethod="GetRelatorios"
                    SelectCountMethod="SelectCount"
                    EnablePaging="true"
                    SortParameterName="sortParameter"
                    DeleteMethod="DeleteData">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtPesqRelatorio" Name="pRELATORIO" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="txtPesqTitulo" Name="pTITULO" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:GridView ID="grdRelatorio" runat="server" 
                    DataKeyNames="ID_RELATORIO"                         
                    OnRowEditing="grdRelatorio_RowEditing"                         
                    OnRowCreated="GridView_RowCreated"     
                    OnRowDeleted="GridView_RowDeleted"                   
                    OnRowCancelingEdit="GridView_RowCancelingEdit"
                    AllowPaging="true" 
                    AllowSorting="true"
                    AutoGenerateColumns="False" 
                    EmptyDataText="A consulta não retornou registros" 
                    CssClass="Table" 
                    ClientIDMode="Static" 
                    PageSize="8" 
                    DataSourceID="odsRelatorio">
                    <PagerSettings
                        Visible="true"
                        PreviousPageText="Anterior"
                        NextPageText="Próxima"
                        Mode="NumericFirstLast" />
                    <Columns>

                        <asp:BoundField DataField="ID_RELATORIO" HeaderText="Código" SortExpression="ID_RELATORIO" ReadOnly="true"/>
                        <asp:BoundField DataField="RELATORIO" HeaderText="Chave" SortExpression="RELATORIO"  ReadOnly="true"/>
                        <asp:BoundField DataField="TITULO" HeaderText="Título" SortExpression="TITULO"  ReadOnly="true"/>
                        <asp:BoundField DataField="ID_TIPO_RELATORIO" HeaderText="Tipo" SortExpression="ID_TIPO_RELATORIO"  ReadOnly="true"/>
                        <asp:BoundField DataField="ARQUIVO" HeaderText="Origem" SortExpression="ARQUIVO"  ReadOnly="true"/>                            
                        <asp:BoundField DataField="RELATORIO_EXTENSAO" HeaderText="extensão" SortExpression="RELATORIO_EXTENSAO"  ReadOnly="true"/>

<%--                            <asp:TemplateField HeaderText="Aux. Cred. Glosa" SortExpression="AUX_CRE_GLOSA" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAuxCredGlosa" runat="server" Text='<%# Bind("AUX_CRE_GLOSA") %>' Width="90px"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator25" ControlToValidate="txtAuxCredGlosa" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAuxCredGlosa" runat="server" Text='<%# Bind("AUX_CRE_GLOSA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>


                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                <asp:Button ID="btCancelar" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="ScrollLeft();" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btEditar" OnClientClick="ScrollRight();" CausesValidation="false" CssClass="button" runat="server" Text="Editar" CommandName="Edit" />
                                <asp:Button ID="btExcluir" OnClientClick="return confirm('Tem certeza que deseja excluir este relatório?\n\nATENÇÃO: Esta ação também removerá todos os parâmetros deste relatório.');"  CausesValidation="false" CssClass="button" runat="server" Text="Excluir" CommandName="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </asp:Panel>

                <asp:Panel class="tabelaPagina" ID="pnlDetalhe" runat="server" Visible="false">

                <h1>Manutenção de Relatórios</h1>
                <table>
                    <tr>

                    </tr>
                    <tr>
                        <td>Código:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server" ReadOnly="true"></asp:TextBox></td>

                        <td>Tipo:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipo" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>

                        <td>Chave:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRelatorio" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>

                        <td>Título:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtTitulo" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Origem:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtOrigem" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Extensão:
                        </td>
                        <td>
                            <asp:TextBox ID="txtExtensao" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Salvar" />
                            <asp:Button ID="btnInserirParam" OnClick="btnInserirParam_Click"  CssClass="button" CausesValidation="false" runat="server" Text="Adicionar parâmetro" />                            
                            <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Voltar" />
                            <asp:Label id="spanMensagemDetalhe" runat="server" visible="false"  style="display: inline-table;"></asp:Label> 
                        </td>
                    </tr>
                </table>

                <asp:ObjectDataSource runat="server" ID="odsRelatorioParametro"
                    TypeName="IntegWeb.Administracao.Aplicacao.BLL.RelatorioBLL"
                    SelectMethod="GetParametros"
                    SelectCountMethod="SelectCountParam"
                    EnablePaging="true"
                    SortParameterName="sortParameter"
                    DeleteMethod="DeleteParam">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtCodigo" Name="pID_RELATORIO" PropertyName="Text" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>



                <asp:GridView ID="grdParametro" runat="server" 
                    DataKeyNames="ID_RELATORIO_PARAMETRO"                         
                    OnRowEditing="grdParametro_RowEditing"                         
                    OnRowCreated="GridView_RowCreated"
                    OnRowDeleted="GridView_RowDeleted"
                    OnRowCancelingEdit="GridView_RowCancelingEdit" 
                    AllowPaging="true" 
                    AllowSorting="true"
                    AutoGenerateColumns="False" 
                    EmptyDataText="A consulta não retornou registros" 
                    CssClass="Table" 
                    ClientIDMode="Static" 
                    PageSize="8" 
                    DataSourceID="odsRelatorioParametro">
                    <PagerSettings
                        Visible="true"
                        PreviousPageText="Anterior"
                        NextPageText="Próxima"
                        Mode="NumericFirstLast" />
                    <Columns>
                            
                        <asp:BoundField DataField="ORDEM" HeaderText="Ordem" SortExpression="ORDEM"  ReadOnly="true"/>
                        <asp:BoundField DataField="DESCRICAO" HeaderText="Descrição" SortExpression="DESCRICAO"  ReadOnly="true"/>
                        <asp:BoundField DataField="PARAMETRO" HeaderText="Parâmetro" SortExpression="PARAMETRO" ReadOnly="true"/>
                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO"  ReadOnly="true"/>
                        <asp:BoundField DataField="COMPONENTE_WEB" HeaderText="Componente Tela" SortExpression="COMPONENTE_WEB"  ReadOnly="true"/>
                        <asp:BoundField DataField="HABILITADO" HeaderText="Status" SortExpression="HABILITADO"  ReadOnly="true"/>                            
                        <asp:BoundField DataField="VISIVEL" HeaderText="Visível" SortExpression="VISIVEL"  ReadOnly="true"/>      
                        <asp:BoundField DataField="ID_RELATORIO_PARAMETRO" HeaderText="Código" SortExpression="ID_RELATORIO_PARAMETRO"  ReadOnly="true"/>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                <asp:Button ID="btCancelar" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="ScrollLeft();" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btEditar" OnClientClick="ScrollRight();" CausesValidation="false" CssClass="button" runat="server" Text="Editar" CommandName="Edit" />
                                <asp:Button ID="btExcluir" OnClientClick="return confirm('Tem certeza que deseja excluir este parâmetro?');"  CausesValidation="false" CssClass="button" runat="server" Text="Excluir" CommandName="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </asp:Panel>

                <asp:Panel class="tabelaPagina" ID="pnlParametro" runat="server" Visible="false">

                    <h1>Manutenção de Relatórios - Parâmetros</h1>
                    <table>
                        <tr>
                            <td>Ralatório:
                            </td>
                            <td>
                                <asp:TextBox ID="txtRelatorioParam" runat="server" ReadOnly="true"></asp:TextBox></td>

                            <td>Cód. Parâmetro:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigoParam" runat="server" ReadOnly="true" Width="70%"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>Ordem:
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrdem" runat="server" onkeypress="mascara(this, soNumeros)"></asp:TextBox></td>

                            <td>Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="70%">
                                    <asp:ListItem Text="Ativo" Value="S" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Desabilitado" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Parâmetro:
                            </td>
                            <td>
                                <asp:TextBox ID="txtParametro" runat="server"></asp:TextBox></td>

                            <td>Tipo:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoParam" runat="server"  Width="70%">
                                    <asp:ListItem Text="StringField" Value="StringField" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="NumberField" Value="NumberField"></asp:ListItem>
                                    <asp:ListItem Text="DateField" Value="DateField"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Descrição:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDescricao" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Componente:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlComponente" runat="server" Width="100%">
                                    <asp:ListItem Text="TextBox" Value="TextBox" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="DropDownList" Value="DropDownList"></asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td>Consulta<br />DropDownList:
                            </td>
                            <td>
                                <asp:TextBox ID="txtConsultaDropdownList" runat="server" TextMode="MultiLine" Width="220px" Height="100px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Valor inicial:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtValorInicial" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Visível:
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chkVisivel" runat="server"></asp:CheckBox></td></td>
                        </tr>
                        <tr>
                            <td>Permite Nulo:
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chkPermiteNulo" runat="server"></asp:CheckBox></td></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnParamVoltar" OnClick="btnParamVoltar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Voltar" />
                                <asp:Button ID="btnParamSalvar" OnClick="btnParamSalvar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Salvar" />
                                <asp:Label id="spanMensagemParametro" runat="server" visible="false" ></asp:Label> 
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>    
            </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>
