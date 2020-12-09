<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_ChangePass.aspx.cs" Inherits="Foods.frm_ChangePass" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<!-- start: Meta -->
	<meta charset="utf-8" />
	<title>Eye Vision</title>
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
			
			        <div class="row-fluid sortable">
				        <div class="box span12">
					        <div class="box-header" data-original-title>
						        <div class="box-icon">
						        </div>
					        </div>
					        <div class="box-content">
						        
                                <div class="span8">
                                <div class="control-group  span12">							
							        <div class="controls">      
                                        <div class="span12">
                                            <asp:Label ID="lblerr" runat="server" style="color:red;"></asp:Label>
                                            <br />
                                            <div class="item">
						                        <label>
							                        <span>Enter New password</span>
						                        </label>
                                                <asp:TextBox ID="TBPass" TextMode="Password" runat="server" placeholder="******" required CssClass="input-large span5" ></asp:TextBox>
                                            </div>
                                       </div>                          
                                        <div class="span12">
                                            <div class="item">
                                               <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
					                        </div>
                                       </div>                                                                  
							        </div>
				                </div>
                             </div>
					        </div>
				        </div><!--/span-->

			        </div><!--/row-->			
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
                            <br />
                            <br />			
			        <!--/row-->
			        <div class="span12">&nbsp;</div><!--/row-->
			        <div class="span12">&nbsp;</div><!--/row-->
	            </div><!--/.fluid-container-->
	
			    <!-- end: Content -->
		    </div><!--/#content.span10-->
		</div><!--/fluid-row-->
		
            <div id="ModalAlert" class="modal fade" style="display:none;">
			  <div class="modal-dialog">
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
						<h4 class="modal-title">Error!</h4>
					</div>
					<!-- dialog body -->
					<div class="modal-body">
					 <asp:Label ID="lblalert" runat="server"></asp:Label>
					</div>
				  <!-- dialog buttons -->
				</div>
			  </div>
			</div>
    


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
     <script type="text/javascript">

         var Alert = function () {

             $("#ModalAlert").on("show", function () {    // wire up the OK button to dismiss the modal when shown

                 $("#ModalAlert a.btn").on("click", function (e) {
                     console.log("button pressed");   // just as an example...
                     $("#ModalAlert").modal('hide');     // dismiss the dialog
                 });

             });

             $("#ModalAlert").on("hide", function () {    // remove the event listeners when the dialog is dismissed
                 $("#ModalAlert a.btn").off("click");
             });

             $("#ModalAlert").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
                 $("#ModalAlert").remove();
             });

             $("#ModalAlert").modal({                    // wire up the actual modal functionality and show the dialog
                 "backdrop": "static",
                 "keyboard": true,
                 "show": true                     // ensure the modal is shown immediately
             });
         }

         this.$('.js-loading-bar').modal({
             backdrop: 'static',
             show: false
         });

         $('#Loader').click(function () {

             var $modal = $('#loading-indicator'),
                 $bar = $modal.find('.box-danger');
             $("#myModal").modal('hide');



             $modal.modal('show');
             $bar.addClass('animate');

             setTimeout(function () {
                 $bar.removeClass('animate');
                 $modal.modal('hide');

             }, 3000);
         });


         var ModalPopUp = function () {

             $("#myModal").on("show", function () {    // wire up the OK button to dismiss the modal when shown

                 $("#myModal a.btn").on("click", function (e) {
                     console.log("button pressed");   // just as an example...
                     $("#myModal").modal('hide');     // dismiss the dialog
                 });

             });

             $("#myModal").on("hide", function () {    // remove the event listeners when the dialog is dismissed
                 $("#myModal a.btn").off("click");
             });

             $("#myModal").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
                 $("#myModal").remove();
             });

             $("#myModal").modal({                    // wire up the actual modal functionality and show the dialog
                 "backdrop": "static",
                 "keyboard": true,
                 "show": true                     // ensure the modal is shown immediately
             });
         }

         var DelPopUp = function () {

             $("#DelModal").on("show", function () {    // wire up the OK button to dismiss the modal when shown
                 $("#DelModal a.btn").on("click", function (e) {
                     console.log("button pressed");   // just as an example...
                     $("#DelModal").modal('hide');     // dismiss the dialog
                 });
             });

             $("#DelModal").on("hide", function () {    // remove the event listeners when the dialog is dismissed
                 $("#DelModal a.btn").off("click");
             });

             $("#DelModal").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
                 $("#DelModal").remove();
             });

             $("#DelModal").modal({                    // wire up the actual modal functionality and show the dialog
                 "backdrop": "static",
                 "keyboard": true,
                 "show": true                     // ensure the modal is shown immediately
             });
         }
     </script>
</html>
