<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="AcertoDemonstrativo.aspx.cs" Inherits="IntegWeb.Previdencia.Web.AcertoDemonstrativo" SmartNavigation ="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">

    function txtCodVerba_onBlur() {
        $('#ContentPlaceHolder1_btCarregaVerba').click();
    }

    function txtCodVerbaPatrocina_onBlur() {
        $('#ContentPlaceHolder1_btCarregaVerbaPatrocina').click();
    }

    function btExcluir_click(btn) {
        return confirm("ATENÇÃO! Tem certeza que deseja excluir este acerto do demonstrativo?");
    }

    function postbackButtonClick() {
        updateProgress = $find("<%= UpdateProg1.ClientID %>");
        window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
        return true;
    }

</script>

<%--    <div class="full_w">
        <div class="h_title">
            <img src="/img/Logo.png" alt="Funcesp">
        </div>
        <div class="MarginGrid">--%>

            <table>
                <tr><td>
                    <div class="h_title" style="width: 800px;">
                    Troca de Arquivos > Acertos no Demonstrativo
                    </div>
                </td></tr>
                <tr>
<%--                    <td>
                        <asp:Panel runat="server" ID="PanelUploadControles">
                        </asp:Panel>
                    </td>--%>
                    <td>
                        <asp:UpdatePanel runat="server" ID="upArqPatrocinadoras">
                            <ContentTemplate>

                                <asp:Panel runat="server" ID="PanelReembolsoAjustes" class="tabelaPagina" Visible="true">
                                    <table class="tabelaPagina">
                                        <tr>
                                            <td colspan="3">                                                
                                                <asp:Button ID="btnImprimirReembolso" CssClass="button" runat="server" Text="Gerar Demonstrativo" OnClick="btnImprimirReembolso_Ajuste_Click" />
                                                <asp:Button ID="btnNovoLancamento" CssClass="button" runat="server" Text="Novo Lançamento" OnClick="btnNovoLancamento_Click"/>
                                                <asp:Button ID="btnVoltar" CssClass="button" runat="server" Text="Voltar" OnClick="btnVoltar_Click" OnClientClick="return postbackButtonClick();"/>
                                                <asp:HiddenField ID="hGRUPO_PORTAL" runat="server" Value="rh_patrocinadora_down_042" />
                                                <asp:HiddenField ID="hCOD_ARQ_PATS" runat="server" Value="" />
                                                <asp:HiddenField ID="hCOD_EMPRS" runat="server" Value="" />
                                                <asp:HiddenField ID="hCOD_STATUS" runat="server" Value="" />
                                            </td>
                                            <td  colspan="5" style="text-align: center; height: 10px;">
                                                <asp:Label ID="TbUpload_Mensagem" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Mês Ref."></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMesRef" runat="server" Text="8" Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Ano Ref."></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAnoRef" runat="server" Text="2016" Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Dt. do Repasse:&nbsp;&nbsp;&nbsp;"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDtRepasse" runat="server" Text="29/04/2016" CssClass="date" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="Dt. do Crédito:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDtCredito" runat="server" Text="28/04/2016" CssClass="date" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <asp:Panel runat="server" ID="pnlNovoLancamento" Visible="false" class="tabelaPagina">
                                                <table>
                                                    <tr>
                                                        <td>Empresa</td>
                                                        <td>Matrícula</td>                                                        
                                                        <td>Verba</td>
                                                        <td>Verba Funcesp</td>
                                                        <td>Tipo</td>
                                                        <td>Valor</td>
                                                        <td>Vlr. Acerto</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:TextBox ID="txtCodEmpresa" runat="server" Text='' Width="100px" onkeypress="mascara(this, soNumeros)"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtNumMatricula" runat="server" Text='' Width="100px" onkeypress="mascara(this, soNumeros)"></asp:TextBox></td>                                                        
                                                        <td>
                                                            <asp:TextBox ID="txtCodVerbaPatrocina" runat="server" Text='' Width="100px" onfocusout="txtCodVerbaPatrocina_onBlur()"></asp:TextBox>
                                                            <asp:ImageButton ID="btCarregaVerbaPatrocina" runat="server" Text="Carregar verba" ImageUrl="~/img/i_search.png" Width="12px" Height="12px" AlternateText="Carregar Verba" ToolTip="Clique para carregar a verba" OnClick="btCarregaVerbaPatrocina_Click"/>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCodVerba" runat="server" Text='' Width="100px" onkeypress="mascara(this, soNumeros)" onfocusout="txtCodVerba_onBlur()" onkeydown="return (event.keyCode!=13);" Enabled="false"></asp:TextBox>
                                                            <!-- <asp:ImageButton ID="btCarregaVerba" runat="server" Text="Carregar verba" ImageUrl="~/img/i_search.png" Width="12px" Height="12px" AlternateText="Carregar Verba" ToolTip="Clique para carregar a verba" OnClick="btCarregaVerba_Click"/> -->&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hTipoVerba" runat="server" Value="" />
                                                            <asp:Label ID="lblTipo" runat="server" Text='' Width="40px" ReadOnly="true"></asp:Label>
                                                        </td>
                                                        <%--<td><asp:TextBox ID="txtTipo" runat="server" Text='' Width="40px" ReadOnly="true"></asp:TextBox></td>--%>
                                                        <td><asp:TextBox ID="txtVlrLancamento" runat="server" Text='' Width="100px" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                                        <%--<td><asp:TextBox ID="txtVlrAcerto" runat="server" Text='' Width="100px" onkeypress="mascara(this, moeda)"></asp:TextBox></td>--%>
                                                        <td>
                                                            <asp:ImageButton ID="btOk" runat="server" Text="Gravar" ImageUrl="~/img/i_ok.png" Width="16px" Height="16px" AlternateText="Gravar" ToolTip="Gravar" OnClick="btOk_Click" />&nbsp;
                                                            <asp:ImageButton ID="btCancel" runat="server" Text="Cancelar" ImageUrl="~/img/i_cancel.png" Width="16px" Height="16px" AlternateText="Cancelar" ToolTip="Cancelar" OnClick="btCancel_Click" />&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </tr>
                                        <tr>
                                            <td colspan="8">
                                                <asp:ObjectDataSource runat="server" ID="odsDemonstrativo"
                                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinaDemonstrativoBLL"
                                                    SelectMethod="GetData"
                                                    SelectCountMethod="GetDataCount"
                                                    EnablePaging="true"
                                                    SortParameterName="sortParameter">
