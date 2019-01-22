<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Cobertura_SAP.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CoberturaSAP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function ScrollRight() {
            window.scrollTo(200, 0);
        }

        function ScrollLeft() {
            window.scrollTo(0, 0);
        }
    </script>
    <asp:UpdatePanel runat="server" ID="upOrgao">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsOrgao" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Alteração de Cobertura SAP</h1>
                    <table>
                        <tr>
                            <td>Empresa:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Matricula:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMatricula" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Nº do Chamado:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumChamado" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Data de Abertura:
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server"
                                ControlToValidate="txtDtAbertura" ErrorMessage="Informe uma data válida (mm/dd/yyyy)"
                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                Text="*"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtAbertura" runat="server" CssClass="date"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Consultar" />
                                <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="button" CausesValidation="false" OnClientClick="ScrollRight();" runat="server" Text="Limpar" />
                            </td>
                        </tr>
                    </table>

                    <asp:ObjectDataSource runat="server" ID="odsCoberturaSAP"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao.CoberturaSAPBLL"
                        SelectMethod="GetCoberturaSAP"
                        SelectCountMethod="SelectCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtEmpresa" Name="paramEmpresa" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtMatricula" Name="paramMatricula" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtNumChamado" Name="paramChamado" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtDtAbertura" Name="paramDTAbertura" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdCoberturaSAP" runat="server" 
                        DataKeyNames="COD_EMPRS, NUM_RGTRO_EMPRG,DATA_SOLIC" 
                        OnRowCancelingEdit="grdCoberturaSAP_RowCancelingEdit" 
                        OnRowEditing="grdCoberturaSAP_RowEditing" 
                        OnRowCreated="GridView_RowCreated"
                        AllowPaging="true" 
                        AllowSorting="true"
                        AutoGenerateColumns="False" 
                        EmptyDataText="A consulta não retornou registros" 
                        CssClass="Table" 
                        ClientIDMode="Static" 
                        PageSize="8" 
                        DataSourceID="odsCoberturaSAP"
                        Visible="true">
                        <PagerSettings
                            Visible="false"
                            PreviousPageText="Anterior"
                            NextPageText="Próxima"
                            Mode="NumericFirstLast" />
                        <Columns>

                            <%--<asp:BoundField DataField="COD_EMPRS" HeaderText="Chamado"  SortExpression="COD_EMPRS" ReadOnly="true"/>--%>
                            <asp:BoundField DataField="COD_EMPRS" HeaderText="Empresa" SortExpression="COD_EMPRS" ReadOnly="true"/>
                            <asp:BoundField DataField="NUM_RGTRO_EMPRG" HeaderText="Matricula" SortExpression="NUM_RGTRO_EMPRG" ReadOnly="true"/>
                            <asp:BoundField DataField="data_solic" HeaderText="Data de Abertura" SortExpression="DATA_SOLIC" ReadOnly="true"/>
                            <asp:BoundField DataField="num_seq_seguro" HeaderText="Nº do Segurado" SortExpression="NUM_SEQ_SEGURO"  ReadOnly="true"/>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
