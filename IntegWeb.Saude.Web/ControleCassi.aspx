<%@ Page Title="" Language="c#" MasterPageFile="~/Principal.Master" CodeBehind="~/ControleCassi.aspx.cs" Inherits="IntegWeb.Saude.Web.ControleCassi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
        var timer;

        function validarArquivo(elem) {
            //elementos
            var parentDiv = elem.parentNode;
            var fileUpload = parentDiv.querySelector('input[type="file"]');
            var lblResponse = $(parentDiv).find('span').get(0);
            var files = fileUpload.files;
            var isValid = false;

            if (files.length > 0) {
                if (fileUpload.id == 'uplDevolucao')
                    isValid = files[0].type == 'text/plain' ? true : false;
                else
                    isValid = files[0].type == 'application/vnd.ms-excel' ||
                        files[0].type == 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' ?
                        true : false;

                lblResponse.innerText = !isValid ? 'Formato de arquivo inválido' : '';
            }
            else
                lblResponse.innerText = 'Nenhum arquivo selecionado';

            return isValid;
        }

        function valSummaryFade() {
            if (!!timer)
                clearTimeout(timer);

            var elem = $('.validation-summary');
            elem.fadeIn();

            timer = setTimeout(function () {
                elem.fadeOut();
            }, 3000);
        }

        function toggleOverflow() {
            $('body').toggleClass('overflow-hidden');
        }
    </script>

    <style type="text/css">
        .modal-header-custom {
            font-size: 14pt;
            color: black;
        }

        .modal-custom {
            width: auto;
            height: auto;
            padding: 10px;
            background: #fff;
        }

        .modal-body-custom {
            display: grid;
        }

        .form-control-custom {
            width: 250px;
        }

        .custom-row {
            width: 100% !important;
        }

            .custom-row input {
                width: 97% !important;
            }

        .form-group-custom {
            display: flex;
        }

        .modal-body-custom label {
            align-self: center;
            text-align: right;
            min-width: 150px;
        }

        .modal-body-custom input:not([type='submit']), select {
            margin-bottom: 5px;
            line-height: 22px;
            width: 95%;
        }

        .modal-background-custom {
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0, 0.7);
            z-index: 10001;
            overflow: hidden;
        }

        .modal-footer-custom {
            text-align: right;
            margin-top: 10px;
        }

        .validation-summary {
            box-shadow: #3c3c3c 5px 5px 5px;
            position: absolute;
            right: -50%;
            background: white;
            line-height: 15px;
            padding: 10px;
            top: 0;
            border-radius: 5px;
            display: none;
            width: 200px;
        }

        .table-custom * {
            font-size: 9px !important;
        }

        .overflow-hidden {
            overflow: hidden;
        }
    </style>

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Controle Cassi</h1>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" AutoPostBack="true" runat="server">
                        <%-- BASE CASSI --%>
                        <ajax:TabPanel ID="TabPanelCassiBase" runat="server" HeaderText="Base Cassi">
                            <ContentTemplate>
                                <div class="tabelaPagina" style="margin-bottom: 5px;">
                                    <span>Pesquisar:</span>
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Nome, CPF ou Cartão"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnSearch" Text="Buscar" CausesValidation="false" CssClass="button" OnClick="btnSearch_Click" />

                                    <asp:Button runat="server" ID="btnAdicionar" CausesValidation="false" Text="Adicionar" OnClientClick="toggleOverflow()" CssClass="button" />
                                    <asp:Button runat="server" ID="btnCassiMovimentacao" CausesValidation="false" Text="Gerar Movimentação" CssClass="button" OnClick="btnCassiMovimentacao_Click" />
                                </div>

                                <hr />

                                <asp:GridView runat="server"
                                    ID="gvParticipantes"
                                    CssClass="Table table-custom"
                                    PageIndex="0"
                                    AllowPaging="True" PageSize="20"
                                    AllowSorting="true"
                                    OnPageIndexChanging="gvParticipantes_PageIndexChanging"
                                    OnSorting="gvParticipantes_Sorting">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="Primeira" LastPageText="Ultima" NextPageText="Próxima" PreviousPageText="Anterior" />
                                </asp:GridView>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <%-- IMPORTAR DEVOLUÇÃO --%>
                        <ajax:TabPanel ID="TabPanelCassiDevolucao" runat="server" HeaderText="Importar Devolução">
                            <ContentTemplate>
                                <h3>Importar Devolução</h3>
                                <asp:FileUpload runat="server" CssClass="button" AllowMultiple="false" ID="uplDevolucao" ClientIDMode="Static" />
                                <asp:Button ID="btnImportDevolucao"
                                    CausesValidation="false"
                                    ClientIDMode="Static"
                                    runat="server"
                                    CssClass="button"
                                    Text="Enviar"
                                    OnClientClick="return validarArquivo(this)"
                                    OnClick="btnImportDevolucao_Click"
                                    CommandArgument="uplDevolucao" />
                                <asp:Label runat="server" ID="lblDevolucaoVal" ClientIDMode="Static" ForeColor="Red" />

                                <br />

                                <asp:Panel runat="server" ID="pnDevolucaoResult" />
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <%-- IMPORTAR BASE --%>
                        <ajax:TabPanel ID="TabPanelImportBase" runat="server" HeaderText="Importar Base" Visible="false">
                            <ContentTemplate>
                                <h3>Importar Base</h3>
                                <asp:FileUpload runat="server" CssClass="button" AllowMultiple="false" ID="uplBase" ClientIDMode="Static" />
                                <asp:Button ID="btnImportBase"
                                    CausesValidation="false"
                                    runat="server"
                                    CssClass="button"
                                    OnClick="btnImportBase_Click"
                                    OnClientClick="return validarArquivo(this)"
                                    Text="Enviar"
                                    CommandArgument="uplBase" />
                                <asp:Label runat="server" ID="lblBaseImportacaoVal" ClientIDMode="Static" ForeColor="Red" />
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>

                    <ajax:ModalPopupExtender runat="server"
                        ID="ModalPopupExtender1"
                        TargetControlID="TabContainer$TabPanelCassiBase$btnAdicionar"
                        PopupControlID="pnAdicionar"
                        CancelControlID="btnCancelar"
                        OnCancelScript="toggleOverflow()"
                        BackgroundCssClass="modal-background-custom"
                        DropShadow="true">
                    </ajax:ModalPopupExtender>

                    <asp:Panel runat="server" ID="pnAdicionar" CssClass="modal-custom">
                        <div class="modal-header-custom">Adicionar</div>

                        <div class="modal-body-custom">
                            <%-- Nome --%>
                            <div class="custom-row form-control-custom">
                                <label>*Nome:</label>
                                <asp:TextBox runat="server" ID="txtNome" placeholder="Nome"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="txtNomeVal" ControlToValidate="txtNome" ErrorMessage="Nome" Display="None"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group-custom">
                                <%-- CPF --%>
                                <div class="form-control-custom">
                                    <label>*CPF:</label>
                                    <asp:TextBox runat="server" ID="txtCPF" placeholder="CPF" onkeypress="MascaraCPF(this)" MaxLength="14"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="txtCPFVal" ControlToValidate="txtCPF" ErrorMessage="CPF" Display="None"></asp:RequiredFieldValidator>
                                </div>

                                <%-- Data Nascimento --%>
                                <div class="form-control-custom">
                                    <label>*Data de Nascimento:</label>
                                    <asp:TextBox runat="server" ID="txtNascimento" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server"
                                        ID="valNascimento"
                                        ControlToValidate="txtNascimento"
                                        ErrorMessage="Data de Nascimento"
                                        Display="None" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangeNascimento"
                                        Type="Date"
                                        ControlToValidate="txtNascimento"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValitiAB1" />
                                </div>
                            </div>

                            <div class="form-group-custom">
                                <%-- Situação --%>
                                <div class="form-control-custom">
                                    <label>*Situação:</label>
                                    <asp:DropDownList runat="server" ID="ddlSituacao" CssClass="button">
                                        <asp:ListItem Value="">Selecione...</asp:ListItem>
                                        <asp:ListItem Value="1">Normal</asp:ListItem>
                                        <asp:ListItem Value="0">Cancelado</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server"
                                        ID="ddlSituacaoVal"
                                        ControlToValidate="ddlSituacao"
                                        ErrorMessage="Situação"
                                        Display="None" />
                                </div>

                                <%-- UF --%>
                                <div class="form-control-custom">
                                    <label>UF:</label>
                                    <asp:DropDownList runat="server" ID="ddlUF" CssClass="button">
                                        <asp:ListItem Value="">Selecione...</asp:ListItem>
                                        <asp:ListItem Value="AC">AC</asp:ListItem>
                                        <asp:ListItem Value="AL">AL</asp:ListItem>
                                        <asp:ListItem Value="AM">AM</asp:ListItem>
                                        <asp:ListItem Value="AP">AP</asp:ListItem>
                                        <asp:ListItem Value="BA">BA</asp:ListItem>
                                        <asp:ListItem Value="CE">CE</asp:ListItem>
                                        <asp:ListItem Value="DF">DF</asp:ListItem>
                                        <asp:ListItem Value="ES">ES</asp:ListItem>
                                        <asp:ListItem Value="GO">GO</asp:ListItem>
                                        <asp:ListItem Value="MA">MA</asp:ListItem>
                                        <asp:ListItem Value="MG">MG</asp:ListItem>
                                        <asp:ListItem Value="MS">MS</asp:ListItem>
                                        <asp:ListItem Value="MT">MT</asp:ListItem>
                                        <asp:ListItem Value="PA">PA</asp:ListItem>
                                        <asp:ListItem Value="PB">PB</asp:ListItem>
                                        <asp:ListItem Value="PE">PE</asp:ListItem>
                                        <asp:ListItem Value="PI">PI</asp:ListItem>
                                        <asp:ListItem Value="PR">PR</asp:ListItem>
                                        <asp:ListItem Value="RJ">RJ</asp:ListItem>
                                        <asp:ListItem Value="RN">RN</asp:ListItem>
                                        <asp:ListItem Value="RS">RS</asp:ListItem>
                                        <asp:ListItem Value="RO">RO</asp:ListItem>
                                        <asp:ListItem Value="RR">RR</asp:ListItem>
                                        <asp:ListItem Value="SC">SC</asp:ListItem>
                                        <asp:ListItem Value="SE">SE</asp:ListItem>
                                        <asp:ListItem Value="SP">SP</asp:ListItem>
                                        <asp:ListItem Value="TO">TO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="ddlUFVal" ControlToValidate="ddlUF" ErrorMessage="UF" Display="None"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group-custom">
                                <%-- Cartão --%>
                                <div class="form-control-custom">
                                    <label>*Cartão:</label>
                                    <asp:TextBox runat="server" ID="txtCartao" onkeypress="mascara(this, soNumeros)" placeholder="Número do Cartão"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="txtCartaoVal" ControlToValidate="txtCartao" ErrorMessage="Cartão" Display="None"></asp:RequiredFieldValidator>
                                </div>

                                <%-- Adesão --%>
                                <div class="form-control-custom">
                                    <label>*Adesão:</label>
                                    <asp:TextBox runat="server" ID="txtAdesao" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server"
                                        ID="valtxtAdesao"
                                        ControlToValidate="txtAdesao"
                                        ErrorMessage="Adesão"
                                        Display="None" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangeDataAdesao"
                                        Type="Date"
                                        ControlToValidate="txtAdesao"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValitiAB1" />
                                </div>
                            </div>

                            <div class="form-group-custom">
                                <%-- Validade Inicio --%>
                                <div class="form-control-custom">
                                    <label>*Validade Inicio:</label>
                                    <asp:TextBox runat="server" ID="txtValidadeInicio" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server"
                                        ID="valValidadeInicio"
                                        ControlToValidate="txtValidadeInicio"
                                        ErrorMessage="Validade Inicio"
                                        Display="None" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangeValidadeInicio"
                                        Type="Date"
                                        ControlToValidate="txtValidadeInicio"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValitiAB1" />
                                </div>

                                <%-- Validade Fim --%>
                                <div class="form-control-custom">
                                    <label>*Validade Fim:</label>
                                    <asp:TextBox runat="server" ID="txtValidadeFim" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server"
                                        ID="valValidadeFim"
                                        ControlToValidate="txtValidadeFim"
                                        ErrorMessage="Validade Fim"
                                        Display="None" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangeValidadeFim"
                                        Type="Date"
                                        ControlToValidate="txtValidadeFim"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValitiAB1" />
                                </div>
                            </div>

                            <i>*Campos obrigatórios</i>

                            <div class="validation-summary">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Por favor, verifique os campos:" Visible="True" />
                            </div>
                        </div>

                        <div class="modal-footer-custom">
                            <asp:Button runat="server" ID="btnSalvar" CssClass="button" OnClientClick="valSummaryFade()" Text="Salvar" />
                            <asp:Button runat="server" ID="btnCancelar" CausesValidation="false" CssClass="button" Text="Cancelar" />
                        </div>
                    </asp:Panel>
                    </form>
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer" />
                </Triggers>
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