<%--                                                    <UpdateParameters>
                                                        <asp:Parameter Name="COD_ARQ_PAT_DEMO"  Type="Int32" />
                                                        <asp:Parameter Name="VLR_ACERTO" Type="Decimal" />
                                                        <asp:Parameter Name="LOG_INCLUSAO" Type="String" />
                                                    </UpdateParameters>--%>
                                                    <SelectParameters>
                                                        <%--<asp:ControlParameter ControlID="hCOD_ARQ_PATS" Name="pCOD_ARQ_PATS" PropertyName="Value" Type="String" />--%>
                                                        <asp:ControlParameter ControlID="hGRUPO_PORTAL" Name="pGRUPO_PORTAL" PropertyName="Value" Type="String" />
                                                        <asp:ControlParameter ControlID="hCOD_EMPRS" Name="pCOD_EMPRS" PropertyName="Value" Type="String" />
                                                        <asp:ControlParameter ControlID="txtMesRef" Name="pMES_REF" PropertyName="Text" Type="Int16" />
                                                        <asp:ControlParameter ControlID="txtAnoRef" Name="pANO_REF" PropertyName="Text" Type="Int16" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <asp:GridView ID="grdReembolsoAjustes" runat="server"
                                                    DataKeyNames="COD_DEMO_DET"
                                                    OnRowCreated="GridView_RowCreated"
                                                    OnRowDeleted="GridView_RowDeleted"
                                                    OnRowCancelingEdit="GridView_RowCancelingEdit"
                                                    AllowPaging="true"
                                                    AllowSorting="true"
                                                    AutoGenerateColumns="False"
                                                    CssClass="Table"
                                                    PagerStyle-CssClass="GridViewPager"
                                                    ClientIDMode="Static"
                                                    PageSize="20"
                                                    DataSourceID="odsDemonstrativo"
                                                    PagerStyle-Font-Size="Medium"
                                                    PagerStyle-Font-Bold="true"
                                                    PagerSettings-PageButtonCount="4"
                                                    PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-FirstPageText="<<"
                                                    PagerSettings-LastPageText=">>"
                                                    OnRowCommand="grdReembolsoAjustes_RowCommand"
                                                    OnRowEditing="grdReembolsoAjustes_RowEditing">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Grupo Financeiro" DataField="DES_GRUPO_FINANCEIRO" SortExpression="DES_GRUPO_FINANCEIRO" ReadOnly="true"/>
                                                        <asp:BoundField HeaderText="Sub-Grupo Financeiro" DataField="DES_SUBGRUPO_FINANCEIRO" SortExpression="DES_SUBGRUPO_FINANCEIRO" ReadOnly="true"/>
                                                        <asp:TemplateField HeaderText="Empresa" SortExpression="COD_EMPRS">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblCodEmpresa" runat="server" Text='<%# Eval("COD_EMPRS") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <div>
                                                                    <asp:TextBox ID="txtCodEmpresa" runat="server" Text='<%# Eval("COD_EMPRS") %>' Width="100px" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Matricula" SortExpression="NUM_RGTRO_EMPRG">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblNumMatricula" runat="server" Text='<%# Eval("NUM_RGTRO_EMPRG") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <div>
                                                                    <asp:TextBox ID="txtNumMatricula" runat="server" Text='<%# Eval("NUM_RGTRO_EMPRG") %>' Width="100px" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo" SortExpression="TIP_CRED_DEB">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblTipo" runat="server" Text='<%# (Eval("TIP_CRED_DEB").ToString()=="C") ? "Crédito" : "Débito" %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Verba Patrocinadora" SortExpression="COD_VERBA_PATROCINA">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblCodVerbaPatrocina" runat="server" Text='<%# Eval("COD_VERBA_PATROCINA") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Verba Funcesp" SortExpression="COD_VERBA">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblCodVerba" runat="server" Text='<%# Eval("COD_VERBA") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Descrição" DataField="DCR_LANCAMENTO" SortExpression="DCR_LANCAMENTO" ReadOnly="true"/>
                                                        <asp:TemplateField HeaderText="Valor" SortExpression="VLR_LANCAMENTO">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblVlrLancamento" runat="server" Text='<%# String.Format("{0:0.00}", Eval("VLR_ACERTO_PATROCINADORA")) %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <div>
                                                                    <asp:TextBox ID="txtVlrLancamento" runat="server" Text='<%# String.Format("{0:0.00}", Eval("VLR_ACERTO_PATROCINADORA")) %>' Width="100px" onkeypress="mascara(this, moeda)"></asp:TextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
