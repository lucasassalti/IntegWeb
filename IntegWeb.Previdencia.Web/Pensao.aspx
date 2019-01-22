<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Pensao.aspx.cs" Inherits="IntegWeb.Previdencia.Web.Pensao" %>

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
        <h1>Percentual de Pensão</h1>
        <div class="MarginGrid">


            <asp:UpdatePanel runat="server" ID="upAcao">
                <ContentTemplate>

                    <asp:ValidationSummary ID="vsPensao" runat="server" ForeColor="Red" ShowMessageBox="true"
                        ShowSummary="false" ErrorMessage="Representante Requerido" />
                    <div runat="server" id="divAction" class="formTable">
                        <table>
                            <tr>

                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>Data de Validade
                                              <asp:RequiredFieldValidator ID="rfvDtVencimento" runat="server" Text="*" ControlToValidate="txtDataValidade"
                                                  ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma data (mm/dd/yyyy)" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtDataValidade" ErrorMessage="Informe uma data válida (mm/dd/yyyy)"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                                    Text="*"></asp:RegularExpressionValidator>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataValidade" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr>

                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>Nº Representante
                                                                   
                             
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCodRepresentante" OnTextChanged="txtCodRepresentante_TextChanged" AutoPostBack="true" runat="server" Width="87px"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divComDados" runat="server" visible="false">
                                                    <h3>Conferência de dados</h3>
                                                    <div class="MarginGrid">
                                                        <b>Representante:</b>
                                                        <asp:Label ID="lblRepresentante" runat="server"></asp:Label><br />
                                                        <b>Código da Empresa:</b>
                                                        <asp:Label ID="lblEmpresa" runat="server"></asp:Label><br />
                                                        <b>Matrícula:</b>
                                                        <asp:Label ID="lblMatricula" runat="server"></asp:Label><br />
                                                        <b>Dígito:</b>
                                                        <asp:Label ID="lblDigito" runat="server"></asp:Label><br />
                                                    </div>
                                                </div>
                                                <div id="divSemDados" runat="server" visible="false">
                                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Não existe Representante com esse código"></asp:Label><br />

                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>%Percentual Total</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtPercentualTotal" runat="server" onkeypress="mascara(this, moeda)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>%Percentual Dividido</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtPercentualDivido" runat="server" onkeypress="mascara(this, moeda)"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div>
                                        <asp:Button CssClass="button" ID="btnSalvar" OnClick="btnSalvar_Click" runat="server" Text="Salvar" />
                                        <asp:Button CausesValidation="false" CssClass="button" ID="btnVoltar" OnClick="btnVoltar_Click" runat="server" Text="Voltar" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="tabelaPagina" id="divSelect" runat="server">
                        <table>
                            <tr>
                                <td>Informe o nº do representante:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRepres" runat="server" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                    <asp:Button CausesValidation="false" ID="btnBuscar" runat="server" Text="Buscar" CssClass="button" OnClick="btnBuscar_Click" />
                                    <asp:Button CausesValidation="false" ID="btnInserir" runat="server" Text="Inserir Percentual de Pensão" CssClass="button" OnClick="btnInserir_Click" />

                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="grdPensao" DataKeyNames="DAT_VALIDADE, NUM_IDNTF_RPTANT" OnRowCancelingEdit="grdPensao_RowCancelingEdit" OnRowDeleting="grdPensao_RowDeleting" OnRowEditing="grdPensao_RowEditing" OnRowUpdating="grdPensao_RowUpdating" OnPageIndexChanging="grdPensao_PageIndexChanging" runat="server" AllowPaging="True" PageSize="20" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="false">
                            <Columns>

                                <asp:BoundField HeaderText="Validade" ReadOnly="true" DataField="DAT_VALIDADE" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField HeaderText="%Percentual Total">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTotalPerc" onkeypress="mascara(this, moeda)" runat="server" Text='<%# Bind("PCT_PENSAO_TOTAL") %>' Width="90px"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator19" ControlToValidate="txtTotalPerc" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPctPercentual" runat="server" Text='<%# Bind("PCT_PENSAO_TOTAL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%Percentual Dividido">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPenDiv" onkeypress="mascara(this, moeda)" runat="server" Text='<%# Bind("PCT_PENSAO_DIVIDIDA") %>' Width="90px"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator20" ControlToValidate="txtPenDiv" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPctPercentualDiv" runat="server" Text='<%# Bind("PCT_PENSAO_DIVIDIDA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <asp:Button ID="btnSalvar" CssClass="button" runat="server" Text="Salvar" CausesValidation="false" CommandName="Update" />&nbsp;&nbsp;
                                       <asp:Button ID="btnCancelar" CssClass="button" runat="server" Text="Cancelar" CausesValidation="false" CommandName="Cancel" OnClientClick="return confirm('Tem certeza que deseja abandonar a atualização do cadastro?');" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" CssClass="button" runat="server" Text="Editar" CausesValidation="false" CommandName="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnExcluir" CssClass="button" runat="server" Text="Excluir" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja excluir?');"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>
</asp:Content>
