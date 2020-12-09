<%@ Page Title="Quotation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="MProQuot.aspx.cs" Inherits="Foods.MProQuot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        <li>
            <a href="#">Purchase</a> 
		    <i class="icon-angle-right"></i>
        </li>
	    <li><a href="MProQuot.aspx">Quotations</a></li>
    </ul>
        
    <!-- imageLoader - START -->

        <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
<div class="row-fluid sortable">
	    <div class="box span12">
			<div class="box-header" data-original-title>
				<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Quotations</h2>
				<div class="box-icon">
				</div>
			</div>
			<div class="box-content">
                <div class="span12">
                    <div style="text-align:center">
                        <asp:Label ID="lbl_err" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                 </div>
                <div class="span12">
                    <div class="control-group  span10">                                                  
					    <label class="control-label span2" for="appendedInputButton">Search</label>
					    <div class="controls">
                                <div class="input-append span4">
                                    <asp:TextBox ID="TBQuotations" runat="server" size="16" AutoPostBack="true" placeholder="Search Quotations..." CssClass="Search"   OnBlur="DisplayLoadingImage();" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!" OnClick="SeacrhBtn_Click"/>
    						    </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <div class="span2">
                                <!--<a href="#" class="btn btn-small btnadd btn-primary"  onclick="ShowQuotations();" runat="server" id="btnadd" ><i class="icon-plus"></i> <b>Add New Quotations</b></a>-->
                            </div>
					    </div>
				    </div>
                 </div>
                <div class="box span12">
					<div class="box-header" data-original-title>
				        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Quotations List</h2>
				        <div class="box-icon">
					        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
					        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
				        </div>
			        </div>	
                    <div class="box-content">                    
                        <div class="scrollit">
                            <div class="div_print">
                                <asp:GridView ID="GVQuot" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="True" CssClass="table table-striped table-bordered" DataKeyNames="MProQuot_id" OnPageIndexChanging="GVQuot_PageIndexChanging" OnRowCommand="GVQuot_RowCommand" OnRowDeleting="GVQuot_RowDeleting" >
                                    <Columns>
                                        <asp:BoundField DataField="MProQuot_sono" HeaderText="ID" SortExpression="MProQuot_sono" />
                                        <asp:BoundField DataField="MProQuot_dat" HeaderText="Date" SortExpression="MProQuot_dat" />
                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" SortExpression="CustomerName" />                                        
                                        <asp:BoundField DataField="MProQuot_rmk" HeaderText="Remarks" SortExpression="MProQuot_rmk" />                                        
                                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                                        <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" CssClass="btn btn-success" ><i class="halflings-icon white zoom-in"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" CommandName="Delete" CssClass="btn btn-danger" runat="server" > <i class="halflings-icon white trash"></i> </asp:LinkButton>
                                                <asp:HiddenField ID="HFQuotID" runat="server" Value='<%# Eval("MProQuot_id")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkShow" CommandName="Show"  runat="server" Text="Show" ></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                    </Columns>
                                </asp:GridView> 
                            </div>
                        </div>
                    </div>		
                </div>                
                <div class="box span12">
					<div class="box-header" data-original-title>
				        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Add/Edit Quotations</h2>
				        <div class="box-icon">
					        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
					        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
				        </div>
			        </div>	
                    <div class="box-content">
                        <div class="span12">
                            <div class="span12">
                                <div class="alert alert-error" id="alerts"  style="display:none;">
							        <strong>Error!</strong>
                                    <label id="lblErrorProductName"> Please Write some thing in the Product Name!!</label>
                                    <label id="lblErrorProductQuantity"> Please Write some thing in the Product Quantity!!</label>
                                    <label id="lblErrorProductUnits">Please Select The Valid Product Units!!</label>
                                    <label id="lblErrorQuotedBy">Please Select Quoted By!!</label>
                                    <label id="lblErrorInt">Please Write Number Only!!</label>
                                    <label id="lblErrorQuotedTo">Please Select Quoted To!!</label>
                                    <label id="lblErrorDate">Please Write Date!!</label>
                                </div>
                              </div>                                                              
                        </div>               
                        <div class="span12">                            
                            <div class="span6">
						            <label class="span4">
							            <span>Quotation NO</span>                                                        
                                    </label>
                                    <div class="span8">
                                        <asp:TextBox ID="txtsono" runat="server"/>
                                    </div>						            
						        </div>
                        </div>         
                        <div class="span12">                            
                            <div class="span6">
						            <label class="span4">
							            <span>Date</span>                                                        
                                    </label>
                                    <div class="span8">
                                        <asp:TextBox ID="TBQuot_dat" runat="server" placeholder="ex. 12/10/2015..." class="datepicker" />
                                    </div>						            
						    </div>
                            <div class="span6">
						            <label class="span3">
							            <span>Is Active</span>                                                        
                                    </label>
                                    <div class="span2">
                                        <asp:CheckBox ID="ck_act" runat="server" />
                                    </div>						            
						        </div>
                        </div>
                         <div class="span12">                                                        
						    <label class="span2">
							    <span>Customer</span>                                                        
                            </label>
                            <div class="span10">
                                <asp:DropDownList ID="DDL_cust" runat="server" data-rel="chosen"></asp:DropDownList>
                                <%--<asp:TextBox ID="TBCust" runat="server" class="span11"></asp:TextBox>--%>
                            </div>						            
                        </div>
                        <div class="span12">                            
                            <label class="span2">
                                <span>Remarks</span>                                                        
                            </label>
                            <div class="span10">
                                <asp:TextBox ID="TBRmk" runat="server" class="span11" TextMode="MultiLine"></asp:TextBox>
                            </div>						            
                        </div>
                        <div class="span12">                            
                           	<asp:GridView ID="GVDetQuot" runat="server" AutoGenerateColumns="False"  class="table table-striped table-bordered" OnRowDeleting="GVDetQuot_RowDeleting"  >
                                <Columns>
                                     <asp:TemplateField HeaderText="PRODUCTS">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="DDL_Pro" runat="server" data-rel="chosen" style="width:70px; height:20px; background:none; border:none;"></asp:DropDownList>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="QUANTITY">
                                         <ItemTemplate>
                                             <asp:TextBox ID="TBProQty" runat="server"  style="width:120px; height:20px; background:none; border:none;" ></asp:TextBox>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="RATE">
                                         <ItemTemplate>
                                             <asp:TextBox ID="TBProrat" runat="server"  style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                         </ItemTemplate>                                       
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotl" Font-Bold="true" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="AMOUNT">
                                         <ItemTemplate>
                                             <asp:TextBox ID="TBAmt" runat="server"   style="width:100px; height:20px; background:none; border:none;" ></asp:TextBox>
                                         </ItemTemplate>                                                  
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NARRATION">
                                         <ItemTemplate>
                                             <asp:TextBox ID="TBNarr" runat="server" style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                         </ItemTemplate>                                      
                                     </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkbtnadd"  OnClick="linkbtnadd_Click" runat="server" ><i class="halflings-icon plus-sign"  ></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                     
                                    <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                                        <ControlStyle CssClass="halflings-icon minus-sign" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>				            
                        </div>                       
                        <div class="span12">
                            <div class="span8">
                            </div>
                            <div class="span2">
                                <asp:Label ID="lbl_grsstol" runat="server" Text="Total Amount"></asp:Label>
                            </div>
                            <div class="span2">
                                <asp:TextBox ID="TBGrssTotal" runat="server" class="span12" ></asp:TextBox>
                            </div>
                        </div>
                        <asp:HiddenField ID="HFQuotationID" runat="server" />
                        <asp:HiddenField ID="HFCreatedBy" runat="server" />
                        <asp:HiddenField ID="HFCreatedAt" runat="server" />
                        <asp:HiddenField ID="HFDQuotID" runat="server" />
                    </div>
                    <div class="clearfix"></div>
                    <div class="form-actions">
                        <div class="span11">&nbsp;</div>
                            <div class="span6">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click"   />
                                    <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                            </div>
    					</div>
                </div>                
            </div>
        </div>	    
    </div>
</asp:Content>
