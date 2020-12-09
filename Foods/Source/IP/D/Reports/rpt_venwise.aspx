<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_venwise.aspx.cs" Inherits="Foods.rpt_venwise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor Wise Purchase Report</title>

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
        .shpkep{
	        width:50%;
	        height:auto;
	        text-align:center;
	        float:left;
        }
        .salemansig{
	        width:49%;
	        text-align:center;
	        height:auto;
	        float:right;
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
                <h1>NMGarments</h1>
                <h2>Purchase Report Vendor Wise</h2>
            </div>
            <div class="left">
                Vendoe Name: <asp:Label ID="lbl_ven" runat="server" ></asp:Label><br />
                Address: <asp:Label ID="lbl_add" runat="server"></asp:Label><br />
                Phone No: <asp:Label ID="lbl_phno" runat="server"></asp:Label>
            </div>
            <div class="right">
                <asp:LinkButton ID="LinkBtnExportExcel" runat="server" OnClick="LinkBtnExportExcel_Click">Export to Excel</asp:LinkButton>       
            </div>
            <div class="clear"></div>
                <fieldset>
                    <br />
                    <br />
                    <div class="gv">
                        <asp:GridView ID="GV_VenPur" runat="server" AutoGenerateColumns="False" style="width:100%; height:auto;" DataKeyNames="MPurID" >
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                <asp:BoundField DataField="PurNo" HeaderText="PurNo" SortExpression="PurNo" />
                                <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                                <asp:BoundField DataField="suppliername" HeaderText="suppliername" SortExpression="suppliername" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" ReadOnly="True" />
                                <asp:BoundField DataField="Value" HeaderText="Stock Value" SortExpression="Value" ReadOnly="True" />
                                <%--<asp:BoundField DataField="Items" HeaderText="Items" SortExpression="Items" ReadOnly="True" />
                                <asp:BoundField DataField="cartons" HeaderText="cartons" ReadOnly="True" SortExpression="cartons" />--%>
                                <asp:BoundField DataField="MPurRmk" HeaderText="Remarks" SortExpression="MPurRmk" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
