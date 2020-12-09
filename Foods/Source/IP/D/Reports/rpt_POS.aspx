<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_POS.aspx.cs" Inherits="Foods.rpt_POS" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml"  lang="en">
<head id="Head1" runat="server">
   <title>Detail Daily Sales Report</title>

    <!-- Required meta tags -->
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous" />

    <style>
		body{
			font-size:12px;
			margin:50px 100px;
		}
		.left p{
			margin:0px;
			padding:0px;
		}
		.right p{
			margin:0px;
			padding:0px;
		}
		h4{
			font-size:12px;
			font-weight:normal;
			float:right;
		}	
		table{
			text-align:center;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        	<div class="container">
	            <div class="row">
		        <div class="col">
		            <h3>NM Garments</h3>
		            <h5><i>
                            <asp:Label ID="lblBrnchNam" runat="server"></asp:Label>
		                </i>
		            </h5>
		            <div class="row">&nbsp;</div>
		            <div class="left">
			            <p><b>Address</b></p>
			            <p><asp:Label ID="lbl_Add" runat="server" ></asp:Label></p>
			            <p><asp:Label ID="lbl_Ph" runat="server" Visible="false" ></asp:Label> Cell: <asp:Label ID="lbl_Mob" runat="server" ></asp:Label></p>
		            </div>
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>
		        <div class="col">
		            <div class="right">
			            <h1><i>Detail Daily Sales Report</i></h1>
			            <div class="row">&nbsp;</div>
			            <p><b>DATE & Time:</b> <asp:Label ID="lbl_dattim" runat="server"></asp:Label></p>
			            <p><b>INVOICE#</b> <asp:Label ID="bill_no" runat="server"></asp:Label></p>			 
			            <p><b>TYPE OF TRANSACTION</b> Cash</p>			 
		            </div>		 
		        </div>
	            </div>
	            <div class="row">&nbsp;</div>
	            <div class="row"><b>Comment or Special Instructions:</b> None</div>
	            <div class="row">&nbsp;</div>	  	
	            <div class="row">&nbsp;</div>	  
	            <div class="row">
		        <div class="col">
                    <asp:GridView ID="GVPOS" runat="server" CssClass="table table-sm" Font-Size="Small" Width="80%" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" ShowFooter="True" AutoGenerateColumns="False"  BorderStyle="Double" BorderWidth="3px" CellPadding="4" OnRowDataBound="GVPOS_RowDataBound" BackColor="White" BorderColor="#dee2e6" GridLines="Horizontal">                
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="ITEM DESCRIPTION" SortExpression="Description" />
                            <asp:TemplateField HeaderText="Price" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_rat" runat="server" Text='<%# Eval("Rate")%>'></asp:Label> Rs/.
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                        <asp:Label ID="lbl_ttlqry" runat="server" ></asp:Label>
                                    </FooterTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="AMOUNT">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amt" runat="server" Text='<%# Eval("Amount")%>'></asp:Label> Rs/.
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#dee2e6" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#dee2e6" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#dee2e6" Font-Bold="True" ForeColor="Black" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>			
		        </div>
	            </div>
	            <div class="row">
		        <div class="col-sm">
		            &nbsp;
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>			
		        <div class="col">
		            <h4>ITEM QTY:</h4>
		        </div>
		        <div class="col">
		            <asp:Label ID="lblitmcnt" runat="server"></asp:Label>
		        </div>
	            </div>
	            <div class="row">
		        <div class="col-sm">
		            &nbsp;
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>			
		        <div class="col">
		            <h4>TOTAL AMOUNT</h4>
		        </div>
		        <div class="col">
		            <asp:Label ID="lbl_netamt" runat="server" ></asp:Label>
		        </div>
	            </div>
	            <div class="row">
		        <div class="col-sm">
		            &nbsp;
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>			
		        <div class="col">
		            <h4>DISCOUNT AMOUNT</h4>
		        </div>
		        <div class="col">
                    <asp:Label ID="lbl_dscamt" runat="server" ></asp:Label>
		        </div>
	            </div>
	            <div class="row">
		        <div class="col-sm">
		            &nbsp;
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>			
		        <div class="col">
		            <h4>GROSS AMOUNT</h4>
		        </div>
		        <div class="col">
                    <asp:Label ID="lbl_grssamt" runat="server" ></asp:Label>
		        </div>
	            </div>
	            <div class="row">
		        <div class="col-sm">
		            &nbsp;
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>			
		        <div class="col">
		            <h4>RECIEVED AMOUNT</h4>
		        </div>
		        <div class="col">
		            <asp:Label ID="lbl_cshrec" runat="server" ></asp:Label>
		        </div>
	            </div>     
                <div class="row">
		        <div class="col-sm">
		            &nbsp;
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>			
		        <div class="col">
		            <h4>BALANCE PAID</h4>
		        </div>
		        <div class="col">
		            <asp:Label ID="lbl_bal" runat="server" ></asp:Label>
		        </div>
	            </div>
                <div class="row">
		        <div class="col-sm">
		            &nbsp;
		        </div>
		        <div class="col">
		            &nbsp;
		        </div>			
		        <div class="col">
		            <h4>PREVOIUS BALANCE</h4>
		        </div>
		        <div class="col">
		            <asp:Label ID="lblprebal" runat="server"></asp:Label>
		        </div>
	            </div>	  
                <table  style=" width:80%;text-align:left; font-size:14px; margin:0px auto"  border="0">
		        <tr>
			        <td colspan="2"></td>
		        </tr>	    
		        <tr>
			        <td style="text-align:left;" colspan="2">&nbsp;</td>
			        <td style="text-align:right;" colspan="2">&nbsp;</td>
		        </tr>
                    <tr>
			        <td style="text-align:left;" colspan="2">&nbsp;</td>
			        <td style="text-align:right;" colspan="2">&nbsp;</td>
		        </tr>
                <tr>
			        <td style="text-align:left;" colspan="2">&nbsp;</td>
			        <td style="text-align:right;" colspan="2">&nbsp;</td>
		        </tr>            
		        <tr>
			        <td style="text-align:left">
                        <b>Cashier/Clerk</b>
			                <br />
                        <asp:Label ID="lbl_usr" runat="server"></asp:Label>
		            </td>	
			        <td style="text-align:center">
			        
                    </td>		    
			        <td style="text-align:right">
                        <b>Terminal No</b>
			            <br />
                        <asp:Label ID="lbl_Terminal" runat="server" Text="" ></asp:Label>
                    </td>			  
		        </tr>
	        </table>
        <hr />       
        </div>
    </div>
    <!-- Optional JavaScript; choose one of the two! -->

    <!-- Option 1: jQuery and Bootstrap Bundle (includes Popper) -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>

    <!-- Option 2: jQuery, Popper.js, and Bootstrap JS
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js" integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s" crossorigin="anonymous"></script>
    -->
    </form>
</body>
</html>
