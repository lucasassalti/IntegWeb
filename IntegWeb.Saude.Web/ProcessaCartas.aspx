<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ProcessaCartas.aspx.cs" Inherits="IntegWeb.Saude.Web.ProcessaCartas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upProcessaCartas" runat="server">
        <ContentTemplate>
            <div class="full_w">
                <div class="h_title">
                </div>
                <h2 style="font-family: Arial, Helvetica, sans-serif; color: #3c454f">Geração - Cartas de Cobrança</h2>

                    <asp:DropDownList ID="ddlDatas" runat="server"></asp:DropDownList>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
