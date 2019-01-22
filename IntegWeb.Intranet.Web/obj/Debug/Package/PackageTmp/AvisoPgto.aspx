<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvisoPgto.aspx.cs" Inherits="IntegWeb.Intranet.Web.AvisoPgto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">

<head runat="server">
    <title>Funcesp - Demonstrativo de Pagamento</title>
    <link href="css/estilo.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>


    <script type="text/javascript">
        function fechaTela() {
            window.close();
        }
   </script>

    <script type="text/javascript">
        function enderecoURL(tipo) {

            if (tipo == '3') {
                if ($('#emailPart').val() === '' || $('#emailPart').val() == '' || $('#emailPart').val() === 'undefined') {
                    alert('Campo e-mail é obrigatório!');
                } else {
                    var EndEmailPart = $('#emailPart').val();
                    window.open("AvisoPgtoRelatorio.aspx?hidCOD_EMPRS=<%=Request.QueryString["nempr"]%>&hidNUM_RGTRO_EMPRG=<%=Request.QueryString["nreg"]%>&hidNUM_IDNTF_RPTANT=<%=string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"]%>&hidNUM_IDNTF_DPDTE=<%=string.IsNullOrEmpty(Request.QueryString["ndep"]) ? "0" : Request.QueryString["ndep"]%>&hidANO_REFERENCIA=<%=string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? HidMesAnoRef.Value : Request.QueryString["hidANO_REFERENCIA"]%>&hidasabono=<%=string.IsNullOrEmpty(Request.QueryString["hidasabono"]) ? hidasabono.Value : Request.QueryString["hidasabono"]%>&hidasquadro=<%=hidasquadro.Value%>&tipo=" + tipo + "&emailPart=" + EndEmailPart, "_self");
                }
            } else {
                var EndEmailPart = $('#emailPart').val();
                window.open("AvisoPgtoRelatorio.aspx?hidCOD_EMPRS=<%=Request.QueryString["nempr"]%>&hidNUM_RGTRO_EMPRG=<%=Request.QueryString["nreg"]%>&hidNUM_IDNTF_RPTANT=<%=string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"]%>&hidNUM_IDNTF_DPDTE=<%=string.IsNullOrEmpty(Request.QueryString["ndep"]) ? "0" : Request.QueryString["ndep"]%>&hidANO_REFERENCIA=<%=string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? HidMesAnoRef.Value : Request.QueryString["hidANO_REFERENCIA"]%>&hidasabono=<%=string.IsNullOrEmpty(Request.QueryString["hidasabono"]) ? hidasabono.Value : Request.QueryString["hidasabono"]%>&hidasquadro=<%=hidasquadro.Value%>&tipo=" + tipo + "&emailPart=" + EndEmailPart, "_self");
            }
        }

        function enderecoURLfiltros() {
            var EndEmailPart = $('#emailPart').val();
            window.open("AvisoPgtoPesquisa.aspx?hidCOD_EMPRS=<%=Request.QueryString["nempr"]%>&hidNUM_RGTRO_EMPRG=<%=Request.QueryString["nreg"]%>&hidNUM_IDNTF_RPTANT=<%=string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"]%>&hidNUM_IDNTF_DPDTE=<%=string.IsNullOrEmpty(Request.QueryString["ndep"]) ? "0" : Request.QueryString["ndep"]%>&hidANO_REFERENCIA=<%=string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? HidMesAnoRef.Value : Request.QueryString["hidANO_REFERENCIA"]%>&hidasabono=<%=string.IsNullOrEmpty(Request.QueryString["hidasabono"]) ? hidasabono.Value : Request.QueryString["hidasabono"]%>&hidasquadro=<%=hidasquadro.Value%>&emailPart=" + EndEmailPart, "_self");
            }

    </script>


