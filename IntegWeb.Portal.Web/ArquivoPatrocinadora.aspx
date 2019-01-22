<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="ArquivoPatrocinadora.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ArquivoPatrocinadora" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        //function AutoRefresh() {
        //    //countdownwin = window.open("about:blank", "", "height=200,width=200");
        //    self.setInterval('window.location.reload(true)', 3000)
        //}

        function pergunta_sobrepor(lista) {

            var r = confirm('Os seguintes arquivos já constam na lista de uploads: ' + lista + '  Deseja sobrepor todos?');
            if (r == true) {
                $('#ContentPlaceHolder1_chkSobreporTodos').prop('checked', true);
                $('#ContentPlaceHolder1_btnProcessar').click();
            } else {
                $('#ContentPlaceHolder1_btnReset').click();
            }

        }

        function GetClickRow(lnk) {
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            //alert("RowIndex: " + rowIndex);
            return rowIndex;
        }

        function GetCheckedRows(tipo) {
            filter = ""
            if (tipo != null)
            {
                filter = "[Tip_Arquivo='" + tipo + "']";
            }
            return $('.span_checkbox input:checkbox:checked').closest('span' + filter);
        }

        function btnValidar_click() {

            var ref_out = { row_master: '0' };
            if (!Validar_Selecao(ref_out)) return false;
            Ano_ref = GetCheckedRows().eq(ref_out.row_master).attr("Ano_ref");
            Mes_ref = GetCheckedRows().eq(ref_out.row_master).attr("Mes_ref");
            Dat_Repasse = GetCheckedRows().eq(ref_out.row_master).attr("Dat_Repasse");
            Dat_Credito = GetCheckedRows().eq(ref_out.row_master).attr("Dat_Credito");
            Grupo_Portal = GetCheckedRows().eq(ref_out.row_master).attr("Grupo_Portal");
            $('#ContentPlaceHolder1_ddlAnoRef').removeAttr('disabled').val(Ano_ref);
            $('#ContentPlaceHolder1_ddlMesRef').removeAttr('disabled').val(Mes_ref);
            $('#ContentPlaceHolder1_txtDtRepasse').hide().removeAttr('disabled').val(Dat_Repasse);
            $('#ContentPlaceHolder1_txtDtCredito').hide().removeAttr('disabled').val(Dat_Credito);
            $('#ContentPlaceHolder1_ddlGrupoNovo').val(Grupo_Portal);
            $('#ContentPlaceHolder1_Label3').css('display', 'none');
            $('#ContentPlaceHolder1_Label4').css('display', 'none');
            //if (!$('#ContentPlaceHolder1_btnDemonstrativo').attr('disabled')) {
            if (GetCheckedRows(4).length > 0) {
                $('#ContentPlaceHolder1_txtDtRepasse').show();
                $('#ContentPlaceHolder1_txtDtCredito').show();
                $('#ContentPlaceHolder1_Label3').css('display', '');
                $('#ContentPlaceHolder1_Label4').css('display', '');
                //$('#ContentPlaceHolder1_txtDtRepasse').val('');
                //$('#ContentPlaceHolder1_txtDtCredito').val('');
            }

            ini_dialog_confirm('Validar arquivo(s)', $('#ContentPlaceHolder1_btnSubmit_valida'));
            $('#pnl_carga').dialog('open');
            return false;
        }

        function btnDemonstrativo_click(btn) {
            //$('.span_checkbox input:checkbox').prop('checked', false);
            //$(btn).closest('tr').find(':checkbox').prop('checked', btn);

            var ref_out = { row_master: '0' };
            if (!Validar_Selecao(ref_out)) return false;     

            if (btn != null) {
                Ano_ref = $(btn).attr("Ano_ref");
                Mes_ref = $(btn).attr("Mes_ref");
                $('#ContentPlaceHolder1_ddlMesRef').val($(btn).attr("Mes_ref"));
                //$('#ContentPlaceHolder1_ddlGrupoNovo').val($(btn).attr("Grupo_Portal"));
            } else if (GetCheckedRows().length > 0) {
                Ano_ref = GetCheckedRows().eq(ref_out.row_master).attr("Ano_ref");
                Mes_ref = GetCheckedRows().eq(ref_out.row_master).attr("Mes_ref");
                Dat_Repasse = GetCheckedRows().eq(ref_out.row_master).attr("Dat_Repasse");
                Dat_Credito = GetCheckedRows().eq(ref_out.row_master).attr("Dat_Credito");
                Grupo_Portal = GetCheckedRows().eq(ref_out.row_master).attr("Grupo_Portal");
            }

            $('#ContentPlaceHolder1_txtDtRepasse').hide();
            $('#ContentPlaceHolder1_txtDtCredito').hide();
            $('#ContentPlaceHolder1_Label3').css('display', 'none');
            $('#ContentPlaceHolder1_Label4').css('display', 'none');

            if (GetCheckedRows(4).length > 0) {
                $('#ContentPlaceHolder1_txtDtRepasse').show();
                $('#ContentPlaceHolder1_txtDtCredito').show();
                $('#ContentPlaceHolder1_Label3').css('display', '');
                $('#ContentPlaceHolder1_Label4').css('display', '');
            }

            $('#ContentPlaceHolder1_ddlAnoRef').attr('disabled', 'disabled').val(Ano_ref);
            $('#ContentPlaceHolder1_ddlMesRef').attr('disabled', 'disabled').val(Mes_ref);
            $('#ContentPlaceHolder1_txtDtRepasse').attr('disabled', 'disabled').val(Dat_Repasse);
            $('#ContentPlaceHolder1_txtDtCredito').attr('disabled', 'disabled').val(Dat_Credito);
            $('#ContentPlaceHolder1_ddlGrupoNovo').val(Grupo_Portal);

            ini_dialog_demonstrativo('Gerar Demonstrativo', $('#ContentPlaceHolder1_btnSubmit_demo'), $('#ContentPlaceHolder1_btnSubmit_ajustes'));
            $('#pnl_carga').dialog('open');
            return false;
        }

        function btnCarregar_click() {

            var ref_out = { row_master: '0' };
            if (!Validar_Selecao(ref_out)) return false;
            Ano_ref = GetCheckedRows().eq(ref_out.row_master).attr("Ano_ref");
            Mes_ref = GetCheckedRows().eq(ref_out.row_master).attr("Mes_ref");
            Dat_Repasse = GetCheckedRows().eq(ref_out.row_master).attr("Dat_Repasse");
            Dat_Credito = GetCheckedRows().eq(ref_out.row_master).attr("Dat_Credito");
            Grupo_Portal = GetCheckedRows().eq(ref_out.row_master).attr("Grupo_Portal");

            $('#ContentPlaceHolder1_txtDtRepasse').hide();
            $('#ContentPlaceHolder1_txtDtCredito').hide();
            $('#ContentPlaceHolder1_Label3').css('display', 'none');
            $('#ContentPlaceHolder1_Label4').css('display', 'none');

            if (GetCheckedRows(4).length > 0) {
                $('#ContentPlaceHolder1_txtDtRepasse').show();
                $('#ContentPlaceHolder1_txtDtCredito').show();
                $('#ContentPlaceHolder1_Label3').css('display', '');
                $('#ContentPlaceHolder1_Label4').css('display', '');
            }

            $('#ContentPlaceHolder1_ddlAnoRef').attr('disabled', 'disabled').val(Ano_ref);
            $('#ContentPlaceHolder1_ddlMesRef').attr('disabled', 'disabled').val(Mes_ref);
            $('#ContentPlaceHolder1_txtDtRepasse').attr('disabled', 'disabled').val(Dat_Repasse);
            $('#ContentPlaceHolder1_txtDtCredito').attr('disabled', 'disabled').val(Dat_Credito);
            $('#ContentPlaceHolder1_ddlGrupoNovo').val(Grupo_Portal);

            ini_dialog_confirm('Carregar arquivo(s)', $('#ContentPlaceHolder1_btnSubmit_carrega'));
            $('#pnl_carga').dialog('open');
            return false;
        }

        function btn_TEM_CERTEZA_CARREGAR_click() {
            return confirm('ATENÇÃO! Tem certeza que deseja carregar os dados dos arquivos selecionados?\nApós confirmada, esta ação não poderá ser desfeita.');
        }

        function Validar_Selecao(ref_out) {

            selecionados = GetCheckedRows();
            ref_diferente = false;

            $('#ContentPlaceHolder1_TbUpload_Mensagem').hide();
            $('#TbUpload_Mensagem_client').hide();
            
            if (selecionados.length > 0) {

                Ano_ref = '0';
                Mes_ref = '0';
                
                for (i = 0; i < selecionados.length; i++) {

                    if ((Ano_ref == '0' || Mes_ref == '0') &&
                        (selecionados.eq(i).attr("Ano_ref") != '0' || selecionados.eq(i).attr("Mes_ref") != '0')) {
                        Ano_ref = selecionados.eq(i).attr("Ano_ref");
                        Mes_ref = selecionados.eq(i).attr("Mes_ref");
                        ref_out.row_master = i;
                    }

                    if (Ano_ref != '0' && Mes_ref != '0' && selecionados.eq(i).attr("Ano_ref") != '0' && selecionados.eq(i).attr("Mes_ref") != '0' &&
                       (Ano_ref != selecionados.eq(i).attr("Ano_ref") || Mes_ref != selecionados.eq(i).attr("Mes_ref"))) {
                        ref_diferente = true;
                    }
                }
                if (ref_diferente) {
                    $('#TbUpload_Mensagem_client div p').text("Ação não permitida! Foram selecionados arquivos com ano/mês de referência diferentes")
                    $('#TbUpload_Mensagem_client').show();
                    return false;
                }
            } else {
                $('#TbUpload_Mensagem_client div p').text("Atenção! Selecione ao menos um arquivo para validar")
                $('#TbUpload_Mensagem_client').show();
                return false;
            }
            return true;
        }

        function ini_dialog_demonstrativo(dialogTitle, print_button, ajust_button) {

            // Elimina o confirm:
            if ($(".dialog-message-confirm").length > 1) {
                $(".dialog-message-confirm:last").remove();
            }

            // Recria o confirm:
            var dlgconfirm = $(".dialog-message-confirm").dialog({
                title: dialogTitle,
                modal: false,
                width: 340,
                autoOpen: false,
                buttons: {
                    Acertos: function () {
                        //postbackButtonClick();
                        $('#ContentPlaceHolder1_ddlAnoRef').removeAttr('disabled');
                        $('#ContentPlaceHolder1_ddlMesRef').removeAttr('disabled');
                        $('#ContentPlaceHolder1_txtDtRepasse').removeAttr('disabled');
                        $('#ContentPlaceHolder1_txtDtCredito').removeAttr('disabled');
                        ajust_button.click();
                        $(this).dialog("close");
                    },
                    Demonstrativo: function () {
                        print_button.click();
                        $(this).dialog("close");
                    },
                    Cancelar: function () {
                        $(this).dialog("close");
                    }
                }
            });

            dlgconfirm.parent().appendTo($("form:first"));

        }

        function chkSelect_click() {

            //Botão Validar:
            $('#ContentPlaceHolder1_btnValidar').attr('disabled', 'disabled');
            if (GetCheckedRows().attr('cod_status') < 6) {
                $('#ContentPlaceHolder1_btnValidar').removeAttr('disabled');
            }

            //Botão Demonstrativo:
            $('#ContentPlaceHolder1_btnDemonstrativo').attr('disabled', 'disabled');
            if (GetCheckedRows(4).length > 0) {
                if (GetCheckedRows().attr('mes_ref') != "0" && GetCheckedRows().attr('ano_ref') != "0" && GetCheckedRows().attr('cod_status') > 4) {
                    $('#ContentPlaceHolder1_btnDemonstrativo').removeAttr('disabled');
                }
            }
            
            //Botão Carregar:
            $('#ContentPlaceHolder1_btnCarregar').attr('disabled', 'disabled');
            if (GetCheckedRows().length > 0) {
                if (GetCheckedRows().attr('mes_ref') != "0" && GetCheckedRows().attr('ano_ref') != "0" && GetCheckedRows().attr('cod_status') > 4 && GetCheckedRows().attr('cod_status') < 7) {
                    $('#ContentPlaceHolder1_btnCarregar').removeAttr('disabled');
                }
            }

            return false;
        }

        function btExcluir_click(btn) {
            if ($(btn).attr("Num_Qtd_Importados") == '0') {
                return confirm("ATENÇÃO! Tem certeza que deseja excluir o arquivo " + $(btn).attr("Arquivo") + "?");
            } else {
                alert("ATENÇÃO! Este arquivo não pode ser excluído pois contém dados já carregados.");
                return false;
            }
        }

    </script>
