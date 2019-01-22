<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Popup.Master" CodeBehind="ConsultaProvisoriaAtendimento.aspx.cs" Inherits="IntegWeb.Intranet.Web.ConsultaProvisoriaAtendimento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Atendimento Provisório</h1>
            <asp:UpdatePanel runat="server" ID="UpdatePanel">
                <ContentTemplate>
                    <div id="divImpressao" runat="server">
                        <asp:Panel ID="pnlCabecalho" runat="server">
                            <asp:Label ID="lblRepresentante" runat="server" Visible="false" Enabled="false" />
                            <asp:Label ID="lblDependente" runat="server" Visible="false" Enabled="false" />
                            <asp:Table ID="tbPesquisa" runat="server">
                                <asp:TableRow runat="server" ID="trBaseUrl">
                                    <asp:TableCell runat="server">Empresa:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox ID="txtEmpresa" runat="server" Enabled="false" ReadOnly="true" Width="120px"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">Registro</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox ID="txtRegistroEmpregado" runat="server" Enabled="false" ReadOnly="true" Width="90px"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">Validade da Autorização:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox ID="txtDataAutorizacao" CssClass="date" onkeypress="mascara(this, data)" runat="server" Width="120px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDataAutorizacao" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1" runat="server">Plano:</asp:TableCell>
                                    <asp:TableCell ID="TableCell2" runat="server">
                                        <asp:DropDownList ID="ddlPlano" runat="server" OnSelectedIndexChanged="ddlPlano_SelectedIndexChanged" Width="124px" AutoPostBack="true">
                                            <asp:ListItem Text="DIGNA/AMH" Value="1111"></asp:ListItem>
                                            <asp:ListItem Text="PES/NOSSO" Value="1112"></asp:ListItem>
                                            <asp:ListItem Text="Em Trânsito" Value="1113"></asp:ListItem>
                                            <asp:ListItem Text="Conveniada" Value="1114"></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="trEmTransito">
                                    <asp:TableCell runat="server">Para:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox ID="txtPara" runat="server" Width="120px"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">Fax:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox ID="txtFax" runat="server" Width="90px" onkeypress="mascara(this, soNumeros)" MaxLength="20"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">Local de atendimento:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox ID="txtLocal" runat="server" Width="120px" MaxLength="45"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">Att:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox ID="txtAtt" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tbCheckBox" runat="server">
                                <asp:TableRow runat="server" ID="trEmTransitoCheck1">
                                    <asp:TableCell runat="server">
                                        <asp:RadioButton ID="radTipoAtend1" runat="server" AutoPostBack="true" OnCheckedChanged="radTipoAtend1_CheckedChanged" Checked="true"></asp:RadioButton>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label ID="lblTipoAtend1" runat="server" AutoPostBack="true">Consultas, exames, internações e procedimentos médicos necessários, conforme contratados 
                                            (não válido para odontologia e farmácia).Informamos que esta Fundação CESP se responsabiliza pelas despesas decorrentes deste atendimento.</asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="trEmTransitoCheck2">
                                    <asp:TableCell runat="server">
                                        <asp:RadioButton ID="radTipoAtend2" runat="server" AutoPostBack="true" OnCheckedChanged="radTipoAtend2_CheckedChanged"></asp:RadioButton>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label ID="lblTipoAtend2" runat="server" AutoPostBack="true" Visible="false">Autorização específica apenas para situação de urgência / emergência. 
                                            Esta autorização não é válida para odontologia, consultas / exames eletivos e farmácias. No caso de internação contatar a Fundação CESP.</asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="tr5" Visible="false">
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" Visible="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <asp:ObjectDataSource runat="server" ID="odsProvisoriaAtendimento"
                                TypeName="IntegWeb.Saude.Aplicacao.BLL.Processos.provisoriaAtendimentoBLL"
                                SelectMethod="GetDataPgto"
                                EnablePaging="True"
                                SortParameterName="sortParameter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlPlano" Name="plano" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="txtEmpresa" Name="empresa" PropertyName="Text" Type="Int32" />
                                    <asp:ControlParameter ControlID="txtRegistroEmpregado" Name="rgtEmpregado" PropertyName="Text" Type="Int32" />
                                    <asp:ControlParameter ControlID="lblRepresentante" Name="representante" PropertyName="Text" Type="Int32" />
                                    <asp:ControlParameter ControlID="lblDependente" Name="dependente" PropertyName="Text" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:GridView ID="grdProvisoriaAtendimento"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataSourceID="odsProvisoriaAtendimento"
                                EmptyDataText="A consulta não retornou registros"
                                AllowSorting="True"
                                CssClass="Table"
                                ClientIDMode="Static"
                                PageSize="40"
                                Visible="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" Checked="true" runat="server" Text="" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" Checked="true" runat="server" Text="" class="span_checkbox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NOM_PARTICIP" HeaderText="Nome" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="DES_PLANO" HeaderText="Nome do Plano" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="DAT_VALIDADECI" HeaderText="Validade CI" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="DAT_PARTO" HeaderText="Dat Parto" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="DAT_NASCM_EMPRG" HeaderText="Nascimento" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" Visible="false" />
                                    <asp:BoundField DataField="NOM_MAE" HeaderText="Nome Mãe" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" Visible="false" />
                                    <asp:BoundField DataField="NUM_CPF" HeaderText="CPF" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" Visible="false" />
                                </Columns>
                            </asp:GridView>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnImprimir" runat="server" CssClass="button" Text="Imprimir" OnClick="btnImprimir_Click" Visible="false" />
                                        <asp:Button ID="btnDigna" runat="server" CssClass="button" Text="Inserir Manualmente" OnClick="btnDigna_Click" Visible="false" Enabled="false" />
                                        <asp:Button ID="btnPesNosso" runat="server" CssClass="button" Text="Inserir Manualmente" OnClick="btnPesNosso_Click" Visible="false" Enabled="false" />
                                        <asp:Button ID="btnEmTransito" runat="server" CssClass="button" Text="Inserir Manualmente" OnClick="btnEmTransito_Click" Visible="false" Enabled="false" />
                                        <asp:Button ID="btnConveniada" runat="server" CssClass="button" Text="Inserir Manualmente" OnClick="btnConveniada_Click" Visible="false" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                    <div id="divInserirDigna" runat="server" class="tabelaPagina" visible="false">
                        <h2>Inserir DIGNA/AMH</h2>
                        <asp:Panel ID="pnlBotaoDigna" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnImprimirNovoDigna" runat="server" CssClass="button" Text="Imprimir" ValidationGroup="grpNovoDigna" OnClick="btnImprimirNovoDigna_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelarDigna" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarDigna_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLimparDigna" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparDigna_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlInserirDigna1" runat="server">
                            <table>
                                <tr>
                                    <td>Nome:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomeDigna1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomeDigna1" ControlToValidate="txtNomeDigna1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoDigna" />
                                    </td>
                                    <td>Nome da mãe:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomeMaeDigna1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomeMaeDigna1" ControlToValidate="txtNomeMaeDigna1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoDigna" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Plano:</td>
                                    <td>
                                        <asp:TextBox ID="txtPlanoDigna1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqPlanoDigna1" ControlToValidate="txtPlanoDigna1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoDigna" />
                                    </td>
                                    <td>CPF:</td>
                                    <td>
                                        <asp:TextBox ID="txtCpfDigna1" runat="server" MaxLength="11" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCpfDigna1" ControlToValidate="txtCpfDigna1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoDigna" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Código:</td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoDigna1" runat="server" MaxLength="3" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCodigoDigna1" ControlToValidate="txtCodigoDigna1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoDigna" />
                                    </td>
                                    <td>Data de nascimento:</td>
                                    <td>
                                        <asp:TextBox ID="txtDataNascimentoDigna1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataNascimentoDigna1" ControlToValidate="txtDataNascimentoDigna1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoDigna" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangDataNascimentoDigna1"
                                            Type="Date"
                                            ControlToValidate="txtDataNascimentoDigna1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoDigna" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Data autorização:</td>
                                    <td>
                                        <asp:TextBox ID="txtDataAutorizacaoNovoDigna1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataAutorizacaoNovoDigna1" ControlToValidate="txtDataAutorizacaoNovoDigna1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoDigna" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangDataAutorizacaoNovoDigna1"
                                            Type="Date"
                                            ControlToValidate="txtDataAutorizacaoNovoDigna1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoDigna" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </div>

                    <div id="divInserirPesNosso" runat="server" class="tabelaPagina" visible="false">
                        <h2>Inserir PES/NOSSO</h2>
                        <asp:Panel ID="pnlBotaoPesNosso" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnImprimirNovoPesNosso" runat="server" CssClass="button" Text="Imprimir" ValidationGroup="grpNovo" OnClick="btnImprimirNovoPesNosso_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelarPesNosso" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarPesNosso_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLimparPesNosso" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparPesNosso_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlInserirPesNosso1" runat="server">
                            <table>
                                <tr>
                                    <td>Nome:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomePesNosso1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomePesNosso1" ControlToValidate="txtNomePesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                    <td>Nome da mãe:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomeMaePesNosso1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomeMaePesNosso1" ControlToValidate="txtNomeMaePesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Plano:</td>
                                    <td>
                                        <asp:TextBox ID="txtPlanoPesNosso1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqPlanoPesNosso1" ControlToValidate="txtPlanoPesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                    <td>CPF:</td>
                                    <td>
                                        <asp:TextBox ID="txtCpfPesNosso1" runat="server" MaxLength="11" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCpfPesNosso1" ControlToValidate="txtCpfPesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Código:</td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoPesNosso1" runat="server" MaxLength="3" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCodigoPesNosso1" ControlToValidate="txtCodigoPesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                    <td>Data de nascimento:</td>
                                    <td>
                                        <asp:TextBox ID="txtDataNascimentoPesNosso1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataNascimentoPesNosso1" ControlToValidate="txtDataNascimentoPesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangDataNascimentoPesNosso1"
                                            Type="Date"
                                            ControlToValidate="txtDataNascimentoPesNosso1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Parto:</td>
                                    <td>
                                        <asp:TextBox ID="txtPartoPesNosso1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPartoPesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="RangeValidator1"
                                            Type="Date"
                                            ControlToValidate="txtPartoPesNosso1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                    <td>Data autorização:</td>
                                    <td>
                                        <asp:TextBox ID="txtDataAutorizacaoNovoPesNosso1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataAutorizacaoNovoPesNosso1" ControlToValidate="txtDataAutorizacaoNovoPesNosso1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoPesNosso" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangDataAutorizacaoNovoPesNosso1"
                                            Type="Date"
                                            ControlToValidate="txtDataAutorizacaoNovoPesNosso1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoPesNosso" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                    <div id="divInserirEmTransito" runat="server" class="tabelaPagina" visible="false">
                        <h2>Inserir Em Transito</h2>
                        <asp:Panel ID="pnlBotaoEmTransito" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnImprimirNovoEmTransito" runat="server" CssClass="button" Text="Imprimir" ValidationGroup="grpNovoEmTransito" OnClick="btnImprimirNovoEmTransito_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelarEmTransito" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarEmTransito_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLimparEmTransito" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparEmTransito_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlInserirEmTransito1" runat="server">
                            <table>
                                <tr>
                                    <td>Nome:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomeEmTransito1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomeEmTransito1" ControlToValidate="txtNomeEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                    <td>Nome da mãe:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomeMaeEmTransito1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomeMaeEmTransito1" ControlToValidate="txtNomeMaeEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Plano:</td>
                                    <td>
                                        <asp:TextBox ID="txtPlanoEmTransito1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqPlanoEmTransito1" ControlToValidate="txtPlanoEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                    <td>CPF:</td>
                                    <td>
                                        <asp:TextBox ID="txtCpfEmTransito1" runat="server" MaxLength="11" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCpfEmTransito1" ControlToValidate="txtCpfEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Est. Civil:</td>
                                    <td>
                                        <asp:TextBox ID="txtEstCivilEmTransito1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqEstCivilEmTransito1" ControlToValidate="txtEstCivilEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                    <td>Grau Parentesco:</td>
                                    <td>
                                        <asp:TextBox ID="txtParentescoEmTransito1" runat="server" MaxLength="11" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqParentescoEmTransito1" ControlToValidate="txtParentescoEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Código:</td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoEmTransito1" runat="server" MaxLength="3" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCodigoEmTransito1" ControlToValidate="txtCodigoEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                    <td>Data de nascimento:</td>
                                    <td>
                                        <asp:TextBox ID="txtDataNascimentoEmTransito1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataNascimentoEmTransito1" ControlToValidate="txtDataNascimentoEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangDataNascimentoEmTransito1"
                                            Type="Date"
                                            ControlToValidate="txtDataNascimentoEmTransito1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Para:</td>
                                    <td>
                                        <asp:TextBox ID="txtParaEmTransito1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqParaEmTransito1" ControlToValidate="txtParaEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                    <td>Local:</td>
                                    <td>
                                        <asp:TextBox ID="txtLocalEmTransito1" runat="server" MaxLength="11" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqLocalEmTransito1" ControlToValidate="txtLocalEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>FAX:</td>
                                    <td>
                                        <asp:TextBox ID="txtFaxEmTransito1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqFaxEmTransito1" ControlToValidate="txtFaxEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                    <td>Quantidade Vias:</td>
                                    <td>
                                        <asp:TextBox ID="txtViasEmTransito1" runat="server" MaxLength="11" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqViasEmTransito1" ControlToValidate="txtViasEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Att:</td>
                                    <td>
                                        <asp:TextBox ID="txtAttEmTransito1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqAttEmTransito1" ControlToValidate="txtAttEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                    <td>Validade:</td>
                                    <td>
                                        <asp:TextBox ID="txtValidadeNovoEmTransito1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqValidadeNovoEmTransito1" ControlToValidate="txtValidadeNovoEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangValidadeNovoEmTransito1"
                                            Type="Date"
                                            ControlToValidate="txtValidadeNovoEmTransito1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Parto:</td>
                                    <td>
                                        <asp:TextBox ID="txtPartoEmTransito1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPartoEmTransito1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoEmTransito" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="RangeValidator2"
                                            Type="Date"
                                            ControlToValidate="txtPartoEmTransito1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoEmTransito" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                    <div id="divInserirConveniada" runat="server" class="tabelaPagina" visible="false">
                        <h2>Inserir Conveniada</h2>
                        <asp:Panel ID="pnlBotaoConveniada" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnImprimirNovoConveniada" runat="server" CssClass="button" Text="Imprimir" ValidationGroup="grpNovoConveniada" OnClick="btnImprimirNovoConveniada_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelarConveniada" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancelarConveniada_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLimparConveniada" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimparConveniada_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="pnlInserirConveniada1" runat="server">
                            <table>
                                <tr>
                                    <td>Nome:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomeConveniada1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomeConveniada1" ControlToValidate="txtNomeConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                    </td>
                                    <td>Nome da mãe:</td>
                                    <td>
                                        <asp:TextBox ID="txtNomeMaeConveniada1" runat="server" MaxLength="60" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqNomeMaeConveniada1" ControlToValidate="txtNomeMaeConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Plano:</td>
                                    <td>
                                        <asp:TextBox ID="txtPlanoConveniada1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqPlanoConveniada1" ControlToValidate="txtPlanoConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                    </td>
                                    <td>CPF:</td>
                                    <td>
                                        <asp:TextBox ID="txtCpfConveniada1" runat="server" MaxLength="11" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCpfConveniada1" ControlToValidate="txtCpfConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Código:</td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoConveniada1" runat="server" MaxLength="3" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqCodigoConveniada1" ControlToValidate="txtCodigoConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                    </td>
                                    <td>Data de nascimento:</td>
                                    <td>
                                        <asp:TextBox ID="txtDataNascimentoConveniada1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqDataNascimentoConveniada1" ControlToValidate="txtDataNascimentoConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangDataNascimentoConveniada1"
                                            Type="Date"
                                            ControlToValidate="txtDataNascimentoConveniada1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoConveniada" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Recurso:</td>
                                    <td>
                                        <asp:TextBox ID="txtRecursoConveniada1" runat="server" MaxLength="32" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqRecursoConveniada1" ControlToValidate="txtRecursoConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />

                                    <td>Validade da Autorização:</td>
                                    <td>
                                        <asp:TextBox ID="txtValidadeNovoConveniada1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqValidadeNovoConveniada1" ControlToValidate="txtValidadeNovoConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangValidadeNovoConveniada1"
                                            Type="Date"
                                            ControlToValidate="txtValidadeNovoConveniada1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoConveniada" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Data Emissão:</td>
                                    <td>
                                        <asp:TextBox ID="txtEmissaoConveniada1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                        <asp:RequiredFieldValidator runat="server" ID="reqEmissaoConveniada1" ControlToValidate="txtEmissaoConveniada1" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpNovoConveniada" />
                                        <asp:RangeValidator
                                            runat="server"
                                            ID="rangEmissaoConveniada1"
                                            Type="Date"
                                            ControlToValidate="txtEmissaoConveniada1"
                                            MaximumValue="31/12/9999"
                                            MinimumValue="31/12/1000"
                                            ErrorMessage="Data Inválida"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            ValidationGroup="grpNovoConveniada" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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

        </div>
    </div>
</asp:Content>
