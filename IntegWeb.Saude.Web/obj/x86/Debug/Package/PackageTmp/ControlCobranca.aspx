<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ControlCobranca.aspx.cs" Inherits="IntegWeb.Saude.Web.ControlCobranca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">

            <h2>Controle / Item de Cobrança</h2>

            <asp:UpdatePanel runat="server" ID="upCobranca">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td colspan="2">Preencha os campos abaixo e clique em buscar:
                            </td>
                        </tr>
                        <tr>
                            <td>Código da empresa:</td>
                            <td>
                                <asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Código do plano</td>
                            <td>
                                <asp:TextBox ID="txtPlano" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="BtnBuscar" OnClick="BtnBuscar_Click" runat="server" CssClass="button" Text="Buscar" />
                            </td>
                        </tr>
                    </table>

                    <asp:GridView DataKeyNames="Cod_Emprs, Cod_Grupo,Cod_Tipo_Comp,Cod_Plano" OnRowCancelingEdit="grdCobranca_RowCancelingEdit" OnRowEditing="grdCobranca_RowEditing" OnRowUpdating="grdCobranca_RowUpdating" AllowPaging="true"
                        AutoGenerateColumns="False" EmptyDataText="A consulta não retornou registros" CssClass="Table" ClientIDMode="Static" ID="grdCobranca" runat="server" OnPageIndexChanging="grdCobranca_PageIndexChanging" PageSize="16">
                        <Columns>
                            <asp:BoundField HeaderText="COD_EMPRS_CT" ReadOnly="true" DataField="COD_EMPRS_CT" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="COD_PLANO_CT" ReadOnly="true" DataField="COD_PLANO_CT" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="COD_TIPO_COMP" ReadOnly="true" DataField="COD_TIPO_COMP" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="DESC_TIPO_COMP" ReadOnly="true" DataField="DESC_TIPO_COMP" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="GRUPO" ReadOnly="true" DataField="GRUPO" ItemStyle-HorizontalAlign="Center" />

                            <asp:TemplateField HeaderText="FCESP_NATUREZA" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFcespNat" runat="server" onkeypress="mascara(this, soNumeros)" Text='<%# Bind("FCESP_NATUREZA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator21" ControlToValidate="txtFcespNat" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFcespNat" runat="server" Text='<%# Bind("FCESP_NATUREZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SUPLEM_NATUREZA" ItemStyle-HorizontalAlign="Center">

                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSuplem" onkeypress="mascara(this, soNumeros)" runat="server" Text='<%# Bind("SUPLEM_NATUREZA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator18" ControlToValidate="txtSuplem" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSuplem" runat="server" Text='<%# Bind("SUPLEM_NATUREZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="PATROC_NATUREZA" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatroc" onkeypress="mascara(this, soNumeros)" runat="server" Text='<%# Bind("PATROC_NATUREZA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator19" ControlToValidate="txtPatroc" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPatroc" runat="server" Text='<%# Bind("PATROC_NATUREZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="COMPL_NATUREZA" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCompl" runat="server" onkeypress="mascara(this, soNumeros)" Text='<%# Bind("COMPL_NATUREZA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator20" ControlToValidate="txtCompl" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCompl" runat="server" Text='<%# Bind("COMPL_NATUREZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                       <asp:Button ID="btCancelar" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="return confirm('Tem certeza que deseja abandonar a atualização do cadastro?');" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" CausesValidation="false" CssClass="button" runat="server" Text="Editar" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </ContentTemplate>

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
