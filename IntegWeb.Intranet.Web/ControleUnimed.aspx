<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ControleUnimed.aspx.cs" Inherits="IntegWeb.Intranet.Web.ControleUnimed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Controle Unimed</h1>

                    <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                       <%-- <asp:ObjectDataSource ID="odsControleUnimed"
                            runat="server"
                            TypeName="IntegWeb.Intranet.Aplicacao.DAL.ControleUnimedCrmDAL"
                            SelectMethod="GetData"
                            SelectCountMethod="GetDataCount"
                            EnablePaging="True"
                            SortParameterName="sortParameter">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="nempr" QueryStringField="nempr" />
                                <asp:QueryStringParameter Name="nreg" QueryStringField="nreg" />
                                <asp:QueryStringParameter Name="sub" QueryStringField="sub" />
                                <asp:QueryStringParameter Name="pessDsCpf" QueryStringField="pessDsCpf" />
                            </SelectParameters>
                        </asp:ObjectDataSource>--%>
                        <div>
                            <asp:GridView ID="grdControleUnimed"
                                runat="server"
                                AutoGenerateColumns="False"
                                EmptyDataText="Não retornou registros"
                                AllowPaging="True"
                                CssClass="Table"
                                ClientIDMode="Static"
                                Font-Size="13px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nome" SortExpression="NOM_PARTICIP" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNome" runat="server" Text='<%# Bind("NomeParticipanteUNIMED") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operadora" SortExpression="DES_PLANO" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesUnimed" runat="server" Text='<%# Bind("DES_PLANO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Movimentação" SortExpression="TIPO" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMovimentacao" runat="server" Text='<%# Bind("TIPO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inclusão na Operadora" SortExpression="DAT_GERACAO" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDatMovimentacao" runat="server" Text='<%# Bind("DAT_GERACAO","{0:dd/MM/yyyy}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Postagem Funcesp" SortExpression="ENVIO_UNIMED" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDatSaida" runat="server" Text='<%# Bind("ENVIO_UNIMED","{0:dd/MM/yyyy}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nº do cartão " SortExpression="NUMERO_UNIMED" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumeroUnimed" runat="server" Text='<%# Bind("NUMERO_UNIMED") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div id="carregando">
                <div class="carregandoTxt">
                    <img src="img/processando.gif" />
                    <br />
                    <br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
