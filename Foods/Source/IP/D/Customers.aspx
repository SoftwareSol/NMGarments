<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs"
     EnableEventValidation = "false" Inherits="Foods.Customers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <meta name="viewport" content="width=device-width, user-scalable=no">
	<link rel="stylesheet" href="../../Content/Dev/fv.css" type="text/css" />
	<!--[if IE]>
	<style>
		.item .tooltip .content{ display:none; opacity:1; }
		.item .tooltip:hover .content{ display:block; }
	</style>
	<![endif]-->
    <style type="text/css">
    
        /* Scroller Start */
        .scrollit {
            overflow:scroll;
            height:300px;
	        width:100%;
	        margin:0px auto;
        }

        /* Scroller End */

        /* Loading Start */

            .LoadingProgress {
                
            display: none;
            height: 200px;
            width: 200px;
            margin-left: 40%;
            margin-top: 15%;
            position:absolute;
            z-index:100;
           
            }
        /* Loading End */
                /* Modal popUp End */
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

   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <ul class="breadcrumb">
	    <li>
		    <i class="icon-home"></i>
		    <a href="WellCome.aspx">Home</a> 
		    <i class="icon-angle-right"></i>
	    </li>
	    <li><a href="#">Set Up</a>
   		    <i class="icon-angle-right"></i>
	    </li>
        <li><a href="Customers.aspx">Customers</a></li>
    </ul>
      
    
    
    <!-- imageLoader - START -->
    
    <%--<img id='HiddenLoadingImage' src="../../../img/page-loader.gif" class="LoadingProgress" />--%>

    <!-- imageLoader - END -->
    
     <div class="span12">
       <%-- <div class="span2" >
            <asp:LinkButton ID="LinkBtnExportExcel" runat="server"  class="quick-button-small span6" OnClick="LinkBtnExportExcel_Click" ><i class="halflings-icon file"></i><p>Export Excel</p></asp:LinkButton>
        
            <a name="b_print"  class="quick-button-small span6" id="btnPrint" runat="server" href="~/Source/OP/Print/CustomerListPrint.aspx?CustomerList" target="_blank" ><i class="halflings-icon print"></i><p>Print</p></a>
        </div>--%>
     </div>
     <div class="span12">&nbsp;</div>
    <div class="row-fluid sortable">       
        <div class="box span12">
               <div class="box-header" data-original-title>
				<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Cutomers</h2>
				<div class="box-icon">
				</div>
			</div>
			<div class="box-content">   
                <div class="span11">

                <div class="control-group  span10">
                                                  
					<label class="control-label span1" for="appendedInputButton">Search</label>
					<div class="controls">
                            <div class="input-append span4">
                                <asp:TextBox ID="TBSearch" runat="server" size="16" AutoPostBack="true" placeholder="Search Customers..." CssClass="Search" OnTextChanged="TBSearch_TextChanged" OnBlur="DisplayLoadingImage();" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!"/>
    						</div>
                        
					</div>
                    <div class=" input-append span2"></div>
                    <div class=" input-append span2">
                            <a href="#" class="btn btn-small btn-primary"  onclick="showcustomer();" runat="server" id="btnadd" ><i class="icon-plus"></i></a>
                        </div>
				</div>
                </div>
                <div class="span10">
                    <%--div class="control-group  span10">
					<label class="control-label span2">Upload File</label>
					    <div class="controls span3">
                             <asp:FileUpload ID="FileUploadToServer" CssClass="upload" runat="server" />
                             <asp:Label ID="lblMsg" runat="server"></asp:Label>    
                        </div>
                        <div class="span1"><asp:LinkButton ID="LinkBtnUpload" runat="server"  class="btn btn-small btn-success" OnClick="LinkBtnUpload_Click" OnClientClick="progressbar();" ><i class="icon-upload-alt"></i>   <b>Upload</b></asp:LinkButton></div>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" style="display:none;">
                    </asp:GridView>--%>                         
                </div>
                <div id="uploadbar" class="span10" style="display:none;">
                    <div class="field_notice"><div class="progress progressUploadAnimate pink"></div></div>
                    <div class="field_notice"></div>
               </div>
              <%--  <span class="must progressUploadAnimateValue">0 Mb</span> / 200 Gb--%>
                <div class="span12">&nbsp;</div>

                <div class="row-fluid sortable">		
	                    <div class="box span12">
				        <div class="box-header" data-original-title>
					        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Cutomers List</h2>
					        <div class="box-icon">
						        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
						        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
					        </div>
			            </div>
				        <div class="box-content">
                            <div class="scrollit">
                                <div class="div_print">
                                    <asp:GridView ID="GVCutomers" runat="server" AutoGenerateColumns="False"
                                         ShowHeader="true" ShowHeaderWhenEmpty="true" 
                                        CssClass="table table-striped table-bordered" DataKeyNames="CustomerID"
                                         OnRowCommand="GVCutomers_RowCommand" OnRowDeleting="GVCutomers_RowDeleting" >
                                <Columns>
                                    <asp:BoundField DataField="CustomerID" HeaderText="ID" ReadOnly="True" SortExpression="CustomerID" />
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />                                        
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" Text="Select" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" CommandName="Delete"  runat="server" OnClientClick="" Text="Delete" > </asp:LinkButton>
                                            <asp:HiddenField ID="HFCustomerID" runat="server" Value='<%# Eval("CustomerID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
                <div id="showcustomers" >                    
                    <div class="row-fluid sortable">
                        <div class="box span12">
				                    <div class="box-header" data-original-title>
                                        <h2><i class="halflings-icon align-justify"></i><span class="break"></span> Add/Edit Cutomers </h2>
					                    <div class="box-icon">
						                    <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>						                
					                    </div>
			                        </div>
				                    <div class="box-content">
                                        <div class="box">
                                            <div class="span12">&nbsp;</div>
                                            <div class="alert alert-error" id="alerts" style="display:none;">
							                    <strong>Error!</strong>
                                                <label id="lblerrorCustomerName"> Please Write some thing in the Customer Name!!</label>
                                                <label id="lblphonenum1"> Please Write some thing in the Phone Num!!</label>
                                                <label id="lblphonenum">Please write The Valid Phone Number!!</label>
                                                <label id="lblEmail">Please write The Valid Email Add!!</label>
                                                <label id="lblInt">Please Write Number Only!!</label>
                                                <label id="lblCellNo">Please Write Cell Number!!</label>
                                                <label id="lblNIC">Please Write NIC!!</label>
                                                <label id="lblAddess">Please Write Address!!</label>
						                    </div>
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
						                            <label>
							                            <span>Name</span>
							                            <input  name="name" placeholder="ex. John f. Kennedy" type="text" runat="server" id="TBCustomersName" class="CustomersName" /><asp:Label ID="v_name" runat="server" ForeColor="Red" Text="" Font-Bold="true"></asp:Label></label>                 
					                            </div>
                                            </div>
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span> NTN: </span>
                                                        <input  name="NTN" placeholder="ex. #NTN 6799799..."  type="text" runat="server" id="TBNTN" />
						                            </label>
                                                </div>
                                            </div>
                                            </div>
                                    
                                        <div class="span12">                                       
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span> Area: </span>
                                                        <asp:TextBox ID="TBArea" runat="server" placeholder="Area..." AutoPostBack="true" OnTextChanged="TBArea_TextChanged"></asp:TextBox>                                                       
                                                          <asp:AutoCompleteExtender ServiceMethod="GetArea" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                        TargetControlID="TBArea" ID="AutoCompleteExtender2"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
						                            </label>
                                                    </div>
                                            </div>  
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span> Mobile No: </span>
                                                        <input  name="Phone" placeholder="ex. +92786678999.."  runat="server" id="TBPhone" class="TBPhone"  />
						                            </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span> Email: </span><%--onblur="CheckEmptyEmail(); validateEmail(this.value);"--%>
                                                        <input  name="Email" placeholder="ex. John@live.com.."  type="text" class="email" runat="server" id="TBEmail"  />
						                            </label>
                                                </div>
                                            </div>
                                            <div class="span6">
                                                <div class="item">
						                                <label>
							                                <span>  NIC: </span>                                                         <%--onblur="CheckEmptyNIC(); checkint(this.value,'.NIC');"--%>
							                                <input  name="NIC" placeholder="ex. 78991-89728000-9 ..."  type="text" runat="server" id="TBNIC" class="NIC"  />
						                                </label>						                                
					                                </div>
                                                </div>   
                                        </div>
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
						                                <label>
							                                <span>   City: </span>                               
                                                            <asp:TextBox ID="TBCity" runat="server" placeholder="City.." AutoPostBack="true"  OnTextChanged="TBCity_TextChanged"></asp:TextBox>                         
                                                              <asp:AutoCompleteExtender ServiceMethod="GetCity" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                        TargetControlID="TBCity" ID="AutoCompleteExtender1"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
							                            </label>
					                            </div>
                                            </div>
                                            <div class="span6">
                                                <div class="item">
						                                <label>
							                                <span> Address: </span>                               
                                                            <textarea  name='message' placeholder="ex. Plot# 678, Mill Area North Wales..." runat="server" id="TBAddress" class="Addess"  ></textarea>                                                                                 
						                                </label>
					                                </div>
                                                </div>                                                                                 
                                            </div>
                                            <div class="span6">
                                                <div class="item">
						                                <label>
							                                <span> Pre Bal: </span>
                                                            <asp:TextBox ID="TBPrevBal" runat="server" AutoPostBack="true" OnTextChanged="TBPrevBal_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="v_prebal" runat="server" Text="" ForeColor="Red" ></asp:Label>
						                                </label>
					                                </div>
                                                </div>                                                                                 
                                            </div>
                                        <div class="clearfix"></div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="span4"></div>
                                            <div class="span6">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
                                                    <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                            </div>
                                            
                                            <div class="span2">
                                                <asp:HiddenField ID="HFCustomerID" runat="server" />
                                            </div>
							            </div>
                                        <div class="span12"></div>
                                    </div>
                            </div>
                    </div>		
	    </div>
		</div>
		</div>
    </div>
    <div id="myModalCustCategory" class="modal fade" style="display:none;">
		<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<h4 class="modal-title">Add/Edit Customer Category</h4>
			</div>
			<!-- dialog body -->
			<div class="modal-body">
					  <table class="table">
                          <tr>
                              <td>Category:</td>
                              <td><asp:TextBox ID="TBCustomerCategoryName" runat="server" PlaceHolder="ex. Important..."></asp:TextBox></td>
                          </tr>
					  </table>
			</div>
			<!-- dialog buttons -->
			<div class="modal-footer">
                <asp:LinkButton ID="LinkBtnAddCustCategory" runat="server" CssClass="btn btn-success" Text="Create" OnClick="LinkBtnAddCustCategory_Click"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton  ID="LinkBtnCancelCustCategory" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="LinkBtnCancelCustCategory_Click"></asp:LinkButton>                                     
			</div>
            <asp:HiddenField ID="HFCustCategory" runat="server" />
		</div>
		</div>
	</div>
   
    <div id="ModalAlert" class="modal fade" style="display:none;">
		<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<h4 class="modal-title">Notification!</h4>
			</div>
			<!-- dialog body -->
			<div class="modal-body">
				<asp:Label ID="lblalert" runat="server"></asp:Label>
                
			</div>
			<!-- dialog buttons -->
            <div class="modal-footer">
                <asp:LinkButton ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" OnClick="btnalertOk_Click"></asp:LinkButton>
                    <%--<asp:Button ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" />--%>
			</div>
		</div>
		</div>
	</div>
    
    <div id="ModalCity" class="modal fade" style="display:none;">
		<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<h4 class="modal-title">Add/Edit City</h4>
			</div>
			<!-- dialog body -->
			<div class="modal-body">
                 <div class="span2" id="alertcity" style="display:none;">
					
                    <label id="lblcity" runat="server"> Please Write some thing in the Customer Name!!</label>                
				</div>
				<table class="table">
                    <tr>
                        <td>City:</td>
                        <td><asp:TextBox ID="TBCitys" runat="server" CssClass="city" PlaceHolder="ex. Karachi..."></asp:TextBox></td>
                    </tr>
				</table>
			</div>
			<!-- dialog buttons -->
			<div class="modal-footer">
                <asp:LinkButton ID="LinkBtnCityInsert" runat="server" CssClass="btn btn-success" Text="Create" OnClick="LinkBtnCityInsert_Click" OnClientClick="return checkcity();" ></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton  ID="LinkBtnCityCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="LinkBtnCityCancel_Click" ></asp:LinkButton>                                     

			</div>
            <asp:HiddenField ID="HFCity" runat="server" />
		</div>
		</div>
	</div>

    <div id="ModalCustomerType" class="modal fade" style="display:none;">
		<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<h4 class="modal-title">Add/Edit Customer Type</h4>
			</div>
			<!-- dialog body -->
			<div class="modal-body">
					  <table class="table">
                          <tr>
                              <td>Customer Type:</td>
                              <td><asp:TextBox ID="TBCustomerType" runat="server" PlaceHolder="ex. Customer Type..."></asp:TextBox></td>
                          </tr>
					  </table>
			</div>
			<!-- dialog buttons -->
			<div class="modal-footer">
                <asp:LinkButton ID="LinkBtnInsertCustomerType" runat="server" CssClass="btn btn-success" Text="Create" OnClick="LinkBtnInsertCustomerType_Click" ></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton  ID="LinkBtnCancelCustomerType" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="LinkBtnCancelCustomerType_Click" ></asp:LinkButton>                                     
               
			</div>
            <asp:HiddenField ID="HFMyCustomerType" runat="server" />
		</div>
		</div>
	</div>

    <div id="MyModalDelete" class="modal fade" style="display:none;">
		<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<h4 class="modal-title">Notification!</h4>
			</div>
			<!-- dialog body -->
			<div class="modal-body">
				<asp:Label ID="lblmodaldelete" runat="server"></asp:Label>
                
			</div>
			<!-- dialog buttons -->
            <div class="modal-footer">
                <asp:LinkButton ID="linkmodaldelete" runat="server" CssClass="btn btn-success" Text="Yes" OnClick="linkmodaldelete_Click"></asp:LinkButton>
                <asp:LinkButton ID="linkbtncanceldel" runat="server" CssClass="btn btn-success" Text="No"></asp:LinkButton>
                <asp:HiddenField ID="HFCustId" runat="server" />
			</div>
		</div>
		</div>
	</div>
    <asp:HiddenField ID="HFAccountCategoryName" runat="server" />
    <asp:HiddenField ID="SubHeadCat" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="../Controller/Customers.js"></script>
    <script src="../Controller/Common.js"></script>


    <script type="text/javascript">
            
                function DisplayLoadingImage() {

                    if ($(".Search").val() == '') {
                        alert('a');
                        document.getElementById("HiddenLoadingImage").style.display = "none";

                    } else {
                        alert('b');
                        document.getElementById("HiddenLoadingImage").style.display = "block";
                        $("body").css({ opacity: 0.9 });
                    }
                };

            </script>                        

</asp:Content>
