<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CargaProtheus.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CargaProtheus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         var updateProgress = null;

         function postbackButtonClick() {
             updateProgress = $find("<%= UpdateProg1.ClientID %>");
             window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
             return true;
         }

         function isNumber(evt) {

             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         }
    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Carga de Dados Protheus</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <div id="divPesquisa" runat="server" class="tabelaPagina">
                        <asp:Panel ID="pnlPesquisa" runat="server">
                            <table>
                                <tr>
                                    <td>Tipo de Carga:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlProtheus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProtheus_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDatPagaVenc" Text="Data de Pagamento/Vencimento:" runat="server" Visible="true" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDatPagaVenc" runat="server" CssClass="date" MaxLength="10" Visible="true" onkeypress="mascara(this, data)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMesRef" runat="server" Text="Mês de referência: " Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMesRef" runat="server" MaxLength="2" Visible="false" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </td>
                              </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRepasseEmp" runat="server" Text="Empresa: " Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRepasseEmp" runat="server" AutoPostBack="true" Visible="false"></asp:DropDownList>
                                       <asp:CheckBoxList ID="cblRepasseEmp" runat="server" AutoPostBack="true" Visible="false"></asp:CheckBoxList>
                                    </td>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCarregaArq" runat="server" Text="Carregar Arquivo" CssClass="button" Visible="false" OnClick="btnCarregaArq_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAssociado" Text="Codigo Associado:" runat="server" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAssociado" runat="server" CssClass="text" MaxLength="10" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDataSuple" Text="Data Suplementados:" runat="server" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataSuple" runat="server" CssClass="date" MaxLength="10" Visible="false" onkeypress="mascara(this, data)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDataComple" Text="Data Complementados:" runat="server" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataComple" runat="server" CssClass="date" MaxLength="10" Visible="false" onkeypress="mascara(this, data)" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblLote" Text="Numero do Lote:" runat="server" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLote" runat="server" CssClass="date" MaxLength="10" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDataInicial" Text="Data Documento Incial:" runat="server" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataInicial" runat="server" CssClass="date" MaxLength="10" Visible="false" onkeypress="mascara(this, data)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDataFinal" Text="Data Documento Final:" runat="server" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataFinal" runat="server" CssClass="date" MaxLength="10" Visible="false" onkeypress="mascara(this, data)" />
                                    </td>
                                </tr>
                                  <tr>
                            <td>
                                <asp:Label ID="lblTxt" Text="Upload Txt:" runat="server" Visible="false" />
                            </td>

                            <td>
                                <asp:FileUpload ID="fuTxt" runat="server" CssClass="button" Visible="false" />
                            </td>
                               <td>
                                    <asp:Button ID="btnCarregaFarm" runat="server" Text="Carregar Arquivo SRU" CssClass="button" Visible="false" OnClick="btnCarregaFarm_Click" />
                               </td>
                        </tr>
                                <%--<tr>
                                    <td>Processamento Imediato:</td>
                                    <td>
                                        <asp:CheckBoxList ID="check1" AutoPostBack="false"
                                            TextAlign="Right" runat="server">
                                            <asp:ListItem>Sim</asp:ListItem>
                                            <asp:ListItem>Não</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>--%>
                                <td>
                                    <asp:Button ID="btnok" runat="server" Text="Gerar" OnClientClick="return postbackButtonClick();" CssClass="button" OnClick="btnok_Click" />
                                </td>
                                </tr> 
                            </table>
                            <table>


                                <tr>
                             <td>
                                 <asp:Label ID="lblMsgCargaHorario" runat="server" Text="*Não é possível validar os processos no momento, carga do Protheus está em andamento" ForeColor="Red" Visible="false"></asp:Label>
                             </td>
                         </tr>
                                <tr>
                                    <td>
                                        <h3>Lotes pendentes:</h3>
                                        <br />
                                         <asp:GridView ID="grdFilaProcesso" 
                                            runat="server"
                                            CssClass="panelTable"
                                            OnRowCommand="grdFilaProcesso_RowCommand"
                                             OnRowDataBound="grdFilaProcesso_RowDataBound"
                                            OnPageIndexChanged="grdFilaProcesso_PageIndexChanged"
                                            AutoGenerateColumns="false"
                                            PageSize ="5" Width="1000px"
                                             >
                                             <Columns>

                                                 <asp:TemplateField HeaderText="Nº Lote">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNumLote" runat="server" Text='<%# Bind("num_lote") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Tipo do relatório">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("dcr_parametros") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Data de pagamento">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblDataPagamento" runat="server" Text='<%# Bind("DATA_PAGAMENTO") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Matrícula de inclusão">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblLogInclusao" runat="server" Text='<%# Bind("log_inclusao") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Data de inclusão">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblDataInclusao" runat="server" Text='<%# Bind("dth_inclusao") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblStatusProcesso" runat="server" Text='<%# Bind("Status_da_carga") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField>
                                                     <ItemTemplate>
                                                         <asp:Button ID="btnGerarRel" runat="server" CommandName="Gerar" Text="Gerar Relatório" CssClass="button" />
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField>
                                                     <ItemTemplate>
                                                         <asp:Button ID="btnValidar" runat="server" CommandName="Validar" Text="Validar" CssClass="button" />
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField>
                                                     <ItemTemplate>
                                                         <asp:Button ID="btnExcluirLote" runat="server" CommandName="Excluir" Text="Excluir" CssClass="button" OnClientClick="return confirm('Deseja excluir esse lote?');" />
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                             </Columns>

                                        </asp:GridView>
                                    </td>
                                </tr>
                                <div id="divClifor" runat="server" class="tabelaPagina" visible="false">
                                  <tr>
                                    <td>
                                        <h3>Contas pendentes na Clifor: </h3> 
                                        <br />
                                     <asp:GridView ID="grdClifor" 
                                            runat="server"
                                            CssClass="panelTable"
                                            AllowPaging="true"
                                            AutoGenerateColumns="False"
                                            PageSize ="5" Width="1000px"
                                            OnSelectedIndexChanged="grdClifor_SelectedIndexChanged"
                                           OnPageIndexChanging="grdClifor_PageIndexChanging"
                                         >
                                         <Columns>

                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Empresa
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblCodEmpr" runat="server" Text='<%# Bind("cod_emprs") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Matrícula
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblMatr" runat="server" Text='<%# Bind("num_rgtro_emprg") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Nº Representante
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblRepresentante" runat="server" Text='<%# Bind("num_idntf_rptant") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Nº Dependente
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblDependente" runat="server" Text='<%# Bind("num_idntf_depdte") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Nome
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblNome" runat="server" Text='<%# Bind("nome") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Banco
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblBanco" runat="server" Text='<%# Bind("banco") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             
                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Agência
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblAgencia" runat="server" Text='<%# Bind("agencia") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField>
                                                 <HeaderTemplate>
                                                     Nº Conta
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblAgencia" runat="server" Text='<%# Bind("numcon") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>


                                         </Columns>
                                         </asp:GridView>
                                        <asp:Button ID="btnAttClifor" runat="server" Text="Atualiza Clifor" OnClick="btnAttClifor_Click" CssClass="button" />
                                    </td>
                                </tr>

                                </div>
                            </table>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
                <Triggers>
                   <asp:PostBackTrigger ControlID="btnOk" />
                        </Triggers>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCarregaArq" />
                </Triggers>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCarregaFarm" />
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
