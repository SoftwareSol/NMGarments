<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="ItmPricing.aspx.cs" Inherits="Foods.ItmPricing" %>
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
            <a href="#">Stock</a> 
		    <i class="icon-angle-right"></i>
        </li>
	    <li><a href="ItmPricing.aspx">Item Pricing</a></li>
    </ul>
        
    <!-- imageLoader - START -->

        <img id='HiddenLoadingImage' src="../../img/page-loader.gif" class="LoadingProgress" />

    <!-- imageLoader - END -->
    
     <div class="row-fluid sortable">
	    <div class="box span12">
			<div class="box-header" data-original-title>
				<h2><i class="halflings-icon align-justify"></i><span class="break"></span>Item Pricing</h2>
				<div class="box-icon">
				</div>
			</div>
			<div class="box-content">
                <div class="span11">
                    <div class="control-group  span10">                                                  
					    <label class="control-label span1" for="appendedInputButton">Search</label>
					    <div class="controls">
                                <div class="input-append span4">
                                    <asp:TextBox ID="TBItmPricin" runat="server" size="16" AutoPostBack="true" placeholder="Search Item Pricing..." CssClass="Search"  OnBlur="DisplayLoadingImage();" /><asp:Button ID="SeacrhBtn"  runat="server" CssClass="btn" Text="GO!"/>
    						    </div>                            
                            <div class="span2">
                                <a href="#" class="btn btn-small btnadd btn-primary"  onclick="ShowQuotations();" runat="server" id="btnadd" ><i class="icon-plus"></i> <b>Add</b></a>
                            </div>
					    </div>
				    </div>
                 </div>
                <div class="box span11">
					<div class="box-header" data-original-title>
				        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Item Pricing List</h2>
				        <div class="box-icon">
					        <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
					        <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
				        </div>
			        </div>	
                    <div class="box-content">                    
                        <div class="scrollit">
                            <div class="div_print">
                                <asp:GridView ID="GVItmPricin" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="table table-striped table-bordered" DataKeyNames="ItmPriID">
                                    <Columns>
                                        <asp:BoundField DataField="ItmPriID" HeaderText="ID" SortExpression="ItmPriID" ReadOnly="true" />
                                        <asp:BoundField DataField="EffDat" HeaderText="Date" SortExpression="EffDat" />
                                        <asp:BoundField DataField="ProductID" HeaderText="Product" SortExpression="ProductID" />
                                        <asp:BoundField DataField="CustomerID" HeaderText="Customer" SortExpression="CustomerID" />
                                        <asp:BoundField DataField="itmpri_Qty" HeaderText="Qty" SortExpression="itmpri_Qty" />
                                        <asp:BoundField DataField="unt_cost" HeaderText="Unit Cost" SortExpression="unt_cost" />
                                        <asp:BoundField DataField="cost" HeaderText="Cost" SortExpression="cost" />
                                        <asp:BoundField DataField="crtd_by" HeaderText="Created by" SortExpression="crtd_by" />
                                        <asp:BoundField DataField="crtd_at" HeaderText="Created At" SortExpression="crtd_at" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" CssClass="btn btn-success" ><i class="halflings-icon white zoom-in"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" CommandName="Delete" CssClass="btn btn-danger" runat="server" OnClientClick="" > <i class="halflings-icon white trash"></i> </asp:LinkButton>
                                                <asp:HiddenField ID="HFItmPriceId" runat="server" Value='<%# Eval("ItmPriId")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView> 
                            </div>
                        </div>
                    </div>		
                </div>
                <div id="ShowItmPricin"  class="span12">
                    <div class="box span11">
					<div class="box-header" data-original-title>
				        <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Add/Edit Item Pricing</h2>
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
                            <div class="span5">
                                <div class="item">
						            <label class="span2">
							            <span>Effective Date</span>                                    
                                    </label>
                                    <div class="span2">                                        
							            <input  name="EffDat" placeholder="ex. 12/1/2016..." type="text" runat="server" id="TBEffDat" class="datepicker" onblur="ProEmptyName();" />
						            </div>
					            </div>
                            </div>
                            <div class="span6">
                                <div class="item">
						            <label class="span2">
							            <span>Product</span>                                                        
                                    </label>
                                    <div class="span2">
                                        <asp:DropDownList ID="DDLProID" runat="server"></asp:DropDownList>							            
                                    </div>						            
						        </div>
                            </div>                                        
                        </div>
                        <div class="span12">
                            <div class="span5">
                                <div class="item">
						            <label class="span2">
							              <span>Customer</span>
                                    </label>
                                    <div class="span2">
							                <asp:DropDownList ID="DDLCusID" runat="server"></asp:DropDownList>
						            </div>
					            </div>
                            </div>
                            <div class="span6">
                                <div class="item">
						            <label class="span2">
							            <span>Qty</span>                                                        
                                    </label>
                                    <div class="span2">
                                        <asp:TextBox ID="TBitmpriQty" runat="server"></asp:TextBox>                                       
                                    </div>						            
						        </div>
                            </div>                                         
                        </div>
                        <div class="span12">
                            <div class="span5">
                                <div class="item">
						            <label class="span2">
							              <span>Unit Cost</span>
                                    </label>
                                    <div class="span2">
							                <asp:TextBox ID="TBuntcost" runat="server"></asp:TextBox>
						            </div>
					            </div>
                            </div>
                            <div class="span6">
                                <div class="item">
						            <label class="span2">
							            <span>Cost</span>                                                        
                                    </label>
                                    <div class="span2">
                                        <asp:TextBox ID="TBCost" runat="server"></asp:TextBox>
                                    </div>						            
						        </div>
                            </div>                                         
                        </div>
                        <asp:HiddenField ID="HFItmPriID" runat="server" /> 
                        <asp:HiddenField ID="HFCreatedBy" runat="server" />
                        <asp:HiddenField ID="HFCreatedAt" runat="server" />
                    </div>
                    <div class="clearfix"></div>
                    <div class="form-actions">
                        <div class="span11">&nbsp;</div>
                            <div class="span6">
                               <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="check();" OnClick="btnSubmit_Click"  />
							   <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="btnEdit_Click"  />
                               <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                            </div>
    					</div>
                    </div>
                </div>
                </div>
            </div>	    
     </div>

        <script src="Controller/Quotations.js"></script>
        <script src="Controller/Common.js"></script>
</asp:Content>
