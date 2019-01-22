<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CotaTela.aspx.cs" Inherits="IntegWeb.Saude.Web.CotaTela" %>



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
    <asp:UpdatePanel ID="upCota" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="vsCota" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">
                <div class="h_title">
                </div>
                <h1>Relatório de Cotas
                </h1>
                <div class="MarginGrid">

                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDtCota" runat="server" CssClass="date" onkeypress="mascara(this, data);" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" Text="*" ControlToValidate="txtDtCota"
                                    ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data válida (mm/dd/yyyy)" />
                                <asp:RegularExpressionValidator ID="RegularDateInicial" ForeColor="Red" runat="server"
                                    ControlToValidate="txtDtCota" ErrorMessage="Informe uma Data Inicial válida (mm/dd/yyyy)"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                    Text="*"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" runat="server" Text="Pesquisar" CssClass="button" /></td>
                            <td>
                                <asp:Button OnClick="btnGeCota_Click" ID="btnGeCota" runat="server" Text="Gerar Relatório de Cotas" CssClass="button" OnClientClick = " return confirm('ATENÇÃO\n\nAo clicar em Ok, será iniciado o processo (Cálculo de Cotas) aguarde o término do processo.');" /></td>
                        </tr>

                    </table>
                    <br />
                    <div class="tabelaPagina">

                        <asp:GridView ID="grdCota" OnRowCommand="grdCota_RowCommand" runat="server" AutoGenerateColumns="false" EmptyDataText="A consulta não retornou dados">
                            <Columns>
                                <asp:BoundField HeaderText="Matrícula" DataField="username" />
                                <asp:BoundField HeaderText="Data de Movimentação" DataField="dtmov" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Data de Geração" DataField="dtinigeracao" DataFormatString="{0:dd/MM/yyyy HH:mm:ss} " />
                                <asp:TemplateField>
                                    <ItemTemplate>

                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" CommandName="GerarCota" CommandArgument='<%# Eval("ID") %>'>Gerar Excel</asp:LinkButton>
                                    </ItemTemplate>


                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>

                </div>
            </div>
        </ContentTemplate>
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

  

</asp:Content>
