<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ArquivoControle.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ArquivoControle" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        function DoPostBack() {

            if ($('#ContentPlaceHolder1_TabContainer_TbGeracao_txtMesGerar').val() != "" && $('#ContentPlaceHolder1_TabContainer_TbGeracao_txtAnoGerar').val() != "") {
                $('#ContentPlaceHolder1_TabContainer_TbGeracao_btnPesquisar').click();
                }
        }

        function DoPostBack_rec() {

            if ($('#ContentPlaceHolder1_TabContainer_TbRecebidos_txtMesGerar_rec').val() != "" && $('#ContentPlaceHolder1_TabContainer_TbRecebidos_txtAnoGerar_rec').val() != "") {
                $('#ContentPlaceHolder1_TabContainer_TbRecebidos_btnPesquisar_rec').click();
            }
        }

        function btnRejeitar_click() {
            if ($('#ContentPlaceHolder1_TabContainer_TbGeracao_txtMesGerar').val() != "" && $('#ContentPlaceHolder1_TabContainer_TbGeracao_txtAnoGerar').val() != "") {
                return confirm('Tem certeza que deseja rejeitar o envio desta area?');
            } else {
                alert('Entre com o Mês e Ano de Referencia para continuar.');
                return false;
            }            
        }

        function btnPublicar_click() {
            if ($('#ContentPlaceHolder1_TabContainer_TbGeracao_txtMesGerar').val() != "" && $('#ContentPlaceHolder1_TabContainer_TbGeracao_txtAnoGerar').val() != "") {
                return confirm('Atenção! Esta ação disponibilizará os arquivos de desconto no Portal Funcesp.  Tem certeza que deseja publicar?');
            } else {
                alert('Entre com o Mês e Ano de Referencia para continuar.');
                return false;
            }
        }

    </script>

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Controle de Envio de Arquivos</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server">
                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbGeracao" HeaderText="Controle de envios" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div id="divPesquisa" runat="server" class="tabelaPagina">
                                    <asp:Panel ID="pnlPesquisa" runat="server">
                                        <table>
                                            <tr>
                                                <td>Mês/Ano:</td>
                                                <td>
                                                    <asp:TextBox ID="txtMesGerar" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" onblur="DoPostBack();"/>
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangMesGerar"
                                                        Type="Integer"
                                                        ControlToValidate="txtMesGerar"
                                                        MaximumValue="13"
                                                        MinimumValue="01"
                                                        ErrorMessage="Mês inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                    <b>/</b>
                                                    <asp:TextBox ID="txtAnoGerar" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" onblur="DoPostBack();" />
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
                                                    <asp:DropDownList ID="ddlGrupo" runat="server" Width="192px" AutoPostBack="true" OnSelectedIndexChanged="btnPesquisar_Click"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
