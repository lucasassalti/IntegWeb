<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BoletoCobrancaSaude.aspx.cs" Inherits="IntegWeb.Saude.Web.BoletoCobrancaSaude" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="full_w">
             <div class="h_title"></div>
             <div class="tabelaPagina">
                        <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                                <ContentTemplate>
                                             <ajax:TabContainer ID="TabContainer" AutoPostBack="True" runat="server" ActiveTabIndex="0" >
                                                    <ajax:TabPanel ID="TabGerarCobranca" HeaderText="Boletos" runat="server" TabIndex="0">
                                                                 <ContentTemplate>
                                                                                <div class="form-style-8" runat="server" >
                                                                                        <center><h2> Boletos de Cobrança - Saúde </h2></center>
                                                                                                <asp:RequiredFieldValidator runat="server" ID="ReqtxtDtVenc" ControlToValidate="txtDtVenc" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/> 
                                                                                                      <asp:RangeValidator
			                                                                                                 runat="server"
			                                                                                                 ID="rangtxtDtVenc"
			                                                                                                 Type="Date"
			                                                                                                 ControlToValidate="txtDtVenc"
			                                                                                                 MaximumValue="31/12/9999"
			                                                                                                 MinimumValue="31/12/1000"
			                                                                                                 ErrorMessage="Data Inválida"
			                                                                                                 ForeColor="Red"
			                                                                                                 Display="Dynamic" ValidationGroup="grpParametros" 
			                                                                                                 />
                                                                                                       <asp:TextBox runat="server" ID="txtDtVenc" CssClass="date" MaxLength="10" placeholder="Data de Vencimento" />
                                                                                                        <asp:RequiredFieldValidator 
                                                                                                           runat="server" 
                                                                                                           ID="ReqtxtDtVencAnt" 
                                                                                                           ControlToValidate="txtDtVencAnt" 
                                                                                                           ErrorMessage="Campo Obrigatório" 
                                                                                                           ForeColor="Red" Display="Dynamic" 
                                                                                                           ValidationGroup="grpParametros"
                                                                                                          /> 
   
                                                                                                           <asp:RangeValidator
                                                                                                            runat="server"
                                                                                                            ID="rangeTxtDtVencAba"
                                                                                                            Type="Date"
                                                                                                            ControlToValidate="txtDtVencAnt"
                                                                                                            MaximumValue="31/12/9999"
                                                                                                            MinimumValue="31/12/1000"
                                                                                                            ErrorMessage="Data Inválida"
                                                                                                            ForeColor="Red"
                                                                                                            Display="Dynamic" ValidationGroup="grpParametros" 
                                                                                                            />
                                                                                                       <asp:TextBox ID="txtDtVencAnt" runat="server"  placeholder="Data do Vencimento Anterior "/>
                                                                                                          <asp:RequiredFieldValidator 
                                                                                                           runat="server" 
                                                                                                           ID="ReqtxtDtTol" 
                                                                                                           ControlToValidate="txtDtTol" 
                                                                                                           ErrorMessage="Campo Obrigatório" 
                                                                                                           ForeColor="Red" Display="Dynamic" 
                                                                                                           ValidationGroup="grpParametros"
                                                                                                          /> 
   
                                                                                                           <asp:RangeValidator
                                                                                                            runat="server"
                                                                                                            ID="RangetxtDtTol"
                                                                                                            Type="Date"
                                                                                                            ControlToValidate="txtDtTol"
                                                                                                            MaximumValue="31/12/9999"
                                                                                                            MinimumValue="31/12/1000"
                                                                                                            ErrorMessage="Data Inválida"
                                                                                                            ForeColor="Red"
                                                                                                            Display="Dynamic" ValidationGroup="grpParametros" 
                                                                                                            />
                                                                                                       <asp:TextBox ID="txtDtTol" runat="server"  placeholder="Data de Tolerância do boleto"/>
                                                                                                             <asp:RequiredFieldValidator 
                                                                                                               runat="server" 
                                                                                                               ID="ReqtxtNumLote" 
                                                                                                               ControlToValidate="txtNumLote" 
                                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                                               ValidationGroup="grpParametros"
                                                                                                              /> 
                                                                                                       <asp:TextBox ID="txtNumLote" runat="server"  placeholder="Número do Lote"/><br />
                                                                                                 <center><asp:Button ID="btnBoletos" runat="server" Text="Processar"  ValidationGroup="grpParametros"  OnClick="btnBoletos_Click"/></center> 
                                                                                </div>
                                                                  </ContentTemplate> 
                                                        </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarFlags" HeaderText="Flags de Insucesso" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="Div5" class="form-style-8" runat="server">
			                                                                               <center><h2> Geração de Flags de insucesso </h2></center>
                                                                                             <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtDtVencAba2" 
                                                                                               ControlToValidate="txtDtVencAba2" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros2"
                                                                                              /> 
   
                                                                                               <asp:RangeValidator
                                                                                                       runat="server"
                                                                                                       ID="RangetxtDtVencAba2"
                                                                                                       Type="Date"
                                                                                                       ControlToValidate="txtDtVencAba2"
                                                                                                       MaximumValue="31/12/9999"
                                                                                                       MinimumValue="31/12/1000"
                                                                                                       ErrorMessage="Data Inválida"
                                                                                                       ForeColor="Red"
                                                                                                       Display="Dynamic" ValidationGroup="grpParametros2" 
                                                                                                />
                                                                                             <asp:TextBox ID="txtDtVencAba2" runat="server"  placeholder="Data de Vencimento"/>
                                                                                                     <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba2" 
                                                                                               ControlToValidate="txtNumLoteAba2" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros2"
                                                                                              /> 
                                                                                             <asp:TextBox ID="txtNumLoteAba2" runat="server"  placeholder="Número do Lote"/>
                                                                                           <center> <asp:Button ID="btnFlags" runat="server" Text="Processar" OnClick="btnFlags_Click" ValidationGroup="grpParametros2" /> </center> 
			                                                                 </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarInadimplentes" HeaderText="Inadimplentes" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="Div2" class="form-style-8" runat="server" >
			                                                                               <center><h2> Geração de Inadimplentes </h2></center>
                                                                                               <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtDtVencAba3" 
                                                                                               ControlToValidate="txtDtVencAba3" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros3"
                                                                                              /> 
   
                                                                                               <asp:RangeValidator
                                                                                                runat="server"
                                                                                                ID="rangeTxtDtVencAba3"
                                                                                                Type="Date"
                                                                                                ControlToValidate="txtDtVencAba3"
                                                                                                MaximumValue="31/12/9999"
                                                                                                MinimumValue="31/12/1000"
                                                                                                ErrorMessage="Data Inválida"
                                                                                                ForeColor="Red"
                                                                                                Display="Dynamic" ValidationGroup="grpParametros3" 
                                                                                                />
                                                                                             <asp:TextBox ID="txtDtVencAba3" runat="server"  placeholder="Data de Vencimento"/>
                                                                                             <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba3" 
                                                                                               ControlToValidate="txtNumLoteAba3" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros3"
                                                                                              /> 
                                                                                             <asp:TextBox ID="txtNumLoteAba3" runat="server"  placeholder="Número do Lote"  />
                                                                                                  <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqddlTipoRotinaIna" 
                                                                                               ControlToValidate="ddlTipoRotinaIna" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros3"
                                                                                              /> 
                                                                                             <asp:DropDownList runat="server" ID="ddlTipoRotinaIna" AutoPostBack="true">
                                                                                                          <asp:ListItem Value="1" Text="Inadimplentes"/>
                                                                                                          <asp:ListItem Value="2" Text="Inadimplentes - Digna"/>
                                                                                             </asp:DropDownList><br />
                                                                                          <center>    <asp:Button ID="btnInadimplentes" runat="server" Text="Processar" OnClick="btnInadimplentes_Click" ValidationGroup="grpParametros3" /></center> 
			                                                                 </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarAvisoCob" HeaderText="Aviso de Cobrança " runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="Div3" class="form-style-8" runat="server">
			                                                                               <center><h2> Geração da Carta de aviso de cobrança </h2></center>
                                                                                              <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtDtVencAba4" 
                                                                                               ControlToValidate="txtDtVencAba4" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros4"
                                                                                              /> 
   
                                                                                               <asp:RangeValidator
                                                                                                runat="server"
                                                                                                ID="rangeTxtDtVencAba4"
                                                                                                Type="Date"
                                                                                                ControlToValidate="txtDtVencAba4"
                                                                                                MaximumValue="31/12/9999"
                                                                                                MinimumValue="31/12/1000"
                                                                                                ErrorMessage="Data Inválida"
                                                                                                ForeColor="Red"
                                                                                                Display="Dynamic" ValidationGroup="grpParametros4" 
                                                                                                />
                                                                                             <asp:TextBox ID="txtDtVencAba4" runat="server"  placeholder="Data de Vencimento"/>
                                                                                                
                                                                                            <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba4" 
                                                                                               ControlToValidate="txtNumLoteAba4" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros4"
                                                                                              /> 
                                                                                             <asp:TextBox ID="txtNumLoteAba4" runat="server"  placeholder="Número do Lote"/>
                                                                                          <center>    <asp:Button ID="btnAviso" runat="server" Text="Processar" OnClick="btnAviso_Click" ValidationGroup="grpParametros4" /></center> 
			                                                                 </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarRel" HeaderText="Relatórios" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="Div1" class="form-style-8" runat="server" >
			                                                                            <center><h2> Relatórios dos Boletos de Cobrança - Saúde </h2></center>
                                                                                            <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtDtVencAba5" 
                                                                                               ControlToValidate="txtDtVencAba5" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros5"
                                                                                              /> 
   
                                                                                               <asp:RangeValidator
                                                                                                runat="server"
                                                                                                ID="rangeTxtDtVencAba5"
                                                                                                Type="Date"
                                                                                                ControlToValidate="txtDtVencAba5"
                                                                                                MaximumValue="31/12/9999"
                                                                                                MinimumValue="31/12/1000"
                                                                                                ErrorMessage="Data Inválida"
                                                                                                ForeColor="Red"
                                                                                                Display="Dynamic" ValidationGroup="grpParametros5" 
                                                                                                />
                                                                                        <asp:TextBox runat="server" ID="txtDtVencAba5" CssClass="date" MaxLength="10" placeholder="Data de Vencimento" />
                                                                                          <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba5" 
                                                                                               ControlToValidate="txtNumLoteAba5" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros5"
                                                                                              /> 
                                                                                        <asp:TextBox ID="txtNumLoteAba5" runat="server"  placeholder="número do Lote"/>

                                                                                         <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqddlTipoRel" 
                                                                                               ControlToValidate="ddlTipoRel" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros5"
                                                                                              /> 
                                                                                        <asp:DropDownList runat="server" ID="ddlTipoRel" AutoPostBack="true">
                                                                                                          <asp:ListItem Value="1" Text="Aviso de Cancelamento"/>
                                                                                                          <asp:ListItem Value="2" Text="Aviso de Inadimplência"/>
                                                                                                          <asp:ListItem Value="3" Text="Boletos Judiciais"/>
                                                                                                          <asp:ListItem Value="4" Text="Boletos com valores menores que 4 reais"/>
                                                                                                          <asp:ListItem Value="5" Text="Boletos com valores menores que 20 reais"/>
                                                                                                          <asp:ListItem Value="6" Text="Boleto impressão procarta"/>
                                                                                                          <asp:ListItem Value="7" Text="Flag - ativo no plano"/>
                                                                                                          <asp:ListItem Value="8" Text="Endereço Nulo"/>
                                                                                                          <asp:ListItem Value="9" Text="Flag Insucesso - inclusão"/>
                                                                                                          <asp:ListItem Value="10" Text="Flag Insucesso - Parcelamento"/>
                                                                                                          <asp:ListItem Value="11" Text="Inadimplentes"/>
                                                                                                          <asp:ListItem Value="12" Text="Inadimplentes - Digna"/>
                                                                                                          <asp:ListItem Value="13" Text="Todos os Flags"/>
                                                                                                          <asp:ListItem Value="14" Text="Todas as cobranças"/>

                                                                                        </asp:DropDownList><br />
                                                                                         <center>  <asp:Button ID="btnRel" runat="server" Text="Processar"  ValidationGroup="grpParametros5" OnClick="btnRel_Click" /></center> 
			                                                                 </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarTxt" HeaderText="Arquivos TXT" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
                                                                        <Center>
			                                                                 <div id="Div4" class="form-style-8" runat="server">
			                                                                           <h2> Arquivos Txts </h2>
                                                                                        <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtDtVencAba6" 
                                                                                               ControlToValidate="txtDatVencAba6" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros6"
                                                                                              /> 
   
                                                                                               <asp:RangeValidator
                                                                                                runat="server"
                                                                                                ID="rangeTxtDatVencAba6"
                                                                                                Type="Date"
                                                                                                ControlToValidate="txtDatVencAba6"
                                                                                                MaximumValue="31/12/9999"
                                                                                                MinimumValue="31/12/1000"
                                                                                                ErrorMessage="Data Inválida"
                                                                                                ForeColor="Red"
                                                                                                Display="Dynamic" ValidationGroup="grpParametros6" 
                                                                                                />
                                                                                             <asp:TextBox ID="txtDatVencAba6" runat="server"  placeholder="Data de Vencimento" />
                                                                                             <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba6" 
                                                                                               ControlToValidate="txtNumLoteAba6" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros6"
                                                                                              /> 
                                                                                             <asp:TextBox ID="txtNumLoteAba6" runat="server"  placeholder="Número do Lote" />
                                                                                              
                                                                                                 <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqddlTiposTxt" 
                                                                                               ControlToValidate="ddlTiposTxt" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros6"
                                                                                              /> 
                                                                                             <asp:DropDownList runat="server" ID="ddlTiposTxt" AutoPostBack="true" >
                                                                                                          <asp:ListItem Value="1" Text="Boletos de Cobrança"/>
                                                                                                          <asp:ListItem Value="2" Text="Boletos Judiciais"/>
                                                                                                          <asp:ListItem Value="3" Text="Cartas de Aviso de Cancelamento"/>
                                                                                                          <asp:ListItem Value="4" Text="Cartas de Aviso de Inadimplência"/>
                                                                                                          <asp:ListItem Value="5" Text="Cartas de Aviso de Inadimplência - Digna"/>
                                                                                        </asp:DropDownList><br/>
                                                                                    <asp:Button ID="btnGerarTxt" runat="server" Text="Gerar" OnClick="btnGerarTxt_Click"  style="margin-right:300px" ValidationGroup="grpParametros6"/>
                                                                                    <asp:Button ID="btnPesquisarTxt" runat="server" Text="Pesquisar" OnClick="btnPesquisarTxt_Click" ValidationGroup="grpParametros6" />
			                                                                 </div>

                                                                             <div id="Div8" class="form-style-8" runat="server" >
                                                                                      <asp:GridView id="gridViewTxt" runat="server"  Width="100%" OnRowCommand="gridViewTxt_RowCommand"  >
                                                                                                  <Columns>
                                                                                                      <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                               <asp:LinkButton runat="server" Text="Download"  id="btnDownload" CommandName="Download"/>
                                                                                                            </ItemTemplate>
                                                                                                      </asp:TemplateField>
                                                                                                      
                                                                                                  </Columns>
                                                                                         </asp:GridView>
                                                                                
                                                                            </div>
                                                                        </Center>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarPdf" HeaderText="Download PDF" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
                                                                        <Center>
			                                                                 <div id="Div6" class="form-style-8" runat="server" style="width:40%">
			                                                                           <h2> MCM - PDFs </h2>
                                                                                                                                                                                       <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtDatVencAba7" 
                                                                                               ControlToValidate="txtDatVencAba7" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros7"
                                                                                              /> 
   
                                                                                               <asp:RangeValidator
                                                                                                runat="server"
                                                                                                ID="rangeTxtDatVencAba7"
                                                                                                Type="Date"
                                                                                                ControlToValidate="txtDatVencAba7"
                                                                                                MaximumValue="31/12/9999"
                                                                                                MinimumValue="31/12/1000"
                                                                                                ErrorMessage="Data Inválida"
                                                                                                ForeColor="Red"
                                                                                                Display="Dynamic" ValidationGroup="grpParametros7" 
                                                                                                />
                                                                                             <asp:TextBox ID="txtDatVencAba7" runat="server"  placeholder="Data de Vencimento" />

                                                                                                   <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba7" 
                                                                                               ControlToValidate="txtNumLoteAba7" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros7"
                                                                                              /> 
   
                                                                                             <asp:TextBox ID="txtNumLoteAba7" runat="server"  placeholder="Número do Lote" />

                                                                                           <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtddlTipoPdf" 
                                                                                               ControlToValidate="ddlTipoPdf" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros7"
                                                                                              /> 
                                                                                             <asp:DropDownList runat="server" ID="ddlTipoPdf" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPdf_SelectedIndexChanged">
                                                                                                          <asp:ListItem Value="1"    Text="COBVINC"   />
                                                                                                          <asp:ListItem Value="2"    Text="COBSIGA"   />
                                                                                                          <asp:ListItem Value="3"   Text="COBSAUDC"  />
                                                                                                          <asp:ListItem Value="4"  Text="COBSAUDAR" />
                                                                                                          <asp:ListItem Value="5"   Text="COBRSERP"  />
                                                                                                          <asp:ListItem Value="6" Text="CIDSAUDATV"/>
                                                                                        </asp:DropDownList><br/>
			                                                                 </div>

                                                                            <div id="Div7" class="form-style-8" runat="server" style="width:40%">
                                                                              <asp:GridView id="gridViewMcm" runat="server"  Width="100%" OnRowCommand="gridViewMcm_RowCommand" >
                                                                                          <Columns>
                                                                                              <asp:TemplateField>
                                                                                                      <ItemTemplate >
                                                                                                             <asp:LinkButton runat="server" Text="Download"  id="btnDownloadMcm" CommandName="Download"/>  
                                                                                                      </ItemTemplate>
                                                                                               </asp:TemplateField>
                                                                                          </Columns>
                                                                                 </asp:GridView>
                                                                            </div>

                                                                        </Center>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                 </ajax:TabContainer>  
                                </ContentTemplate>
                        </asp:UpdatePanel>
             </div>
      </div>

</asp:Content>
