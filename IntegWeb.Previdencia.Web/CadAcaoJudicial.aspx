<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadAcaoJudicial.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CadAcaoJudicial" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel runat="server" ID="upUpdatePanel">
        <ContentTemplate>
            <ajax:TabContainer ID="TabContainer" runat="server">
                <ajax:TabPanel ID="TbCadastro" HeaderText="Cadastro" runat="server" TabIndex="0">
                    <ContentTemplate>

                        <div class="full_w">
                            <div class="tabelaPagina">
                                <h1>Cadastro de Ações Judiciais</h1>
                                <div id="divPesquisa" runat="server">
                                    <table>
                                        <tr>
                                            <td>Empresa:
                                                <asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Matrícula:
                                                <asp:TextBox ID="txtPesqMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlPesquisa" runat="server" ClientIDMode="Static">
                                                    <asp:ListItem Text="Participante" Value="1" Selected="True" />
                                                    <asp:ListItem Text="CPF" Value="2" />
                                                    <asp:ListItem Text="Núm. Processo" Value="3" />
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPesquisa" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                                <asp:Button ID="btnPesqLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnPesqLimpar_Click" />
                                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" OnClick="btnNovo_Click" />
                                            </td>
                                        </tr>
                                    </table>


                                    <asp:ObjectDataSource ID="odsConsultaAcaoJudic" runat="server"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Concessao.CadAcaoJudicialBLL"
                                        SelectMethod="GetData"
                                        SelectCountMethod="GetDataCount"
                                        EnablePaging="True"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlPesquisa" Name="filType" PropertyName="SelectedValue"
                                                Type="Int32" />
                                            <asp:ControlParameter ControlID="txtPesquisa" Name="filValue" PropertyName="Text"
                                                Type="String" />
                                            <asp:ControlParameter ControlID="txtPesqEmpresa" Name="pEmpresa" PropertyName="Text"
                                                Type="Int32" />
                                            <asp:ControlParameter ControlID="txtPesqMatricula" Name="pMatricula" PropertyName="Text"
                                                Type="Int32" />

                                        </SelectParameters>
                                    </asp:ObjectDataSource>


                                    <asp:GridView ID="grdAcaoJudic" runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não Retornou Registros"
                                        AllowPaging="True"
                                        AllowSorting="True"
                                        CssClass="Table"
                                        Visible="False"
                                        DataSourceID="odsConsultaAcaoJudic"
                                        OnRowCommand="grdAcaoJudic_RowCommand"
                                        ClientIDMode="Static">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa" SortExpression="COD_EMPRS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Matricula" SortExpression="NUM_RGTRO_EMPRG">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMatricula" runat="server" Text='<%# Bind("NUM_RGTRO_EMPRG") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nome Participante" SortExpression="NOM_PARTICIP">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNomParticip" runat="server" Text='<%# Bind("NOM_PARTICIP") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fato Gerador" SortExpression="TIP_PLTO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescTiplto" runat="server" Text='<%# Bind("TIP_PLTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="N° Processo" SortExpression="NRO_PROCESSO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumProcesso" runat="server" Text='<%# Bind("NRO_PROCESSO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                                        Text="Abrir" CommandName="Editar" CommandArgument='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div id="divNovoCadastro" runat="server" visible="False" style="position: relative">
                                    <div style="position: absolute; left: 753px; width: 170px; display: inline-block; top: 0">
                                        <asp:Button ID="btnAnterior" runat="server" CssClass="button" Text="Anterior" OnClick="btnAnterior_Click" />
                                        <asp:Button ID="btnProximo" runat="server" CssClass="button" Text="Próximo" OnClick="btnProximo_Click" />
                                    </div>
                                    <asp:Panel ID="pnlCadastroAcaoJudic" runat="server">
                                        <table style="position: relative; float: left;">
                                            <tr class="cab_td">
                                                <td>Empresa</td>
                                                <td>Matrícula</td>
                                                <td>&nbsp</td>
                                                <td>CPF</td>
                                                <td>&nbsp</td>
                                                <td>Página</td>
                                                <td></td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td>
                                                    <asp:HiddenField ID="hfNUM_PAG" runat="server" />
                                                    <asp:HiddenField ID="hfCOD_ACAO_JUDIC" runat="server" />
                                                    <asp:TextBox ID="txtEmpresa" runat="server" Width="100px" MaxLength="3" onkeypress="mascara(this, soNumeros)" /></td>
                                                <td>
                                                    <asp:TextBox ID="txtMatricula" runat="server" Width="100px" MaxLength="10" onkeypress="mascara(this, soNumeros)" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="True" />
                                                </td>
                                                <td>&nbsp</td>
                                                <td>
                                                    <asp:TextBox ID="txtCPF" runat="server" MaxLength="11" onkeypress="mascara(this, soNumeros)" OnTextChanged="txtCPF_TextChanged" AutoPostBack="True" />
                                                </td>
                                                <td>&nbsp</td>
                                                <td>
                                                    <asp:Label ID="lblPagina" runat="server" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                     <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" Visible="False"/>
                                                </td>
                                            </tr>
                                        </table>

                                        <table>
                                            <tr class="cab_td">
                                                <td colspan="3">Participante:</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtNome" runat="server" Width="100%" MaxLength="50" />
                                                </td>
                                            </tr>

                                            <tr class="cab_td">
                                                <td colspan="3">Reclamante:</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtReclamante" runat="server" Width="100%" MaxLength="50" />
                                                </td>
                                            </tr>

                                            <tr class="cab_td">
                                                <td>Plano:</td>
                                                <td>Data DIB:</td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td>
                                                    <asp:DropDownList ID="ddlPlano" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDtDib" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                </td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td>Número do Processo</td>
                                                <td>Data Solicitação:</td>
                                                <td>Data Prazo</td>
                                                <td>Tipo Solicitação:</td>

                                            </tr>

                                            <tr class="cab_td">
                                                <td>
                                                    <asp:TextBox ID="txtNumProcesso" runat="server" Width="300px" MaxLength="100" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDtSolic" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDtPrazo" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipSolic" runat="server">
                                                        <asp:ListItem Text="---Selecione---" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="E-mail" Value="EMAIL"></asp:ListItem>
                                                        <asp:ListItem Text="Carta" Value="CARTA"></asp:ListItem>
                                                        <asp:ListItem Text="RunRun.it" Value="RUNRUNIT"></asp:ListItem>
                                                        <asp:ListItem Text="Outros" Value="OUTROS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td>Advogado:</td>
                                                <td>Pasta:</td>
                                                <td>Cálculo Apresentado:</td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td>
                                                    <asp:DropDownList ID="ddlLawyer" runat="server" ClientIDMode="Static" Width="300px">
                                                        <asp:ListItem Text="---Selecione---" Selected="True" />
                                                        <asp:ListItem Text="BIANCA SAMPAIO" Value="BIANCA SAMPAIO" />
                                                        <asp:ListItem Text="BRUNA DOS SANTOS" Value="BRUNA DOS SANTOS" />
                                                        <asp:ListItem Text="CAROLINE CONCEIÇÃO" Value="FERNANDA GARAVELLI" />
                                                        <asp:ListItem Text="CAROLINE DRAGANE" Value="CAROLINE DRAGANE" />
                                                        <asp:ListItem Text="FERNANDA GARAVELLI" Value="FERNANDA GARAVELLI" />
                                                        <asp:ListItem Text="FERNANDO LENCIONI" Value="FERNANDO LENCIONI" />
                                                        <asp:ListItem Text="ADRIANA DE CARVALHO" Value="ADRIANA DE CARVALHO" />
                                                        <asp:ListItem Text="RICHARD FLOR" Value="RICHARD FLOR" />
                                                        <asp:ListItem Text="RODRIGO RODRIGUES" Value="RODRIGO RODRIGUES" />
                                                        <asp:ListItem Text="GISELE ALVES" Value="GISELE ALVES" />
                                                        <asp:ListItem Text="DANIEL TEIXEIRA" Value="DANIEL TEIXEIRA" />
                                                        <asp:ListItem Text="LAMIS BATISTA" Value="LAMIS BATISTA" />
                                                        <asp:ListItem Text="LEILANE DE PAULA" Value="LEILANE DE PAULA" />
                                                        <asp:ListItem Text="RENATA MANTOVANI" Value="RENATA MANTOVANI" />
                                                        <asp:ListItem Text="RICARDO PASSARELLI" Value="RICARDO PASSARELLI" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPasta" runat="server" MaxLength="30" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCalcHomolog" runat="server">
                                                        <asp:ListItem Text="---Selecione---" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Funcesp" Value="FUNCESP"></asp:ListItem>
                                                        <asp:ListItem Text="Perito" Value="PERITO"></asp:ListItem>
                                                        <asp:ListItem Text="Reclamante" Value="RECLAMANTE"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>



                                            <tr class="cab_td">
                                                <td>Vara:</td>
                                                <td>Fato Gerador:</td>
                                                <td>Tipo Andamento</td>
                                                <td>Pólo-Ação Judicial</td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td>
                                                    <asp:TextBox ID="txtLocalVara" runat="server" MaxLength="100" Width="300px" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFatorGerador" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoAndamento" runat="server" ClientIDMode="Static" onchange="ddlTipoAndamento_onchange();">
                                                        <asp:ListItem Text="---Selecione---" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Assistente Técnico"></asp:ListItem>
                                                        <asp:ListItem Text="Concessão"></asp:ListItem>
                                                        <asp:ListItem Text="Cópia do Processo"></asp:ListItem>
                                                        <asp:ListItem Text="CRM"></asp:ListItem>
                                                        <asp:ListItem Text="Demonstrativo de calculo"></asp:ListItem>
                                                        <asp:ListItem Text="Depósito Judicial – PP"></asp:ListItem>
                                                        <asp:ListItem Text="Depósito Judicial – PR"></asp:ListItem>
                                                        <asp:ListItem Text="Elaboração de Calculo "></asp:ListItem>
                                                        <asp:ListItem Text="Formula de Cálculo (BSPS/BD)"></asp:ListItem>
                                                        <asp:ListItem Text="Impugnação de calculo "></asp:ListItem>
                                                        <asp:ListItem Text="Inicial"></asp:ListItem>
                                                        <asp:ListItem Text="Oficial"></asp:ListItem>
                                                        <asp:ListItem Text="Patrocinador"></asp:ListItem>
                                                        <asp:ListItem Text="Perito"></asp:ListItem>
                                                        <asp:ListItem Text="Revisão"></asp:ListItem>
                                                        <asp:ListItem Text="Esclarecimento Regulamentar"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPoloAcaoJudicial" runat="server" Width="80px">
                                                        <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="lblMedicao" class="cab_td" style="display: block;">
                                                <td>Nº Medição:</td>
                                                <td colspan="3"></td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td style="width: 110px!important; display: inline-block; float: left">
                                                    <asp:TextBox ID="txtMedicao" ClientIDMode="Static" runat="server" MaxLength="100" Width="100px" Style="display: none" />

                                                </td>
                                                <td colspan="3" style="position: absolute; float: left; width: 790px; left: 127px;">
                                                    <a id="box_trg" href="#" onclick="javascript:return boxchecklist_trg();" style="color: #333; border: 1px solid #aaa; width: 100%; display: none; text-align: center; padding: 2px 0; z-index: 100;">Depósitos Judiciais - Checklist</a>
                                                    <div id="box_checklist" style="display: block; position: absolute; width: 780px; height: 0px; left: 7px; top: 23px; background: rgba(255, 255, 255, .9); overflow: hidden; border: 1px solid #ccc;">
                                                        <ajax:TabContainer ID="TabContainer1" runat="server" CssClass="">
                                                            <ajax:TabPanel ID="TabPanel1" HeaderText="Créditos" runat="server">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <!-- creditos -->
                                                                        <tr>
                                                                            <td><span>Benefício Retroativo - PSAP</span><asp:TextBox ID="TextBox1" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Benefício Retroativo - BSPS</span><asp:TextBox ID="TextBox2" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Benefício Retroativo - BD / BPD</span><asp:TextBox ID="TextBox3" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Benefício Retroativo - CV</span><asp:TextBox ID="TextBox4" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Antecipação 25% BSPS</span><asp:TextBox ID="TextBox14" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Periciais e Custas - PSAP</span><asp:TextBox ID="TextBox5" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Periciais e Custas - BSPS</span><asp:TextBox ID="TextBox6" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Periciais e Custas - BD / BPD</span><asp:TextBox ID="TextBox8" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Periciais e Custas - CV</span><asp:TextBox ID="TextBox9" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Advocatícios e Multa - PSAP</span><asp:TextBox ID="TextBox10" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Advocatícios e Multa - BSPS</span><asp:TextBox ID="TextBox11" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Advocatícios e Multa - BD/BPD</span><asp:TextBox ID="TextBox12" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Honorários Advocatícios e Multa - CV</span><asp:TextBox ID="TextBox13" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span><b>Total de Créditos</b></span><asp:TextBox ID="TextBox15" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajax:TabPanel>
                                                            <ajax:TabPanel ID="TabPanel2" HeaderText="Débitos" runat="server">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <!-- debitos -->
                                                                        <tr>
                                                                            <td><span>Contribuições Período  Ativo</span><asp:TextBox ID="TextBox48" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>PSAP</span><asp:TextBox ID="TextBox49" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>BSPS</span><asp:TextBox ID="TextBox50" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>BD / BPD</span><asp:TextBox ID="TextBox51" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>CV</span><asp:TextBox ID="TextBox52" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Contribuições Período  Assistido</span><asp:TextBox ID="TextBox53" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>PSAP</span><asp:TextBox ID="TextBox54" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>BSPS</span><asp:TextBox ID="TextBox55" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>BD / BPD</span><asp:TextBox ID="TextBox56" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>CV</span><asp:TextBox ID="TextBox57" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Reserva matemática - PSAP</span><asp:TextBox ID="TextBox58" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Reserva matemática - BSPS</span><asp:TextBox ID="TextBox59" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span>Reserva matemática - BD / BPD</span><asp:TextBox ID="TextBox60" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span><b>Total de Débitos</b></span><asp:TextBox ID="TextBox61" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajax:TabPanel>
                                                            <ajax:TabPanel ID="TabPanel3" HeaderText="Totais" runat="server">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td><span><b>Valor total Líquido</b></span><asp:TextBox ID="TextBox95" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span><b>Valor total Líquido - PSAP</b></span><asp:TextBox ID="TextBox96" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span><b>Valor total Líquido - BSPS</b></span><asp:TextBox ID="TextBox97" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span><b>Valor total Líquido - BD / BPD</b></span><asp:TextBox ID="TextBox98" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span><b>Valor total Líquido - CV</b></span><asp:TextBox ID="TextBox99" runat="server" Width="150px" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajax:TabPanel>
                                                            <ajax:TabPanel ID="TabPanel4" HeaderText="Dados Envio" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="divForm">
                                                                        <h4>Comunicado às Gerências</h4>
                                                                        <span>Envio do email:</span>
                                                                        <asp:TextBox ID="TextBox20" runat="server" Width="100px" MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                                                                        <input type="radio" name="radIndType" title="Pagamento Previdenciários" value="0" style="margin: 3px 0 0 21px;" /><span>Pagamento Previdenciários</span>
                                                                        <input type="radio" name="radIndType" title="Processamento e Controle Atuarial" value="1" /><span>Processamento e Controle Atuarial</span>
                                                                        <input type="radio" name="radIndType" title="Capitalização" value="2" /><span>Capitalização</span><br />
                                                                        <br />
                                                                        <h4 style="margin-top: 13px;">Comunicado à CESP</h4>
                                                                        <span>Envio do email:</span>
                                                                        <asp:TextBox ID="TextBox22" runat="server" Width="100px" MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Outro Patrocinador" Style="margin: 3px 7px 0 21px" /><br />
                                                                        <br />
                                                                        <h4 style="margin-top: 13px;">Revisão Judicial</h4>
                                                                        <span>DIP:</span>
                                                                        <asp:TextBox ID="TextBox23" runat="server" Width="100px" MaxLength="10" onkeypress="mascara(this, soNumeros)" />
                                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Sem Revisão" Style="margin: 3px 7px 0 21px" /><br />
                                                                        <br />
                                                                        <hr />
                                                                        <br />
                                                                        <span>Observações:</span>
                                                                        <asp:TextBox ID="TextBox7" runat="server" Width="570px" onkeypress="mascara(this, soNumeros)" TextMode="MultiLine" /><br />
                                                                        <br />
                                                                    </div>
                                                                </ContentTemplate>
                                                            </ajax:TabPanel>
                                                        </ajax:TabContainer>
                                                        <div class="actions">
                                                            <asp:Button ID="Button2" runat="server" CssClass="button" Text="Salvar" />
                                                            <asp:Button ID="Button1" runat="server" CssClass="button" Text="Gerar Excel" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">Descrição Ação:</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtDescricaoAcao" runat="server" TextMode="MultiLine" Width="98%" Height="300px"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="6">Observação:</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Width="70%" Height="70px" />
                                                    <asp:CheckBox ID="chkRevisao" runat="server" />
                                                    Revisão Cancelada
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr class="cab_td">
                                                <td>Responsável:</td>
                                                <td>Solicitação SRC:</td>
                                                <td>Data Resposta:</td>
                                                <td>Tipo de Documento</td>
                                                <td>Local Arquivo:</td>
                                            </tr>
                                            <tr class="cab_td">
                                                <td>
                                                    <asp:DropDownList ID="ddlResponsavel" runat="server">
                                                        <asp:ListItem Text="---Selecione---" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="André Simões" Value="ANDRÉ SIMÕES"></asp:ListItem>
                                                        <asp:ListItem Text="Guilherme Massao" Value="GUILHERME MASSAO"></asp:ListItem>
                                                        <asp:ListItem Text="Renata Anjos" Value="RENATA ANJOS"></asp:ListItem>
                                                        <asp:ListItem Text="Rodrigo Feitosa" Value="RODRIGO FEITOSA"></asp:ListItem>
                                                        <asp:ListItem Text="Rosemary Cabral" Value="ROSEMARY CABRAL"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtDtSolicSRC" runat="server" Width="100px" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                                    <asp:CheckBox ID="chkRecebTemplate" runat="server" />
                                                    Receb.Template
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDtResposta" runat="server" Width="100px" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                                                        <asp:ListItem Text="---Selecione---" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="E-mail" Value="EMAIL"></asp:ListItem>
                                                        <asp:ListItem Text="Carta" Value="CARTA"></asp:ListItem>
                                                        <asp:ListItem Text="RunRun.it" Value="RUNRUNIT"></asp:ListItem>
                                                        <asp:ListItem Text="Outros" Value="OUTROS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLocalArquivo" runat="server" onkeypress="mascara(this, soNumeros)" Width="100px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnIncluir" runat="server" CssClass="button" Text="Salvar" OnClick="btnSalvar_Click" />
                                                <asp:Button ID="btnLimparInc" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                                <asp:Button ID="btnDuplicar" runat="server" CssClass="button" Text="Duplicar" OnClick="btnDuplicar_Click" />
                                                <asp:Button ID="btnVoltar" runat="server" CssClass="button" Text="Voltar" OnClick="btnVoltar_Click" />
                                                <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Capa" OnClick="btnCapa_Click" />
                                                <asp:Button ID="btnRelCompSal" runat="server" CssClass="button" Text="Composição Salarial" OnClick="btnRelCompSal_Click" />
                                                <asp:Button ID="btnDeposito" runat="server" CssClass="button" Text="Depósito" OnClick="btnDeposito_Click" />
                                                <asp:Button ID="btnExcluir" runat="server" CssClass="button" Text="Excluir" OnClick="btnExcluir_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <ajax:ModalPopupExtender
                                    ID="ModalPopupExtender"
                                    runat="server"
                                    DropShadow="True"
                                    PopupControlID="pnlPopUpDep"
                                    TargetControlID="btnDeposito"
                                    BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="pnlPopUpDep" runat="server" Style="display: none; background-color: white; border: 1px solid black">

                                    <h3>Inserir valor do parâmetro:</h3>
                                    <table>
                                        <tr>
                                            <td>Data do pagamento:
                                                 <asp:TextBox ID="txtDtPag" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="150px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnOk" runat="server" CssClass="button" Text="OK" OnClick="btnOk_Click" />
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>

                                <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="False" />

                            </div>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>
                <ajax:TabPanel ID="TbRelatorio" HeaderText="Relatório Gerencial" runat="server" TabIndex="1">
                    <ContentTemplate>
                        <div class="full_w">
                            <div class="tabelaPagina">
                                <h1>Relatório Gerencial - Ações Judiciais</h1>
                                <div id="div1" runat="server" class="tabelaPagina">
                                    <table>
                                       <%--<tr>
                                            <td>Empresa:
                                            <asp:TextBox ID="txtEmpresa_rpt" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Matrícula:
                                            <asp:TextBox ID="txtMatricula_rpt" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="btnGerencial" runat="server" CssClass="button" Text="Imprimir" OnClick="btnGerencial_Click" />
                                                <asp:Button ID="btnGerencial_reset" runat="server" CssClass="button" Text="Limpar" OnClick="btnGerencialReset_Click" />
                                            </td>
                                        </tr>--%>

                                        <tr>
                                            <td>
                                                <div class="divForm">
                                                    <asp:RadioButtonList ID="relAcoesJud" AutoPostBack="True" runat="server" OnSelectedIndexChanged="relAcoesJud_SelectedIndexChanged">
                                                        <asp:ListItem Text="Relatório Geral" Value="Geral" />
                                                        <asp:ListItem Text="Relatório Estatistico" Value="Estatistico" />
                                                        <asp:ListItem Text="Relatório de Resposta Pendentes" Value="Pendentes" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </td>
                                        </tr>

                                        <!--Relatorio Geral-->
                                       <tr runat="server" id="trRelGeral" visible="False">
                                            <td id="Td1" colspan="3" runat="server">
                                                <table runat="server" id="tbRelGeral">

                                                    <tr id="Tr1" runat="server">
                                                        <td id="plano" runat="server">Plano</td>
                                                        <td id="Td2" runat="server">
                                                            <asp:DropDownList ID="ddlPlanoRelGeral" runat="server" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td id="fatoGerador" runat="server">Fato Gerador</td>
                                                        <td id="Td3" runat="server">
                                                            <asp:DropDownList ID="ddlFatorGeradorRelGeral" runat="server" />
                                                        </td>
                                                    </tr>

                                                    <tr id="Tr3" runat="server">
                                                        <td id="tipoAndamento" runat="server">Tipo Andamento</td>
                                                        <td id="Td4" runat="server">
                                                             <asp:DropDownList ID="ddlTipoAndamentoRelGeral" runat="server" ClientIDMode="Static" onchange="ddlTipoAndamento_onchange();">
                                                                <asp:ListItem Text="---Selecione---" Selected="True" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Assistente Técnico"></asp:ListItem>
                                                                <asp:ListItem Text="Concessão"></asp:ListItem>
                                                                <asp:ListItem Text="Cópia do Processo"></asp:ListItem>
                                                                <asp:ListItem Text="CRM"></asp:ListItem>
                                                                <asp:ListItem Text="Demonstrativo de calculo"></asp:ListItem>
                                                                <asp:ListItem Text="Depósito Judicial – PP"></asp:ListItem>
                                                                <asp:ListItem Text="Depósito Judicial – PR"></asp:ListItem>
                                                                <asp:ListItem Text="Elaboração de Calculo "></asp:ListItem>
                                                                <asp:ListItem Text="Formula de Cálculo (BSPS/BD)"></asp:ListItem>
                                                                <asp:ListItem Text="Impugnação de calculo "></asp:ListItem>
                                                                <asp:ListItem Text="Inicial"></asp:ListItem>
                                                                <asp:ListItem Text="Oficial"></asp:ListItem>
                                                                <asp:ListItem Text="Patrocinador"></asp:ListItem>
                                                                <asp:ListItem Text="Perito"></asp:ListItem>
                                                                <asp:ListItem Text="Revisão"></asp:ListItem>
                                                                <asp:ListItem Text="Esclarecimento Regulamentar"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>                                                      
                                                    </tr>

                                                     <tr>
                                                        <td id="responsavel" runat="server">Responsável</td>
                                                         <td id="Td5" runat="server">
                                                             <asp:DropDownList ID="ddlResponsavelRelGeral" runat="server">
                                                                <asp:ListItem Text="---Selecione---" Selected="True" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="André Simões" Value="ANDRÉ SIMÕES"></asp:ListItem>
                                                                <asp:ListItem Text="Guilherme Massao" Value="GUILHERME MASSAO"></asp:ListItem>
                                                                <asp:ListItem Text="Renata Anjos" Value="RENATA ANJOS"></asp:ListItem>
                                                                <asp:ListItem Text="Rodrigo Feitosa" Value="RODRIGO FEITOSA"></asp:ListItem>
                                                                <asp:ListItem Text="Rosemary Cabral" Value="ROSEMARY CABRAL"></asp:ListItem>
                                                            </asp:DropDownList>
                                                         </td>
                                                    </tr>

                                                    <tr>
                                                        <td>Local Arquivo</td>
                                                        <td id="Td7" runat="server"><asp:TextBox ID="txtLocalArquivoGeral" runat="server" CssClass="text" onkeypress="mascara(this, soNumeros)" Width="100px" /></td>
                                                    </tr>

                                                    <tr id="Tr5" runat="server">
                                                        <td id="dataResposta" runat="server">Data Resposta</td>
                                                        <td id="Td6" runat="server"><asp:TextBox ID="txtdataResposta" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" /></td>
                                                    </tr>

                                                    <tr id="Tr7" runat="server">
                                                        <td id="dataPrazo" runat="server">Data Prazo</td>
                                                        <td id="Td8"><asp:TextBox  ID="txtdataPrazo" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" /></td>
                                                    </tr>

                                                    <tr id="Tr23" runat="server">                                                          
                                                        <td id="Td24" runat="server"><asp:Button ID="btnGerarRelatorio" runat="server" Text="Gerar Relatorio" CssClass="button" OnClick="btnGerarRelatorioGeral_Click" /></td>
                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>
                                        <!--Fim Relatorio Geral-->

                                        <!--Relatorio Estatistico -->
                                        <tr runat="server" id="trRelEstatistico" visible="False">
                                            <td id="Td25" colspan="3" runat="server">
                                                <table runat="server" id="tbRelEstatistico">
                                                    <tr id="Tr24" runat="server">
                                                        <td>Data Inicio *</td>
                                                        <td><asp:TextBox  ID="dataIni" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Data Fim *</td>
                                                        <td><asp:TextBox  ID="dataFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" /></td>
                                                    </tr>
                                                    
                                                    <tr id="Tr26" runat="server">                                                          
                                                        <td id="Td30" runat="server"><asp:Button ID="btnGerarRelatorioEstatistico" runat="server" Text="Gerar Relatorio" CssClass="button" OnClick="btnGerarRelatorioEstatistico_Click" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!--Fim Relatorio Estatistico-->

                                        <!--Relatorio Pendentes-->
                                        <tr runat="server" id="trRelPendente" visible="False">
                                            <td id="Td31" colspan="3" runat="server">
                                                <table runat="server" id="tbRespPendente">
                                                    <tr id="Tr27" runat="server">
                                                        <td id="Td32" runat="server"><b>Relatório de Respostas Pendentes</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td id="Td33" runat="server"><asp:Button ID="btnGerarRelatorioPendentes" runat="server" CssClass="button" Text="Gerar Relatorio" OnClick="btnGerarRelatorioPendentes_Click" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!--Fim Relatorio Pendentes -->
                                        
                                    </table>

                                </div>


                            </div>
                        </div>

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
    <script type="text/javascript">
        function ddlTipoAndamento_onchange() {
            if ($("#ddlTipoAndamento").val().indexOf("Judicial") > 0) { $("#lblMedicao, #txtMedicao, #box_checklist").fadeIn(); $("#box_trg").css("display", "inline-block") }
            else { $("#lblMedicao, #txtMedicao, #box_trg, #box_checklist").fadeOut(); }
            return false;
        }
        function boxchecklist_trg() {
            var nextState = parseInt($("#box_checklist").css("height")) > 0 ? "close" : "open";
            $("#box_checklist").css("animation", "boxchecklist_trg_" + nextState + " 0.5s linear 0s 1 normal forwards");
            return false;
        }
        function pageLoad() { setTimeout(ddlTipoAndamento_onchange, 500); }


    </script>
    <style>
        @keyframes boxchecklist_trg_open {
            0% {
                height: 0px;
            }

            100% {
                height: 430px;
            }
        }

        @keyframes boxchecklist_trg_close {
            0% {
                height: 430px;
            }

            100% {
                height: 0px;
            }
        }

        #box_checklist .actions input[type="submit"] {
            float: right;
            margin: 7px;
        }

        #box_checklist .actions,
        #box_checklist table {
            width: 753px;
            margin: 10px 0 0 15px;
            height: 345px;
            border: 0px;
        }

        #box_checklist .actions {
            margin: -9px 0 0 25px;
        }

        #box_checklist table tr td span {
            position: relative;
            display: inline-block;
            float: left;
            width: 297px;
            font-size: 11px;
        }

        #box_checklist table tr td {
            background-color: initial!important;
            -webkit-background-color: initial!important;
        }

        #box_checklist .ajax__tab_body {
            border: 0;
        }

        .divForm span {
            display: inline-block;
            width: 126px;
            float: left;
            font-size: 11px;
            margin-left: 7px;
        }

        .divForm {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
            padding: 16px 16px 34px 16px;
        }

            .divForm h4 {
                background: #f3f3f3;
                padding: 7px;
                font-size: 11px;
                font-family: Tahoma, Arial, Helvetica, sans-serif;
                margin: 0 0 13px;
            }

            .divForm input[type="text"], .divForm select {
                margin: 0 34px 7px 0;
                float: left;
            }

            .divForm input[type="radio"] {
                float: left;
            }

            .divForm label {
                margin-left: 7px;
            }

            .divForm hr {
                background: #f3f3f3;
                margin: 9px;
                opacity: .3;
            }
    </style>
</asp:Content>


