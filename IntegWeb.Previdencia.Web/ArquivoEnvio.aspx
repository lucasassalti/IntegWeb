<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ArquivoEnvio.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ArquivoEnvio" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Controle de Envio de Arquivos</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server">
                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbGeracao" HeaderText="Geração/Acerto de Arquivo de Repasse" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div id="divPesquisa" runat="server" class="tabelaPagina">
                                    <asp:Panel ID="pnlPesquisa" runat="server">
                                        <table>
                                            <tr>
                                                <td>Mês/Ano:</td>
                                                <td>
                                                    <asp:TextBox ID="txtMesGerar" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangMesGerar"
                                                        Type="Integer"
                                                        ControlToValidate="txtMesGerar"
                                                        MaximumValue="12"
                                                        MinimumValue="01"
                                                        ErrorMessage="Mês inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                    <b>/</b>
                                                    <asp:TextBox ID="txtAnoGerar" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangAnoGerar"
                                                        Type="Integer"
                                                        ControlToValidate="txtAnoGerar"
                                                        MaximumValue="2100"
                                                        MinimumValue="1900"
                                                        ErrorMessage="Ano inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                </td>

                                                <td>Grupo:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlGrupo" runat="server" Width="192px"></asp:DropDownList>
                                                </td>

                                                <td>Status:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="144px"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Dt. Referência</td>
                                                <td>De:
                                                    <asp:TextBox ID="txtdatIniRef" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="74px"></asp:TextBox>

                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangDatIniRef"
                                                        Type="Date"
                                                        ControlToValidate="txtdatIniRef"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />Até:
                                                    <asp:TextBox ID="txtdatFimRef" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="74px"></asp:TextBox>
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangeDatFimRef"
                                                        Type="Date"
                                                        ControlToValidate="txtdatFimRef"
                                                        MaximumValue="31/12/9999"
                                                        MinimumValue="31/12/1000"
                                                        ErrorMessage="Data Inválida"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                </td>
                                                <td>Referência:</td>
                                                <td>
                                                    <asp:TextBox ID="txtReferencia" runat="server" MaxLength="32" Width="188px" />
                                                </td>
                                                <td>Tipo de Envio:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoEnvio" runat="server" Width="144px"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>

                                        <table id="tableBotoesGeracao">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                                    <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                                    <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Enviar Novo Arquivo" OnClick="btnNovo_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblArea" runat="server" Text="Área:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlArea" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:ObjectDataSource runat="server" ID="odsEnvio"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraEnvioBLL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtMesGerar" Name="mes" PropertyName="Text" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtAnoGerar" Name="ano" PropertyName="Text" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtdatIniRef" Name="datIni" PropertyName="Text" Type="DateTime" />
                                            <asp:ControlParameter ControlID="txtdatFimRef" Name="datFim" PropertyName="Text" Type="DateTime" />
                                            <asp:ControlParameter ControlID="ddlGrupo" Name="grupo" PropertyName="SelectedValue" Type="Int16" />
                                            <asp:ControlParameter ControlID="ddlStatus" Name="status" PropertyName="SelectedValue" Type="Int32" />
                                            <asp:ControlParameter ControlID="ddlArea" Name="area" PropertyName="SelectedValue" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtReferencia" Name="referencia" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="ddlTipoEnvio" Name="tipoEnvio" PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdEnvio" runat="server"
                                        DataKeyNames="COD_ARQ_ENVIO,COD_ARQ_ENVIO_TIPO"
                                        DataSourceID="odsEnvio"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        AllowSorting="True"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        PageSize="8"
                                        OnRowCommand="grdEnvio_RowCommand">

                                        <Columns>
                                            <asp:BoundField HeaderText="Ano" DataField="ANO_REF" SortExpression="ANO_REF" />
                                            <asp:BoundField HeaderText="Mês" DataField="MES_REF" SortExpression="MES_REF" />
                                            <asp:BoundField HeaderText="Referência" DataField="DCR_ARQ_ENVIO" SortExpression="DCR_ARQ_ENVIO" />
                                            <asp:BoundField HeaderText="Tipo Envio" DataField="DCR_ARQ_ENVIO_TIPO" SortExpression="DCR_ARQ_ENVIO_TIPO" />
                                            <asp:BoundField HeaderText="Dt. Evento" DataField="DTH_INCLUSAO" SortExpression="DTH_INCLUSAO" DataFormatString="{0:dd/MM/yyyy}" />
                                            <%--<asp:BoundField HeaderText="Status" DataField="DCR_ARQ_STATUS" SortExpression="DCR_ARQ_STATUS" />--%>
                                            <asp:TemplateField HeaderText="Status" SortExpression="DCR_ARQ_STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDcrStatus" runat="server" Text='<%# Bind("DCR_ARQ_STATUS") %>' Font-Bold="true" ForeColor='<%# (Eval("COD_ARQ_STATUS").ToString().Equals("1")) ? System.Drawing.Color.Red : ((Eval("COD_ARQ_STATUS").ToString().Equals("3")) ? System.Drawing.Color.Green : System.Drawing.Color.Blue )  %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                            <asp:TemplateField HeaderText="Cod. Envio" SortExpression="COD_ARQ_ENVIO" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodEnvio" runat="server" Text='<%# Bind("COD_ARQ_ENVIO") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                            <asp:BoundField HeaderText="Cod. Tipo Envio" DataField="COD_ARQ_ENVIO_TIPO" SortExpression="COD_ARQ_ENVIO_TIPO" Visible="false"/>     
                                            <asp:TemplateField HeaderText="Ações">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVisualizarGrd" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# Eval("COD_ARQ_ENVIO") + "," + Eval("COD_ARQ_ENVIO_TIPO") + "," + Eval("COD_ARQ_STATUS") %>' CssClass="button" CausesValidation="false" />
                                                    <asp:Button ID="btnExcluirGrd" runat="server" Text="Excluir" CommandName="DeleteEnvio" CommandArgument='<%# Eval("COD_ARQ_ENVIO") %>' CssClass="button" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja excluir?');" Enabled = '<%# short.Parse(Eval("COD_ARQ_STATUS").ToString()) <= 2 ? true : false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div id="divDetalhesEnvio" runat="server" class="tabelaPagina" visible="False">
                                    <asp:Panel ID="pnlAcoes" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSalvarEnvio" runat="server" CssClass="button" Text="Enviar" OnClick="btnSalvarEnvio_Click" ValidationGroup="grpSalvarEnvio" />
                                                    <asp:Button ID="btnSalvarEnvioUpload" runat="server" CssClass="button" Text="Enviar" OnClick="btnSalvarEnvio_Click" ValidationGroup="grpSalvarEnvio" Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancelarEnvio" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarEnvio_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDetalhes" runat="server">
                                        <table>
                                            <tr>
                                                <td>Mês/Ano:</td>
                                                <td>
                                                    <asp:TextBox ID="txtMesGerarEnvio" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" AutoPostBack="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtMesGerarEnvio" ControlToValidate="txtMesGerarEnvio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarEnvio" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangMesGerarEnvio"
                                                        Type="Integer"
                                                        ControlToValidate="txtMesGerarEnvio"
                                                        MaximumValue="12"
                                                        MinimumValue="01"
                                                        ErrorMessage="Mês inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        ValidationGroup="grpSalvarEnvio" />
                                                    <b>/</b>
                                                    <asp:TextBox ID="txtAnoGerarEnvio" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" AutoPostBack="True"/>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtAnoGerarEnvio" ControlToValidate="txtAnoGerarEnvio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarEnvio" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangAnoGerarEnvio"
                                                        Type="Integer"
                                                        ControlToValidate="txtAnoGerarEnvio"
                                                        MaximumValue="2100"
                                                        MinimumValue="1900"
                                                        ErrorMessage="Ano inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        ValidationGroup="grpSalvarEnvio" />
                                                    <asp:HiddenField ID="chkedItemGrid" runat="server" />
                                                </td>
                                                <td>Status:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatusEnvio" runat="server" Width="147px" Enabled="false"></asp:DropDownList>
                                                </td>
                                            <tr>

                                            </tr>
                                            <tr>
                                                <td>Tipo de Envio:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoEnvioEnvio" runat="server" Width="147px" OnSelectedIndexChanged="ddlTipoEnvioEnvio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDdlTipoEnvio" ControlToValidate="ddlTipoEnvioEnvio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarEnvio" Width="50px" />
                                                </td>
                                                <td>Grupo:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlGrupoEnvio" runat="server" Width="147px" OnSelectedIndexChanged="ddlGrupoEnvio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDdlGrupoEnvio" ControlToValidate="ddlGrupoEnvio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarEnvio" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Título do envio:</td>
                                                <td>
                                                    <asp:TextBox ID="txtReferenciaEnvio" runat="server" MaxLength="20" Width="195px" />
                                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtReferenciaEnvio" ControlToValidate="txtReferenciaEnvio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarEnvio" />
                                                </td>
                                                <td>Empresa:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmpresaEnvio" runat="server" Width="200px"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="lblArea2" runat="server" Text="Área:"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAreaEnvio" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlAreaEnvio_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqDdlAreaEnvio" ControlToValidate="ddlAreaEnvio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarEnvio" />
                                                </td>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hidCodEnvio" Value=""/>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlRelatorios" runat="server">
                                        <asp:CheckBoxList ID="chklstRelCapJoia" runat="server" CssClass="Table" AutoPostBack="True" Visible="False" ValidationGroup="grpSalvarEnvio">
                                            <asp:ListItem Value="1">Relatório de Detalhe Joia</asp:ListItem>
                                            <asp:ListItem Value="2">Relatório de Resumo Joia</asp:ListItem>
