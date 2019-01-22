<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadTabelaPorte.aspx.cs" Inherits="IntegWeb.Saude.Web.CadTabelaPorte" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title"></div>
        <div class="tabelaPagina">

            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                    <ContentTemplate>
                                <ajax:TabContainer ID="TabContainer" AutoPostBack="True" runat="server" ActiveTabIndex="0" OnActiveTabChanged="TabContainer_ActiveTabChanged">
                                            <ajax:TabPanel ID="TabCadastrar" HeaderText="Cadastrar Vigência" runat="server" TabIndex="0">
                                                         <ContentTemplate>
                                                                     
                                                                            <center>
                                                                                <div id="divCadastro" class="form-style-8" runat="server">
                                                                                      <h2>Cadastrar Vigência de Porte</h2>
                                                                                       <asp:DropDownList runat="server" ID="ddlTipoCadastro" OnSelectedIndexChanged="ddlTipoCadastro_SelectedIndexChanged" AutoPostBack="true">
                                                                                           <asp:ListItem Value="1" Text="Por Prestador"></asp:ListItem>
                                                                                           <asp:ListItem Value="2" Text="Por Classe"></asp:ListItem>
                                                                                       </asp:DropDownList>
                                                                                 </div> 
                                                                                 <div id="divSucesso" style="background:PaleGreen" class="form-style-8" runat="server" visible="false">
                                                                                        <h3>Vigência cadastrada com Sucesso!</h3>
                                                                                  </div>    
                                                                            </center>
                                                    

                                                             <div style="margin: 0 auto; display: table" id="divConv" runat="server" visible="false">
                                                                            <center>
                                                                                <div class="form-style-8">
                                                                                      <form>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="reqddlConv" ControlToValidate="ddlConv" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/>
                                                                                        <asp:DropDownList runat="server" ID="ddlConv"></asp:DropDownList>
                                                                                         <asp:RequiredFieldValidator runat="server" ID="ReqddlDtVig" ControlToValidate="ddlDtVig" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/>
                                                                                        <asp:DropDownList runat="server" ID="ddlDtVig"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="ReqtxtIniDatVigencia" ControlToValidate="txtIniDatVigencia" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros"/> 
                                                                                        <asp:RangeValidator
																											  runat="server"
																											  ID="rangTxtIniValidade"
																											  Type="Date"
																											  ControlToValidate="txtIniDatVigencia"
																											  MaximumValue="31/12/9999"
																											  MinimumValue="31/12/1000"
																											  ErrorMessage="Data Inválida"
																											  ForeColor="Red"
																											  Display="Dynamic" ValidationGroup="grpParametros" 
																											  />
                                                                                        <asp:TextBox runat="server" ID="txtIniDatVigencia" CssClass="date" MaxLength="10" placeholder="Data Inicio da Vigência do Porte"/>
                                                                                        <asp:RangeValidator
																											  runat="server"
																											  ID="RangetxtFimValidade"
																											  Type="Date"
																											  ControlToValidate="txtFimDatVigencia"
																											  MaximumValue="31/12/9999"
																											  MinimumValue="31/12/1000"
																											  ErrorMessage="Data Inválida"
																											  ForeColor="Red"
																											  Display="Dynamic" ValidationGroup="grpParametros" 
																											  />
                                                                                        <asp:TextBox runat="server" ID="txtFimDatVigencia" CssClass="date" MaxLength="10" placeholder="Data Fim da Vigência do Porte (Opcional)"  ValidationGroup="grpParametros"/>
                                                                                        <asp:Button ID="Button1" runat="server" Text="Cadastrar" OnClick="Button1_Click" ValidationGroup="grpParametros"/>
                                                                                      </form>
                                                                                    </div> 
                                                                                    
                                                                            </center>
                                                                     </div>
                                                             
                                                             <div style="margin: 0 auto; line-height: 25px; display: table" id="divClasse" runat="server" visible="false">
                                                                            <center>
                                                                                <div class="form-style-8">
                                                                                      <form>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="ReqddlClasse" ControlToValidate="ddlClasse" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros2"/>
                                                                                        <asp:DropDownList runat="server" ID="ddlClasse"></asp:DropDownList>
                                                                                         <asp:RequiredFieldValidator runat="server" ID="ReqddlDtVig2" ControlToValidate="ddlDtVig2" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros2"/>
                                                                                        <asp:DropDownList runat="server" ID="ddlDtVig2"></asp:DropDownList>
                                                                                        <asp:TextBox runat="server" placeholder="Código da Tabela (Opcional)" onkeypress="mascara(this, soNumeros)" ID="txtCobTab"/>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="ReqtxtIniDatVigencia2" ControlToValidate="txtIniDatVigencia2" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros2"/> 
                                                                                        <asp:RangeValidator
																											  runat="server"
																											  ID="RangeValidator1"
																											  Type="Date"
																											  ControlToValidate="txtIniDatVigencia2"
																											  MaximumValue="31/12/9999"
																											  MinimumValue="31/12/1000"
																											  ErrorMessage="Data Inválida"
																											  ForeColor="Red"
																											  Display="Dynamic" ValidationGroup="grpParametros" 
																											  />
                                                                                        <asp:TextBox runat="server" ID="txtIniDatVigencia2" CssClass="date" MaxLength="10" placeholder="Data Inicio da Vigência do Porte"/>
                                                                                        <asp:RangeValidator
																											  runat="server"
																											  ID="RangeValidator2"
																											  Type="Date"
																											  ControlToValidate="txtFimDatVigencia2"
																											  MaximumValue="31/12/9999"
																											  MinimumValue="31/12/1000"
																											  ErrorMessage="Data Inválida"
																											  ForeColor="Red"
																											  Display="Dynamic" ValidationGroup="grpParametros" 
																											  />
                                                                                        <asp:TextBox runat="server" ID="txtFimDatVigencia2" CssClass="date" MaxLength="10" placeholder="Data Fim da Vigência do Porte (Opcional)"/>
                                                                                        <asp:Button ID="btnCadClasse" runat="server" Text="Cadastrar" ValidationGroup="grpParametros2"  OnClick="btnCadClasse_Click"/>
                                                                                      </form>
                                                                                    </div>   
                                                                            </center>
                                                                     </div>
                                                         </ContentTemplate>
                                            </ajax:TabPanel>
                                            <ajax:TabPanel ID="TabConsultarPrestador" HeaderText="Consultar Vigência" runat="server" TabIndex="1">
                                                         <ContentTemplate>
                                                                     
                                                                            <center>
                                                                                 <div class="form-style-8">
                                                                                     <h2>Consultar Vigência de Porte Cadastrada</h2>
                                                                                           <asp:DropDownList runat="server" ID="ddlTipoCadastro2" OnSelectedIndexChanged="ddlTipoCadastro2_SelectedIndexChanged" AutoPostBack="true">
                                                                                                <asp:ListItem Value="1" Text="Por Prestador"></asp:ListItem>
                                                                                                <asp:ListItem Value="2" Text="Por Classe"></asp:ListItem>
                                                                                           </asp:DropDownList>
                                                                                </div>  
                                                                                <div class="form-style-8" runat="server" visible="false" id="divConv2">
                                                                                    <asp:Label ID="lblButtonSel" runat="server" Text="Label" Visible="false"/>
                                                                                    <asp:RequiredFieldValidator runat="server" ID="ReqtxtCodConv" ControlToValidate="txtCodConv" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros4"/> 
                                                                                    <asp:TextBox id="txtCodConv" runat="server" placeholder="Digite o código do prestador"></asp:TextBox>
                                                                                    <asp:Button id="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" ValidationGroup="grpParametros4" style="margin-right:100px"/>
                                                                                    <asp:Button ID="btnListar" runat="server" Text="Pesquisar Todos os Prestadores" OnClick="btnListar_Click"/>
                                                                                </div>

                                                                                <div class="form-style-8" runat="server" visible="false" id="divClasse2">
                                                                                    <asp:RequiredFieldValidator runat="server" ID="ReqtxtCodClasse" ControlToValidate="txtCodClasse" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros5"/> 
                                                                                    <asp:TextBox id="txtCodClasse" runat="server" placeholder="Digite o código da Classe"></asp:TextBox>
                                                                                    <asp:Button id="btnPesquisarClasse" runat="server" Text="Pesquisar" OnClick="btnPesquisarClasse_Click" ValidationGroup="grpParametros5"  style="margin-right:140px"/>
                                                                                    <asp:Button ID="btnListarClasse" runat="server" Text="Pesquisar todas as Classes" OnClick="btnListarClasse_Click"/>
                                                                                </div>

                                                                                <div id="DivGridConv" runat="server">
                                                                                <asp:GridView 
                                                                                        ID="gridViewLista"
                                                                                        runat="server"
                                                                                        AutoGenerateColumns="false"
                                                                                        PageSize = "10"
                                                                                        AllowPaging="True"
                                                                                        EmptyDataText="Não Existem convenentes com vigências cadastradas"
                                                                                        Visible ="false"
                                                                                        DataKeyNames = "NUM_SEQ"
                                                                                        OnPageIndexChanging="gridViewLista_PageIndexChanging"
                                                                                        OnRowDataBound="gridViewLista_RowDataBound"
                                                                                        OnRowEditing="gridViewLista_RowEditing"
                                                                                        OnRowCommand="gridViewLista_RowCommand"
                                                                                        OnRowCancelingEdit="gridViewLista_RowCancelingEdit"
                                                                                        OnRowUpdating="gridViewLista_RowUpdating"
                                                                                        >
                                                                                        <Columns>
                                                                                                    <asp:BoundField DataField="NUM_SEQ" HeaderText="nº sequencial" ReadOnly="true"  visible="false"/>
                                                                                                    <asp:BoundField DataField="COD_CONVENENTE" HeaderText="Cod Convenente" ReadOnly="true"/>
																									<asp:BoundField DataField="NOM_CONVENENTE" HeaderText="Convenente" ReadOnly="true"/>
																									<asp:BoundField DataField="DT_VIG_PORTE" HeaderText="Data vig Porte" DataFormatString="{0:d}" ReadOnly="true"/>
																									<asp:BoundField DataField="DT_INI_VIG" HeaderText="Data Inicio" DataFormatString="{0:d}" ReadOnly="true"/>
                                                                                                    <asp:TemplateField HeaderText="Data Fim">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label runat="server" ID="lblDtFimVigGrid" Text='<%# Eval("DT_FIM_VIG","{0:d}") %>'/>
                                                                                                            </ItemTemplate>
                                                                                                            <EditItemTemplate>
                                                                                                                <asp:TextBox runat="server" ID="TxtDtFimVigGrid" CssClass="date" placeholder="Digite a Data Fim" MaxLength="10"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator runat="server" ID="ReqTxtDtFimVigGrid" ControlToValidate="TxtDtFimVigGrid" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic"  />
                                                                                                                <asp:RangeValidator
                                                                                                                    runat="server"
                                                                                                                    ID="rangTxtDtFimVigGrid"
                                                                                                                    Type="Date"
                                                                                                                    ControlToValidate="TxtDtFimVigGrid"
                                                                                                                    MaximumValue="31/12/9999"
                                                                                                                    MinimumValue="31/12/1000"
                                                                                                                    ErrorMessage="Data Inválida"
                                                                                                                    ForeColor="Red"
                                                                                                                    Display="Dynamic" />
                                                                                                            </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                      <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Incluir Data Fim" CssClass="button"   />
                                                                                                                      <asp:Button ID="btnAtualizar" runat="server" CommandName="Update" Text="Atualizar" CssClass="button"  visible="false"/>
                                                                                                                     <asp:Button ID="BtnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" CssClass="button" OnClientClick="return confirm('Tem certeza que deseja abandonar a operação?');"  visible="false" CausesValidation="false" />
                                                                                                                     
                                                                                                                </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    
                                                                                            
	
                                                                                        </Columns>
                                                                                             <PagerStyle Font-Size="Smaller"/>
																							 <PagerSettings mode="Numeric" Position="Bottom"/>
                                                                                    </asp:GridView>
                                                                                    </div>
                                                                           
                                                                                <div id="DivGridClasse" runat="server">
                                                                                <asp:GridView 
                                                                                        ID="gridViewListaClasse"
                                                                                        runat="server"
                                                                                        AutoGenerateColumns="false"
                                                                                        PageSize = "10"
                                                                                        AllowPaging="True"
                                                                                        EmptyDataText="Não Existem Classes com vigências cadastradas"
                                                                                        Visible ="false"
                                                                                        DataKeyNames = "NUM_SEQ"
                                                                                        OnPageIndexChanging="gridViewListaClasse_PageIndexChanging"
                                                                                        OnRowDataBound="gridViewListaClasse_RowDataBound"
                                                                                        OnRowEditing="gridViewListaClasse_RowEditing"
                                                                                        OnRowCommand="gridViewListaClasse_RowCommand"
                                                                                        OnRowCancelingEdit="gridViewListaClasse_RowCancelingEdit"
                                                                                        OnRowUpdating="gridViewListaClasse_RowUpdating"
                                                                                        >
                                                                                        <Columns>
                                                                                                    <asp:BoundField DataField="NUM_SEQ" HeaderText="nº sequencial" ReadOnly="true" Visible="False"/>
                                                                                                    <asp:BoundField DataField="COD_CLASSE" HeaderText="Cod Classe" ReadOnly="true"/>
                                                                                                    <asp:BoundField DataField="NOM_CLASSE" HeaderText=" Desc Classe" ReadOnly="true"/>
																									<asp:BoundField DataField="COD_TAB_REC" HeaderText="cod Tab Recurso" ReadOnly="true"/>
																									<asp:BoundField DataField="DT_VIG_PORTE" HeaderText="Data vig Porte" DataFormatString="{0:d}" ReadOnly="true"/>
																									<asp:BoundField DataField="DT_INI_VIG" HeaderText="Data Inicio" DataFormatString="{0:d}" ReadOnly="true"/>
                                                                                                    <asp:TemplateField HeaderText="Data Fim">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label runat="server" ID="lblDtFimVigGridClasse" Text='<%# Eval("DT_FIM_VIG","{0:d}") %>'/>
                                                                                                            </ItemTemplate>
                                                                                                            <EditItemTemplate>
                                                                                                                <asp:TextBox runat="server" ID="TxtDtFimVigGridClasse" CssClass="date" placeholder="Digite a Data Fim" MaxLength="10"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator runat="server" ID="ReqTxtDtFimVigGridClasse" ControlToValidate="TxtDtFimVigGridClasse" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="" />
                                                                                                                <asp:RangeValidator
                                                                                                                    runat="server"
                                                                                                                    ID="rangTxtDtFimVigGridClasse"
                                                                                                                    Type="Date"
                                                                                                                    ControlToValidate="TxtDtFimVigGridClasse"
                                                                                                                    MaximumValue="31/12/9999"
                                                                                                                    MinimumValue="31/12/1000"
                                                                                                                    ErrorMessage="Data Inválida"
                                                                                                                    ForeColor="Red"
                                                                                                                    Display="Dynamic" ValidationGroup="" />
                                                                                                            </EditItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                      <asp:Button ID="btnEditarClasse" runat="server" CommandName="Edit" Text="Incluir Data Fim" CssClass="button"   />
                                                                                                                      <asp:Button ID="btnAtualizarClasse" runat="server" CommandName="Update" Text="Atualizar" CssClass="button"  visible="false" />
                                                                                                                     <asp:Button ID="BtnCancelarClasse" runat="server" CommandName="Cancel" Text="Cancelar" CssClass="button" OnClientClick="return confirm('Tem certeza que deseja Cancelar a operação?');"  visible="false" CausesValidation="false" />
                                                                                                                     
                                                                                                                </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    
                                                                                            
	
                                                                                        </Columns>
                                                                                             <PagerStyle Font-Size="Smaller"/>
																							 <PagerSettings mode="Numeric" Position="Bottom"/>
                                                                                    </asp:GridView>
                                                                                    </div>
                                                                                 </center>
                                                                   
                                                         </ContentTemplate>
                                            </ajax:TabPanel>
                                            <ajax:TabPanel ID="TabConsultar" HeaderText="Consultar Portes" runat="server" TabIndex="1">
                                                 <ContentTemplate>
                                                                     
                                                                            <center>
                                                                                    
                                                                                <div class="form-style-8">
                                                                                    <h2>Consultar Porte de  Procedimentos</h2>
                                                                                    <asp:RequiredFieldValidator runat="server" ID="ReqtxtDtVig" ControlToValidate="txtDtVig" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpParametros3"/> 
                                                                                      <asp:RangeValidator
																											  runat="server"
																											  ID="RangetxtDtVig"
																											  Type="Date"
																											  ControlToValidate="txtDtVig"
																											  MaximumValue="31/12/9999"
																											  MinimumValue="31/12/1000"
																											  ErrorMessage="Data Inválida"
																											  ForeColor="Red"
																											  Display="Dynamic" ValidationGroup="grpParametros" 
																											  />   
																					<asp:TextBox id="txtDtVig" runat="server" placeholder="Digite a Vigência do Porte" CssClass="date" MaxLength="10"/>
                                                                                    <asp:Button id="btnPesquisarPorte" runat="server" Text="Pesquisar"  OnClick="btnPesquisarPorte_Click" ValidationGroup="grpParametros3"/>
                                                                                </div>

                                                                                <div>
                                                                                <asp:GridView 
                                                                                        ID="gridViewListaPorte"
                                                                                        runat="server"
                                                                                        PageSize ="10"
                                                                                        AutoGenerateColumns="false"
                                                                                        AllowPaging="True"
                                                                                        EmptyDataText="Não Existe Porte cadastrado nesta Vigência"
                                                                                        Visible ="false"
                                                                                        OnPageIndexChanging="gridViewListaPorte_PageIndexChanging"
                                                 
                                                                                        >
                                                                                        <Columns>
                                                                                                    <asp:BoundField DataField="PRECODPORTEREC" HeaderText="Porte" ItemStyle-Width ="165" ItemStyle-HorizontalAlign="Center" />
																									<asp:BoundField DataField="VPRDATVALIDADE" HeaderText="Validade" DataFormatString="{0:d}" ItemStyle-Width ="165" ItemStyle-HorizontalAlign="Center" />
																									<asp:BoundField DataField="VPRVALPORTEREC" HeaderText="Valor"  ItemStyle-Width ="165" ItemStyle-HorizontalAlign="Center" />			
	
                                                                                        </Columns>
                                                                                        <PagerStyle Font-Size="Smaller"/>
																					    <PagerSettings mode="Numeric" Position="Bottom"/>
                                                                                    </asp:GridView>
                                                                                    </div>
                                                                            </center>
                                                                   
                                                </ContentTemplate>
                                             </ajax:TabPanel>                                
                                </ajax:TabContainer>
                    </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>

