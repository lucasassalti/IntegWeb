<% @ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ControleFlags.aspx.cs" Inherits="IntegWeb.Financeira.Web.ControleFlags" %>

<% @ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<% @ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

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
            <h1>Controle de FLAGS</h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>
                    <asp:Table ID="tbLayoutCampos" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                Empresa: <asp:TextBox ID="txtEmp" runat="server" CssClass="text" Width="50px" MaxLength="3" onkeypress="return isNumber(event)"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                Matricula: <asp:TextBox ID="txtMatr" runat="server" CssClass="text" Width="100px" onkeypress="return isNumber(event)" MaxLength="13"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                Nº Representante: <asp:TextBox ID="txtNumRepr" runat="server" CssClass="text" Width="100px" onkeypress="return isNumber(event)" MaxLength ="10" ></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                Nome: <asp:TextBox ID="txtNome" runat="server" CssClass="text" Width="200px" ></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>    
                        <asp:TableRow>
                          
                      <asp:TableCell>
                         <asp:RadioButtonList ID="rbdFlag" runat="server" RepeatDirection="Horizontal"  AutoPostBack="true">
                          <asp:ListItem Text="Flag Judicial" Value="J" />
                          <asp:ListItem Text="Flag Insucesso" Value="I" />
                          
                              </asp:RadioButtonList>
                                           
                               </asp:TableCell>
        
                            

                        </asp:TableRow>
                    </asp:Table>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPesquisar" runat="server" CssClass="button" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                  &nbsp;&nbsp;&nbsp; <asp:Button ID="btnInserir" runat="server" CssClass="button" OnClick="btnInserir_Click" Text="Inserir" />
                    &nbsp;&nbsp;&nbsp; <asp:Button ID="btnGerar" runat="server" CssClass="button" OnClick="btnGerar_Click" Text="Gerar Relatório" />
                      &nbsp;&nbsp;&nbsp; <asp:Button ID="btnLimpar" runat="server" CssClass="button" OnClick="btnLimpar_Click" Text="Limpar" />
                    
                    <p></p>
                                <asp:GridView ID="gridFlag" 
                                            runat="server"
                                            CssClass="panelTable"
                                            AutoGenerateColumns="false"
                                            PageSize ="10"
                                            AllowPaging="True"
                                             ClientIDMode="Static"
                                            Width="1000px"
                                          OnPageIndexChanging="gridFlag_PageIndexChanging"
                                          OnRowCommand="gridFlag_RowCommand"
                                             >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Empresa" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("cod_emprs") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Matrícula">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumRgtro" runat="server" Text='<%# Bind("num_rgtro_emprg") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Representante">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumRepr" runat="server" Text='<%# Bind("num_idntf_rptant") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nome">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNome" runat="server" Text='<%# Bind("nom_emprg_repres") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Data Exclusão">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDatExclusao" runat="server" Text='<%# Bind("dt_exclusao") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Data Inclusão">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDataIncl" runat="server" Text='<%# Bind("dt_inclusao") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Solicitante">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSolicIncl" runat="server" Text='<%# Bind("nom_solic_inclusao") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Flag">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipoFlag" runat="server" Text='<%# Bind("Tipo_flag") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
<%--                                        <asp:TemplateField HeaderText=" Insucesso">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlagInsucesso" runat="server" Text='<%# Bind("flag_insucesso") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnExcluir" runat="server" CommandName="Excluir" CssClass="button" Text="Excluir" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
             

                                        
                                    </Columns>
                                    </asp:GridView>
                      


                </ContentTemplate>
           </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
                <ProgressTemplate>
                    <div id="carregando">
                        <div class="carregandoTxt">
                            <img src="img/processando.gif" />
                            <br />
                            <h2>Processando. Aguarde...</h2></div></div></ProgressTemplate></asp:UpdateProgress>

        </div>

    </div>

    </asp:Content>