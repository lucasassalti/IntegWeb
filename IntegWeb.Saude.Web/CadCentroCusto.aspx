<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadCentroCusto.aspx.cs" Inherits="IntegWeb.Saude.Web.CadCentroCusto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function ScrollRight() {
            window.scrollTo(200, 0);
        }

        function ScrollLeft() {
            window.scrollTo(0, 0);
        }
    </script>
    <asp:UpdatePanel runat="server" ID="upOrgao">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsOrgao" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Centro de Custo / Orgão</h1>
                    <table>
                        <tr>
                            <td>Número do Orgão:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumOrgao" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Código do Plano:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodPlano" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Consultar" />
                                <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Limpar" />
                            </td>
                        </tr>
                    </table>

                    <asp:ObjectDataSource runat="server" ID="odsCCusto"
                        TypeName="IntegWeb.Saude.Aplicacao.BLL.CentroCustoBLL"
                        SelectMethod="GetCCustos"
                        SelectCountMethod="SelectCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter"
                        UpdateMethod="UpdateData">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtNumOrgao" Name="paramNumOrgao" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtCodPlano" Name="paramCodPlano" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdCCusto" runat="server" 
                        DataKeyNames="NUM_ORGAO, COD_PLANO" 
                        OnRowCancelingEdit="grdCCusto_RowCancelingEdit" 
                        OnRowEditing="grdCCusto_RowEditing" 
                        OnRowCreated="GridView_RowCreated"
                        AllowPaging="true" 
                        AllowSorting="true"
                        AutoGenerateColumns="False" 
                        EmptyDataText="A consulta não retornou registros" 
                        CssClass="Table" 
                        ClientIDMode="Static" 
                        PageSize="8" 
                        DataSourceID="odsCCusto">
                        <PagerSettings
                            Visible="true"
                            PreviousPageText="Anterior"
                            NextPageText="Próxima"
                            Mode="NumericFirstLast" />
                        <Columns>

                            <asp:BoundField DataField="DS_CCUSTO" HeaderText="Centro de Custo" SortExpression="DS_CCUSTO" ReadOnly="true"/>
                            <asp:BoundField DataField="SG_CCUSTO" HeaderText="Sigla" SortExpression="SG_CCUSTO"  ReadOnly="true"/>
                            <asp:BoundField DataField="NUM_ORGAO" HeaderText="Núm. Orgão" SortExpression="NUM_ORGAO"  ReadOnly="true"/>
                            <asp:BoundField DataField="COD_PLANO" HeaderText="Cód. Plano" SortExpression="COD_PLANO"  ReadOnly="true"/>

                            <asp:TemplateField HeaderText="Deb. Util" SortExpression="CCUSTO_DEB_UTIL" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCustoDebUtil" runat="server" Text='<%# Bind("CCUSTO_DEB_UTIL") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator18" ControlToValidate="txtCustoDebUtil" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustoDebUtil" runat="server" Text='<%# Bind("CCUSTO_DEB_UTIL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cred. Util" SortExpression="CCUSTO_CRE_UTIL" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCustoCredUtil" runat="server" Text='<%# Bind("CCUSTO_CRE_UTIL") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator19" ControlToValidate="txtCustoCredUtil" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustoCredtil" runat="server" Text='<%# Bind("CCUSTO_CRE_UTIL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Deb. Glosa" SortExpression="CCUSTO_DEB_GLOSA" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCustoDebGlosa" runat="server" Text='<%# Bind("CCUSTO_DEB_GLOSA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator26" ControlToValidate="txtCustoCreGlosa" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustoCreGlosa" runat="server" Text='<%# Bind("CCUSTO_DEB_GLOSA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cred. Glosa" SortExpression="CCUSTO_CRE_GLOSA" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCustoCreGlosa" runat="server" Text='<%# Bind("CCUSTO_CRE_GLOSA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator20" ControlToValidate="txtCustoDebGlosa" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustoDebGlosa" runat="server" Text='<%# Bind("CCUSTO_CRE_GLOSA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Aux. Deb. Util" SortExpression="AUX_DEB_UTIL" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAuxDebUtil" runat="server" Text='<%# Bind("AUX_DEB_UTIL") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator22" ControlToValidate="txtAuxDebUtil" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAuxDebUtil" runat="server" Text='<%# Bind("AUX_DEB_UTIL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Aux. Cred. Util" SortExpression="AUX_CRE_UTIL" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAuxCredUtil" runat="server" Text='<%# Bind("AUX_CRE_UTIL") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator23" ControlToValidate="txtAuxCredUtil" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAuxCredUtil" runat="server" Text='<%# Bind("AUX_CRE_UTIL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Aux. Deb. Glosa" SortExpression="AUX_DEB_GLOSA" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAuxDebGlosa" runat="server" Text='<%# Bind("AUX_DEB_GLOSA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator24" ControlToValidate="txtAuxDebGlosa" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAuxDebGlosa" runat="server" Text='<%# Bind("AUX_DEB_GLOSA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Aux. Cred. Glosa" SortExpression="AUX_CRE_GLOSA" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAuxCredGlosa" runat="server" Text='<%# Bind("AUX_CRE_GLOSA") %>' Width="90px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator25" ControlToValidate="txtAuxCredGlosa" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAuxCredGlosa" runat="server" Text='<%# Bind("AUX_CRE_GLOSA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                       <asp:Button ID="btCancelar" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="ScrollLeft();" />
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