<%--                                            <asp:ListItem Value="3">Relatório de Cancelamento Joia</asp:ListItem>
                                            <asp:ListItem Value="4">Relatório de Conferência Joia</asp:ListItem>--%>
                                        </asp:CheckBoxList>

                                        <asp:CheckBoxList ID="chklstRelCapAutoPatr" runat="server" CssClass="Table" AutoPostBack="True" Visible="False" ValidationGroup="grpSalvarEnvio">
                                            <asp:ListItem Value="3">Relatório Contribuições de Autopatrocínio</asp:ListItem>
  <%--                                          <asp:ListItem Value="2">Relatório de Conferência Autopatrocínio</asp:ListItem>
                                            <asp:ListItem Value="3">Relatório de Detalhe Autopatrocínio</asp:ListItem>
                                            <asp:ListItem Value="4">Relatório de Resumo Autopatrocínio</asp:ListItem>--%>
                                        </asp:CheckBoxList>

                                        <asp:CheckBoxList ID="chklstRelCapVol" runat="server" CssClass="Table" AutoPostBack="True" Visible="False" ValidationGroup="grpSalvarEnvio">
                                            <asp:ListItem Value="1">Relatório de Detalhe Voluntária</asp:ListItem>
                                            <asp:ListItem Value="2">Relatório de Resumo Voluntária</asp:ListItem>
