<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="PriorizaChamado.aspx.cs" Inherits="IntegWeb.Intranet.Web.PriorizaChamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function ScrollRight() {
            window.scrollTo(200, 0);
        }

        function ScrollLeft() {
            window.scrollTo(0, 0);
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsOrgao" runat="server" ForeColor="Red" ShowMessageBox="true" ShowSummary="false" />
            <div class="full_w">

            <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color: silver; margin-right: 10px; float: right; margin-top: 10px;">
                    <tr>
                        <td valign="top" width="262" style="font-weight: bold;">
                                Área
                        </td>
                        <td valign="top" width="141" style="font-weight: bold;width: 160px;" align="right">
                                Qtd máxima de priorizações em andamento
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="262">
                                Cadastro
                        </td>
                        <td valign="top" width="141">
                                1
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="262">
                                Saúde
                        </td>
                        <td valign="top" width="141">
                                2
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="262">
                                Previdência
                        </td>
                        <td valign="top" width="141">
                                2
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="262">
                                Comunicações/Ouvidoria/Atendimento
                        </td>
                        <td valign="top" width="141">
                                1
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="262">
                                Tesouraria/Controladoria/Administrativo
                        </td>
                        <td valign="top" width="141">
                                1
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="262">
                                Investimentos
                        </td>
                        <td valign="top" width="141">
                                1
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="262">
                                Demais áreas
                        </td>
                        <td valign="top" width="141">
                                1
                        </td>
                    </tr>
                </table>

                <div class="tabelaPagina">

                    <h1>Priorização de Chamados</h1>
                    <table>
                        <tr>
                            <td style="width: 100px;">Nº Chamado:
                            </td>
                            <td style="width: 100px;">
                                <asp:TextBox ID="txtNumChamado" runat="server"  style="width: 130px;" onkeypress="mascara(this, soNumeros)"></asp:TextBox></td>
                            <td>Area:
                            </td>
                            <td>
                                <asp:DropDownList id="ddlSiglaArea" runat="server" style="width:150px;" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="<TODAS>"></asp:ListItem>
                                    <asp:ListItem Value="AA" Text="AA"></asp:ListItem>
                                    <asp:ListItem Value="AAC" Text="AAC"></asp:ListItem>
                                    <asp:ListItem Value="AAG" Text="AAG"></asp:ListItem>
                                    <asp:ListItem Value="AAR" Text="AAR"></asp:ListItem>
                                    <asp:ListItem Value="AAT" Text="AAT"></asp:ListItem>                                    
                                    <asp:ListItem Value="AS" Text="AS"></asp:ListItem>
                                    <asp:ListItem Value="ASC" Text="ASC"></asp:ListItem>
                                    <asp:ListItem Value="ASN" Text="ASN"></asp:ListItem>
                                    <asp:ListItem Value="ASP" Text="ASP"></asp:ListItem>
                                    <asp:ListItem Value="AT" Text="AT"></asp:ListItem>
                                    <asp:ListItem Value="ATC" Text="ATC"></asp:ListItem>
                                    <asp:ListItem Value="ATD" Text="ATD"></asp:ListItem>
                                    <asp:ListItem Value="ATI" Text="ATI"></asp:ListItem>
                                    <asp:ListItem Value="IF" Text="IF"></asp:ListItem>
                                    <asp:ListItem Value="II" Text="II"></asp:ListItem>
                                    <asp:ListItem Value="IV" Text="IV"></asp:ListItem>
                                    <asp:ListItem Value="OUV" Text="OUV"></asp:ListItem>
                                    <asp:ListItem Value="PA" Text="PA"></asp:ListItem>
                                    <asp:ListItem Value="PG" Text="PG"></asp:ListItem>
                                    <asp:ListItem Value="PI" Text="PI"></asp:ListItem>
                                    <asp:ListItem Value="PJ" Text="PJ"></asp:ListItem>
                                    <asp:ListItem Value="PM" Text="PM"></asp:ListItem>
                                    <asp:ListItem Value="PMR" Text="PMR"></asp:ListItem>
                                    <asp:ListItem Value="RA" Text="RA"></asp:ListItem>
                                    <asp:ListItem Value="RAC" Text="RAC"></asp:ListItem>
                                    <asp:ListItem Value="RAE" Text="RAE"></asp:ListItem>
                                    <asp:ListItem Value="RS" Text="RS"></asp:ListItem>
                                    <asp:ListItem Value="RSC" Text="RSC"></asp:ListItem>
                                    <asp:ListItem Value="RSP" Text="RSP"></asp:ListItem>
                                    <asp:ListItem Value="RSS" Text="RSS"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Analista:
                            </td>
                            <td>
                                <asp:DropDownList id="ddlPesqANALISTAS" runat="server" style="width:150px;" AutoPostBack="true"></asp:DropDownList></td>
                        </tr>
                        <tr>
                           <td>Status:
                           </td>
                           <td>
                                <asp:DropDownList id="ddlPesqSTATUS" runat="server" style="width:130px;" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="<TODOS>"></asp:ListItem>
                                    <asp:ListItem Value="EM ANDAMENTO" Text="EM ANDAMENTO"></asp:ListItem>
                                    <asp:ListItem Value="PAUSADO" Text="PAUSADO"></asp:ListItem>
                                    <asp:ListItem Value="CONCLUÍDO" Text="CONCLUÍDO"></asp:ListItem>
                                    <asp:ListItem Value="DESPRIORIZADO" Text="DESPRIORIZADO"></asp:ListItem>
                                </asp:DropDownList>
                           </td>
                            <td>Neste status a mais de:
                            </td>
                            <td>
                                <asp:DropDownList id="ddlIdadeStatus" runat="server" style="width:150px;" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="<TODOS>"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15 DIAS"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30 DIAS"></asp:ListItem>
                                    <asp:ListItem Value="60" Text="60 DIAS"></asp:ListItem>
                                    <asp:ListItem Value="90" Text="90 DIAS"></asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Consultar" />
                                <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Limpar" />
                                <asp:Button ID="btnNovo" OnClick="btnNovo_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Novo" Visible="false" />
                                <asp:Button ID="btnEmailAlerta" OnClick="btnEmailAlerta_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight(); return confirm('Tem certeza que deseja enviar e-mail de alerta para cada chamado priorizado neste grid?');" runat="server" Text="Enviar e-mail de alerta" Visible="false" />
                            </td>
                            <td style="text-align:center;font-weight:bold;">
                               Usuário :&nbsp<asp:Label ID="lblUsuario" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <asp:ObjectDataSource runat="server" ID="odsPriorizaChamados"
                        TypeName="Intranet.Aplicacao.BLL.PriorizaChamadoBLL"
                        SelectMethod="GetPriorizaChamados"
                        SelectCountMethod="SelectCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter"
                        UpdateMethod="UpdateData"
                        InsertMethod="InsertData"
                        DeleteMethod="DeleteData">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtNumChamado" Name="paramNumChamado" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlSiglaArea" Name="paramSiglaArea" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlPesqSTATUS" Name="paramStatus" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlPesqANALISTAS" Name="paramLoginAnalista" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlIdadeStatus" Name="paramIdadeStatus" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdPriorizaChamados" runat="server" 
                        DataKeyNames="CHAMADO" 
                        OnRowCancelingEdit="grdPriorizaChamados_RowCancelingEdit" 
                        OnRowEditing="grdPriorizaChamados_RowEditing" 
                        OnRowCreated="GridView_RowCreated"
                        OnRowDataBound="grdPriorizaChamados_RowDataBound"
                        AllowPaging="true" 
                        AllowSorting="true"
                        AutoGenerateColumns="False" 
                        EmptyDataText="A consulta não retornou registros" 
                        CssClass="Table" 
                        ClientIDMode="Static" 
                        PageSize="20" 
                        DataSourceID="odsPriorizaChamados" OnRowCommand="grdPriorizaChamados_RowCommand">
                        <PagerSettings
                            Visible="true"
                            PreviousPageText="Anterior"
                            NextPageText="Próxima"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:TemplateField HeaderText="Nº Chamado" SortExpression="CHAMADO" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign ="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCHAMADO" runat="server" Text='<%# Bind("CHAMADO") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtChamado" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descrição" SortExpression="TITULO" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblTITULO" runat="server" Text='<%# Bind("TITULO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTITULO" runat="server" Text='<%# Bind("TITULO") %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTITULO" runat="server" Width="100%"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Area" SortExpression="AREA" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAREA" runat="server" Text='<%# Bind("AREA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList id="ddlAREA" runat="server" style="width:100px;"></asp:DropDownList>
                                    <asp:HiddenField ID="hidAREA" runat="server" Value='<%# Bind("AREA") %>'></asp:HiddenField>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList id="ddlAREA" runat="server" style="width:100px;"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Analista" SortExpression="ANALISTA" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblANALISTA" runat="server" Text='<%# Bind("ANALISTA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList id="ddlANALISTA" runat="server" style="width:150px;"></asp:DropDownList>
                                    <asp:HiddenField ID="hidID_USUARIO" runat="server" Value='<%# Bind("ID_USUARIO") %>'></asp:HiddenField>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList id="ddlANALISTA" runat="server" style="width:150px;"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>        
                                    <asp:HiddenField ID="hidSTATUS" runat="server" Value='<%# Bind("STATUS") %>'></asp:HiddenField>                            
                                    <asp:DropDownList id="ddlSTATUS" runat="server" style="width:130px;" SelectedValue='<%# Bind("STATUS") %>'>
                                        <asp:ListItem Value="EM ANDAMENTO" Text="EM ANDAMENTO"></asp:ListItem>
                                        <asp:ListItem Value="PAUSADO" Text="PAUSADO"></asp:ListItem>
                                        <asp:ListItem Value="CONCLUÍDO" Text="CONCLUÍDO"></asp:ListItem>
                                        <asp:ListItem Value="DESPRIORIZADO" Text="DESPRIORIZADO"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSTATUS" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList id="ddlSTATUS" runat="server" style="width:130px;">
                                        <asp:ListItem Value="EM ANDAMENTO" Text="EM ANDAMENTO"></asp:ListItem>
                                        <asp:ListItem Value="PAUSADO" Text="PAUSADO"></asp:ListItem>
                                        <asp:ListItem Value="CONCLUÍDO" Text="CONCLUÍDO"></asp:ListItem>
                                        <asp:ListItem Value="DESPRIORIZADO" Text="DESPRIORIZADO"></asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dt. Inclusão" SortExpression="DT_INCLUSAO" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                <EditItemTemplate>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDT_INCLUSAO" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                    <asp:TextBox id="txtDT_INCLUSAO" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" Width="100px" Text='<%# Bind("DT_INCLUSAO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="DT_INCLUSAO" runat="server" Text='<%# Bind("DT_INCLUSAO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server" ControlToValidate="txtDT_INCLUSAO" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                    <asp:TextBox id="txtDT_INCLUSAO" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" Width="100px" Text='<%# Bind("DT_INCLUSAO") %>'></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Dt. Término" SortExpression="DT_TERMINO" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" >
                                <EditItemTemplate>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" runat="server" ControlToValidate="txtDT_TERMINO" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                    <asp:TextBox id="txtDT_TERMINO" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" Width="100px" Text='<%# Bind("DT_TERMINO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="DT_TERMINO" runat="server" Text='<%# Bind("DT_TERMINO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ForeColor="Red" runat="server" ControlToValidate="txtDT_TERMINO" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                    <asp:TextBox id="txtDT_TERMINO" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10" Width="100px" Text='<%# Bind("DT_TERMINO") %>'></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Obs." SortExpression="OBS" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btObs" OnClientClick="ScrollRight(); $('.btObs').hide(); $('.lblOBS').show(); return false; " CausesValidation="false" CssClass="button btObs" runat="server" Text="Obs." Visible="false" />
                                    <asp:Label ID="lblOBS" runat="server" Text='<%# Bind("OBS") %>' Style="display:none" CssClass="lblOBS"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btObs" OnClientClick="ScrollRight(); $('.btObs').hide(); $('#txtOBS').show(); return false; " CausesValidation="false" CssClass="button btObs" runat="server" Text="Obs." Visible="true" />
                                    <asp:TextBox id="txtOBS" runat="server" Width="300px" Height="50px" Text='<%# Bind("OBS") %>' TextMode="MultiLine" MaxLength="300" Style="display:none"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox id="txtOBS" runat="server" Width="300px" Height="50px" Text='<%# Bind("OBS") %>' TextMode="MultiLine" MaxLength="300" CssClass="lblOBS"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <EditItemTemplate>
                                    <asp:Button ID="btSalvar" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Update" OnClientClick="$('#hidID_USUARIO').val($('#ddlANALISTA').val());  $('#hidAREA').val($('#ddlAREA').val());"  />&nbsp;&nbsp;
                                    <asp:Button ID="btCancelar"  CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="Cancel" OnClientClick="ScrollLeft();" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" OnClientClick="ScrollRight();" CausesValidation="false" CssClass="button" runat="server" Text="Editar" CommandName="Edit" Visible="false" />&nbsp;&nbsp;
                                    <asp:Button ID="btExcluir"  CausesValidation="false" CssClass="button" runat="server" Text="Excluir" CommandName="Delete" OnClientClick="ScrollLeft(); return confirm('Atenção!! \n\nDeseja realmente excluir este registro?');" Visible="false" />                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btSalvarNovo" OnClientClick="ScrollRight();" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="AddNew" />
                                    <asp:Button ID="btCancelar"  CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" CommandName="CancelAdd" OnClientClick="ScrollLeft();" />
                                </FooterTemplate>
                            </asp:TemplateField>



                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
