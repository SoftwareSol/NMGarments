<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SMS.aspx.cs" Inherits="Foods.Source.IP.SMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <center>
        <div>
            <br />
         <h3>Send SMS</h3>
            <table border="0">
                <tr>
                    <td> 
                        <asp:Label ID="Label1" runat="server" Text="Enter Mobile Number"></asp:Label>    
                    </td>
                    <td>
                        <asp:TextBox ID="TBNo" runat="server"></asp:TextBox>    
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="TBMsg" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button runat="server" ID="btnSent" Text="Sent SMS" OnClick="btnSent_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <br />    
            <br />
            
        </div>
        </center>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="2000" DynamicLayout ="true">
        <ProgressTemplate>
        <center><img id="Img1" src="~/Images/123.gif" runat="server" /></center>
        </ProgressTemplate>
        </asp:UpdateProgress>

</asp:Content>
