<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ArquivoRepasse.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ArquivoRepasse1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function postbackButtonClick() {
            if (Page_ClientValidate()) {
                updateProgress = $find("<%= UpdateProg1.ClientID %>");
                window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
                return true;
            }
        }

        function Carrega_Verba_Novo_Acerto() {
            if ($('#ContentPlaceHolder1_txtNovoAcertoVerba').val() != "") {
                $('#ContentPlaceHolder1_btnNovoAcertoCarregaVerba').click();
            }
        }

        function Carrega_Verba_Filtro() {
            if ($('#ContentPlaceHolder1_txtFiltroAcertoVerba').val() != "") {
                $('#ContentPlaceHolder1_btnFiltroCarregaVerba').click();
            }
        }

        function Carrega_Verba_Grid() {
            if ($('#txtVerbaGrd').val() != "") {
                $('#btnVerbaGrd').click();
            }
        }

        function ValidaCampoEmpresa(sender, args) {

            var txtBox = $('#<%=txtCodEmpresa.ClientID %>');
            var optTipo = $('#<%=rdTipoImport.ClientID %>_1');

            args.IsValid = false;

            if ((optTipo.is(':checked') && (txtBox.val() != '')) || !optTipo.is(':checked')) {
                args.IsValid = true;
            }

        }

        function btnReprocessar_click() {
            $('#ContentPlaceHolder1_btnReprocessar').click();
        }

    </script>

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Geração/Acertos Arquivos de Descontos - ATIVOS</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <div id="divPesquisa" runat="server" class="tabelaPagina">

                        <asp:Panel ID="pnlInicial" runat="server">
                            <table>
                                <tr>
                                    <td><asp:Label ID="lblArea" runat="server" Text="Área:"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlArea" runat="server" Width="190px"></asp:DropDownList>
                                    </td>
                                    <td><asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo processamento" OnClick="btnNovo_Click" /></td>
                                    <td><asp:Button ID="btnLiberar" runat="server" CssClass="button" Text="Liberar para o cadastro" OnClick="btnLiberar_Click" /></td>
                                    <td><asp:Button ID="btnExibirAnteriores" runat="server" CssClass="button" Text="Exibir anteriores" OnClick="btnExibirAnteriores_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMensagemInicial" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlNovoProcessamento" runat="server"  Visible="false">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnGerar" runat="server" CssClass="button" Text="Gerar" OnClick="btnGerar_Click" OnClientClick="return postbackButtonClick();"/>
                                        <asp:Button ID="btnLiberar2" runat="server" CssClass="button" Text="Liberar" OnClick="btnLiberar2_Click"/>
                                        <asp:Button ID="btnReprocessar" runat="server" CssClass="button" Text="Reprocessar" OnClick="btnReprocessar_Click" OnClientClick="return postbackButtonClick();" Visible="false"/>
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Area:</td>
                                    <td>
                                        <asp:Label ID="lblArea2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mês/Ano Ref.:</td>
                                    <td>
                                        <asp:TextBox ID="txtMes" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="RangeValidator1"
                                            Type="Integer"
                                            ControlToValidate="txtMes"
                                            MaximumValue="12"
                                            MinimumValue="01"
                                            ErrorMessage="Mês inválido"
                                            ForeColor="Red"
                                            Display="Dynamic" />
                                        <b>/</b>
                                        <asp:TextBox ID="txtAno" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="RangeValidator2"
                                            Type="Integer"
                                            ControlToValidate="txtAno"
                                            MaximumValue="2100"
                                            MinimumValue="1900"
                                            ErrorMessage="Ano inválido"
                                            ForeColor="Red"
                                            Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Grupo:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrupo" runat="server" Width="190px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodEmpresa" runat="server" Text="Empresa:" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodEmpresa" runat="server" Width="200px" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNovoProcessamento" runat="server" Text="Arquivo:" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fuNovoProcessamento" runat="server" CssClass="button" Visible="false" Width="300px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMensagemNovo" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlPesquisa" runat="server" Visible="false">

                            <table id="table1">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnFiltrar" runat="server" CssClass="button" Text="Filtrar" OnClick="btnFiltrar_Click" />
                                        <asp:Button ID="btnNovoEmBranco" runat="server" CssClass="button" Text="Novo Arquivo Desconto em Branco" OnClick="btnNovoEmBranco_Click" Visible="false"/>
                                        <asp:Button ID="btnVoltar" runat="server" CssClass="button" Text="Voltar" OnClick="btnVoltar_Click" />
                                        <%--<asp:Button ID="btnNovo2" runat="server" CssClass="button" Text="Novo processamento" OnClick="btnNovo_Click" />
                                        <asp:Button ID="btnLiberar2" runat="server" CssClass="button" Text="Liberar para o cadastro" OnClick="btnLiberar_Click" />                                        --%>
                                    </td>
                                </tr>
                            </table>

                            <asp:Panel ID="pnlFiltro" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>Mês/Ano:</td>
                                        <td>
                                            <asp:TextBox ID="txtMesFiltro" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangMesGerar"
                                                Type="Integer"
                                                ControlToValidate="txtMesFiltro"
                                                MaximumValue="12"
                                                MinimumValue="01"
                                                ErrorMessage="Mês inválido"
                                                ForeColor="Red"
                                                Display="Dynamic" />
                                            <b>/</b>
                                            <asp:TextBox ID="txtAnoFiltro" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangAnoGerar"
                                                Type="Integer"
                                                ControlToValidate="txtAnoFiltro"
                                                MaximumValue="2100"
                                                MinimumValue="1900"
                                                ErrorMessage="Ano inválido"
                                                ForeColor="Red"
                                                Display="Dynamic" />
                                        </td>

                                        <td>Grupo:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlGrupoFiltro" runat="server" Width="190px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Status:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatusFiltro" runat="server" Width="144px"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlAreaFiltro" runat="server" Width="190px" Visible="false"></asp:DropDownList>
                                            <asp:TextBox ID="txtRefFiltro" runat="server" MaxLength="32" Width="144px" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <%-- <td>Dt. Referência</td>
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
                                                    </td>--%>
                                    </tr>
                                </table>

                                <table id="tableBotoesGeracao">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                            <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>



                            <asp:ObjectDataSource runat="server" ID="odsRepasse"
                                TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Cadastro.ArqPatrocinadoraRepasseBLL"
                                SelectMethod="GetData"
                                SelectCountMethod="GetDataCount"
                                EnablePaging="True"
                                SortParameterName="sortParameter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtMesFiltro" Name="mes" PropertyName="Text" Type="Int16" />
                                    <asp:ControlParameter ControlID="txtAnoFiltro" Name="ano" PropertyName="Text" Type="Int16" />
                                    <asp:ControlParameter ControlID="ddlGrupoFiltro" Name="grupo" PropertyName="SelectedValue" Type="Int16" />
                                    <asp:ControlParameter ControlID="ddlStatusFiltro" Name="status" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="txtRefFiltro" Name="referencia" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="ddlAreaFiltro" Name="area" PropertyName="SelectedValue" Type="Int16" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:GridView ID="grdGeracao" runat="server"
                                DataKeyNames="COD_ARQ_ENV_REPASSE"
                                OnRowEditing="grdGeracao_RowEditing"
                                OnRowCommand="grdGeracao_RowCommand"
                                DataSourceID="odsRepasse"
                                AutoGenerateColumns="False"
                                EmptyDataText="Não retornou registros"
                                AllowPaging="True"
                                AllowSorting="True"
                                CssClass="Table"
                                ClientIDMode="Static"
                                PageSize="8">

                                <Columns>
                                    <asp:BoundField HeaderText="Ano" DataField="ANO_REF" SortExpression="ANO_REF" />
                                    <asp:BoundField HeaderText="Mês" DataField="MES_REF" SortExpression="MES_REF" />
                                    <asp:BoundField HeaderText="Título" DataField="DCR_ARQ_ENV_REPASSE" SortExpression="DCR_ARQ_ENV_REPASSE" />
                                    <asp:BoundField HeaderText="Dt. Criação" DataField="DTH_INCLUSAO" SortExpression="DTH_INCLUSAO" />
                                    <%--<asp:BoundField HeaderText="Status" DataField="DCR_ARQ_STATUS" SortExpression="DCR_ARQ_STATUS" />--%>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# short.Parse(Eval("COD_ARQ_STATUS").ToString())==2 ? "Liberado" : Eval("DCR_ARQ_STATUS").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEditarAB1" runat="server" Text='<%# bool.Parse(Eval("READ_ONLY").ToString()) ? "Visualizar" : "Editar" %>' CommandName="Edit" CssClass="button" Causes="false" Width="80px" />
                                            <%--<asp:Button ID="btnEnviar" runat="server" Text="Enviar" CommandName="Enviar" CssClass="button" Causes="false" Visible='<%# short.Parse(Eval("COD_ARQ_STATUS").ToString()).Equals(1) ? true : false%>' />
                                                    <asp:Button ID="btnVisualizarAB1" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# Eval("COD_ARQ_ENV_REPASSE") %>' CssClass="button" CausesValidation="false" Visible='<%# short.Parse(Eval("COD_ARQ_STATUS").ToString())>1 ? true : false %>' />--%>
                                            <asp:Button ID="btnExcluirAB1" runat="server" Text="Excluir" CommandName="DeleteRepasse" CommandArgument='<%# Eval("COD_ARQ_ENV_REPASSE") %>' CssClass="button" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja excluir?');" Enabled='<%# !bool.Parse(Eval("READ_ONLY").ToString()) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                    </div>

                    <div id="divDetalhes" runat="server" class="tabelaPagina" visible="False">
                        <asp:Panel ID="pnlBotao" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSalvarDetalhes" runat="server" CssClass="button" Text="Salvar" OnClick="btnSalvarDetalhes_Click" ValidationGroup="grpSalvarRepasse" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelarDetalhes" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarDetalhes_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlInserir" runat="server">
                            <table>
                                <tr>
                                    <td>Mês/Ano: (MM/AAAA)<br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMesDetalhes" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="64px" ValidationGroup="grpSalvarRepasse" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqMesDetalhesRepasse" ControlToValidate="txtMesDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarRepasse" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqMesDetalhesLinha" ControlToValidate="txtMesDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangeMesDetalhes"
                                            Type="Integer"
                                            ControlToValidate="txtMesDetalhes"
                                            MaximumValue="12"
                                            MinimumValue="01"
                                            ErrorMessage="Mês inválido"
                                            ForeColor="Red"
                                            Display="Dynamic" />
                                        <b>/</b>
                                        <asp:TextBox ID="txtAnoDetalhes" runat="server" MaxLength="4" Width="84px" onkeypress="mascara(this, soNumeros)" ValidationGroup="grpSalvarRepasse" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqAnoDetalhesRepasse" ControlToValidate="txtAnoDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarRepasse" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqAnoDetalhesLinha" ControlToValidate="txtAnoDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangAnoDetalhes"
                                            Type="Integer"
                                            ControlToValidate="txtAnoDetalhes"
                                            MaximumValue="2100"
                                            MinimumValue="1900"
                                            ErrorMessage="Ano inválido"
                                            ForeColor="Red"
                                            Display="Dynamic" />
                                    </td>
                                    <td>Status:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatusDetalhes" runat="server" Enabled="False" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                <td>Dt. Criação:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataCriacaoDetalhes" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" DataFormatString="{0:dd/MM/yyyy}" ValidationGroup="grpSalvarRepasse" Width="170px" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataCriacaoDetalhesRepasse" ControlToValidate="txtDataCriacaoDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarRepasse" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataCriacaoDetalhesLinha" ControlToValidate="txtDataCriacaoDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangDataCriacaoDetalhes"
                                            Type="Date"
                                            ControlToValidate="txtDataCriacaoDetalhes"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic" />
                                    </td>
                                    <td>Grupo:                                                   
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrupoDetalhes" runat="server" Width="174px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Título:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReferenciaDetalhes" runat="server" ValidationGroup="grpSalvarRepasse" MaxLength="32" Width="200px" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqReferenciaDetalhesRepasse" ControlToValidate="txtReferenciaDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarRepasse" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqReferenciaDetalhesLinha" ControlToValidate="txtReferenciaDetalhes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                        <asp:DropDownList ID="ddlAreaDetalhes" runat="server" Width="200px" Visible="false"></asp:DropDownList>
                                    </td>                                    
                                </tr>
                            </table>


                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnNovoDetalhes" runat="server" CssClass="button" Text="Novo Acerto" OnClick="btnNovoDetalhes_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnFiltrarDetalhes" runat="server" CssClass="button" Text="Filtrar" OnClick="btnFiltrarDetalhes_Click" />
                                    </td>
                                </tr>
                            </table>

                            <asp:Panel ID="pnlNovoAcerto" runat="server" Visible="False">
                                <table>
                                    <thead>
                                        <tr>
                                            <td style="font-weight: bolder;">Novo Acerto</td>
                                            <td colspan="5" style="text-align: center; height: 10px;">
                                                <asp:Label ID="lblMensagemNovoAcerto" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Empresa<br />
                                                <asp:TextBox ID="txtNovoAcertoEmpresa" runat="server" ValidationGroup="grpSalvarRepasse" onkeypress="mascara(this, soNumeros)" MaxLength="3" Width="96px"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqNovoAcertoEmpresa" ControlToValidate="txtNovoAcertoEmpresa" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                            </td>
                                            <td>Matrícula<br />
                                                <asp:TextBox ID="txtNovoAcertoMatricula" runat="server" ValidationGroup="grpSalvarRepasse" onkeypress="mascara(this, soNumeros)" MaxLength="10" Width="96px"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqNovoAcertoMatricula" ControlToValidate="txtNovoAcertoMatricula" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                            </td>
                                            <td>Verba<br />
                                                <asp:TextBox ID="txtNovoAcertoVerba" runat="server" onkeypress="mascara(this, soNumeros)" ValidationGroup="grpSalvarRepasse" MaxLength="5" Width="96px" onblur="Carrega_Verba_Novo_Acerto();"></asp:TextBox>
                                                <asp:HiddenField ID="hNovaVerbaPatrocina" runat="server" Value=""></asp:HiddenField>
                                                <asp:ImageButton ID="btnNovoAcertoCarregaVerba" runat="server" Text="Carregar" ImageUrl="~/img/i_search.png" Width="12px" Height="12px" AlternateText="Carregar Verba" ToolTip="Clique para carregar a verba" OnClick="btnNovoAcertoCarregaVerba_Click" CausesValidation="false" />&nbsp;                                                           
                                                            <asp:RequiredFieldValidator runat="server" ID="reqNovoAcertoVerba" ControlToValidate="txtNovoAcertoVerba" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                            </td>
                                            <td>Tipo<br />
                                                <asp:TextBox ID="txtNovoAcertoTipo" runat="server" ReadOnly="True" ValidationGroup="grpSalvarRepasse" Width="96px"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqNovoAcertoTipo" ControlToValidate="txtNovoAcertoTipo" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                            </td>
                                            <td>Descrição<br />
                                                <asp:TextBox ID="txtNovoAcertoDcrVerba" runat="server" ReadOnly="True" ValidationGroup="grpSalvarRepasse" Width="230px"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqNovoAcertoDcrVerba" ControlToValidate="txtNovoAcertoDcrVerba" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                            </td>
                                            <td>Percentual<br />
                                                <asp:TextBox ID="txtNovoAcertoPercentual" runat="server" ValidationGroup="grpSalvarRepasse" Width="96px" onkeypress="mascara(this, moeda)" MaxLength="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqNovoAcertoPercentual" ControlToValidate="txtNovoAcertoPercentual" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                            </td>
                                            <td>Valor<br />
                                                <asp:TextBox ID="txtNovoAcertoValor" runat="server" ValidationGroup="grpSalvarRepasse" Width="96px" onkeypress="mascara(this, moeda)" MaxLength="12"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqNovoAcertoValor" ControlToValidate="txtNovoAcertoValor" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpSalvarLinha" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInserirNovoAcerto" runat="server" Text="Inserir" CssClass="button" OnClick="btnInserirNovoAcerto_Click" ValidationGroup="grpSalvarLinha" Width="90px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancelarNovoAcerto" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarNovoAcerto_Click" Width="90px" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>


                            <asp:Panel ID="pnlFiltrarAcerto" runat="server" Visible="False" class="tabelaPagina">
                                <table>
                                    <thead>
                                        <tr>
                                            <td style="font-weight: bolder">Filtrar</td>
                                            <td colspan="5" style="text-align: center; height: 10px;">
                                                <asp:Label ID="lblVerbaFiltro_Mensagem" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>
                                            </td>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Empresa<br />
                                                <asp:TextBox ID="txtFiltroAcertoEmpresa" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="3" Width="96px"></asp:TextBox>
                                            </td>
                                            <td>Matrícula<br />
                                                <asp:TextBox ID="txtFiltroAcertoMatricula" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="10" Width="96px"></asp:TextBox>
                                            </td>
                                            <td>Verba<br />
                                                <asp:TextBox ID="txtFiltroAcertoVerba" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="5" Width="96px" onblur="Carrega_Verba_Filtro();"></asp:TextBox>
                                                <asp:ImageButton ID="btnFiltroCarregaVerba" runat="server" Text="Carregar" ImageUrl="~/img/i_search.png" Width="12px" Height="12px" AlternateText="Carregar Verba" ToolTip="Clique para carregar a verba" OnClick="btnFiltroCarregaVerba_Click" />&nbsp;  
                                            </td>
                                            <td>Tipo<br />
                                                <asp:DropDownList ID="ddlFiltroAcertoTipo" runat="server" Width="96px">
                                                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Crédito" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="Débito" Value="D"></asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnFiltrarNovoAcerto" runat="server" Text="Filtrar" CssClass="button" OnClick="btnFiltrarNovoAcerto_Click" Width="90px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnLimparNovoAcerto" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimparNovoAcerto_Click" Width="90px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancelarFiltro" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarFiltro_Click" Width="90px" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>

                            <%-- HiddenFields --%>
                            <asp:HiddenField ID="hdfCodigoRepasse" runat="server" />
                            <asp:HiddenField ID="hdfLinhaRep" runat="server" />
                            <asp:HiddenField ID="hdfCodArqEnvio" runat="server" />
                            <table>
                                <td colspan="5" style="text-align: center; height: 6px;">
                                    <asp:Label ID="lblGridDetalhes_Mensagem" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>
                                </td>
                            </table>
                            <asp:ObjectDataSource runat="server" ID="odsDetalhes"
                                TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Cadastro.ArqPatrocinadoraRepasseBLL"
                                SelectMethod="GetDataDetalhes"
                                SelectCountMethod="GetDataCountDetalhes"
                                EnablePaging="True"
                                SortParameterName="sortParameter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="hdfCodigoRepasse" Name="codigo" PropertyName="Value" Type="Int32" />
                                    <asp:ControlParameter ControlID="txtFiltroAcertoEmpresa" Name="empresa" PropertyName="text" Type="Int16" />
                                    <asp:ControlParameter ControlID="txtFiltroAcertoVerba" Name="verba" PropertyName="text" Type="Int32" />
                                    <asp:ControlParameter ControlID="txtFiltroAcertoMatricula" Name="matricula" PropertyName="text" Type="Int64" />
                                    <asp:ControlParameter ControlID="ddlFiltroAcertoTipo" Name="tipo" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:GridView ID="grdDetalhes"
                                runat="server"
                                DataKeyNames="COD_ARQ_ENV_REP_LINHA"
                                OnRowCommand="grdDetalhes_RowCommand"
                                OnRowEditing="grdDetalhes_RowEditing"
                                OnRowCancelingEdit="grdDetalhes_RowCancelingEdit"
                                AutoGenerateColumns="False"
                                DataSourceID="odsDetalhes"
                                EmptyDataText="Não retornou registros"
                                AllowPaging="True"
                                AllowSorting="True"
                                CssClass="Table"
                                ClientIDMode="Static"
                                PageSize="10">

                                <Columns>
                                    <asp:TemplateField HeaderText="Empresa" SortExpression="COD_EMPRS" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpresaGrd" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEmpresaGrd" runat="server" Text='<%# Bind("COD_EMPRS") %>' Width="100%" onkeypress="mascara(this, soNumeros)" MaxLength="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="reqEmpresaGrd" ControlToValidate="txtEmpresaGrd" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditarGrd" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Matrícula" SortExpression="NUM_RGTRO_EMPRG" HeaderStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMatriculaGrd" runat="server" Text='<%# Bind("NUM_RGTRO_EMPRG") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMatriculaGrd" runat="server" Text='<%# Bind("NUM_RGTRO_EMPRG") %>' Width="100%" onkeypress="mascara(this, soNumeros)" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="reqMatriculaGrd" ControlToValidate="txtMatriculaGrd" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditarGrd" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Verba" SortExpression="COD_VERBA" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerba" runat="server" Text='<%# Bind("COD_VERBA") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtVerbaGrd" runat="server" Text='<%# Bind("COD_VERBA") %>' Width="80%" onkeypress="mascara(this, soNumeros)" MaxLength="5" onblur="Carrega_Verba_Grid();"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="reqVerbaGrd" ControlToValidate="txtVerbaGrd" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditarGrd" />
                                            <asp:ImageButton ID="btnVerbaGrd" runat="server" Text="Carregar" ImageUrl="~/img/i_search.png" Width="12px" Height="12px" AlternateText="Carregar Verba" ToolTip="Clique para carregar a verba" OnClick="btnVerbaGrd_Click" />&nbsp;
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Verba Patrocinadora" SortExpression="COD_VERBA_PATROCINA" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerbaPatroc" runat="server" Text='<%# Bind("COD_VERBA_PATROCINA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Descrição" SortExpression="DCR_VERBA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescricaoGrd" runat="server" Text='<%# Bind("DCR_VERBA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo" SortExpression="TIPO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoGrd" runat="server" Text='<%# ((Eval("TIPO")!=null) ? Eval("TIPO") : "").ToString() == "C" ? "Crédito" : "Débito" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Percentual" SortExpression="VLR_PERCENTUAL" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPercentualGrd" runat="server" Text='<%# Bind("VLR_PERCENTUAL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPercentualGrd" runat="server" Text='<%# Bind("VLR_PERCENTUAL") %>' Width="100%" onkeypress="mascara(this, moeda)" MaxLength="6"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="reqPercentualGrd" ControlToValidate="txtPercentualGrd" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditarGrd" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Valor" SortExpression="VLR_DESCONTO" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValorGrd" runat="server" Text='<%# Bind("VLR_DESCONTO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtValorGrd" runat="server" Text='<%# Bind("VLR_DESCONTO") %>' Width="100px" onkeypress="mascara(this, moeda)" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="reqValorGrd" ControlToValidate="txtValorGrd" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpEditarGrd" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEditarGrd" runat="server" Text="Editar" CommandName="Edit" CssClass="button" CausesValidation="false" />
                                            <asp:Button ID="btnExcluirGrd" runat="server" Text="Excluir" CommandName="DeleteGrd" CommandArgument='<%# Eval("COD_ARQ_ENV_REP_LINHA") %>' CssClass="button" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja excluir?');" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button ID="btnSalvarGrd" runat="server" Text="Salvar" CommandName="UpdateGrd" CssClass="button" ValidationGroup="grpEditarGrd" />
                                            <asp:Button ID="btnCancelarGrd" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" OnClientClick="return confirm('Tem certeza que deseja abandonar a operação?');" CausesValidation="false" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>

                    <asp:Panel ID="Importacao" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td>Mês/Ano:</td>
                                <td>
                                    <asp:TextBox ID="txtMesAb2" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtMesAb2" ControlToValidate="txtMesAb2" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpImportacao" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangMesAb2"
                                        Type="Integer"
                                        ControlToValidate="txtMesAb2"
                                        MaximumValue="12"
                                        MinimumValue="01"
                                        ErrorMessage="Mês inválido"
                                        ForeColor="Red"
                                        Display="Dynamic" />
                                    <b>/</b>
                                    <asp:TextBox ID="txtAnoAb2" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqTxtAnoAb2" ControlToValidate="txtAnoAb2" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpImportacao" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangAnoAb2"
                                        Type="Integer"
                                        ControlToValidate="txtAnoAb2"
                                        MaximumValue="2100"
                                        MinimumValue="1900"
                                        ErrorMessage="Ano inválido"
                                        ForeColor="Red"
                                        Display="Dynamic" />
                                </td>
                                <td>Título:</td>
                                <td>
                                    <asp:TextBox ID="txtReferenciaAba2" runat="server" Width="200px" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqtxtReferenciaAba2" ControlToValidate="txtReferenciaAba2" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpImportacao" />
                                </td>
                            </tr>
                            <tr>
                                <td>Grupo:</td>
                                <td>
                                    <asp:DropDownList ID="ddlGrupoAb2" runat="server" Width="164px"></asp:DropDownList></td>
                                <td>Área:</td>
                                <td>
                                    <asp:DropDownList ID="ddlAreaAb2" runat="server" Width="200px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Empresa:</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmprs" runat="server" Width="200px" />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtCodEmprs" ForeColor="Red" Display="Dynamic" ValidateEmptyText="true" ValidationGroup="grpImportacao" ClientValidationFunction="ValidaCampoEmpresa"></asp:CustomValidator>
                                </td>
                                <td>&nbsp</td>
                                <td>&nbsp</td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rdTipoImport" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdTipoImport_SelectedIndexChanged">
                                        <asp:ListItem Text="Banco de Dados" Value="1" Selected="True" />
                                        <asp:ListItem Text="Arquivo" Value="2" />
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" Visible="false" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="ImportacaoAba2" runat="server" CssClass="button" Text="Importar" OnClientClick="return postbackButtonClick();" OnClick="ImportacaoAba2_Click" ValidationGroup="grpImportacao" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h4>
                                        <asp:Label ID="contador" runat="server" Text="Número de Registros importados :"></asp:Label></h4>
                                    <h4>
                                        <asp:Label runat="server" ID="StatusLabel" Text="Upload Status: " /></h4>
                                </td>
                                <td>
                                    <asp:Label ID="lblMensagemImportacao" runat="server" Visible="False" Width="360px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ImportacaoAba2" />
                    <asp:PostBackTrigger ControlID="btnNovo" />
                    <asp:PostBackTrigger ControlID="btnGerar" />
                    <asp:PostBackTrigger ControlID="btnReprocessar" />
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
