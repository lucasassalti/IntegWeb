<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="True" CodeBehind="CadAnaliseSuS.aspx.cs" Inherits="IntegWeb.Saude.Web.CadAnaliseSuS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function ScrollRight() {
            window.scrollTo(200, 0);
        }

        function ScrollLeft() {
            window.scrollTo(0, 0);
        }
    </script>
     <asp:UpdatePanel runat="server" ID="upCadSus">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsCadSus" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Consultar\Cadastrar Impugnação - SUS</h1>
                    <table>
                        <tr>
                            <td>Còdigo do Beneficiário:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigoBeneficiario" runat="server"></asp:TextBox></td>
                            <td>Número AIH\APAC
                            </td>
                            <td>
                                <asp:TextBox ID="txtBuscaAIHAPAC" runat="server"></asp:TextBox></td>
                            <td>Competência
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompetencia" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Consultar" />
                                <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Limpar" />
                            </td>
                        </tr>
                    </table>

                    <asp:ObjectDataSource runat="server" ID="odsCadSus"
                        TypeName="IntegWeb.Saude.Aplicacao.BLL.CadAnaliseSuSBLL"
                        SelectMethod="GetImpugSus"
                        SelectCountMethod="SelectCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter"
                        UpdateMethod="UpdateData">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtCodigoBeneficiario" Name="paramCodigoUsuario" PropertyName="Text" Type="String" />                            
                            <asp:ControlParameter ControlID="txtBuscaAIHAPAC" Name="paramBuscaAIHAPAC" PropertyName="Text" Type="String" />                            
                            <asp:ControlParameter ControlID="txtCompetencia" Name="paramCompetencia" PropertyName="Text" Type="String" />                            
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdCadSus" runat="server" 
                        OnRowCancelingEdit="grdCadSus_RowCancelingEdit" 
                        OnRowEditing="grdCadSus_RowEditing" 
                        OnRowCreated="GridView_RowCreated"
                        AllowPaging="True" 
                        AllowSorting="False"
                        Visible="false"
                        AutoGenerateColumns="False" 
                        EmptyDataText="A consulta não retornou registros" 
                        CssClass="Table" 
                        ClientIDMode="Static" 
                        PageSize="8" 
                        DataSourceID="odsCadSus">
                        <PagerSettings
                            Visible="true"
                            PreviousPageText="Anterior"
                            NextPageText="Próxima"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:BoundField DataField="NUMEROOFICIO" HeaderText="Oficio" SortExpression="NUMEROOFICIO"/>
                            <asp:BoundField DataField="NUMEROABI" HeaderText="ABI" SortExpression="NUMEROABI"/>
                            <asp:BoundField DataField="NUMERO" HeaderText="Número" SortExpression="NUMERO"/>                           
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" />
                            <asp:BoundField DataField="CODIGOBENEFICIARIO" HeaderText="Cod-Benef" SortExpression="CODIGOBENEFICIARIO" />
                            <asp:BoundField DataField="CODIGOCCO" HeaderText="CCO" SortExpression="CODIGOCCO" />
                            <asp:BoundField DataField="COMPETENCIA" HeaderText="Competência" SortExpression="COMPETENCIA" />
                            <asp:BoundField DataField="DATAINICIOATENDIMENTO" HeaderText="DT Inicio" SortExpression="DATAINICIOATENDIMENTO" DataFormatString ="{0:dd-MM-yyyy}" ItemStyle-Width="80px" />
                            <asp:BoundField DataField="DATAFIMATENDIMENTO" HeaderText="Dt Fim" SortExpression="DATAFIMATENDIMENTO" DataFormatString = "{0:dd-MM-yyyy}" ItemStyle-Width="80px"/>
                            <asp:BoundField DataField="COD_EMP" HeaderText="Empresa" SortExpression="COD_EMP" />
                            <asp:BoundField DataField="MATRICULA" HeaderText="Matricula" SortExpression="MATRICULA" />
                            <asp:BoundField DataField="SUB_MATRICULA" HeaderText="Sub" SortExpression="SUB_MATRICULA" />
                            <asp:BoundField DataField="NOMEBENEFICIARIO" HeaderText="Nome" SortExpression="NOMEBENEFICIARIO" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="CODIGOPROCEDIMENTO" HeaderText="Codigo" SortExpression="CODIGOPROCEDIMENTO" />
                            <asp:BoundField DataField="DESCRICAOPROCEDIMENTO" HeaderText="Descrição" SortExpression="DESCRICAOPROCEDIMENTO" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="VALORPROCEDIMENTO" HeaderText="Valor" SortExpression="VALORPROCEDIMENTO" />                                                   
                            
                                
                        </Columns>
                    </asp:GridView>
                    <div id="divPreenchimento" runat="server">
                                            <div align="center">
                                            <h1>Preenchimento de Dados para Impugnação:</h1>
                                                <Table>

                                            <tr>
                                                <td>
                                                    
                                                    <h3>Tipo de Petição:</h3>
                                                    <asp:RadioButton ID="rdbImpugnacao" runat="server" Text="Impugnação" />
                                                    <asp:RadioButton ID="rdbRecurso" runat="server" Text="Recurso" />
                                                </td>
                                                <td>
                                                    <h3>Número do Registro do Produto:</h3>
                                                    <asp:TextBox ID="txtNumeroRegistro" runat="server" />
                                                </td>
                                            </tr>
                                                </Table></div>

                    <center>
                        <table>                                
                                    <caption>
                                            <tr>
                                                <td>
                                                    <h3>Tempestividade : </h3>
                                                    <asp:TextBox ID="txtTempestividade" runat="server" Height="100px" TextMode="MultiLine" Width="200px" />
                                                </td>
                                                <td>
                                                    <h3>Detalhamento do Motivo : </h3>
                                                    <asp:TextBox ID="txtDetalheMotivo" runat="server" Height="100px" TextMode="MultiLine" Width="200px" />
                                                </td>
                                                <td>
                                                    <h3>Memória de Cálculo:</h3>
                                                    <asp:TextBox ID="txtMemorialCalculo" runat="server" Height="100px" TextMode="MultiLine" Width="200px" />
                                                </td>
                                                <td>
                                                    <h3>Documentos Comprobatórios:</h3>
                                                    <asp:TextBox ID="txtDocsComprob" runat="server" Height="100px" TextMode="MultiLine" Width="200px" />
                                                </td>
                                            </tr>                                    
                                    </caption>
                        </table>
                    
                    <table>
                        <tr>
                            <td><h3>Pedido(S)</h3>
                                <asp:checkbox id="chkPedidoA" text="Anulação de Identificação do Atendimento." runat="server" />                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:checkbox id="chkPedidoR" text="Retificação do valor a ser ressarcido para:" runat="server" />   <asp:textbox id="txtValorR" runat="server" />                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:checkbox id="chkPedidoAR" text="Anulação da identificação do atendimento ou, subsidiariamente, retificação do valor a ser ressarcido para: " runat="server" /> <asp:textbox id="txtValorAR" runat="server" />                                
                            </td>
                        </tr>
                    </table>                                            
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnGeraRelatorio" Text="Gerar Relatório" runat="server" OnClick="btnGeraRelatorio_Click" />
                                </td>
                            </tr>
                        </table>
                    </center>
                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Visible="false" />
                    </div>

                    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                        <ProgressTemplate>
                            <div id="carregando">
                                <div class="carregandoTxt">
                                    <img src="img/processando.gif" />
                                    <br /><br />
                                    <h2>Processando. Aguarde...</h2>
                                </div>
                            </div>
                        </ProgressTemplate>
                   </asp:UpdateProgress>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
