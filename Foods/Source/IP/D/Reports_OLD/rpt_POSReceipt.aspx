<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_POSReceipt.aspx.cs" Inherits="Foods.rpt_POSReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Recipet</title>

<style type="text/css">
    body{
        font-size:12px;
        margin:0px;
        padding:0px;
        font-family:arial;
    }
    .container{
        width:400px;
        height:auto;
        margin:0px auto;
    }
    h1{
        text-align:center;
    }
    h4 {
        width:100%;
        height:20px;
        text-align:center;
        background-color:#6B696B;
        color:#fff;
    }
    h5 {
        text-align:center;
        margin:0px;
        padding:0px;
    }
    table{
        width:100%;
        height:auto;
        font-size:12px;
        margin-top:30px;
    }
</style>
    <script type="text/javascript">
        function winClose() {
            window.print();
            window.setTimeout("window.close();", 1000)
            }
        </script>
</head>
<body onload="winClose();">
    <form id="form1" runat="server">
    <div>
        <div class="container">
	    <h1><asp:Label ID="lbl_Comp" runat="server"></asp:Label></h1>
        <h5><asp:Label ID="lbl_add" runat="server"></asp:Label></h5>
        <h5><asp:Label ID="lbl_no" runat="server"></asp:Label></h5>

	    <h4>Sales Recipet</h4>
	    
	    <table>
		    <tr>
			    <td>Reciept:</td>
			    <td><asp:Label ID="lbl_bill" runat="server"></asp:Label></td>
		    </tr>
		    <tr>
			    <td>Date & Time:</td>
			    <td><asp:Label ID="lbl_dattim" runat="server"></asp:Label></td>
		    </tr>
		    <tr>
			    <td>Terminal:</td>
			    <td>View Point Malir Branch</td>
		    </tr>
		    <tr>
			    <td>Served By:</td>
			    <td><asp:Label ID="lbl_usr" runat="server"></asp:Label></td>
		    </tr>
	    </table>
            <asp:GridView ID="GVPOS" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                            <asp:Label ID="lbl_rat" runat="server" Text='<%# Eval("Rate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
	    
	    <table  style=" width:180px;text-align:left; float:left; font-size:12px;">
		    <tr>
			    <td>No. Of Item(s):</td>
			    <td><asp:Label ID="lblitmcnt" runat="server"></asp:Label></td>
		    </tr>
	    </table>
	    <table style=" width:180px;text-align:right; float:right; font-size:12px;">
		    <tr>
			    <td style="text-align:left;">Net Amount:</td>
			    <td style="text-align:right;"><asp:Label ID="lbl_netamt" runat="server" ></asp:Label></td>
		    </tr>
	    </table>
        <br />
        <br />
        <br />
        <br />
        <br />
	    <h5>Thanks You for your Custom.</h5>
	    <h5>Please Come Again</h5>
        <h5>Powered By Software Solutions(pvt) Ltd.</h5>
        </div>

    </div>
    </form>
</body>
</html>
