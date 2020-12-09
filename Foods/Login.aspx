<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Foods.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Eximma - Login</title>
    	<!-- start: Mobile Specific -->
	<meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, user-scalable=no"/>

	<!-- end: Mobile Specific -->
	
	<!-- start: CSS -->
	<link href="Apps/css/bootstrap.min.css" rel="stylesheet" />
	<link href="Apps/css/bootstrap-responsive.min.css" rel="stylesheet" />
	<link href="Apps/css/style.css" rel="stylesheet" />
	<link href="Apps/css/style-responsive.css" rel="stylesheet" />
	<link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800&subset=latin,cyrillic-ext,latin-ext' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="Apps/Dev/fv.css" type="text/css" />
	<!-- end: CSS -->
	

	<!-- The HTML5 shim, for IE6-8 support of HTML5 elements -->
	<!--[if lt IE 9]>
	  	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
		<link  href="css/ie.css" rel="stylesheet">
	<![endif]-->
	
	<!--[if IE 9]>
		<link href="css/ie9.css" rel="stylesheet">
	<![endif]-->
		
	<!-- start: Favicon -->
	<link rel="shortcut icon" href="img/Softsol_logo.png" />
	<!-- end: Favicon -->
	
			<style type="text/css">
             body { 
                  background: url(img/bg-login.jpeg) no-repeat center center fixed; 
                  -webkit-background-size: cover;
                  -moz-background-size: cover;
                  -o-background-size: cover;
                  background-size: cover;
                }			
		</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container-fluid-full">
		    <div class="row-fluid">
                <asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="width:100%; height:auto; float:left;">
                            <div class="row-fluid">
				            <div class="login-box">
                                <center><img src="img/Softsol_logo.png" alt="SOfwaretsol logo" height="100" width="100" />
                                <h1>EXIMMA</h1></center>
                                <br />
					            <div class="icons">
						            <%--<a href="Source/IO/DashBoard.aspx"><i class="halflings-icon home"></i></a>
						            <a href="#"><i class="halflings-icon cog"></i></a>--%>
					            </div>
					            <h2>Login to your account</h2>
					            <div class="form-horizontal">
						            <fieldset>
							            <div class="input-prepend" title="Username">
                                            <div class="item">
								                <span class="add-on"><i class="halflings-icon user"></i></span>
                                         <asp:DropDownList ID="ddl_brnch" CssClass="form-control"  data-rel="chosen" runat="server" OnSelectedIndexChanged="ddl_com_SelectedIndexChanged"></asp:DropDownList>             
                                            </div>
							            </div>
							            <div class="input-prepend" title="Username">
                                            <div class="item">
								                <span class="add-on"><i class="halflings-icon user"></i></span>
                                                 <asp:TextBox ID="txtuser" runat="server" CssClass="input-large span10" name="username" placeholder="type username" ></asp:TextBox>
                                            </div>
							            </div>
							            <div class="input-prepend" title="Password">
                                            <div class="item">
								                <span class="add-on"><i class="halflings-icon lock"></i></span>
                                                <asp:TextBox ID="password" runat="server" CssClass="input-large span10" name="username"   placeholder="type Password" TextMode="Password" ></asp:TextBox>
                                            </div>
							            </div>
                                        <asp:Panel ID="pnl_pass" runat="server">
							                <label class="remember" for="remember"><input type="checkbox" id="remember" />Forget Passsword ??</label>
                                        </asp:Panel>
							            <div class="button-login">
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-primary" OnClick="btnLogin_Click" />
							            </div>
							            <div class="clearfix"></div>
                                        <br />
                                        <div style="width:100%; height:auto; margin:0px 0px 15px 0px; font-size:12px; text-align:center;">
                                            Version 1.0 &nbsp;&nbsp;
                                            Powered by: Software Solutions.
                                            <br />
                                            Last Updated On: 09-12-2020
                                        </div>
					            </div>
				            </div><!--/span-->
			            </div>
                        </div><!--/row-->                        
                    </ContentTemplate>
                </asp:UpdatePanel>
	        </div><!--/.fluid-container-->	
		</div><!--/fluid-row-->	

    <div id="ModalAlert" class="modal fade" style="display:none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Error!</h4>
                </div>
                <!-- dialog body -->
                <div class="modal-body">
                    <asp:Label ID="lblalert" runat="server"></asp:Label>                
                </div>
                <!-- dialog buttons -->
                <div class="modal-footer">
                    <asp:LinkButton ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" ></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
	<!-- start: JavaScript-->

		<script src="Apps/js/jquery-1.9.1.min.js"></script>
	    
        <script src="Apps/js/jquery-migrate-1.0.0.min.js"></script>
	
		<script src="Apps/js/jquery-ui-1.10.0.custom.min.js"></script>
	
		<script src="Apps/js/jquery.ui.touch-punch.js"></script>
	
		<script src="Apps/js/modernizr.js"></script>
	
		<script src="Apps/js/bootstrap.min.js"></script>
	
		<script src="Apps/js/jquery.cookie.js"></script>
	
		<script src='Apps/js/fullcalendar.min.js'></script>
	
		<script src='Apps/js/jquery.dataTables.min.js'></script>

		<script src="Apps/js/excanvas.js"></script>

	    <script src="Apps/js/jquery.flot.js"></script>

	    <script src="Apps/js/jquery.flot.pie.js"></script>

	    <script src="Apps/js/jquery.flot.stack.js"></script>

	    <script src="Apps/js/jquery.flot.resize.min.js"></script>
	
		<script src="Apps/js/jquery.chosen.min.js"></script>
	
		<script src="Apps/js/jquery.uniform.min.js"></script>
		
		<script src="Apps/js/jquery.cleditor.min.js"></script>
	
		<script src="Apps/js/jquery.noty.js"></script>
	
		<script src="Apps/js/jquery.elfinder.min.js"></script>
	
		<script src="Apps/js/jquery.raty.min.js"></script>
	
		<script src="Apps/js/jquery.iphone.toggle.js"></script>
	
		<script src="Apps/js/jquery.uploadify-3.1.min.js"></script>
	
		<script src="Apps/js/jquery.gritter.min.js"></script>
	
		<script src="Apps/js/jquery.imagesloaded.js"></script>
	
		<script src="Apps/js/jquery.masonry.min.js"></script>
	
		<script src="Apps/js/jquery.knob.modified.js"></script>
	
		<script src="Apps/js/jquery.sparkline.min.js"></script>
	
		<script src="Apps/js/counter.js"></script>
	
		<script src="Apps/js/retina.js"></script>

		<script src="Apps/js/custom.js"></script>
	<!-- end: JavaScript-->
    </div>
    </form>
</body>
</html>
