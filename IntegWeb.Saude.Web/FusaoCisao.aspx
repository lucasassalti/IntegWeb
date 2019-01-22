<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="FusaoCisao.aspx.cs" Inherits="IntegWeb.Saude.Web.FusaoCisao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel runat="server" ID="upCisao">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsCisao" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="h_title">
                </div>
                <h1>Cisão / Fusão</h1>

                <ajax:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <ajax:TabPanel ID="TabPanel1" HeaderText="Buscar Matrículas" runat="server" TabIndex="1">
                        <ContentTemplate>
                            <div class="tabelaPagina">
                                <table>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnConsultar" CssClass="button" CausesValidation="false" OnClick="btnConsultar_Click" runat="server" Text="Consultar" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView AllowPaging="true"
                                    AutoGenerateColumns="False" EmptyDataText="A consulta não retornou registros" CssClass="Table" ClientIDMode="Static" ID="grdFusao" runat="server" OnPageIndexChanging="grdFusao_PageIndexChanging" PageSize="10">
                                    <Columns>
                                        <asp:BoundField HeaderText="Empresa Anterior    " DataField="COD_EMPRS_ANT" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Matrícula Anterior" DataField="NUM_RGTRO_EMPRG_ANT" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Empresa Atual" DataField="COD_EMPRS_ATU" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Matrícula Atual" DataField="NUM_RGTRO_EMPRG_ATU" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Dígito" DataField="NUM_DIGVER_ATU" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Data Base Cisão" DataField="DAT_BASE_CISAO" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Data Atualização" DataField="DAT_ATUALIZACAO" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />

                                    </Columns>

                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel ID="TabPanel2" HeaderText="Processar" runat="server" TabIndex="2">
                        <ContentTemplate>
                            <div class="tabelaPagina">
                                <table>
                                    <tr>
                                        <td>Para executar o processo de CISÃO/FUSÃO selecione o Botão abaixo:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnProcessar" CssClass="button" CausesValidation="false" OnClick="btnProcessar_Click" runat="server" Text="Processar..." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Para verificar os últimos processamentos selecione o Botão abaixo:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnVerifica" CssClass="button" CausesValidation="false" OnClick="btnVerifica_Click" runat="server" Text="Consultar" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView AutoGenerateColumns="False" EmptyDataText="Não existem processamentos" CssClass="Table" ClientIDMode="Static" ID="grdProcesso" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdProcesso_PageIndexChanging">

                                    <Columns>
                                        <asp:BoundField HeaderText="Responsável" DataField="matricula" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Status" DataField="status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Data" DataField="dt_inclusao" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                        <asp:BoundField HeaderText="Horário" DataField="dt_inclusao" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:T}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                </ajax:TabContainer>

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