<%--    <div class="full_w">
        <div class="h_title">
            <img src="/img/Logo.png" alt="Funcesp">
        </div>
        <div class="MarginGrid">--%>

            <table>
                <tr>
                    <td>
                        <div class="h_title" style="width: 600px;">
                        Troca de Arquivos > Enviar arquivos
                        </div>
                    </td>
                </tr>
                <tr>
<%--                    <td>
                        <asp:Panel runat="server" ID="PanelUploadControles">
                        </asp:Panel>
                    </td>--%>
                    <td>
                        <asp:UpdatePanel runat="server" ID="upArqPatrocinadoras">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="PanelUpload" class="tabelaPagina">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <div style="float:left;">
                                                    <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" AllowMultiple="true" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnExibir" CssClass="button" runat="server" Text="Exibir anteriores" OnCLick="btnExibir_Click" />
                                                <asp:Button ID="btnValidar" CssClass="button" runat="server" Text="Validar" OnClientClick="return btnValidar_click();" Visible="false"/>
                                                <asp:Button ID="btnDemonstrativo" CssClass="button" runat="server" Text="Demonstrativo" OnClientClick="return btnDemonstrativo_click();" Visible="false" disabled="disabled"/>
                                                <%--<asp:Button ID="btnDemonstrativo" CssClass="button" runat="server" Text="Demonstrativo" OnClick="btnDemonstrativo_Click" Visible="false" disabled="disabled"/> --%>
                                                <asp:Button ID="btnCarregar" CssClass="button" runat="server" Text="Carregar" OnClientClick="return btnCarregar_click();" Visible="false" disabled="disabled"/>
                                                <asp:Button ID="btnVoltar" CssClass="button" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                                <asp:Button ID="btnRefresh" CssClass="button" runat="server" Text="Processar" OnClick="btnRefresh_Click" Style="display: none;" />
                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Processar" OnClick="btnReset_Click" Style="display: none;" />
                                                <asp:Button ID="btnProcessar" CssClass="button" runat="server" Text="Processar" OnClick="btnProcessar_Click" Style="display: none;" />
                                                <asp:Button ID="btnSubmit_valida" CssClass="button" runat="server" Text="Validar" OnClick="btnValidar_Click" Style="display: none;" />
                                                <%--<asp:Button ID="btnSubmit_valida" CssClass="button" runat="server" Text="Validar" OnClick="btnValidar_Click" OnClientClick="postbackButtonClick();" Style="display: none;" /> --%>
                                                <asp:Button ID="btnSubmit_carrega" CssClass="button" runat="server" Text="Carregar" OnClick="btnCarregar_Click" OnClientClick="return btn_TEM_CERTEZA_CARREGAR_click();" Style="display: none;" />
                                                <asp:Button ID="btnSubmit_demo" CssClass="button" runat="server" Text="Demonstrativo" OnClick="btnImprimirDemonstrativo_Click" Style="display: none;" />
                                                <asp:Button ID="btnSubmit_ajustes" CssClass="button" runat="server" Text="Ajustes" OnClick="btnAjustes_Click" Style="display: none;" OnClientClick="return postbackButtonClick();"/>
                                            </td>
                                            <td>
                                                <div class="dialog-message-confirm" title="Carregar dados / Demonstrativo" id="pnl_carga" style="display: none;">
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
                                                                        <asp:ListItem Text="Setembro" Value="9" />
                                                                        <asp:ListItem Text="Outubro" Value="10" />
                                                                        <asp:ListItem Text="Novembro" Value="11" />
                                                                        <asp:ListItem Text="Dezembro" Value="12" />
                                                                        <asp:ListItem Text="Dezembro (13o)" Value="13" />
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
                                                                        <asp:ListItem Text="2016" Value="2016" />
                                                                        <asp:ListItem Text="2017" Value="2017" />
                                                                        <asp:ListItem Text="2018" Value="2018" />
                                                                        <asp:ListItem Text="2019" Value="2019" />
                                                                        <asp:ListItem Text="2020" Value="2020" />
                                                                        <asp:ListItem Text="2021" Value="2021" />
                                                                        <asp:ListItem Text="2022" Value="2022" />
                                                                        <asp:ListItem Text="2023" Value="2023" />
                                                                        <asp:ListItem Text="2024" Value="2024" />
                                                                        <asp:ListItem Text="2025" Value="2025" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text="Dt. do Repasse:&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDtRepasse" runat="server" Text="" CssClass="date" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text="Dt. do Crédito:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDtCredito" runat="server" Text="" CssClass="date" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text="Grupo:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                     <asp:DropDownList ID="ddlGrupoNovo" runat="server" Width="220px">
                                                                        
                                                                     </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </p>
                                                </div>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td colspan="2" rowspan="2" style="text-align: center; height: 10px;">
                                                <asp:Label ID="TbUpload_Mensagem" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>

                                                <span id="TbUpload_Mensagem_client" class="pnlMensagem" style="display: none;">
                                                    <div class="n_warning"><p>&nbsp;</p>
                                                    </div>
                                                </span>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="pnlMensagem">
                                                <div style="font-size:small; display:none;">
                                                    &nbsp&nbsp<asp:CheckBox ID="chkSobreporTodos" runat="server" Checked="false" Text="Sobrepor arquivos idênticos" />
                                                </div>
                                            </td>
                                            <td colspan="3">
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr class="pnlMensagem">
                                            <td>
                                                <h3>Exibir:</h3>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rExibir" RepeatDirection="Horizontal" AutoPostBack="true" Style="margin: 0px; border-color: #f9d9d9; border-style: solid;" OnSelectedIndexChanged="rExibir_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Recentes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Com críticas"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <%--<h3>Agrupar por:</h3>--%>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rAgrupa" RepeatDirection="Horizontal" AutoPostBack="true" Style="margin: 0px; border-color: #f9d9d9; border-style: solid;" OnSelectedIndexChanged="rAgrupa_SelectedIndexChanged" Visible="false">
                                                    <asp:ListItem Value="1" Text="Arquivo" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Empresa"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:HiddenField runat="server" ID="hidGrupos" />
                                                <asp:HiddenField runat="server" ID="hidCodArquivos" />
                                                <asp:HiddenField runat="server" ID="hidCodEmprs" />
                                                <asp:HiddenField runat="server" ID="hidCodStatus" />
                                                <asp:HiddenField runat="server" ID="hidParamAcertos" />
                                            </td>
                                            <td>
                                                <h3>&nbsp</h3>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="ddlGrupo" runat="server" Visible="false">
                                                    <asp:ListItem Text="<TODOS>" Value="9999" />
                                                 </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:ObjectDataSource runat="server" ID="odsArqPatrocinadora"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraBLL"
                                        SelectMethod="GetGroupBy"
                                        SelectCountMethod="GetDataCountGroupBy"
                                        EnablePaging="true"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hidGrupos" Name="pGruposAcesso" PropertyName="Value"
                                                Type="String" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="rExibir" Name="pExibir" PropertyName="SelectedValue"
                                                Type="Int32" ConvertEmptyStringToNull="true" />                                            
                                            <asp:ControlParameter ControlID="rAgrupa" Name="pAgrupar" PropertyName="SelectedValue"
                                                Type="Int32" ConvertEmptyStringToNull="true" />
                                            <asp:ControlParameter ControlID="ddlGrupo" Name="pGrupo" PropertyName="SelectedValue"
                                                Type="String" ConvertEmptyStringToNull="true" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdArqEnviados" runat="server"  Visible="false"
                                        DataKeyNames="COD_ARQ_PAT"
                                        OnRowCreated="GridView_RowCreated"
                                        OnRowDeleted="GridView_RowDeleted"
                                        OnRowCancelingEdit="GridView_RowCancelingEdit"
                                        OnRowCommand="grdArqEnviados_RowCommand"
                                        OnRowDataBound="grdArqEnviados_RowDataBound"
                                        AllowPaging="true"
                                        AllowSorting="true"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="A consulta não retornou registros"
                                        CssClass="Table"
                                        PagerStyle-CssClass="GridViewPager"
                                        ClientIDMode="Static"
                                        PageSize="8"
                                        DataSourceID="odsArqPatrocinadora"
                                        PagerStyle-Font-Size="Medium"
                                        PagerStyle-Font-Bold="true"
                                        PagerSettings-PageButtonCount="4"
                                        PagerSettings-Mode="NumericFirstLast"
                                        PagerSettings-FirstPageText="<<"
                                        PagerSettings-LastPageText=">>">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" onchange="return chkSelect_click();" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text='' class="span_checkbox" onchange="return chkSelect_click();" 
                                                        Cod_Status='<%# Eval("COD_STATUS").ToString() %>' 
                                                        Tip_Arquivo='<%# Eval("TIP_ARQUIVO").ToString() %>' 
                                                        Ano_ref='<%# Eval("ANO_REF") %>' 
                                                        Mes_ref='<%# Eval("MES_REF") %>' 
                                                        Dat_Repasse='<%# Eval("DAT_REPASSE", "{0:dd/MM/yyyy}") %>' 
                                                        Dat_Credito='<%# Eval("DAT_CREDITO", "{0:dd/MM/yyyy}") %>' 
                                                        Cod_Emprs='<%# Eval("COD_EMPRS") %>' 
                                                        Grupo_Portal='<%# Eval("GRUPO_PORTAL") %>'
                                                    />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Data Envio" SortExpression="DTH_INCLUSAO">
                                                <ItemTemplate>
                                                    <asp:Label ID="Dt_Envio" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DTH_INCLUSAO")) %>' ToolTip='<%# Eval("DTH_INCLUSAO").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dt. Ref." SortExpression="MES_REF">
                                                <ItemTemplate>
                                                    <asp:Label ID="Dt_Ref" runat="server"  Text='<%# Eval("MES_REF").ToString().PadLeft(2, char.Parse("0")) + "/" + Eval("ANO_REF").ToString() %>' Visible='<%# (int.Parse(Eval("COD_STATUS").ToString()) > 4) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Arquivo" DataField="NOM_ARQUIVO" SortExpression="NOM_ARQUIVO" />
                                            <asp:TemplateField HeaderText="Emps.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Cod_Emprs" runat="server" Text='<%# Eval("COD_EMPRS").ToString() %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="DCR_TIPO" SortExpression="DCR_TIPO" />
                                            <asp:BoundField HeaderText="Linhas" DataField="NUM_QTD_REGISTROS" SortExpression="NUM_QTD_REGISTROS" />
