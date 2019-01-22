<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CadastroEmpregado.aspx.cs" Inherits="IntegWeb.Intranet.Web.CadastroEmpregado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="tabelaPagina">

            <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="1">
                <ajax:TabPanel ID="tbEmpregado" HeaderText="Empregado" runat="server" TabIndex="0">
                    <ContentTemplate>
                        <h4>Dados Cadastrais</h4>
                        <table>
                            <tr>
                                <td>Nome:</td>
                                <td>
                                    <asp:Label ID="lblNome" runat="server" Width="311px"></asp:Label></td>
                                <td>Sexo: 
                                </td>
                                <td>
                                    <asp:Label ID="lblSexo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Dt. de Nasc.:</td>
                                <td>
                                    <asp:Label ID="lblDataNascimento" runat="server" Width="311px"></asp:Label>
                                </td>

                                <td>Dt. de Falec.:</td>
                                <td>
                                    <asp:Label ID="lblDataFalecimento" runat="server" Style="width: 50px;" Width="184px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Nacionalidade:</td>
                                <td>
                                    <asp:Label ID="lblNacionalidade" runat="server" Style="width: 50px;" Width="182px"></asp:Label>
                                </td>

                                <td>Natural de:</td>
                                <td>
                                    <asp:Label ID="lblNaturalDe" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <h4>Dados - Conta Corrente</h4>
                        <table>
                            <tr>
                                <td>Banco:</td>
                                <td>
                                    <asp:Label ID="lblBanco" runat="server" Style="width: 50px;" Width="182px"></asp:Label>
                                </td>

                                <td>Agência:</td>
                                <td>
                                    <asp:Label ID="lblAgencia" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Tipo Conta Corrente:</td>
                                <td>
                                    <asp:Label ID="lblTipoContaCorrente" runat="server" Style="width: 50px;" Width="182px"></asp:Label>
                                </td>

                                <td>Conta Corr:</td>
                                <td>
                                    <asp:Label ID="lblContaCorrente" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <h4>Empresa</h4>
                        <table>
                            <tr>
                                <td>Razão Social:</td>
                                <td>
                                    <asp:Label ID="lblRazaoSocial" runat="server" Style="width: 50px;" Width="182px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Dt. de Admissão:</td>
                                <td>
                                    <asp:Label ID="lblDataAdmissao" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                                <td>Dt. Desligamento:</td>
                                <td>
                                    <asp:Label ID="lblDateDesligamento" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Cargo:</td>
                                <td>
                                    <asp:Label ID="lblCargo" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                                <td>Mot. Desligamento:</td>
                                <td>
                                    <asp:Label ID="lblMotivoDesligamento" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Ocupação Profissional:</td>
                                <td>
                                    <asp:Label ID="lblOcupacaoProfissional" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <h4>Orgão de Lotação</h4>
                        <table>
                            <tr>
                                <td>Nome:</td>
                                <td>
                                    <asp:Label ID="lblNomeOrgaoLotacao" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Endereço:</td>
                                <td>
                                    <asp:Label ID="lblEnrecoOrgaoLotacao" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Bairro:</td>
                                <td>
                                    <asp:Label ID="lblBairroOrgaoLotacao" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                                <td>CEP:</td>
                                <td>
                                    <asp:Label ID="lblCepOrgaoLotacao" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Cidade/UF:</td>
                                <td>
                                    <asp:Label ID="lblCidadeUfOrgaoLotacao" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                                <td>Fone:</td>
                                <td>
                                    <asp:Label ID="lblFoneOrgaoLotacao" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajax:TabPanel>

                <ajax:TabPanel ID="TbDependente" HeaderText="Dependente" runat="server" TabIndex="0">
                    <ContentTemplate>
                        <h4>Dependentes</h4>
                        <%--<asp:ObjectDataSource
                            ID="odsResultadoDependente"
                            runat="server"></asp:ObjectDataSource>   --%>

                        <asp:GridView
                            ID="grdResultadoDependentes"
                            runat="server"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="Nome">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNome" runat="server" Text="oi" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sexo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSexo" runat="server" Text="oi2" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                        <h4>Dados Cadastrais</h4>
                        <table>
                        </table>
                        <h4>Dados - Conta Corrente</h4>
                        <table>
                            <tr>
                                <td>Banco:</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Style="width: 50px;" Width="182px"></asp:Label>
                                </td>

                                <td>Agência:</td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Tipo Conta Corrente:</td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Style="width: 50px;" Width="182px"></asp:Label>
                                </td>

                                <td>Conta Corr:</td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Style="width: 50px;" Width="301px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajax:TabPanel>


            </ajax:TabContainer>
        </div>
    </div>


</asp:Content>
