<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporting.aspx.cs" Inherits="Foods.Reporting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Eye Vision: Reporting</title>
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
            height:auto ;
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
 
			        <div class="row-fluid ">
				        <div class="box span12">
					        <div class="box-content">
							    <fieldset>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>                                            
                                               <div class="row-fluid sortable">
				                                    <div class="box span12">
					                                    <div class="box-header">
						                                    <h2><i class="halflings-icon th"></i><span class="break"></span>Select Report Type </h2>
					                                    </div>
					                                    <div class="box-content">
                                                                <asp:Label ID="lbl_mssg" runat="server" ForeColor="Red"></asp:Label>
                                                                <asp:DropDownList ID="ddl_rpttyp" runat="server"></asp:DropDownList>                                                           
    						                                    <asp:LinkButton ID="lnlDSR" runat="server" CssClass="quick-button metro yellow span2"  OnClick="lnlDSR_Click">
                                                                    <i class="icon-group"></i>
                                                				    <p>Daily Sales</p>
							                                    </asp:LinkButton>
							                                    <asp:LinkButton ID="lnkbtnCreditReports" CssClass="quick-button metro red span2" runat="server"  OnClick="lnkbtnCreditReports_Click" >
                                                                    <i class="icon-comments-alt"></i>
					                                                <p>Credit Reports</p>
							                                    </asp:LinkButton></a>
							                                    <asp:LinkButton ID="lnkbtnprofshet" CssClass="quick-button metro green span2" runat="server" OnClick="lnkbtnprofshet_Click" >
                                                                    <i class="icon-barcode"></i>
                                                					<p>Profit Sheets</p>
							                                    </asp:LinkButton>
                                                                <div class="span12">&nbsp;</div>
                                                                <div class="span12">&nbsp;</div>
                                                                <div class="span12">&nbsp;</div>

						                                        <asp:Panel ID="DailySales" runat="server">
                                                                        <div class="span12">
                                                                            <label class="span2">
                                                                                <span>Date Wise</span>
                                                                            </label>
                                                                            <div class="span10">
                                                                                <div class="span5">
                                                                                    <label class="span2">
                                                                                        <span>From Date</span>
                                                                                    </label>
                                                                                <asp:TextBox ID="TBFDWise" runat="server" placeholder="ex. 12/10/2015..." />
                                                                                <asp:CalendarExtender ID="CETBFDWise" PopupButtonID="imgPopup" runat="server" TargetControlID="TBFDWise" Format="dd/MM/yyyy" />
                                                                                </div>
                                                                                <div class="span5">
                                                                                    <label class="span2">
                                                                                        <span>To Date</span>
                                                                                    </label>
                                                                                <asp:TextBox ID="TBTDWise" runat="server" placeholder="ex. 12/10/2015..." />
                                                                                <asp:CalendarExtender ID="CETBTDWise" PopupButtonID="imgPopup" runat="server" TargetControlID="TBTDWise" Format="dd/MM/yyyy" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
						                                        </asp:Panel>
						                                        <asp:Panel ID="Credit" runat="server">
								                                    <div class="span12">
                                                                        <label class="span2">
                                                                            <span>Mobile Number</span>
                                                                        </label>
                                                                        <div class="span10">
                                                                            <div class="span5">
                                                                                <label class="span2">
                                                                                    <span></span>
                                                                                </label>
                                                                            <asp:TextBox ID="TBMobNo" runat="server" Height="18px" ValidationGroup="grp" Width="142px" placeholder="Enter Mobile No.."></asp:TextBox>
                                                                            <asp:AutoCompleteExtender ServiceMethod="GetCustMob" OnClientShown="PopupShown" CompletionListCssClass="completionList"
                                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBMobNo" ID="AutoCompleteExtender2"  
                                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
							                                    </asp:Panel>
							                                    <asp:Panel ID="Profit" runat="server">
								                                    <div class="span12">
                                                                        <label class="span2">
                                                                        <span>Date Wise</span>
                                                                        </label>
                                                                        <div class="span10">
                                                                            <div class="span5">
                                                                                <label class="span2">
                                                                                    <span>From Date</span>
                                                                                </label>
                                                                                <asp:TextBox ID="TBPFrmdat" runat="server" placeholder="ex. 12/10/2015..."  />
                                                                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="TBPFrmdat" Format="dd/MM/yyyy" />
                                                                            </div>
                                                                            <div class="span5">
                                                                                <label class="span2">
                                                                                    <span>To Date</span>
                                                                                </label>
                                                                                <asp:TextBox ID="TBPTrmdat" runat="server" placeholder="ex. 12/10/2015..." />
                                                                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup" runat="server" TargetControlID="TBPTrmdat" Format="dd/MM/yyyy" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
							                                    </asp:Panel>						                                    
					                                    </div>
				                                    </div><!--/span-->			
			                                    </div><!--/row-->			                                                
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
						        </fieldset>
						        <div class="form-actions" style=" text-align:right; padding-right:11%;">
                                                   
                                    <asp:Button ID="btn_View" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btn_View_Click" />
						        </div>
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
                            <br />
                            <br />                            
				        </div>
                        <!--/span-->
			        </div>	
	            </div><!--/.fluid-container-->
			    <!-- end: Content -->
		    </div><!--/#content.span10-->
		</div><!--/fluid-row-->
	    <div class="clearfix"></div>
        <footer>
		    <p>
                <span style="text-align:left;float:left">&copy; 2019 <a href="#" alt="Eye Vision">Powered By Software Solutions</a></span>
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
    <script>
        function PopupShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 99999999;
        }
   </script>
</body>
</html>
