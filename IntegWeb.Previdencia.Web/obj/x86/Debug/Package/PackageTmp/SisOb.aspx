<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="SisOb.aspx.cs" Inherits="IntegWeb.Previdencia.Web.SisOb" %>

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
            <asp:UpdatePanel runat="server" ID="upSys">
                <ContentTemplate>
                    <div id="divAction" runat="server">
                        <h1>Importação do arquivo SisOb</h1>
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

                                    <asp:Button ID="btnUpLoad" runat="server" OnClick="btnUpLoad_Click" Text="Importar" OnClientClick="return postbackButtonClick();" CssClass="button" />
                                </td>

                            </tr>
                            <tr>
                                <td>

                                    <h4>
                                        <asp:Label ID="contador" runat="server" Text="Número de Registros importados :"></asp:Label></h4>
                                    <h4>
                                        <asp:Label runat="server" ID="StatusLabel" Text="Upload Status: " /></h4>
                                </td>

                            </tr>


                        </table>
                    </div>
                    

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpLoad" />
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
</asp:Content>
