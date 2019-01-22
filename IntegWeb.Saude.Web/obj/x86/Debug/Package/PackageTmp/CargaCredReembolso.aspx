<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="false" CodeBehind="CargaCredReembolso.aspx.cs" Inherits="IntegWeb.Saude.Web.CargaCredReembolso1" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">


        $(document).ready(function AtualizaCampo() {
            $("#ContentPlaceHolder1_dataInicio").change(function changeDataInicio() {
                var dataIncio = $("#ContentPlaceHolder1_dataInicio").val();
                $("#ContentPlaceHolder1_dataFim").val(dataIncio);
            });
        });


        function mascaraData(campo, e) {
            var kC = (document.all) ? event.keyCode : e.keyCode;
            var data = campo.value;

            if (kC != 8 && kC != 46) {
                if (data.length == 2) {
                    campo.value = data += '/';
                }
                else if (data.length == 5) {
                    campo.value = data += '/';
                }
                else
                    campo.value = data;
            }
        }

       


        function validaData() {

            var data_1 = $('#ContentPlaceHolder1_dataInicio').val();
            var data_2 = $('#ContentPlaceHolder1_dataFim').val();

            if ((data_1 == '') || (data_2 == '')) {
                alert('Informe o período!')
                return false;
            } else {

                var Compara01 = parseInt(data_1.split("/")[2].toString() + data_1.split("/")[1].toString() + data_1.split("/")[0].toString());
                var Compara02 = parseInt(data_2.split("/")[2].toString() + data_2.split("/")[1].toString() + data_2.split("/")[0].toString());

                if ((Compara01 < Compara02) || (Compara01 == Compara02)) {
                    // alert('Inicio: ' + Compara01 + '*** Fim: ' + Compara02);

                    //  alert("OK");
                    $('#ContentPlaceHolder1_hiddataini1').val(data_1);
                    $('#ContentPlaceHolder1_hiddataini2').val(data_2);

                    return true
                } else {
                    alert("Data final deve ser maior que a inicial!");
                    // alert('Inicio: ' + Compara01 + '*** Fim: ' + Compara02);
                    $('#ContentPlaceHolder1_hiddataini1').val(data_1);
                    $('#ContentPlaceHolder1_hiddataini2').val(data_2);

                    return false
                }
            }

        }

    </script>
    
        <ContentTemplate>

            <div class="full_w">
                <div class="h_title">
                    Carga Crédito de Reembolso
                </div>

                <div class="tabelaPagina">

                    <h2>Carga Crédito de Reembolso</h2>

                    <asp:HiddenField ID="hiddataini1" runat="server" Value="" />
                    <asp:HiddenField ID="hiddataini2" runat="server" Value="" />


                    <div id="divCargaReembolso">

                        <h3>Informe o período:
                        </h3>

                        <table>
                            <tr>
                                <td>
                                    <label class="txtCampoForm">
                                        De:&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox CssClass="date" ID="dataInicio" maxlength="10" onkeypress="mascaraData( this, event )"  runat="server" Width="150"></asp:TextBox>
                                    </label>

                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="txtCampoForm">
                                        Até: &nbsp;&nbsp;&nbsp;<asp:TextBox CssClass="date" ID="dataFim" maxlength="10" onkeypress="mascaraData( this, event )" runat="server"  Width="150"></asp:TextBox>

                                    </label>

                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Button CssClass="button" runat="server" ID="btngerar" Text="Processar >>" OnClientClick="return validaData();" OnClick="btngerar_Click" />
                                </td>

                                <td>
                                    <asp:Button CssClass="button" runat="server" ID="btnconsultar" OnClientClick="return validaData();" Text="Consultar >>" OnClick="btnconsultar_Click" />
                                </td>

                            </tr>




                        </table>
                        <div>
                            <asp:Label ID="txtVerifica" runat="server"></asp:Label>
                            <uc1:ReportCrystal runat="server" ID="ReportCrystal" Visible="false" />
                            <br />
                            <br />
                        </div>
                    </div>

                </div>
            </div>
           <asp:UpdatePanel runat="server" ID="upCadSus"> </asp:UpdatePanel>
        </ContentTemplate>
</asp:Content>
