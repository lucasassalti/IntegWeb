<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="SingleSignOn.aspx.cs" Inherits="IntegWeb.Intranet.Web.SingleSignOn_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
	<div class="wpthemeInner">					
        <div class="centeredTextDiv">
            <span class="bigErrorText">Ocorreu um erro na tentativa de autenticar a sessão. Por favor tente novamente mais tarde.</span><br />
            <span><asp:Label runat="server" ID="lblMessagem"></asp:Label></span>
        </div>
	</div>
                            
</asp:Content>
