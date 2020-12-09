<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_loadsheet.aspx.cs" Inherits="Foods.frm_loadsheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Load Sheet</title>
    <style type="text/css">
        body {
            font-family:CordiaUPC;
            font-size:18px;                        
           }
        h4,h5, GridView {
            text-align:center;
        }
        .gv {
            width:100%;
            height:auto;
            margin:0px auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4><u>Ham Brothers</u></h4>
        <h5><u>Load Sheet</u></h5>
        <br />
        <asp:Label ID="label2" runat="server" Text="Sales Man:"></asp:Label>&nbsp;&nbsp;&nbsp;<u><asp:Label ID="lbl_salesMan" runat="server" Text="Sales Man:"></asp:Label></u>
        <fieldset><legend>Load Sheet</legend>
        <asp:GridView ID="GVLoadSheet" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="dsrid" CssClass="gv">
            <Columns>
                <asp:BoundField DataField="dsrid" HeaderText="ID" ReadOnly="True" SortExpression="dsrid" />
                <asp:BoundField DataField="ProductName" HeaderText="Product" SortExpression="ProductName" />
                <asp:BoundField DataField="Qty" HeaderText="Quantity" SortExpression="Qty" />
                <asp:BoundField DataField="dozen1" HeaderText="Dozen" SortExpression="dozen1" />
                <asp:BoundField DataField="cartons" HeaderText="Cartons" SortExpression="cartons" />
                <%--<asp:BoundField DataField="items" HeaderText="Items" SortExpression="items" />--%>

                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkok" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
            <asp:Label ID="label1" runat="server" Text="Total"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="lbltotal" runat="server" ></asp:Label>
        </fieldset>
    </div>
    </form>
</body>
</html>
