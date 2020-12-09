<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Foods.Source.IP.test" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true"
        OnPageIndexChanging="OnPageIndexChanging" ShowFooter="true">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="Order ID" ItemStyle-Width="60" />
            <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-Width="210" />
            <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-Width="60" DataFormatString="{0:N2}"
                ItemStyle-HorizontalAlign="Right" />
        </Columns>        
    </asp:GridView>
    <asp:Button ID="btnsendmail" runat="server" Text="Send Email" OnClick="btnsendmail_Click" />    
     <br />
     <asp:Label ID="lblmail" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