<%--                                            <asp:ListItem Value="3">Relatório de Cancelamento Voluntária</asp:ListItem>
                                            <asp:ListItem Value="4">Relatório de Conferência Voluntária</asp:ListItem>--%>
                                        </asp:CheckBoxList>

                                        <asp:CheckBoxList ID="chklstRelEmprest" runat="server" CssClass="Table" AutoPostBack="True" Visible="False" ValidationGroup="grpSalvarEnvio">
                                            <asp:ListItem Value="1">Relatório Detalhe do Movimento</asp:ListItem>
                                            <asp:ListItem Value="2">Relatório Resumo de Cobrança Mensal</asp:ListItem>
                                            <asp:ListItem Value="4">Relatório Previdenciário</asp:ListItem>                                            
                                            <%--<asp:ListItem Value="4">Relatório de Resumo Empréstimo</asp:ListItem>--%>
                                        </asp:CheckBoxList>

                                        <asp:CheckBoxList ID="chklstRelSaude" runat="server" CssClass="Table" AutoPostBack="True" Visible="False" ValidationGroup="grpSalvarEnvio">                                            
                                            <asp:ListItem Value="1">Relatório Detalhe do Movimento</asp:ListItem>
                                            <asp:ListItem Value="2">Relatório Resumo de Cobrança Mensal</asp:ListItem>
                                            <asp:ListItem Value="5">Relatório Detalhe das Verbas Geradas</asp:ListItem>
                                            <asp:ListItem Value="6">Resumo Inadimplentes/Adimplentes</asp:ListItem>
                                            <asp:ListItem Value="7">Utilização Indevida Adimplentes</asp:ListItem>
                                            <asp:ListItem Value="8">Utilização Indevida Inadimplentes</asp:ListItem>
                                        </asp:CheckBoxList>

                                        <asp:CheckBoxList ID="chklstRelSeguro" runat="server" CssClass="Table" AutoPostBack="True" Visible="False" ValidationGroup="grpSalvarEnvio">
                                            <asp:ListItem Value="1">Relatório de Detalhe Seguros</asp:ListItem>
                                            <asp:ListItem Value="2">Relatório de Resumo Seguros</asp:ListItem>
                                            <asp:ListItem Value="9">Relatório de Cancelamento Seguros</asp:ListItem>
                                            <asp:ListItem Value="10">Relatório de Conferência Seguros</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlUpload" runat="server" Enabled="False" Visible="False">
                                        <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" Width="300px" />
                                    </asp:Panel>

                                    <asp:Panel ID="pnlRepasse" runat="server" Enabled="False" Visible="False">
                                        <asp:ObjectDataSource runat="server" ID="odsRepasse"
                                            TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraEnvioBLL"
                                            SelectMethod="GetDataRepasse"
                                            SelectCountMethod="GetDataCountRepasse"
                                            EnablePaging="True"
                                            SortParameterName="sortParameter">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlAreaEnvio" Name="area" PropertyName="SelectedValue" Type="Int16" />
                                                <asp:ControlParameter ControlID="ddlGrupoEnvio" Name="grupo" PropertyName="SelectedValue" Type="Int16" />
                                                <asp:ControlParameter ControlID="txtMesGerarEnvio" Name="mesRef" PropertyName="Text" Type="Int32" />
                                                <asp:ControlParameter ControlID="txtAnoGerarEnvio" Name="anoRef" PropertyName="Text" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>

                                        <asp:GridView ID="grdRepasse" runat="server"
                                            DataKeyNames="COD_ARQ_ENV_REPASSE"
                                            DataSourceID="odsRepasse"
                                            AutoGenerateColumns="False"
                                            EmptyDataText="Não retornou registros"
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            CssClass="Table"
                                            ClientIDMode="Static"
                                            PageSize="8">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Ações">
                                                    <ItemTemplate>                                                        
                                                        <asp:CheckBox ID="chckRepasse" runat="server" OnCheckedChanged="chckRepasse_CheckedChanged" AutoPostBack="true" GroupName="rdList" ValidationGroup="grpSalvarEnvio" Checked='<%# Eval("COD_ARQ_ENV_REPASSE").ToString().Equals(chkedItemGrid.Value.ToString()) ? true : false %>' />
                                                        <asp:Label ID="Label1" runat="server" Text=">>" Visible='<%# Eval("COD_ARQ_ENV_REPASSE").ToString().Equals(chkedItemGrid.Value.ToString()) ? true : false %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. Repasse" SortExpression="COD_ARQ_ENVIO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescricaoArquivo" runat="server" Text='<%# Bind("DCR_ARQ_ENV_REPASSE") %>' />                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Mês" DataField="MES_REF" SortExpression="MES_REF" />
                                                <asp:BoundField HeaderText="Ano" DataField="ANO_REF" SortExpression="ANO_REF" />
                                                <asp:BoundField HeaderText="Grupo Empresa" DataField="DCR_GRUPO_EMPRS" SortExpression="DCR_GRUPO_EMPRS" />
                                                <asp:TemplateField HeaderText="Cod. Envio" SortExpression="COD_ARQ_ENVIO" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodEnvio" runat="server" Text='<%# Bind("COD_ARQ_ENVIO") %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$TbGeracao$btnSalvarEnvioUpload" />
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
