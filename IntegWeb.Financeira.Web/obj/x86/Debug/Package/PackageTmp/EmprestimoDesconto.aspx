<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="EmprestimoDesconto.aspx.cs" Inherits="IntegWeb.Financeira.Web.EmprestimoDesconto" EnableViewState="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function btnProcessar_click() {

            $('#ContentPlaceHolder1_TabContainer_TbGeracao_txtDtComplementados').show();
            $('#ContentPlaceHolder1_TabContainer_TbGeracao_txtDtSuplementados').show();
            $('#ContentPlaceHolder1_TabContainer_TbGeracao_Label3').css('display', '');
            $('#ContentPlaceHolder1_TabContainer_TbGeracao_Label4').css('display', '');

            ini_dialog_confirm('Processar Descontos', $('#ContentPlaceHolder1_TabContainer_TbGeracao_btnSubmit_Processar'));
            $('#pnl_processar').dialog('open');
            return false;
        }

        function btnGerarTxt_click() {

            $('#ContentPlaceHolder1_TabContainer_TbGeracao_txtDtComplementados').hide();
            $('#ContentPlaceHolder1_TabContainer_TbGeracao_txtDtSuplementados').hide();
            $('#ContentPlaceHolder1_TabContainer_TbGeracao_Label3').css('display', 'none');
            $('#ContentPlaceHolder1_TabContainer_TbGeracao_Label4').css('display', 'none');

            ini_dialog_confirm('Processar Descontos', $('#ContentPlaceHolder1_TabContainer_TbGeracao_btnSubmit_Gerar'));
            $('#pnl_processar').dialog('open');
            return false;
        }

    </script>
    <style>
        .tabelaPagina table th { 
            font-size:x-small
        }

        .tabelaPagina table td {
            font-size:x-small
        }
    </style>

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Desconto Empréstimo - Geração de Arquivo</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server">
                        <%--ABA 1--%>
                        <ajax:TabPanel ID="TbGeracao" HeaderText="Base de Empréstimos" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div id="divPesquisa" runat="server" class="tabelaPagina">
                                    <asp:Panel ID="pnlPesquisa" runat="server">
                                        <table>
                                            <tr>
                                                <td>Mês/Ano:</td>
                                                <td>
                                                    <asp:TextBox ID="txtMesref" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangMesGerar"
                                                        Type="Integer"
                                                        ControlToValidate="txtMesref"
                                                        MaximumValue="12"
                                                        MinimumValue="01"
                                                        ErrorMessage="Mês inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />
                                                    <b>/</b>
                                                    <asp:TextBox ID="txtAnoref" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                                    <asp:RangeValidator
                                                        runat="server"
                                                        ID="rangAnoGerar"
                                                        Type="Integer"
                                                        ControlToValidate="txtAnoref"
                                                        MaximumValue="2100"
                                                        MinimumValue="1900"
                                                        ErrorMessage="Ano inválido"
                                                        ForeColor="Red"
                                                        Display="Dynamic" />                                                    
                                                </td>
                                                <td>Status:</td>
                                                <td>
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
                                                    <asp:ListItem Text="<Todos>" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Novo" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Calculado" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Rejeitado" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Gerado TXT" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="pnlFiltro" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>Empresa:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPesqCodEmprs" runat="server" MaxLength="3" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                                </td>
                                                <td>Registro:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPesqMatricula" runat="server" MaxLength="6" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Repres:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPesqRepres" runat="server" MaxLength="6" onkeypress="mascara(this, soNumeros)" Width="80px" />
                                                </td>
                                                <td>Cpf:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPesqCpf" runat="server" MaxLength="11" onkeypress="mascara(this, soNumeros)" Width="80px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nome:</td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtPesqNome" runat="server" Width="240px" />
                                                </td>
                                            </tr>
                                        </table>
                                        </asp:Panel>
                                        <table id="tableBotoesGeracao">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                                    <asp:Button ID="btnFiltrar" runat="server" CssClass="button" Text="Filtrar" OnClick="btnFiltrar_Click" />
                                                    <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                                    <asp:Button ID="btnProcessar" runat="server" CssClass="button" Text="Processar" OnClientClick="return btnProcessar_click();" />
                                                    <asp:Button ID="btnGerarTxt" runat="server" CssClass="button" Text="Gerar TXT" OnClientClick="return btnGerarTxt_click();" />
                                                    <asp:Button ID="btnSubmit_Processar" runat="server" CssClass="button" Text="Processar" OnClick="btnProcessar_Click" Style="display: none;" />
                                                    <asp:Button ID="btnSubmit_Gerar" runat="server" CssClass="button" Text="Gerar" OnClick="btnSubmit_Gerar_Click" Style="display: none;" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMensagemInicial" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <div class="dialog-message-confirm" title="Processar Descontos" id="pnl_processar" style="display: none;">
                                                        <p>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text="Mês Ref."></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlMesRef" runat="server">
                                                                            <asp:ListItem Text="" Value="0" />
                                                                            <asp:ListItem Text="Janeiro" Value="1" />
                                                                            <asp:ListItem Text="Fevereiro" Value="2" />
                                                                            <asp:ListItem Text="Março" Value="3" />
                                                                            <asp:ListItem Text="Abril" Value="4" />
                                                                            <asp:ListItem Text="Maio" Value="5" />
                                                                            <asp:ListItem Text="Junho" Value="6" />
                                                                            <asp:ListItem Text="Julho" Value="7" />
                                                                            <asp:ListItem Text="Agosto" Value="8" />
                                                                            <asp:ListItem Text="Setembro" Value="9"/>
                                                                            <asp:ListItem Text="Outubro" Value="10" />
                                                                            <asp:ListItem Text="Novembro" Value="11" />
                                                                            <asp:ListItem Text="Dezembro" Value="12" />
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" Text="Ano Ref."></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlAnoRef" runat="server">
                                                                            <asp:ListItem Text="" Value="0" />
                                                                            <asp:ListItem Text="2015" Value="2015" />
                                                                            <asp:ListItem Text="2016" Value="2016" />
                                                                            <asp:ListItem Text="2017" Value="2017" />
                                                                            <asp:ListItem Text="2018" Value="2018" />
                                                                            <asp:ListItem Text="2019" Value="2019" />
                                                                            <asp:ListItem Text="2020" Value="2020" />
                                                                            <asp:ListItem Text="2021" Value="2021" />
                                                                            <asp:ListItem Text="2022" Value="2022" />
                                                                            <asp:ListItem Text="2023" Value="2023" />
                                                                            <asp:ListItem Text="2024" Value="2024"/>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Text="Dt. Pgto. Complementados:&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDtComplementados" runat="server" CssClass="date" Width="100px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text="Dt. Pgto. Suplementados:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDtSuplementados" runat="server" CssClass="date" Width="100px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:ObjectDataSource runat="server" ID="odsEnvio"
                                        TypeName="IntegWeb.Financeira.Aplicacao.BLL.EmprestimoDescontoBLL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>                                            
                                            <asp:ControlParameter ControlID="txtAnoref" Name="pAno_ref" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="txtMesref" Name="pMes_ref" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="txtPesqCodEmprs" Name="pEmpresa" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="txtPesqMatricula" Name="pMatricula" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="txtPesqRepres" Name="pRepresentante" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="txtPesqCpf" Name="pCpf" PropertyName="Text" Type="Int64" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="txtPesqNome" Name="pNome" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="ddlStatus" Name="pCod_Status" PropertyName="SelectedValue" Type="Int16" ConvertEmptyStringToNull="true" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdEnvio" runat="server"
                                        DataKeyNames="COD_EMPRESTIMO_DESCONTO"
                                        DataSourceID="odsEnvio"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        AllowSorting="True"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        PageSize="20"
                                        OnRowCommand="grdEnvio_RowCommand">
                                        <Columns>
                                            <asp:BoundField HeaderText="Ano" DataField="ANO_REF" SortExpression="ANO_REF" ControlStyle-Width="30px" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Mês" DataField="MES_REF" SortExpression="MES_REF" ControlStyle-Width="20px" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Perfil" DataField="DCR_TIPO" SortExpression="" ControlStyle-Width="70px" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Cpf" DataField="NUM_CPF" SortExpression="NUM_CPF" ControlStyle-Width="80px" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Emp." DataField="COD_EMPRS" SortExpression="COD_EMPRS" ControlStyle-Width="20px" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Registro" DataField="NUM_RGTRO_EMPRG" SortExpression="NUM_RGTRO_EMPRG" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Matríc." DataField="NUM_MATR_PARTF" SortExpression="NUM_MATR_PARTF" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Repres." DataField="NUM_IDNTF_RPTANT" SortExpression="NUM_IDNTF_RPTANT" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Nome" DataField="NOM_EMPRG" SortExpression="NOM_EMPRG" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="Saldo" SortExpression="VLR_DIVIDA">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblVlrSaldo" runat="server" Text='<%# Eval("VLR_DIVIDA") %>' Width="80px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div>
                                                        <asp:TextBox ID="txtVlrSaldo" runat="server" Text='<%# Eval("VLR_DIVIDA") %>' Width="80px" onkeypress="mascara(this, moeda)"></asp:TextBox>
                                                    </div>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Benefício" DataField="VLR_DESC" SortExpression="VLR_DESC" ControlStyle-Width="60px" ControlStyle-Font-Size="Small" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Desconto" DataField="VLR_LIQ" SortExpression="VLR_LIQ" ControlStyle-Width="60px" ControlStyle-Font-Size="Small" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Líquido" DataField="LIMITE" SortExpression="LIMITE" ControlStyle-Width="60px" ControlStyle-Font-Size="Small" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Parc. Empréstimo" DataField="VLR_DO_MES" SortExpression="VLR_DO_MES" ControlStyle-Width="60px" ControlStyle-Font-Size="Small" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="Vlr. Desc. Folha" SortExpression="VLR_CARGA">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblVlrDivida" runat="server" Text='<%# Eval("VLR_CARGA") %>' Width="80px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div>
                                                        <asp:TextBox ID="txtVlrDivida" runat="server" Text='<%# Eval("VLR_CARGA") %>' Width="80px" onkeypress="mascara(this, moeda)"></asp:TextBox>
                                                    </div>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="50px" HeaderStyle-Font-Size="X-Small">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:ImageButton ID="btAlterar" runat="server" Text="Alterar" CommandName="Edit" CommandArgument='<%# Eval("COD_EMPRESTIMO_DESCONTO") %>' ImageUrl="~/img/i_edit.png" Width="16px" Height="16px" AlternateText="Alterar" ToolTip="Alterar" Visible='<%# grdEnvio.EditIndex == -1 %>' />&nbsp;
                                                        <asp:ImageButton ID="btExcluir" runat="server" Text="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("COD_EMPRESTIMO_DESCONTO") %>' ImageUrl="~/img/i_delete.png" Width="16px" Height="16px" AlternateText="Excluir" ToolTip="Excluir" Visible='<%# grdEnvio.EditIndex == -1 %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');" />&nbsp;
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div>
                                                        <asp:ImageButton ID="btOk" runat="server" Text="Gravar" CommandName="Gravar" CommandArgument='<%# Eval("COD_EMPRESTIMO_DESCONTO") %>' ImageUrl="~/img/i_ok.png" Width="16px" Height="16px" AlternateText="Gravar" ToolTip="Gravar" />&nbsp;
                                                        <asp:ImageButton ID="btCancel" runat="server" Text="Cancelar" CommandName="Cancel" CommandArgument='<%# Eval("COD_EMPRESTIMO_DESCONTO") %>' ImageUrl="~/img/i_cancel.png" Width="16px" Height="16px" AlternateText="Cancelar" ToolTip="Cancelar" />&nbsp;
                                                    </div>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TbUpload" HeaderText="Importação de planilha" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <h3>Entre com o arquivo recebido</h3>
                                            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />                                            
                                        </td>
                                        <td>Mês/Ano:</td>
                                        <td>
                                            <asp:TextBox ID="txtMesref_upload" runat="server" MaxLength="2" onkeypress="mascara(this, soNumeros)" Width="50px" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangeMesref_upload"
                                                Type="Integer"
                                                ControlToValidate="txtMesref_upload"
                                                MaximumValue="12"
                                                MinimumValue="01"
                                                ErrorMessage="Mês inválido"
                                                ForeColor="Red"
                                                Display="Dynamic" />
                                            <b>/</b>
                                            <asp:TextBox ID="txtAnoref_upload" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="70px" />
                                            <asp:RangeValidator
                                                runat="server"
                                                ID="rangeAnoref_upload"
                                                Type="Integer"
                                                ControlToValidate="txtAnoref_upload"
                                                MaximumValue="2100"
                                                MinimumValue="1900"
                                                ErrorMessage="Ano inválido"
                                                ForeColor="Red"
                                                Display="Dynamic" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnUpload" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" Text="Importar arquivo" OnClick="btnUpload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="TbUpload_Mensagem" runat="server" Visible="False"></asp:Label>
<%--                                            <asp:LinkButton ID="lkYes" runat="server" OnClick="lkYes_Click" CommandArgument="" Text="SIM" Visible="false"></asp:LinkButton>--%>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$TbUpload$btnUpload" />
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
