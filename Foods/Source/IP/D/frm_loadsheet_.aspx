<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_loadsheet_.aspx.cs" Inherits="Foods.frm_loadsheet_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Load Sheet</title>
    <style type="text/css">
        body{
        font-size:12px;
        font-family:arial;
        }
        h2 { 
	        text-align:center; width:100%; height:30px;
        }
        h3{ 
	        text-align:center; width:100%; height:30px;
	        text-decoration:underline;
        }
        #container{
	        width:100%;
	        height:100%;
        }
        .up{
	        width:100%;
    	    height:30px;

    }
        .up_left{

	        width:33%;
	        height:30px;
	        float:left;
	        text-align:left;
        }
        .up_middl{

	        width:33%;
	        height:30px;
	        float:left;
	        text-align:center;
        }
        .up_right{

	        width:33%;
	        height:30px;	        
	        float:left;
	        text-align:right;
        }
        .gv{
	    width:100%;
	    height:auto;
        text-align:center;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <script>
            function printDiv(divName) {
                var printContents = document.getElementById(divName).innerHTML;
                var originalContents = document.body.innerHTML;
                document.body.innerHTML = printContents;
                window.print();
                document.body.innerHTML = originalContents;
            }
	</script>

      
        <button onclick="printDiv('printMe')">Print</button>
        <div  id="printMe">
            <div id="container">

	<h2> <asp:Label ID="lblcom" runat="server"></asp:Label> </h2>
	<h3> Load Sheet </h3>
	<div class="up">	
		<div class="up_left">
			Sales Man: ______________________________
		</div>	
		<div class="up_middl">
			Area: __________________________________
		</div>	
		<div class="up_right">
			Date: __________________________________
		</div>	
	</div>
    <asp:GridView ID="GVLoadSheet" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true"  CssClass="gv">
        <%--<Columns>
            <asp:BoundField DataField="dsrdat" HeaderText="ID" ReadOnly="True" SortExpression="dsrdat" />
            <asp:BoundField DataField="ProductName" HeaderText="Product" SortExpression="ProductName" />
            <asp:BoundField DataField="Qty" HeaderText="Quantity" SortExpression="Qty" />
            <asp:BoundField DataField="cartons" HeaderText="Cartons" SortExpression="cartons" />
            <asp:BoundField DataField="Items" HeaderText="Items" SortExpression="items" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkok" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
                
        </Columns>--%>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkok" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
	<div class="clear"></div>		
    </div>
        </div>
    </form>
</body>
</html>
