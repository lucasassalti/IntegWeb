<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="QtdAtendimentos.aspx.cs" Inherits="IntegWeb.Saude.Web.QtdAtendimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upOrgao">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsOrgao" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Contador de Seriados</h1>
                    <table>
                        <tr>
                            <td>Procedimento</td>
                            <td>
                                <asp:DropDownList ID="ddlProcedimento" runat="server" Style="width: 130px;" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="<TODOS>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="FISIOTERAPIA"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="FONOAUDIOLOGIA"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="NUTRICIONISTA"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="ODONTOLOGIA"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="PSICOLOGIA"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="RPG"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="TERAPIA OCUPACIONAL"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="ACUPUNTURA"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>


                        <tr>
                            <td>Tipo de Pesquisa</td>
                            <td>
                                <asp:DropDownList ID="ddlTpPesquisa" runat="server" Style="width: 130px;" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="Pesquisa Simples"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Pesquisa Detalhada"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>Matrícula</td>
                            <td>
                                <asp:TextBox ID="txtNumMatricula" runat="server" Style="width: 100px;"></asp:TextBox>
                            </td>
                            <td>Sub-Matrícula</td>
                            <td>
                                <asp:TextBox ID="txtNumSubMatricula" runat="server" Style="width: 100px;"></asp:TextBox>
                                <asp:TextBox ID="txtCodEmpresa" runat="server" Style="width: 100px; display: none;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Período de emissão</td>
                            <td>
                                <asp:TextBox ID="txtDtIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDtIni" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                            </td>
                            <td>até&nbsp&nbsp&nbsp
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server" ControlToValidate="txtDtFim" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Consultar" />
                            </td>
                            <td>
                                <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="button" CausesValidation="false" runat="server" Text="Limpar" />
                            </td>
                        </tr>
                    </table>

                    <asp:ObjectDataSource runat="server" ID="odsAtendimento"
                        TypeName="IntegWeb.Saude.Aplicacao.BLL.AtendimentosBLL"
                        SelectMethod="GetQtdAtendimentos"
                        SelectCountMethod="SelectCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtCodEmpresa" Name="paramCodEmpresa" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtNumMatricula" Name="paramNumMatricula" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtNumSubMatricula" Name="paramNumSubMatricula" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtDtIni" Name="paramDtIni" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtDtFim" Name="paramDtFim" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlProcedimento" Name="paramNumProcedimento" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlTpPesquisa" Name="paramTipoPesquisa" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdAtendimento" runat="server"
                        OnRowCancelingEdit="grdAtendimento_RowCancelingEdit"
                        OnRowEditing="grdAtendimento_RowEditing"
                        OnRowCreated="GridView_RowCreated"
                        AllowPaging="true"
                        AllowSorting="true"
                        AutoGenerateColumns="False"
                        EmptyDataText="A consulta não retornou registros"
                        CssClass="Table"
                        ClientIDMode="Static"
                        PageSize="8"
                        DataSourceID="odsAtendimento">
                        <PagerSettings
                            Visible="true"
                            PreviousPageText="Anterior"
                            NextPageText="Próxima"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:BoundField DataField="num_matricula" HeaderText="Matrícula" SortExpression="num_matricula" ReadOnly="true" />
                            <asp:BoundField DataField="num_sub_matricula" HeaderText="Sub-Matrícula" SortExpression="num_sub_matricula" ReadOnly="true" />
                            <asp:BoundField DataField="cod_empresa" HeaderText="Empresa" SortExpression="cod_empresa" ReadOnly="true" />
                            <asp:BoundField DataField="nome_participante" HeaderText="Participante" SortExpression="nome_participante" ReadOnly="true" />
                            <asp:BoundField DataField="procedimento" HeaderText="Procedimento" SortExpression="procedimento" ReadOnly="true" />
                            <asp:BoundField DataField="qtd_recurso" HeaderText="Qtd. Recurso" SortExpression="qtd_recurso" ReadOnly="true" />

                            <asp:BoundField DataField="anoFatura" HeaderText="Ano Fatura" SortExpression="anoFatura" ReadOnly="true" />
                            <asp:BoundField DataField="numSeqFatura" HeaderText="N. Seq. Fatura" SortExpression="numSeqFatura" ReadOnly="true" />

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div id="carregando">
                <div class="carregandoTxt">
                    <img src="img/processando.gif" />
                    <br />
                    <br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
