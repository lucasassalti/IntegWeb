<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadastroCisao.aspx.cs" Inherits="IntegWeb.Saude.Web.CadastroCisao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $(".date").datepicker({
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }

        });
    </script>
    <asp:UpdatePanel runat="server" ID="upCisao">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsCisao" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="h_title">
                </div>
                <h1>Cisão / Fusão</h1>

                <ajax:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <ajax:TabPanel ID="tabSelect" HeaderText="Selecionar" runat="server" TabIndex="0">
                        <ContentTemplate>
                            <div class="tabelaPagina">
                                <table>
                                    <tr>
                                        <td><b>Consultar Matrículas:</b></td>
                                    </tr>
                              </table>
                                <table>
                                    <tr>
                                        <td>Matrícula:</td>
                                        <td>Mês:</td>
                                        <td>Ano:</td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtMatricula" Width="85px" runat="server" MaxLength="9" onkeypress="mascara(this, soNumeros)"></asp:TextBox> </td>
                                         <td><asp:DropDownList ID="drpMes" runat="server">
                                                            <asp:ListItem Selected="True" Text="--Selecine--" Value="0">
                                       
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                        </asp:DropDownList>

                                        </td>
                                        <td><asp:TextBox ID="txtAno"  Width="85px" runat="server"  MaxLength="4" onkeypress="mascara(this, soNumeros)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Button ID="btnConsultar" CssClass="button" CausesValidation="false" OnClick="btnConsultar_Click" runat="server" Text="Consultar" /></td>
                                        <td><asp:Button ID="btnLimpar" CssClass="button" CausesValidation="false" OnClick="btnLimpar_Click" runat="server" Text="Limpar"/></td>
                                    </tr>
                                    </table>

                                <asp:GridView DataKeyNames="COD_EMPRS_ANT, NUM_RGTRO_EMPRG_ANT" OnRowCancelingEdit="grdFusao_RowCancelingEdit" OnRowDeleting="grdFusao_RowDeleting" OnRowEditing="grdFusao_RowEditing" OnRowUpdating="grdFusao_RowUpdating" AllowPaging="true"
                                    AutoGenerateColumns="False" EmptyDataText="A consulta não retornou registros" CssClass="Table" ClientIDMode="Static" ID="grdFusao" runat="server" OnPageIndexChanging="grdFusao_PageIndexChanging" PageSize="10">
                                    <Columns>
                                        <asp:BoundField HeaderText="Empresa Anterior    " ReadOnly="true" DataField="COD_EMPRS_ANT" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Matrícula Anterior" ReadOnly="true" DataField="NUM_RGTRO_EMPRG_ANT" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Empresa Atual">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCdEmpAtu" onkeypress="mascara(this, soNumeros)" runat="server" Text='<%# Bind("COD_EMPRS_ATU") %>' Width="90px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator18" ControlToValidate="txtCdEmpAtu" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCdEmpAtu" runat="server" Text='<%# Bind("COD_EMPRS_ATU") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Matrícula Atual">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCdMatAtu" onkeypress="mascara(this, soNumeros)" runat="server" Text='<%# Bind("NUM_RGTRO_EMPRG_ATU") %>' Width="90px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator19" ControlToValidate="txtCdMatAtu" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCdMatAtu" runat="server" Text='<%# Bind("NUM_RGTRO_EMPRG_ATU") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dígito">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDig" runat="server" onkeypress="mascara(this, soNumeros)" Text='<%# Bind("NUM_DIGVER_ATU") %>' Width="90px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator20" ControlToValidate="txtDig" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDig" runat="server" Text='<%# Bind("NUM_DIGVER_ATU") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Data Base Cisão">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDtBase" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" runat="server" Text='<%# Bind("DAT_BASE_CISAO", "{0:d}") %>' Width="90px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator21" ControlToValidate="txtDtBase" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDtBase" runat="server" Text='<%# Bind("DAT_BASE_CISAO", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Data Atualização">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtdtAtu" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" runat="server" Text='<%# Bind("DAT_ATUALIZACAO", "{0:d}") %>' Width="90px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" TabIndex="0" Text="Obrigatório!" ID="RequiredFieldValidator22" ControlToValidate="txtdtAtu" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldtAtu" runat="server" Text='<%# Bind("DAT_ATUALIZACAO", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" />&nbsp;&nbsp;
                                       <asp:Button ID="btCancelar" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="return confirm('Tem certeza que deseja abandonar a atualização do cadastro?');" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Button ID="btEditar" CausesValidation="false" CssClass="button" runat="server" Text="Editar" CommandName="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btExcluir" CausesValidation="false" CssClass="button" runat="server" Text="Excluir" CommandName="Delete" OnClientClick="return confirm('Tem certeza que deseja excluir?');"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel ID="TabelaInert" HeaderText="Inserir" runat="server" TabIndex="1">
                        <ContentTemplate>
                            <div class="formTable">

                                <asp:Panel ID="pnlMain" runat="server" GroupingText="<b>Data Base</b>" HorizontalAlign="Center" Height="100px" Width="200px">
                                    <table style="padding:10px">
                                        <tr>
                                            <td>Data Base Cisão
                                                  <asp:RequiredFieldValidator ID="rfvDtCisao" runat="server" Text="*" ControlToValidate="txtBaseCisao"
                                                      ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data Base Cisão válida (mm/dd/yyyy)" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtBaseCisao" ErrorMessage="Informe uma Data Pagamento válida (mm/dd/yyyy)"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                                    Text="*"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>Data Base Atualização
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ControlToValidate="txtBaseAtu"
                                                   ForeColor="Red" Font-Bold="true" ErrorMessage="Informe uma Data Base Atualização válida (mm/dd/yyyy)" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server"
                                                    ControlToValidate="txtBaseAtu" ErrorMessage="Informe uma Data Pagamento válida (mm/dd/yyyy)"
                                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                                    Text="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox MaxLength="10" onkeypress="mascara(this, data)" CssClass="date" ID="txtBaseCisao" runat="server" Width="190px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox MaxLength="10" onkeypress="mascara(this, data)" CssClass="date" ID="txtBaseAtu" runat="server" Width="190px"></asp:TextBox>
                                            </td>

                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="Panel1" runat="server"  GroupingText="<b>Empresa Anterior</b>" HorizontalAlign="Center" Height="100px" Width="200px">

                                    <table style="padding:10px">
                                        <tr>
                                            <td>Código da Empresa Anterior</td>
                                            <td>Nº da Matrícula Anterior</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCodEmpAnt" onkeypress="mascara(this, soNumeros)" AutoPostBack="true" OnTextChanged="txtCodEmpAnt_TextChanged" runat="server" Width="190px"></asp:TextBox><br />

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMatriculaAnt" onkeypress="mascara(this, soNumeros)" runat="server" AutoPostBack="true" OnTextChanged="txtMatriculaAnt_TextChanged" Width="190px"></asp:TextBox></br>
                                                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEmpAnt" Font-Size="Smaller" Visible="false" ForeColor="Red" runat="server" Text="Label">COMPANHIA PIRATININGA DE FORCA E LUZ</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMatAnt" Font-Size="Smaller" Visible="false" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                </asp:Panel><br/>

                                <asp:Panel ID="Panel2" runat="server" GroupingText="<b>Empresa Atual</b>" HorizontalAlign="Center" Height="100px" Width="200px">

                                    <table style="padding:10px">
                                        <tr>
                                            <td>Código da Empresa Atual</td>
                                            <td>Nº da Matrícula Atual</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCodEmpAtu" onkeypress="mascara(this, soNumeros)" AutoPostBack="true" OnTextChanged="txtCodEmpAtu_TextChanged" runat="server" Width="190px"></asp:TextBox><br />

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMatriculaAtu" onkeypress="mascara(this, soNumeros)" runat="server" AutoPostBack="true" OnTextChanged="txtMatriculaAtu_TextChanged" Width="190px"></asp:TextBox></br>
                                                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEmpAtu" Font-Size="Smaller" Visible="false" ForeColor="Red" runat="server" Text="Label">COMPANHIA PIRATININGA DE FORCA E LUZ</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMatAtu" Font-Size="Smaller" Visible="false" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>


                                </asp:Panel>
                                <br/><br/>


                                <table>
                                    <tr>
                                        <td>Dígito</td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDigito" onkeypress="mascara(this, soNumeros)" runat="server" Width="100px"></asp:TextBox>
                                            <asp:CheckBox ID="ckConsulta" OnCheckedChanged="ckConsulta_CheckedChanged" AutoPostBack="true" Text="Consultar Dígito." runat="server" />
                                        </td>

                                    </tr>
                                </table>

                                <table>
                                    <tr>
                                        <td><asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" CssClass="button" runat="server" Text="Salvar" /> </td>
                                        <td><asp:Button ID="btnLimpar2" CssClass="button" CausesValidation="false" OnClick="btnLimpar2_Click" runat="server" Text="Limpar"/></td>


                                    </tr>
                                </table>

                            </div>
                            <br />
                            <div id="divMatricula" runat="server" visible="false" class="tabelaPagina">
                                <%--       <table>
                                    <tr>
                                        <td>Digite a Matrícula:</td>
                                        <td>
                                            <asp:TextBox ID="txtPesquisa" onkeypress="mascara(this, soNumeros)" runat="server" Width="190px"></asp:TextBox>
                                            <asp:Button ID="btnPesquisa" CausesValidation="false" CssClass="button" OnClick="btnPesquisa_Click" runat="server" Text="Pesquisar" />
                                        </td>
                                    </tr>
                                </table>--%>
                                <asp:GridView AutoGenerateColumns="False" EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdPesquisa" runat="server">

                                    <Columns>
                                        <asp:BoundField HeaderText="Empresa" DataField="cod_emprs" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Nome" DataField="nom_particip" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Matrícula" DataField="num_rgtro_emprg_atu" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Dígito" DataField="DGV_PARTICIP" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>

                </ajax:TabContainer>

            </div>


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
</asp:Content>
