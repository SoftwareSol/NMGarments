<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frm_Emp.aspx.cs"
     EnableEventValidation = "false" Inherits="Foods.frm_Emp" %>

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
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
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
        <li><a href="frm_Emp.aspx">Employees</a></li>
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
                                <asp:TextBox ID="TBSearch" runat="server" size="16" AutoPostBack="true" placeholder="Search Employees..." CssClass="Search" OnTextChanged="TBSearch_TextChanged" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!"/>
                                <a href="#" class="btn btn-primary"  onclick="showempyee();" runat="server" id="btnadd" style="margin-left:20px;" ><i class="icon-plus"></i></a>
                            </div>
					    </div>
                    </div>
                </div>
                <div class="span12">&nbsp;</div>
                <div class="row-fluid sortable">
                    <div class="box span12">
				        <div class="box-header" data-original-title>
					        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Employee List</h2>
					        <div class="box-icon">
						        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
						        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
					        </div>
			            </div>
				        <div class="box-content">
                            <div class="scrollit">
                                <div class="div_print">
                                    <asp:GridView ID="GVEmp" runat="server" AutoGenerateColumns="False" ShowHeader="true" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" DataKeyNames="Username" OnRowDeleting="GVEmp_RowDeleting" OnRowCommand="GVEmp_RowCommand" >
                                        <Columns>
                                            <asp:BoundField DataField="Username" HeaderText="User Name" ReadOnly="True" SortExpression="Username" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />                                        
                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MobileNo" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server"  OnClientClick="showempyee();" Text="Select" ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField> 
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" CommandName="Delete"  runat="server"  Text="Delete" > </asp:LinkButton>
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
                <div class="row-fluid sortable" id="showemp" >
                        <div class="box span12">
				            <div class="box-header" data-original-title>
                                <h2><i class="halflings-icon align-justify"></i><span class="break"></span> Add/Edit Employees </h2>
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
							                        <span>Name</span>
							                        <input  name="name" placeholder="ex. Name..." type="text" runat="server" id="TBEmpName"  /><asp:Label ID="v_employee" runat="server" ForeColor="Red" Text="" Font-Bold="true"></asp:Label>
						                        </label>
					                        </div>
                                        </div>
                                        <div class="span6">
                                            <div class="item">
						                        <label>
							                        <span> Address </span>                                                        
							                        <input  name="add" placeholder="Address..."  type="text" runat="server" id="TBAdd" />
						                        </label>
						                    </div>
                                        </div>                                        
                                    </div>
                                    <div class="span12">
                                        <div class="span6">
                                            <div class="item">
						                        <label>
                                                    <span> Designation </span>
                                                    <input  name="desig" placeholder="Manager..."  type="text" runat="server" id="TBdesig" /><asp:Label ID="v_desc" runat="server" ForeColor="Red" Text="" Font-Bold="true"></asp:Label>
                                                </label>
                                            </div>    
                                        </div>                                       
                                        <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> Tele Phone No: </span>
                                                    <input  name="phno" placeholder="ex. 021-67868989..."  type="text" runat="server" id="TBphno" />
						                        </label>
                                            </div>
                                        </div>
                                        </div>                                    
                                    <div class="span12">          
                                        <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> Mobile No: </span>
                                                    <input  name="mbno" placeholder="ex. 0321789897..."   type="text" runat="server" id="TBmbno" />
						                        </label>
                                            </div>
                                        </div>
                                        <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> Email: </span>
                                                    <input  name="emal" placeholder="ex. hambros@live.com..."  type="text" runat="server" id="TBemal" />
						                        </label>
                                            </div>
                                        </div>  
                                    </div>
                                    <div class="span12">          
                                        <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> Salary: </span>
                                                    <input  name="sal" placeholder="ex. Salary..."   type="text" runat="server" id="TBSal" />
						                        </label>
                                            </div>
                                        </div>  
                                         <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> Role: </span>
                                                    <asp:DropDownList ID="DDL_Desig" runat="server">
                                                        <asp:ListItem>--Select Role--</asp:ListItem>
                                                        <asp:ListItem Value="1">Supervisor</asp:ListItem>
                                                        <asp:ListItem Value="8">Salesman</asp:ListItem>
                                                    </asp:DropDownList>
						                        </label>
                                            </div>
                                        </div>                                        
                                    </div>
                                    <div class="span12">
                                        <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> Can Change Password: </span>
                                                    <input  name="chgpass"  type="checkbox" runat="server" id="chkchgpass" />
						                        </label>
                                            </div>
                                        </div>
                                        <div class="span6">
                                            <div class="item">
                                                <label>
							                        <span> Account Disabled: </span>
                                                    <input  name="accdisbl"  type="checkbox" runat="server" id="chkaccdisbl" />
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
                                        <asp:HiddenField ID="HFAccountCategoryName" runat="server" />
                                        <asp:HiddenField ID="HFSubHead" runat="server" />
                                        
                                    </div>
							    </div>
                                <div class="span12">
                                    <div class="span6" style="display:none;">
                                            <div class="item">
                                                <label>
                                                    <span> Fax No: </span>
                                                    <input  name="fxno" placeholder="ex. 021-67868989..."  type="text" runat="server" id="TBfxno" />                                                
                                                </label>
                                            </div>
                                    </div>
                                </div>
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
                    <asp:LinkButton ID="linkmodaldelete" runat="server" CssClass="btn btn-success" Text="Yes" OnClick="linkmodaldelete_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="linkbtncanceldel" runat="server" CssClass="btn btn-success" Text="No"></asp:LinkButton>
                    <asp:HiddenField ID="HFUsrId" runat="server" />
			    </div>
		    </div>
		</div>
	</div>
            </ContentTemplate>
    </asp:UpdatePanel>
     <script src="../Controller/Employees.js"></script>
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
