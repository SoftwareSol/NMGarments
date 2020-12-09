<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_PSal_.aspx.cs" Inherits="Foods.frm_PSal_" %>
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
                    <div class="row-fluid sortable">
				        <div class="box span12">
					        <div class="box-content">
                                <div class="span12">
                                    <asp:Label ID="lblmssg" runat="server" ForeColor="Red" ></asp:Label>
                                </div>
						        <div class="span3">
                                    <asp:LinkButton ID="btn_Cust" runat="server" CssClass="quick-button span6" >							
                                        <i class="icon-group"></i>
                                        <p>Add New Customer</p>
                                    </asp:LinkButton>
					            </div>
						        <div class="span3">Bill No: <asp:Label ID="lbl_BillNo" runat="server" Text=""></asp:Label></div>
						        <div class="span3">Date: <asp:Label ID="lbl_dat" runat="server" Text=""></asp:Label></div>
						        <div class="span2" style="text-align:right;">
							        Time:  <input type="text" id="txtClock" runat="server"  name="Clock" class="span8" style="background:none; border:none;" />
						        </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="control-group span3">
                                            <label class="control-label" for="TBCust">Customer: </label>
                                            <div class="controls">
                                                <asp:TextBox ID="TBCust"  runat="server" AutoPostBack="true" ValidationGroup="val_Psal" placeholder="Enter Customer Name..." OnTextChanged="TBCust_TextChanged" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="val_Psal" Font-Size="Smaller" runat="server" ControlToValidate="TBCust" ForeColor="Red" ErrorMessage="* Customer Name is Required!.."></asp:RequiredFieldValidator>
                                                <asp:AutoCompleteExtender ServiceMethod="GetSearch" CompletionListCssClass="completionList"
                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBCust" ID="AutoCompleteExtender1"  
                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="control-group span3">
                                            <label class="control-label" for="TB_MobNo">Mobile No: </label>
                                            <div class="controls">
                                                <asp:TextBox ID="TB_MobNo" runat="server"  placeholder="Enter Mobile Number..." AutoPostBack="true" OnTextChanged="TB_MobNo_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="val_Psal" Font-Size="Smaller" runat="server" ControlToValidate="TB_MobNo" ForeColor="Red" ErrorMessage="* Customer Mobile No. is Required!.."></asp:RequiredFieldValidator>
                                                <asp:AutoCompleteExtender ServiceMethod="GetCustMob" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TB_MobNo" ID="AutoCompleteExtender3"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="control-group span4">
                                            <label class="control-label" for="ckh_right">Right Eye: </label>
                                            <div class="controls">
                                                <asp:CheckBox ID="ckh_right" runat="server"  />
                                                <asp:TextBox ID="TBRCyl" runat="server" CssClass="span3"  placeholder="Enter Cylendrical..."></asp:TextBox>
                                                <asp:TextBox ID="TBRSph" runat="server" CssClass="span3"  placeholder="Enter Sphere..."></asp:TextBox>
                                                <asp:TextBox ID="TBRAxis" runat="server" CssClass="span3" placeholder="Enter Axis..."></asp:TextBox>
                                                <asp:TextBox ID="TBRAdd_" runat="server" CssClass="span2"  placeholder="Add..."></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="control-group span4">
                                            <label class="control-label" for="chk_left">Left Eye: </label>
                                            <div class="controls">
                                                <asp:CheckBox ID="chk_left" runat="server" />
                                                <asp:TextBox ID="TBLCyl" runat="server" CssClass="span3"  placeholder="Enter Cylendrical..."></asp:TextBox>
                                                <asp:TextBox ID="TBLSph" runat="server" CssClass="span3"  placeholder="Enter Sphere..."></asp:TextBox>
                                                <asp:TextBox ID="TBLAxis" runat="server" CssClass="span3" placeholder="Enter Axis..."></asp:TextBox>
                                                <asp:TextBox ID="TBLAdd_" runat="server" CssClass="span2" placeholder="Add..."></asp:TextBox>
                                            </div>
                                        </div>
                                            <fieldset>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="scrollit">
                                                            <asp:GridView ID="GV_POS" ShowHeader="true" CssClass="table table-striped table-bordered span12" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"  runat="server" OnRowCommand="GV_POS_RowCommand" OnRowDeleting="GV_POS_RowDeleting">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ITEMS" >  
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="PROID" runat="server" Value='<%# Eval("ProductID")%>' /> 
                                                                        <asp:TextBox ID="TBItms"  runat="server" Text='<%# Eval("Items")%>'  ValidationGroup="val_Psal"  placeholder="Enter Product Name" OnTextChanged="TBItms_TextChanged" ></asp:TextBox>                                                        
                                                                        <asp:RequiredFieldValidator ID="RFVItms" ForeColor="Red" Font-Size="Smaller" ValidationGroup="val_Psal" runat="server" ErrorMessage="Please Write Some in Products" ControlToValidate="TBItms"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ITEM TYPE" >  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PROTYPID" runat="server" Value='<%# Eval("PROTYPID")%>'  ></asp:Label>
                                                                        <asp:DropDownList ID="DDL_PROTYPID" runat="server" ValidationGroup="val_Psal"  ></asp:DropDownList> 
                                                                        <asp:RequiredFieldValidator id="reqPROTYPID" Text="Please Select Item Type.." ForeColor="Red" Font-Size="Smaller" ValidationGroup="val_Psal" InitialValue="0" ControlToValidate="DDL_PROTYPID" Runat="server" />                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="FITTING CHARGES">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="tbfitpric" runat="server" Text='<%# Eval("fitpric")%>' style="width:70px; height:20px;" ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                   
                                                                <asp:TemplateField HeaderText="PURCHASE PRICE">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="tbItmpris" runat="server" Text='<%# Eval("Itempric")%>' style="width:70px; height:20px;" ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                   
                                                                <asp:TemplateField HeaderText="SALES PRICE">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="tbsalpris" runat="server" Text='<%# Eval("salpric")%>' style="width:70px; height:20px;"  ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                   
                                                                <asp:TemplateField HeaderText="QUANTITY">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TBItmQty" runat="server" Text='<%# Eval("QTY")%>'  ValidationGroup="val_Psal" style="width:70px; height:20px;" AutoPostBack="true" OnTextChanged="TBItmQty_TextChanged" ></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RFVQty" ForeColor="Red" ValidationGroup="val_Psal" runat="server" Font-Size="Smaller" ErrorMessage="Please Write Some in Quantity" ControlToValidate="TBItmQty"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TOTAL">
                                                                    <ItemTemplate>    
                                                                        <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false" />
                                                                        <asp:Label ID="lblttl"  runat="server" Text='<%# Eval("TTL")%>'  ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>     
                                                                <asp:TemplateField>                                                
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkbtnadd" ValidationGroup="gvItems" CommandName="Add"  OnClick="linkbtnadd_Click" runat="server"><i class="halflings-icon plus-sign" ></i>+</asp:LinkButton>
                                                                        <asp:HiddenField ID="HFDSal" runat="server" Value='<%# Eval("Dposid")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                     
                                                                <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                                                                    <ControlStyle CssClass="halflings-icon minus-sign" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate> 
                                                        </asp:GridView>
                                                        </div>
                                                        <div class="span12">&nbsp;</div>
                                                        <div class="control-group span10" style="float:right;">
                                                            Advance: <asp:TextBox ID="TBAdvance"  runat="server" style="width:200px; height:20px;"  placeholder="Enter Advance..."  AutoPostBack="true" OnTextChanged="TBAdvance_TextChanged" ></asp:TextBox>
                                                            Balance: <asp:TextBox ID="TBBalance" runat="server" style="width:200px; height:20px;"  placeholder="Enter Balance..."   ></asp:TextBox>
                                                            Total: <asp:TextBox ID="TBTtl" runat="server"  style="width:200px; height:20px;" placeholder="Enter Total..."   ></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="val_Psal" Font-Size="Smaller" runat="server" ControlToValidate="TBTtl" ForeColor="Red" ErrorMessage="* Total Amount Can not be Empty!.."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
						                    </fieldset>
						                <div class="form-actions" style=" text-align:right; padding-right:11%;">
                                            <asp:Button ID="btn_prtbil" runat="server" Text="Print Bill" CssClass="btn btn-danger"  OnClick="btn_prtbil_Click" />
                                            <asp:Button ID="btn_Sav" runat="server" Text="Save" CssClass="btn btn-primary"  ValidationGroup="val_Psal" OnClick="btn_Sav_Click" />
                                            <asp:Button ID="Btn_Cancl" CssClass="btn" runat="server" Text="Cancel" OnClick="Btn_Cancl_Click" />
						                </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
					        </div>
				        </div>
                        <!--/span-->
			        </div>	
	            </div>
                <!--/.fluid-container-->
			    <!-- end: Content -->
		    </div><!--/#content.span10-->
		</div><!--/fluid-row-->
        <asp:Panel ID="Panel1"  CssClass="modalPopup" Style="display: none;" runat="server" HorizontalAlign="Center"  Width="495px" GroupingText="Add/Edit Customer">
            <div class="modal" >
                <div class="modal-header">
                    <!--<button type="button" class="close" data-dismiss="modal">×</button>-->
                    <asp:Button ID="closebtn" Text="x"  CssClass="close" data-dismiss="modal" runat="server"   />
                    <h3>Add New Customer</h3>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="text-align:left">
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblerr" runat="server" ForeColor="Red" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="HFCustID" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td>
                                                    Name
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TB_CustNam" runat="server" Height="18px" ValidationGroup="grp" 
                                                        Width="142px"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkbtn_del" runat="server" Text="Delete" OnClick="lnkbtn_del_Click"></asp:LinkButton>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Size="Smaller"
                                                        ControlToValidate="TB_CustNam" ErrorMessage="Customer Name" ForeColor="Red" 
                                                        ValidationGroup="grp"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                  Mobile No.
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TBMobNo" runat="server" Height="18px" ValidationGroup="grp" AutoPostBack="true" Width="142px" OnTextChanged="TBMobNo_TextChanged"></asp:TextBox>
                                                    <asp:HiddenField ID="HFMobNo" runat="server" />
                                                    <asp:AutoCompleteExtender ServiceMethod="GetCustMob" OnClientShown="PopupShown" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBMobNo" ID="AutoCompleteExtender2"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>

                                                </td>
                                            </tr>
                                            <tr>
                                                 <td>
                                                    Right Eye.
                                                 </td>
                                                <td>
                                                     <asp:CheckBox ID="CHK_RightEye" runat="server" />
                                                    <asp:TextBox ID="TBRSphl" runat="server"  style="width:70px; height:30px;" placeholder="Enter Sphere..."></asp:TextBox>
                                                    <asp:TextBox ID="TBRCyln" runat="server"  style="width:70px; height:30px;" placeholder="Enter Cylendrical..."></asp:TextBox>                                            
                                                    <asp:TextBox ID="TBRAXSIS" runat="server" style="width:70px; height:30px;" placeholder="Enter Axis..."></asp:TextBox>
                                                    <asp:TextBox ID="TBRADD" runat="server" style="width:70px; height:30px;" placeholder="Add..."></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Left Eye. 
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CHK_LeftEye" runat="server" />
                                                    <asp:TextBox ID="TBLSphl" runat="server" style="width:70px; height:30px;" placeholder="Enter Sphere..."></asp:TextBox>
                                                    <asp:TextBox ID="TBLCyln" runat="server" style="width:70px; height:30px;" placeholder="Enter Cylendrical..."></asp:TextBox>
                                                    <asp:TextBox ID="TBLAXSIS" runat="server" style="width:70px; height:30px;" placeholder="Enter Axis..."></asp:TextBox>
                                                    <asp:TextBox ID="TBLADD" runat="server" style="width:70px; height:30px;" placeholder="Add..."></asp:TextBox>
                                                </td>
                                            </tr>
                            
                                            <tr>
                                                <td colspan="2">
                                                    <table  class="span6">
                                                        <tr>
                                                            <td>
                                                                <div class="modal-footer">
                                                                    <asp:Button ID="BSave" ValidationGroup="grp" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="BSave_Click"  />
                                                                    <asp:Button ID="BReset" runat="server" CssClass="btn"  Text="Reset" OnClick="BReset_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
        PopupControlID="Panel1" TargetControlID="btn_Cust"
        CancelControlID="closebtn" BackgroundCssClass="modalBackground1">
        </asp:ModalPopupExtender>
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

   <script>
       function PopupShown(sender, args) {
           sender._popupBehavior._element.style.zIndex = 99999999;
       }
        
</script>
</html>
