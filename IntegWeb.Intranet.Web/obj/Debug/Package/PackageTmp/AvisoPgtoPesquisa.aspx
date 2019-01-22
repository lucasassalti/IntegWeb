<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AvisoPgtoPesquisa.aspx.cs" Inherits="IntegWeb.Intranet.Web.AvisoPgtoPesquisa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

    <script type="text/javascript">

        function enderecoURL(tipo) {
            var EndEmailPart = $('#campoEmailPart').val();
            if (EndEmailPart === '' || EndEmailPart === 'undefined') {
                alert('Campo e-mail é obrigatório!')
            } else {

                var chkArray = [];
                var chkArrayAbono = [];

                $("[name='chkSelect']:checked").each(function () {
                    chkArray.push($(this).val());
                    chkArrayAbono.push($(this).attr("ID"));
                });

                var anoMesReferencia = chkArray.join('|');
                var tipoMesAbono = chkArrayAbono.join('|');

                //alert(anoMesReferencia);
                //alert(tipoMesAbono);
                window.open("AvisoPgtoRelatorio.aspx?hidCOD_EMPRS=<%=Request.QueryString["hidCOD_EMPRS"]%>&hidNUM_RGTRO_EMPRG=<%=Request.QueryString["hidNUM_RGTRO_EMPRG"]%>&hidNUM_IDNTF_RPTANT=<%=Request.QueryString["hidNUM_IDNTF_RPTANT"]%>&hidNUM_IDNTF_DPDTE=<%=Request.QueryString["hidNUM_IDNTF_DPDTE"]%>&hidANO_REFERENCIA=" + anoMesReferencia + "&hidasabono=" + tipoMesAbono + "&hidasquadro=<%=Request.QueryString["hidasquadro"]%>&tipo=" + tipo + "&emailPart=" + EndEmailPart + "&NomeTipoAviso=<%=Request.QueryString["NomeTipoAviso"]%>", "_self");
            }
        }
    </script>

    <title>Funcesp - Demonstrativo de Pagamento - Filtro pesquisa</title>
    <link href="css/estilo.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            width: 108px;
        }

        .auto-style2 {
            width: 15px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="geral">
            <div class="row">

                <br />
                <br />

                <p>
                    <strong>E-mail cadastrado:</strong>
                    <asp:TextBox runat="server" Text="" ID="campoEmailPart" Width="230px"></asp:TextBox>
                </p>

                <asp:HiddenField ID="dataMaxima" runat="server" Value="" />
                <asp:HiddenField ID="anoMaximo" runat="server" Value="" />
                <asp:HiddenField ID="DataCompleta" runat="server" Value="" />
                <asp:HiddenField ID="NomeTipoAviso" runat="server" Value="" />





                <div class="col-sm-12">
                    <div class="panel panel-pagamento">

                        <div class="panel-heading"><strong>Dados</strong></div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-6">
                                    <p>
                                        <strong>NOME</strong>:
                                        <asp:Label ID="resNome" runat="server" Text="Label"></asp:Label>
                                    </p>
                                    <p><strong>EMPRESA</strong>:<asp:Label ID="rescodEmp" runat="server" Text="Label"></asp:Label></p>
                                    <p>
                                        <strong>NOME DA EMPRESA</strong>:
                                        <asp:Label ID="resnomEmp" runat="server" Text="Label"></asp:Label>
                                    </p>
                                </div>

                                <div class="col-sm-6">
                                    <p>
                                        <strong>PLANO</strong>:
                                        <asp:Label ID="resNomPlano" runat="server" Text="Label"></asp:Label>
                                    </p>
                                    <p>
                                        <strong>MATRÍCULA</strong>:
                                        <asp:Label ID="resNumMatr" runat="server" Text="Label"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-12 col-md-12 col-sm-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-sm-12">
                    <form name="extrato2viaForm">
                        <div class="panel panel-pagamento">
                            <div class="panel-heading"><strong>Pesquisa por período</strong></div>
                            <div class="panel-body">
                                <table border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 84px; height: 42px; font-size: 12px; font-weight: bold">DATA INÍCIO:</td>
                                            <td class="auto-style1">
                                                <asp:DropDownList ID="mesInicio" runat="server" CausesValidation="true">
                                                    <asp:ListItem Value="01">JANEIRO</asp:ListItem>
                                                    <asp:ListItem Value="02">FEVEREIRO</asp:ListItem>
                                                    <asp:ListItem Value="03">MARÇO</asp:ListItem>
                                                    <asp:ListItem Value="04">ABRIL</asp:ListItem>
                                                    <asp:ListItem Value="05">MAIO</asp:ListItem>
                                                    <asp:ListItem Value="06">JUNHO</asp:ListItem>
                                                    <asp:ListItem Value="07">JULHO</asp:ListItem>
                                                    <asp:ListItem Value="08">AGOSTO</asp:ListItem>
                                                    <asp:ListItem Value="09">SETEMBRO</asp:ListItem>
                                                    <asp:ListItem Value="10">OUTUBRO</asp:ListItem>
                                                    <asp:ListItem Value="11">NOVEMBRO</asp:ListItem>
                                                    <asp:ListItem Value="12">DEZEMBRO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style2">&nbsp;/</td>
                                            <td style="width: 317px">
                                                <asp:DropDownList ID="anoInicio" runat="server" CausesValidation="true">
                                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                                    <asp:ListItem Value="2021">2021</asp:ListItem>
                                                    <asp:ListItem Value="2022">2022</asp:ListItem>
                                                    <asp:ListItem Value="2023">2023</asp:ListItem>
                                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                                    <asp:ListItem Value="2025">2025</asp:ListItem>
                                                    <asp:ListItem Value="2026">2026</asp:ListItem>
                                                    <asp:ListItem Value="2027">2027</asp:ListItem>
                                                    <asp:ListItem Value="2028">2028</asp:ListItem>
                                                    <asp:ListItem Value="2029">2029</asp:ListItem>
                                                    <asp:ListItem Value="2030">2030</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 84px; font-size: 12px; font-weight: bold">DATA FIM:</td>
                                            <td class="auto-style1">
                                                <asp:DropDownList ID="mesFim" runat="server" CausesValidation="true">
                                                    <asp:ListItem Value="01">JANEIRO</asp:ListItem>
                                                    <asp:ListItem Value="02">FEVEREIRO</asp:ListItem>
                                                    <asp:ListItem Value="03">MARÇO</asp:ListItem>
                                                    <asp:ListItem Value="04">ABRIL</asp:ListItem>
                                                    <asp:ListItem Value="05">MAIO</asp:ListItem>
                                                    <asp:ListItem Value="06">JUNHO</asp:ListItem>
                                                    <asp:ListItem Value="07">JULHO</asp:ListItem>
                                                    <asp:ListItem Value="08">AGOSTO</asp:ListItem>
                                                    <asp:ListItem Value="09">SETEMBRO</asp:ListItem>
                                                    <asp:ListItem Value="10">OUTUBRO</asp:ListItem>
                                                    <asp:ListItem Value="11">NOVEMBRO</asp:ListItem>
                                                    <asp:ListItem Value="12">DEZEMBRO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style2">&nbsp;/</td>
                                            <td style="width: 317px">
                                                <asp:DropDownList ID="anoFim" runat="server" CausesValidation="true">
                                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                                    <asp:ListItem Value="2021">2021</asp:ListItem>
                                                    <asp:ListItem Value="2022">2022</asp:ListItem>
                                                    <asp:ListItem Value="2023">2023</asp:ListItem>
                                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                                    <asp:ListItem Value="2025">2025</asp:ListItem>
                                                    <asp:ListItem Value="2026">2026</asp:ListItem>
                                                    <asp:ListItem Value="2027">2027</asp:ListItem>
                                                    <asp:ListItem Value="2028">2028</asp:ListItem>
                                                    <asp:ListItem Value="2029">2029</asp:ListItem>
                                                    <asp:ListItem Value="2030">2030</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>



                            </div>
                            <!-- /.panel-body-->
                            <div class="panel-footer" style="text-align: right">
                                <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-info" Text="LIMPAR" OnClick="btnLimpar_Click" CausesValidation="true" />
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="btn btn-success" Text="PESQUISAR" OnClick="btnPesquisar_Click" CausesValidation="true" />
                            </div>
                        </div>
                        <!-- /.panel panel-default -->
                    </form>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row" id="resultados">
                <div class="col-sm-12">
                    <div class="panel panel-pagamento">
                        <div class="panel-heading"><strong>Aviso de Pagamento Mensal(Avisos de pagamentos a partir de janeiro/2015)</strong></div>
                        <div class="panel-body">
                            <div class="panel panel-pagamento">
                                <table class="table table-striped table-bordered">
                                    <asp:ObjectDataSource
                                        ID="odsResultadoTr"
                                        runat="server"
                                        TypeName="Intranet.Aplicacao.BLL.pagamentosBLL"
                                        SelectMethod="RetornarPgtos">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="mesInicio" Name="mesInicial" PropertyName="SelectedValue" Type="String" />
                                            <asp:ControlParameter ControlID="anoInicio" Name="anoInicial" PropertyName="SelectedValue" Type="String" />
                                            <asp:ControlParameter ControlID="mesFim" Name="mesFim" PropertyName="SelectedValue" Type="String" />
                                            <asp:ControlParameter ControlID="anoFim" Name="anoFim" PropertyName="SelectedValue" Type="String" />

                                            <asp:QueryStringParameter Name="nempr" QueryStringField="hidCOD_EMPRS" />
                                            <asp:QueryStringParameter Name="nreg" QueryStringField="hidNUM_RGTRO_EMPRG" />
                                            <asp:QueryStringParameter Name="repres" QueryStringField="hidNUM_IDNTF_RPTANT" />

                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView
                                        ID="grdResultadoTr"
                                        runat="server"
                                        DataSourceID="odsResultadoTr"
                                        OnRowCommand="grdResultadoTr_RowCommand"
                                        AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Text="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Periodo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("mesAnoref") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Visualizar Extrato">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkVisualizar" runat="server" Text="Visualizar" CommandName="Visualizar" CommandArgument='<%# Bind("REFERENCIA") %>' />
                                                    <asp:HiddenField ID="hfReferencia" runat="server" Value='<%# Bind("REFERENCIA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </table>
                            </div>
                            <asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar selecionados por e-mail" CssClass="btn btn-warning" OnClick="btnEnviarEmail_Click" />
                            <br />
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-12 -->
            </div>

        </div>
    </form>
</body>
</html>
