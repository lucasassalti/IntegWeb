<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="SaudeAnexoII.aspx.cs" Inherits="IntegWeb.Saude.Web.SaudeAnexoII" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function getContrato(ddl, txtName) {

            var strText = ddl.options[ddl.selectedIndex].value;
            document.getElementById(txtName).value = strText;
        }

        function getContratoDll(txt, ddlName) {
            $(ddlName).val($(txt).val())

        };


    </script>



    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">

                <div class="tabelaPagina">
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbEmissaoAnexoII" HeaderText="Emissão Anexo II" runat="server" TabIndex="0">
                            <contenttemplate>
                                <div id="divPesquisaAB1" runat="server" class="tabelaPagina">
                                    <h3>Emissão Anexo II:</h3>
                                    <table>
                                        <tr>
                                            <td>Número Contrato:
                                            <asp:TextBox ID="txtNumContratoAB1" runat="server" onkeypress="mascara(this, soNumeros);" Width="100px" onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbEmissaoAnexoII_ddlContratoAB1' )" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqNumContratoAB1" ControlToValidate="txtNumContratoAB1" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB1" />
                                                <asp:DropDownList ID="ddlContratoAB1" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbEmissaoAnexoII_txtNumContratoAB1')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnGerarRelatorioAB1" runat="server" Text="Gerar Relatório" CssClass="button" CausesValidation="true" ValidationGroup="ValidaAB1" OnClick="btnGerarRelatorioAB1_Click" />
                                                <asp:Button ID="btnLimparAB1" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimparAB1_Click" />
                                                <asp:Button ID="btnConsultaAntAB1" runat="server" Text="Consulta Anteriores" CssClass="button" OnClick="btnConsultaAntAB1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divPesquisaAntAB1" runat="server" class="tabelaPagina" visible="false">
                                    <h3>Emissão Anexo II Consulta Anteriores:</h3>
                                    <table>
                                        <tr>
                                            <td>Número Contrato:
                                            <asp:TextBox ID="txtContratoAntAB1" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbEmissaoAnexoII_ddlContratoAntAB1' )"/>
                                                <asp:RequiredFieldValidator runat="server" ID="reqContratoAntAB1" ControlToValidate="txtContratoAntAB1" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAntAB1" />
                                                <asp:DropDownList ID="ddlContratoAntAB1" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbEmissaoAnexoII_txtContratoAntAB1')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnPesquisaAntAB1" runat="server" Text="Consultar" CssClass="button" CausesValidation="true" ValidationGroup="ValidaAntAB1" OnClick="btnPesquisaAntAB1_Click" />
                                                <asp:Button ID="btnVoltarAB1" runat="server" Text="Voltar" CssClass="button" OnClick="btnVoltarAB1_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:ObjectDataSource ID="odsConsultaAnt" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="GetServHospAnt">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtContratoAntAB1" Name="codHosp" PropertyName="Text" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdContratoAnt" runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsConsultaAnt"
                                        EmptyDataText="Não Retornou Registros"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        Visible="false"
                                        OnRowCommand="grdContratoAnt_RowCommand">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Data Vigência">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatIniVigenciaAntAB1" runat="server" Text='<%# Bind("DAT_INI_VIGENCIA","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Fim Vigência">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatFimVigenciaAntAB1" runat="server" Text='<%# Bind("DAT_FIM_VIGENCIA","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnGerarAntAB1" runat="server" Text="Gerar Relatório" CommandName="Visualizar" CssClass="button" CommandArgument="<%# Container.DataItemIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </contenttemplate>
                        </ajax:TabPanel>

                        <%--ABA 2--%>
                        <ajax:TabPanel ID="TbAumento" HeaderText="Aumentos" runat="server" TabIndex="0">
                            <contenttemplate>
                                <h3>Aumentos Valores de Serviços:</h3>

                                <div id="divTiposAumentoAB2" runat="server" class="tabelaPagina">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rdblTipoAumentoAB2" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblTipoAumentoAB2_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="LOTE" Value="LOTE" />
                                                    <asp:ListItem Text="GERAL" Value="GERAL" />
                                                    <asp:ListItem Text="POR VALOR" Value="PORVALOR" />
                                                    <asp:ListItem Text="ESCALONADO" Value="ESCALONADO" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="divPesquisarAB2" runat="server" class="tabelaPagina" visible="false">
                                    <table>
                                        <tr>
                                            <td>Número Contrato:
                                            <asp:TextBox ID="txtNumContratoAB2" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbAumento_ddlContratoAB2' )" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqNumContratoAB2" ControlToValidate="txtNumContratoAB2" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB2" />
                                                <asp:DropDownList ID="ddlContratoAB2" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbAumento_txtNumContratoAB2')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnPesquisarAB2" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="true" ValidationGroup="ValidaAB2" OnClick="btnPesquisarAB2_Click" />
                                                <asp:Button ID="btnLimparAB2" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimparAB2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="divLoteAB2" runat="server" class="tabelaPagina" visible="false" style="width: 100%; height: 500px; overflow: scroll">

                                    <asp:ObjectDataSource ID="odsHospitalAB2" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="CarregaGeralHospital"></asp:ObjectDataSource>
                                    <asp:GridView ID="grdHospitalAB2"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsHospitalAB2"
                                        EmptyDataText="Não Retornou Registros"
                                        CssClass="Table"
                                        ClientIDMode="Static">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text="" class="span_checkbox" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Num.Contrato" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumContratoLoteAB2" runat="server" Text='<%# Bind("COD_HOSP") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Contratos">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContratoLoteAB2" runat="server" Text='<%# Bind("NOME_FANTASIA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div id="divServPrestAB2" runat="server" class="tabelaPagina" visible="false" style="width: 100%; height: 500px; overflow: scroll">

                                    <asp:ObjectDataSource ID="odsServHospAB2" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="GetServPrest">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtNumContratoAB2" Name="codHosp" PropertyName="Text" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdServPrestAB2"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsServHospAB2"
                                        EmptyDataText="Não Retornou Registros"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        OnRowDataBound="grdServPrestAB2_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text="" class="span_checkbox" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Código" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdRegAB2" runat="server" Text='<%# Bind("ID_REG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Contrato" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContratoAB2" runat="server" Text='<%# Bind("COD_HOSP") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Código Serviço" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodServAB2" runat="server" Text='<%# Bind("COD_SERV") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Serviços">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescServAB2" runat="server" Text='<%#Eval("COD_SERV").ToString() + " - " + Eval("DESC_SERV").ToString() %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </div>

                                <div id="divCamposPercAumentoAB2" runat="server" class="tabelaPagina" visible="false">
                                    <table>
                                        <tr>
                                            <td>Percentual Reajuste:
                                               <asp:TextBox ID="txtPercReajAB2" runat="server" onkeypress="mascara(this, moeda)" Width="100px" />
                                                %
                                                &nbsp&nbsp Percentual Desconto:
                                                  <asp:TextBox ID="txtPercDescAB2" runat="server" onkeypress="mascara(this, moeda)" Width="100px" />
                                                %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Data Vigência:
                                                       <asp:TextBox ID="txtDatPercVigenciaAB2" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="70px" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="RangeDatPercVigenciaAB2"
                                                    Type="Date"
                                                    ControlToValidate="txtDatPercVigenciaAB2"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValitiAB2" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="divValorAB2" runat="server" class="tabelaPagina" visible="false">
                                    <table>
                                        <tr>
                                            <td>Valor Proposto:
                                              <asp:TextBox ID="txtValPropostoAB2" runat="server" onkeypress="mascara(this, moeda)" Width="100px" />
                                                Data Vigência:
                                                       <asp:TextBox ID="txtDatValVigenciaAB2" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="70px" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="ragDatValVigenciaAB2"
                                                    Type="Date"
                                                    ControlToValidate="txtDatValVigenciaAB2"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValitiAB2Val" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>


                                <div id="divBotoesAB2" runat="server" class="tabelaPagina" visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnConfirmaAumentoAB2" CssClass="button" runat="server" Text="Confirmar Aumento" OnClick="btnConfirmaAumentoAB2_Click" />
                                                <asp:Button ID="btnPlanilhaAprovacaoAB2" CssClass="button" runat="server" Text="Aumentar/Planilha de Aprovação" OnClick="btnPlanilhaAprovacaoAB2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </contenttemplate>
                        </ajax:TabPanel>

                        <%--ABA 3--%>
                        <ajax:TabPanel ID="TbHospital" HeaderText="Hospitais" runat="server" TabIndex="0">
                            <contenttemplate>
                                <h3>Cadastro de Prestador:</h3>
                                <table>
                                    <tr>
                                        <td>Número Contrato:
                                            <asp:TextBox ID="txtNumContratoAB3" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px"  onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbHospital_ddlContratoAB3' )" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqNumContratoAB3" ControlToValidate="txtNumContratoAB3" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB3" />
                                            <asp:DropDownList ID="ddlContratoAB3" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbHospital_txtNumContratoAB3')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisarAB3" runat="server" Text="Pesquisar" CssClass="button" OnClick="btnPesquisarAB3_Click" CausesValidation="true" ValidationGroup="ValidaAB3" />
                                            <asp:Button ID="btnLimparAB3" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimparAB3_Click" />
                                            <asp:Button ID="btnNovoContratoAB3" runat="server" Text="Novo Contrato" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ObjectDataSource ID="odsHospital" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="GetHospital">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtNumContratoAB3" Name="codHosp" PropertyName="Text" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdHospital"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataSourceID="odsHospital"
                                    EmptyDataText="Não Retornou Registros"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    Visible="false"
                                    OnRowCommand="grdHospital_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Código" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdRegAB3" runat="server" Text='<%# Bind("ID_REG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contrato">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContratoAB3" runat="server" Text='<%# Bind("COD_HOSP") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtContratoAB3" runat="server" Text='<%# Bind("COD_HOSP") %>' onkeypress="mascara(this, soNumeros)" Width="60px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nome">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNomeAB3" runat="server" Text='<%# Bind("NOME_FANTASIA") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNomeAB3" runat="server" Text='<%# Bind("NOME_FANTASIA") %>' MaxLength="250" TextMode="MultiLine" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dat.Ini.Contrato">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDatIniContratoAB3" runat="server" Text='<%# Bind("DAT_INICIO_CONTRATO","{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDatIniContratoAB3" runat="server" Text='<%# Bind("DAT_INICIO_CONTRATO","{0:dd/MM/yyyy}") %>' CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="70px" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="RangeDatIniContratoAB3"
                                                    Type="Date"
                                                    ControlToValidate="txtDatIniContratoAB3"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValitiAB3" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credenciador">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCredenciadorAB3" runat="server" Text='<%# Bind("CREDENCIADOR") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCredenciadorAB3" runat="server" Text='<%# Bind("CREDENCIADOR") %>' MaxLength="150" Width="50px" TextMode="MultiLine" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cidade">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCidadeAB3" runat="server" Text='<%# Bind("CIDADE") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCidadeAB3" runat="server" Text='<%# Bind("CIDADE") %>' MaxLength="100" Width="80px" TextMode="MultiLine" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Regional">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegionalAB3" runat="server" Text='<%# Bind("REGIONAL") %>' Width="50px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRegionalAB3" runat="server" Text='<%# Bind("REGIONAL") %>' MaxLength="100" Width="70px" TextMode="MultiLine" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contato">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContatoAB3" runat="server" Text='<%# Bind("CONTATO") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtContatoAB3" runat="server" Text='<%# Bind("CONTATO") %>' MaxLength="100" Width="80px" TextMode="MultiLine" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditarAB3" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                <asp:Button ID="btnDeleteAB3" runat="server" Text="Excluir" CommandName="DeleteAB3" CommandArgument="<%# Container.DataItemIndex %>" CssClass="button" OnClientClick="return confirm('Ao Excluir o Contrato também irá Excluir os Serviços atrelado a ele, Tem certeza que deseja excluir?');" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnSalvarAB3" runat="server" Text="Salvar" CommandName="UpdateAB3" CssClass="button" ValidationGroup="ValitiAB3" />
                                                <asp:Button ID="btnCancelarAB3" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" CausesValidation="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>


                                <ajax:ModalPopupExtender
                                    ID="ModalPopupExtenderAB3"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopupAB3"
                                    TargetControlID="btnNovoContratoAB3"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="panelPopupAB3" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Inclusão Novo Contrato</h3>
                                    <table>
                                        <tr>
                                            <td>Código Contrato:
                                                <asp:TextBox ID="txtNumContratoPopAB3" runat="server" onkeypress="mascara(this, soNumeros)" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqNumContratoPopAB3" ControlToValidate="txtNumContratoPopAB3" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaIncAB3" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nome:
                                                 <asp:TextBox ID="txtNomePopAB3" runat="server" onkeypress="mascara(this, soLetras)" MaxLength="250" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqNomePopAB3" ControlToValidate="txtNomePopAB3" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaIncAB3" />
                                                Data Inicio Contrato:
                                                 <asp:TextBox ID="txtDatInicioContratoPopAB3" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqDatInicioContratoPopAB3" ControlToValidate="txtDatInicioContratoPopAB3" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaIncAB3" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangDatIniContratoAB3"
                                                    Type="Date"
                                                    ControlToValidate="txtDatInicioContratoPopAB3"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValidaIncAB3" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Credenciador:
                                                 <asp:TextBox ID="txtCredenciadorPopAB3" runat="server" onkeypress="mascara(this, soLetras)" MaxLength="150" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cidade:
                                                 <asp:TextBox ID="txtCidadePopAB3" runat="server" onkeypress="mascara(this, soLetras)" MaxLength="100" />
                                                Regional:
                                                 <asp:TextBox ID="txtRegionalPopAB3" runat="server" onkeypress="mascara(this, soLetras)" MaxLength="100" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Contato:
                                                 <asp:TextBox ID="txtContatoPopAB3" runat="server" MaxLength="150" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInserirAB3" runat="server" Text="Inserir" CssClass="button" OnClick="btnInserirAB3_Click" ValidationGroup="ValidaIncAB3" CausesValidation="true" />
                                                <asp:Button ID="btnCancelarAB3" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarAB3_Click" />
                                                <asp:Label ID="lblCriticaAB3" runat="server" Visible="false" ForeColor="Red" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                            </contenttemplate>
                        </ajax:TabPanel>

                        <%--ABA 4--%>
                        <ajax:TabPanel ID="TbCodServico" HeaderText="Códigos de Serviço" runat="server" TabIndex="0">
                            <contenttemplate>
                                <h3>Cadastro de Serviços:</h3>
                                <table>
                                    <tr>
                                        <td>Código do Serviço:
                                            <asp:TextBox ID="txtCodServicoAB4" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqCodServicoAB4" ControlToValidate="txtCodServicoAB4" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB4" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisarAB4" runat="server" Text="Pesquisar" CssClass="button" OnClick="btnPesquisarAB4_Click" CausesValidation="true" ValidationGroup="ValidaAB4" />
                                            <asp:Button ID="btnLimparAB4" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimparAB4_Click" />
                                            <asp:Button ID="btnNovoServicoAB4" runat="server" Text="Novo Serviço" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ObjectDataSource ID="odsCodServico" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="GetServico">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCodServicoAB4" Name="codServ" PropertyName="Text" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdServico"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataSourceID="odsCodServico"
                                    EmptyDataText="Não Retornou Registros"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    Visible="false"
                                    OnRowCommand="grdServico_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Código" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdRegAB4" runat="server" Text='<%# Bind("ID_REG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Código Serviço">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodServAB4" runat="server" Text='<%# Bind("COD_SERV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descrição">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescricaoAB4" runat="server" Text='<%# Bind("DESCRICAO") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescricaoAB4" runat="server" Text='<%# Bind("DESCRICAO") %>' MaxLength="2000" TextMode="MultiLine" Width="470px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditarAB4" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                <asp:Button ID="btnDeleteAB4" runat="server" Text="Excluir" CommandName="DeleteAB4" CommandArgument="<%# Container.DataItemIndex %>" CssClass="button" OnClientClick="return confirm('Ao Excluir o Serviço também irá Excluir os Serviços atrelado aos Prestadores, Tem certeza que deseja excluir?');" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnSalvarAB4" runat="server" Text="Salvar" CommandName="UpdateAB4" CssClass="button" />
                                                <asp:Button ID="btnCancelarAB4" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" CausesValidation="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <ajax:ModalPopupExtender
                                    ID="ModalPopupServico"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopupServico"
                                    TargetControlID="btnNovoServicoAB4"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="panelPopupServico" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Cadastro de Serviços:</h3>
                                    <table>
                                        <tr>
                                            <td>Código do Serviço:
                                            <asp:TextBox ID="txtCodServPopAB4" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqCodServPopAB4" ControlToValidate="txtCodServPopAB4" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaPopAB4" />
                                            </td>
                                            <td>Descrição:
                                            <asp:TextBox ID="txtDescServPopAB4" runat="server" TextMode="MultiLine" Width="470px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqDescServPopAB4" ControlToValidate="txtDescServPopAB4" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaPopAB4" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInserirServPopAB4" runat="server" Text="Inserir" CssClass="button" OnClick="btnInserirServPopAB4_Click" ValidationGroup="ValidaPopAB4" />
                                                <asp:Button ID="btnCancelaServPopAB4" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelaServPopAB4_Click" />
                                                <asp:Label ID="lblCriticaPopAB4" runat="server" Visible="false" ForeColor="Red" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                            </contenttemplate>
                        </ajax:TabPanel>

                        <%--ABA 5--%>
                        <ajax:TabPanel ID="TbServPrestador" HeaderText="Tabela de Serviços por Prestador" runat="server" TabIndex="0">
                            <contenttemplate>
                                <h3>Serviço por Prestador:</h3>
                                <table>
                                    <tr>
                                        <td>Número Contrato:
                                            <asp:TextBox ID="txtNumContratoAB5" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbServPrestador_ddlContratoAB5' )" />
                                            <asp:RequiredFieldValidator runat="server" ID="reqNumContratoAB5" ControlToValidate="txtNumContratoAB5" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB5" />
                                            <asp:DropDownList ID="ddlContratoAB5" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbServPrestador_txtNumContratoAB5')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Código Serviço:
                                              <asp:TextBox ID="txtCodServicoAB5" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisarAB5" runat="server" Text="Pesquisar" CssClass="button" OnClick="btnPesquisarAB5_Click" CausesValidation="true" ValidationGroup="ValidaAB5" />
                                            <asp:Button ID="btnLimparAB5" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimparAB5_Click" />
                                            <asp:Button ID="btnNovoServPrestAB5" runat="server" Text="Novo" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ObjectDataSource ID="odsServPrestAB5" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="GetServHosp">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtNumContratoAB5" Name="codHosp" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtCodServicoAB5" Name="codServ" PropertyName="Text" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdServPrestAB5"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataSourceID="odsServPrestAB5"
                                    EmptyDataText="Não Retornou Registros"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    Visible="false"
                                    OnRowCommand="grdServPrestAB5_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Código" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdRegAB5" runat="server" Text='<%# Bind("ID_REG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contrato">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContratoAB5" runat="server" Text='<%# Bind("COD_HOSP") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Código Serviço">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodServAB5" runat="server" Text='<%# Bind("COD_SERV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Valor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValorAB5" runat="server" Text='<%# Bind("VALOR","{0:N2}") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtValorAB5" runat="server" Text='<%# Bind("VALOR","{0:N2}") %>' onkeypress="mascara(this, moeda)" Width="70px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Data Vigência">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDatIniVigenciaAB5" runat="server" Text='<%# Bind("DAT_INI_VIGENCIA","{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDatIniVigenciaAB5" runat="server" Text='<%# Bind("DAT_INI_VIGENCIA","{0:dd/MM/yyyy}") %>' CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="70px" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="RangeDatIniVigenciaAB5"
                                                    Type="Date"
                                                    ControlToValidate="txtDatIniVigenciaAB5"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValitiAB5" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditarAB5" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                <asp:Button ID="btnDeleteAB5" runat="server" Text="Excluir" CommandName="DeleteAB5" CommandArgument="<%# Container.DataItemIndex %>" CssClass="button" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnSalvarAB5" runat="server" Text="Salvar" CommandName="UpdateAB5" CssClass="button" ValidationGroup="ValitiAB5" />
                                                <asp:Button ID="btnCancelarAB5" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" CausesValidation="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <ajax:ModalPopupExtender
                                    ID="ModalPopupServPrest"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopUpServPrest"
                                    TargetControlID="btnNovoServPrestAB5"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="panelPopUpServPrest" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Cadastro de Serviços para Prestador:</h3>
                                    <table>
                                        <tr>
                                            <td>Contrato:
                                                <asp:TextBox ID="txtNumContratoPopAB5" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbServPrestador_ddlContratoPopUpAB5' )" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqNumContratoPopAB5" ControlToValidate="txtNumContratoPopAB5" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiPopAB5" />
                                                <asp:DropDownList ID="ddlContratoPopUpAB5" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbServPrestador_txtNumContratoPopAB5')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Serviço:
                                                <asp:TextBox ID="txtNumServicoPopAB5" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbServPrestador_ddlServicoPopUpAB5' )" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqNumServicoPopAB5" ControlToValidate="txtNumServicoPopAB5" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiPopAB5" />
                                                <asp:DropDownList ID="ddlServicoPopUpAB5" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbServPrestador_txtNumServicoPopAB5')" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Valor:
                                                  <asp:TextBox ID="txtValorPopUpAB5" runat="server" onkeypress="mascara(this, moeda)" Width="70px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqValorPopUpAB5" ControlToValidate="txtValorPopUpAB5" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiPopAB5" />
                                                Data Inicio Vigência:
                                                  <asp:TextBox ID="txtDatIniVigenciaPopUpAB5" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="70px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqDatIniVigenciaPopUpAB5" ControlToValidate="txtDatIniVigenciaPopUpAB5" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValitiPopAB5" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="RangeDatIniVigenciaPopUpAB5"
                                                    Type="Date"
                                                    ControlToValidate="txtDatIniVigenciaPopUpAB5"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValitiPopAB5" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInserirServPrestPopUpAB5" runat="server" Text="Inserir" CssClass="button" ValidationGroup="ValitiPopAB5" OnClick="btnInserirServPrestPopUpAB5_Click" />
                                                <asp:Button ID="btnCancelarServPrestPopUpAB5" runat="server" Text="Cancelar/Voltar" CssClass="button" OnClick="btnCancelarServPrestPopUpAB5_Click" />
                                                <asp:Label ID="lblCriticaAB5" runat="server" Visible="false" ForeColor="Red" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>


                            </contenttemplate>
                        </ajax:TabPanel>

                        <%--ABA 6--%>
                        <ajax:TabPanel ID="TbObservacao" HeaderText="Observações Contratuais" runat="server" TabIndex="0">
                            <contenttemplate>
                                <h3>Observações Contratuais:</h3>
                                <table>
                                    <tr>
                                        <td>Número Contrato:
                                            <asp:TextBox ID="txtNumContratoAB6" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" onkeyup="getContratoDll(this, '#ContentPlaceHolder1_TabContainer_TbObservacao_ddlContratoAB6' )"/>
                                            <asp:RequiredFieldValidator runat="server" ID="reqNumContratoAB6" ControlToValidate="txtNumContratoAB6" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaAB6" />
                                            <asp:DropDownList ID="ddlContratoAB6" runat="server" onchange="getContrato(this,'ContentPlaceHolder1_TabContainer_TbObservacao_txtNumContratoAB6')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPesquisarAB6" runat="server" Text="Pesquisar" CssClass="button" OnClick="btnPesquisarAB6_Click" ValidationGroup="ValidaAB6" />
                                            <asp:Button ID="btnLimparAB6" runat="server" Text="Limpar" CssClass="button" OnClick="btnLimparAB6_Click" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ObjectDataSource ID="odsObsContratual" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="GetHospital">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtNumContratoAB6" Name="codHosp" PropertyName="Text" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdObsContratual"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataSourceID="odsObsContratual"
                                    EmptyDataText="Não Retornou Registros"
                                    CssClass="Table"
                                    ClientIDMode="Static"
                                    Visible="false"
                                    OnRowCommand="grdObsContratual_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Código" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdRegAB6" runat="server" Text='<%# Bind("ID_REG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contrato" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContratoAB6" runat="server" Text='<%# Bind("COD_HOSP") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Observação Contratual">
                                            <ItemTemplate>
                                                <asp:Label ID="lblObsContratualAB6" runat="server" Text='<%# Bind("OBSERVACAOCONTRATUAL") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtObsContratualAB6" runat="server" Text='<%# Bind("OBSERVACAOCONTRATUAL") %>' TextMode="MultiLine" Width="590px" Height="200px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditarAB6" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnSalvarAB6" runat="server" Text="Salvar" CommandName="UpdateAB6" CssClass="button" />
                                                <asp:Button ID="btnCancelarAB6" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" CausesValidation="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </contenttemplate>
                        </ajax:TabPanel>

                        <%--ABA 7--%>
                        <ajax:TabPanel ID="TbExportacao" HeaderText="Exportação para o SCAM" runat="server" TabIndex="0">
                            <contenttemplate>
                                <h3>Exportação Anexo II / SCAM:</h3>
                                <div id="divExportacaoAB7" runat="server" class="tabelaPagina">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnExportacaoAB7" runat="server" Text="Exportar para o SCAM" CssClass="button" OnClick="btnExportacaoAB7_Click" />
                                                <asp:Button ID="btnHistoricoExportacaoAB7" runat="server" Text="Histórico de Exportação" CssClass="button" OnClick="btnHistoricoExportacaoAB7_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                    <div style="width: 100%; height: 500px; overflow: scroll">

                                        <asp:ObjectDataSource ID="odsHospitalAB7" runat="server" TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL" SelectMethod="CarregaGeralHospital"></asp:ObjectDataSource>

                                        <asp:GridView ID="grdExportacao"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataSourceID="odsHospitalAB7"
                                            EmptyDataText="Não Retornou Registros"
                                            CssClass="Table"
                                            ClientIDMode="Static">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text="" class="span_checkbox" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cod.Contrato" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodContratoAB7" runat="server" Text='<%# Bind("COD_HOSP") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contratos">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContratoAB7" runat="server" Text='<%# Bind("NOME_FANTASIA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>


                                    </div>
                                </div>

                                <div id="divHistoricoExportacao" runat="server" class="tabelaPagina" visible="false">
                                    <table>
                                        <tr>
                                            <td>Número Contrato:
                                            <asp:TextBox ID="txtNumContratoHistAB7" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Data Exportação:
                                                 <asp:TextBox ID="txtDatExportacaoHistAB7" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="70px" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="RangeDatExportacaoHistAB7"
                                                    Type="Date"
                                                    ControlToValidate="txtDatExportacaoHistAB7"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValitiAB7" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnPesquisarAB7" runat="server" Text="Pesquisar" CssClass="button" ValidationGroup="ValitiAB7" OnClick="btnPesquisarAB7_Click" />
                                                <asp:Button ID="btnVoltarAB7" runat="server" Text="Voltar" CssClass="button" OnClick="btnVoltarAB7_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:ObjectDataSource ID="odsHistoricoExportacao"
                                        runat="server"
                                        TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.SaudeAnexoIIBLL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtNumContratoHistAB7" Name="codHosp" PropertyName="Text" Type="Int32" />
                                            <asp:ControlParameter ControlID="txtDatExportacaoHistAB7" Name="datExportacao" PropertyName="Text" Type="DateTime" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdHistExportacaoAB7"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsHistoricoExportacao"
                                        EmptyDataText="Não Retornou Registros"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        AllowPaging="true"
                                        AllowSorting="true"
                                        PageSize="10"
                                        OnRowCommand="grdHistExportacaoAB7_RowCommand">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Cod.Contrato">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodContratoHistAB7" runat="server" Text='<%# Bind("COD_HOSP") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qtd.Serviços Processados">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodServicoHistAB7" runat="server" Text='<%# Bind("QTD_SERV") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dat.Processamento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatProcessamentoHistAB7" runat="server" Text='<%# Bind("DAT_PROCESSAMENTO","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Usuário">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsuProcessamentoHistAB7" runat="server" Text='<%# Bind("USU_PROCESSAMENTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDetalhesAB7" runat="server" Text="Detalhes" CommandName="Visualizar" CssClass="button" CommandArgument="<%# Container.DataItemIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                </div>

                            </contenttemplate>
                        </ajax:TabPanel>

                    </ajax:TabContainer>
                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="false" />
                </div>
            </div>
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

</asp:Content>
