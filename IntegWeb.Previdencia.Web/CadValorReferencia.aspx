<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadValorReferencia.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CadValorReferencia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tabelaPagina input[type="text"] {
            width: 100%;
        }

        .tabelaPagina select {
            width: 100%;
        }
    </style>
    <script type="text/javascript">

        function ddlSituacao_onchange() {
            $('#ContentPlaceHolder1_lblEfetivacao').hide();
            $('#ContentPlaceHolder1_txtEfetivacao').hide();
            if ($('#ContentPlaceHolder1_ddlSituacao').val() == 'VR') {
                $('#ContentPlaceHolder1_lblEfetivacao').show();
                $('#ContentPlaceHolder1_txtEfetivacao').show();
            };
        }


    </script>

    <script type="text/javascript">

        function str2float(element_Id) {
            if (document.getElementById(element_Id).value == "" || document.getElementById(element_Id).value == "0")
            {
                return 0
            } else {
                return parseFloat(document.getElementById(element_Id).value.replace(/[.]/g, "").replace(",", "."));
            }
        }

        function CalculoPart() {            

            var cntr_part_at_bsps = str2float("cntr_part_at_bsps");
            var bnf_part_ret_bsps = str2float("bnf_part_ret_bsps");
            var cntr_part_ret_bsps = str2float("cntr_part_ret_bsps");
            var resmat_part_bsps = str2float("resmat_part_bsps");
            var resmat_ant_part_bsps = str2float("resmat_ant_part_bsps");
            var cntr_part_at_bd = str2float("cntr_part_at_bd");
            var bnf_part_ret_bd = str2float("bnf_part_ret_bd");
            var cntr_part_ret_bd = str2float("cntr_part_ret_bd");
            var resmat_part_bd = str2float("resmat_part_bd");
            var cntr_part_at_cv = str2float("cntr_part_at_cv");
            var bnf_part_ret_cv = str2float("bnf_part_ret_cv");

            var TotalParticipante = bnf_part_ret_bsps + bnf_part_ret_bd - cntr_part_at_bsps - cntr_part_ret_bsps
                                    - resmat_part_bsps - resmat_ant_part_bsps - cntr_part_at_bd - cntr_part_ret_bd
                                    - resmat_part_bd - cntr_part_at_cv - bnf_part_ret_cv;

            if (isNaN(TotalParticipante)) {
                return document.getElementById("txtTotalParticipante").value = "0";
            }
            else {
                document.getElementById("txtTotalParticipante").value = TotalParticipante.toFixed(2).replace(".", ",");
            }
        }

        function ValidaBeneficio() {


            if (document.getElementById("resmat_part_bd").value == "" || document.getElementById("resmat_part_bd").value == "0") {
                document.getElementById("prc_part_resmat_bd").value = "0";
            }
            else {
                document.getElementById("prc_part_resmat_bd").value = "50";
            }
            if (document.getElementById("resmat_patr_bd").value == "" || document.getElementById("resmat_patr_bd").value == "0") {
                document.getElementById("prc_patr_resmat_bd").value = "0";
            }
            else {
                document.getElementById("prc_patr_resmat_bd").value = "50";
            }
        }

        function CalculoPatr() {            

            var cntr_patr_at_bsps = str2float("cntr_patr_at_bsps");
            var bnf_patr_ret_bsps = str2float("bnf_patr_ret_bsps");
            //var cntr_patr_ret_bsps = str2float("cntr_patr_ret_bsps");
            var resmat_patr_bsps = str2float("resmat_patr_bsps");
            var resmat_ant_patr_bsps = str2float("resmat_ant_patr_bsps");
            var cntr_patr_at_bd = str2float("cntr_patr_at_bd");
            var bnf_patr_ret_bd = str2float("bnf_patr_ret_bd");
            //var cntr_patr_ret_bd = str2float("cntr_patr_ret_bd");
            var resmat_patr_bd = str2float("resmat_patr_bd");
            var cntr_patr_at_cv = str2float("cntr_patr_at_cv");
            var bnf_patr_ret_cv = str2float("bnf_patr_ret_cv");

            var TotalPatrocinador = cntr_patr_at_bsps + bnf_patr_ret_bsps + resmat_patr_bsps + resmat_ant_patr_bsps + cntr_patr_at_bd +
                                    bnf_patr_ret_bd + resmat_patr_bd + cntr_patr_at_cv + bnf_patr_ret_cv;

            if (isNaN(TotalPatrocinador)) {
                return document.getElementById("txtTotalPatrocinador").value = "0";
            }
            else {
                document.getElementById("txtTotalPatrocinador").value = TotalPatrocinador.toFixed(2).replace(".", ",");
            }
        }

    </script>


    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Manutenção Valor de Referência<asp:Label ID="SubTituloTela" runat="server" Text=" - Importar processo" Visible="false"></asp:Label></h1>
        <asp:UpdatePanel runat="server" ID="upValorReferencia">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlSelectVr" runat="server">
                    <table>
                        <tr>
                            <td>Empresa</td>
                            <td>
                                <asp:TextBox ID="txtCodEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Matrícula</td>
                            <td>
                                <asp:TextBox ID="txtCodMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="drpPesquisa" runat="server">
                                    <asp:ListItem Text="Participante" Value="3" Selected="True" />
                                    <asp:ListItem Text="CPF" Value="4" />
                                    <asp:ListItem Text="Núm. Processo" Value="5" />
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtPesquisa" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar   " OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo Cadastro" OnClick="btnNovo_Click" />
                                <asp:Button ID="btnImportarVr" runat="server" CssClass="button" Text="Importar VR" OnClick="btnImportarVr_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlGridVr" class="tabelaPagina" Visible="false">
                    <asp:ObjectDataSource runat="server" ID="odsProcessosVr"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial.ValorReferenciaBLL"
                        SelectMethod="ListarProcessosVr"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpPesquisa" Name="filType" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="txtPesquisa" Name="filValue" PropertyName="Text"
                                Type="String" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtCodEmpresa" Name="codEmpresa" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtCodMatricula" Name="codMatricula" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdValorReferencia" runat="server"
                        OnRowCreated="GridView_RowCreated"
                        OnRowCommand="grdValorReferencia_RowCommand"
                        AllowPaging="true"
                        AllowSorting="true"
                        AutoGenerateColumns="False"
                        EmptyDataText="A consulta não retornou registros"
                        CssClass="Table"
                        ClientIDMode="Static"
                        PageSize="8"
                        DataSourceID="odsProcessosVr">
                        <%--OnRowCancelingEdit="grdCCusto_RowCancelingEdit" 
                        OnRowEditing="grdCCusto_RowEditing" 
                        OnRowUpdating="grdCCusto_RowUpdating"--%>
                        <Columns>
                            <asp:BoundField HeaderText="Empresa" DataField="cod_emprs" SortExpression="cod_emprs" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Matrícula" DataField="num_rgtro_emprg" SortExpression="num_rgtro_emprg" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nº Processo" DataField="num_proc" SortExpression="num_proc" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nº Pasta" DataField="num_pasta" SortExpression="num_pasta" />
                            <asp:BoundField HeaderText="Nome Empregado" DataField="nome_emprg" SortExpression="nome_emprg" />
                            <asp:BoundField HeaderText="Tipo Atualização" DataField="desc_abrv_atlz" SortExpression="desc_abrv_atlz" />
                            <asp:BoundField HeaderText="Origem" DataField="cod_origem_dados" SortExpression="cod_origem_dados" />
                            <asp:BoundField HeaderText="Situação" DataField="cod_situacao" SortExpression="cod_situacao" />
                            <%--<asp:BoundField HeaderText="Inclusão" DataField="Dtinclusao" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                            <asp:BoundField HeaderText="Desc. Processo" DataField="desc_processo" />
                            <asp:BoundField HeaderText="Desc. MR" DataField="DESC_MR" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Abrir" CommandName="Alterar" CommandArgument='<%# Eval("NUM_MATR_PARTF")+","+Eval("NUM_SQNCL_PRC")+","+Eval("NUM_PROC")+","+Eval("COD_TIP_ATLZ") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Excluir" CommandName="Deletar" CommandArgument='<%# Eval("NUM_MATR_PARTF")+","+Eval("NUM_SQNCL_PRC")+","+Eval("NUM_PROC")+","+Eval("COD_TIP_ATLZ") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlGridImportarVr" class="tabelaPagina" Visible="false">
                    <table>
                        <tr>
                            <td>Empresa</td>
                            <td>
                                <asp:TextBox ID="txtCodEmpresaImportar" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Matrícula</td>
                            <td>
                                <asp:TextBox ID="txtCodMatriculaImportar" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="drpPesquisaImportar" runat="server">
                                    <asp:ListItem Text="Participante" Value="3" Selected="True" />
                                    <asp:ListItem Text="CPF" Value="4" />
                                    <asp:ListItem Text="Núm. Processo" Value="5" />
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtPesquisaImportar" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnVoltarImportar" runat="server" CssClass="button" Text="<- Voltar" OnClick="btnVoltarImportar_Click" />
                                <asp:Button ID="btnPesquisarImportar" runat="server" CssClass="button" Text="Pesquisar   " OnClick="btnPesquisarImportar_Click" />
                                <asp:Button ID="btnLimparImportar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparImportar_Click" />
                            </td>
                        </tr>
                    </table>

                    <asp:ObjectDataSource runat="server" ID="odsImportarVr"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial.ValorReferenciaBLL"
                        SelectMethod="CarregaProcessosImportVr"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpPesquisaImportar" Name="filType" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="txtPesquisaImportar" Name="filValue" PropertyName="Text"
                                Type="String" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtCodEmpresaImportar" Name="codEmpresa" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtCodMatriculaImportar" Name="codMatricula" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdImportar" runat="server"
                        OnRowCommand="grdImportar_RowCommand"
                        AllowPaging="true"
                        AllowSorting="true"
                        AutoGenerateColumns="False"
                        EmptyDataText="A consulta não retornou registros"
                        CssClass="Table"
                        ClientIDMode="Static"
                        PageSize="8"
                        DataSourceID="odsImportarVr">
                        <Columns>
                            <asp:BoundField HeaderText="Empregado" DataField="nom_emprg" SortExpression="nom_emprg" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Empresa" DataField="cod_emprs" SortExpression="cod_emprs" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Matrícula" DataField="num_rgtro_emprg" SortExpression="num_rgtro_emprg" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nº Processo" DataField="nro_processo" SortExpression="nro_processo" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="IGP-DI" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIGPDI" runat="server" CssClass="span_checkbox"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trabalhista" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkTrab" runat="server" CssClass="span_checkbox"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cívil" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCivil" runat="server" CssClass="span_checkbox"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkImportar" runat="server" CssClass="button"
                                        Text="Importar" CommandName="Importar" CommandArgument='<%# Container.DataItemIndex + "," + Eval("NUM_MATR_PARTF")+","+Eval("NUM_SQNCL_PRC") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlDetalhesVr" class="tabelaPagina" Visible="false">
                    <asp:Panel runat="server" ID="pnlControles" class="tabelaPagina">
                    <table>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Salvar" Enabled="false" />
                                    <asp:Button ID="btnDuplicar" OnClick="btnSalvar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Duplicar" CommandName="Duplicar" Visible="false" />
                                    <asp:Button ID="btnEditar" OnClick="btnEditar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Editar" />
                                <asp:Button ID="btnImprimir" OnClick="btnImprimir_Click" CausesValidation="false" CssClass="button" runat="server" Text="Imprimir" />
                                <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
                            </td>
                            <td>
                                    <div class='n_warning' id="msgAlerta" runat="server" visible="false">
                                        <p>Participante não localizado!</p>
                                    </div>
                            </td>
                        </tr>
                    </table>                    
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlDetalhes" class="tabelaPagina">

                    <table>
                        <tr>
                            <td style="font-weight: bolder">Identificação do Participante</td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>Empresa</td>
                                        <td>Matrícula</td>
                                        <td>Nº Seq. Processo</td>
                                        <td>Cadastro</td>
                                        <td>Origem</td>
                                        <td>Situação</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hdIdVR" runat="server" Value="0" />
                                            <asp:TextBox ID="txtEmpresa" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:HiddenField ID="hfNUM_MATR_PARTF" runat="server" />
                                            <asp:TextBox ID="txtMatricula" runat="server" Enabled="false" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtNumSeqProcesso" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtDtCadastro" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:DropDownList ID="ddlOrigem" runat="server" Width="100%" Enabled="false">
                                                <asp:ListItem Text="Automática" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="Manual" Value="M" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        <td>
                                            <asp:DropDownList ID="ddlSituacao" runat="server" Width="100%" onchange="ddlSituacao_onchange();">
                                                <asp:ListItem Text="Valores Estimados" Value="VE"></asp:ListItem>
                                                <asp:ListItem Text="Valores Revistos" Value="VR" Selected="True"></asp:ListItem>
                                                    <%--  <asp:ListItem Text="Depósito Judicial" Value="DJ"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">Participante</td>
                                        <td>CPF</td>
                                        <td>Adesão</td>
                                        <td>Plano</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtNome" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCPF" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtDtAdesao" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtPlano" runat="server" Enabled="false"></asp:TextBox></td>
                            </td>
                        </tr>
                        <tr>
                            <td>Admissão</td>
                            <td>Demissão</td>
                            <td>Nascimento</td>
                            <td>Perfil</td>
                            <td>DIB</td>
                            <td>Pasta</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDtAdmissao" runat="server" Enabled="false"></asp:TextBox>
                            </td>

                            <td>
                                <asp:TextBox ID="txtDtDemissao" runat="server" Enabled="false"></asp:TextBox>
                            </td>

                            <td>
                                <asp:TextBox ID="txtDtNascimento" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPerfil" runat="server" Enabled="false"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtDIB" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                    <asp:TextBox ID="txtPasta" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>Número do Processo</td>
                                    <td>Pólo-Ação Judicial</td>
                                    <td>Vara</td>
                                    <td colspan="2">Assunto (Pleito)</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNumProcesso" runat="server" Width="300px"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlPoloAcaoJudicial" runat="server" Width="80px">
                                            <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVara" runat="server"></asp:TextBox></td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlAssunto" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Tipo Atualização</td>
                                    <td>Base Atualização Retr.</td>
                                    <td>Histórico</td>                                    
                                    <td>Prescrição/DIP</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblEfetivacao" Text="Efetivação Revisão"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoAtualizacao" runat="server" Width="100%">
                                        </asp:DropDownList></td>
                                    <td style="display: inline-flex;">
                                        <asp:TextBox ID="txtBaseAtualizacao" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" Width="100px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server"
                                            ControlToValidate="txtBaseAtualizacao" ErrorMessage="Informe uma data válida (mm/dd/yyyy)"
                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                            Text="*"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="ddlHistorico" runat="server">
                                        </asp:DropDownList></td>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="txtDIP" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" Width="100px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" runat="server"
                                        ControlToValidate="txtDIP" ErrorMessage="Informe uma data válida (mm/dd/yyyy)"
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                        Text="*"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="txtEfetivacao" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" Width="100px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server"
                                        ControlToValidate="txtEfetivacao" ErrorMessage="Informe uma data válida (mm/dd/yyyy)"
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                        Text="*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                            </td>
                    </tr>
                </table>

                    <table>
                        <tr>
                            <td style="font-weight: bolder">SEM PLEITO</td>
                            <td style="font-weight: bolder">COM PLEITO</td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="2" style="width: 66%">Valor da Suplementação da Data do Início do Beneficio - DIB</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 66%">Benefício Fundação/BSPS</td>
                                                    <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bsps_dib_sem_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 66%">BD/BPD</td>
                                                    <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bd_dib_sem_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 66%">CV</td>
                                                    <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cv_dib_sem_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="2" style="width: 66%">Valor da Suplementação da Data do Início do Beneficio - DIB</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 66%">Benefício Fundação/BSPS</td>
                                                    <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bsps_dib_com_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 66%">BD/BPD</td>
                                                    <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bd_dib_com_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 66%">CV</td>
                                                    <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cv_dib_com_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bolder">SEM PLEITO</td>
                            <td style="font-weight: bolder">COM PLEITO</td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="2">Valor da Suplementação Atual</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                        <asp:TextBox ID="bsps_atu_sem_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">BD/BPD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                        <asp:TextBox ID="bd_atu_sem_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">CV</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                        <asp:TextBox ID="cv_atu_sem_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                    </tr>
                                </table>

                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="2">Valor da Suplementação Atual</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                        <asp:TextBox ID="bsps_atu_com_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">BD/BPD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                        <asp:TextBox ID="bd_atu_com_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">CV</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                        <asp:TextBox ID="cv_atu_com_pleito" runat="server" Width="120px"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td style="font-weight: bolder">Custo Participante</td>
                            <td style="font-weight: bolder">Custo Patrocinador</td>
                        </tr>
                        <tr>
                            <td>

                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 66%">Contribuição - período ativo - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_part_at_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício retroativo - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bnf_part_ret_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Contribuição - benefício retroativo - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_part_ret_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Reserva Matemática - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="resmat_part_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Antecipação dos 25% Reserva Matemática</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="resmat_ant_part_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Contribuição - período ativo - BD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_part_at_bd" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício retroativo - BD/BPD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bnf_part_ret_bd" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Contribuição - Benefício retroativo - BD/BPD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_part_ret_bd" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Reserva Matemática - BD/BPD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="resmat_part_bd" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart(); ValidaBeneficio();" onblur="CalculoPart(); ValidaBeneficio();"
                                                        onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Contribuição - período ativo - CV</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_part_at_cv" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício retroativo - CV</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bnf_part_ret_cv" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPart();" onblur="CalculoPart();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Total - Participante</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="txtTotalParticipante" runat="server" Width="120px" ClientIDMode="Static" onkeypress="mascara(this, moeda)" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td>

                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 66%">Contribuição - período ativo - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_patr_at_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício retroativo - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bnf_patr_ret_bsps" runat="server" Width="120px" ClientIDMode="Static"  onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Reserva Matemática - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="resmat_patr_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Antecipação dos 25% Reserva Matemática</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="resmat_ant_patr_bsps" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Contribuição - período ativo - BD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_patr_at_bd" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício retroativo - BD/BPD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bnf_patr_ret_bd" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Reserva Matemática - BD/BPD</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="resmat_patr_bd" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPatr(); ValidaBeneficio();" onblur="CalculoPatr(); ValidaBeneficio();"
                                                         onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Contribuição - período ativo - CV</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="cntr_patr_at_cv" runat="server" Width="120px" ClientIDMode="Static" onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Benefício retroativo - CV</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="bnf_patr_ret_cv" runat="server" Width="120px" ClientIDMode="Static"  onchange="CalculoPatr();" onblur="CalculoPatr();" onkeypress="mascara(this, moeda)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Total - Patrocinador</td>
                                        <td class="coluna_esquerda">R$&nbsp;
                                                    <asp:TextBox ID="txtTotalPatrocinador" runat="server" Width="120px" ClientIDMode="Static" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 66%">Percentual de Reserva Matemática - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">&nbsp;%&nbsp;
                                                    <asp:TextBox ID="prc_part_resmat_bsps" runat="server" Width="120px" ClientIDMode="Static" onkeypress="mascara(this, moeda)" MaxLength="6"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Percentual de Reserva Matemática - BD/BPD</td>
                                        <td class="coluna_esquerda">&nbsp;%&nbsp;
                                                    <asp:TextBox ID="prc_part_resmat_bd" runat="server" Width="120px" ClientIDMode="Static" onkeypress="mascara(this, moeda)" MaxLength="6" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td>

                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 66%">Percentual de Reserva Matemática - Benefício Fundação/BSPS</td>
                                        <td class="coluna_esquerda">&nbsp;%&nbsp;
                                                    <asp:TextBox ID="prc_patr_resmat_bsps" runat="server" Width="120px" ClientIDMode="Static" onkeypress="mascara(this, moeda)" MaxLength="6"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 66%">Percentual de Reserva Matemática - BD/BPD</td>
                                        <td class="coluna_esquerda">&nbsp;%&nbsp;
                                                    <asp:TextBox ID="prc_patr_resmat_bd" runat="server" Width="120px" ClientIDMode="Static" onkeypress="mascara(this, moeda)" MaxLength="6" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">Nota<asp:TextBox ID="nota" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">Observação<asp:TextBox ID="obs" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                    </table>
                </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImprimir" />
                <asp:PostBackTrigger ControlID="btnImportarVr" />
                <asp:PostBackTrigger ControlID="btnVoltarImportar" />
            </Triggers>
        </asp:UpdatePanel>

    </div>

</asp:Content>
