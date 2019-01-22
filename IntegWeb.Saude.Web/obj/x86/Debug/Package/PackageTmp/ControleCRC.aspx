<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ControleCRC.aspx.cs" Inherits="IntegWeb.Saude.Web.ControleCRC" %>

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

            <h1>CRC - PAGAMENTO</h1>

            <asp:UpdatePanel runat="server" ID="upSys">
                <ContentTemplate>
                    <div id="divAction" runat="server">
                        <table>
                            <tr>
                                <td>Selecione o botão abaixo para processar o relatório para o mês atual:</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btProcessar" OnClientClick="postbackButtonClick();" runat="server" Text="Processar" CssClass="button" OnClick="btProcessar_Click" />

                                </td>
                            </tr>
                            <tr>
                                <td>Digite Mês/Ano referência para consultar:</td>
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
                                    <asp:Button ID="btnExel" runat="server" Text="Gerar Relatório" OnClick="btnExel_Click" CssClass="button" />
                                </td>

                            </tr>

                        </table>
                        <asp:GridView ID="grdCrc" OnRowCommand="grdCrc_RowCommand" OnPageIndexChanging="grdCrc_PageIndexChanging" runat="server" AutoGenerateColumns="false" EmptyDataText="A consulta não retornou dados" AllowPaging="true" PageSize="10">
                            <Columns>
                                <asp:BoundField HeaderText="Responsável" DataField="MATRICULA" />
                                <asp:BoundField HeaderText="Data" DataField="DT_INCLUSAO" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Horário" DataField="DT_INCLUSAO" DataFormatString="{0:T} " />
                                <asp:TemplateField>
                                    <ItemTemplate>

                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" CommandName="Exportar" CommandArgument='<%# Eval("DT_INCLUSAO") %>'>Gerar Excel</asp:LinkButton>
                                    </ItemTemplate>


                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btProcessar" />
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
                    <asp:Label ID="lblMesage" runat="server" Text="Exite um processamento para o mês atual, deseja continuar?" Font-Size="Medium" />
                </td>
            </tr>
    <%--        <tr>
                <td>
                    <asp:Label ID="lblMsgAviso" runat="server" Font-Size="Small" Text="Ao cliclar em CONTINUAR o processamento atual será apagado" /><br />
                </td>
            </tr>--%>

            <tr>
                <td>
                    <br />
                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Font-Size="Small" Text="Dados do Processamento atual:" /><br />
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