<%--                                            <asp:TemplateField HeaderText="Qtd. Válidos" SortExpression="NUM_QTD_VALIDOS">
                                                <ItemTemplate>
                                                    <asp:Label ID="Qtd_Validos" runat="server"  Text='<%# Eval("NUM_QTD_VALIDOS").ToString() %>' Visible='<%# (int.Parse(Eval("COD_STATUS").ToString()) > 4) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Erros" SortExpression="NUM_QTD_ERROS">
                                                <ItemTemplate>
                                                    <asp:Label ID="Qtd_Erros" runat="server" Text='0' Visible='<%# (int.Parse(Eval("NUM_QTD_ERROS").ToString())==0) && (int.Parse(Eval("COD_STATUS").ToString()) > 4) %>'></asp:Label>
                                                    <asp:LinkButton ID="Qtd_Erros2" runat="server" Text='<%# Eval("NUM_QTD_ERROS").ToString() %>' Visible='<%# (int.Parse(Eval("NUM_QTD_ERROS").ToString())>0) %>' CommandName="Criticas" CommandArgument='<%# Eval("COD_ARQ_PAT").ToString() + "," + Eval("TIP_ARQUIVO").ToString() + ",1" %>' ForeColor="Red" Font-Bold="true" Font-Underline="true"></asp:LinkButton>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>         
                                            <asp:TemplateField HeaderText="Alertas" SortExpression="NUM_QTD_ALERTAS">
                                                <ItemTemplate>
                                                    <asp:Label ID="Qtd_Alertas" runat="server" Text='0' Visible='<%# (int.Parse(Eval("NUM_QTD_ALERTAS").ToString())==0) && (int.Parse(Eval("COD_STATUS").ToString()) > 4) %>'></asp:Label>                                          
                                                    <asp:LinkButton ID="Qtd_Alertas2" runat="server" Text='<%# Eval("NUM_QTD_ALERTAS").ToString() %>' Visible='<%# (int.Parse(Eval("NUM_QTD_ALERTAS").ToString())>0) %>' CommandName="Criticas" CommandArgument='<%# Eval("COD_ARQ_PAT").ToString() + "," + Eval("TIP_ARQUIVO").ToString() + ",2" %>' ForeColor="Red" Font-Bold="true" Font-Underline="true"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                    
                                            <asp:TemplateField HeaderText="Carregados" SortExpression="NUM_QTD_IMPORTADOS">
                                                <ItemTemplate>
                                                    <asp:Label ID="Qtd_Carregados" runat="server"  Text='<%# Eval("NUM_QTD_IMPORTADOS").ToString() %>' Visible='<%# (int.Parse(Eval("COD_STATUS").ToString()) > 7) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Status" DataField="DCR_STATUS" SortExpression="DCR_STATUS" />--%>
                                            <asp:TemplateField HeaderText="Status" SortExpression="DCR_STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="Status" runat="server"  Text='<%# Eval("DCR_STATUS").ToString() %>' ></asp:Label>
                                                    <%--<asp:Button ID="btCancelarProcessamento" CssClass="button" runat="server" Text="Cancelar" OnClientClick='return confirm("ATENÇÃO! Tem certeza que deseja cancelar o processamento do arquivo " + $(this).attr("Arquivo") + "?");' Arquivo='<%# Eval("NOM_ARQUIVO") %>' CommandName="CancelarProcessamento" CommandArgument='<%# Eval("COD_ARQ_PAT").ToString() + "," + Eval("COD_STATUS").ToString() %>' Width="54px" Height="18px" AlternateText="Cancelar" ToolTip="Cancelar processamento"  Font-Size="XX-Small" Visible='<%# (int.Parse(Eval("COD_STATUS").ToString()) == 7) || (int.Parse(Eval("COD_STATUS").ToString()) == 4) %>' Style="padding: 0px;"/>&nbsp;--%>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("NUM_PERC_PROCESSADOS").ToString() + "%" %>' ForeColor="Blue" Font-Bold="true" Font-Siza="10px" Visible='<%# (int.Parse(Eval("COD_STATUS").ToString()) == 7) || (int.Parse(Eval("COD_STATUS").ToString()) == 4) %>' ></asp:Label>
                                                    &nbsp;&nbsp;
                                                    <asp:Image ID="imgStatus" runat="server" ImageUrl="~/img/load.gif" Visible='<%# (int.Parse(Eval("COD_STATUS").ToString()) == 7) || (int.Parse(Eval("COD_STATUS").ToString()) == 4) %>' Style="padding: 0px;" />                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Grupo Portal" DataField="GRUPO_PORTAL" SortExpression="GRUPO_PORTAL" />                                            
                                            <asp:TemplateField HeaderText="Excluir Arq.">
                                                <ItemTemplate>
                                                    <div class="dialog-message-popup" title="Históricos de ações">
                                                        <p>
                                                            <asp:Label ID="lstAcoes" runat="server" Text="Não há ações registradas."></asp:Label>
                                                        </p>
                                                    </div>
                                                    <asp:ImageButton ID="btExcluir" CssClass="button" runat="server" Text="Excluir" OnClientClick='return btExcluir_click(this);' Arquivo='<%# Eval("NOM_ARQUIVO") %>' Num_Qtd_Importados='<%# Eval("NUM_QTD_IMPORTADOS") %>' CommandName="Excluir" CommandArgument='<%# Eval("COD_ARQ_PAT") %>' ImageUrl="~/img/delete.png" Width="16px" Height="16px" AlternateText="Excluir" ToolTip="Excluir" />&nbsp;
                                                    <%--<asp:ImageButton ID="btHistorico" CssClass="button" runat="server" Text="Histórico" OnClientClick='$(".dialog-message-popup").eq(GetClickRow(this)).dialog("open"); return false;' ImageUrl="~/img/history.png" Width="16px" Height="16px" ToolTip="Histórico de ações" />&nbsp;
                                                    <asp:ImageButton ID="btDemonstrativo" CssClass="button btDemo" runat="server" Text="Demonstrativo" OnClientClick="return btnDemonstrativo_click(this);" Ano_ref='<%# Eval("ANO_REF") %>' Mes_ref='<%# Eval("MES_REF") %>' Grupo_Portal='<%# Eval("GRUPO_PORTAL") %>' CommandName="Demonstrativo" CommandArgument='<%# Eval("COD_ARQ_PAT") %>' ImageUrl="~/img/financial.png" Width="16px" Height="16px" Visible='<%# (short.Parse(Eval("TIP_ARQUIVO").ToString())==4) %>' ToolTip="Demonstrativo de Repasse" />--%>&nbsp;
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:GridView ID="grdArqEnviadosPorEmpresa" runat="server" Visible="false"
                                        DataKeyNames="COD_EMPRS"
                                        OnRowCreated="GridView_RowCreated"
                                        OnRowDeleted="GridView_RowDeleted"
                                        OnRowCancelingEdit="GridView_RowCancelingEdit"
                                        AllowPaging="true"
                                        AllowSorting="true"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="A consulta não retornou registros"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        PageSize="9"
                                        DataSourceID="odsArqPatrocinadora"
                                        PagerStyle-Font-Size="Medium"
                                        PagerStyle-Font-Bold="true"
                                        PagerSettings-PageButtonCount="4"
                                        PagerSettings-Mode="NumericFirstLast"
                                        PagerSettings-FirstPageText="<<"
                                        PagerSettings-LastPageText=">>">
                                        <Columns>
                                            <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" SortExpression="COD_EMPRS" />
                                            <asp:BoundField HeaderText="Arquivo" DataField="NOM_ARQUIVO" SortExpression="NOM_ARQUIVO" />
                                            <asp:BoundField HeaderText="Dt. Envio" DataField="DTH_INCLUSAO" SortExpression="DTH_INCLUSAO" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="Qtd. Linhas" DataField="NUM_QTD_REGISTROS" SortExpression="NUM_QTD_REGISTROS" />
                                        </Columns>
                                    </asp:GridView>

                                </asp:Panel>

                                <asp:Panel runat="server" ID="PanelCriticas" class="tabelaPagina" Visible="false">
                                    <table>
                                        <tr>
                                            <td style="min-width: 180px;">
                                                <asp:Button ID="btnImprimirCriticas" CssClass="button" runat="server" Text="Imprimir" OnClick="btnImprimirCriticas_Click" />
                                                <asp:Button ID="btnVoltarCriticas" CssClass="button" runat="server" Text="Voltar" OnClick="btnVoltarCriticas_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="display: -webkit-inline-box; width: 130px;">
                                                <h3>Agrupar Críticas:</h3>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rAgrupaCritica" RepeatDirection="Horizontal" AutoPostBack="true" Style="margin: 0px; border-color: #f9d9d9; border-style: solid;" OnSelectedIndexChanged="rAgrupaCritica_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Text="Todas" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Por Linha"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:HiddenField ID="hidCOD_ARQ_PAT_CRITICA" runat="server" Value=""></asp:HiddenField>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="display: -webkit-inline-box; width: 130px;">
                                                <h3>Exibir Críticas:</h3>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rTIP_CRITICA" RepeatDirection="Horizontal" AutoPostBack="true" Style="margin: 0px; border-color: #f9d9d9; border-style: solid;" OnSelectedIndexChanged="rTIP_CRITICA_SelectedIndexChanged">
                                                    <asp:ListItem Value="" Text="Todas" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Erros"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alertas"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>

                                                <asp:ObjectDataSource runat="server" ID="odsCriticasTodas"
                                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraBLL"
                                                    SelectMethod="CRITICA_GetAllData"
                                                    SelectCountMethod="CRITICA_GetAll_Count"
                                                    EnablePaging="true"
                                                    SortParameterName="sortParameter">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hidCOD_ARQ_PAT_CRITICA" Name="pCOD_ARQ_PAT" PropertyName="Value"
                                                            Type="Int32" ConvertEmptyStringToNull="true" />
                                                        <asp:ControlParameter ControlID="rTIP_CRITICA" Name="pTIP_CRITICA" PropertyName="SelectedValue"
                                                            Type="Int16" ConvertEmptyStringToNull="true" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <asp:GridView ID="grdCriticasTodas" runat="server"
                                                    DataKeyNames="COD_ARQ_PAT_CRITICA"
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
                                                    DataSourceID="odsCriticasTodas"
                                                    RowStyle-Height="28px"
                                                    PagerStyle-Font-Size="Medium"
                                                    PagerStyle-Font-Bold="true"
                                                    PagerSettings-PageButtonCount="4"
                                                    PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-FirstPageText="<<"
                                                    PagerSettings-LastPageText=">>">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Tipo" SortExpression="TIP_CRITICA">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgwarning" ImageUrl="~\img\i_warning.png" runat="server" Visible='<%# (bool)Eval("ALERTA")  %>'/>
                                                                <asp:Image ID="imgerror" ImageUrl="~\img\i_error.png" runat="server" Visible='<%# !(bool)Eval("ALERTA") %>'/>                                                                
                                                                <asp:Label ID="lTIPO" runat="server" Text='<%# Eval("DCR_TIP_CRITICA")  %>' ForeColor='<%# ((bool)Eval("ALERTA")) ? System.Drawing.Color.FromArgb(56,56,56) : System.Drawing.Color.Red  %>' Font-Bold="true"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crítica" SortExpression="COD_CRITICA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCRITICA" runat="server" Text='<%# Eval("DCR_CRITICA")  %>' ForeColor='<%# ((bool)Eval("ALERTA")) ? System.Drawing.Color.FromArgb(56,56,56) : System.Drawing.Color.Red  %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                 
                                                        <asp:BoundField HeaderText="Linha" DataField="NUM_LINHA" SortExpression="NUM_LINHA" />                                                        
                                                        <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" SortExpression="COD_EMPRS" />
                                                        <asp:BoundField HeaderText="Registro" DataField="NUM_RGTRO_EMPRG" SortExpression="NUM_RGTRO_EMPRG" />
                                                        <asp:BoundField HeaderText="Verba" DataField="COD_VERBA" SortExpression="COD_VERBA" />                                                        
                                                    </Columns>
                                                </asp:GridView>

                                                <asp:ObjectDataSource runat="server" ID="odsCriticas"
                                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraBLL"
                                                    SelectMethod="CRITICA_GetData"
                                                    SelectCountMethod="CRITICA_GetDataCountGroup"
                                                    EnablePaging="true"
                                                    SortParameterName="sortParameter">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hidCOD_ARQ_PAT_CRITICA" Name="pCOD_ARQ_PAT" PropertyName="Value"
                                                            Type="Int32" ConvertEmptyStringToNull="true" />
                                                        <asp:ControlParameter ControlID="rTIP_CRITICA" Name="pTIP_CRITICA" PropertyName="SelectedValue"
                                                            Type="Int16" ConvertEmptyStringToNull="true" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <asp:GridView ID="grdCriticas" runat="server"
                                                    DataKeyNames="COD_ARQ_PAT_LINHA"
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
                                                    DataSourceID="odsCriticas"
                                                    RowStyle-Height="28px"
                                                    PagerStyle-Font-Size="Medium"
                                                    PagerStyle-Font-Bold="true"
                                                    PagerSettings-PageButtonCount="4"
                                                    PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-FirstPageText="<<"
                                                    PagerSettings-LastPageText=">>"
                                                    Visible="false">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Código" DataField="COD_CRITICA" SortExpression="COD_CRITICA" />
                                                        <asp:BoundField HeaderText="Campo" DataField="NOM_CAMPO" SortExpression="NOM_CAMPO" />
                                                        <asp:TemplateField HeaderText="Tipo" SortExpression="TIP_CRITICA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lTIPO" runat="server" Text='<%# (Eval("TIP_CRITICA").ToString().Equals("2")) ? "Alerta" : "ERRO"  %>' ForeColor='<%# (Eval("TIP_CRITICA").ToString().Equals("2")) ? System.Drawing.Color.Black : System.Drawing.Color.Red  %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Crítica" DataField="DCR_CRITICA" SortExpression="DCR_CRITICA" ItemStyle-ForeColor="Red" />
                                                    </Columns>
                                                </asp:GridView>

                                                <asp:ObjectDataSource runat="server" ID="odsCriticasLinhas"
                                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraBLL"
                                                    SelectMethod="LINHA_GetData"
                                                    SelectCountMethod="LINHA_GetDataCountGroup"
                                                    EnablePaging="true"
                                                    SortParameterName="sortParameter">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hidCOD_ARQ_PAT_CRITICA" Name="pCOD_ARQ_PAT" PropertyName="Value"
                                                            Type="Int32" ConvertEmptyStringToNull="true" />
                                                        <asp:ControlParameter ControlID="rTIP_CRITICA" Name="pTIP_CRITICA" PropertyName="SelectedValue"
                                                            Type="Int16" ConvertEmptyStringToNull="true" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <asp:GridView ID="grdCriticasLinhas" runat="server"
                                                    DataKeyNames="COD_ARQ_PAT_LINHA"
                                                    OnRowCreated="GridView_RowCreated"
                                                    OnRowDeleted="GridView_RowDeleted"
                                                    OnRowCancelingEdit="GridView_RowCancelingEdit"
                                                    OnRowDataBound="grdCriticasLinhas_RowDataBound"
                                                    AllowPaging="true"
                                                    AllowSorting="true"
                                                    AutoGenerateColumns="False"
                                                    CssClass="Table"
                                                    PagerStyle-CssClass="GridViewPager"
                                                    ClientIDMode="Static"
                                                    PageSize="20"
                                                    DataSourceID="odsCriticasLinhas"
                                                    RowStyle-Height="28px"
                                                    PagerStyle-Font-Size="Medium"
                                                    PagerStyle-Font-Bold="true"
                                                    PagerSettings-PageButtonCount="4"
                                                    PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-FirstPageText="<<"
                                                    PagerSettings-LastPageText=">>"
                                                    Visible="false">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Linha" DataField="NUM_LINHA" SortExpression="NUM_LINHA" />
                                                        <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" SortExpression="COD_EMPRS" />
                                                        <asp:BoundField HeaderText="Registro" DataField="NUM_RGTRO_EMPRG" SortExpression="NUM_RGTRO_EMPRG" />
                                                        <%--<asp:BoundField HeaderText="Dados" DataField="DADOS" SortExpression="DADOS" />--%>
                                                        <asp:TemplateField HeaderText="Dados" SortExpression="DADOS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lDADOS" runat="server" Text='<%# Eval("DADOS").ToString().Substring(0,10) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crítica(s)">
                                                            <ItemTemplate>
                                                                <div class="accordion" style='display: <%# (int.Parse(Eval("PRE_TBL_ARQ_PATROCINA_CRITICA.Count").ToString()) > 1 ? "block" : "none") %>'>
                                                                    <h3 style="color: red">Criticas encontradas</h3>
                                                                    <div>
                                                                        <asp:Label ID="lstCriticas" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div style='display: <%# (int.Parse(Eval("PRE_TBL_ARQ_PATROCINA_CRITICA.Count").ToString()) == 1 ? "block" : "none") %>'>
                                                                    <asp:Label ID="lblCritica" runat="server" ForeColor="Red"></asp:Label>
                                                                </div>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                </asp:Panel>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit_ajustes" />
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

                <script type="text/javascript">


                    var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
                    // Firefox 1.0+
                    var isFirefox = typeof InstallTrigger !== 'undefined';
                    // At least Safari 3+: "[object HTMLElementConstructor]"
                    var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
                    // Internet Explorer 6-11
                    var isIE = /*@cc_on!@*/false || !!document.documentMode;
                    // Edge 20+
                    var isEdge = !isIE && !!window.StyleMedia;
                    // Chrome 1+
                    var isChrome = !!window.chrome && !!window.chrome.webstore;
                    // Blink engine detection
                    var isBlink = (isChrome || isOpera) && !!window.CSS;

                    var updateProgress = null;

                    function postbackButtonClick() {
                        updateProgress = $find("<%= UpdateProg1.ClientID %>");
                        window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
                        return true;
                    }

                    function sendFile(file) {

                        var formData = new FormData();
                        formData.append('file', $('#ContentPlaceHolder1_FileUploadControl')[0].files[0]);
                        $.ajax({
                            type: 'post',
                            url: 'ArquivoPatrocinadora.ashx',
                            data: formData,
                            success: function (status) {
                                if (status != 'error') {
                                    var my_path = "MediaUploader/" + status;
                                    $("#myUploadedImg").attr("src", my_path);
                                }
                            },
                            processData: false,
                            contentType: false,
                            error: function () {
                                alert("Whoops something went wrong!");
                            }
                        });
                    }

                    function build_uploadify() {

                        $('#ContentPlaceHolder1_FileUploadControl').uploadify({
                            'swf': 'js/uploadify/uploadify.swf',
                            'uploader': 'ArquivoPatrocinadora.ashx',
                            'fileDataName': 'file',
                            'buttonText': 'Enviar Arquivos',
                            'multi': true,
                            'sizeLimit': 1048576,
                            'simUploadLimit': 2,
                            'auto': true,
                            'removeCompleted': false,
                            'force_replace': true,
                            'width': 140,
                            'height': 16,
                            'buttonImage': 'img/upload.png',
                            'wmode': 'transparent',
                            'queueID': true, //Esconde lista de Uploads
                            'onUploadError': function (file, errorCode, errorMsg, errorString) {
                                alert('The file ' + file.name + ' could not be uploaded: ' + errorString);
                            },
                            'onQueueComplete': function (queueData) {
                                //alert(queueData.uploadsSuccessful);
                                //postbackButtonClick();
                                $('#ContentPlaceHolder1_btnProcessar').click();
                            },
                            'onSelect': function (file) {
                            },
                            'onCheck': function (file, exists) {
                                if (exists) {
                                    alert('upload failed because the file is a duplicate');
                                }
                            }
                        });

                        $('#SWFUpload_0').attr('width', '166');
                        $('#SWFUpload_0').attr('height', '32');
                    }

                    //chkSelect_click();

                        ////} else if (isChrome) {
                        //    //var _URL = window.URL || window.webkitURL;
                        //    $("#ContentPlaceHolder1_FileUploadControl").on('change', function () {

                        //        if (isChrome) {

                        //            var file, img;
                        //            if ((file = this.files[0])) {
                        //                //img = new File();
                        //                //img.onload = function () {
                        //                debugger
                        //                sendFile(file);
                        //                //};
                        //                //img.onerror = function (e, data) {
                        //                //    debugger
                        //                //    alert("Not a valid file:" + file.type + " - ex: " + e);
                        //                //};
                        //                //img.src = _URL.createObjectURL(file);
                        //            }
                        //        } else if (isIE) {
                        //            sendFileIE();
                        //        }
                        //    });
                        //}       

                </script>
            </table>
<%--        </div>
    </div>--%>
</asp:Content>
