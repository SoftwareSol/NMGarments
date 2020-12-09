<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_PSal.aspx.cs" Inherits="Foods.frm_PSal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    	<!-- start: Meta -->
	<meta charset="utf-8" />
	<title>POS</title>
    <!-- start: Favicon -->
	<link rel="shortcut icon" href="../../../img/Softsol_logo.png" />
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

        .scrollit_ {
            overflow:scroll;
            height:300px;
	        width:350px;           
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
				    <a class="brand" href="WellCome.aspx"><span>POS</span></a>								
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
			        <div class="row-fluid sortable">
				        <div class="box span12">
					        <div class="box-content">
                                <div class="span12">
                                    <asp:Label ID="lblmssg" runat="server" ForeColor="Red" ></asp:Label>
                                </div>
						        <div class="span1">
                                    <asp:LinkButton ID="btn_Cust" runat="server" CssClass="quick-button-small span12" >							
                                        <i class="icon-group"></i>
                                        <p>Add</p>
                                    </asp:LinkButton>
					            </div>
                                <div class="span1">
                                    <asp:LinkButton ID="btnhold" runat="server" CssClass="quick-button-small span12" >							
                                        <i class="icon-group"></i>
                                        <p>Hold History</p>
                                    </asp:LinkButton>
					            </div>
                                <div class="span1">
                                    <asp:LinkButton ID="lnk_salhis" runat="server" CssClass="quick-button-small span12" >							
                                        <i class="icon-group"></i>
                                        <p>Sales History</p>
                                    </asp:LinkButton>
					            </div><div class="span1">
                                    <a href="frm_Expences.aspx" target="_blank" class="quick-button-small span12" >							
                                        <i class="icon-group"></i>
                                        <p>Transactions</p>
                                    </a>
                                </div>
                                <table class="table table-striped span7">
                                    <tr>
                                        <td style="padding-left:30px; width:200px; text-align:right;">
                                            Bill No: 
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_BillNo" runat="server" ForeColor="Red" Text=""></asp:Label>
                                        </td>
                                        <td style="padding-left:30px; width:200px; text-align:right;">
                                            Date:
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_dat" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbl_Openbalance" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbl_openbalance1" runat="server" Text=""></asp:Label>
                                            
                                        </td>
                                        <td style="padding-left:30px; width:300px; text-align:right;">
                                            Time:
                                        </td>
                                        <td >
                                            <span style="text-align:right; ">
                                                <input type="text" id="txtClock" runat="server"  name="Clock" style="height:10px; background:none; border:none;" />
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btn_POScancl" runat="server" Text="Cancel" style="float:right; margin-right:20px;"  CssClass="btn btn-danger" OnClick="btn_POScancl_Click"  />                               
                                <asp:HiddenField ID="isCancel" runat="server" Value="0" />
                                        <table  class=" table table-bordered span12">
                                            <tr>
                                                <div class="control-group span3">
                                                    <td style="width:150px;">
                                                        <label class="control-label" for="TBCust">Customer: </label>
                                                        <label class="control-label" for="TBREC">Reciever: </label>
                                                    </td>
                                                    <td>
                                                        <div class="controls">
                                                            <asp:TextBox ID="TBCust"  runat="server" AutoPostBack="true" ValidationGroup="val_Psal" placeholder="Enter Customer Name..." OnTextChanged="TBCust_TextChanged" />
                                                            <asp:TextBox ID="TBREC"  runat="server" ValidationGroup="val_Psal" placeholder="Enter Reciever Name..."/>
                                                            <asp:AutoCompleteExtender ServiceMethod="GetSearch" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBCust" ID="AutoCompleteExtender1"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                            <asp:Label ID="lb_custtyp" runat="server"></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td style="width:150px;">
                                                        Acc No.
                                                        <asp:Label ID="lbl_Acc" runat="server" ></asp:Label>
                                                    </td>
                                                </div>
                                                <div class="control-group span3">
                                                    <td>
                                                        <label class="control-label" for="TB_SalNO">Sale No: </label>
                                                    </td>
                                                    <td>
                                                        <div class="controls">
                                                            <asp:TextBox ID="TB_SalNO" runat="server"  placeholder="Enter Sale Number..."
                                                                 AutoPostBack="true" OnTextChanged="TB_SalNO_TextChanged" ></asp:TextBox>
                                                            <asp:AutoCompleteExtender ServiceMethod="GetBillNO" CompletionListCssClass="completionList"
                                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                 MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                                                TargetControlID="TB_SalNO" ID="AutoCompleteExtender3"  
                                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                        </div>
                                                    </td>
                                                </div>
                                           
                                                <div class="control-group span1">
                                                    <td><label class="control-label" for="LB_Due">Due: </label></td>
                                                    <td  style="width:150px;"><div class="controls">
                                                           <asp:TextBox ID="lbldue" runat="server" Text="0.00"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </div>
                                            </tr>
                                        </table>
                                        <div class="control-group span4" style="display:none">
                                            <label class="control-label" for="ckh_right">Right Eye: </label>
                                            <div class="controls">
                                                <asp:CheckBox ID="ckh_right" runat="server"  />
                                                <asp:TextBox ID="TBRCyl" runat="server" CssClass="span3"  placeholder="Enter Cylendrical..."></asp:TextBox>
                                                <asp:TextBox ID="TBRSph" runat="server" CssClass="span3"  placeholder="Enter Sphere..."></asp:TextBox>
                                                <asp:TextBox ID="TBRAxis" runat="server" CssClass="span3" placeholder="Enter Axis..."></asp:TextBox>
                                                <asp:TextBox ID="TBRAdd_" runat="server" CssClass="span1"  placeholder="Add..."></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="control-group span4" style="display:none">
                                            <label class="control-label" for="chk_left">Left Eye: </label>
                                            <div class="controls">
                                                <asp:CheckBox ID="chk_left" runat="server" />
                                                <asp:TextBox ID="TBLCyl" runat="server" CssClass="span3"  placeholder="Enter Cylendrical..."></asp:TextBox>
                                                <asp:TextBox ID="TBLSph" runat="server" CssClass="span3"  placeholder="Enter Sphere..."></asp:TextBox>
                                                <asp:TextBox ID="TBLAxis" runat="server" CssClass="span3" placeholder="Enter Axis..."></asp:TextBox>
                                                <asp:TextBox ID="TBLAdd_" runat="server" CssClass="span1" placeholder="Add..."></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="span12">
                                            <fieldset>
                                             
                                                        <div class="span8">
                                                            <div class="span12 scrollit_">
                                                                <asp:GridView ID="GV_POS" ShowHeader="true" CssClass="table table-striped table-bordered span12" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"  runat="server" OnRowCommand="GV_POS_RowCommand" OnRowDeleting="GV_POS_RowDeleting" OnDataBound="GV_POS_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ITEMS" >  
                                                                        <ItemTemplate>                                                                      
                                                                            <asp:TextBox ID="TBItms"  runat="server" Text='<%# Eval("Items")%>'  ValidationGroup="val_Psal" style="width:50px; height:20px; float:left"  placeholder="Code" AutoPostBack="true" OnTextChanged="TBItms_TextChanged" ></asp:TextBox>                                                        
                                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TBItmdesc" runat="server" Text='<%# Eval("ItemDesc")%>' ValidationGroup="val_Psal" style=" width:100px; height:20px; float:right"  placeholder="Item Name" ></asp:TextBox>                                                        
                                                                            <asp:AutoCompleteExtender ServiceMethod="Getpro" CompletionListCssClass="completionList"
                                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" 
                                                                                CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBItms" ID="AutoCompleteExtender3"  
                                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                                            <asp:RequiredFieldValidator ID="RFVItms" ForeColor="Red" Font-Size="Smaller" ValidationGroup="val_Psal" runat="server" ErrorMessage="Please Write Some in Products" ControlToValidate="TBItms"></asp:RequiredFieldValidator>
                                                                            <asp:Label ID="lblchkpro" ForeColor="Red"  Font-Size="X-Small" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList runat="server" ID="ddlUnit" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"></asp:DropDownList>
                                                                            <asp:TextBox ID="TBUnit" runat="server" Text='<%# Eval("UNIT")%>' placeholder="Enter Units..." ValidationGroup="val_Psal" style="width:70px; height:20px;" AutoPostBack="true" OnTextChanged="TBUnit_TextChanged" ></asp:TextBox>
                                                                            <asp:Label ID="lblchk_unt" runat="server" Text="0"  Visible="false"></asp:Label>
                                                                            <asp:AutoCompleteExtender ServiceMethod="Getunts" CompletionListCssClass="completionList"
                                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                    MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                                                                TargetControlID="TBUnit" ID="AutoCompleteExtender5"  
                                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>                                                                           
                                                                            <asp:RequiredFieldValidator ID="RFVUnts" ForeColor="Red" Font-Size="Smaller" ValidationGroup="val_Psal" runat="server" ErrorMessage="Please Write Some in Units" ControlToValidate="TBUnit"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PRICE">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="tbsalpris" runat="server" Text='<%# Eval("salpric")%>' placeholder="Enter Price..." style="width:70px; height:20px;" AutoPostBack="true" OnTextChanged="tbsalpris_TextChanged" ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                   
                                                                    <asp:TemplateField HeaderText="QUANTITY">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TBItmQty" runat="server" Text='<%# Eval("QTY")%>' placeholder="Enter Quantity..." ValidationGroup="val_Psal" style="width:70px; height:20px;" AutoPostBack="true" OnTextChanged="TBItmQty_TextChanged" ></asp:TextBox>
                                                                            <asp:Label ID="lblchkqty" ForeColor="Red"  Font-Size="X-Small" runat="server"></asp:Label>

                                                                            <asp:RequiredFieldValidator ID="RFVQty" ForeColor="Red" ValidationGroup="val_Psal" runat="server" Font-Size="Smaller" ErrorMessage="Please Write Some in Quantity" ControlToValidate="TBItmQty"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                     
                                                                    <asp:TemplateField HeaderText="TOTAL">
                                                                        <ItemTemplate>    
                                                                            <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false"  />
                                                                            <asp:Label ID="lblttl"  runat="server" Text='<%# Eval("TTL")%>'  ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>   
                                                                    <asp:TemplateField HeaderText="SUB TOTAL" Visible="false">
                                                                        <ItemTemplate>    
                                                                            <asp:Label ID="lblsubttl"  runat="server" Text='<%# Eval("TTL")%>'  ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>     
                                                                    <asp:TemplateField>                                                
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnadd" ValidationGroup="gvItems" CommandName="Add" onkeydown="esc();"  OnClick="linkbtnadd_Click" runat="server"><i class="halflings-icon plus-sign" ></i></asp:LinkButton>
                                                                            <asp:HiddenField ID="HFDSal" runat="server" Value='<%# Eval("Dposid")%>' />
                                                                            <asp:HiddenField ID="HFPROID" runat="server" Value='<%# Eval("ProductID")%>'  />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                     
                                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                                                                        <ControlStyle CssClass="halflings-icon minus-sign" />
                                                                    </asp:CommandField>
                                                                </Columns>
                                                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate> 
                                                            </asp:GridView>
                                                            </div>
                                                            <div class="span11">
                                                                <table class="table table-striped table-bordered">
                                                                    <tr>
                                                                        <td  style="width:80px;">Total Items</td>
                                                                        <td  style="width:50px;"><asp:Label ID="lbl_itmqty" runat="server" Text="0.00"></asp:Label></td> 
                                                                        <td style="width:50px;">Total Qty</td>
                                                                        <td style="width:50px; text-align:left;">&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_ttlqty" runat="server" Text="0.00"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="control-group span12">
                                                                
                                                            <table class="table table-bordered" style=" float:right; text-align:right; margin-right:20px;">
                                                                <asp:Panel ID="pnl_extra" runat="server">
                                                                <tr>
                                                                    <td style="width:130px;">Discount (Amount):</td>
                                                                    <td><asp:TextBox ID="TBDisc"  runat="server" AutoPostBack="true" style="width:100px; height:20px;" Text="0"  placeholder="Enter Discount..." OnTextChanged="TBDisc_TextChanged1" ></asp:TextBox>
                                                                        <asp:Button ID="btn_disrevert" runat="server" Text="Revert" CssClass="btn btn-info"  OnClick="btn_disrevert_Click" />
                                                                    </td>
                                                                
                                                                    <td><asp:CheckBox ID="chk_tax1" AutoPostBack="true" runat="server" Text="17" OnCheckedChanged="chk_tax1_CheckedChanged" /> </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TBTaxPer"  runat="server"  style="width:100px; height:20px;"  placeholder="Enter Tax %..." Text="0.00" ></asp:TextBox></td>
                                                                
                                                                    <td><asp:CheckBox ID="chk_tax2" AutoPostBack="true" runat="server" Text="3" OnCheckedChanged="chk_tax2_CheckedChanged" /></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TBOthChrgs"  runat="server" style="width:100px; height:20px;"  placeholder="Enter Other Charges..." Text="0.00"  ></asp:TextBox></td>
                                                                </tr>
                                                                </asp:Panel>
                                                                <tr>
                                                                    <td>Amount Paid:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TBAdvance" CausesValidation="true"  runat="server" style="width:100px; height:20px;"  placeholder="Enter Advance..."  AutoPostBack="true" OnTextChanged="TBAdvance_TextChanged" ></asp:TextBox>
                                                                    </td>
                                                                    <td>Balance:</td>

                                                                    <td>
                                                                        <asp:Label ID="lblval" runat="server" ForeColor="Red"></asp:Label>
                                                                        <asp:TextBox ID="TBBalance" runat="server" style="width:100px; height:20px;"  placeholder="Enter Balance..."   ></asp:TextBox></td>
                                                                  
                                                                  <td>Total:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TBTtl" runat="server"  style="width:100px; height:20px;" placeholder="Enter Total..."   ></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                                
                                                            <div class="clearfix"></div>
                                                            <table style="width:100%; float:right; text-align:right; margin-right:20px;">
                                                                <tr> 
                                                                  <td>                                                                    
                                                                      <asp:Label ID="v_typofpay" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>                                                                     
                                                                  </td> 
                                                                  <td><h1>Total Payables:</h1></td>
                                                                    <td style="text-align:left;">
                                                                        <h1>&nbsp;Rs&nbsp;<asp:Label ID="lblttls" Text="0.00" runat="server"></asp:Label>&nbsp;/-</h1>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div class="clearfix"></div>
                                                                <table style="width:100%; float:right; text-align:right; margin-right:20px;">
                                                                    <tr>  
                                                                      <td>
                                                                         <div class="form-actions" style=" text-align:center; padding-right:11%;">
                                                                            <asp:Button ID="Btn_Hold" runat="server" Text="Hold" CssClass="btn btn-primary" OnClick="Btn_Hold_Click" />
                                                                             <asp:HiddenField ID="HFHold" runat="server" Value="0" />
                                                                            <asp:Button ID="btn_prtbil" runat="server" Visible="false" Text="Print Bill" CssClass="btn btn-danger"  OnClick="btn_prtbil_Click" />
                                                                            <asp:Button ID="btn_Sav" runat="server" Text="Print Bill" CssClass="btn btn-danger"  ValidationGroup="val_Psal" OnClick="btn_Sav_Click" />
                                                                            <asp:Button ID="Btn_Cancl" CssClass="btn" runat="server" Text="Cancel" OnClick="Btn_Cancl_Click" />
						                                                 </div>
                                                                     </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="span3">
                                                            <div class="scrollit_">
                                                                <h3>Items Avalable</h3>
                                                                <asp:TextBox ID="TBsearhPro" runat="server" placeholder="Enter Product Name.." AutoPostBack="true" OnTextChanged="TBsearhPro_TextChanged"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ServiceMethod="Getpro" CompletionListCssClass="completionList"
                                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" 
                                                                                CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBsearhPro" ID="AutoCompleteExtender5"  
                                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                                <asp:GridView ID="GVRemanItms" PageSize="10" runat="server" CssClass="table table-striped table-bordered span12"  EmptyDataText="NO Record Found" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" OnPageIndexChanging="GVRemanItms_PageIndexChanging" OnRowCancelingEdit="GVRemanItms_RowCancelingEdit" OnRowEditing="GVRemanItms_RowEditing" OnRowUpdating="GVRemanItms_RowUpdating">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID" Visible="false">  
                                                                            <ItemTemplate>  
                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label>  
                                                                            </ItemTemplate>  
                                                                        </asp:TemplateField> 
                                                                       <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" ReadOnly="true" />                                                                                                                  
                                                                       <asp:BoundField DataField="Dstk_Qty" HeaderText="Avail Qty" SortExpression="Dstk_Qty" ReadOnly="true" />                                                                                                                  
                                                                       <asp:BoundField DataField="Dstk_unt" HeaderText="Size" SortExpression="Dstk_unt" ReadOnly="true" />                                                                                                                                                                                          
                                                                         <asp:TemplateField HeaderText="Rate">  
                                                                            <ItemTemplate>  
                                                                                <asp:Label ID="lbl_Rate" runat="server" Text='<%#Eval("RetalPrice") %>'></asp:Label>  
                                                                            </ItemTemplate>  
                                                                            <EditItemTemplate>  
                                                                                <asp:TextBox ID="txt_Rate" runat="server" Text='<%#Eval("RetalPrice") %>' style="width:40px; height:30px;"></asp:TextBox>  
                                                                            </EditItemTemplate>  
                                                                        </asp:TemplateField> 
                                                                        <asp:TemplateField>  
                                                                            <ItemTemplate>  
                                                                                <asp:Button ID="btn_Edit" runat="server" Text="Edit" CssClass="btn btn-primary" CommandName="Edit" />  
                                                                            </ItemTemplate>  
                                                                            <EditItemTemplate>  
                                                                                <asp:Button ID="btn_Update" runat="server" CssClass="btn btn-primary" Text="Update" CommandName="Update"/>  
                                                                                <asp:Button ID="btn_Cancel" runat="server" CssClass="btn btn-danger" Text="Cancel" CommandName="Cancel"/>  
                                                                            </EditItemTemplate>  
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                   
						                    </fieldset>
						                   
                                        </div>
                                 
					        </div>
				        </div>
                        <!--/span-->
			        </div>	
	            </div>
                <!--/.fluid-container-->
			    <!-- end: Content -->
		    </div><!--/#content.span10-->
		</div><!--/fluid-row-->
        <asp:HiddenField ID="SubHeadCat" runat="server" Value="0" />
            <asp:HiddenField ID="HFAccountCategoryName" runat="server" Value="" />
        <asp:Panel ID="Panel1"  CssClass="modalPopup" Style="display: none;" runat="server" HorizontalAlign="Center"  Width="495px" GroupingText="">
            <div class="modal" >
                <div class="modal-header">
                    <!--<button type="button" class="close" data-dismiss="modal">×</button>-->
                    <asp:Button ID="closebtn1" Text="x"  CssClass="close" data-dismiss="modal" runat="server"   />
                    <h3>Add New Customer</h3>
                </div>
                <div class="modal-body">
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
                                            NTN No.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TBNTN" runat="server" Height="18px" ValidationGroup="grp" Width="142px" ></asp:TextBox>
                                            <asp:HiddenField ID="HFMobNo" runat="server" />

                                        </td>
                                    </tr>
                                    <tr style="display:none">
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
                                    <tr style="display:none">
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
                </div>
            </div>

            <asp:HiddenField ID="HFDposid" runat="server" Value="0" />
            <asp:HiddenField ID="HFCposid" runat="server" Value="0" />
        </asp:Panel>
                <asp:Panel ID="Panel2"  CssClass="modalPopup" Style="display: none;" runat="server" HorizontalAlign="Center"  Width="495px" GroupingText="">
            <div class="modal" >
                <div class="modal-header">
                    <!--<button type="button" class="close" data-dismiss="modal" >×</button>-->
                    <asp:Button ID="closebtn2" Text="x"  CssClass="close" data-dismiss="modal" runat="server"   />
                    <h3>Hold List</h3>
                </div>
                <div class="modal-body">
                            <table style="text-align:left">
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td>
                                                    Enter Bill No.
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TBBillNo" runat="server" Height="18px"  Width="142px" placeholder="Enter Bill Number..." AutoPostBack="true" OnTextChanged="TBBillNo_TextChanged"></asp:TextBox>
                                                     <asp:AutoCompleteExtender ServiceMethod="GetHoldBill" CompletionListCssClass="completionList"
                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted" MinimumPrefixLength="1" 
                                                    CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="TBBillNo" ID="AutoCompleteExtender4"  
                                                    runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="GVHoldList" runat="server" PageSize="5" AutoGenerateColumns="false"  OnRowCommand="GVHoldList_RowCommand" DataKeyNames="BillNO" CssClass="table table-striped table-bordered" OnRowDeleting="GVHoldList_RowDeleting" >
                                                        <Columns>
                                                           <asp:BoundField DataField="BillNO" HeaderText="Bill NO" SortExpression="BillNO" ReadOnly="true" />
                                                           <asp:BoundField DataField="billdat" HeaderText="Bill Date" SortExpression="billdat" ReadOnly="true" />
                                                           <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" ReadOnly="true" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkslecthol" runat="server" Text="Select" CommandName="Select"></asp:LinkButton>
                                                                    <asp:HiddenField ID="HFHoldBill" Value='<%#Eval("BillNO") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="True" DeleteText="Delete"  >                                                                        
                                                            </asp:CommandField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                </div>
            </div>
        </asp:Panel>
                
        <%--<asp:Panel ID="Panel3"  CssClass="modalPopup" Style="display: none;" runat="server" HorizontalAlign="Center"  Width="495px" Height="400px" GroupingText="">
            <div class="modal" >
                <div class="modal-header">
                    <!--<button type="button" class="close" data-dismiss="modal">×</button>-->
                    <asp:Button ID="closebtn3" Text="x"  CssClass="close" data-dismiss="modal" runat="server"   />
                    <h3>Sales History</h3>
                </div>
                <div class="modal-body">
                            <table style="text-align:left;  height:250px;">
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="HFSALHIS" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td>
                                                    Enter Date.
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TBSalhisdat" runat="server" Height="18px"  Width="142px" placeholder="Enter Date..." AutoPostBack="true" OnTextChanged="TBSalhisdat_TextChanged"></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender1" CssClass="calender" PopupButtonID="imgPopup" runat="server" 
                                    TargetControlID="TBSalhisdat" Format="yyyy-MM-dd"> </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </td>
                                </tr>
                            </table>
                </div>
            </div>
            <asp:HiddenField ID="HFHoldId" runat="server" />
        </asp:Panel>--%>

        <asp:Panel ID="Panel3"  CssClass="modalPopup" Style="display: none;" runat="server" HorizontalAlign="Center"  Width="495px" Height="400px" GroupingText="">
            <div class="modal" >
                <div class="modal-header">
                    <!--<button type="button" class="close" data-dismiss="modal">×</button>-->
                    <asp:Button ID="closebtn3" Text="x"  CssClass="close" data-dismiss="modal" runat="server"   />
                    <h3>Sales History</h3>
                </div>
                <div class="modal-body">
                            <table style="text-align:left;  height:250px;">
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="HFSALHIS" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    From
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TBSalhisfdat" runat="server" Height="18px"  Width="142px" placeholder="Enter Date..."></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender1" CssClass="calender" PopupButtonID="imgPopup" runat="server" 
                                                        TargetControlID="TBSalhisfdat" Format="yyyy-MM-dd"> </asp:CalendarExtender>
                                                </td>
                                                <td>
                                                    To
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TBSalhistdat" runat="server" Height="18px"  Width="142px" placeholder="Enter Date..."></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender2" CssClass="calender" PopupButtonID="imgPopup" runat="server" 
                                                         TargetControlID="TBSalhistdat" Format="yyyy-MM-dd"> </asp:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_dsr" runat="server" Text="Show" CssClass="btn-primary" OnClick="btn_dsr_Click" />
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </td>
                                </tr>
                            </table>
                </div>
            </div>
            <asp:HiddenField ID="HFHoldId" runat="server" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
        PopupControlID="Panel1" TargetControlID="btn_Cust"
        CancelControlID="closebtn1" BackgroundCssClass="modalBackground1">
            </asp:ModalPopupExtender>
         <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
        PopupControlID="Panel2" TargetControlID="btnhold"
        CancelControlID="closebtn2" BackgroundCssClass="modalBackground1">
             </asp:ModalPopupExtender>
                 <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
        PopupControlID="Panel3" TargetControlID="lnk_salhis"
        CancelControlID="closebtn3" BackgroundCssClass="modalBackground1">
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
    <script type="text/javascript" language="javascript">
         esc = function (eventRef) {
             if (!eventRef)
                 eventRef = event;

             var keyStroke = (eventRef.keyCode) ? eventRef.keyCode : ((eventRef.charCode) ? eventRef.charCode : eventRef.which);

             if (keyStroke == 27)
                 var TBAdvance = document.getElementById("TBAdvance");

             TBAdvance.focus();
             //return false;

         }
    </script>         
</html>
