<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Popup.Master" CodeBehind="CadComponentes.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CadComponentes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        .custom-response-fancybox {
            display: flex !important;
            justify-content: center;
            align-items: center;
            min-width: 250px;
            min-height: 100px;
            width: auto;
            height: auto;
        }

            .custom-response-fancybox span {
                font-size: 11.5pt;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlTabelas').change(function (e) {
                hideFields();
            });

            var response = $('#hdnResponse').val();
            if (!!response) {
                var div = $('<div/>').addClass('custom-response-fancybox')
                .append($('<span/>').text(response));

                $.fancybox.open(div);
                $('#hdnResponse').val('');
            }
             
            //manipula os elementos criados em runtime (substitui o setTimeout)
            $.when($(document).ready())
            .then(function () {
                var ddl = $('#ddlFilter')[0];
                if (!!ddl) {
                    ddl.addEventListener('change', function (e) {
                        var ddl = e.target;
                        var txt = $('#txtFilter')[0];

                        txt.value = '';

                        txt.setAttribute('data-type', ddl[ddl.selectedIndex].dataset.type);
                        applyMasc(txt);
                    });
                }

                $('#btnFilterReset').click(function (e) {
                    e.preventDefault();
                    var args = [{ Key: "clearFilter" }];
                    __doPostBack(e.id, JSON.stringify(args));
                })

                //Aplica máscara nos campos
                var inputs = $('input[type="text"]');

                if (!!inputs && inputs.length > 0) {
                    inputs.each(function (idx, elem) {
                        if (!!elem.dataset.type)
                            applyMasc(elem);
                    });
                }
            });
        });

        function applyMasc(elem) {
            if (elem.dataset.type.toLowerCase() == 'decimal')
                elem.addEventListener('keyup', function (e) {
                    mascara(elem, soNumeros);
                });
        }

        function hideFields() {
            $('#trFilter').hide()
                .empty();

            $('#tblAdd').hide();

            $('#trForm').hide()
                .empty();

            $('#pnlGrid').hide();
        }

        function showAlert(message) {
            var div = $('<div/>').addClass('custom-response-fancybox')
                .append($('<span/>').text(message));
            $.fancybox.opts = {
                autoDimensions: true
            };
            $.fancybox.open(div);
        }

        function filter(elem) {
            var valid = !!$('#txtFilter').val();

            if (!valid)
                showAlert('Informe o termo da pesquisa.');

            return valid;
        }

        function createRow(elem) {
            var data = [];
            var inputs = $('#tblForm input[required]');

            inputs.each(function (i, e) {
                var obj = {};
                if (!!e.value) {
                    obj[e.dataset.key] = e.value;
                    data.push(obj);
                }
            });

            if (data.length == inputs.length) __doPostBack(elem.id, JSON.stringify(data));
            else
                showAlert('Preecha todos os campos obrigatórios.');

        }

        function rowValidate(elem) {
            var td = elem.parentElement;
            var tr = td.parentElement;
            var inputs = tr.querySelectorAll('input[required]');
            var valid = false;

            for (var i = 0; i < inputs.length; i++) {
                valid = !!inputs[i].value;
                if (!valid) break;
            }

            if (!valid) {
                showAlert('Preencha todos os campos obrigatórios.');

                inputs.forEach(function (elem) {
                    if (!elem.nextElementSibling)
                        $(elem).after($('<label/>').css('display', 'block').text('*Campo obrigatório'));
                })
            }

            return valid;
        }
    </script>
    <div class="full_w">
        <div class="h_title"></div>
        <h1>Integração Protheus</h1>

        <div class="tabelaPagina">
            <asp:Table runat="server">
                <asp:TableRow ID="trTabelas">
                    <asp:TableCell>Tabela:</asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server"
                            ID="ddlTabelas"
                            ClientIDMode="Static">
                            <asp:ListItem Text="Selecione..." Value="" />
                            <asp:ListItem Text="pln_prg_sau" Value="pln_prg_sau" />
                            <asp:ListItem Text="pln_prg_prv" Value="pln_prg_prv" />
                            <asp:ListItem Text="cmpte_submsa_devpt" Value="cmpte_submsa_devpt" />
                            <asp:ListItem Text="cmpte_submsa_ftmto" Value="cmpte_submsa_ftmto" />
                            <asp:ListItem Text="item_ctr_cap" Value="item_ctr_cap" />
                            <asp:ListItem Text="item_ctr_devpt" Value="item_ctr_devpt" />
                            <asp:ListItem Text="item_ctr_rede" Value="item_ctr_rede" />
                            <asp:ListItem Text="patr_sau" Value="patr_sau"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button runat="server" ID="btnConsultar" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:RequiredFieldValidator runat="server"
                            ID="ddlTabelasVal"
                            ControlToValidate="ddlTabelas"
                            ClientIDMode="Static"
                            ForeColor="Red"
                            ErrorMessage="Informe a tabela" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="trFilter" Visible="false" ClientIDMode="Static" />
            </asp:Table>

            <asp:Table runat="server" ID="tblAdd" ClientIDMode="Static" Visible="false">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button runat="server" ID="btnAdd" OnClick="btnAdd_Click" Text="Adicionar" CssClass="button" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow Visible="false" ID="trForm">
                    <asp:TableCell>
                        <asp:Table runat="server" ID="tblForm" ClientIDMode="Static">
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Panel runat="server" ID="pnlGrid" ClientIDMode="Static" CssClass="tabelaPagina">
                <asp:GridView runat="server"
                    ID="gvData"
                    AutoGenerateColumns="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvData_PageIndexChanging"
                    OnRowEditing="gvData_RowEditing"
                    OnRowUpdating="gvData_RowUpdating"
                    OnRowCancelingEdit="gvData_RowCancelingEdit">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="Primeira" LastPageText="Ultima" NextPageText="Próxima" PreviousPageText="Anterior" />
                </asp:GridView>
            </asp:Panel>
        </div>

        <asp:HiddenField runat="server" ID="hdnResponse" ClientIDMode="Static" />
    </div>
</asp:Content>
