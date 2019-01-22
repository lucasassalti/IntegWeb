<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RecadastramentoBase.aspx.cs" Inherits="IntegWeb.Previdencia.Web.RecadastramentoBase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
        <h1>Base de Recadastramento Anual</h1>
        <div class="MarginGrid">
            <asp:UpdatePanel runat="server" ID="upRecadastramento">
                <ContentTemplate>
                    <ajax:tabcontainer id="TabContainer" runat="server" activetabindex="0">
                        <ajax:TabPanel ID="TbImportar" HeaderText="Importar TXT" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <h3>Entre com o arquivo recebido</h3>
                                            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                            <asp:Button ID="btnUpload" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" Text="Importar arquivo" OnClick="btnUpload_Click" />
                                            <%--<asp:Button ID="btnConsultar" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" Text="Consultar anteriores" OnClick="btnConsultar_Click" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label id="TbImportar_Mensagem" runat="server" visible="False"></asp:Label> 
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TbExportar" HeaderText="Exportar TXT" runat="server" TabIndex="0">
                            <ContentTemplate> 
                              <div class="tabelaPagina">   
                                                       
                                <table>
                                    <tr>
                                        <td>Período</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDtRefIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDtRefIni" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                        até&nbsp&nbsp&nbsp
                                            <asp:TextBox ID="txtDtRefFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server" ControlToValidate="txtDtRefFim" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Núm. Contrato</td>
                                        <td>
                                            <asp:TextBox ID="txtNumContrato" runat="server" Style="width: 100px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnConsultar" Text="Consultar"  CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" OnClick="btnConsultar_Click" />
                                            <asp:Button ID="btnLimpar" Text="Limpar" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();"  OnClick="btnLimpar_Click" />
                                        </td>
                                        <td> 
                                            <asp:Label id="TbExportar_Mensagem" runat="server" visible="False"></asp:Label> 
                                        </td> 

                                    </tr>
                                </table>

                                <asp:ObjectDataSource runat="server" ID="odsLstRecadastramento1"
                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.RecadastramentoBLL"
                                    SelectMethod="GetRecad_base"
                                    SelectCountMethod="GetRecad_baseCount"
                                    EnablePaging="True"
                                    SortParameterName="sortParameter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtDtRefIni" Name="pDtRef_ini" PropertyName="Text" Type="DateTime" ConvertEmptyStringToNull="true"/>
                                        <asp:ControlParameter ControlID="txtDtRefFim" Name="pDtRef_fim" PropertyName="Text" Type="DateTime" ConvertEmptyStringToNull="true"/>
                                        <asp:ControlParameter ControlID="txtNumContrato" Name="pNUM_CONTRATO" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true"/>
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdRecadastramento1" runat="server" 
                                    DataKeyNames="DAT_REF_RECAD"
                                    OnRowCancelingEdit="GridView_RowCancelingEdit" 
                                    OnRowCreated="GridView_RowCreated"
                                    OnRowCommand="grdRecadastramento1_RowCommand"
                                    AllowPaging="True" 
                                    AllowSorting="True"
                                    AutoGenerateColumns="False" 
                                    EmptyDataText="A consulta não retornou registros" 
                                    CssClass="Table" 
                                    ClientIDMode="Static" 
                                    PageSize="8" 
                                    DataSourceID="odsLstRecadastramento1">
                                    <PagerSettings
                                        PreviousPageText="Anterior"
                                        NextPageText="Próxima"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:BoundField DataField="DAT_REF_RECAD" HeaderText="Dt. Base Ref." SortExpression="DAT_REF_RECAD" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="NUM_CONTRATO" HeaderText="Núm. Contrato" SortExpression="NUM_CONTRATO" />
                                        <%--<asp:BoundField DataField="DTH_INCLUSAO" HeaderText="Dt. Criação Base" SortExpression="DTH_INCLUSAO" DataFormatString="{0:dd/MM/yyyy}" />--%>
                                        <asp:BoundField DataField="TOTAL_REGISTROS" HeaderText="Total de registros" SortExpression="TOTAL_REGISTROS" />

                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btExportar" CausesValidation="false" CssClass="button" Font-Size="X-Small" runat="server" Text="Exportar Todos" CommandName="Exportar" CommandArgument='<%# Eval("DAT_REF_RECAD") +","+ Eval("NUM_CONTRATO") + ", false" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btExportarPendentes" CausesValidation="false" CssClass="button" Font-Size="X-Small" runat="server" Text="Exportar Pendentes" CommandName="Exportar" CommandArgument='<%# Eval("DAT_REF_RECAD") +","+ Eval("NUM_CONTRATO") + ", true" %>' ToolTip="Exporta apenas os registros sem data de recadastramento preenchida" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Dt. Vencimento" SortExpression="DTA_VENCIMENTO" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDTA_VENCIMENTO" runat="server" Text='<%# (!String.IsNullOrEmpty(Eval("DTA_VENCIMENTO").ToString().Trim()) ? Eval("DTA_VENCIMENTO").ToString().Substring(6,2) + "/" + Eval("DTA_VENCIMENTO").ToString().Substring(4,2) + "/" + Eval("DTA_VENCIMENTO").ToString().Substring(0,4) : Eval("DTA_VENCIMENTO"))  %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Valor" SortExpression="VLR_DEBITO" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVALOR" runat="server" Text='<%# (Eval("ID_TP_REGISTRO").Equals("F") ? decimal.Parse(Eval("VLR_DEBITO").ToString().Insert(13,",")).ToString() : Eval("VLR_DEBITO"))  %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Retorno" SortExpression="COD_MOTIVO_RET">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRETORNO" runat="server" Text='<%# Eval("COD_MOTIVO_RET") + " - " + (Eval("AAT_TBL_RET_DEB_CONTA_MOTIVO.DESC_MOTIVO") ?? "").ToString() %>' ForeColor='<%# ((Eval("COD_MOTIVO_RET") ?? "00").Equals("00")) ? System.Drawing.Color.Black : System.Drawing.Color.Red %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                    </Columns>
                                </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="TbGerar" HeaderText="Gerar Base Anual" runat="server" TabIndex="0">
                            <ContentTemplate> 
                              <div class="tabelaPagina">   
                                <table>
                                    <tr>
                                        <td>Dt. Base</td>
                                        <td>
                                            <asp:TextBox ID="txtDtBase" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" runat="server" ControlToValidate="txtDtBase" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Núm. Contrato</td>
                                        <td>
                                            <asp:TextBox ID="txtNumContratoGerar" runat="server" Style="width: 100px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnGerar" Text="Gerar base" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" OnClick="btnGerar_Click" />
                                            <asp:Button ID="btnLimparGerar" Text="Limpar Campos" CssClass="button" runat="server" OnClientClick="return postbackButtonClick();" OnClick="btnLimparGerar_Click" />
                                        </td>
                                        <td> 
                                            <asp:Label id="TbGerar_Mensagem" runat="server" visible="False"></asp:Label> 
                                        </td> 
                                    </tr>
                                </table>

                                <asp:ObjectDataSource runat="server" ID="odsLstRecadastramento2"
                                    TypeName="IntegWeb.Previdencia.Aplicacao.BLL.RecadastramentoBLL"
                                    SelectMethod="GetRecad_base"
                                    SelectCountMethod="GetRecad_baseCount"
                                    EnablePaging="True"
                                    SortParameterName="sortParameter">
                                </asp:ObjectDataSource>

                                <asp:GridView ID="grdRecadastramento2" runat="server" 
                                    DataKeyNames="DAT_REF_RECAD"
                                    OnRowCancelingEdit="GridView_RowCancelingEdit" 
                                    OnRowCreated="GridView_RowCreated"
                                    AllowPaging="True" 
                                    AllowSorting="True"
                                    AutoGenerateColumns="False" 
                                    EmptyDataText="A consulta não retornou registros" 
                                    CssClass="Table" 
                                    ClientIDMode="Static" 
                                    PageSize="8" 
                                    DataSourceID="odsLstRecadastramento2">
                                    <PagerSettings
                                        PreviousPageText="Anterior"
                                        NextPageText="Próxima"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:BoundField DataField="DAT_REF_RECAD" HeaderText="Dt. Base Ref." SortExpression="DAT_REF_RECAD"  DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="NUM_CONTRATO" HeaderText="Núm. Contrato" SortExpression="NUM_CONTRATO" />
                                        <%--<asp:BoundField DataField="DTH_INCLUSAO" HeaderText="Dt. Criação Base" SortExpression="DTH_INCLUSAO"  DataFormatString="{0:dd/MM/yyyy}" />--%>
                                        <asp:BoundField DataField="TOTAL_REGISTROS" HeaderText="Total de registros" SortExpression="TOTAL_REGISTROS" />
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:tabcontainer>
                </ContentTemplate>
                <Triggers>                    
                    <asp:PostBackTrigger ControlID="TabContainer$TbImportar$btnUpload" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
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
