<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_dsr_.aspx.cs" Inherits="Foods.rpt_dsr_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Sale Report</title>
    <style type="text/css">
        body {
            font-family:CordiaUPC;
            font-size:18px;            
            text-align:center;
           }
        h4, GridView {
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
 
     <asp:LinkButton ID="LinkBtnExportExcel" runat="server" OnClick="LinkBtnExportExcel_Click">Export to Excel</asp:LinkButton>       
 
    <h4>Daily Sales Report</h4>
        <asp:GridView ID="GVDSR" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="gv">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="dsrdat" HeaderText="Date" SortExpression="dsrdat" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer" SortExpression="CustomerName" />
                <asp:BoundField DataField="ProductName" HeaderText="Product" SortExpression="ProductName" />
                <asp:BoundField DataField="Qty" HeaderText="Quantity" SortExpression="Qty" />
                <asp:BoundField DataField="salrat" HeaderText="Sale Rate" SortExpression="salrat" />
                <asp:BoundField DataField="salrturn" HeaderText="Sale Returns" SortExpression="salrturn" />
                <asp:BoundField DataField="recvry" HeaderText="Recovery" SortExpression="recvry" />
                <asp:BoundField DataField="outstan" HeaderText="Outstanding" SortExpression="outstan" />
                <asp:BoundField DataField="dsrrmk" HeaderText="Remarks" SortExpression="dsrrmk" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
