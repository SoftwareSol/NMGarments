<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_Chkcust.aspx.cs" Inherits="Foods.frm_Chkcust" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- start: Meta -->
	<meta charset="utf-8" />
	<title>Eye Vision</title>
    <!-- start: Favicon -->
	<link rel="shortcut icon" href="../../../img/eye_icon.ico" />
	<!-- end: Favicon -->

	<meta name="description" content="Bootstrap Metro Dashboard" />
	<meta name="author" content="Dennis Ji" />
	<meta name="keyword" content="Metro, Metro UI, Dashboard, Bootstrap, Admin, Template, Theme, Responsive, Fluid, Retina" />
	<!-- end: Meta -->
	
	<!-- start: Mobile Specific -->
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<!-- end: Mobile Specific -->
	
	<!-- start: CSS -->
	<link href="../../../Apps/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../../Apps/css/bootstrap-responsive.min.css" rel="stylesheet" />
	<link href="../../../Apps/css/style.css" rel="stylesheet" />
	<link href="../../../Apps/css/style-responsive.css" rel="stylesheet" />
	<link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800&subset=latin,cyrillic-ext,latin-ext' rel='stylesheet' type='text/css' />
	<!-- end: CSS -->
		
	<!-- start: Favicon -->
	<link rel="shortcut icon" href="../../../img/favicon.ico" />
	<!-- end: Favicon -->
        <style type="text/css">
            /* Scroller Start */
        .scrollit {
            overflow:scroll;
            height:100%;
	        width:100%;           
	        margin:0px auto;
        }

      /* Scroller End */
      /* Modal SalespUp Start */

        .modalBackground{
                background-color: #000000;
                filter: alpha(opacity=10);
                opacity: 0.7;
        }
         .modalBackgroundSupplier {
                 background-color: #000000;
                filter: alpha(opacity=10);
                opacity: 0.7;
         }
        .modalBackground1{
                width: 500px;
                height: 500px;
                background-color: #000000;
                filter: alpha(opacity=10);
                opacity: 0.6;
        }
        .modalSalespup{
                border: 3px solid #000000;
                background-color: #FFFFFF;
                margin-top: 0px;
                color: #000000;
                margin-right: -3px;
                margin-bottom: 0px;
        }

        .modalSalespup1{
                width:202px;
                height:140px;
                border: 3px solid #000000;
                background-color: #FFFFFF;
                color: #FF0000;
                margin-right: -3px;
                text-align: center;
                margin-left: -20px;
                margin-top: -80px;
        }
        .closebtn {
                float:right;
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

        /* Modal SalespUp End */
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- start: Header -->
	    <div class="navbar">
		    <div class="navbar-inner">
			    <div class="container-fluid">
				    <a class="btn btn-navbar" data-toggle="collapse" data-target=".top-nav.nav-collapse,.sidebar-nav.nav-collapse">
					    <span class="icon-bar"></span>
					    <span class="icon-bar"></span>
					    <span class="icon-bar"></span>
				    </a>
				    <a class="brand" href="frm_PSal.aspx"><span>Eye Vision</span></a>								
				    <!-- start: Header Menu -->
				    <div class="nav-no-collapse header-nav">
					    <ul class="nav pull-right">						
						    <!-- start: User Dropdown -->
						    <li class="dropdown">
							    <a class="btn dropdown-toggle" data-toggle="dropdown" href="#">
								    <i class="halflings-icon white user"></i> <asp:Label ID="lbl_usr" runat="server"></asp:Label>
								    <span class="caret"></span>
							    </a>
							    <ul class="dropdown-menu">
								    <li class="dropdown-menu-title">
 									    <span>Account Settings</span>
								    </li>
								    <li><a href="#">Profile</a></li>
								    <li><asp:LinkButton ID="lnkbtn_Logout" runat="server" OnClick="lnkbtn_Logout_Click">Logout</asp:LinkButton></li>
							    </ul>
						    </li>
						    <!-- end: User Dropdown -->
					    </ul>
				    </div>
				    <!-- end: Header Menu -->
			    </div>
		    </div>
	    </div>
	    <!-- start: Header -->
        <div class="container-fluid-full">
		    <div class="row-fluid">
			
			    <noscript>
				    <div class="alert alert-block span12">
					    <h4 class="alert-heading">Warning!</h4>
					    <p>You need to have <a href="http://en.wikipedia.org/wiki/JavaScript" target="_blank">JavaScript</a> enabled to use this site.</p>
				    </div>
			    </noscript>
			
			    <!-- start: Content -->
			    <div  class="span12">
			        <br />			
			       <ul class="breadcrumb">
				        <li>
					        <i class="icon-home"></i>
					        <a href="WellCome.aspx">Home</a>
					        <i class="icon-angle-right"></i> 
				        </li>
				        <li>
					        <i class="icon-edit"></i>
					        <a href="frm_PSal.aspx">Point of Sales</a>
                            <i class="icon-angle-right"></i> 
				        </li>
                       	<li>
					        <i class="icon-edit"></i>
					        <a href="frm_ItemTyp.aspx">Type of Items</a>
                            <i class="icon-angle-right"></i> 
				        </li>
                        <li>
					        <i class="icon-edit"></i>
					        <a href="frm_Payments.aspx">Payments</a>
                            <i class="icon-angle-right"></i> 
				        </li>

				        <li>
					        <i class="icon-edit"></i>
					        <a href="frm_Chkcust.aspx">Check Customers</a>
                            <i class="icon-angle-right"></i> 
				        </li>
				        <li>
					        <i class="icon-edit"></i>
					        <a href="Reporting.aspx">Reporting</a>
                            <i class="icon-angle-right"></i> 
				        </li>
                        <li>
					        <i class="icon-edit"></i>
					        <a href="frm_ChangePass.aspx">Change Password</a>
				        </li>
			        </ul>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>			
			            <div class="row-fluid sortable">
				            <div class="box span12">
					            <div class="box-header" data-original-title>
						            <div class="box-icon">
						            </div>
                                    <p>Customer Details</p>
					            </div>
					            <div class="box-content">
                                    <div class="control-group span12">
                                        <label class="control-label" for="TB_SearchCust">Enter Mobile NO: </label>
                                        <div class="controls">
                                            <asp:TextBox ID="TB_SearchCust" runat="server" placeholder="Enter Mobile Number..." AutoPostBack="true" OnTextChanged="TB_SearchCust_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ServiceMethod="GetCustMob" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TB_SearchCust" ID="AutoCompleteExtender3"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                        </div>
                                    </div>
						            <div class="span12"><asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label></div>					         
							        <fieldset>
                                        <div class="scrollit">
                                            <asp:GridView ID="GVSerachCust" ShowHeader="true" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="BillNO" runat="server" OnRowCommand="GVSerachCust_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID"  SortExpression="ID" />
                                                    <asp:BoundField DataField="BillNO" HeaderText="BILL NO"  SortExpression="BillNO" />
                                                    <asp:BoundField DataField="CustomerName" HeaderText="CUSTOMER NAME"  SortExpression="CustomerName" />
                                                    <asp:BoundField DataField="Items" HeaderText="PRODUCT NAME" SortExpression="Items" />                                                                                    
                                                    <asp:BoundField DataField="CellNo1" HeaderText="MOBILE NUMBER"  SortExpression="CellNo1" />                                                    
                                                    <asp:BoundField DataField="billdat" HeaderText="BILL DATE" SortExpression="billdat" />                                                                                    
                                                    <asp:BoundField DataField="billtim" HeaderText="BILL TIME"  SortExpression="billtim" />                                                   
                                                    <asp:BoundField DataField="createdby" HeaderText="CREATED BY" SortExpression="createdby" />                                                                                    
                                                    <asp:BoundField DataField="createdat" HeaderText="CREATED AT" SortExpression="createdat" />                                                                                    
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtndel" runat="server" ForeColor="Red" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnshow" CommandName="Select" runat="server" Text="Reciept"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate> 
                                            </asp:GridView>
                                        </div>
						            </fieldset>
					            </div>
				            </div>
                            <!--/span-->
			            </div>	
                    </ContentTemplate>
                </asp:UpdatePanel>

			        <div class="span12">&nbsp;</div><!--/row-->
			        <div class="span12">&nbsp;</div><!--/row-->
	            </div><!--/.fluid-container-->
			    <!-- end: Content -->
		    </div><!--/#content.span10-->
		</div><!--/fluid-row-->
                   
    <div class="clearfix"></div>

        <footer>
		    <p>
                <span style="text-align:left;float:left">&copy; 2019 <a href="#" alt="View Point">Powered By Software Solutions</a></span>
		    </p>
	    </footer>    
    </div>
    </form>
	<!-- start: JavaScript-->

		<script src="../../../Apps/js/jquery-1.9.1.min.js"></script>
    	<script src="../../../Apps/js/jquery-migrate-1.0.0.min.js"></script>
	
		<script src="../../../Apps/js/jquery-ui-1.10.0.custom.min.js"></script>
	
		<script src="../../../Apps/js/jquery.ui.touch-punch.js"></script>
	
		<script src="../../../Apps/js/modernizr.js"></script>
	
		<script src="../../../Apps/js/bootstrap.min.js"></script>
	
		<script src="../../../Apps/js/jquery.cookie.js"></script>
	
		<script src='../../../Apps/js/fullcalendar.min.js'></script>
	
		<script src='../../../Apps/js/jquery.dataTables.min.js'></script>

		<script src="../../../Apps/js/excanvas.js"></script>
	    <script src="../../../Apps/js/jquery.flot.js"></script>
	    <script src="../../../Apps/js/jquery.flot.pie.js"></script>
	    <script src="../../../Apps/js/jquery.flot.stack.js"></script>
	    <script src="../../../Apps/js/jquery.flot.resize.min.js"></script>
	
		<script src="../../../Apps/js/jquery.chosen.min.js"></script>
	
		<script src="../../../Apps/js/jquery.chosen.min.js"></script>
		<script src="../../../Apps/js/jquery.uniform.min.js"></script>
		
		<script src="../../../Apps/js/jquery.cleditor.min.js"></script>
	
		<script src="../../../Apps/js/jquery.noty.js"></script>
	
		<script src="../../../Apps/js/jquery.elfinder.min.js"></script>
	
		<script src="../../../Apps/js/jquery.raty.min.js"></script>
	
		<script src="../../../Apps/js/jquery.iphone.toggle.js"></script>
	
		<script src="../../../Apps/js/jquery.uploadify-3.1.min.js"></script>
	
		<script src="../../../Apps/js/jquery.gritter.min.js"></script>
	
		<script src="../../../Apps/js/jquery.imagesloaded.js"></script>
	
		<script src="../../../Apps/js/jquery.masonry.min.js"></script>
	
		<script src="../../../Apps/js/jquery.knob.modified.js"></script>
	
		<script src="../../../Apps/js/jquery.sparkline.min.js"></script>
	
		<script src="../../../Apps/js/counter.js"></script>
	
		<script src="../../../Apps/js/retina.js"></script>

		<script src="../../../Apps/js/custom.js"></script>

	<!-- end: JavaScript-->
	
</body>
</html>
