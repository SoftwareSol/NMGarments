<%@ Page Title="Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="frm_company.aspx.cs" Inherits="Foods.frm_company" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
                 /* Modal popUp Start */

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
        .modalPopup{
                border: 3px solid #000000;
                background-color: #FFFFFF;
                margin-top: 0px;
                color: #000000;
                margin-right: -3px;
                margin-bottom: 0px;
        }

        .modalPopup1{
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

        /* Modal popUp End */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<%--    <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
        <ContentTemplate>  
            </ContentTemplate>
    </asp:UpdatePanel>--%>
      
            <ul class="breadcrumb">
				<li>
					<i class="icon-home"></i>
					<a href="index.html">Home</a> 
					<i class="icon-angle-right"></i>
				</li>
				<li><a href="frm_company.aspx">Company</a></li>
			</ul>
           <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header" data-original-title>
						<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Company</h2>
						<div class="box-icon">
						</div>
					</div>
					<div class="box-content">
                        <asp:Label ID="lblCompany" runat="server" style="color:red;"></asp:Label>
                        <div class="span8">
                        <div class="control-group  span12">
							<label class="control-label span2" for="appendedInputButton">Search</label>
							<div class="controls">
                                 <div class="input-append span6">
                                       <asp:TextBox ID="TBSearchCompany" runat="server" size="16" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!" OnClick="SeacrhBtn_Click" />
    						    </div>
                              <%--  <asp:LinkButton ID="btnAdd" runat="server" CssClass="quick-button-small span1"  OnClientClick="show();">
                                    <i class="icon-plus"></i> onclick="show();"
                                    <p></p>
                                </asp:LinkButton>--%>
                                <a href="#" class="btn btn-small btn-primary"  onclick="ModalPopUp();" style="margin-left:20px;">
                                    <i class="icon-plus"></i>
                                    <p></p>
                                </a>
							</div>
                           

				        </div>
                     </div>
                        
                        <div class="row-fluid sortable">		
	                         <div class="box span12">
				                <div class="box-header" data-original-title>
					                <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Company List</h2>
					                <div class="box-icon">
						                <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
						                <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
					                </div>
			                    </div>
				                <div class="box-content">
                                    <asp:GridView ID="GVCompany" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="5" DataKeyNames="CompanyId" CssClass="table table-striped table-bordered" OnRowCommand="GVfrm_companyRowCommand" OnPageIndexChanging="GVfrm_companyPageIndexChanging" OnRowDeleting="GVfrm_companyRowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="CompanyId" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="CompanyId" />
                                            <asp:BoundField DataField="Name" HeaderText="Company" SortExpression="Name" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" >Select</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" CommandName="Delete" runat="server" OnClick="lnkDelete_Click"> Delete </asp:LinkButton>
                                                    <asp:HiddenField ID="HFCompanyID" runat="server" Value='<%# Eval("CompanyId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                               </div>
                        </div>
                    </div>
                </div>
            </div>
           </div>
    	  
    
    <div id="myModal" class="modal fade" style="display:none;">
			  <div class="modal-dialog">
				<div class="modal-content">
					<div class="modal-header">
						<%--<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
						<h4 class="modal-title">Add/Edit Company</h4>
					</div>
					<!-- dialog body -->
					<div class="modal-body">
					 <table class="table span12">
                         <tr>
                            <td class="span3">
                                <asp:Label ID="Label1" runat="server">Company ID</asp:Label>
                            </td>
                            <td class="span8">
                                <asp:Label ID="lbl_CompID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="span3">
                                <asp:Label ID="lbl_Company" runat="server">Company Name</asp:Label>
                            </td>
                            <td class="span8">
                                <asp:TextBox ID="TBCompany" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="span3">
                                <asp:Label ID="lblAdd" runat="server">Address</asp:Label>
                            </td>
                            <td class="span8">
                                <asp:TextBox ID="TbAdd" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="span3">
                                <asp:Label ID="lbl_ContctPer" runat="server">Contact Person</asp:Label>
                            </td>
                            <td class="span8">
                                <asp:TextBox ID="TBContctPer" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="span3">
                                <asp:Label ID="lbl_TelNo" runat="server">TelePhone</asp:Label>
                            </td>
                            <td class="span8">
                                <asp:TextBox ID="TB_TelNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="span3">
                                <asp:Label ID="lbl_act" runat="server">Active</asp:Label>
                            </td>
                            <td class="span8">
                                <asp:CheckBox ID="ck_act" runat="server" />        
                            </td>
                        </tr>
                    </table>
					</div>
				  <!-- dialog buttons -->
					<div class="modal-footer">
                            <asp:Button ID="BtnCreateCompany" runat="server" CssClass="btn btn-success" Text="Create" OnClick="BtnCreateCompany_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnCancelCompany" runat="server" CssClass="btn btn-danger" Text="Cancel"  OnClick="BtnCancelCompany_Click" OnClientClick="show();" />
                            
					</div>
                    <asp:HiddenField ID="HFCompany" runat="server" />
				</div>
			  </div>
			</div>

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
    <%--<asp:Button ID="BtnCancelCompany" runat="server" CssClass="btn btn-danger" Text="Cancel"  OnClick="BtnCancelfrm_companyClick" OnClientClick="show();" />--%>						
</asp:Content>
