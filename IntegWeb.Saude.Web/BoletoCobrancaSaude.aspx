<%@ Page Title="" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="BoletoCobrancaSaude.aspx.cs" Inherits="IntegWeb.Saude.Web.BoletoCobrancaSaude" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="full_w">
             <div class="h_title"></div>
             <div class="tabelaPagina">
                        <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                                <ContentTemplate>
                                             <ajax:TabContainer ID="TabContainer" AutoPostBack="True" runat="server" ActiveTabIndex="0" >
                                                    <ajax:TabPanel ID="TabGerarCobranca" HeaderText="1º Boletos" runat="server" TabIndex="0">
                                                                 <ContentTemplate>
                                                                                <div id="divPrincipalAba1" class="form-style-8" runat="server" >
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
                                                                                                         <asp:RequiredFieldValidator runat="server" ID="ReqtxtDtTol" ControlToValidate="txtDtTol" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/> 
   
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
                                                                                                       <asp:TextBox ID="txtNumLote" runat="server"  placeholder="Número do Lote" onkeypress="mascara(this, soNumeros)" /><br />
                                                                                                 <center>
                                                                                                        <asp:Button ID="btnBoletos" runat="server" Text="Processar"  ValidationGroup="grpParametros"  OnClick="btnBoletos_Click" OnClientClick="return confirm('Deseja realmente iniciar o processo?')"/><br/><br/> 
                                                                                                        <asp:label id="Label2" runat="server" Text="* Caso seja a geração de um novo lote, inserir o nº 0 no campo 'Número do Lote' "  Font-Bold="true" ForeColor="Red" Font-Size ="8" />
                                                                                                 </center> 

                                                                                </div>

                                                                                <div id="DivSucessoAba1" class="form-style-8 align-center" runat="server" style="background: #F6FFEC;border: 1px solid #89B755; border-radius: 3px;" visible =" false" >
                                                                                        
                                                                                             <h3> Lote gerado com sucesso!</h3>
                                                                                            
                                                                                             <b><asp:Label  id="lblAba1" runat="server"  ></asp:Label></b><br/><br/>
                                                                                             <asp:Label  id="lbl2Aba1" runat="server" Text="Próxima Rotina a ser processada - Aba 2º Flags de Insucesso"  ></asp:Label>
                                                                                </div>

                                                                                      
                                                                                
                                                                  </ContentTemplate> 
                                                        </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarFlags" HeaderText="2º Flags de Insucesso" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="divPrincipalAba2" class="form-style-8" runat="server">
			                                                                               <center><h2> Geração de Flags de insucesso </h2></center>
                                                                                     <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba2" 
                                                                                               ControlToValidate="txtNumLoteAba2" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros2"
                                                                                              /> 
                                                                                             <asp:TextBox ID="txtNumLoteAba2" runat="server"  placeholder="Número do Lote" onkeypress="mascara(this, soNumeros)"/>
                                                                                           <center> <asp:Button ID="btnFlags" runat="server" Text="Processar" OnClick="btnFlags_Click" ValidationGroup="grpParametros2" OnClientClick="return confirm('Deseja realmente iniciar o processo?')" /> </center> 
			                                                                 </div>
                                                                             
                                                                            <div id="DivSucessoAba2" class="form-style-8 align-center" runat="server" style="background: #F6FFEC;border: 1px solid #89B755; border-radius: 3px;" visible="false">
                                                                                        
                                                                                             <h3> Rotina processada com sucesso!</h3>
                                                                                            
                                                                                             
                                                                                             <asp:Label  id="lbl2Aba2" runat="server" Text="Próxima Rotina a ser processada - Aba 3º Inadimplentes -> Inadimplentes"  ></asp:Label>
                                                                               
                                                                            </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarInadimplentes" HeaderText="3º Inadimplentes" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="divPrincipalAba3" class="form-style-8" runat="server" >
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
                                                                                             <asp:TextBox ID="txtNumLoteAba3" runat="server"  placeholder="Número do Lote" onkeypress="mascara(this, soNumeros)"  />
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
                                                                                          <center>    <asp:Button ID="btnInadimplentes" runat="server" Text="Processar" OnClick="btnInadimplentes_Click" ValidationGroup="grpParametros3" OnClientClick="return confirm('Deseja realmente iniciar o processo?')"/></center> 
			                                                                 </div>
                                                                             <div id="DivSucessoAba3" class="form-style-8 align-center" runat="server" style="background: #F6FFEC;border: 1px solid #89B755; border-radius: 3px;" visible="false">
                                                                                        
                                                                                             <h3> Rotina processada com sucesso!</h3>
                                                                                            
                                                                                             
                                                                                              <asp:Label  id="lbl2Aba3" runat="server" Text="Próxima Rotina a ser processada - Aba 3º Inadimplentes -> Inadimplentes - Digna"  ></asp:Label>

                                                                             </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarCobExtraJud" HeaderText="4º Inclusão Aviso Cancelamento " runat="server" TabIndex="0">
                                                                    <ContentTemplate>
                                                                        <div id="divPrincipalAba4" class="form-style-8" runat="server">
			                                                               <center><h2> Inclusão de Aviso de Cancelamento </h2></center>
                                                                        <asp:TextBox ID="txtCodEmpresaAbaEJ" runat="server"  placeholder="Código da empresa" onkeypress="mascara(this, soNumeros)"/>
                                                                        <asp:TextBox ID="txtNumMatriculaAbaEJ" runat="server"  placeholder="Número da matrícula" onkeypress="mascara(this, soNumeros)"/>
                                                                        <asp:TextBox ID="txtNumRepres" runat="server"  placeholder="Número de Representante" onkeypress="mascara(this, soNumeros)"/>
                                                                        <asp:TextBox ID="txtNumLoteAbaEJ" runat="server"  placeholder="Número do Lote" onkeypress="mascara(this, soNumeros)"/>
                                                                         
                                                                           <asp:RequiredFieldValidator 
                                                                        	   runat="server" 
                                                                        	   ID="ReqtxtDtVencAbaEJ" 
                                                                        	   ControlToValidate="txtDtVencAbaEJ" 
                                                                        	   ErrorMessage="Campo Obrigatório"
                                                                        	   ForeColor="Red" 
                                                                        	   Display="Dynamic" 
                                                                        	   ValidationGroup="grpParametros"
                                                                           /> 
                                                                           <asp:RangeValidator
                                                                               runat="server"
                                                                               ID="rangtxtDtVencAbaEJ"
                                                                               Type="Date"
                                                                               ControlToValidate="txtDtVencAbaEJ"
                                                                               MaximumValue="31/12/9999"
                                                                               MinimumValue="31/12/1000"
                                                                               ErrorMessage="Data Inválida"
                                                                               ForeColor="Red"
                                                                               Display="Dynamic" ValidationGroup="grpParametros4" 
                                                                               />
                                                                        <asp:TextBox ID="txtDtVencAbaEJ" runat="server"  placeholder="Data de Vencimento"/>

                                                                            <asp:RequiredFieldValidator 
                                                                        	   runat="server" 
                                                                        	   ID="ReqtxtDtVencAntAbaEJ" 
                                                                        	   ControlToValidate="txtDtVencAntAbaEJ" 
                                                                        	   ErrorMessage="Campo Obrigatório"
                                                                        	   ForeColor="Red" 
                                                                        	   Display="Dynamic" 
                                                                        	   ValidationGroup="grpParametros"
                                                                           /> 
                                                                           <asp:RangeValidator
                                                                               runat="server"
                                                                               ID="rangtxtDtVencAntAbaEJ"
                                                                               Type="Date"
                                                                               ControlToValidate="txtDtVencAntAbaEJ"
                                                                               MaximumValue="31/12/9999"
                                                                               MinimumValue="31/12/1000"
                                                                               ErrorMessage="Data Inválida"
                                                                               ForeColor="Red"
                                                                               Display="Dynamic" ValidationGroup="grpParametros4" 
                                                                               />
                                                                        <asp:TextBox ID="txtDtVencAntAbaEJ" runat="server"  placeholder="Data de Vencimento do mês anterior"/>
                                                                            <br />

                                                                       <center><asp:Button ID="btnCobEJ" runat="server" Text="Cadastrar" OnClick="btnCobEJ_Click" ValidationGroup="grpParametros4" OnClientClick="return confirm('Deseja realmente Inserir este registro?')" /></center> 
                                                                       </div>

                                                                        <div id="DivSucessoAba4" class="form-style-8 align-center" runat="server" style="background: #F6FFEC;border: 1px solid #89B755; border-radius: 3px;" visible="false">
                                                                                        
                                                                                             <h3> Registro incluído com sucesso!</h3>
                                                                                            
                                                                                             
                                                                                             <asp:Label  id="lbl2Aba4" runat="server" Text="Próxima Rotina a ser processada - Aba 5º Aviso de Cobrança"  ></asp:Label>
                                                                         </div>
                                                                    </ContentTemplate>
                                                    </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarAvisoCob" HeaderText=" 5º Aviso de Cancelamento " runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="divPrincipalAba5" class="form-style-8" runat="server">
			                                                                               <center><h2> Geração da carta de aviso de cancelamento </h2></center>
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
                                                                                             <asp:TextBox ID="txtNumLoteAba4" runat="server"  placeholder="Número do Lote" onkeypress="mascara(this, soNumeros)"/>
                                                                                          <center>    <asp:Button ID="btnAviso" runat="server" Text="Processar" OnClick="btnAviso_Click" ValidationGroup="grpParametros4" OnClientClick="return confirm('Deseja realmente iniciar o processo?')" /></center> 
			                                                                 </div>

                                                                        <div id="DivSucessoAba5" class="form-style-8 align-center" runat="server" style="background: #F6FFEC;border: 1px solid #89B755; border-radius: 3px;" visible="false">
                                                                                        
                                                                                             <h3> Rotina processada com sucesso!</h3>
                                                                                            
                                                                                             
                                                                                             <asp:Label  id="Label1" runat="server" Text="Próxima Rotina a ser processada - Aba 6º Relatórios"  ></asp:Label>
                                                                         </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarRel" HeaderText="6º Relatórios" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
			                                                                 <div id="Div1" class="form-style-8" runat="server" >
			                                                                            <center><h2> Relatórios - Boletos de Cobrança Saúde </h2></center>
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
                                                                                        <asp:TextBox ID="txtNumLoteAba5" runat="server"  placeholder="número do Lote" onkeypress="mascara(this, soNumeros)"/>

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
                                                                                                          <asp:ListItem Value="6" Text="Boleto impressão procarta"/>
                                                                                                          <asp:ListItem Value="8" Text="Endereço Nulo"/>
                                                                                                          <asp:ListItem Value="12" Text="Inadimplentes - Digna"/>
                                                                                                          <asp:ListItem Value="13" Text="Todos os Flags"/>
                                                                                                          <asp:ListItem Value="14" Text="Todas as cobranças"/>

                                                                                        </asp:DropDownList><br />
                                                                                         <center> 
                                                                                                 <asp:Button ID="btnRel" runat="server" Text="Processar"  ValidationGroup="grpParametros5" OnClick="btnRel_Click" /><br/><br/>
                                                                                                 <asp:label id="lblCount" runat="server" visible="false" Font-Bold="true" ForeColor="Red"/>   
                                                                                          </center>
			                                                                 </div>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarTxt" HeaderText="7º Arquivos TXT" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
                                                                        <Center>

			                                                                 <div id="DivGerar" class="form-style-8" runat="server" >
			                                                                           <h2> Geração de arquivos txt </h2>
                                                                                             <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba6" 
                                                                                               ControlToValidate="txtNumLoteAba6" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros6"
                                                                                              /> 
                                                                                             <asp:TextBox ID="txtNumLoteAba6" runat="server"  placeholder="Número do Lote" onkeypress="mascara(this, soNumeros)"/>
                                                                                              
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
                                                                                        </asp:DropDownList><br />

                                                                                   
                                                                                       <asp:Button ID="btnGerarTxt" runat="server" Text="Gerar" OnClick="btnGerarTxt_Click"  ValidationGroup="grpParametros6"/><br /><br />
                                                                                       
                                                                                  <div Style="text-align:left">
                                                                                       <asp:label id="lblTxt1" runat="server" Text="Boletos de Cobrança txt foi gerado com sucesso!" visible="false" Font-Bold="true" ForeColor="Red" /><br />
                                                                                       <asp:label id="lblTxt2" runat="server" Text="Boletos Judiciais txt foi gerado com sucesso!" visible="false" Font-Bold="true" ForeColor="Red"/><br />
                                                                                       <asp:label id="lblTxt3" runat="server" Text="Cartas de Aviso de Cancelamento txt foi gerado com sucesso!" visible="false" Font-Bold="true" ForeColor="Red"/><br />
                                                                                       <asp:label id="lblTxt4" runat="server" Text="Cartas de Aviso de Inadimplência txt foi gerado com sucesso!" visible="false" Font-Bold="true" ForeColor="Red"/><br />
                                                                                       <asp:label id="lblTxt5" runat="server" Text="Cartas de Aviso de Inadimplência - Digna txt foi gerado com sucesso!" visible="false" Font-Bold="true" ForeColor="Red"/><br />
                                                                                 </div>
                                                                                  
			                                                                 </div>

                                                                     

                                                                            
                                                                        </Center>
	                                                                </ContentTemplate> 
                                                         </ajax:TabPanel>
                                                    <ajax:TabPanel ID="TabGerarPdf" HeaderText=" 8º Download PDF" runat="server" TabIndex="0">
                                                                    <ContentTemplate>
                                                                        <Center>
			                                                                 <div id="Div6" class="form-style-8" runat="server" >
			                                                                           <h2> Download de arquivos PDF - MCM Gráfica </h2>
                                                                                            <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqtxtNumLoteAba8" 
                                                                                               ControlToValidate="txtNumLoteAba8" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros8"
                                                                                              /> 
                                                                                             <asp:TextBox ID="txtNumLoteAba8" runat="server"  placeholder="número do Lote na MCM" onkeypress="mascara(this, soNumeros)"/>
                                                                                                <asp:RequiredFieldValidator 
                                                                                               runat="server" 
                                                                                               ID="ReqddlTipoPdf" 
                                                                                               ControlToValidate="ddlTipoPdf" 
                                                                                               ErrorMessage="Campo Obrigatório" 
                                                                                               ForeColor="Red" Display="Dynamic" 
                                                                                               ValidationGroup="grpParametros8"
                                                                                              /> 
                                                                                             <asp:DropDownList runat="server" ID="ddlTipoPdf" AutoPostBack="true" >
                                                                                                          <asp:ListItem Value="1" Text="COBSAUDC"  />
                                                                                                          <asp:ListItem Value="2" Text="CAISAUD"  />
                                                                                                          <asp:ListItem Value="3" Text="COBSAUDAR" />
                                                                                                          <asp:ListItem Value="4" Text="CIDSAUD"/>
                                                                                                          <asp:ListItem Value="5" Text="CACSAUD"  />
                                                                                     
                                                                                        </asp:DropDownList><br/><br/>

                                                                                 <asp:Button ID="BtnDownloadAba8" runat="server" Text="Pesquisar" OnClick="BtnDownloadAba8_Click"  ValidationGroup="grpParametros8"/><br /><br />
			                                                                 </div>

                                                                            <div id="DivPesquisaAba8" class="form-style-8" runat="server"  visible="false">
                                                                              <asp:GridView id="gridViewMcm" runat="server"  Width="100%" OnRowCommand="gridViewMcm_RowCommand" EmptyDataText="Não existem arquivos para este lote">
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
