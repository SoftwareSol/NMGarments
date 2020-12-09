<%@ Page Title="Salary Sheet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="frm_mSalary.aspx.cs" Inherits="Foods.frm_mSalary" %>
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
        <li><a href="frm_mSalary.aspx">Employees Salary Sheet</a></li>
    </ul>
      
    
    
    <!-- imageLoader - START -->
    
    <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->    

    <div class="span12">&nbsp;</div>
    <div class="row-fluid sortable">       
        <div class="box span12">
               <div class="box-header" data-original-title>
				<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Employees Salary Sheet</h2>
				<div class="box-icon">
				</div>
			</div>
			<div class="box-content">
                <div class="span11">

                <div class="control-group  span10">
                                                  
					<label class="control-label span1" for="appendedInputButton">Search</label>
					<div class="controls">
                            <div class="input-append span4">
                                <asp:TextBox ID="TBSearch" runat="server" size="16" AutoPostBack="true" placeholder="Search Employees..." CssClass="Search" OnTextChanged="TBSearch_TextChanged" OnBlur="DisplayLoadingImage();" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!"/>
    						</div>
                        <div class="span2">
                        </div>
					</div>
				</div>
                </div>
                
                <div class="span12">&nbsp;</div>

                <div class="row-fluid sortable">		
	                    <div class="box span12">
				        <div class="box-header" data-original-title>
					        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Employees List</h2>
					        <div class="box-icon">
						        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
						        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
					        </div>
			            </div>
				        <div class="box-content">
                            <div class="scrollit">
                                <div class="div_print">
                                    <asp:GridView ID="GVEmp" runat="server" AutoGenerateColumns="False" ShowHeader="true" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" DataKeyNames="MSal_ID" OnRowCommand="GVEmp_RowCommand" OnRowDeleting="GVEmp_RowDeleting" >
                                <Columns>
                                    <asp:BoundField DataField="MSal_ID" HeaderText="ID" ReadOnly="True" SortExpression="MSal_ID" />
                                    <asp:BoundField DataField="employeeName" HeaderText="Employee Name" SortExpression="employeeName" />     
                                    <asp:BoundField DataField="create_by" HeaderText="Created By" SortExpression="create_by" />
                                    <asp:BoundField DataField="craete_at" HeaderText="Created At" SortExpression="craete_at" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" Text="Select" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" CommandName="Delete"  runat="server" OnClientClick="" Text="Delete" > </asp:LinkButton>
                                            <asp:HiddenField ID="HFMSal_ID" runat="server" Value='<%# Eval("MSal_ID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRpt" CommandName="Show" runat="server" Text="Show" ></asp:LinkButton>
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
                                        <h2><i class="halflings-icon align-justify"></i><span class="break"></span> Add/Edit Employee Salary Sheet </h2>
					                    <div class="box-icon">
						                    <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>						                
					                    </div>
			                        </div>
				                    <div class="box-content">
                                        <div class="box"> 
                                        <div class="span12"></div>                                           
                                        <div class="span12">
                                            <div class="span6">
                                                <div class="item">
						                            <label>
							                            <span>Employee</span>							                            
						                            </label>
                                                    <asp:DropDownList ID="DDLEmp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLEmp_SelectedIndexChanged"></asp:DropDownList>
					                            </div>
                                            </div>
                                            <div class="span6">                                                
                                            </div>                                        
                                        </div>
                                        <fieldset>
                                            <legend>Salary Information</legend>
                                                <div class="span12">
                                                <div class="span6">
                                                    <div class="item">
						                                <label>
							                                <span>Basic Salary</span>							                            
						                                </label>
                                                        <asp:TextBox ID="TB_BS" runat="server" placeholder="Basic Salary.." Text="0" ></asp:TextBox>
					                                </div>
                                                </div>
                                                <div class="span6"> 
                                                    <div class="item">
						                                <label>
							                                <span>Increament</span>							                            
						                                </label>
                                                        <asp:TextBox ID="TB_INC" runat="server" placeholder="Increament.." Text="0" ></asp:TextBox>
					                                </div>                                               
                                                </div>                                        
                                            </div>
                                                <div class="span12">
                                                    <div class="span6">
                                                        <div class="item">
						                                    <label>
							                                    <span>Sale Per Day</span>							                            
						                                    </label>
                                                            <asp:TextBox ID="TB_SalPerDay" runat="server" placeholder="Sale Per Day.." Text="0" ></asp:TextBox>
					                                    </div>
                                                    </div>
                                                    <div class="span6"> 
                                                        <div class="item">
						                                    <label>
							                                    <span>Sale Per Hr</span>							                            
						                                    </label> 
                                                            <asp:TextBox ID="TB_SalPerHR" runat="server" placeholder="Sale Per Hour.." Text="0"></asp:TextBox>
					                                    </div>                                               
                                                    </div>                                        
                                                </div>
                                                <div class="span12">
                                                <div class="span6">
                                                    <div class="item">
						                                <label>
							                                <span>Days Absent</span>							                            
						                                </label>
                                                        <asp:TextBox ID="TB_DyAbsnt" runat="server" placeholder="Days Absent.." Text="0" ></asp:TextBox>
					                                </div>
                                                </div>
                                                <div class="span6"> 
                                                    <div class="item">
						                                <label>
							                                <span>Days Attendance</span>							                            
						                                </label>
                                                        <asp:TextBox ID="TB_DyAttn" runat="server" placeholder="Sale Per Hour.." Text="0" ></asp:TextBox>
					                                </div>                                               
                                                </div>                                        
                                            </div>
                                            </fieldset>
                                            <fieldset>
                                                <legend>Over Time</legend>
                                                <div class="span12">
                                            <div class="span6">
                                                <div class="item">
						                            <label>
							                            <span>Hours</span>							                            
						                            </label>
                                                    <asp:TextBox ID="TBHRS" runat="server" placeholder="Hours .." Text="0" ></asp:TextBox>
					                            </div>
                                            </div>
                                            <div class="span6"> 
                                                <div class="item">
						                            <label>
							                            <span>Days</span>							                            
						                            </label>
                                                    <asp:TextBox ID="TBDays" runat="server" placeholder="Days .." Text="0" ></asp:TextBox>
					                            </div>                                               
                                            </div>                                        
                                        </div>
                                            <div class="span12">
                                                <div class="span6">
                                                    <div class="item">
						                                <label>
							                                <span>Amount</span>							                            
						                                </label>
                                                        <asp:TextBox ID="TBOTAmt" runat="server" placeholder="Amount .." Text="0" ></asp:TextBox>
					                                </div>
                                                </div>
                                                <div class="span6"> 
                                                </div>                                        
                                            </div>
                                            </fieldset>
                                            <fieldset>
                                                <legend>Deduction</legend>
                                                <div class="span12">
                                                    <div class="span6">
                                                        <div class="item">
						                                    <label>
							                                    <span> Advance Deduction </span>							                            
						                                    </label>
                                                            <asp:TextBox ID="TBAdvDTC" runat="server" placeholder="Advance Deduction .." Text="0" ></asp:TextBox>
					                                    </div>
                                                    </div>
                                                    <div class="span6"> 
                                                        <div class="item">
						                                    <label>
							                                    <span> Loan Deduction </span>							                            
						                                    </label>
                                                            <asp:TextBox ID="TBLNDTC" runat="server" placeholder="Loan Deduction .." Text="0" ></asp:TextBox>
					                                    </div>                                               
                                                    </div>                                        
                                               </div>
                                            </fieldset> 
                                            <fieldset>
                                                <legend>Filling Section</legend>
                                                <div class="span12">
                                                    <div class="span6">
                                                        <div class="item">
						                                    <label>
							                                    <span> Cartons Types </span>							                            
						                                    </label>
                                                            <asp:TextBox ID="TBCrtnTyp" runat="server" placeholder="Cartons Types .." Text="-" ></asp:TextBox>
					                                    </div>
                                                    </div>
                                                    <div class="span6"> 
                                                        <div class="item">
						                                    <label>
							                                    <span> Cartons Price </span>							                            
						                                    </label>
                                                            <asp:TextBox ID="TBCrtnPric" runat="server" placeholder="Cartons Price .." Text="0" ></asp:TextBox>
					                                    </div>                                               
                                                    </div>                                        
                                               </div>
                                               <div class="span12">
                                                    <div class="span6">
                                                        <div class="item">
						                                    <label>
							                                    <span> Bottles Types </span>							                            
						                                    </label>
                                                            <asp:TextBox ID="TBBtlsTyp" runat="server" placeholder="Bottles Types .." Text="-" ></asp:TextBox>
					                                    </div>
                                                    </div>
                                                    <div class="span6"> 
                                                        <div class="item">
						                                    <label>
							                                    <span> Bottles Price </span>							                            
						                                    </label>
                                                            <asp:TextBox ID="TBBtlsPric" runat="server" placeholder="Bottles Price .." Text="0" ></asp:TextBox>
					                                    </div>                                               
                                                    </div>                                        
                                               </div>
                                            </fieldset>                                                                     
                                        <div class="clearfix"></div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="span4"></div>
                                            <div class="span6">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click"/>							                        
                                                    <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                            </div>                                            
                                            <div class="span2">
                                                <asp:HiddenField ID="HFMSalary_ID" runat="server" />
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
                <asp:LinkButton ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" OnClick="btnalertOk_Click"></asp:LinkButton>
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
                <asp:LinkButton ID="linkmodaldelete" runat="server" CssClass="btn btn-success" Text="Yes" OnClick="linkmodaldelete_Click"></asp:LinkButton>
                <asp:LinkButton ID="linkbtncanceldel" runat="server" CssClass="btn btn-success" Text="No"></asp:LinkButton>
                <asp:HiddenField ID="HFCustId" runat="server" />
			</div>
		</div>
		</div>
	</div>
    <asp:HiddenField ID="HFEmpSalID" runat="server" />

  </ContentTemplate>
        </asp:UpdatePanel>

    <script src="Controller/Customers.js"></script>
    <script src="Controller/Common.js"></script>


            <script type="text/javascript">

                function DisplayLoadingImage() {

                    if ($(".Search").val() == '') {

                        document.getElementById("HiddenLoadingImage").style.display = "none";

                    } else {

                        document.getElementById("HiddenLoadingImage").style.display = "block";
                        $("body").css({ opacity: 0.9 });
                    }
                };

            </script>                        


</asp:Content>
