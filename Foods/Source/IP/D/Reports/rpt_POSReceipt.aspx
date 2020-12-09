<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_POSReceipt.aspx.cs" Inherits="Foods.rpt_POSReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Recipet</title>

<style type="text/css">
    body{
        font-size:18px;
        margin:0px;
        padding:0px;
        font-weight:bolder;
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
        font-size:28px;      
        
    }
    h5 {
        text-align:center;
        margin:0px;
        padding:0px;
    }
    table{
        width:100%;
        height:auto;
        font-size:18px;
        margin-top:30px;
    }
    hr {
        color:gray !important; 
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
        <asp:Image ID="Imglogo" runat="server" Width="100%"  Height="100" ImageUrl="~/img/branchlogo/ibene.jpeg"  /> 
        <table style="font-size:14px; text-align:center" border="0">		
		    <tr>
			    <td>
                    <asp:Label ID="lbl_Add" runat="server" ></asp:Label>
			    </td>			   
		    </tr>
		    <tr>
			    <td>****************************************</td>
		    </tr>
            <tr>
			    <td><asp:Label ID="lbl_Ph" runat="server" Visible="false" ></asp:Label> Cell: <asp:Label ID="lbl_Mob" runat="server" ></asp:Label></td>			  
		   </tr>	    
		    <tr>
			    <td>Cash</td>			   
		    </tr>		    
            <tr>
			    <td><asp:Label ID="lbl_servto" runat="server"></asp:Label></td>			  
		    </tr>	   	
		    <tr>
			    <td>Date & Time:</td>
            </tr>	   	
		    <tr>
			    <td><asp:Label ID="lbl_dattim" runat="server"></asp:Label></td>
		    </tr>	
            <tr>
			    <td>Bill NO: <asp:Label ID="bill_no" runat="server"></asp:Label></td>
		    </tr>	
	    </table>
            <hr />
            <asp:GridView ID="GVPOS" runat="server" Font-Size="Small" Width="80%" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" ShowFooter="true" AutoGenerateColumns="False"  BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="GVPOS_RowDataBound">                
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Item Description" SortExpression="Description" />
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:Label ID="lbl_rat" runat="server" Text='<%# Eval("Rate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                                (<asp:Label ID="lbl_ttlqry" runat="server" ></asp:Label>)
                            </FooterTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lbl_amt" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
	    <hr />
	    <table  style=" width:80%;text-align:left; margin:0px auto; font-size:14px;" border="0">
		    <tr>
			    <td colspan="2">Item Qty:<asp:Label ID="lblitmcnt" runat="server"></asp:Label></td>
		    </tr>	    
		    <tr>
			    <td style="text-align:left;" colspan="2">Total Amount:</td>
			    <td style="text-align:right;" colspan="2"><asp:Label ID="lbl_netamt" runat="server" ></asp:Label></td>
		    </tr>
             <tr>
			    <td style="text-align:left;" colspan="2">Discount Amount:</td>
			    <td style="text-align:right;" colspan="2"><asp:Label ID="lbl_dscamt" runat="server" ></asp:Label></td>
		    </tr>
            <tr>
			    <td style="text-align:left;" colspan="2">Gross Amount:</td>
			    <td style="text-align:right;" colspan="2"><asp:Label ID="lbl_grssamt" runat="server" ></asp:Label></td>
		    </tr>
              <tr>
			    <td style="text-align:left;" colspan="2">Recieved Amount:</td>
			    <td style="text-align:right;" colspan="2"><asp:Label ID="lbl_cshrec" runat="server" ></asp:Label></td>
		    </tr>
              <tr>
			    <td style="text-align:left;" colspan="2">Balance Paid:</td>
			    <td style="text-align:right;" colspan="2"><asp:Label ID="lbl_bal" runat="server" ></asp:Label></td>
		    </tr>
            <tr>
			    <td style="text-align:left;" colspan="2">Prev. Bal:</td>
			    <td style="text-align:right;" colspan="2"><asp:Label ID="lblprebal" runat="server"></asp:Label></td>
		    </tr>	    	
		    <tr>
			    <td colspan="3" style="text-align:center">&nbsp;Timings 12:00 am To 10:00 Pm</td>			   
		    </tr>
		    <tr>
			    <td colspan="3" style="text-align:center">No RETURN</td>
		    </tr>
            <tr>
			    <td colspan="3" style="text-align:center">Exchange With In 3 days With Slip</td>			  
		    </tr>	   
		    <tr>
			    <td style="text-align:left">
                    Cashier/Clerk
			            <br />
                    <asp:Label ID="lbl_usr" runat="server"></asp:Label>
		        </td>	
			    <td style="text-align:center">
                    <b>Floor</b>
			            <br />
                    <asp:Label ID="Label3" runat="server" Text="" ></asp:Label>
                </td>		    
			    <td style="text-align:right">
                    <b>Terminal No</b>
			        <br />
                    <asp:Label ID="lbl_Terminal" runat="server" Text="" ></asp:Label>
                </td>			  
		    </tr>
	    </table>
        <hr />
        <h5>Thanks You for your Custom.</h5>
	    <h5>Please Come Again</h5>
        <h5>Powered By Software Solutions.</h5>
        <h5>Contact: +923152884100  +923122326301</h5>
        </div>
    </div>
    </form>
</body>
</html>
