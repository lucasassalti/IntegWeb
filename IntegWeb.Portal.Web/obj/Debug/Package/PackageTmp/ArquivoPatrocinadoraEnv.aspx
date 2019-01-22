<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="ArquivoPatrocinadoraEnv.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ArquivoPatrocinadoraEnv" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table>
        <tr>
            <td>
                <div class="h_title" style="width: 600px;">
                    Troca de Arquivos > Receber arquivos
                </div>
            </td>
        </tr>
        <tr>

            <td>
                <asp:UpdatePanel runat="server" ID="upArqPatrocinadoras">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="PanelUpload" class="tabelaPagina" DefaultButton="btnExibir">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnExibir" CssClass="button" runat="server" Text="Exibir anteriores" OnClick="btnExibir_Click" />
                                        <asp:Button ID="btnVoltar" CssClass="button" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                    </td>
                                    <td>
                                        <div class="dialog-message-confirm" title="Carregar dados / Demonstrativo" id="pnl_carga" style="display: none;">
                                            <p>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Mês Ref."></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlMesRef" runat="server">
                                                                <asp:ListItem Text="" Value="0" />
                                                                <asp:ListItem Text="Janeiro" Value="1" />
                                                                <asp:ListItem Text="Fevereiro" Value="2" />
                                                                <asp:ListItem Text="Março" Value="3" />
                                                                <asp:ListItem Text="Abril" Value="4" />
                                                                <asp:ListItem Text="Maio" Value="5" />
                                                                <asp:ListItem Text="Junho" Value="6" />
                                                                <asp:ListItem Text="Julho" Value="7" />
                                                                <asp:ListItem Text="Agosto" Value="8" />
                                                                <asp:ListItem Text="Setembro" Value="9" />
                                                                <asp:ListItem Text="Outubro" Value="10" />
                                                                <asp:ListItem Text="Novembro" Value="11" />
                                                                <asp:ListItem Text="Dezembro" Value="12" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Ano Ref."></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAnoRef" runat="server">
                                                                <asp:ListItem Text="" Value="0" />
                                                                <asp:ListItem Text="2015" Value="2015" />
                                                                <asp:ListItem Text="2016" Value="2016" />
                                                                <asp:ListItem Text="2017" Value="2017" />
                                                                <asp:ListItem Text="2018" Value="2018" />
                                                                <asp:ListItem Text="2019" Value="2019" />
                                                                <asp:ListItem Text="2020" Value="2020" />
                                                                <asp:ListItem Text="2021" Value="2021" />
                                                                <asp:ListItem Text="2022" Value="2022" />
                                                                <asp:ListItem Text="2023" Value="2023" />
                                                                <asp:ListItem Text="2024" Value="2024" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Dt. do Repasse:&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDtRepasse" runat="server" Text="" CssClass="date" Width="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Dt. do Crédito:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDtCredito" runat="server" Text="" CssClass="date" Width="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Grupo:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlGrupoNovo" runat="server" Width="220px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </p>
                                        </div>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td colspan="2" rowspan="2" style="text-align: center; height: 10px;">
                                        <asp:Label ID="TbUpload_Mensagem" runat="server" Visible="False" CssClass="pnlMensagem"></asp:Label>

                                        <span id="TbUpload_Mensagem_client" class="pnlMensagem" style="display: none;">
                                            <div class="n_warning">
                                                <p>&nbsp;</p>
                                            </div>
                                        </span>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="pnlMensagem">
                                        <div style="font-size: small; display: none;">
                                            &nbsp&nbsp<asp:CheckBox ID="chkSobreporTodos" runat="server" Checked="false" Text="Sobrepor arquivos idênticos" />
                                        </div>
                                    </td>
                                    <td colspan="3">&nbsp
                                    </td>
                                </tr>
                                <tr style="font: 14px 'Open Sans', Arial, sans-serif">
                                    <td>
                                        <h3>Exibir:</h3>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rExibir" RepeatDirection="Horizontal" AutoPostBack="true" Style="margin: 0px; border-color: #f9d9d9; border-style: solid;" OnSelectedIndexChanged="rExibir_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Recentes"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Filtrar"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:HiddenField runat="server" ID="hidGrupos" />
                                    </td>
                                    <td>
                                        <%--<h3>Agrupar por:</h3>--%>
                                    </td>
                                    <td>
                                        <h3>&nbsp</h3>
                                    </td>
                                    <td>
                                        <h3>&nbsp</h3>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrupo" runat="server" Visible="false">
                                            <asp:ListItem Text="<TODOS>" Value="9999" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                            <table id="trFiltro" runat="server" visible="false" >
                                <tr>
                                    <td>
                                        <h2>Filtro:</h2>
                                    </td>
                                    <td>
                                        <h3>Período de:</h3>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDatIni" runat="server" Text="" CssClass="date" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <h3>até</h3>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDatFim" runat="server" Text="" CssClass="date" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <h3>Arquivo</h3>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReferencia" runat="server" Text="" Width="100px"></asp:TextBox>
                                        <asp:TextBox ID="txtExtensao" runat="server" Text="" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <h3>Tipo</h3>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipo" runat="server"></asp:DropDownList>
                                        <asp:TextBox ID="txtAno" runat="server" Text="" Width="100px" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtMes" runat="server" Text="" Width="100px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btFiltrar" runat="server" Text="Carregar verba" ImageUrl="~/img/i_search.png" Width="20px" Height="20px" AlternateText="Aplicar Filtro" ToolTip="Aplicar Filtro" OnClick="btFiltrar_Click"/>
                                        <asp:ImageButton ID="btLimparFiltro" runat="server" Text="Carregar verba" ImageUrl="~/img/delete_2.png" Width="20px" Height="20px" AlternateText="Limpar Filtro" ToolTip="Limpar Filtro" OnClick="btLimparFiltro_Click"/>
                                    </td>
                                </tr>
                            </table>

                            <asp:ObjectDataSource runat="server" ID="odsArqPatrocinadora"
                                TypeName="IntegWeb.Previdencia.Aplicacao.BLL.ArqPatrocinadoraEnvioBLL"
                                SelectMethod="GetDataPortal"
                                SelectCountMethod="GetDataPortalCount"
                                EnablePaging="true"
                                SortParameterName="sortParameter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="rExibir" Name="pExibir" PropertyName="SelectedValue" Type="Int32" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="txtDatIni" Name="datIni" PropertyName="Text" Type="DateTime" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="txtDatFim" Name="datFim" PropertyName="Text" Type="DateTime" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="hidGrupos" Name="grupos" PropertyName="Value" Type="String" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="txtAno" Name="ano" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="txtMes" Name="mes" PropertyName="Text" Type="Int16" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="txtExtensao" Name="ext" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="txtReferencia" Name="referencia" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />
                                    <asp:ControlParameter ControlID="ddlTipo" Name="tipoEnvio" PropertyName="SelectedValue" Type="Int16" ConvertEmptyStringToNull="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:GridView ID="grdArqEnviados" runat="server" Visible="false"
                                DataKeyNames="COD_ARQ_ENVIO"
                                OnRowCreated="GridView_RowCreated"
                                OnRowDeleted="GridView_RowDeleted"
                                OnRowCancelingEdit="GridView_RowCancelingEdit"
                                OnRowCommand="grdArqEnviados_RowCommand"
                                AllowPaging="true"
                                AllowSorting="true"
                                AutoGenerateColumns="False"
                                EmptyDataText="A consulta não retornou registros"
                                CssClass="Table"
                                PagerStyle-CssClass="GridViewPager"
                                ClientIDMode="Static"
                                PageSize="8"
                                DataSourceID="odsArqPatrocinadora"
                                PagerStyle-Font-Size="Medium"
                                PagerStyle-Font-Bold="true"
                                PagerSettings-PageButtonCount="4"
                                PagerSettings-Mode="NumericFirstLast"
                                PagerSettings-FirstPageText="<<"
                                PagerSettings-LastPageText=">>">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" Text="" onchange="return chkSelect_click();" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" Checked="false" runat="server" Text='' class="span_checkbox" onchange="return chkSelect_click();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Disponivel desde" DataField="DTH_INCLUSAO" SortExpression="DTH_INCLUSAO" />
                                    <asp:BoundField HeaderText="Arquivo" DataField="DCR_ARQ_ENVIO" SortExpression="DCR_ARQ_ENVIO" />
                                    <asp:BoundField HeaderText="Tipo" DataField="DCR_ARQ_ENVIO_TIPO" SortExpression="DCR_ARQ_ENVIO_TIPO" />
                                    <asp:BoundField HeaderText="Status" DataField="DCR_ARQ_STATUS" SortExpression="DCR_ARQ_STATUS" />
                                    <asp:BoundField HeaderText="Grupo Portal" DataField="DCR_GRUPO_EMPRS" SortExpression="DCR_GRUPO_EMPRS" />
                                    <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btDownReport" CssClass="button" runat="server" Text="Download" CommandName="Download" CommandArgument='<%# Eval("COD_ARQ_ENVIO") %>' ImageUrl="~/img/pdf-icon.png" Width="24px" AlternateText="Download" ToolTip="Download" Visible='<%# short.Parse(Eval("COD_ARQ_ENVIO_TIPO").ToString())==1 %>' Style="background-color: White; padding: 0px;"/>
                                            <asp:ImageButton ID="btDownTxt" CssClass="button" runat="server" Text="Download" CommandName="Download" CommandArgument='<%# Eval("COD_ARQ_ENVIO") %>' ImageUrl="~/img/text-icon.png" Width="24px" AlternateText="Download" ToolTip="Download" Visible='<%# short.Parse(Eval("COD_ARQ_ENVIO_TIPO").ToString())==2 %>' Style="background-color: White; padding: 0px;"/>
                                            <asp:ImageButton ID="btDownOutros" CssClass="button" runat="server" Text="Download" CommandName="Download" CommandArgument='<%# Eval("COD_ARQ_ENVIO") %>' ImageUrl="~/img/report-icon.png" Width="24px" AlternateText="Download" ToolTip="Download" Visible='<%# short.Parse(Eval("COD_ARQ_ENVIO_TIPO").ToString())==3 && !Eval("DCR_ARQ_EXT").ToString().Equals("zip") %>' Style="background-color: White; padding: 0px;"/>
                                            <asp:ImageButton ID="btDownZip" CssClass="button" runat="server" Text="Download" CommandName="Download" CommandArgument='<%# Eval("COD_ARQ_ENVIO") %>' ImageUrl="~/img/zip-icon.png" Width="24px" AlternateText="Download" ToolTip="Download" Visible='<%# short.Parse(Eval("COD_ARQ_ENVIO_TIPO").ToString())==3 && Eval("DCR_ARQ_EXT").ToString().Equals("zip") %>' Style="background-color: White; padding: 0px;"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
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
            </td>
        </tr>
    </table>
</asp:Content>
