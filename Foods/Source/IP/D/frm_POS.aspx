<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_POS.aspx.cs" Inherits="Foods.Source.IP.frm_POS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POS</title>
    <style>
        .clear{
            clear:both;
        }
        h2{ 
	        text-align:center; width:100%; height:30px;	        
        }
        #container {
            width: 100%;
            height: 100%;
        }
        .left{
	        width:30%;
	        height:auto;
	        float:left;
        }
        .date{
	        width:40%;
	        height:auto;
	        float:left;
        }
        .time{
	        width:40%;
	        height:auto;
	        float:right;
	        text-align:right;
	        padding-right:20px;
        }
        .right {
            width: 69%;
            height: auto;
            float: right;
            border-left:1px solid #000;
        }
        .custyp
        {
	        width:83%;
	        height:auto;
	        float:left;

        }
        .billno
        {
	        width:15%;
	        height:auto;	
	        float:left;

        }
        .upper {
                width:100%;
                height:auto;
        
        }
        .mid {
                width:100%;
                height:auto;
        
        }
        .bot {
                width:100%;
                height:auto;
        
        }
        .gvitems
        {
	        width:100%;
	        height:auto;
	        
        }
        .gvitemsleft
        {
	        width:74.5%;
	        height:auto;
	        float:left;
	        

        }
        .gvitemsright
        {
	        width:25%;
	        height:500px;
	        float:right;
	        border-left:1px solid #000;
            border-top:1px solid #000;
        }
        .itmgrid
        {
	        width:100%;
	        height:auto;
	        text-align:center;
        }
        .completionList {

            border:solid 1px Gray;

            margin:0px;

            padding:3px;

            height: 120px;

            overflow:auto;

            background-color: #FFFFFF;     

        } 

        .listItem {

            color: #191919;

        } 

        .itemHighlighted {

            background-color: #ADD6FF;       

        }
        .footer {
            width:100%;
            height:60px;
            border-top:1px solid #000;
        }
        .salefoot {
           width:100%;
           height:auto;           
        }
        .salefoot_btn {
            float:right;
            margin-right:20px;
            margin-top:10px;
            }
</style>

    <!-- start: Mobile Specific border:1px solid #000; -->
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<!-- end: Mobile Specific -->
  
    	<!-- start: CSS -->
	<link href="../../Apps/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../Apps/css/bootstrap-responsive.min.css" rel="stylesheet" />
	<link href="../../Apps/css/style.css" rel="stylesheet" />
	<link href="../../Apps/css/style-responsive.css" rel="stylesheet" />
	<link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800&subset=latin,cyrillic-ext,latin-ext' rel='stylesheet' type='text/css' />
	<!-- end: CSS -->
	

	<!-- The HTML5 shim, for IE6-8 support of HTML5 elements -->
	<!--[if lt IE 9]>
	  	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
		<link href="Apps/css/ie.css" rel="stylesheet">
	<![endif]-->
	
	<!--[if IE 9]>
		<link href="Apps/css/ie9.css" rel="stylesheet">
	<![endif]-->
		
	<!-- start: Favicon -->
	<link rel="shortcut icon" href="img/favicon.ico" />
	<!-- end: Favicon -->
	
		

		    	<!-- start: JavaScript-->

		<script src="../../Apps/js/jquery-1.9.1.min.js"></script>
    
        <script src="../../Apps/js/jquery-migrate-1.0.0.min.js"></script>
	
		<script src="../../Apps/js/jquery-ui-1.10.0.custom.min.js"></script>
	
		<script src="../../Apps/js/jquery.ui.touch-punch.js"></script>
	
		<script src="../../Apps/js/modernizr.js"></script>
	
		<script src="../../Apps/js/bootstrap.min.js"></script>
	
		<script src="../../Apps/js/jquery.cookie.js"></script>
	
		<script src='../../Apps/js/fullcalendar.min.js'></script>
	
		<script src='../../Apps/js/jquery.dataTables.min.js'></script>

		<script src="../../Apps/js/excanvas.js"></script>

	    <script src="../../Apps/js/jquery.flot.js"></script>

	    <script src="../../Apps/js/jquery.flot.pie.js"></script>

	    <script src="../../Apps/js/jquery.flot.stack.js"></script>

	    <script src="../../Apps/js/jquery.flot.resize.min.js"></script>
	
		<script src="../../Apps/js/jquery.chosen.min.js"></script>
	
		<script src="../../Apps/js/jquery.uniform.min.js"></script>
		
		<script src="../../Apps/js/jquery.cleditor.min.js"></script>
	
		<script src="../../Apps/js/jquery.noty.js"></script>
	
		<script src="../../Apps/js/jquery.elfinder.min.js"></script>
	
		<script src="../../Apps/js/jquery.raty.min.js"></script>
	
		<script src="../../Apps/js/jquery.iphone.toggle.js"></script>
	
		<script src="../../Apps/js/jquery.uploadify-3.1.min.js"></script>
	
		<script src="../../Apps/js/jquery.gritter.min.js"></script>
	
		<script src="../../Apps/js/jquery.imagesloaded.js"></script>
	
		<script src="../../Apps/js/jquery.masonry.min.js"></script>
	
		<script src="../../Apps/js/jquery.knob.modified.js"></script>
	
		<script src="../../Apps/js/jquery.sparkline.min.js"></script>
	
		<script src="../../Apps/js/counter.js"></script>
	
		<script src="../../Apps/js/retina.js"></script>

		<script src="../../Apps/js/custom.js"></script>
	<!-- end: JavaScript-->
	
