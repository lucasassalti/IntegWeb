<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="false" CodeBehind="CargaUtilizacoes.aspx.cs" Inherits="IntegWeb.Saude.Web.CargaUtilizacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function validaData() {

            $('.n_error').remove();
            $('.n_warning').remove();

            var data_1 = $('#ContentPlaceHolder1_dataMovimento').val();
            if (data_1 == '') {
                //alert('Informe a data de movimento!')
                $('#ContentPlaceHolder1_txtVerifica').append('<div class="n_warning"><p>Informe a data de movimento!</p></div>');
                return false;
            } else if (data_1 == '') {
                    //alert('Informe a data de movimento!')
                    $('#ContentPlaceHolder1_txtVerifica').append('<div class="n_warning"><p>Informe a data de movimento!</p></div>');
                    return false;
            } else if (!isDate(data_1)) {
                //alert('A Data de movimento informada é inválida!')
                $('#ContentPlaceHolder1_txtVerifica').append('<div class="n_error"><p>A data de movimento informada é inválida!</p></div>');
                return false;
            } else {
                $('#ContentPlaceHolder1_hiddataini1').val(data_1);
            }
        }

    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="full_w">
                <div class="h_title">
                    Carga Extrato de Utilização
                </div>
                <div class="tabelaPagina">
                    <h2>Carga Extrato de Utilização</h2>
                    <asp:HiddenField ID="hiddataini1" runat="server" Value="" />
                    <div id="divCargaExtrato">
                        <h3>Informe o período:
                        </h3>

                        <table>
                            <tr>
                                <td>
                                    <label class="txtCampoForm">
                                        Data de Movimento:&nbsp;&nbsp;
                                        <asp:TextBox CssClass="date" ID="dataMovimento" onkeypress="mascara(this, data)" runat="server" Width="150" ></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="dataMovimento" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                            <tr>
                                <td>
                                    <asp:Button CssClass="button" runat="server" ID="btngerar" Text="Processar >>" OnClientClick="return validaData();" OnClick="btngerar_Click" CausesValidation="true" />
                                </td>
                            </tr>
                        </table>
                        <div>
                            <asp:Label ID="txtVerifica" runat="server"></asp:Label>
                            <br />
                            <br />
                        </div>
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
                    <br /><br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
