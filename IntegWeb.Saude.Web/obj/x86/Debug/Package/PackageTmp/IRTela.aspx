<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="IRTela.aspx.cs" Inherits="IntegWeb.Saude.Web.IRTela" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script>
            function ScrollRight() {
                window.scrollTo(280, 0);
            }

            function ScrollLeft() {
                window.scrollTo(0, 0);
            }
    </script>
    <asp:UpdatePanel runat="server" ID="upIr">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsIR" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Centro de Custo / IR</h1>
                
                    <table>
                        <tr>
                            <td colspan="2">Preencha os campos abaixo para consultar
                            </td>
                        </tr>
                        <tr>
                            <td>Código da Empresa</td>
                            <td>
                                <asp:TextBox onkeypress="mascara(this, soNumeros)" ID="txtCodEmpresa" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Código do Plano</td>
                            <td>
                                <asp:TextBox onkeypress="mascara(this, soNumeros)" ID="txtCodPlano" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnConsultar" OnClientClick="ScrollRight();" OnClick="btnConsultar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Consultar" />
                            </td>
                        </tr>
                    </table>

                    <asp:GridView DataKeyNames="COD_EMPRS, COD_PLANO" OnRowCancelingEdit="grdIR_RowCancelingEdit" OnRowEditing="grdIR_RowEditing" OnRowUpdating="grdIR_RowUpdating" AllowPaging="true"
                        AutoGenerateColumns="False" EmptyDataText="A consulta não retornou registros" CssClass="Table" ClientIDMode="Static" ID="grdIR" runat="server" OnPageIndexChanging="grdIR_PageIndexChanging" PageSize="16">
                        <Columns>

                            <asp:TemplateField HeaderText="NOM_ABRVO_EMPRS" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNomAbrvoEmprs" runat="server" Text='<%# Bind("NOM_ABRVO_EMPRS") %>' Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator39" ControlToValidate="txtNomAbrvoEmprs" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomAbrvoEmprs" runat="server" Text='<%# Bind("NOM_ABRVO_EMPRS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="NOM_RZSOC_EMPRS" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNomRzsocEmprs" runat="server" Text='<%# Bind("NOM_RZSOC_EMPRS") %>' Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator21" ControlToValidate="txtNomRzsocEmprs" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomRzsocEmprs" runat="server" Text='<%# Bind("NOM_RZSOC_EMPRS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="TIPO_ITEM_ORC" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTipoItemOrc" runat="server" Text='<%# Bind("TIPO_ITEM_ORC") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator19" ControlToValidate="txtTipoItemOrc" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoItemOrc" runat="server" Text='<%# Bind("TIPO_ITEM_ORC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ITEM_ORCAMENTARIO" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTipoItemOrcam" runat="server" Text='<%# Bind("ITEM_ORCAMENTARIO") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator26" ControlToValidate="txtTipoItemOrcam" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoItemOrcam" runat="server" Text='<%# Bind("ITEM_ORCAMENTARIO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CONSOLIDA_PLANO" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtConsolidaPlano" runat="server" Text='<%# Bind("CONSOLIDA_PLANO") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator22" ControlToValidate="txtConsolidaPlano" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblConsolidaPlano" runat="server" Text='<%# Bind("CONSOLIDA_PLANO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="COD_EMPRS_CT" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCodEmprsCt" onkeypress="mascara(this, soNumeros)" runat="server" Text='<%# Bind("COD_EMPRS_CT") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator23" ControlToValidate="txtCodEmprsCt" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodEmprsCt" runat="server" Text='<%# Bind("COD_EMPRS_CT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="COD_PLANO_CT" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCodPlanoCt" onkeypress="mascara(this, soNumeros)" runat="server" Text='<%# Bind("COD_PLANO_CT") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator24" ControlToValidate="txtCodPlanoCt" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodPlanoCt" runat="server" Text='<%# Bind("COD_PLANO_CT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DESC_PLANO" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescPlano" runat="server" Text='<%# Bind("DESC_PLANO") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator25" ControlToValidate="txtDescPlano" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescPlano" runat="server" Text='<%# Bind("DESC_PLANO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DESC_NATUREZA" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescNatureza" runat="server" Text='<%# Bind("DESC_NATUREZA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator29" ControlToValidate="txtDescNatureza" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescNatureza" runat="server" Text='<%# Bind("DESC_NATUREZA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="COD_NATUREZA_CT" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCodNatureza" runat="server" Text='<%# Bind("COD_NATUREZA_CT") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator40" ControlToValidate="txtCodNatureza" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodNatureza" runat="server" Text='<%# Bind("COD_NATUREZA_CT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                       <asp:Button ID="btCancelar"  CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" OnClientClick="ScrollRight();" CausesValidation="false" CssClass="button" runat="server" Text="Editar" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
