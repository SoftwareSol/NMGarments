<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_custcrdt.aspx.cs" Inherits="Foods.rpt_custcrdt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Wise Credit Report</title>
          <style type="text/css">
        #container{
	        width:100%;
	        height:100%;
	        font-family:CordiaUPC;
            font-size:18px; 
        }
        .uppper{
	        width:45%;
	        height:auto;
	        margin:0px auto;
	        text-align:center;
        }
        .left{
	        width:45%;
	        height:auto;
	        float:left;
	        margin-left:20px;
        }
        .right{
	        width:49%;
	        height:auto;
	        float:right;
	        text-align:center;
        }
        .gv{
	        width:100%;
	        height: auto;
            text-align:center;
        }
        .clear{
        clear: both;
        }
        h1 {
        margin:0px;
        padding: 0px;
        }
        h2 {
        margin:0px;
        padding: 0px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div id="container">
            <div class="uppper">
                <h1>Nm Garments</h1>
                <h2>Customer Wise Credit Report</h2>
            </div>
            <div class="left">
                Customer Name: <asp:Label ID="lbl_Cust" runat="server" ></asp:Label><br />
            </div>
            <div class="right">
                <asp:LinkButton ID="LinkBtnExportExcel" runat="server" OnClick="LinkBtnExportExcel_Click">Export to Excel</asp:LinkButton>       
            </div>
            <div class="clear"></div>
                <fieldset>
                    <br />
                    <br />
                    <div class="gv">
                        <asp:GridView ID="GV_CustCre" runat="server" EmptyDataText="No Record Found!" AutoGenerateColumns="False" style="width:100%; height:auto;" >
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                <asp:BoundField DataField="MSal_sono" HeaderText="MSal_sono" SortExpression="MSal_sono" />
                                <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                                <asp:BoundField DataField="area_" HeaderText="Area" SortExpression="area_" />
                                <asp:BoundField DataField="Booker" HeaderText="Booker" SortExpression="Booker" />
                                <asp:BoundField DataField="SalMan" HeaderText="SalMan" SortExpression="SalMan" />
                                <asp:BoundField DataField="MPurDate" HeaderText="MPurDate" ReadOnly="True" SortExpression="MPurDate" />
                                <asp:TemplateField HeaderText="Out Standing">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_outstand" runat="server" Text='<%# Eval("OutStanding")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_cshamt" runat="server" Text='<%# Eval("Balance")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Recovery" HeaderText="Recovery" SortExpression="Recovery" />
                                <asp:BoundField DataField="CredAmt" HeaderText="Amount Left" SortExpression="CredAmt" />
                                <asp:BoundField DataField="GTtl" HeaderText="Gross Total" SortExpression="GTtl" />
                                <asp:BoundField DataField="Amt" HeaderText="Net Total" SortExpression="Amt" />
                                <asp:BoundField DataField="ChqNo" HeaderText="ChqNo" ReadOnly="True" SortExpression="ChqNo" />
                                <asp:BoundField DataField="ChqAmt" HeaderText="ChqAmt" ReadOnly="True" SortExpression="ChqAmt" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="gv">
                            Total Quantity: <asp:Label ID="ttl_qty" runat="server" Text="--"></asp:Label>
                            Total: <asp:Label ID="lbl_ttl" runat="server" Text="--"></asp:Label>
                    </div>
                </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