</head>
<body>
    <form id="form1" runat="server" defaultbutton="Button1">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="container">
            <div class="left">
                
                

	            <h1>Software Solutions</h1>
            </div>
            <div class="right">
	            <div class="date">
		            Date: <asp:Label ID="lbldat" runat="server"></asp:Label>
	            </div>
	            <div class="time">
		            Time: 
                    <asp:Label  ID="lbltim" runat="server"></asp:Label>
                    <input type="text" id="txtClock" runat="server" size="11" name="Clock" style="width:100px; height:20px; background:none; border:none;" />
                    <%--<asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                </asp:Timer>--%>
	            </div>
                <script language="javascript">
<!--
    /*By George Chiang (JK's JavaScript tutorial)
    http://javascriptkit.com
    Credit must stay intact for use*/
    function show() {
        var Digital = new Date()
        var hours = Digital.getHours()
        var minutes = Digital.getMinutes()
        var seconds = Digital.getSeconds()
        var dn = "AM"
        if (hours > 12) {
            dn = "PM"
            hours = hours - 12
        }
        if (hours == 0)
            hours = 12
        if (minutes <= 9)
            minutes = "0" + minutes
        if (seconds <= 9)
            seconds = "0" + seconds
        document.getElementById('txtClock').value = hours + ":" + minutes + ":"
        + seconds + " " + dn
        setTimeout("show()", 1000)
    }
    show()
    //-->
