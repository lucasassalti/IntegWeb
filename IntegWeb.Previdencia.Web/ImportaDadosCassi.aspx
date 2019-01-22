<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ImportaDadosCassi.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ImportaDadosCassi" %>

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
        <h1>Dados Cassi </h1>
        <div class="tabelaPagina">
            <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                <ajax:TabPanel ID="tbImportacao" HeaderText="Importar Dados Cassi" runat="server" TabIndex="0">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPesquisar" runat="server">
                            <table>
                                <tr>
                                    <td>Entre com o arquivo de carga:</td>
                                    <td>
                                        <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLimpar" CausesValidation="false" CssClass="button" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="btnProcessar1" OnClick="btnProcessar_Click" OnClientClick="return postbackButtonClick();" CausesValidation="false" CssClass="button" runat="server" Text="Processar" />--%>
                                        <%--<asp:Button ID="btnProcessar" OnClick="btnProcessar_Click" OnClientClick="return postbackButtonClick();" CausesValidation="false" CssClass="button" runat="server" Text="Processar" style="display: none;" />--%>
                                        <asp:Button ID="btnProcessar" OnClick="btnProcessar_Click" OnClientClick="return postbackButtonClick();" CausesValidation="false" CssClass="button" runat="server" Text="Processar" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Processo_Mensagem" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </ajax:TabPanel>

                <ajax:TabPanel ID="tabConsulta" HeaderText="Consultar Dados Cassi" runat="server"  TabIndex="0">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Nome</td>
                                <td>
                                    <asp:TextBox ID="txtNome" Width="200px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Matricula</td>
                                <td>
                                    <asp:TextBox ID="txtMatricula" Width="130px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                    <asp:Button ID="btnLimparPesquisa" runat="server" CssClass="button" Text="Limpar Pesquisa" OnClick="btnLimparPesquisa_Click" />
                                </td>
                            </tr>
                        </table>

                        <asp:ObjectDataSource runat="server" ID="odsDadosCassi"
                            TypeName="IntegWeb.Previdencia.Aplicacao.BLL.DadosCartaoCassiBLL"
                            SelectMethod="GetData"
                            SelectCountMethod="GetDataCount"
                            EnablePaging="True"
                            SortParameterName="sortParameter">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtNome" Name="pNome" PropertyName="Text"
                                    Type="String" />
                                <asp:ControlParameter ControlID="txtMatricula" Name="pMatricula" PropertyName="Text"
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:GridView ID="grdDadosCassi" runat="server"
                            DataKeyNames="MatFuncional"
                            AllowPaging="True"
                            AllowSorting="True"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            ClientIDMode="Static"
                            PageSize="8"
                            DataSourceID="odsDadosCassi">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="NOME" SortExpression="NOME" />
                                <asp:BoundField HeaderText="Cpf" DataField="CPF" SortExpression="CPF" />
                                <asp:BoundField HeaderText="Matrícula Funcesp" DataField="MATFUNCIONAL" SortExpression="MATFUNCIONAL" />
                                <asp:BoundField HeaderText="Cartão Cassi" DataField="CARTAO" SortExpression="CARTAO" />
                                <asp:BoundField HeaderText="Data de Validade" DataField="DATAFINAL" SortExpression="DATAFINAL" />
                            </Columns>
                        </asp:GridView>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Processo_MensagemPesquisar" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajax:TabPanel>

                <ajax:TabPanel ID="tbControleCassi" HeaderText="Controle Cassi" runat="server" TabIndex="0">
                    <ContentTemplate>
                        <asp:Button ID="btnPesquisarControleCassi" runat="server" CssClass="button" Text="Consultar Controle" OnClick="btnPesquisarControleCassi_Click" />

                        <asp:Panel ID="pnlFiltro" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td>Empresa</td>
                                    <td>Matricula</td>
                                    <td>Sub</td>
                                    <td>Nome Participante</td>
                                    <td>Tipo de Movimentação</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFiltroEmpresa" Width="130px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtFiltroMatricula" Width="130px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtFiltroSub" Width="130px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtFiltroNomeParticpante" Width="180px" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlFitroTipoMotivo" runat="server">
                                            <asp:ListItem Text="Inclusão" Value="1" />
                                            <asp:ListItem Text="Segunda Via" Value="2" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtDtAlteracaoLote" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)" />
                                        <asp:Button ID="btnAlteracaoLote" runat="server" CssClass="button" Text="Alteração em Lote" OnClick="btnAlteracaoLote_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnFiltrar" runat="server" CssClass="button" Text="FILTRAR" OnClick="btnFiltrar_Click" />
                                    </td>
                                    <td colspan="3">
                                        <asp:Button ID="btnLimparFiltro" runat="server" CssClass="button" Text="LIMPAR" OnClick="btnLimparFiltro_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:GridView ID="grdControleCassi"
                            runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="Não retornou registros"
                            AllowPaging="True"
                            CssClass="Table"
                            OnPageIndexChanging="grdControleCassi_PageIndexChanging"
                            OnRowCommand="grdControleCassi_RowCommand"
                            OnRowEditing="grdControleCassi_RowEditing"
                            OnRowCancelingEdit="grdControleCassi_RowCancelingEdit"
                            OnRowUpdating="grdControleCassi_RowUpdating"
                            ClientIDMode="Static"
                            Font-Size="13px">
                            <Columns>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" Checked="true" runat="server" Text="" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" Checked="true" runat="server" Text="" class="span_checkbox" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ID_REG" Visible="false" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Empresa" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("EMPRESA") %>' />
                                    </ItemTemplate>
                                    <%-- <EditItemTemplate>
                                        <asp:TextBox ID="txtEmpresa" runat="server" Text='<%# Bind("EMPRESA") %>'
                                            Width="100%" MaxLength="2" onkeypress="mascara(this, soNumeros)" />
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Matricula" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatricula" runat="server" Text='<%# Bind("MATRICULA") %>' />
                                    </ItemTemplate>
                                    <%--  <EditItemTemplate>
                                        <asp:TextBox ID="txtMatricula" runat="server" Text='<%# Bind("MATRICULA") %>'
                                            Width="100%" MaxLength="10" onkeypress="mascara(this, soNumeros)" />
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sub Matricula" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubMatricula" runat="server" Text='<%# Bind("SUB_MATRICULA") %>' />
                                    </ItemTemplate>
                                    <%--<EditItemTemplate>
                                        <asp:TextBox ID="txtSubMatricula" runat="server" Text='<%# Bind("SUB_MATRICULA") %>'
                                            Width="100%" MaxLength="10" onkeypress="mascara(this, soNumeros)" />
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nome Participante" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOME" runat="server" Text='<%# Bind("NOM_PARTICIP") %>' />
                                    </ItemTemplate>
                                    <%--  <EditItemTemplate>
                                        <asp:TextBox ID="txtNOME" runat="server" Text='<%# Bind("NOM_PARTICIP") %>'
                                            Width="100%" MaxLength="250" onkeypress="mascara(this, soLetras)" />
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Carterinha" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCarterinha" runat="server" Text='<%# Bind("NUMEROCARTERINHA") %>' />
                                    </ItemTemplate>
                                    <%-- <EditItemTemplate>
                                        <asp:TextBox ID="txtCarterinha" runat="server" Text='<%# Bind("NUMEROCARTERINHA") %>'
                                            Width="100%" MaxLength="10" onkeypress="mascara(this, soNumeros)" />
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dt.Movimentação" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDtMovitacao" runat="server" Text='<%# Bind("dat_adesao","{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                    <%-- <EditItemTemplate>
                                        <asp:TextBox ID="txtDtMovitacao" runat="server" CssClass="date"
                                            Style="width: 100px;" Text='<%# Bind("dat_adesao","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tipo da Movimentação" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoMovitacao" runat="server" Text='<%# Bind("Tipomovimetacao") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dt.Envio" HeaderStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDtEnvio" runat="server" Text='<%# Bind("DT_ENVIO","{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDtEnvio" runat="server" CssClass="date"
                                            Style="width: 100px;" Text='<%# Bind("DT_ENVIO","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </ajax:TabPanel>

            </ajax:TabContainer>
        </div>

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
        <script type="text/javascript" src="js/build_uploadify.js"></script>
    </div>
</asp:Content>

