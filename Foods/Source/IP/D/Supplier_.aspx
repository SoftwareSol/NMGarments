<%@ Page Title="Supplier" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Supplier_.aspx.cs"
     Inherits="Foods.Supplier_" %>
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
        <li>
            <a href="Supplier_.aspx">Supplier</a>
        </li>
    </ul>

    <!-- imageLoader - START -->
    
    <%--<img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />--%>

    <!-- imageLoader - END -->
    
     <div class="span12">
        <%--<div class="span2" >
            <asp:LinkButton ID="LinkBtnExportExcel" runat="server"  class="quick-button-small span6" OnClick="LinkBtnExportExcel_Click"  ><i class="halflings-icon file"></i><p>Export Excel</p></asp:LinkButton>
        
            <a name="b_print"  class="quick-button-small span6" id="btnPrint" runat="server" href="~/Source/OP/Print/CustomerListPrint.aspx?CustomerList" target="_blank" ><i class="halflings-icon print"></i><p>Print</p></a>
        </div>--%>
     </div>
     <div class="span12">&nbsp;</div>
    <div class="row-fluid sortable">       
        <div class="box span12">
               <div class="box-header" data-original-title>
				<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Supplier</h2>
				<div class="box-icon">
				</div>
			</div>
			<div class="box-content">
                <div class="span11">

                <div class="control-group  span10">
                                                  
					<label class="control-label span1" for="appendedInputButton">Search</label>
					<div class="controls">
                            <div class="input-append span4">
                                <asp:TextBox ID="TBSearch" runat="server" size="16" AutoPostBack="true" placeholder="Search Suppliers..." CssClass="Search"  OnBlur="DisplayLoadingImage();" OnTextChanged="TBSearch_TextChanged" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!"/>
    						</div>
                        <div class="span2"></div>   
                        <div class="span2">
                            <a href="#" class="btn btn-small btn-primary"   onclick="showsupplier();" runat="server" id="btnadd" ><i class="icon-plus"></i></a>
                        </div>
					</div>
				</div>
                </div>
                <div class="span10" style="text-align:center;">
                    <asp:Label ID="lblmssg" runat="server" ForeColor="Red"></asp:Label>
                    <%--<div class="control-group  span10">
					<label class="control-label span2">Upload File</label>
					    <div class="controls span3">
                             <asp:FileUpload ID="FileUploadToServer" CssClass="upload" runat="server" />
                             <asp:Label ID="lblMsg" runat="server"></asp:Label>    
                        </div>
                        <div class="span1"><asp:LinkButton ID="LinkBtnUpload" runat="server"  class="btn btn-small btn-success"  OnClientClick="progressbar();" OnClick="LinkBtnUpload_Click" ><i class="icon-upload-alt"></i>   <b>Upload</b></asp:LinkButton></div>
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
					        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Supplier List</h2>
					        <div class="box-icon">
						        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
						        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
					        </div>
			            </div>
				        <div class="box-content">
                            <div class="scrollit">
                                <div class="div_print">
                                    <asp:GridView ID="GVSupplier" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="table table-striped table-bordered" DataKeyNames="Sup_Code,supplierId" OnRowCommand="GVSupplier_RowCommand" OnRowDeleting="GVSupplier_RowDeleting" >
                                        <Columns>
                                            <asp:BoundField DataField="supplierId" HeaderText="ID" ReadOnly="True" SortExpression="supplierId" />
                                            <asp:BoundField DataField="suppliername" HeaderText="Supplier Name" SortExpression="suppliername" />                                        
                                            <asp:BoundField DataField="contactperson" HeaderText="Contact Person" SortExpression="contactperson" />
                                            <asp:BoundField DataField="City_" HeaderText="City" SortExpression="City_" />
                                            <asp:BoundField DataField="phoneno" HeaderText="Phone No" SortExpression="phoneno" />
                                            <asp:BoundField DataField="NTNNTRNo" HeaderText="NTN/NTR No" SortExpression="NTNNTRNo" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" Text="Select" ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" CommandName="Delete" runat="server" OnClientClick="" Text="Delete"></asp:LinkButton>
                                                    <asp:HiddenField ID="HFSupplierID" runat="server" Value='<%# Eval("supplierId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
                <div id="showsupplier" >                    
                    <div class="row-fluid sortable">
                        <div class="box span12">
				                    <div class="box-header" data-original-title>
                                        <h2><i class="halflings-icon align-justify"></i><span class="break"></span> Add/Edit Supplier </h2>
					                    <div class="box-icon">
						                    <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>						                
					                    </div>
			                        </div>
				                    <div class="box-content">
                                        <div class="box">
                                            <div class="span12">&nbsp;</div>
                                            <div class="alert alert-error" id="alerts" style="display:none;">
							                    <strong>Notification !</strong>
                                                <label id="lblerrorSupplierName"> Please Write some thing in the Supplier Name!!</label>                                               
                                                <label id="lblPhone">Please write The Valid Phone Num!!</label>
                                                <label id="lblCellNo">Please Write Cell Number!!</label>                                               
                                                <label id="lblInt">Please Write Number Only!!</label>
                                                <label id="lblDesignation">Please Write Designation!!</label>
                                                <label id="lblAddressOne">Please Write Address One!!</label>                                                
                                                <label id="lblNIC">Please Write CNIC!!</label>
                                                <label id="lblBusinessNature">Please Write Business Nature!!</label>
                                               
						                    </div>
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
						                            <label>
							                            <span>Company Name:</span><%--"--%><input  name="Supplier" placeholder="ex. John f. Kennedy..." type="text" runat="server" id="TBSupplierName" class="SupplierName" onblur="SuppEmptyName();" /></label></div>
                                                          <asp:Label ID="v_cname" runat="server" Font-Bold="true" ForeColor="Red" Text="" ></asp:Label>
                                                 </div>
                                            <div class="span6">
                                                <div class="item">
						                            <label>
							                            <span> Contact Person: </span>                                                        
							                            <input  name="ContactPerson" placeholder="ex. Contact Person..."  type="text" runat="server" id="TBContactPerson" />
						                            </label>
                                                    <asp:Label ID="v_conper" runat="server" Font-Bold="true" ForeColor="Red" Text="" ></asp:Label>
						                        </div>
                                            </div>                                        
                                        </div>
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
						                                <label>
							                                <span>  NTN/NTRNo: </span> <%--onblur="CheckEmptyNIC(); checkint(this.value,'.NIC');"--%>                                                       
							                                <input  name="NTNNTRNo" placeholder="ex. 8990-800909..."   type="text" runat="server" id="TBNTNNTRNo" class="NTNNTR"   />
						                                </label>						                                
					                                </div>
                                                </div>   
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span>   City: </span>                                                        
                                                        <asp:TextBox ID="TBCity" runat="server" placeholder="City.." AutoPostBack="true" OnTextChanged="TBCity_TextChanged"></asp:TextBox>                         
                                                              <asp:AutoCompleteExtender ServiceMethod="GetCity" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                        TargetControlID="TBCity" ID="AutoCompleteExtender1"  
                                                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
						                            </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span12">                                       
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
                                                            <span> Mobile No: </span><%--"--%>
                                                            <input  name="PhoneNo" placeholder="ex. +927810290090.."   type="text" runat="server" id="TBPhoneNo" class="Phone" onblur="CheckEmptyPhone(); checkint(this.value,'.Phone');" />
                                                    </label>
                                                </div>
                                            </div>                                            
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span> Address: </span>
                                                        <input   placeholder="ex. 26th Street North Avenue.."   type="text" runat="server" id="TBAddressTwo" />
						                            </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span> CNIC: </span><%--onblur="CheckEmptyNIC(); checkint(this.value,'.NIC');"--%>
                                                        <input  name="NIC" placeholder="ex. 78991-89728000-9 ..."  type="text" runat="server" id="TBNIC" class="NIC"   />
						                            </label>
                                                </div>
                                            </div>
                                            <div class="span6">
                                                <div class="item">
                                                    <label>
							                            <span> Url: </span>
                                                        <input   placeholder="ex. http://www.alsehatpharma.com.."  type="text" runat="server" id="TBUrl" />
						                            </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
						                            <label>
							                            <span>  Email: </span>                                                        
							                            <input  name="Email" placeholder="ex. john@live.com.."  type="text" runat="server" id="TBEmail" />
						                            </label>
					                            </div>
                                            </div>
                                            <div class="span6">
                                                <div class="item">
						                            <label>
							                            <span> Pre Bal: </span>
                                                        <asp:TextBox ID="TBPrevBal" runat="server"></asp:TextBox>
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
                                                   <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
							                       <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="btnEdit_Click" />
                                                   <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                            </div>
                                            
                                            <div class="span2">
                                                <asp:HiddenField ID="HFSupplierID" runat="server" />
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
    

            
                                            <div class="span6" style="display:none" >
                                                <div class="item">
                                                    <label>
							                            <span> Designation: </span>
                                                        <input  name="designation" placeholder="ex. Software Engineer.." runat="server" id="TBDesignation" class="Designation" onblur="CheckEmptyDesignation();" />
						                            </label>
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
                <asp:LinkButton ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" ></asp:LinkButton>
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
                <asp:LinkButton ID="LinkBtnCityInsert" runat="server" CssClass="btn btn-success" Text="Create" OnClientClick="return checkcity();" OnClick="LinkBtnCityInsert_Click" ></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton  ID="LinkBtnCityCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" ></asp:LinkButton>                                     

			</div>
            <asp:HiddenField ID="HFCity" runat="server" />
		</div>
		</div>
	</div>

    <asp:HiddenField ID="HFSupID" runat="server" />

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
            <div class="modl-footer">
                <asp:LinkButton ID="linkmodaldelete" runat="server" CssClass="btn btn-success" Text="Yes" OnClick="linkmodaldelete_Click"></asp:LinkButton>
                <asp:LinkButton ID="linkbtncanceldel" runat="server" CssClass="btn btn-success" Text="No"></asp:LinkButton>
                <asp:HiddenField ID="HFCustId" runat="server" />
			</div>
		</div>
		</div>
	</div>
    <asp:HiddenField ID="HFSubHeadCatID" runat="server" />
    <asp:HiddenField ID="SubHeadCat" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../Controller/Supplier.js"></script>
    <script src="../Controller/Common.js"></script>


            <script type="text/javascript">

                function DisplayLoadingImage() {

                    //if ($(".Search").val() == '') {
                    //    alert('a');
                    //    document.getElementById("HiddenLoadingImage").style.display = "none";
                       
                    //} else {
                    //    alert('b');
                    //    document.getElementById("HiddenLoadingImage").style.display = "block";
                    //    $("body").css({ opacity: 0.9 });
                    //}
                };
            </script>               
</asp:Content>
