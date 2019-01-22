<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="SimulacaoBD.aspx.cs" Inherits="IntegWeb.Previdencia.Web.SimulacaoBD" %>

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


            <h1>Importação de Arquivos (Simulação BD)</h1>



            <asp:UpdatePanel runat="server" ID="upSimulacao">
                <ContentTemplate>
                    <table>

                        <tr>
                            <td>
                                <h3>Selecione uma planilha Excel</h3>


                                <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                <asp:Button runat="server" OnClientClick="return postbackButtonClick();" ID="UploadButton" Text="Importar arquivo" OnClick="UploadButton_Click" CssClass="button" />
                                <asp:Button ID="btnConsultar" CssClass="button" runat="server" Text="Consultar últimos dados importados" OnClick="btnConsultar_Click" />
                            </td>
                        </tr>
                    </table>
                    <div id="divselect" runat="server" visible="false">
                        <h3>Dados da Importação</h3>
                        <table id="tblDados" runat="server" visible="false">
                            <tr>
                                <td>Responsável pela importação:</td>
                                <td>
                                    <asp:Label ID="lblResponsavel" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Data:</td>
                                <td>
                                    <asp:Label ID="lblData" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Horário:</td>
                                <td>
                                    <asp:Label ID="lblHorario" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                        </table>
                        <h3>Últimos dados importados</h3>
                    
                        <asp:GridView ID="grdSim" OnPageIndexChanging="grdSim_PageIndexChanging" runat="server" AllowPaging="True" PageSize="20" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="EMPRESA" HeaderText="EMPRESA" />
                                <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" />
                                <asp:BoundField DataField="PLANO" HeaderText="PLANO" />
                                <asp:BoundField DataField="MATRICULA" HeaderText="MATRICULA" />
                            </Columns>
                        </asp:GridView>

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
</asp:Content>
