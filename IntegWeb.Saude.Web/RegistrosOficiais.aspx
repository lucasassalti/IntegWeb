<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RegistrosOficiais.aspx.cs" Inherits="IntegWeb.Saude.Web.RegistrosOficiais" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function postbackButtonClick() {
            updateProgress = $find("ContentPlaceHolder1_UpdateProg");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }


    </script>


    <div class="full_w">

        <div class="h_title">  
       
             </div>

        <div class="tabelaPagina">


            <asp:UpdatePanel runat="server" ID="upUpdatePanel">

                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" OnActiveTabChanged="TabContainer_ActiveTabChanged" AutoPostBack = True>


                        <ajax:TabPanel ID="TabDadosRo" HeaderText="Geração de Dados do RO" runat="server" TabIndex="0"  >
                            <ContentTemplate>
                                <h1>Geração de Dados do RO</h1>

                                <br />
                                <br />

                                <table>
                                    <tr>
                                        <td> digite o <b>mês</b> de competência:</td>
                                    </tr>
                                    <tr>
                                        <td> 
                                            <asp:TextBox runat="server" ID="txtMes" MaxLength="2" OnKeyPress="mascara(this,soNumeros)"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="reqTxtMes" ControlToValidate="txtMes" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpData"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMes" Type="Integer" MinimumValue="1" MaximumValue="12" ErrorMessage="Mês Inválido" ForeColor="Red" Display="Dynamic" ValidationGroup="grpData"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> digite o <b>ano</b> de competência:</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAno" MaxLength="4" OnKeyPress="mascara(this,soNumeros)"> </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="reqTxtAno" ControlToValidate="txtAno" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpData"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator runat="server" ControlToValidate="txtAno" Type="Integer" MinimumValue="1940" MaximumValue="2025" ErrorMessage="Ano Inválido" ForeColor="Red" Display="Dynamic" ValidationGroup="grpData"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp &nbsp 
                                          <asp:Button runat="server" ID="btnGerar" Text="Gerar Dados" CssClass="button" OnClick="btnGerar_Click" ValidationGroup="grpData" />
                                        </td>
                                    </tr>
                                </table>

                                </br>
                                 &nbsp
                               
                                
                               <%-- <asp:UpdatePanel ID="timerUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer" runat="server" OnTick="Timer_Tick1" Interval="2000" Enabled="false">
                                        </asp:Timer>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>


                            </ContentTemplate>
                        </ajax:TabPanel >

                        <ajax:TabPanel ID="TabRelRo" HeaderText="Geração de Relatórios do RO" runat="server" TabIndex="1" >
                            <ContentTemplate>
                                <h1>Geração de Relatórios do RO</h1>

                                <br />
                                <br />

                                <table>
                                    <tr>
                                        <td>Selecione Mês/Ano:
                                            <asp:DropDownList ID="dllMesAno" runat="server" OnSelectedIndexChanged="dllMesAno_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>

                                <br />
                                <br />

                                <asp:GridView
                                    ID="gridRel"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="COD_SAU_TBL_RO_GERACAO"
                                    OnSelectedIndexChanged="gridRel_SelectedIndexChanged"
                                    >

                                    <Columns>

                                        <asp:BoundField DataField="COD_SAU_TBL_RO_GERACAO" HeaderText="ID Relatório" />
                                        <asp:TemplateField HeaderText="Relatório">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbRel" Text='<%# "PESL_" + Eval("COD_MES_RO") + "/" + Eval("COD_ANO_RO") + "_" + Eval("COD_SAU_TBL_RO_GERACAO") + ".csv"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DTH_ACAO" HeaderText="Data Geração" DataFormatString="{0:d}" /> 
                                        <asp:ButtonField Text="Download" CommandName="Select" />
                                        
                                    </Columns>
                                </asp:GridView>


                            </ContentTemplate>

                        </ajax:TabPanel>

                    </ajax:TabContainer>


                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$TabRelRo$dllMesAno" />
                </Triggers>
            </asp:UpdatePanel>
           <asp:UpdateProgress ID="UpdateProg" DisplayAfter="0" runat="server" >
                <ProgressTemplate>
                    <div id="carregando">
                        <div class="carregandoTxt">
                            <img src="img/processando.gif" />
                            <br />
                           <h2> <asp:Label ID="LabelStatus" runat="server" Text="Processando.Por favor, aguarde alguns minutos" Enabled="false"></asp:Label></h2> 
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
     

</asp:Content>
