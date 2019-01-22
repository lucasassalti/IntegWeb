<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AssinaturaTela.aspx.cs" Inherits="IntegWeb.Periodico.Web.AssinaturaTela" %>

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
    </script>
    <div class="full_w">

        <div class="h_title">
        </div>
        <h1>Assinatura</h1>

        <asp:UpdatePanel runat="server" ID="upAssinatura">
            <ContentTemplate>
                <div id="divSelect" class="tabelaPagina" runat="server">
                    <div style="text-align: left">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="drpAssinatura" runat="server" OnSelectedIndexChanged="drpAssinatura_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Selecione" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Código Assinatura" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Mês/Ano Vencimento" Value="2"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtAssinatura" runat="server" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="false" OnClick="btnPesquisar_Click" />


                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />

                                    <asp:LinkButton ID="lnkInserirGrupo" runat="server" CausesValidation="False" OnClick="lnkInserirGrupo_Click" CssClass="button">Inserir Assinatura</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <asp:GridView AutoGenerateColumns="False" EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="gridAssinatura" OnRowCommand="gridAssinatura_RowCommand" runat="server" OnPageIndexChanging="gridAssinatura_PageIndexChanging" PageSize="10" AllowPaging="true">

                        <Columns>
                            <asp:BoundField HeaderText="Código" DataField="cod_assinatura" />
                            <asp:BoundField HeaderText="Periódico" ControlStyle-Width="30%" DataField="NOME_PERIODICO" />
                            <asp:BoundField HeaderText="Período" DataField="DESC_PERIODO" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Distribuição" DataField="DESC_DIST_ASSINAT" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Valor" DataField="VALOR_ASSINAT" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                            <asp:BoundField HeaderText="Data Vencimento" DataField="DT_VECTO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button34" CssClass="button" CommandArgument='<%#Eval("ID_ASSINATURA")%>' CausesValidation="false" runat="server" CommandName="Email" ButtonType="Link" Text="Enviar E-mail para Área" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CssClass="button" CommandArgument='<%#Eval("ID_ASSINATURA")%>' CausesValidation="false" runat="server" CommandName="Atualizar" ButtonType="Link" Text="Atualizar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button4" CssClass="button" CommandArgument='<%#Eval("ID_ASSINATURA")%>' CausesValidation="false" runat="server" CommandName="Cancelar" ButtonType="Link" Text="Cancelar Assinatura" OnClientClick="if ( !confirm('Atenção! \n\nSerá necessário informar o motivo do cancelamento confirma?')) return false;" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="MarginGrid" id="divAction" runat="server">
                    <div class="formTable">
                        <asp:ValidationSummary ID="vsAssinatura" runat="server" ForeColor="Red" ShowMessageBox="true"
                            ShowSummary="false" />
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Código da Assinatura</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCodigo" runat="server" Width="125px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Data Início
                                            <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" Text="*" ControlToValidate="txtDtInicio"
                                                ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data Inicial válida (mm/dd/yyyy)" />
                                                <asp:RegularExpressionValidator ID="RegularDateInicial" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtDtInicio" ErrorMessage="Informe uma Data Inicial válida (mm/dd/yyyy)"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                                    Text="*"></asp:RegularExpressionValidator><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDtInicio" runat="server" Width="190px" onkeypress="mascara(this, data)" MaxLength="10" CssClass="date"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>

                                    <table>
                                        <tr>
                                            <td>Data Vencimento
                                            <asp:RequiredFieldValidator ID="rfvDtVencimento" runat="server" Text="*" ControlToValidate="txtDtVencimento"
                                                ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data Vencimento válida (mm/dd/yyyy)" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtDtVencimento" ErrorMessage="Informe uma Data Pagamento válida (mm/dd/yyyy)"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                                    Text="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDtVencimento" CssClass="date" runat="server" Width="190px" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Revista/Jornal</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpPeriodico" runat="server" Width="190px"></asp:DropDownList><br />
                                                <asp:LinkButton Visible="false" ID="lnkVEditora" runat="server" OnClick="lnkVEditora_Click">Consultar Editora</asp:LinkButton>&nbsp;
                                            </td>

                                        </tr>
                                    </table>
                                </td>

                                <td>

                                    <table>
                                        <tr>
                                            <td>Período</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpPeriodo" runat="server" Width="190px"></asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2">

                                    <table>
                                        <tr>
                                            <td>Distribuição</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpDistribuicao" runat="server" Width="190px"></asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr>
                                <td>

                                    <table>
                                        <tr>
                                            <td>Data Pagamento
                                                <asp:RequiredFieldValidator ID="rfvDtPagamento" runat="server" Text="*" ControlToValidate="txtDtPagamento"
                                                    ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data Pagamento válida (mm/dd/yyyy)" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtDtPagamento" ErrorMessage="Informe uma Data Pagamento válida (mm/dd/yyyy)"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                                    Text="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox CssClass="date" ID="txtDtPagamento" runat="server" Width="190px" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox></td>
                                        </tr>

                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Valor Assinatura</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtValorAssinatura" runat="server" Width="190px" onkeypress="mascara(this, moeda)"></asp:TextBox>

                                            </td>
                                        </tr>

                                    </table>

                                </td>

                                <td>
                                    <table>
                                        <tr>
                                            <td>Data Base
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ControlToValidate="txtDtVigencia"
                                                    ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data Vigência válida (mm/dd/yyyy)" />
                                                <asp:HiddenField ID="hdVigencia" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDtVigencia" runat="server" Width="190px" onkeypress="mascara(this, data)" MaxLength="10" CssClass="date"></asp:TextBox>
                                                <asp:Button ID="btnSalv" Visible="false" runat="server" CssClass="button" Text="Salvar Nova Vigência" OnClick="btnSalv_Click" />
                                                <asp:Button ID="btnCanc" Visible="false" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCanc_Click" />
                                                <asp:Button ID="btnAlt" OnClientClick="if ( !confirm('Deseja alterar o valor de vigência?')) return false;" Visible="false" runat="server" CssClass="button" Text="Alterar Valor da Assinatura" OnClick="btnAlt_Click" />
                                            </td>
                                        </tr>

                                    </table>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Quantidade</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtQuantidade" runat="server" Width="190px" onkeypress="mascara(this, soNumeros)"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" CssClass="button" runat="server" Text="Salvar Assinatura" />
                                    <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />

                                </td>
                            </tr>
                        </table>

                    </div>
                    <br />

                    <div id="divEditora" runat="server" class="tabelaPagina">


                        <h2>Histórico de Valores</h2>
                        <br />
                        <asp:GridView AllowPaging="true" AutoGenerateColumns="false"
                            EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdValores" runat="server" OnPageIndexChanging="grdValores_PageIndexChanging" PageSize="07">
                            <Columns>
                                <asp:BoundField HeaderText="Valor" DataField="VALOR" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:n2}" />
                                <asp:BoundField HeaderText="Início Vigência" DataField="DATA_INICIO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                <asp:BoundField HeaderText="Fim Vigência" DataField="DATA_FIM" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                <asp:BoundField HeaderText="Responsável" DataField="MATRICULA" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Data" DataField="DATA_ALTERACAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                <asp:BoundField HeaderText="Horário" DataField="DATA_ALTERACAO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:T}" />
                            </Columns>
                        </asp:GridView>

                        <h2>Setores Vinculados</h2>
                        <br />
                        <asp:GridView DataKeyNames="ID_AREA" AllowPaging="true" AutoGenerateColumns="false"
                            EmptyDataText="A Assinatura não possui nenhum Setor Vinculado" CssClass="Table" ClientIDMode="Static" ID="grdArea" runat="server" OnPageIndexChanging="grdArea_PageIndexChanging" PageSize="7">
                            <Columns>
                                <asp:BoundField HeaderText="Área" DataField="DESCRICAO" />
                                <asp:BoundField HeaderText="Responsável" DataField="RESPONSAVEL" />
                                <asp:BoundField HeaderText="Andar" DataField="ANDAR" />
                            </Columns>
                        </asp:GridView>
                        <asp:LinkButton ID="btnArea" CssClass="button" runat="server" OnClick="btnArea_Click" CausesValidation="false" Text="Vincular Setor" /></td>


                    </div>
                </div>

                <a href="#divEdit" class="fancybox" id="lnkEditora" style="display: none" runat="server"></a>
                <div style="margin: 6px; width:100%; display: none" id="divEdit">
                    <table class="panelLabel">
                        <tr>
                            <td>
                                <center><b><asp:Label ID="lblNome" runat="server" Text="Label"></asp:Label></b></center>
                            </td>
                        </tr>
                        <tr>
                            <td><b>CNPJ/CPF:&nbsp;</b><asp:Label ID="lblCgc" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Rua:&nbsp;</b><asp:Label ID="lblRua" runat="server" Text="Label"></asp:Label>&nbsp;<b>Número:&nbsp;</b><asp:Label ID="lblNumero" runat="server" Text="Label"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td><b>Complemento:&nbsp;</b><asp:Label ID="lblComplemento" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Bairro:&nbsp;</b><asp:Label ID="lblBairro" runat="server" Text="Label"></asp:Label>&nbsp;<b>Cep:&nbsp;</b><asp:Label ID="lblCep" runat="server" Text="Label"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <b>Cidade:&nbsp;</b><asp:Label ID="lblCidade" runat="server" Text="Label"></asp:Label>&nbsp;<b>UF:&nbsp;</b><asp:Label ID="lblUF" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Fone:&nbsp;</b><asp:Label ID="lblFone" runat="server" Text="Label"></asp:Label>&nbsp;<b>Fax:&nbsp;</b><asp:Label ID="lblFax" runat="server" Text="Label"></asp:Label>&nbsp;<b>Contato:&nbsp;</b><asp:Label ID="lblContato" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>E-mail:&nbsp;</b><asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Site:&nbsp;</b><a id="link" runat="server"></a></td>
                        </tr>
                    </table>
                </div>

                <div id="DivActionArea" runat="server" class="MarginGrid" visible="false">
                    <br />
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSalvarArea" OnClick="btnSalvarArea_Click" CssClass="button" runat="server" Text="Salvar" CausesValidation="false" />
                                <asp:Button ID="btVoltar" OnClick="btVoltar_Click" CssClass="button" runat="server" Text="Voltar" CausesValidation="false" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblPagina" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Área/Responsável</b>
                                        </td>
                                        <td></td>
                                        <td>
                                            <b>Área/Assinante</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstPagina" runat="server" Height="500px" Width="400px" SelectionMode="Multiple"></asp:ListBox>

                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnEnvio" runat="server" Text=">" Width="45px" OnClick="btnEnvio_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnEnvioTodos" runat="server" Text=">>" Width="45px" OnClick="btnEnvioTodos_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnRemovs" runat="server" Text="<" Width="45px" OnClick="btnRemovs_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnRemovsTodos" runat="server" Text="<<" Width="45px" OnClick="btnRemovsTodos_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstPaginaAcesso" runat="server" Height="500px" Width="400px" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                </div>
                <div id="DivDelete" class="tabelaPagina" runat="server" visible="false">
                    <h2>Digite o motivo do cancelamento:</h2>
                    <table>

                        <tr>
                            <td>
                                <asp:TextBox ID="txtobs" Width="400" Height="250" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Ex: O setor ATD não renovou assinatura"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSalvObs" runat="server" OnClick="btnSalvObs_Click" CssClass="button" Text="Salvar" />
                                <asp:Button ID="butonVoltar" runat="server" OnClick="butonVoltar_Click" CssClass="button" Text="Voltar" />
                            </td>
                        </tr>
                    </table>

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridAssinatura" EventName="RowCommand" />
                <asp:PostBackTrigger ControlID="lnkVEditora" />
            </Triggers>
        </asp:UpdatePanel>

    </div>




</asp:Content>
