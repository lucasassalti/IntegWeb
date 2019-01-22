<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ControleAudit.aspx.cs" Inherits="IntegWeb.Saude.Web.ControleAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">

            <h2>Importação de Dados (Auditoria técnica de campo).</h2>

            <asp:UpdatePanel runat="server" ID="upAudit">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Selecione o arquivo para importação:</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />

                            </td>
                        </tr>
                        <tr>
                            <td >Selecione a Empresa:</td>
                        </tr>
                        <tr>
                            <td >
                                <asp:DropDownList ID="drpEmpresa" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Digite Mês/Ano referência para importação do arquivo:</td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>Mês
                                        </td>
                                        <td>Ano
                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="drpMes" runat="server">
                                                <asp:ListItem Selected="True" Text="--Selecine--" Value="0">
                                       
                                                </asp:ListItem>
                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            </asp:DropDownList>

                                        <td>
                                            <asp:TextBox ID="txtAno" AutoPostBack="true" runat="server" Width="50px" MaxLength="4" onkeypress="mascara(this, soNumeros)"></asp:TextBox></br>
                                      
                                        </td>
                                    </tr>

                                </table>

                                <asp:Button runat="server" OnClick="UploadButton_Click" OnClientClick="return postbackButtonClick();" ID="UploadButton" Text="Importar arquivo" CssClass="button" />
                            </td>

                        </tr>


                    </table>


                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="UploadButton" />
                </Triggers>
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
        </div>
    </div>
    <a href="#DivMessage" class="fancybox" id="lnkErro" style="display: none" runat="server"></a>

    <div style="margin: 6px; display: none" id="DivMessage">
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lbAviso" runat="server" Text="Atenção!" Font-Bold="true" Font-Size="Large" />
                </td>
            </tr>
            <tr>
                <td>
                    <h3>Coluna(s) não encontrada(s):<asp:Label runat="server" ID="lbColuns" /></h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>Verifique se o cabeçalho das colunas carregadas estão de acordo com o formato abaixo:</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="img/planilha.png" style="width: 1000px; height: 300px" />
                </td>
            </tr>
        </table>
    </div>

    <a href="#DivExists" class="fancybox" id="lnkErro1" style="display: none" runat="server"></a>

    <div style="margin: 6px; display: none" id="DivExists">
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="Atenção!" Font-Bold="true" Font-Size="Large" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMesage" runat="server" Text="Exite um processamento para o mês/ano escolhido e será sobrescrevido. Deseja continuar?" Font-Size="Medium" />
                </td>
            </tr>

            <tr>
                <td>
                    <br />
                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Font-Size="Small" Text="Dados do Processamento:" /><br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblResponsavel" runat="server" Font-Size="Small" /><br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblData" runat="server" Font-Size="Small" /><br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHorario" runat="server" Font-Size="Small" /><br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnOk" runat="server" Text="Continuar" OnClientClick="$.fancybox.close();postbackButtonClick();" OnClick="btnOk_Click" CssClass="button" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
