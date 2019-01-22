<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CargaMatMedPrestadores.aspx.cs" Inherits="IntegWeb.Saude.Web.CargaMatMedPrestadores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

    </script>
  
    <div class="full_w">

        <div class="h_title"></div>

        <div class="tabelaPagina">
       
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" AutoPostBack="True" OnActiveTabChanged="TabContainer_ActiveTabChanged" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="TabCarga" HeaderText="Realizar Carga" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div style="margin: 0 auto; line-height: 25px; display: table">
                                    <center>
																
											<div class="form-style-8">
                                               <h2>Cadastrar materiais e medicamentos</h2>
                                                   <form>
                                                       <asp:RequiredFieldValidator runat="server" ID="reqDdlConvenente" ControlToValidate="ddlConvenente" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/>
                                                       <asp:DropDownList ID="ddlConvenente" runat="server" OnSelectedIndexChanged="ddlConvenente_SelectedIndexChanged" AutoPostBack="true" class="button"/>
                                                       <asp:RequiredFieldValidator runat="server" ID="ReqddlTbMatMed" ControlToValidate="ddlTbMatMed" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/> 
                                                       <asp:DropDownList ID="ddlTbMatMed" runat="server" class="button" />
                                                       <asp:RequiredFieldValidator runat="server" ID="ReqddlTipoCarga" ControlToValidate="ddlTipoCarga" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/> 
                                                       <asp:DropDownList ID="ddlTipoCarga" runat="server" class="button">
															<asp:ListItem Text="MATERIAL" Value ="1"/>
															<asp:ListItem Text="MEDICAMENTO" Value="2"/>
														</asp:DropDownList>
                                                       <asp:RequiredFieldValidator runat="server" ID="ReqtxtDatVigencia" ControlToValidate="txtDatVigencia" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/> 
                                                       <asp:RangeValidator
																											  runat="server"
																											  ID="rangTxtValidade"
																											  Type="Date"
																											  ControlToValidate="txtDatVigencia"
																											  MaximumValue="31/12/9999"
																											  MinimumValue="31/12/1000"
																											  ErrorMessage="Data Inválida"
																											  ForeColor="Red"
																											  Display="Dynamic" ValidationGroup="grpParametros" 
																											  />
                                                       <asp:TextBox runat="server" ID="txtDatVigencia" CssClass="date" MaxLength="10" placeholder="Data de vigência"/>
                                                       <asp:FileUpload ID="fileExcel" runat="server" CssClass="button"/><br /> <br />
													 <asp:Button ID="btnCarregar" OnClientClick="return postbackButtonClick();" runat="server" Text="Carregar" CssClass="button"  ValidationGroup="grpParametros" OnClick ="btnCarregar_Click"/>
                                                     </form>
                                            </div> 			
												               <asp:Label ID="lblTotalPrc" runat="server" Font-Bold="true"  />

																<div class="form-style-8" id="divResultado" style="text-align:center;" runat="server" visible ="false">
																	 
                                                                    
                                                                    <div ><h4> Quantidade de Materiais e Medicamentos Cadastrados:</h4></div>
																		 <div>
																				<div>
																						<asp:Label ID="Label1"  text="Mat/Med:" runat="server"/>
																						<div>
																								<asp:Label ID="lblTotalCad" runat="server"/>
																						</div>
																				</div>
																				<div>
																						<asp:Label ID="Label2"  text="Cobertura:" runat="server"/>

																						<div>
																								 <asp:Label ID="lblReCob" runat="server"/>
																						</div>
																				</div>
																				<div>
																						<asp:Label ID="Label3"  text="Especialidade:" runat="server"/>
																						<div>
																								<asp:Label ID="lblEsp" runat="server"/>
																						</div>
																				</div><br/>
                                                                             <div><h4> Quantidade de Materiais e Medicamentos Atualizados:</h4></div>
                                                                                    <div>
																						</b> <asp:Label ID="Label4"  text="Mat/Med:" runat="server"/>
																						<div>
																								<asp:Label ID="lblTotalAtu" runat="server"/>
																						</div>
																				</div>
																		 </div>
																</div>
                                               
																		<div id="divGrid" runat="server"  class="n_error" visible ="false">
																				 <h4>Quantidade de Materiais e Medicamentos que não foram atualizados:  </h4>
																				 <asp:Label ID="lblNAtu" runat="server" ForeColor ="red" Font-Bold="true"/> 
																				 <br/>
																				 <asp:GridView ID="gridViewProc" runat="server"
																				  AutoGenerateColumns="false" 
																				  class="button"
																				  AllowPaging="True"
																				  PageSize ="10"
																				  EmptyDataText="Não houve procedimentos que não foram atualizados"
																				  OnPageIndexChanging="gridViewProc_PageIndexChanging">
																							 <Columns>
																									<asp:BoundField DataField="COD_PROCEDIMENTO" HeaderText="Procedimento"/>
																									<asp:BoundField DataField="DESCRICAO" HeaderText="Descrição"/>
																									<asp:BoundField DataField="PRECO" HeaderText="Preço"/>
																									<asp:BoundField DataField="OBS_ERRO" HeaderText="Motivo"/>
																							 </Columns>
																							 <PagerStyle Font-Size="Smaller"/>
																							 <RowStyle Font-Size="Smaller"/>
																							 <PagerSettings mode="Numeric" Position="Bottom"/>
																				  </asp:GridView>
																				 
																		</div>
																
									</center>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabRealizCarga" HeaderText="Consultar cargas" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <div style="margin: 0 auto; line-height: 25px; display: table">
                                    <center>
																<h1>Cargas de Materiais e Medicamentos Realizadas</h1>
																<br/>
																<div>
																		 <table>
																				<tr>
																						<td>
																								<asp:DropDownList ID="ddlConvenenteAba2" runat="server"  AutoPostBack="true" class="button" OnSelectedIndexChanged="ddlConvenenteAba2_SelectedIndexChanged"/>
																						</td>
																				</tr> 
																		 </table>
																</div>
																<div id="divGridCarga"  runat="server" visible="False">
																		<asp:GridView id="gridCarga" runat="server"  
																			 AutoGenerateColumns="false" 
																			 class="button"
																			 OnSelectedIndexChanged="gridCarga_SelectedIndexChanged"                                                              
																		     DataKeyNames="COD_CARGA"
																		     AllowPaging="True"
																		     EmptyDataText="Não houve nenhuma carga para este convenente"
																		     PageSize="5"
																		     OnPageIndexChanging="gridCarga_PageIndexChanging">
																					<Columns>
																							<asp:BoundField DataField="COD_CARGA" HeaderText="Cod Carga"/>
																						    <asp:BoundField DataField="DAT_EXEC" HeaderText="Dat_Execução" DataFormatString="{0:d}"/> 
																						    <asp:BoundField DataField="DAT_VIG" HeaderText="Vigência" DataFormatString="{0:d}"/> 
																						    <asp:BoundField DataField="COD_CONVENENTE" HeaderText="Cod Convenente"/>
																						    <asp:BoundField DataField="NM_ARQ_IMPORT" HeaderText="Arquivo"/>
																						    <asp:BoundField DataField="TOTAL_INCLUIDO" HeaderText="Total Incluido"/>
																						    <asp:BoundField DataField="TOTAL_NAO_INCLUIDO" HeaderText="Total Não Incluido"/>
																						    <asp:ButtonField Text ="Proc. não incluidos" ButtonType="Button" CommandName="Select" ControlStyle-CssClass="button-error" ControlStyle-Height="30px" ControlStyle-Width="90px" ControlStyle-Font-Size="Smaller" />
																					</Columns>
																					 <PagerStyle Font-Size="Smaller"/>
																					 <RowStyle Font-Size="Smaller"/>
																					<PagerSettings mode="Numeric" Position="Bottom"/>
																		</asp:GridView>
																</div>	

															    <div id="divGrid3" runat="server"  class="n_error" visible ="false">
																		<h4>Procedimentos que não foram inseridos:</h4> 
																		<br/>
																		<asp:GridView ID="gridViewProcAba2" runat="server" 
																			AutoGenerateColumns="false" 
																			class="button"
																			AllowPaging="True"
																			PageSize ="10"
																			EmptyDataText="Não houve procedimentos que não foram inseridos"
																			OnPageIndexChanging="gridViewProcAba2_PageIndexChanging">
																				 <Columns>
																							 <asp:BoundField DataField="COD_PROCEDIMENTO" HeaderText="Procedimento"/>
																							 <asp:BoundField DataField="DESCRICAO" HeaderText="Descrição"/>
																							 <asp:BoundField DataField="PRECO" HeaderText="Preço"/>
																							 <asp:BoundField DataField="OBS_ERRO" HeaderText="Motivo"/>
																				 </Columns>
																				 <RowStyle Font-Size="Smaller"/>
																				 <PagerStyle Font-Size="Smaller"/>
																				 <PagerSettings mode="Numeric" Position="Bottom"/>
																		</asp:GridView>
																</div>
														 </center>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
                <Triggers>
                     <asp:PostBackTrigger ControlID="TabContainer$TabCarga$btnCarregar" />
                    </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProg" DisplayAfter="0" runat="server">
                <ProgressTemplate>
                    <div id="carregando">
                        <div class="carregandoTxt">
                            <img src="img/processando.gif" />
                            <br />
                            <h2>Processando...</h2>
                                

                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>


        </div>
    </div>

</asp:Content>
