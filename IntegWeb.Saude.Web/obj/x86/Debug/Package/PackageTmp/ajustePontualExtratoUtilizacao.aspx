<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ajustePontualExtratoUtilizacao.aspx.cs" Inherits="IntegWeb.Saude.Web.ajustePontualExtratoUtilizacao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>



<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
        $(document).ready(function () {
            var id;
            id = $("").val();
        })
    </script>


    <style type="text/css">
        .gridviewAjustePontualExtrato {
            width: 100px;
            height: 100px;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-top: -50px;
            margin-left: -50px;
        }

        .btnGerarGrid {
            margin-left: 15%;
            margin-bottom: 1%;
        }

        .btnAtualizar {
            background-color: #e4e4e4;
            font-family: Arial,Tahoma,Verdana;
            color: black;
            font-size: 13px;
            /*border-radius: 4px;*/
        }

        #gridviewAjustePontualExtrato {
            text-align: center;
            font-size: 10pt;
        }
    </style>


    <div id="area_parametro">
        <div class="h_title"></div>
        <div class="full_w">
            <div class="tabelaPagina">
                <h2>Acerto Pontual de Coparticipação</h2>
                <br />
                <table>
                    <tbody>
                        <tr>
                            <td colspan="2"></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblAnoFichaCaixa" ForeColor="Black" runat="server" Text="Ano Ficha Caixa"></asp:Label>
                                <asp:TextBox ID="txtAnoFichaCaixa" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblMesFichaCaixa" ForeColor="Black" runat="server" Text="Mês Ficha Caixa"></asp:Label>
                                <asp:TextBox ID="txtMesFichaCaixa" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblCodEmpresa" ForeColor="Black" runat="server" Text="Código Empresa"></asp:Label>
                                <asp:TextBox ID="txtCodEmpresa" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblNumMatricula" ForeColor="Black" runat="server" Text="Num. Matrícula  &nbsp;"></asp:Label>
                                <asp:TextBox ID="txtNumMatricula" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
            </div>
           <asp:Button ID="btnGerarGrid" runat="server" CssClass="btnGerarGrid align-right" Text="Buscar Informações" OnClick="btnGerarGrid_Click" />
        </div>

        <div class="full_w">
            <div id="gridviewAjustePontualExtrato">
                <asp:GridView ID="GridView1"
                    runat="server"
                    AllowPaging="True"
                    AutoGenerateColumns="False"
                    ForeColor="Black"
                    OnRowEditing="GridView1_RowEditing"
                    OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowUpdating="GridView1_RowUpdating"
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    DataKeyNames="VAL_P_PARTICIP, IDC_Internacao, VAL_PARTICIP">
                    <Columns>
                        <asp:BoundField DataField="Cod_Convenente" HeaderText="Código" ReadOnly="true" />
                        <asp:BoundField DataField="nom_convenente" HeaderText="Nome Conv." ReadOnly="true" />
                        <asp:BoundField DataField="Cod_Emprs" HeaderText="Empresa" ReadOnly="true" />
                        <asp:BoundField DataField="Num_Matricula" HeaderText="Matrícula" ReadOnly="true" />
                        <asp:BoundField DataField="Num_Sub_Matric" HeaderText="Sub. Matrícula" ReadOnly="true" />
                        <asp:BoundField DataField="Nom_Particip" HeaderText="Nome Partic" ReadOnly="true" />
                        <%--<asp:BoundField DataField="IDC_Internacao" HeaderText="Idc. Intern." ReadOnly="true" />--%>

                        <asp:TemplateField HeaderText="Idc. Intern.">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblIDC_Internacao" Text='<%# Eval("IDC_Internacao")%>'></asp:Label>
                            </ItemTemplate>

                            <%--<EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtIDC_Internacao" Text='<%# Eval("IDC_Internacao")%>'></asp:TextBox>                                
                            </EditItemTemplate>--%>

                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlIDC_Internacao" runat="server">
                                    <asp:ListItem Value="S">S</asp:ListItem>
                                    <asp:ListItem Value="N">N</asp:ListItem>                                    
                                </asp:DropDownList>
                            </EditItemTemplate>

                        </asp:TemplateField>

                        <asp:BoundField DataField="Ano_Receb_Particip" HeaderText="Ano Receb. Partic" ReadOnly="true" />
                        <asp:BoundField DataField="num_seq_receb_partic" HeaderText="Seq. Receb. Partic." ReadOnly="true" />
                        <asp:BoundField DataField="ANO_FATURA" HeaderText="Ano Fatura" ReadOnly="true" />
                        <asp:BoundField DataField="NUM_SEQ_FATURA" HeaderText="Seq. Fatura" ReadOnly="true" />
                        <asp:BoundField DataField="NUM_SEQ_ATEND" HeaderText="Seq. Atend" ReadOnly="true" />
                        <asp:BoundField DataField="NUM_SEQ_ITEM" HeaderText="Seq. Item" ReadOnly="true" />
                        <asp:BoundField DataField="COD_RECURSO" HeaderText="Cod. Recur." ReadOnly="true" />
                        <asp:BoundField DataField="DES_RECURSO" HeaderText="Desc. Recur." ReadOnly="true" />
                        <asp:BoundField DataField="DAT_REALIZ" HeaderText="Dat. Realiz" ReadOnly="true" />
                        <asp:BoundField DataField="HOR_REALIZ" HeaderText="Hora Realiz" ReadOnly="true" />

                        <%--<asp:BoundField DataField="VAL_P_PARTICIP" HeaderText="Val.% Particip" />--%>

                        <asp:TemplateField HeaderText="Val.% Particip">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblValpParticip" Text='<%# Eval("VAL_P_PARTICIP")%>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtValpParticip" Text='<%# Eval("VAL_P_PARTICIP")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="VAL_COBRADO" HeaderText="Val. Cobrado" ReadOnly="true" />
                        <asp:BoundField DataField="VAL_CALCULADO" HeaderText="Val. Calc" ReadOnly="true" />
                        <asp:BoundField DataField="VAL_PAGAR" HeaderText="Val. Pago" ReadOnly="true" />
                        <asp:BoundField DataField="COD_PLANO_PAGTO" HeaderText="Plano Pagto" ReadOnly="true" />
                        <asp:BoundField DataField="FAT_ACOMODACAO" HeaderText="Fat. Acomod." ReadOnly="true" />
                        <asp:BoundField DataField="RCOCODPROCEDIMENTO" HeaderText="Cod. Proced." ReadOnly="true" />

                        <%-- <asp:BoundField DataField="VAL_PARTICIP" HeaderText="Val. Partic." ReadOnly="true" />--%>

                        <asp:TemplateField HeaderText="Val. Partic.">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblValParticip" Text='<%# Eval("VAL_PARTICIP")%>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtValParticip" Text='<%# Eval("VAL_PARTICIP")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField CancelText="Cancelar" DeleteText="Deletar" CausesValidation="true" EditText="Editar" ShowEditButton="True" UpdateText="Atualizar" HeaderText="Ação" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <asp:UpdatePanel runat="server" ID="upCisao"></asp:UpdatePanel>

    </div>



</asp:Content>




