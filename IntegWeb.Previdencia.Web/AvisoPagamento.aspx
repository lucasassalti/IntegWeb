<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="false" CodeBehind="AvisoPagamento.aspx.cs" Inherits="IntegWeb.Previdencia.Web.AvisoPagamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">

   

<script type="text/javascript"> 

    //var codEmpresa = $("#hidEmpresa").val();
    //var codMatricula = $("#hidMatricula").val();
    //var codRepress = $("#hidRepress").val();

    function recarregarFunction() {
        
       // if (codMatricula != "") {
            $("#divAvisoPagamanto").show();
            conteudo = "<span class='button downloadAvis'>Visualizar Aviso de Pagamento</span><br><br>";
            $('#divURL').append(conteudo);
        //}

          if ($("#hidRepress").val() != "" || $("#hidRepress").val() != 0) {
            $(".downloadAvis").click(function () {
                window.open("http://intraprod/prod/AvisoPgto.aspx?nempr=" + $("#hidEmpresa").val() + "&nreg=" + $("#hidMatricula").val() + "&ndep=0&nrepr=" + $("#hidRepress").val() + "&hidANO_REFERENCIA=");// + $(this)[0].getAttribute("id"));

            });
        } else {
            $(".downloadAvis").click(function () {
                window.open("http://intraprod/prod/AvisoPgto.aspx?nempr=" + $("#hidEmpresa").val() + "&nreg=" + $("#hidMatricula").val() + "&ndep=0&nrepr=0&hidANO_REFERENCIA=");// + $(this)[0].getAttribute("id"));

            });
        }
    }

    //if (window.event.keyCode == 13) {
    //    alert('aqui');
    //    //$("#btnPesquisar").click();
    //}


   
   
</script>
    <style>
       #divURL{
            margin-left: 15px;
        }

        #grdAvisoPagamento, #grdAvisoRepress{
            width:500px !important;

        }
       table th {
            padding: 5px !important;
        }



     span.alt_lista_pdf {
      color: #a8a7a7;
      float: left;
      font-size: 14px;
      font-weight: bold;
      line-height: 23px;
      text-transform: uppercase;
      background: url(img/icone_pdf.jpg) left top no-repeat;
      margin: 0 0 12px 20px;
      padding: 0px 0 0 27px;
      width: 177px;
      cursor: pointer;
    }

    </style>

    <div class="full_w">

        <div class="h_title">Aviso de Pagamento</div>

        <asp:UpdatePanel runat="server" ID="upPanel">
            <ContentTemplate>
        <div class="tabelaPagina">
           <h1> Consultar Aviso de Pagamento</h1>
            <div class="MarginGrid">
                <table>
                     <tr>
                          <td>Digite o número da empresa:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodEmpresa" runat="server"  onkeypress="mascara(this, soNumeros)" Width="100px"></asp:TextBox>
                        </td>
                    </tr>                   
                    
                    <tr>
                        <td>Digite o número da matrícula:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodMatricula" runat="server"  onkeypress="mascara(this, soNumeros)" Width="100px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>Digite o número do Representante:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodRepres" runat="server"  onkeypress="mascara(this, soNumeros)" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                 
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" runat="server" CssClass="button" Text="Pesquisar   " />
                            <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" runat="server" CssClass="button" Text="Limpar" />
                    </tr>
                </table>
            </div>
         
        
            <asp:GridView EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="true" CssClass="" ID="grdAvisoPagamento" runat="server">
                       <%-- <Columns>
                            <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Matrícula" DataField="NUM_RGTRO_EMPRG" ItemStyle-HorizontalAlign="Center" />
                           <asp:BoundField HeaderText="Nome" DataField="NOM_EMPREG" />
                          </Columns>--%>
                    </asp:GridView>

             <asp:GridView EmptyDataText="A consulta não retornou dados" AutoGenerateColumns="true" CssClass="Table" ID="grdAvisoRepress" runat="server">
                        
                    </asp:GridView>


            <div id="divAvisoPagamanto" runat="server">

                <asp:HiddenField ID="hidEmpresa" runat="server"/>
                <asp:HiddenField ID="hidMatricula" runat="server"/>
                <asp:HiddenField ID="hidRepress" runat="server"/>
              
                 <div id="divURL"></div>

            </div>


        </div>
        </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
