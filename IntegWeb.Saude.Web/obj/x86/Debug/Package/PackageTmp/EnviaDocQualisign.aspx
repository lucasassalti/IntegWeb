<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="EnviaDocQualisign.aspx.cs" Inherits="IntegWeb.Saude.Web.EnviaDocQualisign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Envio Documentos QualiSign</h1>
            <asp:UpdatePanel runat="server" ID="upArqPatrocinadoras">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="PanelUpload" class="tabelaPagina">
                        <table>
                            <tr>
                                <td>Referência:</td>
                                <td><asp:TextBox ID="txtArqRef" runat="server" Style="width: 100px;"></asp:TextBox></td>
                                <td>Nome Arq.:</td>
                                <td><asp:TextBox ID="txtNomeArquivo" runat="server"  Style="width: 100px;"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Período de envio</td>
                                <td>
                                    <asp:TextBox ID="txtDtIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                    até&nbsp&nbsp&nbsp
                                    <asp:TextBox ID="txtDtFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                </td>
                                <td>Situação:</td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value=""  Text="<TODOS>" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="A" Text="Vigente / Ativo"></asp:ListItem>
                                        <asp:ListItem Value="B" Text="Bloqueado"></asp:ListItem>
                                        <asp:ListItem Value="C" Text ="Cancelado"></asp:ListItem>
                                        <asp:ListItem Value="D" Text="Em Definição de Papéis"></asp:ListItem>
                                        <asp:ListItem Value="G" Text="Aguardando ativação de outro contrato"></asp:ListItem>
                                        <asp:ListItem Value="P" Text="Pendente de assinatura/ aprovação/aceite"></asp:ListItem>
                                        <asp:ListItem Value="R" Text="Recusado"></asp:ListItem>
                                        <asp:ListItem Value="T" Text="Aguardando Carimbo de Tempo"></asp:ListItem>
                                        <asp:ListItem Value="V" Text="Vencido"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <div class="dialog-message-popup" title="Definir CPF para envio" id="pnl_carga" style="display: none;">
                                        <p>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCpfRepres" runat="server" Text="" Width="100px" MaxLength="14" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="float:left;">
                                        <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" AllowMultiple="true" />
                                    </div>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDefinirCpf" CssClass="button" runat="server" Text="Definir CPF" OnClientClick="$('#pnl_carga').dialog('open'); return false;" />
                                    <asp:Button ID="btnProcessar" CssClass="button" runat="server" Text="Processar" OnClick="btnProcessar_Click" style="display: none;" />
                                    <asp:Button ID="btnAtualizarStatus" CssClass="button" runat="server" Text="Atualizar Status" OnClick="btnAtualizarStatus_Click"  />
                                    <asp:Button ID="btnPesquisar" CssClass="button" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click"  />
                                    <asp:Button ID="btnLimpar" CssClass="button" runat="server" Text="Limpar" OnClick="btnLimpar_Click"  />
                                </td>
                                <td rowspan="2" colspan="2">
                                    <asp:Label ID="TbUpload_Mensagem" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <asp:ObjectDataSource runat="server" ID="odsDocsEnviados"
                            TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.WsQualiSignBLL"
                            SelectMethod="GetData"
                            SelectCountMethod="GetDataCount"
                            EnablePaging="true"
                            SortParameterName="sortParameter">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtArqRef" Name="pArqRef" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />
                                <asp:ControlParameter ControlID="txtNomeArquivo" Name="pNomeArquivo" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />
                                <asp:ControlParameter ControlID="txtDtIni" Name="pDtIni" PropertyName="Text" Type="DateTime" ConvertEmptyStringToNull="true" />
                                <asp:ControlParameter ControlID="txtDtFim" Name="pDtFim" PropertyName="Text" Type="DateTime" ConvertEmptyStringToNull="true" />
                                <asp:ControlParameter ControlID="ddlStatus" Name="pStatus" PropertyName="SelectedValue" Type="String" ConvertEmptyStringToNull="true" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:GridView ID="grdDocsEnviados" runat="server"
                            DataKeyNames="COD_QUALISIGN_DOC"
                            OnRowCreated="GridView_RowCreated"
                            OnRowDeleted="GridView_RowDeleted"
                            OnRowCancelingEdit="GridView_RowCancelingEdit"
                            OnRowDataBound="grdDocsEnviados_RowDataBound"
                            DataSourceID="odsDocsEnviados"
                            AllowPaging="true"
                            AllowSorting="true"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            ClientIDMode="Static"
                            PageSize="8"                                            
                            PagerStyle-Font-Size="Medium"
                            PagerStyle-Font-Bold="true">
                            <Columns>                                                
                                <asp:BoundField HeaderText="Dt. Envio" DataField="DTH_INCLUSAO" SortExpression="DTH_INCLUSAO" />
                                <asp:BoundField HeaderText="Ref. Arquivo " DataField="NOM_REF_ARQUIVO" SortExpression="NOM_REF_ARQUIVO" />
                                <asp:BoundField HeaderText="Arquivo" DataField="NOM_ARQUIVO"  SortExpression="NOM_ARQUIVO" />
                                <asp:BoundField HeaderText="Nº lote" DataField="COD_QUALISIGN" SortExpression="COD_QUALISIGN" />
                                <asp:TemplateField HeaderText="Situação" SortExpression="COD_STATUS">
                                    <ItemTemplate>                                                        
                                        <asp:Label ID="lblSituacao" runat="server" Text='<%# Eval("COD_STATUS") %>' ForeColor='<%# (!Eval("COD_RETORNO").ToString().Equals("0")) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retorno" SortExpression="DCR_RETORNO">
                                    <ItemTemplate>                                                        
                                        <asp:Label ID="lblRetorno" runat="server" Text='<%# Eval("DCR_RETORNO") %>' ForeColor='<%# (!Eval("COD_RETORNO").ToString().Equals("0")) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="PassCode" DataField="DCR_PASSCODE" SortExpression="DCR_PASSCODE" />
                            </Columns>
                        </asp:GridView>                                        

                    </asp:Panel>
                </ContentTemplate>
                <%--<Triggers>
                    <asp:PostBackTrigger ControlID="btnVoltar" />
                </Triggers>--%>
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
            <script type="text/javascript" src="js/build_uploadify.js"> </script>
        </div>
    </div>
</asp:Content>
