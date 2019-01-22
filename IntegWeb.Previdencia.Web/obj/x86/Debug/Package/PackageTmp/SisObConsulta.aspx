<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="SisObConsulta.aspx.cs" Inherits="IntegWeb.Previdencia.Web.SisObConsulta" %>

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

                    <div id="divSelect" runat="server" class="MarginGrid">
                        <h2>Histórico de Importação</h2>
                        <table>
                            <tr>
                                <td>Digite mês/ano para filtrar:</td>
                                <td>
                                    <asp:TextBox ID="txtArquivo" runat="server" Width="100px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="button" CausesValidation="false" OnClick="btnPesquisar_Click" />
                                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar Pesquisa" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="grdArquivo" OnPageIndexChanging="grdArquivo_PageIndexChanging" runat="server" AllowPaging="True" PageSize="10" EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="mesanoref" HeaderText="Mês/Ano" />
                                <asp:BoundField DataField="quantidade" HeaderText="Registros" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="responsavel" HeaderText="Responsável" />
                                <asp:BoundField DataField="dt_inclusao" HeaderText="Data" DataFormatString="{0:dd/M/yyyy}" />
                                <asp:BoundField DataField="dt_inclusao" HeaderText="Hora" DataFormatString="{0:T}" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <h2>Dados da Importação</h2>
                        <table>
                            <tr>
                                <td>
                                    <%--<asp:DropDownList ID="drpSys" runat="server">
                                        <asp:ListItem Text="Selecione" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Nome do Falecido" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="CPF" Value="2"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                    Nome do Falecido:</td>
                                <td>
                                    <asp:TextBox ID="txtNomeFalecido" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>CPF:</td>
                                <td>
                                    <asp:TextBox ID="txtCpf" runat="server" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Nome do Mãe:</td>
                                <td>
                                    <asp:TextBox ID="txtNomeMae" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>Dt. Nascimento:</td>
                                <td>
                                    <asp:TextBox ID="txtDtNasc" runat="server" Width="100px" CssClass="date"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnFind" runat="server" Text="Pesquisar" CssClass="button" OnClick="btnFind_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="grdOb" OnPageIndexChanging="grdOb_PageIndexChanging" runat="server" AutoGenerateColumns="false" EmptyDataText="A consulta não retornou dados" AllowPaging="true" PageSize="50">
                            <Columns>
                                <asp:BoundField HeaderText="LIVRO" DataField="Livro" />
                                <asp:BoundField HeaderText="FOLHA" DataField="Folha" />
                                <asp:BoundField HeaderText="TERMO" DataField="Termo" />
                                <asp:BoundField HeaderText="DTCERTIDAO" DataField="DTCERTIDAO" />
                                <asp:BoundField HeaderText="NBENEFICIO" DataField="NBENEFICIO" />
                                <asp:BoundField HeaderText="NOMEFALEC" DataField="NOMEFALEC" />
                                <asp:BoundField HeaderText="NOMEMAE" DataField="NOMEMAE" />
                                <asp:BoundField HeaderText="DTNASC" DataField="DTNASC" />
                                <asp:BoundField HeaderText="DTOBITO" DataField="DTOBITO" />
                                <asp:BoundField HeaderText="CPF" DataField="CPF" />
                                <asp:BoundField HeaderText="NIT" DataField="NIT" />
                                <asp:BoundField HeaderText="TIPOIDCART" DataField="TIPOIDCART" />
                                <asp:BoundField HeaderText="IDCART" DataField="IDCART" />
                                <asp:BoundField HeaderText="FILLER" DataField="FILLER" />
                                <asp:BoundField HeaderText="MESANOREF" DataField="MESANOREF" />
                                <asp:BoundField HeaderText="MATRICULA" DataField="MATRICULA" />
                                <asp:BoundField HeaderText="Data" DataField="DT_INCLUSAO" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Horário" DataField="DT_INCLUSAO" DataFormatString="{0:T} " />

                            </Columns>
                        </asp:GridView>
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
        </div>
    </div>
</asp:Content>
