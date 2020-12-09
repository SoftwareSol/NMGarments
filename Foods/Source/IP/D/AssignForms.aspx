<%@ Page Title="Assign Pages" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="AssignForms.aspx.cs" Inherits="Foods.AssignForms" %>
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
				<li><a href="AssignForms.aspx">Assign Pages</a></li>
			</ul>
           <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header" data-original-title>
						<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Assign Pages</h2>
						<div class="box-icon">
						</div>
					</div>
					<div class="box-content">
                        <asp:Label ID="lblerr" runat="server" style="color:red;"></asp:Label>
                        <div class="span8">
                        <div class="control-group  span12">							
							<div class="controls">      
                                <div class="span12">
                                    <div class="item">
						                <label>
							                <span>Select Users</span>
						                </label>
                                        <asp:DropDownList ID="DDL_Usr" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Usr_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                               </div>         
                                <div class="row-fluid sortable">		
                                    <div class="box span6">
                                        <div class="box-header" data-original-title>
                                            <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Select Module</h2>
                                            <div class="box-icon">
                                            </div>
                                        </div>
                                        <div class="box-content">
                                            <div class="span8">
                                                <div class="control-group  span12">							
                                                    <div class="controls">                                                                
                                                        <div class="span5">
                                                        </div>
                                                        <div class="span6">
                                                            <div class="span12">
                                                                <div class="item">
                                                                    <label>
                                                                        <span></span>
                                                                    </label>
                                                                    <asp:GridView ID="GVModul" runat="server" AutoGenerateColumns="False" DataKeyNames="MenuId" >
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select Modules">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_slct" runat="server" />
                                                                                    <asp:HiddenField id="HFSlctModul" runat="server" Value='<%# Eval("MenuId")%>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>                          
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </div>	
                                    <div class="box span6">
                                        <div class="box-header" data-original-title>
                                            <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Select Pages</h2>
                                            <div class="box-icon">
                                            </div>
                                        </div>
                                        <div class="box-content">
                                            <div class="span8">
                                                <div class="control-group  span12">							
                                                    <div class="controls">                                                                
                                                        <div class="span5">
                                                        </div>
                                                        <div class="span6">
                                                            <div class="span12">
                                                                <div class="item">
                                                                    <label>
							                                            <span></span>
						                                            </label>
                                                                    <asp:GridView ID="GVSlectPgs" runat="server" AutoGenerateColumns="False" DataKeyNames="SubMenuId" >
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select Pages">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_slctpg" runat="server" />
                                                                                    <asp:HiddenField id="HFSlctPg" runat="server" Value='<%# Eval("SubMenuId")%>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="SubMenuNam" HeaderText="SubMenuNam" SortExpression="SubMenuNam" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>                          
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </div>	
                                </div>                 
                                <%--<a href="#" class="btn btn-small btn-primary"  onclick="ModalPopUp();" style="margin-left:20px;">
                                    <i class="icon-plus"></i>
                                    <p></p>
                                </a>--%>
							</div>
				        </div>
                     </div>
                </div>
            </div>
           </div>
           <div class="form-actions">
               <div class="span4"></div>
               <div class="span6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Assign" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
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
    <%--<asp:Button ID="BtnCancelCategory" runat="server" CssClass="btn btn-danger" Text="Cancel"  OnClick="BtnCancelAssignFormsClick" OnClientClick="show();" />--%>						
</asp:Content>
