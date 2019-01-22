<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="TrocaArquivos.aspx.cs" Inherits="IntegWeb.Previdencia.Web.TrocaArquivos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>

        a img {
            border: 0;
        }

        .block {
            margin-bottom: 15px;
            width: 200px;
        }

            .block .block-title {
                font-weight: 700;
                color: #444;
                font-size: 15px;
                margin: 0 0 3px;
            }

            .block .block-image {
                margin: 0 0 3px;
                background-size: cover;
                text-align: center;
                background-position: center center;
            }

                .block .block-image img {
                    width: 100%;
                }

                .block .block-image + div {
                    margin-top: 10px;
                }

            .block .block-obs {
                color: #999;
                font-size: 11px;
                margin: 0 0 3px;
            }

            .block .block-body {
                font-size: 13px;
            }

                .block .block-body > ul {
                    list-style: outside;
                    padding: 0 0 0 17px;
                    margin: 5px 0 15px;
                }

            .block .block-body {
                margin-bottom: 15px;
                margin-left: 10px;
                margin-right: 10px;
            }


                .block .block-body .block-actions .pull-left {
                    margin-right: 10px;
                }

            .block .block-call {
                display: block;
                background-color: #999;
                padding: 7px 20px;
                color: #fff;
                margin-top: 4px;
                font-size: 12px;
                float: right;
            }


                .block .block-call:hover {
                    background: #eb1946;
                }

            .block.loan-block .block-call:hover {
                background: #00978a;
            }
    </style>

    <table>
        <tr>
            <td>
                <div class="h_title" style="width: 600px;">
                    Serviços On-line - Troca de Arquivos
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tabelaPagina">
                    <tr>
                        <td style="padding: 20px;">
                            <div class="block">
                                <div class="block-title">Enviar arquivos</div>
                                <div class="feed-item-image">
                                    <a href="ArquivoPatrocinadora.aspx">
                                        <img src="img/UploadPanel.png" alt="Aviso de Pagamento" style="width: 200px; height: 70px;">
                                    </a>
                                </div>
                                <div class="block-body">
                                    <div id="mCSB_28" tabindex="0">
                                        <div id="mCSB_28_container" style="position: relative; top: 0; left: 0; color: black;" dir="ltr">Clique aqui para validar, enviar e carregar arquivos.<br />
                                            Aqui você também pode emitir o demonstrativo de repasse.</div>
                                    </div>
                                </div>
                                <a title="" target="_self" href="ArquivoPatrocinadora.aspx" class="block-call">Acesse<i class="fa fa-plus"></i></a>
                            </div>
                        </td>
                        <td style="padding: 20px;">
                            <div class="block">
                                <div class="block-title">Receber arquivos</div>
                                <div class="feed-item-image">
                                    <a href="ArquivoPatrocinadoraEnv.aspx">
                                        <img src="img/DownloadPanel.png" alt="Aviso de Pagamento" style="width: 200px; height: 70px;">
                                    </a>
                                </div>
                                <div class="block-body">
                                    <div id="mCSB_29" tabindex="0">
                                        <div id="mCSB_29_container" style="position: relative; top: 0; left: 0; color: black;" dir="ltr">Clique aqui para ter acesso aos arquivos e documentos que a Funcesp disponibiliza para você.</div>
                                    </div>
                                </div>
                                <a title="" target="_self" href="ArquivoPatrocinadoraEnv.aspx" class="block-call">Acesse<i class="fa fa-plus"></i></a>
                            </div>
                        </td>
                        <td style="padding: 20px;">
                            <div class="block">
                                <div class="block-title">Sair</div>
                                <div class="feed-item-image">
                                    <a href="#" onclick="window.open('about:blank', '_self', '');">
                                        <img src="img/LogoutPanel.png" alt="Aviso de Pagamento" style="width: 200px; height: 70px;">
                                    </a>
                                </div>
                                <div class="block-body">
                                    <div id="Div1" tabindex="0">
                                        <div id="Div2" style="position: relative; top: 0; left: 0; color: black;" dir="ltr">Clique aqui para sair desta funcionalidade e retornar ao Portal Funcesp.<br /><br /></div>
                                    </div>
                                </div>
                                <a title="" target="_self" href="#" class="block-call" onclick="window.open('about:blank', '_self', ''); window.close();">Sair<i class="fa fa-plus"></i></a>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