<%--                                                        <asp:TemplateField HeaderText="Acerto" SortExpression="VLR_ACERTO">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblAcerto" runat="server" Text='<%# String.Format("{0:0.00}", Eval("VLR_ACERTO")) %>'></asp:Label>                                                                    
                                                                </div>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <div>
                                                                    <asp:TextBox ID="txtVlrAcerto" runat="server" Text='<%# String.Format("{0:0.00}", Eval("VLR_ACERTO")) %>' Width="100px" onkeypress="mascara(this, moeda)"></asp:TextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="A Recolher" SortExpression="VLR_LANCAMENTO">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblValorRecolher" runat="server" Text='<%# String.Format("{0:0.00}", decimal.Parse(Eval("VLR_LANCAMENTO").ToString()) + decimal.Parse(Eval("VLR_ACERTO_PATROCINADORA").ToString())) %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:ImageButton ID="btAlterar" runat="server" Text="Alterar" CommandName="Edit" CommandArgument='<%# Eval("COD_DEMO_DET") %>' ImageUrl="~/img/i_edit.png" Width="16px" Height="16px" AlternateText="Alterar" ToolTip="Alterar" Visible='<%# (Eval("DTH_IMPORTADO")==null && grdReembolsoAjustes.EditIndex == -1) %>' />&nbsp;
                                                                    <asp:ImageButton ID="btExcluir" runat="server" Text="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("COD_DEMO_DET") %>' ImageUrl="~/img/i_delete.png" Width="16px" Height="16px" AlternateText="Excluir" ToolTip="Excluir" OnClientClick='return btExcluir_click(this);' Visible='<%# (Eval("DTH_IMPORTADO")==null && grdReembolsoAjustes.EditIndex == -1) %>' />&nbsp;
                                                                </div>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <div>
                                                                    <asp:ImageButton ID="btOk" runat="server" Text="Gravar" CommandName="Gravar" CommandArgument='<%# Eval("COD_DEMO_DET") %>' ImageUrl="~/img/i_ok.png" Width="16px" Height="16px" AlternateText="Gravar" ToolTip="Gravar" />&nbsp;
                                                                    <asp:ImageButton ID="btCancel" runat="server" Text="Cancelar" CommandName="Cancel" ImageUrl="~/img/i_cancel.png" Width="16px" Height="16px" AlternateText="Cancelar" ToolTip="Cancelar" />&nbsp;
                                                                </div>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnVoltar" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
                            <ProgressTemplate>
                                <div id="carregando">
                                    <div class="carregandoTxt">
                                        <img src="img/processando.gif" />
                                        <br />
                                        <br />
                                        <h2>Processando. Aguarde...</h2>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>

                </tr>

                

                    <ajax:ModalPopupExtender ID="modalPopUpNovoLancamentoErro" runat="server" CancelControlID="btnFechaPopUpNovoLancamentoErro" PopupControlID="pnlPopUpNovoLancamentoErro" TargetControlID ="lblPopUpMsg">

                    </ajax:ModalPopupExtender>

                    
                    <asp:Panel BorderWidth="2px" ID="pnlPopUpNovoLancamentoErro" runat="server" CssClass="formTable">
                    <div class="fancybox-error">
                        <asp:Label ID="lblPopUpMsg" runat="server" Text="Alterações no repasse mensal não pode ser efetuadas pois os lançamentos para este Mês/Ano já foram carregados." CssClass="ui-dialog-content"></asp:Label>
                        <br /><br />
                                   <div class="centeredButtonPopUp">
                                       <asp:Button ID="btnFechaPopUpNovoLancamentoErro" Text="OK" runat="server" CssClass="button" />
                                   </div>
                                 
                        
                    </div>        

                </asp:Panel>
               

            </table>
<%--        </div>
    </div>--%>
</asp:Content>
