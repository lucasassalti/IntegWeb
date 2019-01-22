<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AlteracaoTempoServico.aspx.cs" Inherits="IntegWeb.Previdencia.Web.AlteracaoTempoServico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upUpdatePanel">
        <ContentTemplate>

            <div class="full_w">
                <div class="tabelaPagina">
                    <h1>Alteração de Tempo de Serviço</h1>
                    <div id="divPesquisa" runat="server" class="tabelaPagina divPesquisa">
                        <table>
                            <tr>
                                <td><span>Empresa:</span>
                                    <asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td><span>Matrícula:</span>
                                    <asp:TextBox ID="txtPesqMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" />
                                    <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar"  />
                                    <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" ClientIDMode="Static" OnClick="btnNovo_Click"  />
                                </td>
                            </tr>
                        </table>

                        <asp:GridView ID="grdPesquisa" runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="Não Retornou Registros"
                            AllowPaging="True"
                            AllowSorting="True"
                            CssClass="Table"
                            PageSize="10"
                            ClientIDMode="Static">
                            <Columns>
                                <asp:TemplateField HeaderText="Sequencial" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSequencial" runat="server" Text='' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Matrícula" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatrícula" runat="server" Text='' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nome" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNome" runat="server" Text='' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data Atualização" SortExpression="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDtUpd" runat="server" Text='' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a id="lnkEditar" class="button" href="#">Editar</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a id="lnkExcluir" class="button" href="#">Excluir</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="divForm" runat="server" class="tabelaPagina divForm" visible="false">
                        <h4>Identificação do Participante</h4>
                        <span>Empresa:</span>
                        <asp:TextBox ID="txtEmpresa" runat="server" Width="100px" MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                        <span style="margin-left: 0;">Matrícula:</span>
                        <asp:TextBox ID="txtMatricula" runat="server" Width="100px" MaxLength="10" onkeypress="mascara(this, soNumeros)" />
                        <br/><br/>
                        <span>Nome:</span>
                        <asp:TextBox ID="TextBox2" runat="server" Width="328px" /><br/><br/>
                        <span>Admissão:</span><asp:Label ID="lblAdmissao" runat="server" Width="100px" />
                        <asp:TextBox ID="TextBox3" runat="server" Width="100px" MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                        <span style="margin-left: 0;">Demissão:</span><asp:Label ID="lblDemissao" runat="server" Width="100px" />
                        <asp:TextBox ID="TextBox4" runat="server" Width="100px" MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                        <br/><br/>
                        <h4>Dados do Benefício</h4>
                        <span>Plano:</span><asp:DropDownList ID="ddlPlano" runat="server" /><br/><br/>
                        <span>Benefício:</span><asp:DropDownList ID="ddlBeneficio" runat="server" /><br/><br/>
                        <span>Data DIB:</span><asp:TextBox ID="txtDib" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                        <span style="margin-left: 0;">Data Base:</span><asp:TextBox ID="TextBox1" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                        <br/><br/>
                        <h4>Tempo de Serviço</h4>
                        <div class="mylist">
                            <asp:Button ID="btnAdicionar" runat="server" CssClass="button" Text="Adicionar Indicador" />
                            <span>Indicador:</span><asp:DropDownList ID="DropDownList1" runat="server" /><br/><br/>
                            <input type="radio" name="radIndType" title="Período" value="0" checked="checked" style="margin-left: 100px;" onclick="javascript: radswitch(0);" /><span>Período</span>
                            <input type="radio" name="radIndType" title="Tempo" value="1" onclick="javascript: radswitch(1);" /><span>Tempo</span><br/><br/>
                            <span id="lblPeriodoIni">Início:</span><asp:TextBox ID="txtInicio" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" ClientIDMode="Static" />
                            <span id="lblPeriodoFim" style="margin-left: 0;">Fim:</span><asp:TextBox ID="txtFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" ClientIDMode="Static" /><br/><br/>
                            <div class="divTime">
                                <span>Anos:</span><asp:TextBox ID="txtAnos" runat="server" disabled="gray" MaxLength="10" onkeypress="mascara(this, data)" Width="50px" ClientIDMode="Static" />
                                <span style="margin-left: 0;width:50px">Meses:</span><asp:TextBox ID="txtMeses" runat="server" disabled="gray" MaxLength="10" onkeypress="mascara(this, data)" Width="50px" ClientIDMode="Static" />
                                <span style="margin-left: 0;width:50px">Dias:</span><asp:TextBox ID="txtDias" runat="server" disabled="gray" MaxLength="10" onkeypress="mascara(this, data)" Width="50px" ClientIDMode="Static" />
                            </div>
                            <br/><br/>

                            <asp:GridView ID="grdIndicadores" runat="server" AutoGenerateColumns="false" 
                                EmptyDataText="Não Retornou Registros"
                                AllowPaging="True"
                                AllowSorting="True"
                                CssClass="Table"
                                PageSize="10"
                                ClientIDMode="Static" >
                                <Columns>        
                                    <asp:BoundField DataField="Indicador" HeaderText="Indicador" />
                                    <asp:BoundField DataField="Periodo" HeaderText="Período" />
                                    <asp:BoundField DataField="Anos" HeaderText="Anos" />
                                    <asp:BoundField DataField="Meses" HeaderText="Meses" />
                                    <asp:BoundField DataField="Dias" HeaderText="Dias" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEditar" runat="server" Text="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br/><br/>
                        <div class="actions">
                            <asp:Button ID="btnSalvar" runat="server" CssClass="button" Text="Salvar" />
                            <asp:Button ID="btnVoltar" runat="server" CssClass="button" Text="Voltar" OnClick="btnVoltar_Click" />
                        </div>
                        
                    </div>
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
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script>
        function radswitch(param) {
            if (param == 0) {
                $("#txtInicio, #txtFim, #lblPeriodoIni, #lblPeriodoFim").fadeIn();
                $(".divTime").removeClass("moveUp").addClass("moveDown");
                $("#txtAnos, #txtMeses, #txtDias").attr("disabled", "gray");
            }
            else {
                $("#txtInicio, #txtFim, #lblPeriodoIni, #lblPeriodoFim").fadeOut();
                $(".divTime").removeClass("moveDown").addClass("moveUp");
                $("#txtAnos, #txtMeses, #txtDias").removeAttr("disabled");
            }
        }
        function buildDate(str) {
            var partes = str.split("/");
            return new Date(partes[2], partes[1] - 1, partes[0]);
        }
        function buildTime() {
            var date1 = buildDate($("#txtFim").val());
            var date2 = buildDate($("#txtInicio").val());
                
            var days = ((date1 - date2) / (60 * 60 * 24 * 1000));
            var months = Math.floor(days / 30);
            var years = Math.floor(months / 12);
            months = months - (years * 12);
            days = years <= 0 ? (days - (months * 30)) : (days - (years * 365));

            $("#txtAnos").val(parseInt(years));
            $("#txtMeses").val(parseInt(months));
            $("#txtDias").val(parseInt(days));
            return false;
        }
        function verifyTime() {
             var inicio = $("#txtInicio").val();
             var fim = $("#txtFim").val();
             if (inicio.length != 10 || fim.length != 10) return;

             if (buildDate(fim) <= buildDate(inicio)) { alert("A data inicial é maior que a data final"); $("#txtInicio, #txtFim").val(''); }
             else { buildTime(); }
        }
        function page_init() {
            $("#txtInicio, #txtFim").each(function () {
                $(this).on('change', function (e) {
                    if ($("#txtInicio").val().length == 10 && $("#txtFim").val().length == 10) { verifyTime(); }
                });
            });
        }
        window.addEventListener('DOMContentLoaded', function () {
            setTimeout(page_init,2000);
        }, false);
    </script>
