<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_stk.aspx.cs" Inherits="Foods.rpt_stk" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Report</title>
    <style type="text/css">
	    body{
	
		    background:#fff;
		    margin:0;
		    padding:20px;
		    font-family:Arial;
		    font-size:12px;
	    }
	    #Container{
		    width:100%;
		    height:800px auto;
	    }
	    .header{
		    width:100%;
		    height:auto;
	    }
	    .header_left
	    {
		    width:50%;
		    height:auto;
		    float:left;
	    }
	    .header_right
	    {
		    width:30%;
            padding:80px 0px 0px;
		    height:auto;
		    float:right;
		    text-decoration:underline;
	    }
	    .logo
	    {
		    width:300px;
		    height:180px;
	    }
	    .small{
	
		    width:100%;
		    height: 30px;
		    font-size:12px;
		    background:#bfbfbf;
	    }
	    .content
	    {
		    width:94%;
		    height:auto;
		    padding:30px;
	    }
	    .heading
	    {
		    width:100%;
		    height:auto;
		    text-decoration:underline;
		    text-align:center;
		    font-style:italic;
	    }
	    .left{
		    width:49%;
		    height:auto;
		    float:left;
	    }
	    .low_left{
		    width:50%;
		    height:auto;
		    float:left;
	    }
	    .custadd
	    {
		    width:300px;
		    height:80px;		 
	    }
	    .right{
		    width:49%;
		    height:auto;
		    float:right;
	    }
	    .low_right{
		    width:29%;
		    height:auto;
		    float:right;
		    padding:180px 0px 40px 130px;
	    }
	    .clear{
		    clear:both;
	    }
	    table{
		    width:100%;
		    height:auto;
		    font-size:12px;
		    border:1px solid #000;
	    }
	    tr td {
		    font-size:12px;	
		    border:1px solid #000;
	    }
	    #Main
	    {
		    width:100%;
		    height:auto;
	    }
	    #Footer
	    {
		    width:100%;
		    font-size:14px;
		    height:auto;
		    text-align:center;
	    }
	    .end
	    {
		    width:100%;
		    height:30px;
		    background:green;
	    }
	    p{
		    padding:0px;
		    margin:0px;
	    }

    </style>
</head>
<body >
    <form id="form1" runat="server">
        <div id="Container">
	        <div class="header">
		        <div class="header_left">
                    NM Garments
		        </div>
		        <div class="header_right">
                    <b></b>
		        </div>
	        </div>
	        <div class="clear"></div>
	        <div class="content">
		        <div class="heading">
			        <h1><asp:Label ID="lbl_rptnam" runat="server"></asp:Label></h1>
		        </div>
		        <div class="left">
			        <h4></h4>
			        <div class="custadd">
                        <asp:LinkButton ID="LinkBtnExportExcel" runat="server" OnClick="LinkBtnExportExcel_Click">Export to Excel</asp:LinkButton>       
			        </div>
		        </div>
		        <div class="right">
			        <table style="float:right; width:50%; height:auto;">
				        <tr>
					        <td>Date</td>
					        <td><asp:Label ID="lbl_dat" runat="server" Text=""></asp:Label></td>
				        </tr>
			        </table>
		        </div>	
		        <div class="clear"></div>	
		        <br/>
		        <div id="Main">
                <asp:GridView ID="GVStk" runat="server" AutoGenerateColumns="false" class="gv" EmptyDataText="NO Record Found" ShowHeader="true" ShowHeaderWhenEmpty="true" >
                    <Columns>                    
                        <asp:BoundField DataField="productname" HeaderText="Product" SortExpression="productname" />
                        <asp:BoundField DataField="Dstk_unt" HeaderText="Sizes" SortExpression="Dstk_unt" />
                        <asp:BoundField DataField="Dstk_Qty" HeaderText="Qty" SortExpression="Dstk_Qty" />
                        <asp:BoundField DataField="Dstk_rat" HeaderText="Rate" SortExpression="Dstk_rat" />
                        <asp:BoundField DataField="Stock Value" HeaderText="Stock Value" SortExpression="Stock Value" />  
                    </Columns>  
                </asp:GridView>
                 
		        </div>	
		        <br />
                <table style="width:200px; height:auto; float:right">
				    <tr>
					    <td><label>Stock Value:</label></td>
					    <td style="border-bottom:1px solid #000">
                            <b><asp:Label ID="lbl_ttlStkVal" runat="server" Text="0.00" />Only/=</b>
					    </td>
				    </tr>				
                </table>
		        <br />
		        <br />
		        <div class="clear"></div>	
	        </div>
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
	        <br />
            <div id="Footer">
		        <p></p>
		        <p></p>
		        <div class="end"></div>
	        </div>
        </div>            
    </form>
</body>
</html>
