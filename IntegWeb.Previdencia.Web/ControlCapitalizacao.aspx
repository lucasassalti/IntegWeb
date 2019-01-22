<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ControlCapitalizacao.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ControlCapitalizacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function _client_side_script() {
            $('#chkSelectAll').click(function () {
                $('.span_checkbox input:checkbox').prop('checked', $('#chkSelectAll').prop('checked'));
            });
        };

    </script>
    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Controle Capitalização</h1>
        <asp:UpdatePanel runat="server" ID="upAdesao">
            <ContentTemplate>
                <div runat="server" id="divSelectProposta" class="tabelaPagina">

                    <table>
                        <tr>
                            <td>Empresa</td>
                            <td>
                                <asp:TextBox ID="txtCodEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                            </td>
                            <td>Matricula</td>
                            <td>
                                <asp:TextBox ID="txtCodMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Período de inclusão</td>
                            <td>
                                <asp:TextBox ID="txtDtIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDtIni" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                </td><td>
                            até&nbsp&nbsp&nbsp</td>
                                <td>
                            <asp:TextBox ID="txtDtFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server" ControlToValidate="txtDtFim" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Status</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnTextChanged="ddlStatus_TextChanged" >
                                    <asp:ListItem Text="<TODOS>" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Pendentes" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Deferidos" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Indeferidos" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="false" OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />
                                <asp:Button ID="btnDeferirTodas" runat="server" Text="Deferir Selecionadas" CssClass="button" CausesValidation="false" OnClick="btnDeferirTodas_Click" />
                            </td>
                        </tr>
                    </table>

                    <%--<div style="text-align: left"></div>--%>

                    <asp:HiddenField ID="hdIdProposta" runat="server" Value="0" />

                    <asp:ObjectDataSource runat="server" ID="odsPropostaControle"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.PropostaAdesaoBLL"
                        SelectMethod="ListarControles"
                        SelectCountMethod="SelectCount"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtCodEmpresa" Name="codEmpresa" PropertyName="Text" ConvertEmptyStringToNull="true"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="txtCodMatricula" Name="codMatricula" PropertyName="Text" ConvertEmptyStringToNull="true"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="txtDtIni" Name="dtIni" PropertyName="Text" ConvertEmptyStringToNull="true"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="txtDtFim" Name="dtFim" PropertyName="Text" ConvertEmptyStringToNull="true"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="ddlStatus" Name="intStatus" PropertyName="SelectedValue" ConvertEmptyStringToNull="true"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdAdesao" runat="server"
                        OnRowCreated="GridView_RowCreated"
                        OnRowCommand="grdAdesao_RowCommand"
                        AllowPaging="true"
                        AllowSorting="true"
                        AutoGenerateColumns="False"
                        EmptyDataText="A consulta não retornou registros"
                        CssClass="Table"
                        ClientIDMode="Static"
                        PageSize="100"
                        DataSourceID="odsPropostaControle">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" Visible='<%# btnDeferirTodas.Enabled %>'/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text="" class="span_checkbox" Visible='<%# btnDeferirTodas.Enabled %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Empresa" DataField="cod_emprs" SortExpression="cod_emprs" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Registro" DataField="Registro" SortExpression="Registro" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nome" DataField="NOM_PARTICIP" SortExpression="NOM_PARTICIP" />                            
                            <%--<asp:BoundField HeaderText="Dt. Perfil" DataField="dt_perfil" SortExpression="dt_perfil" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />--%>
                            <asp:BoundField HeaderText="Inclusão" DataField="DT_ATIVO_ATIVO" SortExpression="DT_ATIVO_ATIVO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Adesão" DataField="DATA_DA_ADESAO" SortExpression="DATA_DA_ADESAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Status" DataField="status" SortExpression="status"/>
                            <asp:BoundField HeaderText="Deferimento" DataField="dt_deferimento" SortExpression="dt_deferimento" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button" Text="Detalhe da Proposta" CommandName="Alterar" CommandArgument='<%# Eval("ID_PRADPREV")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div id="divActionProposta" runat="server" class="MarginGrid" visible="false">
                    <div class="formTable">
                        <table>

                            <tr>

                                <td>
                                    <table>
                                        <tr>
                                            <td>Registro Empregado</td>

                                            <td>Código da Empresa</td>

                                            <td>Status</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtRegistro" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmpresa" runat="server" Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtStatus" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Nome</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtNome" runat="server" Width="526px" Enabled="false"></asp:TextBox>
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
                                            <td>Perfil</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtPerfil" runat="server" Enabled="false" Width="347px"></asp:TextBox>                                                
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>Dt. Perfil (ATIVO-ATIVO)</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDtAtivo" runat="server" CssClass="date" Enabled="false"></asp:TextBox>                                                
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>% Voluntária</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtVoluntaria" runat="server" Enabled="false"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>Data de Deferimento</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDtDeferimento" runat="server" CssClass="date" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="1">
                                            <table>
                                                <tr>
                                                    <td>Data de Inclusão</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDtInclusao" runat="server" CssClass="date" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ID Deferimento</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtIdDeferimento" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                            </tr>
                            <%--                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>% Voluntária</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtVoluntaria" runat="server"></asp:TextBox>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Regime de Tributação</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="drpRegime" runat="Server" Width="170px"></asp:DropDownList>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Situação Participante</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="drpSitParticip" runat="Server" Width="170px"></asp:DropDownList>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>--%>
                            <tr>
                                <td colspan="3">
                                    <div id="divCap" runat="server" visible="false">
                                        <table class="panelLabel" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <center><b><asp:Label ID="lblNome" runat="server" Text="Label">Capitalização</asp:Label></b></center>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><b>Código (num_matri):&nbsp;</b><asp:Label ID="lblNumMatri" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><b>Regime de Tributação:&nbsp;</b><asp:Label ID="lblRegime" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><b>Plano:&nbsp;</b><asp:Label ID="lblPlano" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <%--  <tr>
                                                        <td><b>% Voluntária:&nbsp;</b><asp:Label ID="lblVoluntaria" runat="server" Text="Label"></asp:Label>
                                                    </tr>--%>
                                            <tr>
                                                <td>
                                                    <b>Data de Adesão:&nbsp;</b><asp:Label ID="lblDtAdesao" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><b>Data de Admissão:&nbsp;</b><asp:Label ID="lblDtAdmissao" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <%--  <tr>
                                                        <td><b>Data de Inclusão:&nbsp;</b><asp:Label ID="lblDtInclusao" runat="server" Text="Label"></asp:Label></td>
                                                    </tr>--%>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>

                                <td colspan="2"> 
                                    <table>
                                        <tr>
                                            <td>Tipo de Beneficiário</td>
                                            <td>Tempo de Serviço</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpBenef" runat="Server" Width="200px" Enabled="false"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpTempoServ" runat="Server" Width="200px" Enabled="false"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                          
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Faltam dados na proposta Recebida?</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList Enabled="false" ID="rdSituacao" AutoPostBack="true" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Não" Value="0" />
                                                    <asp:ListItem Text="Sim" Value="1" />
                                                </asp:RadioButtonList>
                                            </td>

                                        </tr>
                                    </table>
                                </td>


                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table runat="server" id="tbMotivoSit" visible="false">
                                        <tr>
                                            <td>Qual(is) dado(s)?</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtMotivoSit" Height="90px" TextMode="MultiLine" runat="server" Width="526px" Enabled="false"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div id="motivoIndeferido" visible="false" runat="server">
                                        <table>
                                            <tr>
                                                <td>Histórico Observações:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtIndMotivo" Width="526px" Height="90px" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Incluir observação:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtNovoCometario" Width="526px" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <h3>Beneficiários Recusados</h3>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="tabelaPagina">
                                        <asp:GridView AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="grdBenef_PageIndexChanging" PageSize="10"
                                            EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdBenef" runat="server">
                                            <Columns>
                                                <asp:BoundField HeaderText="Nome" DataField="nome" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="CPF" DataField="cpf" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="RG" DataField="grau" />
                                                <asp:BoundField HeaderText="Data Nascimento" DataField="DTNASCIMENTO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                <asp:BoundField HeaderText="grau" DataField="grau" />
                                                <asp:BoundField HeaderText="Nome da Mãe" DataField="MAE" />
                                                <asp:BoundField HeaderText="Nome da Pai" DataField="pai" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <h3>Tempo de Serviço</h3>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="tabelaPagina">
                                        <asp:GridView AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="grdTemp_PageIndexChanging" PageSize="10"
                                            EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdTemp" runat="server">
                                            <Columns>
                                                <asp:BoundField HeaderText="Empresa" DataField="EMPRESA" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Data Admissão" DataField="DTADMISSAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                <asp:BoundField HeaderText="Data Demissão" DataField="DTDEMISSAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnDeferir" OnClick="btnDeferir_Click" CausesValidation="false" CssClass="button" runat="server" Text="Deferir" OnClientClick=" return confirm('Atenção \n\n Confirma o deferimento da proposta?');" />
                                    <asp:Button ID="btnIndeferir" OnClick="btnIndeferir_Click" CausesValidation="false" CssClass="button" runat="server" Text="Indeferir" Visible="true" />
                                    <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div id="divIndefirir" visible="false" runat="server" class="MarginGrid">
                    <div class="formTable">
                        <table>
                            <tr>
                                <td>Digite o motivo:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtMotivoInd" Height="90px" Width="526px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" />
                                    <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <%--            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridAssinatura" EventName="RowCommand" />
                <asp:PostBackTrigger ControlID="lnkVEditora" />
            </Triggers>--%>
        </asp:UpdatePanel>

    </div>
</asp:Content>
