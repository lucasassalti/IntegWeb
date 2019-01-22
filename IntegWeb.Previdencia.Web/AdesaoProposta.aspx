<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdesaoProposta.aspx.cs" Inherits="IntegWeb.Previdencia.Web.AdesaoProposta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $(".date").datepicker({
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }
        });

        function drpPesquisa_onchange() {
            $('#ContentPlaceHolder1_txtPesquisa').hide();
            $('#ContentPlaceHolder1_drpStatus').hide();
            if ($('#ContentPlaceHolder1_drpPesquisa').val() == 1) {
                $('#ContentPlaceHolder1_drpStatus').show();
            } else {
                $('#ContentPlaceHolder1_txtPesquisa').show();
            };
        }

    </script>
    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Proposta de Adesão Previdência</h1>
        <asp:UpdatePanel runat="server" ID="upAdesao">
            <ContentTemplate>
                <div runat="server" id="divSelectProposta" class="tabelaPagina">
                    <div style="text-align: left">
                        <asp:HiddenField ID="hdIdProposta" runat="server" Value="0" />
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="drpPesquisa" runat="server" onchange="drpPesquisa_onchange();">
                                        <asp:ListItem Text="Status" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Registro" Value="2" Selected="True"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtPesquisa" runat="server" Width="300px"></asp:TextBox>
                                    <asp:DropDownList ID="drpStatus" runat="server" Width="300px" style="display: none"></asp:DropDownList>
                                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="false" OnClick="btnPesquisar_Click" />
                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />
                                    <asp:LinkButton ID="lnkInserir" runat="server" CausesValidation="False" OnClick="lnkInserir_Click" CssClass="button">Inserir Proposta</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:GridView AllowPaging="true" AutoGenerateColumns="false"
                        EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdAdesao" runat="server" OnPageIndexChanging="grdAdesao_PageIndexChanging" PageSize="100" OnRowCommand="grdAdesao_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Empresa" DataField="cod_emprs" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Registro" DataField="Registro" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nome" DataField="NOM_PARTICIP" />
                            <asp:BoundField HeaderText="Perfil" DataField="PERFIL" />
                            <asp:BoundField HeaderText="Inclusão" DataField="Dtinclusao" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                            <asp:BoundField HeaderText="Tipo de Beneficiário" DataField="DESC_TPBENEFICIO" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="Tempo de Serviço" DataField="DESC_TPSERVICO" />
                            <asp:BoundField HeaderText="Status" DataField="status" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Alterar" CommandName="Alterar" CommandArgument='<%# Eval("ID_PRADPREV")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Deletar" CommandName="Deletar" CommandArgument='<%# Eval("ID_PRADPREV") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="divActionProposta" runat="server" visible="false">
                    <ajax:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="TbCadastro" HeaderText="Cadastro da Proposta" runat="server" TabIndex="0">
                            <ContentTemplate>


                                <table>

                                    <tr>

                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Registro Empregado</td>

                                                    <td>Código da Empresa</td>

                                                    <td>&nbsp&nbsp Status</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox  ID="txtRegistro" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmpresa" AutoPostBack="true" OnTextChanged="txtEmpresa_TextChanged" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        &nbsp&nbsp<asp:HiddenField ID="hidIdStatus" runat="server" ></asp:HiddenField> <asp:Label ID="lblStatus" runat="server" Enabled="false"></asp:Label></td>
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
                                                        <asp:TextBox ID="txtNome" runat="server" Width="526px"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtPerfil" runat="server" Width="347px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>Data do Perfil</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDtAtivo" runat="server" CssClass="date"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtVoluntaria" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>Data de Inclusão</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDtInclusao" runat="server" CssClass="date"></asp:TextBox>
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
                                                    <td>  <asp:Label ID="lblTempoServico" runat="server" Text="Tempo de Serviço"></asp:Label>  </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="drpBenef" runat="Server" Width="200px" ></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drpTempoServ" runat="Server" Width="200px" ></asp:DropDownList>
                                                        <asp:HiddenField ID="hidcodPlano" runat="server" />
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
                                                        <asp:RadioButtonList ID="rdSituacao" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rdSituacao_SelectedIndexChanged" RepeatDirection="Horizontal">
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
                                            <table runat="server" id="tbMotivoSit">
                                                <tr>
                                                    <td>Qual(is) dado(s)?</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtMotivoSit" Height="90px" TextMode="MultiLine" runat="server" Width="526px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div id="motivoIndeferido" runat="server">
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
                                            <asp:Button ID="btnSalvarCad" OnClick="btnSalvarCad_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" />                                            
                                            <asp:Button ID="btnNovo" OnClick="btnNovo_Click" CausesValidation="false" CssClass="button" runat="server" Text="Novo" Visible="false"/>
                                            <asp:Button ID="btnEnviarCap" OnClick="btnEnviarCap_Click" CausesValidation="false" CssClass="button" runat="server" Text="Enviar para Capitalização" OnClientClick=" return confirm('Atenção \n\nAo Enviar para capitalização não será possível alterar dados. \n\n Confirma o Envio?');" Visible="false" />
                                            <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
                                        </td>
                                    </tr>
                                </table>



                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TbBenefRecusado" Visible="false" HeaderText="Beneficiário Recusado" runat="server" TabIndex="1">
                            <ContentTemplate>
                                <div id="divActionBenef" visible="false" runat="server">
                                    <asp:HiddenField ID="hdIdBenef" runat="server" Value="0" />
                                    <table>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>Nome Beneficiário</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtBeneficiario" runat="server" Width="536"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>CPF</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtCPF" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>

                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>RG</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtRg" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Data de Nascimento</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDtNascimento" runat="server" CssClass="date"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>Grau</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtGrau" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>Nome da Mãe</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtNomMae" runat="server" Width="536"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>Nome do Pai</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtNomPai" runat="server" Width="536"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSalvarBenef" OnClick="btnSalvarBenef_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" />
                                                <asp:Button ID="VoltarBenef" OnClick="VoltarBenef_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divSelectBenef" runat="server" class="tabelaPagina">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkInsereBenef" runat="server" CausesValidation="False" OnClick="lnkInsereBenef_Click" CssClass="button">Inserir Beneficiário Recusado</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView AllowPaging="true" AutoGenerateColumns="false"
                                                    EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdBenef" runat="server" OnRowCommand="grdBenef_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Nome" DataField="nome" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="CPF" DataField="cpf" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="RG" DataField="grau" />
                                                        <asp:BoundField HeaderText="Data Nascimento" DataField="DTNASCIMENTO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                        <asp:BoundField HeaderText="Grau" DataField="grau" />
                                                        <asp:BoundField HeaderText="Nome da Mãe" DataField="MAE" />
                                                        <asp:BoundField HeaderText="Nome da Pai" DataField="pai" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                                                    Text="Alterar" CommandName="Alterar" CommandArgument='<%# Eval("ID_BENEFRECUSADO")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                                    Text="Deletar" CommandName="Deletar" CommandArgument='<%# Eval("ID_BENEFRECUSADO") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TbServico" Visible="false" HeaderText="Tempo de Serviço Recusado" runat="server" TabIndex="2">
                            <ContentTemplate>

                                <div id="divSelectTemp" runat="server" class="tabelaPagina">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkInserirTemp" OnClick="lnkInserirTemp_Click" runat="server" CausesValidation="False" CssClass="button">Inserir Tempo de Serviço</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView AllowPaging="true" AutoGenerateColumns="false"
                                                    EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdTemp" runat="server" OnRowCommand="grdTemp_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Empresa" DataField="EMPRESA" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Data Admissão" DataField="DTADMISSAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                        <asp:BoundField HeaderText="Data Demissão" DataField="DTDEMISSAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                                                    Text="Alterar" CommandName="Alterar" CommandArgument='<%# Eval("ID_TEMPRECUSADO")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                                    Text="Deletar" CommandName="Deletar" CommandArgument='<%# Eval("ID_TEMPRECUSADO") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divActionTemp" runat="server" visible="false">
                                    <asp:HiddenField ID="hdIdTemp" runat="server" Value="0" />
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Empresa</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtEmprs" Width="400px" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Data de Admissão</td>
                                                        <td>Data de Demissão</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDtAdmissao" runat="server" CssClass="date"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDemissao" runat="server" CssClass="date"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSalvarTemp" OnClick="btnSalvarTemp_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" />
                                                <asp:Button ID="btnVoltarTemp" OnClick="btnVoltarTemp_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TbEnvioKit" HeaderText="Envio do Kit" runat="server" Visible="false" TabIndex="3">
                            <ContentTemplate>
                                <table>

                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Data de Envio do Kit</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtdtEnvioKit" runat="server" CssClass="date"></asp:TextBox>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>

                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Data A/R</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDtAr" runat="server" CssClass="date"></asp:TextBox>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>

                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Data  Metrofile</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDtMetrofile" runat="server" CssClass="date"></asp:TextBox>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table>
                                                <tr>
                                                    <td>Código Metrofile </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCodMetrofile" runat="server" Width="536px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>

                                    </tr>

                                </table>
                                <asp:Button ID="btnSavalKit" OnClick="btnSavalKit_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" />
                                <asp:Button ID="btnArquivar" OnClick="btnArquivar_Click" 
                                                             OnClientClick="return confirm('Deseja realmente arquivar esta proposta?');" 
                                                             CausesValidation="false" 
                                                             CssClass="button" 
                                                             runat="server" 
                                                             Text="Arquivar proposta" 
                                                             Enabled="false"/>
                                <asp:Button ID="Button1" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
                            </ContentTemplate>
                        </ajax:TabPanel>

                    </ajax:TabContainer>
                </div>
            </ContentTemplate>
            <%--            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridAssinatura" EventName="RowCommand" />
                <asp:PostBackTrigger ControlID="lnkVEditora" />
            </Triggers>--%>
        </asp:UpdatePanel>

    </div>

</asp:Content>
