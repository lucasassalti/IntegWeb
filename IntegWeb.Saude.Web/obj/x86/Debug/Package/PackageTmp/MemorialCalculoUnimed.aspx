<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="MemorialCalculoUnimed.aspx.cs" Inherits="IntegWeb.Saude.Web.MemorialCalculoUnimed" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
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
        <div class="tabelaPagina">
            <h1>Memorial Cálculo Unimed </h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <div id="divCamposPesquisa" runat="server" visible="true">
                        <table>
                            <tr>
                                <td>Data Inicio:
                                <asp:TextBox ID="txtDatInicio" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqDatIni" ControlToValidate="txtDatInicio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiAB1" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangDatInicio"
                                        Type="Date"
                                        ControlToValidate="txtDatInicio"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValitiAB1" />
                                </td>
                            </tr>
                            <tr>
                                <td>Data Fim:
                                <asp:TextBox ID="txtDatFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqDatFim" ControlToValidate="txtDatFim" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiAB1" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="ranDatFim"
                                        Type="Date"
                                        ControlToValidate="txtDatFim"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValitiAB1" />
                                </td>
                            </tr>
                            <tr>
                                <td>Unimed:
                               <asp:DropDownList ID="ddlNomeUnimed" runat="server" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqNome" ControlToValidate="ddlNomeUnimed" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiAB1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" ValidationGroup="ValitiAB1" OnClick="btnPesquisar_Click" CausesValidation="true" />

                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divPesquisar" runat="server" class="tabelaPagina" visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnGerar" runat="server" Text="Gerar Relatórios" CssClass="button" CausesValidation="true" OnClick="btnGerar_Click" />

                                    <asp:Button ID="btnGeraAprova" runat="server" Text="Gerar Pagamento" CssClass="button" OnClick="btnGeraAprova_Click" />

                                    <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar" CssClass="button" OnClick="btnAtualizar_Click" />

                                    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="button" OnClick="btnVoltar_Click" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblQtdText" runat="server" Text="Quantidade de movimentações selecionadas: " Visible="false"></asp:Label>
                                    <asp:Label ID="lblQtdCartoes" runat="server" Text="" Visible="false"></asp:Label>
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblValorText" runat="server" Text="Valor total: " Visible="false"></asp:Label>
                                    <asp:Label ID="lblValorTotal" runat="server" Text="0" Visible="false"></asp:Label>
                                </td>

                            </tr>
                        </table>
                      
                        <asp:GridView ID="grdMemorialUnimed"
                            runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="Não retornou registros"
                            AllowPaging="True"
                            AllowSorting="True"
                            OnPageIndexChanging="grdMemorialUnimed_PageIndexChanging"
                            CssClass="Table"
                            ClientIDMode="Static"
                            PageSize="50">

                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Text="" class="span_checkbox" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dt_Mov">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDatMovimentacao" runat="server" Text='<%# Bind("DAT_GERACAO","{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Código" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodControleUnimed" runat="server" Text='<%# Bind("COD_CONTROLEUNIMED") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Empresa">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Matrícula">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumMatricula" runat="server" Text='<%# Bind("NUM_MATRICULA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sub">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSub" runat="server" Text='<%# Bind("SUB_MATRICULA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Identificação">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodIdentificacao" runat="server" Text='<%# Bind("COD_IDENTIFICACAO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nome">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNome" runat="server" Text='<%# Bind("NOM_PARTICIP") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Codigo Unimed" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodUnimed" runat="server" Text='<%# Bind("COD_UNIMED") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unimed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesUnimed" runat="server" Text='<%# Bind("DES_PLANO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Movimentação">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMovimentacao" runat="server" Text='<%# Bind("TIPO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Codigo cesp" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoCesp" runat="server" Text='<%# Bind("COD_PLANO_CESP") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Memorial">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemorial" runat="server" Text='<%# Bind("COBRANCA_MEMORIAL") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValorCI" runat="server" Text='<%# Bind("VALOR","{0:N2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>


                    </div>


                                 <ajax:ModalPopupExtender
                                    ID="ModalPopupExtender1"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopup"
                                    TargetControlID="btnGeraAprova"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="panelPopup" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Informe a Data de Pagamento:</h3>
                                    <table>
                                        <tr>
                                            <td>Data Pagamento:
                                                <asp:TextBox ID="txtDatPagtoPopUp" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px"/>
                                                 <asp:RangeValidator
                                                            runat="server"
                                                            ID="rangDatPagtoPopUp"
                                                            Type="Date"
                                                            ControlToValidate="txtDatPagtoPopUp"
                                                            MaximumValue="31/12/9999"
                                                            MinimumValue="31/12/1000"
                                                            ErrorMessage="Data Inválida"
                                                            ForeColor="Red"
                                                            Display="Dynamic" 
                                                            ValidationGroup="ValidaPopUp"
                                                     />
                                            </td>                                           
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnConfirmaGeracao" runat="server" Text="Confirmar" CssClass="button" OnClick="btnConfirmaGeracao_Click" CausesValidation="true" ValidationGroup="ValidaPopUp"/>
                                                <asp:Button ID="btnCancelarPopUp" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarPopUp_Click" />
                                            </td>

                                        </tr>
                                    </table>

                                </asp:Panel>

                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="false" />
                </ContentTemplate>
                <%--     <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer" />
                </Triggers>--%>
            </asp:UpdatePanel>


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
        </div>
    </div>

</asp:Content>
