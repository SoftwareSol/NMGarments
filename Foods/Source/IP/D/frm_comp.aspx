<%@ Page Title="Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="frm_comp.aspx.cs" Inherits="Foods.frm_comp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

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

   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
	    <li>
		    <i class="icon-home"></i>
		    <a href="WellCome.aspx">Home</a> 
		    <i class="icon-angle-right"></i>
	    </li>
	    <li><a href="#">Set Up</a>
   		    <i class="icon-angle-right"></i>
	    </li>
        <li><a href="frm_comp.aspx">Company</a></li>
    </ul>
      
    <!-- imageLoader - START -->
    
    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
    <div class="row-fluid sortable">       
        <div class="box span12">
            <div class="box-content">
                <div class="span11">
                    <div class="control-group  span10">
					    <label class="control-label span2" for="appendedInputButton">Search</label>
					    <div class="controls">
                            <div class="input-append span4">
                                <asp:TextBox ID="TBSearch" runat="server" size="16" placeholder="Search ompany..." CssClass="Search"  /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!"/>
                                <!--<a href="#" class="btn btn-primary"  onclick="showcustomer();" runat="server" id="btnadd" style="margin-left:20px;" ><i class="icon-plus"></i></a>-->
                            </div>
					    </div>
                    </div>
                </div>
                <div class="span12">&nbsp;</div>
                <div class="row-fluid sortable">
                    <div class="box span12">
				        <div class="box-header" data-original-title>
					        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Companies List</h2>
					        <div class="box-icon">
						        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
						        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
					        </div>
			            </div>
				        <div class="box-content">
                            <div class="scrollit">
                                <div class="div_print">
                                    <asp:GridView ID="GVComp" runat="server" AutoGenerateColumns="False" ShowHeader="true" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" DataKeyNames="Username" OnRowDeleting="GVComp_RowDeleting" >
                                        <Columns>
                                            <asp:BoundField DataField="CompanyId" HeaderText="Company ID" ReadOnly="True" SortExpression="CompanyId" />
                                            <asp:BoundField DataField="BranchId" HeaderText="Branch ID" SortExpression="BranchId" />                                        
                                            <asp:BoundField DataField="Username" HeaderText="User Name" SortExpression="Username" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" Text="Select" ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField> 
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" CommandName="Delete"  runat="server" OnClientClick="" Text="Delete" > </asp:LinkButton>
                                                    <asp:HiddenField ID="HFUsername" runat="server" Value='<%# Eval("Username")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid sortable">
                        <div class="box span12">
				            <div class="box-header" data-original-title>
                                <h2><i class="halflings-icon align-justify"></i><span class="break"></span> Add/Edit Company </h2>
					            <div class="box-icon">
						            <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>						                
					            </div>
			                </div>
				            <div class="box-content">
                                <div class="box">
                                    <div class="span12">&nbsp;</div>
                                    <div class="span12">
                                        <div class="span6">
                                            <div class="item">
						                        <label>
							                        <span>Company ID</span>
							                        <input  name="compid" placeholder="ex. Company ID..." type="text" runat="server" id="TBcompid"  />
						                        </label>
					                        </div>
                                        </div>
                                        <div class="span6">
                                            <div class="item">
						                        <label>
							                        <span> Company </span>    
                                                    <asp:DropDownList ID="DDL_ComNam" runat="server"></asp:DropDownList>
						                        </label>
						                    </div>
                                        </div>                                        
                                    </div>
                                    <div class="span12">
                                        <div class="span6">
                                            <div class="item">
						                        <label>
                                                    <span> Branch ID </span>
                                                    <input  name="desig" placeholder="ex. Branch ID..."  type="text" runat="server" id="TBBrchID" />
                                                </label>
                                            </div>    
                                        </div>
                                        <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> User Name: </span>
                                                    <input  name="phno" placeholder="ex. User Name..."  type="text" runat="server" id="TBuname" />
						                        </label>
                                            </div>
                                            </div>
                                        </div>                                    
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-actions">
                                    <div class="span4"></div>
                                    <div class="span6">
                                        <%--OnClientClick="return check();"--%>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
                                        <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                    </div> 
                                    <div class="span2">
                                        <asp:HiddenField ID="HFUsrNam" runat="server" />
                                    </div>
							    </div>
                                <div class="span12"></div>
                            </div>
                        </div>
                 </div>		
		    </div>
		</div>
    </div>
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
                <asp:LinkButton ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK"></asp:LinkButton>
                    <%--<asp:Button ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" />--%>
			</div>
		</div>
		</div>
	</div>
    <div id="MyModalDelete" class="modal fade" style="display:none;">
		<div class="modal-dialog">
		    <div class="modal-content">
			    <div class="modal-header">
				    <h4 class="modal-title">Error!</h4>
			    </div>
			    <!-- dialog body -->
			    <div class="modal-body">
				    <asp:Label ID="lblmodaldelete" runat="server"></asp:Label>
                
			    </div>
			    <!-- dialog buttons -->
                <div class="modal-footer">
                    <asp:LinkButton ID="linkmodaldelete" runat="server" CssClass="btn btn-success" Text="Yes" ></asp:LinkButton>
                    <asp:LinkButton ID="linkbtncanceldel" runat="server" CssClass="btn btn-success" Text="No"></asp:LinkButton>
                    <asp:HiddenField ID="HFUsrId" runat="server" />
			    </div>
		    </div>
		</div>
	</div>
     <script src="Controller/Employees.js"></script>
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
