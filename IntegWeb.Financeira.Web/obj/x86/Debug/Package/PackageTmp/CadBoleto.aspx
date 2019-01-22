<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadBoleto.aspx.cs" Inherits="IntegWeb.Financeira.Web.CadBoleto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class="MarginGrid">
    <script type="text/javascript">
        function _client_side_script() {
            $('#chkSelectAll').click(function () {
                $('.span_checkbox input:checkbox').prop('checked', $('#chkSelectAll').prop('checked'));
            });
        };
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="full_w">
                <asp:Panel runat="server" ID="pnlGrid" class="tabelaPagina">
                    <h1>Consulta Boletos por Participante</h1>
                    <table>
                        <tr>
                            <td>Tipo</td>
                            <td>
                                <asp:DropDownList ID="ddlPesqTipoBoleto" runat="server" Width="210px" OnSelectedIndexChanged="ddlPesqTipoBoleto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td>Sub-Tipo</td>
                            <td>
                                <asp:DropDownList ID="ddlPesqSubTipoBoleto" runat="server" Width="130px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Empresa</td>
                            <td>
                                <asp:TextBox ID="txtPesqCodEmpresa" runat="server" Style="width: 30px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                &nbsp&nbsp&nbsp&nbspMatrícula&nbsp&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtPesqMatricula" runat="server" Style="width: 50px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                            </td>
                            <td>
                            Digíto</td>
                            <td>
                                <asp:TextBox ID="txtPesqDigito" runat="server" Style="width: 20px;" onkeypress="mascara(this, soNumeros)" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CPF</td>
                            <td>
                                <asp:TextBox ID="txtPesqCpf" runat="server" Style="width: 100px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                <asp:HiddenField ID="hidPesqNUM_IDNTF_RPTANT" runat="server" />
                            </td>
                            <td>Nº Lote</td>
                            <td>
                                <asp:TextBox ID="txtPesqLote" runat="server" Style="width: 100px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Nome</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPesqNome" runat="server" Style="width: 376px;"></asp:TextBox><br />
                                <asp:Label ID="lblPesqEnd1" runat="server" Style="width: 376px;" ></asp:Label><br />
                                <asp:Label ID="lblPesqEnd2" runat="server" Style="width: 376px;" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Período Processa.</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDtIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" runat="server" ControlToValidate="txtDtIni" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                &nbsp&nbsp&nbspaté&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtDtFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ForeColor="Red" runat="server" ControlToValidate="txtDtFim" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>E-Mail para envio</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtEMail" runat="server" Width="267px"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnEmail" runat="server" CssClass="button" Text="Enviar" OnClick="btnEmail_Click" Style="height: 20px; padding-top: 3px;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btLimpar_Click" />
                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" OnClick="btnNovo_Click" />
                                <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" Style="display: none;" />
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblMensagem" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <asp:ObjectDataSource runat="server" ID="odsBoleto"
                        TypeName="IntegWeb.Financeira.Aplicacao.BLL.BoletoBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPesqCodEmpresa" Name="pEmpresa" PropertyName="Text" Type="Int16" />
                            <asp:ControlParameter ControlID="txtPesqMatricula" Name="pMatricula" PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlPesqTipoBoleto" Name="pTipoBoleto" PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlPesqSubTipoBoleto" Name="pSubTipoBoleto" PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtPesqCpf" Name="pCpf" PropertyName="Text" Type="Int64" />
                            <asp:ControlParameter ControlID="txtPesqLote" Name="pLote" PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="txtPesqNome" Name="pNome" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtDtIni" Name="pDtIni" PropertyName="Text" Type="DateTime" />
                            <asp:ControlParameter ControlID="txtDtFim" Name="pDtFim" PropertyName="Text" Type="DateTime" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdBoleto" runat="server" 
                        DataKeyNames="COD_BOLETO" 
                        OnRowCancelingEdit="GridView_RowCancelingEdit" 
                        OnRowCreated="GridView_RowCreated"
                        OnRowCommand="grdBoleto_RowCommand"
                        AllowPaging="true" 
                        AllowSorting="true"
                        AutoGenerateColumns="False" 
                        EmptyDataText="A consulta não retornou registros" 
                        CssClass="Table" 
                        ClientIDMode="Static" 
                        PageSize="50" 
                        DataSourceID="odsBoleto">
                        <PagerSettings
                            Visible="true"
                            PreviousPageText="Anterior"
                            NextPageText="Próxima"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text="" class="span_checkbox" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="CPF" DataField="NUM_CPF" SortExpression="NUM_CPF"/>
                            <%--<asp:BoundField HeaderText="Tipo" DataField="AAT_TBL_BOLETO_TIPO.NOM_BOLETO" SortExpression="COD_BOLETO_TIPO"/>
                            <asp:BoundField HeaderText="Tipo" DataField="TIPO_BOLETO" SortExpression="TIPO_BOLETO"/> --%>
                            <asp:BoundField HeaderText="Tipo" DataField="AAT_TBL_BOLETO_TIPO.NOM_BOLETO_ABREV" SortExpression="COD_BOLETO_TIPO"/>
                            <asp:BoundField HeaderText="Nome" DataField="NOM_EMPR" SortExpression="NOM_EMPR"/>
                            <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" SortExpression="COD_EMPRS"/>
                            <asp:BoundField HeaderText="Registro" DataField="NUM_RGTRO_EMPRG" SortExpression="NUM_RGTRO_EMPRG"/>                            
                            <asp:BoundField HeaderText="Dt. Vencimento" DataField="DAT_VENCT_LCEMP" SortExpression="DAT_VENCT_LCEMP" DataFormatString="{0:d}"/>
                            <asp:BoundField HeaderText="Dt. Processamento" DataField="DT_PROCESSAMENTO" SortExpression="DT_PROCESSAMENTO" DataFormatString="{0:d}"/>
                            <asp:BoundField HeaderText="Valor" DataField="VLR_DOCTO" SortExpression="VLR_DOCTO" DataFormatString="{0:c}" />
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btVisualizar" CausesValidation="false" CssClass="button" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# Eval("COD_BOLETO") + "," + Eval("NUM_DCMCOB_BLPGT") + "," + Eval("AAT_TBL_BOLETO_TIPO.NOM_BOLETO_ABREV") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Visible="false" />

                   </asp:Panel>
                   <asp:Panel runat="server" ID="pnlNovoBoleto" class="tabelaPagina" Visible="false">
                       <h1>Gerar no Título de Cobrança</h1>
                        <table>

                            <tr>
                                <td>Tipo</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoBoleto" runat="server" Width="210px" OnSelectedIndexChanged="ddlTipoBoleto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td>Sub-Tipo</td>
                                <td>
                                    <asp:DropDownList ID="ddlSubTipoBoleto" runat="server" Width="130px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Empresa</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" runat="server" Style="width: 30px;" onkeypress="mascara(this, soNumeros)" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    &nbsp&nbsp&nbsp&nbspMatrícula&nbsp&nbsp&nbsp&nbsp
                                    <%-- OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true" --%>
                                    <asp:TextBox ID="txtMatricula" runat="server" Style="width: 50px;" onkeypress="mascara(this, soNumeros)" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:HiddenField ID="hidNUM_IDNTF_RPTANT" runat="server" />
                                </td>
                                <td>
                                Digíto</td>
                                <td>
                                    <asp:TextBox ID="txtDigito" runat="server" Style="width: 20px;" onkeypress="mascara(this, soNumeros)" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>CPF</td>
                                <td>
                                    <asp:TextBox ID="txtCpf" runat="server" Style="width: 100px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                </td>
                                <td>Nº Lote</td>
                                <td>
                                    <asp:TextBox ID="txtLote" runat="server" Style="width: 100px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Nome</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtNome" runat="server" Style="width: 376px;"></asp:TextBox><br />
                                    <asp:Label ID="lblEnd1" runat="server" Style="width: 376px;" ></asp:Label><br />
                                    <asp:Label ID="lblEnd2" runat="server" Style="width: 376px;" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Valor</td>
                                <td>
                                    <asp:TextBox ID="txtValor" runat="server" Style="width: 107px;" onkeypress="mascara(this, moeda)" Text=""></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    Dt. Vencimento
                                    <asp:TextBox ID="txtDtVencimento" runat="server" CssClass="date" Style="width: 82px;" onkeypress="mascara(this, data)" Text=""></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDtVencimento" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator></td>
                                </td>
                            </tr>
                            <tr>
                                <td>Texto Intruções</td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtInstrucoes" runat="server" TextMode="MultiLine" Style="width: 378px; height: 80px;" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnGerar" runat="server" CssClass="button" Text="Gerar" OnClick="btnGerar_Click" />
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelar_Click" />
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblMensagemNovo" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                   </asp:Panel>

                </div>            
                </ContentTemplate>
<%--                <Triggers>                    
                <asp:PostBackTrigger ControlID="TabContainer$TbUpload$btnConsultar" />
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
</asp:Content>
