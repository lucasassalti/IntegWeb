<%@ Page Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="AnexoII.aspx.cs" Inherits="IntegWeb.Saude.Web.Anexo2" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
        $(document).ready(function () {

            var id;
            id = $("").val();

            

            $("#ContentPlaceHolder1_TabContainer_TabPanel1_btnConfirmaAumento").click(function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Você deseja realmente prosseguir com a alteração?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            });

            // INÍCIO - FUNÇÕES - TELA AUMENTO
            $("#ContentPlaceHolder1_TabContainer_TabPanel1_rdblTipo_1").change(
                function ValidaRadioButton() {
                    if ($("#ContentPlaceHolder1_TabContainer_TabPanel1_rdblTipo_1").val() === "GERAL") {
                        $("#ContentPlaceHolder1_TabContainer_TabPanel1_lstServicos").attr('disabled', true);
                    } else {
                        $("#ContentPlaceHolder1_TabContainer_TabPanel1_lstServicos").attr('disabled', false);
                    }
                });


            // Ajax
            $("#ContentPlaceHolder1_TabContainer_TabPanel1_lstServicos").change(
              function BloquearAlteracaoIdEmpresa() {

                  var validaAcao = false;
                  // Verifica se a opção "LOTE" está marcada, se estiver, não passa pela consulta ajax.
                  $('input:radio[id*=rdblTipo]').each(function () {
                      if ($(this).is(':checked'))
                          if ($(this).val() === "LOTE" || $(this).val() === "GERAL") {
                              validaAcao;
                          } else {
                              validaAcao = true;
                          }
                  });
                  // Se validaAcao for igual a LOTE ou GERAL, ele não deve prosseguir no preenchimento de valores à partir do ListView.
                  if (validaAcao === false) {
                      return;
                  }

                  var obj = new Object();
                  obj.codigoHospital = $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtConvenente").val();
                  obj.codigoServico = $("#ContentPlaceHolder1_TabContainer_TabPanel1_lstServicos").val();
                  var parametros = JSON.stringify(obj);

                  $.ajax({
                      type: "POST",
                      url: "ChamadasAjax.ashx/changeListaDeServicos",
                      data: parametros,
                      dataType: "json",
                      contentType: "application/json; charset=utf-8",
                      success: function (data) {

                          if ($("#ContentPlaceHolder1_TabContainer_TabHosp_txtNumContrato").val(data.COD_HOSP)[0].value > 0) {
                              $("#ContentPlaceHolder1_TabContainer_TabHosp_txtNumContrato").val(data.COD_HOSP);
                          } else {
                              $("#ContentPlaceHolder1_TabContainer_TabHosp_txtNumContrato").val("0");
                          };
                          if ($("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPercentReajuste").val(data.PPROPOSTA)[0].value > 0) {
                              $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPercentReajuste").val(data.PPROPOSTA);
                          } else {
                              $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPercentReajuste").val("0");
                          };
                          if ($("#ContentPlaceHolder1_TabContainer_TabPanel1_txtValProposto").val(data.VPROPOSTO)[0].value > 0) {
                              $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtValProposto").val(data.VPROPOSTO);
                          } else {
                              $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtValProposto").val("0");
                          };
                          if ($("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPerDesconto").val(data.DESCONTO)[0].value > 0) {
                              $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPerDesconto").val(data.DESCONTO);
                          } else {
                              $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPerDesconto").val("0");
                          };

                          if (data.DTREAJUSTEPROP != null) {
                              var mydate = data.DTREAJUSTEPROP
                              var date = new Date(parseInt(mydate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                              mydate = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
                          }
                          $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtDataVigencia").val(mydate);
                      },
                      error: function (data) {
                      }
                  });
              });

            $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtConvenente").keyup(
                    function SelecionarLinhaContrato() {
                        $("#ContentPlaceHolder1_TabContainer_TabPanel1_ddlConvenente").val($("#ContentPlaceHolder1_TabContainer_TabPanel1_txtConvenente").val())
                    });

            // INÍCIO -  PERMITE QUE SOMENTE A CAIXA DE PERCENTUAL DESCONTO OU A PERCENTUAL REAJUSTE SEJA PREENCHIDA.
            $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPercentReajuste").keyup(
                       function LimpaTextoReajuste() {
                           $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPerDesconto").val("");
                       }
                   );
            $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPerDesconto").keyup(
                      function LimpaTextoDesconto() {
                          $("#ContentPlaceHolder1_TabContainer_TabPanel1_txtPercentReajuste").val("");
                      }
                  );
            // FIM -  PERMITE QUE SOMENTE A CAIXA DE PERCENTUAL DESCONTO OU A PERCENTUAL REAJUSTE SEJA PREENCHIDA.



            // INÍCIO -  FUNÇÕES - TABELA DE SERVIÇOS
            $("#ContentPlaceHolder1_TabContainer_TabServicos_ddlHospitalServico").change(
                      function RecuperaIdHospital() {
                          $("#ContentPlaceHolder1_TabContainer_TabServicos_txtNumContratoServicos").val($("#ContentPlaceHolder1_TabContainer_TabServicos_ddlHospitalServico").val());
                      });
            $("#ContentPlaceHolder1_TabContainer_TabServicos_txtNumContratoServicos").keyup(
                  function SelecionarLinhaContrato() {
                      $("#ContentPlaceHolder1_TabContainer_TabServicos_ddlHospitalServico").val($("#ContentPlaceHolder1_TabContainer_TabServicos_txtNumContratoServicos").val())
                  });
            // FIM -  FUNÇÕES - TABELA DE SERVIÇOS     



            // INÍCIO -  FUNÇÕES - OBSERVAÇÕES CONTRATUAIS
            $("#ContentPlaceHolder1_TabContainer_TabContratuais_ddlHospitalCdEmp").change(
                      function RecuperaIdHospital() {
                          $("#ContentPlaceHolder1_TabContainer_TabContratuais_txtcodEmpresaObsContratual").val($("#ContentPlaceHolder1_TabContainer_TabContratuais_ddlHospitalCdEmp").val());
                      });
            $("#ContentPlaceHolder1_TabContainer_TabContratuais_txtcodEmpresaObsContratual").keyup(
                  function SelecionarLinhaContrato() {
                      $("#ContentPlaceHolder1_TabContainer_TabContratuais_ddlHospitalCdEmp").val($("#ContentPlaceHolder1_TabContainer_TabContratuais_txtcodEmpresaObsContratual").val())
                  });
            // FIM -  FUNÇÕES - OBSERVAÇÕES CONTRATUAIS
            

            // INÍCIO -  FUNÇÕES - ESCALONADO
            $('#ContentPlaceHolder1_TabContainer_TabPanel1_txtEscalonado').change(function () {
                $("#ContentPlaceHolder1_TabContainer_TabPanel1_lstServicos").val($("input#ContentPlaceHolder1_TabContainer_TabPanel1_txtEscalonado").val());
            });

            // FIM -  FUNÇÕES - ESCALONADO


            // INÍCIO -  FUNÇÕES - CÓDIGO DE SERVIÇOS
            $("#ContentPlaceHolder1_TabContainer_TabCdServico_ddlListaServicos").change(
                      function RecuperaIdHospital() {
                          $("#ContentPlaceHolder1_TabContainer_TabCdServico_txtTbCodigoServico").val($("#ContentPlaceHolder1_TabContainer_TabCdServico_ddlListaServicos").val());
                      });
            $("#ContentPlaceHolder1_TabContainer_TabCdServico_txtTbCodigoServico").keyup(
                  function SelecionarLinhaContrato() {
                      $("#ContentPlaceHolder1_TabContainer_TabCdServico_ddlListaServicos").val($("#ContentPlaceHolder1_TabContainer_TabCdServico_txtTbCodigoServico").val())
                  });
            // FIM -  FUNÇÕES - CÓDIGO DE SERVIÇOS
        });
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

        .bkgCadastros {
            background-color: #DCDCDC;
        }

        .tamanhotxtddl {
            width: 150px;
        }

        .tamanhoSelect {
            width: 650px;
            height: 200px;
        }

        .AjustarMargem {
            margin: 1%;
        }

        .MarginLeft {
            margin-left: 26px;
        }

        .multiline-text {
            width: 345px;
            height: 95px;
        }

        .displayBlock {
            display: block;
        }

        .displayInline {
            display: inline;
        }
    </style>


    <div id="area_parametro">
        <div class="h_title"></div>
        <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="false" />
        <div class="full_w">

            <div>
                <br />
            </div>

            <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">

                <!-- AUMENTO -->
                <ajax:TabPanel ID="TabPanel1" HeaderText="AUMENTO" runat="server" TabIndex="1">
                    <ContentTemplate>

                        <div class="AjustarMargem radio">
                            <div>
                                <asp:Label Tex="Número Contrato" onkeypress="mascara(this, soNumeros)" ID="lblNumContrato" runat="server">Número Contrato</asp:Label>
                                <asp:TextBox CssClass="tamanhotxtddl" ID="txtConvenente" AutoPostBack="true" OnTextChanged="ddlConvenente_SelectedIndexChanged" runat="server"></asp:TextBox>
                                <asp:DropDownList CssClass="tamanhotxtddl" Width="400px" runat="server" ID="ddlConvenente" AutoPostBack="True" OnSelectedIndexChanged="ddlConvenente_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rdblTipo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdblTipo_SelectedIndexChanged">
                                                <asp:ListItem Text="LOTE&nbsp;&nbsp;" Value="LOTE" />
                                                <asp:ListItem Text="GERAL&nbsp;&nbsp;" Value="GERAL" />
                                                <asp:ListItem Text="POR VALOR&nbsp;&nbsp;" Value="PORVALOR" />
                                                <asp:ListItem Text="ESCALONADO" Value="ESCALONADO" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox BackColor="Gray" ForeColor="White" Width="70px" Enabled="False" ID="txtEscalonado" runat="server"></asp:TextBox>
                                            <asp:Label runat="server" ID="lbl2">Emitir Relatório >></asp:Label>
                                            <asp:CheckBox runat="server" ID="cbxEmiteRel" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div>
                                <asp:ListBox CssClass="tamanhoSelect" ID="lstServicos" runat="server"></asp:ListBox>
                            </div>
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPerReajuste" runat="server" Text="Percentual Reajuste"></asp:Label>
                                            <asp:TextBox CssClass="tamanhotxtddl " Enabled="false" onkeypress="mascara(this, soNumeros)" ID="txtPercentReajuste" runat="server"></asp:TextBox>

                                            <asp:Label ID="lblPerDesconto" runat="server" Text="Percentual Desconto"></asp:Label>
                                            <asp:TextBox CssClass="tamanhotxtddl" Enabled="false" onkeypress="mascara(this, soNumeros)" ID="txtPerDesconto" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblValProposto" runat="server" Text="Valor Proposto &nbsp;"></asp:Label>
                                            <asp:TextBox CssClass="tamanhotxtddl MarginLeft" ID="txtValProposto" Enabled="false" runat="server"></asp:TextBox>

                                            <asp:Label ID="lblDtVigencia" runat="server" Text="Data Vigência&amp;nbsp;&amp;nbsp;&nbsp;"></asp:Label>
                                            <asp:TextBox CssClass="tamanhotxtddl Largura date MarginLeft" Enabled="false" onkeypress="mascara(this, data)" MaxLength="10" ID="txtDataVigencia" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div>
                                <asp:Button CssClass="button" ID="btnConfirmaAumento" runat="server" Text="Confirmar aumento" OnClick="btnConfirmaAumento_Click" />



                                <asp:HiddenField ID="hdnFake" runat="server" />
                                <asp:Button CssClass="button" ID="btnPlanilhaAprovacao" runat="server" Text="Aumentar /Planilha de Aprovação" OnClick="btnPlanilhaAprovacao_Click" />
                                <asp:Button runat="server" CssClass="button" Text="Limpar" ID="btnLimparTelaAumento" OnClick="btnLimparTelaAumento_Click" />
                            </div>

                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>

                <!-- Hospital -->
                <ajax:TabPanel ID="TabHosp" HeaderText="HOSPITAL" runat="server" TabIndex="2">
                    <ContentTemplate>
                        <h3 class="AjustarMargem">Cadastro de Prestador:</h3>
                        <table>
                            <tr>
                                <td>Número do Contrato: 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumContrato" onkeypress="intNumeros(this, data)" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Selecione o Hospital:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlNmHospital" AutoPostBack="true" OnSelectedIndexChanged="ddlNmHospital_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Nome do Hospital: 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNmHospital" Width="450px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Início do Contrato:

                                </td>
                                <td>
                                    <asp:TextBox ID="txtInicioContrato" CssClass="date" onkeypress="mascara(this, data)" runat="server"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>Cidade:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCidade" Width="450px" runat="server"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>Regional:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRegional" Width="450px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Contato:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContato" Width="450px" runat="server"></asp:TextBox>

                                </td>

                            </tr>
                            <tr>
                                <td>Credenciador:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCredenciador" Width="450px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <table style="width: 500px;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnInsEmpresaSalvar" runat="server" CssClass="button AjustarMargem" Text="Incluir" OnClick="btnInsEmpresaSalvar_Click" />
                                    <asp:Button ID="btnUpdServicoAlterar" runat="server" CssClass="button AjustarMargem" Text="alterar" OnClick="btnUpdServicoAlterar_Click" />
                                    <asp:Button ID="btnDelServicoExcluir" runat="server" CssClass="button AjustarMargem" Text="Excluir" OnClick="btnDelServicoExcluir_Click" />
                                    <asp:Button ID="btnServicoVoltar" runat="server" CssClass=" button AjustarMargem" Text="Limpar" OnClick="btnCadEmpvoltar_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajax:TabPanel>


                <!-- CÓDIGO SERVIÇOS BASE TISS -->
                <ajax:TabPanel ID="TabCdServico" HeaderText="CÓDIGO SERVIÇOS" runat="server" TabIndex="3">
                    <ContentTemplate>
                        <h3>Cadastro código de serviço:</h3>
                        <table>
                            <tr>
                                <td>Código do Serviço:
                                    <asp:TextBox ID="txtTbCodigoServico" Width="70px" runat="server"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnPesquisarServico" Text="Pesquisar" CssClass="button AjustarMargem" OnClick="btnPesquisarServico_Click" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Descrição:&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtTbDescricaoCodigoServico" TextMode="MultiLine" Width="470px" CssClass=" multiline-text" runat="server"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnincluirServico" Text="Incluir" CssClass="button" OnClick="btnincluirServico_Click" />
                                    <asp:Button runat="server" ID="btnAlterarServico" Text="Alterar" CssClass="button" OnClick="btnAlterarServico_Click" />
                                    <asp:Button runat="server" ID="btnExcluirServico" Text="Excluir" CssClass="button" OnClick="btnExcluirServico_Click" />
                                    <asp:Button runat="server" ID="btnVoltarServico" Text="Limpar" CssClass="button" OnClick="btnVoltarServico_Click" />
                                </td>
                                <td></td>
                            </tr>
                        </table>

                    </ContentTemplate>
                </ajax:TabPanel>

                <!-- TABELA SERVIÇOS -->
                <ajax:TabPanel ID="TabServicos" HeaderText="TABELA SERVIÇOS POR PRESTADOR" runat="server" TabIndex="4">
                    <ContentTemplate>
                        <h3>Tabela de código de serviço:</h3>
                        <table>
                            <tr>
                                <td>Número do Cotrato:
                                                <asp:TextBox runat="server" Width="80px" ID="txtNumContratoServicos"></asp:TextBox>
                                    <asp:DropDownList runat="server" ID="ddlHospitalServico" Width="250px"></asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td>Código do Serviço:&nbsp;
                                                <asp:TextBox runat="server" CssClass="displayInline" Width="80px" ID="txtCodigoServico" AutoPostBack="true" OnTextChanged="txtCodigoServico_Changed"></asp:TextBox>
                                    <label runat="server" id="lbl">Utilização Descrição Base?</label>
                                    <asp:RadioButtonList CssClass="displayInline" ID="rdblDescricaoBase" AutoPostBack="true" OnSelectedIndexChanged="txtCodigoServico_Changed" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem Selected="True" Text="Sim" Value="yes" />
                                        <asp:ListItem Text="Não" Value="no" />
                                    </asp:RadioButtonList>

                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Descrição: &nbsp;&nbsp;
                                    <asp:TextBox ID="txtDescricaoServico" TextMode="MultiLine" CssClass="AjustarMargem" runat="server" Width="300px" Style="margin-left: 45px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td>Valor Atual:&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtValorAtual" Width="80px" CssClass="AjustarMargem" runat="server" Style="margin-left: 35px"></asp:TextBox>
                                    Vigência Inicial:
                                                <asp:TextBox ID="txtVigenciaInicial" Width="80px" runat="server" CssClass="tamanhotxtddl Largura date AjustarMargem" onkeypress="mascara(this, data)"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnIncluirServicoPrestador" runat="server" CssClass="button AjustarMargem" Text="Incluir" OnClick="btnIncluirServicoPrestador_Click" />
                                    <asp:Button ID="btnAlterarServicoPrestador" runat="server" CssClass="button AjustarMargem" Text="Alterar" OnClick="btnAlterarServicoPrestador_Click" />
                                    <asp:Button ID="btnExcluirServicoPrestador" runat="server" CssClass="button AjustarMargem" Text="Excluir" OnClick="btnExcluirServicoPrestador_Click" />
                                    <asp:Button ID="btnTbServicoLimpar" runat="server" CssClass="button AjustarMargem" Text="Limpar" OnClick="btnTbServicoLimpar_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajax:TabPanel>

                <!-- OBSERVAÇÕES CONTRATUAIS -->
                <ajax:TabPanel ID="TabContratuais" HeaderText="OBSERVAÇÕES CONTRATUAIS" runat="server" TabIndex="5">
                    <ContentTemplate>
                        <h3 class="AjustarMargem">Observações Contratuais:</h3>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblobs1" runat="server" Text="Contrato: "></asp:Label>
                                        <asp:TextBox runat="server" ID="txtcodEmpresaObsContratual" AutoPostBack="true" OnTextChanged="ddlHospitalCdEmp_SelectedIndexChanged"></asp:TextBox>
                                        <asp:DropDownList runat="server" Width="350px" ID="ddlHospitalCdEmp" AutoPostBack="true" OnSelectedIndexChanged="ddlHospitalCdEmp_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl12" runat="server" Text="Observações Contratuais: "></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" Width="590px" Height="200px" TextMode="MultiLine" ID="txtObservacoesContratuais"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button runat="server" CssClass="button" Text="Incluir" ID="btnIncluirObsContratual" OnClick="btnIncluirObsContratual_Click" />
                                        <asp:Button runat="server" CssClass="button" Text="Alterar" ID="btnAlterarObsContratual" OnClick="btnAlterarObsContratual_Click" />
                                        <asp:Button runat="server" CssClass="button" Text="Excluir" ID="btnExcluirObsContratual" OnClick="btnExcluirObsContratual_Click" />
                                        <asp:Button runat="server" CssClass="button" Text="Limpar" ID="btnLimparObsContratual" OnClick="btnLimparObsContratual_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>


                <!-- OBSERVAÇÕES CONTRATUAIS -->
                <ajax:TabPanel ID="TabPanel2" HeaderText="EXPORTAÇÃO" runat="server" TabIndex="6">
                    <ContentTemplate>
                        <h3 class="AjustarMargem">Exportação de arquivos:</h3>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox CssClass="tamanhoSelect" SelectionMode="Multiple" ID="lstEmpresasExportacao" runat="server"></asp:ListBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <asp:Button runat="server" CssClass="button" Text="Exportar informações para SCAM" ID="btnExportaScan" OnClick="btnGeraArquivoScan_Click" />
                                    </td>
                                </tr>
                                <br />

                                <tr>
                                    <td>Início:&ensp;&ensp;&ensp;&ensp;&ensp;<asp:TextBox ID="txtDataIniLog" CssClass="date" onkeypress="mascara(this, data)" runat="server"></asp:TextBox>
                                        Fim:
                                        <asp:TextBox ID="txtDataFimLog" CssClass="date" onkeypress="mascara(this, data)" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <td>Prestador:
                                    <asp:TextBox ID="txtPrestadorLog" runat="server"></asp:TextBox>    
                                    Procedimento:
                                    <asp:TextBox ID="txtProcServLog" Width="104px" runat="server"></asp:TextBox>                                
                                </td>

                                <td>
                                </td>
                                </tr>
                             
                                <tr>
                                    <td>
                                        <asp:Button runat="server" CssClass="button" Text="Carregar Histórico" ID="btnCarregaLog" OnClick="btnCarregaLog_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <asp:GridView runat="server"
                            ID="gridLogExportacao"
                            AutoGenerateColumns="false"
                            AllowPaging="True"
                            ForeColor="Black"
                            OnPageIndexChanging="GridView1_PageIndexChanging"
                            EmptyDataText="A consulta não retornou dados">
                            <Columns>
                                <asp:BoundField HeaderText="&nbsp;&nbsp; USUÁRIO ALT. &nbsp;&nbsp;" DataField="USUARIO" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="&nbsp;&nbsp; DATA EXPORTAÇÃO  &nbsp;&nbsp;" DataField="DataAlteracao" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="&nbsp;&nbsp; DATA REAJUSTE  &nbsp;&nbsp;" DataField="dt_reajuste_tb_val_recurso" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                
                                <%--<asp:BoundField HeaderText="&nbsp;&nbsp; COD. RECURSO &nbsp;&nbsp;  " DataField="COD_RECURSO_TB_VAL_RECURSO" ItemStyle-HorizontalAlign="Center" />--%>
                                <asp:BoundField HeaderText="&nbsp;&nbsp; VALOR AJUSTADO &nbsp;&nbsp; " DataField="val_recurso_tb_val_recurso" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="&nbsp;&nbsp; PROCEDIMENTO &nbsp;&nbsp; " DataField="COD_SERV" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="&nbsp;&nbsp; PRESTADOR &nbsp;&nbsp; " DataField="COD_HOSP" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>

                    </ContentTemplate>
                </ajax:TabPanel>
            </ajax:TabContainer>


        </div>

        <asp:UpdatePanel runat="server" ID="upCisao"></asp:UpdatePanel>
    </div>

    <ajax:ModalPopupExtender
        ID="ModalPopupValidaData"
        runat="server"
        DropShadow="true"
        PopupControlID="panelPopup"
        TargetControlID="ctl00$ContentPlaceHolder1$TabContainer$TabPanel1$hdnFake"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>

    <asp:Panel ID="panelPopup" runat="server" Font-Size="Larger" ForeColor="Black" Width="400px" Height="100px" Style="display: none; background-color: white; border: 1px solid black">
        <h3 class="align-center">MENSAGEM:</h3>
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblMsgmDatas"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" Text="SIM" CssClass=" button AjustarMargem" ID="btnConfirmarAumentoAtualiza" OnClick="btnAjustaDataVigencia_Click" />
                    <asp:Button runat="server" Text="NÃO" CssClass=" button AjustarMargem" ID="btnConfirmarAumentoAtualizaVoltar" OnClick="btnNaoAjustaDataVigencia_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>