<%--                                                <td>Dt. Referência</td>
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
                                                <td>Status:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="144px"></asp:DropDownList>
                                                </td>
                                                <td>Area:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlArea" runat="server" Width="192px"></asp:DropDownList>
                                                </td>
                                                --%>
                                            </tr>
                                        </table>

                                        <table id="tableBotoesGeracao">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" Style="display: none;" />
                                                    <asp:Button ID="btnPublicar" runat="server" CssClass="button" Text="Publicar" OnClientClick="return btnPublicar_click();"  OnClick="btnPublicar_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label id="TbEnvioMensagem" runat="server" visible="False"></asp:Label> 
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:ObjectDataSource runat="server" ID="odsEnvio"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraEnvioBLL"
                                        SelectMethod="GetDataControle"
                                        SelectCountMethod="GetDataControleCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlGrupo" Name="grupo" PropertyName="SelectedValue" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtMesGerar" Name="mes" PropertyName="Text" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtAnoGerar" Name="ano" PropertyName="Text" Type="Int16" />                                            
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdEnvio" runat="server"
                                        DataKeyNames="COD_ARQ_AREA"
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
                                            <asp:BoundField HeaderText="Area" DataField="DCR_ARQ_AREA_SUB_AREA" SortExpression="DCR_ARQ_AREA_SUB_AREA" />
                                            <asp:TemplateField HeaderText="Validado">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEmpty" ImageUrl="~\img\i_empty.png" runat="server" Visible='<%# short.Parse(Eval("QTD_GERADOS").ToString()) == 0 ? true : false %>' ToolTip='<%# Eval("QTD_GERADOS").ToString() %>'/>
                                                    <%--<asp:Image ID="imgWarning" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# short.Parse(Eval("QTD_GERADOS").ToString()) > 0 && short.Parse(Eval("QTD_GERADOS").ToString()) < 2 ? true : false %>'  ToolTip='<%# Eval("QTD_GERADOS").ToString() %>'/>--%>
                                                    <asp:Image ID="imgOk" ImageUrl="~\img\i_ok.png" runat="server" Visible='<%# short.Parse(Eval("QTD_GERADOS").ToString()) > 0 ? true : false %>' ToolTip='<%# Eval("QTD_GERADOS").ToString() %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Enviado">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEmpty1" ImageUrl="~\img\i_empty.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_ENVIADOS").ToString()) == 0 ? true : false) && short.Parse(Eval("QTD_PUBLICADOS").ToString()) == 0 ? true : false %>' ToolTip='<%# Eval("QTD_ENVIADOS").ToString() %>'/>
                                                    <asp:Image ID="imgWarning1" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_ENVIADOS").ToString()) > 0) && (short.Parse(Eval("QTD_ENVIADOS").ToString()) < 2 ? true : false) && short.Parse(Eval("QTD_PUBLICADOS").ToString()) == 0 ? true : false %>' ToolTip='<%# Eval("QTD_ENVIADOS").ToString() %>'/>
                                                    <asp:Image ID="imgOk1" ImageUrl="~\img\i_ok.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_ENVIADOS").ToString()) > 1 ? true : false) || short.Parse(Eval("QTD_PUBLICADOS").ToString()) > 0 ? true : false %>' ToolTip='<%# Eval("QTD_ENVIADOS").ToString() %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Publicado">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEmpty3" ImageUrl="~\img\i_empty.png" runat="server" Visible='<%# short.Parse(Eval("QTD_PUBLICADOS").ToString()) == 0 ? true : false %>' ToolTip='<%# Eval("QTD_PUBLICADOS").ToString() %>'/>
                                                    <asp:Image ID="imgWarning3" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# short.Parse(Eval("QTD_PUBLICADOS").ToString()) > 0 && short.Parse(Eval("QTD_PUBLICADOS").ToString()) < 2 ? true : false %>' ToolTip='<%# Eval("QTD_PUBLICADOS").ToString() %>'/>
                                                    <asp:Image ID="imgOk3" ImageUrl="~\img\i_ok.png" runat="server" Visible='<%# short.Parse(Eval("QTD_PUBLICADOS").ToString()) > 1 ? true : false %>' ToolTip='<%# Eval("QTD_PUBLICADOS").ToString() %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnRejeitar" runat="server" Text="Rejeitar" CommandName="Rejeitar" CommandArgument='<%# Eval("COD_ARQ_AREA") %>' CssClass="button" CausesValidation="false" OnClientClick="return btnRejeitar_click();" Enabled='<%# short.Parse(Eval("QTD_ENVIADOS").ToString()) > 0 && short.Parse(Eval("QTD_PUBLICADOS").ToString()) == 0 ? true : false %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>                                
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <%--ABA 2--%>
                        <ajax:TabPanel ID="TbRecebidos" HeaderText="Arquivos Recebidos" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div id="divPesquisa_rec" runat="server" class="tabelaPagina">
                                    <asp:Panel ID="pnlPesquisa_rec" runat="server">
                                        <table>
                                            <tr>
                                                <td>Mês/Ano:</td>
                                                <td>
                                                    <asp:TextBox ID="txtMesGerar_rec" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" onblur="DoPostBack_rec();"/>
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangMesGerar_rec"
                                                        Type="Integer"
                                                        ControlToValidate="txtMesGerar_rec"
                                                        MaximumValue="13"
                                                        MinimumValue="01"
                                                        ErrorMessage="Mês inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                    <b>/</b>
                                                    <asp:TextBox ID="txtAnoGerar_rec" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" onblur="DoPostBack_rec();" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangAnoGerar_rec"
                                                        Type="Integer"
                                                        ControlToValidate="txtAnoGerar_rec"
                                                        MaximumValue="2100"
                                                        MinimumValue="1900"
                                                        ErrorMessage="Ano inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                </td>
                                            </tr>
                                        </table>

                                        <table id="tableBotoesRecebimento">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnPesquisar_rec" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_rec_Click" Style="display: none;" />
                                                </td>
                                                <td>
                                                    <asp:Label id="TbEnvioMensagem_rec" runat="server" visible="False"></asp:Label> 
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:ObjectDataSource runat="server" ID="odsRecebidos"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraBLL"
                                        SelectMethod="GetDataControle"
                                        SelectCountMethod="GetDataControleCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtAnoGerar_rec" Name="ano" PropertyName="Text" Type="Int16" />
                                            <asp:ControlParameter ControlID="txtMesGerar_rec" Name="mes" PropertyName="Text" Type="Int16" />                                            
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdRecebidos" runat="server"
                                        DataKeyNames="COD_GRUPO_EMPRS"
                                        DataSourceID="odsRecebidos"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        AllowSorting="True"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        PageSize="10"
                                        OnRowCommand="grdRecebidos_RowCommand">
                                        <Columns>
                                            <asp:BoundField HeaderText="Area" DataField="DCR_GRUPO_EMPRS_MASK" SortExpression="DCR_GRUPO_EMPRS_MASK" />
                                            <asp:TemplateField HeaderText="Cadastral" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEmpty" ImageUrl="~\img\i_empty.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_CADASTRAL_VALIDADO").ToString()) == 0 && short.Parse(Eval("QTD_CADASTRAL_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_CADASTRAL").ToString() %>'/>
                                                    <asp:Image ID="imgWarning" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_CADASTRAL_VALIDADO").ToString()) > 0 && short.Parse(Eval("QTD_CADASTRAL_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_CADASTRAL").ToString() %>'/>
                                                    <asp:Image ID="imgOk" ImageUrl="~\img\i_ok.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_CADASTRAL_CARREGADO").ToString()) > 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_CADASTRAL").ToString() %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Afastamento" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEmpty" ImageUrl="~\img\i_empty.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_AFASTAMENTO_VALIDADO").ToString()) == 0 && short.Parse(Eval("QTD_AFASTAMENTO_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_AFASTAMENTO").ToString() %>'/>
                                                    <asp:Image ID="imgWarning" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_AFASTAMENTO_VALIDADO").ToString()) > 0 && short.Parse(Eval("QTD_AFASTAMENTO_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_AFASTAMENTO").ToString() %>'/>
                                                    <asp:Image ID="imgOk" ImageUrl="~\img\i_ok.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_AFASTAMENTO_CARREGADO").ToString()) > 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_AFASTAMENTO").ToString() %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Orgão de Lotação" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEmpty" ImageUrl="~\img\i_empty.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_ORGAO_LOTACAO_VALIDADO").ToString()) == 0 && short.Parse(Eval("QTD_ORGAO_LOTACAO_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_ORGAO_LOTACAO").ToString() %>'/>
                                                    <asp:Image ID="imgWarning" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_ORGAO_LOTACAO_VALIDADO").ToString()) > 0 && short.Parse(Eval("QTD_ORGAO_LOTACAO_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_ORGAO_LOTACAO").ToString() %>'/>
                                                    <asp:Image ID="imgOk" ImageUrl="~\img\i_ok.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_ORGAO_LOTACAO_CARREGADO").ToString()) > 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_ORGAO_LOTACAO").ToString() %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Financeiro" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEmpty" ImageUrl="~\img\i_empty.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_FINANCEIRO_VALIDADO").ToString()) == 0 && short.Parse(Eval("QTD_FINANCEIRO_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_FINANCEIRO").ToString() %>'/>
                                                    <asp:Image ID="imgWarning" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_FINANCEIRO_VALIDADO").ToString()) > 0 && short.Parse(Eval("QTD_FINANCEIRO_CARREGADO").ToString()) == 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_FINANCEIRO").ToString() %>'/>
                                                    <asp:Image ID="imgOk" ImageUrl="~\img\i_ok.png" runat="server" Visible='<%# (short.Parse(Eval("QTD_FINANCEIRO_CARREGADO").ToString()) > 0) ? true : false %>' ToolTip='<%# Eval("DCR_QTD_FINANCEIRO").ToString() %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
<%--                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAcoes" runat="server" Text="Ações" CommandName="Rejeitar" CommandArgument='<%# Eval("COD_GRUPO_EMPRS") %>' CssClass="button" CausesValidation="false" OnClientClick="return btnRejeitar_click();" Enabled='<%# short.Parse(Eval("COD_GRUPO_EMPRS").ToString()) > 0 ? true : false %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>                                
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>

                    </ajax:TabContainer>
                </ContentTemplate>
<%--                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$TbGeracao$btnSalvarEnvioUpload" />
                </Triggers>--%>

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
