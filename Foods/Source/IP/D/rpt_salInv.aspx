<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_salInv.aspx.cs" Inherits="Foods.Source.IP.rpt_salInv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sale Invoice</title>
    <style>
        h2{ 
	        text-align:center; width:100%; height:30px;
	         margin:0px; padding: 0px;
        }h5{ 
	        text-align:center; width:100%; height:30px;
	        margin:0px; padding: 0px;
        }
        #container{
	        width:100%;
	        height:100%;
            font-family:Arial Narrow;
	    
        }
        .left{
	        width:30%;
	        height:auto;
	        float:left;

        }
        .right{
	        width:69%;
	        height:auto;
	        float:right;
	        text-align:right;
        }
        .GVSal {
            width:100%;
	        height:auto;	   
            text-align:center; 
        }
    </style>
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="container">
            <button onclick="printDiv('printMe')">Print</button>
            <div  id="printMe">
            <h2>Ham Brothers</h2>
            <h5>Korangi</h5>
            <h5>ph no 0218897</h5>
            <div class="left">
	            P.O #: <asp:Label ID="lblpo" runat="server" Text="-" /><br />
	            Bill No #: <asp:Label ID="lblbillno" runat="server" Text="-" /><br />
	            Date: <asp:Label ID="lbldat" runat="server" Text="-" /><br />
                
	
            </div>
            <div class="right">
                Customer Name<br />
                <asp:Label ID="lblcust" runat="server" Text="Ham Brothers (Pvt) Ltd" />            
            </div>	
            
            <div class="clear"></div>
            <br />  
            <br />  
            <br />  
            <br />  	            
            <hr />
            <br />  
            <br />  
            <br />  
           
             <asp:GridView ID="GVSal" CssClass="GVSal" runat="server" ShowFooter="true" EmptyDataText="Not Sales Generated!" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDataBound="GVSal_RowDataBound">                
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="S.No" SortExpression="ID" ReadOnly="True" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product" SortExpression="ProductName" ReadOnly="True" />
                    <asp:BoundField DataField="DSal_ItmQty" HeaderText="Qty" SortExpression="DSal_ItmQty" ReadOnly="True" />
                    <asp:BoundField DataField="rat" HeaderText="Rate" SortExpression="rat" ReadOnly="True" />
                    <asp:BoundField DataField="Amt" HeaderText="Amount" SortExpression="Amt"  ReadOnly="True"/>
                    <asp:BoundField DataField="Discount" HeaderText="Discount" SortExpression="Discount" ReadOnly="True" />                                                                                        
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label ID="lbl_amt" runat="server" Text='<%# Eval("Total") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbl_ttlamt" runat="server" Text='<%# Eval("Total") %>' />                            
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>	

           
            </div>
     </div>
        </div>
    </form>
</body>
</html>