</script>
	            <div class="custyp">
		            Customer Type: <asp:DropDownList style="width:100%; height:30px;"  ID="ddl_custtyp" runat="server"></asp:DropDownList>
                    Customer Name: <asp:TextBox ID="TBCust" style="width:450px; height:25px;" runat="server" AutoPostBack="true" placeholder="Enter Customer Name..." OnTextChanged="TBCust_TextChanged" />
                     <asp:AutoCompleteExtender ServiceMethod="GetSearch" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBCust" ID="AutoCompleteExtender1"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
	            </div>
	            <div class="billno">
		            Bill NO: <asp:Label ID="lblbilno" runat="server" style="width:60px; height:20px;" Text="" ></asp:Label>
	            </div>
	            <div class="date">
		            User: <asp:Label ID="lblusr" runat="server" ></asp:Label>
	            </div>
	            <div class="time">
		            <asp:LinkButton ID="lblLogOut" runat="server"  OnClick="LinkBtnlogout_Click"><i class="halflings-icon off"></i>Logout</asp:LinkButton>
	            </div>
                <div class="date">
                    <asp:TextBox ID="TBPos" runat="server" AutoPostBack="true" placeholder="Enter Bill No..." OnTextChanged="TBPos_TextChanged" />
                     <asp:AutoCompleteExtender ServiceMethod="GetBill" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBPos" ID="AutoCompleteExtender3"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                </div>
            </div>		
            <div class="clear"></div>
        </div>	
        <div id="grid">           
            <div class="gvitemsleft">
                <asp:GridView ID="GV_POS" CssClass="gvitems" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"  runat="server" OnRowCommand="GV_POS_RowCommand" OnRowDeleting="GV_POS_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Items" >  
                            <ItemTemplate>
                                <asp:HiddenField ID="PROID" runat="server" Value='<%# Eval("ProductID")%>' /> 
                                <asp:TextBox ID="TBItms" runat="server" Text='<%# Eval("Items")%>' style="width:200px; height:30px;" placeholder="Enter Product Name" AutoPostBack="true" OnTextChanged="TBItms_TextChanged" ></asp:TextBox>                                                        
                                <asp:AutoCompleteExtender ServiceMethod="Getpro" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBItms" ID="AutoCompleteExtender2"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Price">
                            <ItemTemplate>
                                <asp:Label ID="lblItmpris" runat="server" Text='<%# Eval("Itempric")%>'  ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                   
                        <asp:TemplateField HeaderText="QTY">
                            <ItemTemplate>
                                <asp:TextBox ID="TBItmQty" runat="server" Text='<%# Eval("QTY")%>' style="width:200px; height:30px;" AutoPostBack="true" OnTextChanged="TBItmQty_TextChanged" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVQty" ForeColor="Red" ValidationGroup="gvItems" runat="server" ErrorMessage="Please Write Some in Quantity" ControlToValidate="TBItmQty"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" >
                            <ItemTemplate>       
                                <asp:HiddenField ID="PROCATID" runat="server" Value='<%# Eval("ProductTypeID")%>' /> 
                                <asp:Label ID="lblcat" runat="server" Text='<%# Eval("Itemcat")%>'  ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>    
                                <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false" />
                                <asp:Label ID="lblttl" runat="server" Text='<%# Eval("TTL")%>'  ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>     
                        <asp:TemplateField>                                                
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnadd" ValidationGroup="gvItems" CommandName="Add"  OnClick="linkbtnadd_Click" runat="server"><i class="halflings-icon plus-sign" ></i></asp:LinkButton>
                                <asp:HiddenField ID="HFDSal" runat="server" Value='<%# Eval("Dposid")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>                                     
                        <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                            <ControlStyle CssClass="halflings-icon minus-sign" />
                        </asp:CommandField>

                    </Columns>
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate> 
                </asp:GridView>
                 <div class="clear"></div>
            <div class="salefoot">
                <div class="salefoot_btn">
                    <asp:HiddenField ID="POSID" runat="server" />
                    <b>Total:</b>&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" class="span2" ID="TBTtl" Text="0.00"></asp:TextBox>
                    <br />
                    <asp:Button Id="Button1" runat="server" Text="Save" CssClass="btn" OnClick="Button1_Click"></asp:Button>
                    <asp:Button Id="btnclear" runat="server" Text="Clear" CssClass="btn" OnClick="btnclear_Click"></asp:Button>

                    <div class="clear"></div>
                </div>
            </div>
            </div>
            <div class="gvitemsright">
		        
	        </div>
        </div>
            <div class="clear"></div>
            <div class="footer">
                Prepared By Software Solutions(Pvt) Ltd
            </div>
            </ContentTemplate>
            
            </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
