<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GV_DSR.aspx.cs" Inherits="Foods.GV_DSR" EnableEventValidation = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Daily Sales Report</title>  
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

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
        <div class="container">
	      <div class="row">
		    <div class="col">
                <asp:Image ID="Imglogo" runat="server" Width="100" Height="100" />
		      <h3><asp:Label ID="lblcom" runat="server"></asp:Label> </h3>
		      <h5></h5>
		      <div class="row">&nbsp;</div>
		      <div class="left">
			      <p><b>Address</b></p>
			      <p><asp:Label ID="lbl_brnam" runat="server" Font-Bold="true"></asp:Label></p>
			      <p><asp:Label ID="lbl_Add" runat="server"></asp:Label></p>
			      <p>Phone: <asp:Label ID="lbl_Ph" runat="server"></asp:Label> Mobile: <asp:Label ID="lbl_Mob" runat="server"></asp:Label></p>
		      </div>
		      <div class="row">&nbsp;</div>
		      <button class="btn-info" onclick="printDiv('printMe')">Print</button>
		    </div>
		    <div class="col">
		      &nbsp;
		    </div>
		    <div class="col">
		      <div class="right">
			      <h2><i>Daily Sales Report</i></h2>
			      <div class="row">&nbsp;</div>
			      <p><asp:Label ID="lbldat" runat="server" Text="Date:" Font-Bold="true" /><asp:Label ID="lbl_dat" runat="server" /></p>			     
		      </div>
		      <div class="row">&nbsp;</div>
		      <h5>Export to Excel</h5>
		      <asp:LinkButton ID="LinkBtnExportExcel" runat="server" OnClick="LinkBtnExportExcel_Click">Convert to Excel</asp:LinkButton>       
		    </div>
	      </div>
	      <div class="row">&nbsp;</div>
	      <div class="row"><b>Comment or Special Instructions:</b> None</div>
	      <div class="row">&nbsp;</div>	  
	      <div class="row">
		    <div class="col">
                <div id="printMe">
                    <asp:GridView ID="GVDSR" runat="server" AutoGenerateColumns="false" class="gv" ShowHeader="true" CssClass="table table-bordered table-sm" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="GVDSR_RowDataBound">
                        <Columns>                            
                            <asp:TemplateField HeaderText="Reciever Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_rec" runat="server" Text='<%# Eval("RecieverNam")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reciept No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbl_pro" runat="server" Text='<%# Eval("BillNo")%>' OnClientClick='<%# "return NewWindow(" +Eval("BillNo") + " );" %>' ></asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amt" runat="server" Text='<%# Eval("Amt")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblsh2" runat="server" Text="Total:" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lblttlamt" runat="server" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>                      
                            <asp:TemplateField HeaderText="Amount Recieve">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amts" runat="server" Text='<%# Eval("Adv")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblsh" runat="server" Text="Total:" />&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_ttlamts" runat="server" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div> 			    
		    </div>
	      </div>
	      <div class="row">&nbsp;</div>
	      <div class="row">&nbsp;</div>	  
	    </div>        
    </form>
    <!-- Optional JavaScript; choose one of the two! -->
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
        function NewWindow(billNo) {
            //document.forms[0].target = '_blank';
            window.open('Reports/rpt_POS.aspx?ID=POS&HOLD=0&POSID=' + billNo + '', '_blank', 'height=600px,width=1000px,scrollbars=1');
        }
	</script>

    <!-- Option 1: jQuery and Bootstrap Bundle (includes Popper) -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>

    <!-- Option 2: jQuery, Popper.js, and Bootstrap JS
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js" integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s" crossorigin="anonymous"></script>
    -->
</body>
</html>