<style>
    #grdIndicadores input[type="submit"] {
        background: #F3F3F3;
        border: 1px solid #DCDCDC;
        border-radius: 2px;
        /* color: #444444; */
        cursor: pointer;
        display: inline-block;
        font: 700 11px Tahoma, Arial, sans-serif;
        margin-right: 10px;
        padding: 7px 12px 7px 12px;
        position: relative;
        text-decoration: none;
        text-shadow: 0px 1px 0px #FFFFFF;
    }
    #grdIndicadores input[type="submit"]:hover{
        border-bottom-color: #999999;
        border-left-color: #999999;
        border-right-color: #999999;
        border-top-color: #999999;
        color: #333333;
        text-decoration: none;
    }
    .tabelaPagina > .tabelaPagina table tbody tr td > span, 
    .divForm span 
    { display: inline-block; width: 90px; float: left; font-size: 10pt; margin-left: 7px; }
    .divForm {
        font-family: verdana,tahoma,helvetica;
        font-size: 10pt;
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
    .divForm select {
        width: 331px;
    }
    .divForm .actions {
        padding: 9px; background: #eee;
    }
        .mylist input[type="text"], .mylist select {
            margin: 0 34px 7px 0;
        }
    .mylist #grdIndicadores {
        margin-left: 0;
    }
    .divForm .divTime {
        position: relative;
        top: 0;
    }
    @keyframes timeUp {
        from {
            top: 0;
        }
        to {
            top: -32px;
        }
    }
    @keyframes timeDown {
        from {
            top: -32px;
        }
        to {
            top: 0;
        }
    }
    .moveUp {
        animation: timeUp 0.3s linear 0s 1 normal forwards;
    }
    .moveDown {
        animation: timeDown 0.3s linear 0s 1 normal forwards;
    }
</style>
    
</asp:Content>