</head>
<body>
    <form id="form1" runat="server">


        <asp:HiddenField ID="HidMesAnoRef" runat="server" />
        <asp:HiddenField ID="dataMaximaRef" runat="server" />

        <asp:HiddenField Value="0" runat="server" ID="Hidanqtdeaviso" />

        <asp:HiddenField Value="0" runat="server" ID="HidNumDep" />
        <asp:HiddenField Value="N" runat="server" ID="hidasabono" />
        <asp:HiddenField Value="1" runat="server" ID="hidasquadro" />
        <asp:HiddenField Value="2" runat="server" ID="hidasquadro2" />
        <asp:HiddenField Value="3" runat="server" ID="hidasquadro3" />

        <asp:HiddenField ID="NomeTipoAviso" runat="server" />
        <br />
        <br />
        <div id="geral">




            <div class="row">
                <p>
                    <strong>E-mail cadastrado:</strong>
                    <asp:TextBox runat="server" Text="" ID="emailPart" Width="230px"></asp:TextBox>
                </p>
                <div class="col-sm-9">

                    <div class="panel panel-pagamento">


                        <div class="panel-heading"><strong>Dados</strong></div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-6">
                                    <p><strong>NOME</strong>:
                                        <asp:Label ID="resNome" runat="server" Text="Label"></asp:Label></p>
                                    <p><strong>EMPRESA</strong>:<asp:Label ID="rescodEmp" runat="server" Text="Label"></asp:Label></p>
                                    <p><strong>NOME DA EMPRESA</strong>:
                                        <asp:Label ID="resnomEmp" runat="server" Text="Label"></asp:Label></p>
                                </div>

                                <div class="col-sm-6">
                                    <p><strong>PLANO</strong>:
                                        <asp:Label ID="resNomPlano" runat="server" Text="Label"></asp:Label></p>
                                    <p><strong>MATRÍCULA</strong>:
                                        <asp:Label ID="resNumMatr" runat="server" Text="Label"></asp:Label></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-12 col-md-12 col-sm-12 -->
                <div class="col-sm-3">
                    <button type="button" onclick="enderecoURLfiltros();" style="border: 0; background: transparent; float: right; padding: 0;">
                        <img src="img/img_segunda_via_pagamento.jpg" style="border: 0" />
                    </button>
                </div>
                <!-- /col-lg-3 col-md-3 col-sm-3 -->
            </div>
            <!-- /.row -->

            <asp:Label ID="DivErro" runat="server">

                <div class="panel panel-default">
                    <div class="panel-footer">
                        <strong>
                            <asp:Label ID="mensagemErro" runat="server">
                                <asp:Label ID="resAbono" runat="server"></asp:Label>
                            </asp:Label>

                        </strong>
                    </div>

                </div>
            </asp:Label>


            <div id="DivConteudoAbono" runat="server">
                <div class="row">
                    <div class="col-sm-12" style="margin-bottom: -21px !important;">
                        <div class="panel panel-pagamento">
                            <div class="panel-heading"><strong>SELECIONE O AVISO DE PAGAMENTO QUE DESEJA CONSULTAR</strong></div>
                            <div class="panel-body" style="margin-bottom: -10px !important;">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <asp:Label runat="server">&nbsp;<asp:RadioButton ID="RadioButton1" runat="server" GroupName="tipoAviso" OnCheckedChanged="RadioButton1_CheckedChanged" AutoPostBack="True" />
                                            &nbsp;Adiantamento Abono Anual</asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="Label1" runat="server">
                                            <asp:RadioButton ID="RadioButton2" runat="server" OnCheckedChanged="RadioButton2_CheckedChanged" GroupName="tipoAviso" AutoPostBack="true" />
                                            &nbsp;Pagamento Mensal -
                                            <asp:Label ID="mesAtual" runat="server"></asp:Label>
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <br />


            </div>

            <div id="DivConteudo" runat="server">

                <div class="row">
                    <div class="col-sm-12" style="margin-bottom: -21px !important;">
                        <div class="panel panel-pagamento">
                            <div class="panel-heading"><strong>
                                <asp:Label ID="nomeAviso" runat="server"></asp:Label></strong></div>
                            <div class="panel-body" style="margin-bottom: -10px !important;">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <p>MÊS / ANO: <strong class="text-vermelho">
                                            <asp:Label ID="resMesAno" runat="server" Text="Label"></asp:Label></strong></p>
                                    </div>
                                    <div class="col-sm-5">&nbsp;</div>
                                    <div class="col-sm-4">
                                        <p>DATA DE CRÉDITO: <strong class="text-vermelho">
                                            <asp:Label ID="resdataCred" runat="server" Text="Label"></asp:Label></strong></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">&nbsp;</div>
                                    <div class="col-sm-5">&nbsp;</div>
                                    <div class="col-sm-4">
                                        <p>LÍQUIDO A RECEBER: <strong>
                                            <asp:Label ID="resLiquido" runat="server" Text="Label"></asp:Label></strong></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <br />

                <div id="DivMsgAdiantamento" runat="server" class="panel panel-default">
                    <div class="panel-footer" style="background-color: transparent !important;">
                        Adiantamento previsto para o próximo mês: <strong>
                            <asp:Label ID="resPagPrev" runat="server" Text="Label"></asp:Label></strong>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-4">
                        <div class="panel panel-pagamento">
                            <div class="panel-heading">
                                <h3 class="panel-title">Banco: Número / Nome</h3>
                            </div>
                            <div class="panel-body">
                                <p>
                                    <asp:Label ID="resBanco" runat="server" Text="Label"></asp:Label></p>
                            </div>
                        </div>
                    </div>
                    <!-- /col-lg-4 col-md-4 col-sm-4 -->
                    <div class="col-sm-4">
                        <div class="panel panel-pagamento">
                            <div class="panel-heading">
                                <h3 class="panel-title">Agência: Número / Nome</h3>
                            </div>
                            <div class="panel-body">
                                <p>
                                    <asp:Label ID="resAgencia" runat="server" Text="Label"></asp:Label></p>
                            </div>
                        </div>
                    </div>
                    <!-- /col-lg-4 col-md-4 col-sm-4-->
                    <div class="col-sm-4">
                        <div class="panel panel-pagamento">
                            <div class="panel-heading">
                                <h3 class="panel-title">Conta Corrente: Tipo / Número</h3>
                            </div>
                            <div class="panel-body">
                                <p>
                                    <asp:Label ID="resContatipo" runat="server" Text="Label"></asp:Label></p>
                            </div>
                        </div>
                    </div>
                    <!-- /.col-lg-4 col-md-4 col-sm-4 -->
                </div>
                <!-- /.row -->

                <div class="panel panel-default">
                    <div class="panel-footer">
                        <strong>
                            <asp:Label ID="resTXTFIXO31" runat="server" Text="Label"></asp:Label></strong>
                    </div>
                </div>


                <asp:GridView ID="grvBloco2" runat="server" AutoGenerateColumns="false" CssClass="table table-responsivo">
                    <Columns>
                        <asp:BoundField DataField="AVISO_HISTORICO" HeaderText="HISTÓRICOS" ItemStyle-Width="585px" HeaderStyle-CssClass="info" />
                        <asp:BoundField DataField="AVISO_VENCIMENTO" HeaderText="VENCIMENTOS" DataFormatString="{0:n2}" HtmlEncode="false" HeaderStyle-CssClass="info" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="AVISO_DESCONTO" HeaderText="DESCONTOS" DataFormatString="{0:n2}" HtmlEncode="false" HeaderStyle-CssClass="info" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <table border="1" id="Table1" class="table table-responsivo">
                    <tbody>
                        <tr class="info">
                            <td style="width: 585px"><strong>TOTAIS</strong></td>
                            <td style="text-align: right"><strong>
                                <asp:Label ID="TXTTOTAIS1" runat="server" Text="Label"></asp:Label></strong></td>
                            <td style="text-align: right"><strong>
                                <asp:Label ID="TXTTOTAIS2" runat="server" Text="Label"></asp:Label></strong></td>
                        </tr>
                        <tr class="danger">
                            <td><strong>LIQUIDO A RECEBER</strong></td>

                            <td colspan="2" style="text-align: right"><strong>
                                <asp:Label ID="TXTTOTAIS3" runat="server" Text="Label"></asp:Label></strong></td>
                        </tr>

                    </tbody>
                </table>

                <div id="refbloco1_3" class="panel panel-default" runat="server">
                    <div class="panel-footer">
                        <strong>Acompanhamento de saldos referentes a cálculos retroativos</strong>
                    </div>
                    <div class="panel-footer">
                        <p><strong>
                            <asp:Label ID="resTXTFIXO24" runat="server" Text="Label"></asp:Label></strong></p>
                        <p><strong>
                            <asp:Label ID="resTXTFIXO25" runat="server" Text="Label"></asp:Label></strong></p>
                    </div>
                </div>

                <div id="TableBloco3" class="panel panel-pagamento" runat="server">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>HISTÓRICO</th>
                                <th>SALDO ANTERIOR</th>
                                <th>MOVIMENTAÇÃO DO MÊS</th>
                                <th>SALDO ATUAL</th>
                            </tr>
                        </thead>
                        <tbody>

                            <tr>
                                <td>
                                    <asp:Label ID="resHistbloco3" runat="server" Text="Label"></asp:Label></td>
                                <td style="text-align: right">
                                    <asp:Label ID="resSaldobloco3" runat="server" Text="Label"></asp:Label></td>
                                <td style="text-align: right">
                                    <asp:Label ID="resMovbloco3" runat="server" Text="Label"></asp:Label></td>
                                <td style="text-align: right">
                                    <asp:Label ID="resSaldoAtualbloco3" runat="server" Text="Label"></asp:Label></td>
                            </tr>

                        </tbody>
                    </table>
                </div>

                <div class="panel-body" id="refbloco2_3" runat="server" visible="false">
                    <p><strong>Notas</strong>:</p>
                    <p>
                        <asp:Label ID="resRODAPE1" runat="server" Text="Label"></asp:Label></p>
                    <p>
                        <asp:Label ID="resRODAPE2" runat="server" Text="Label"></asp:Label></p>
                    <p>
                        <asp:Label ID="resRODAPE3" runat="server" Text="Label"></asp:Label></p>
                </div>

                <div style="text-align: right">

                    <input type="button" id="exportCrystal" class="btn btn-warning" value="Exportar" onclick="enderecoURL(1);" />
                    <input type="button" id="downPdf" class="btn btn-warning" value="Visualizar PDF" onclick="enderecoURL(2);" />
                    <input type="button" id="Button2" class="btn btn-warning" value="Enviar por e-mail" onclick="enderecoURL(3);" /><br />
                    <br />



                </div>
            </div>
        </div>
    </form>

</body>
</html>
