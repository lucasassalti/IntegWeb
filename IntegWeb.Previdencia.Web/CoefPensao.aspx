<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CoefPensao.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CoefPensao" %>

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

        <h1>Coeficiente de Pensão</h1>
        <asp:UpdatePanel runat="server" ID="update">
            <ContentTemplate>
                <div id="divSelect" runat="server">

                    <div class="tabelaPagina" id="div1" runat="server">

                        <table>

                            <tr>
                                <td>Digite o Nº Empresa</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Digite o Nº Matrícula</td>
                                <td>
                                    <asp:TextBox ID="txtCodMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" runat="server" CssClass="button" Text="Pesquisa" CausesValidation="false"/>

                                    <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" runat="server" CssClass="button" Text="Limpar" CausesValidation="false"/>

                                    <asp:Button ID="btnInserir" OnClick="btnInserir_Click" runat="server" CssClass="button" Text="Inserir Coeficiente" CausesValidation="false"/></td>
                            </tr>
                        </table>

                        <div id="divParticip" runat="server" >

                            <div class="formTable">
                                <asp:HiddenField ID="hdMatricula" runat="server" />
                                <asp:HiddenField ID="HdEmpresa" runat="server" />
                                <asp:HiddenField ID="hdNumPartif" runat="server" />
                                <asp:GridView AllowPaging="true" 
                                    AutoGenerateColumns="false"
                                    EmptyDataText="A consulta não retornou dados"
                                    CssClass="Table" 
                                    ClientIDMode="Static" 
                                    ID="grd" 
                                    runat="server" 
                                    OnPageIndexChanging="grd_PageIndexChanging" 
                                    PageSize="10" 
                                    OnRowCommand="grd_RowCommand">
                                    <Columns>
                                        <asp:BoundField HeaderText="Número" DataField="NUM_MATR_PARTF" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Representante" DataField="NUM_IDNTF_RPTANT" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Ano de" DataField="ANO_DE" />
                                        <asp:BoundField HeaderText="Mês de" DataField="MES_DE" />
                                        <asp:BoundField HeaderText="Ano até" DataField="ANO_ATE" />
                                        <asp:BoundField HeaderText="Mês até" DataField="MES_ATE" />
                                        <asp:BoundField HeaderText="Coeficiente" DataField="COEF_PENS_MES" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeletar" runat="server" CssClass="button"
                                                    Text="Deletar" CommandName="Deletar" CommandArgument='<%# Eval("NUM_MATR_PARTF")+","+Eval("NUM_IDNTF_RPTANT")+","+Eval("ANO_DE")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>

                </div>

                <div id="divInsert" runat="server" class="formTable" visible="false">
                    <table>
                        <tr>
                            <td>

                                <table>
                                    <tr>

                                        <td>Matrícula FSS</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtNumMatr" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server" MaxLength="6"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>

                                        <td>Matrícula Representante</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtRepresentante" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server" MaxLength="6"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>

                                        <td>Ano de</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtAnoDe" MaxLength="4" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>

                                        <td>Mês de</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtMesDe"  MaxLength="2" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>

                                        <td>Ano ate</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtAnoAte"  MaxLength="4" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>

                                        <td>Mês Ate</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtMesAte" MaxLength="2" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>

                                        <td>Coeficiente de Pensão</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtCoeficiente"  CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" runat="server" CssClass="button" Text="Salvar" />
                                <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" runat="server" CssClass="button" Text="Voltar á Tela Inicial" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
