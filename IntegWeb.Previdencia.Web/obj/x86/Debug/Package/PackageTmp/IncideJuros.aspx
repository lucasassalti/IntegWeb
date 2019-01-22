<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="IncideJuros.aspx.cs" Inherits="IntegWeb.Previdencia.Web.IncideJuros" %>
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

        <h1>Incidência de Juros</h1>
        <asp:UpdatePanel runat="server" ID="update">
            <ContentTemplate>
                <div id="divSelect" runat="server">

                    <div class="tabelaPagina" id="div1" runat="server">

                        <table>

                            <tr>
                                <td>Digite o Nº Empresa</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="6"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Digite o Nº Matrícula</td>
                                <td>
                                    <asp:TextBox ID="txtCodMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="6"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" runat="server" CssClass="button" Text="Pesquisar   " />

                                    <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" runat="server" CssClass="button" Text="Limpar" />


                                    <asp:Button ID="btnInserir" OnClick="btnInserir_Click" runat="server" CssClass="button" Text="Inserir" /></td>
                            </tr>
                        </table>

                        <div id="divParticip" runat="server" >

                            <div class="formTable">
                                <asp:HiddenField ID="hdMatricula" runat="server" />
                                <asp:HiddenField ID="HdEmpresa" runat="server" />
                                <asp:HiddenField ID="hdNumPartif" runat="server" />
                                <asp:GridView AllowPaging="true" AutoGenerateColumns="false"
                                    EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grd" runat="server" OnPageIndexChanging="grd_PageIndexChanging" PageSize="10" OnRowCommand="grd_RowCommand">
                                    <Columns>
                                        <asp:BoundField HeaderText="Número" DataField="NUM_MATR_PARTF" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Representante" DataField="NUM_IDNTF_RPTANT" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Taxa de Juros" DataField="TX_JUROS" />
                                        <asp:BoundField HeaderText="Data Início Vigência" DataField="DT_INIC_VIG" dataformatstring="{0:dd/MM/yyyy}" />
                                        <asp:BoundField HeaderText="Data Fim Vigência" DataField="DT_FIM_VIG" dataformatstring="{0:dd/MM/yyyy}"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeletar" runat="server" CssClass="button"
                                                    Text="Deletar" CommandName="Deletar" CommandArgument='<%# Eval("NUM_MATR_PARTF")+","+Eval("NUM_IDNTF_RPTANT")+","+Eval("DT_INIC_VIG")%>'></asp:LinkButton>
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

                                        <td>Matrícula ISS</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtNumMatr" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server"></asp:TextBox>

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
                                            <asp:TextBox ID="txtRepresentante" onkeypress="mascara(this, soNumeros)" CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>

                                        <td>Data Início Vigência</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtDtIni" onkeypress="mascara(this, data)" MaxLength="10" CssClass="date" CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>

                                        <td>Data Fim Vigência</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtDtFim" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>

                                        <td>Taxa de Juros</td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtJuros"  CausesValidation="false" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                            </td>
       
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
