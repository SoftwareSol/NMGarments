<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutStand.aspx.cs" Inherits="Foods.Source.IP.OutStand" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList id="ddlVenNam" runat="server"  OnSelectedIndexChanged="ddlVenNam_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>                                               
        <br />
        Previous Balance <asp:TextBox ID="txt_outstand"  runat="server" />
        <br />
        <br />
        More <asp:TextBox ID="TBOutstand" runat="server" ></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnoutstand" runat="server" Text="Save" OnClick="btnoutstand_Click" />
    </div>
    </form>
</body>
</html>
