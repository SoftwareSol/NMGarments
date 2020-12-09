<%@ Page Title="Chart of Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="ChartofAccount.aspx.cs" Inherits="Foods.ChartofAccount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
            /* Scroller Start */
            .scrollit {
                overflow:scroll;
                height:100%;
	            width:100%;           
	            margin:0px auto;
            }
          /* Scroller End */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<ul class="breadcrumb">
    <li>
        <i class="icon-home"></i>
        <a href="DashBoard.aspx">Home</a> 
        <i class="icon-angle-right"></i>
    </li>
    <li>
        <a href="#">SetUp</a> 
        <i class="icon-angle-right"></i>
    </li>
    <li>
        <a href="ChartofAccount.aspx">Chart of Account</a>
    </li>
</ul>
<!-- imageLoader - START -->

<%--<img id='HiddenLoadingImage' src="../../img/page-loader.gif"  class="LoadingProgress" />--%>

<!-- imageLoader - END  src="../../img/page-loader.gif"-->
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header">
                    <h2><i class="halflings-icon th"></i><span class="break"></span>Chart of Account</h2>
                </div>
                <div class="box-content">
                    <div class="box span6">
                        <div class="box-header">
                            <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Account</h2>
                            <div class="box-icon">
                                <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
                                <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="controls">
                                <div class="input-append">
                                    <asp:TextBox runat="server" class="span12" ID="TBHead" AutoPostBack="True" OnTextChanged="TBHead_TextChanged"  ></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton5" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                    <%--<asp:AutoCompleteExtender ID="TBHead_AutoCompleteExtender"    ServiceMethod="SearchedHead" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
                                            CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="divwidth" TargetControlID="TBHead" runat="server" FirstRowSelected="false" ></asp:AutoCompleteExtender>--%>
                                </div>
                            </div>
                            <div class="scrollit">
                                <asp:GridView ID="GVAccouont" runat="server" AutoGenerateColumns="False" DataKeyNames="HeadID,HeadGeneratedID" ShowHeaderWhenEmpty="true"  EmptyDataText="Not Records Exits!"  CssClass="table table-bordered table-striped table-condensed" OnRowCommand="GVAccouont_RowCommand" >
                                    <Columns>
                                        <asp:BoundField DataField="HeadGeneratedID" HeaderText="Account ID" InsertVisible="False" ReadOnly="True" SortExpression="HeadGeneratedID" />
                                        <asp:BoundField DataField="HeadName" HeaderText="Account " InsertVisible="False" ReadOnly="True" SortExpression="HeadName" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBSearchAccount" runat="server" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="DisplayLoadingImage();" ><i class="icon-search"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                
                                    </Columns>
                                <PagerStyle CssClass="pagination pagination-centered" />
                                </asp:GridView>
                            </div>
                            <div class="span12">
                                <div class="span4"><label style="color:#000000" for="TBAccountGeneratedID" >ID:</label>
                                    <asp:TextBox ID="TBAccountGeneratedID" runat="server" CssClass="AccGenID span12" ></asp:TextBox>
                                </div>
                                <div class="span4"><label style="color:#000000"  for="TBInsert"> Name:</label>                                                                              
                                    <input  name="name" placeholder="ex. Assets/Sub Assets" type="text" runat="server" id="TBInsert" class="span12 InsertAccount" onblur="InsertAcc();" />                               
                                </div>                             
                                <div class="span3">                                 
                                    <asp:LinkButton ID="LinkBtnInsertHead" runat="server" Text="Insert" CssClass="btn btn-success span5" OnClick="LinkBtnInsertHead_Click" OnClientClick="return InsertAcc();"><i class="icon-save"></i></asp:LinkButton>
                                    <asp:LinkButton ID="LinkBtnCancel" runat="server" CssClass="btn btn-danger span5"  ><i class="icon-undo"></i></asp:LinkButton>   
                                    <asp:HiddenField ID="HFaccount" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box span6">
                        <div class="box-header">
                            <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Sub Account</h2>
                            <div class="box-icon">
                                <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
                                <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="controls">
                                <div class="input-append">
                                    <asp:TextBox runat="server" class="span12" ID="TBSubAcc" AutoPostBack="True" OnTextChanged="TBSubAcc_TextChanged" ></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton4" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="scrollit">
                                <asp:GridView ID="GVSubAcc" runat="server" AutoGenerateColumns="False" DataKeyNames="SubHeadID,SubHeadGeneratedID,HeadGeneratedID" ShowHeaderWhenEmpty="true" EmptyDataText="Not Records Exits!"  CssClass="table table-bordered table-striped table-condensed" OnRowCommand="GVSubAcc_RowCommand" OnRowDeleting="GVSubAcc_RowDeleting" OnRowCancelingEdit="GVSubAcc_RowCancelingEdit" OnRowEditing="GVSubAcc_RowEditing" OnRowUpdating="GVSubAcc_RowUpdating" >
                                    <Columns>
                                        <asp:BoundField DataField="HeadGeneratedID" HeaderText="Account" InsertVisible="False" ReadOnly="True" SortExpression="HeadGeneratedID" />
                                        <asp:BoundField DataField="SubHeadGeneratedID" HeaderText="Sub Account" InsertVisible="False" ReadOnly="True" SortExpression="SubHeadGeneratedID" />
                                        <asp:TemplateField HeaderText="Sub Account Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSubHead" runat="server" Text='<%# Eval("SubHeadName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TBSubHead" runat="server" Text='<%# Eval("SubHeadName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBEditSubAcc" runat="server" CommandName="Edit"  CommandArgument='<%# Container.DataItemIndex %>' >edit</asp:LinkButton>
                                            </ItemTemplate>
                                                <EditItemTemplate>  
                                                    <asp:LinkButton ID="LBSubEdit" runat="server"   CommandName="Update">edit</asp:LinkButton>
                                                    <asp:LinkButton ID="LBSubCancel" runat="server"   CommandName="Cancel" ><i class="halflings-icon  minus-sign"></i></asp:LinkButton>
                                                </EditItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBSearchAccount" runat="server" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="DisplayLoadingImage();" ><i class="icon-search"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                
                                    </Columns>
                                <PagerStyle CssClass="pagination pagination-centered" />
                                </asp:GridView>
                            </div>  
                            <div class="span12">
                                <div class="span3"><label style="color:#000000" for="TBSubAccGeneratedID">ID:</label>
                                    <asp:TextBox ID="TBSubAccGeneratedID" runat="server" CssClass="SubAccGenID span12" ></asp:TextBox>
                                </div>
                                <div class="span3"><label style="color:#000000" for="DDLHead" >Head:</label>                                                                              
                                    <asp:DropDownList runat="server" ID="DDLHead" CssClass="Head span12" ></asp:DropDownList>
                                </div> 
                                <div class="span4"><label style="color:#000000" for="SubAccount" >Name:</label>                                                                     
                                    <input  name="SubAccount" placeholder="ex. Assets/Sub Assets" type="text" runat="server" id="TbAddSubAccount" class="span12 SubAccount" onblur="InsertSubAcc();" />
                                </div>                            
                                <div class="span3">                                 
                                    <asp:LinkButton ID="LinkBtnSubAccInsert" runat="server" Text="Insert" CssClass="btn btn-success span5" OnClientClick="return InsertSubAcc();" OnClick="LinkBtnSubAccInsert_Click"><i class="icon-save"></i></asp:LinkButton>
                                    <asp:LinkButton ID="LinkBtnSubAccCancel" runat="server" CssClass="btn btn-danger span5" ><i class="icon-undo"></i></asp:LinkButton>  
                                    <asp:HiddenField ID="HFSubAccount" runat="server" />
                                    <asp:HiddenField ID="HFSubAcc" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                    <div class="box-content">
                        <div class="box span12">
                            <div class="box-header">
                                <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Sub Categories Account</h2>
                                <div class="box-icon">                                    
                                    <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
                                    <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
                                </div>
                            </div>
                            <div class="box-content">
                                <div class="controls">
                                    <div class="input-append">
                                        <asp:TextBox runat="server" class="span12" ID="TBSubHeadCategories" AutoPostBack="True" OnTextChanged="TBSubHeadCategories_TextChanged"  ></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton3" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="scrollit">
                                    <asp:GridView ID="GVSubAccountCatgories" runat="server" AutoGenerateColumns="False"  EmptyDataText="Not Records Exits!" ShowHeaderWhenEmpty="true" 
                                    DataKeyNames="SubHeadCategoriesID,HeadGeneratedID,SubHeadGeneratedID,SubHeadCategoriesGeneratedID"  CssClass="table table-bordered table-striped table-condensed" OnRowCommand="GVSubAccountCatgories_RowCommand" OnRowCancelingEdit="GVSubAccountCatgories_RowCancelingEdit" OnRowEditing="GVSubAccountCatgories_RowEditing" OnRowUpdating="GVSubAccountCatgories_RowUpdating"   >
                                        <Columns>
                                            <asp:BoundField DataField="HeadGeneratedID" HeaderText="Account" InsertVisible="False" ReadOnly="True" SortExpression="HeadGeneratedID" />
                                            <asp:BoundField DataField="SubHeadGeneratedID" HeaderText="Sub Account" InsertVisible="False" ReadOnly="True" SortExpression="SubHeadGeneratedID" />
                                            <asp:BoundField DataField="SubHeadCategoriesGeneratedID" HeaderText="Sub Account Categories" InsertVisible="False" ReadOnly="True"  SortExpression="SubHeadCategoriesGeneratedID" />
                                            <asp:TemplateField HeaderText="Sub Account Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSubHeadCategoriesName" runat="server" Text='<%# Eval("SubHeadCategoriesName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TBSubHeadCategoriesName" runat="server" Text='<%# Eval("SubHeadCategoriesName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            </asp:TemplateField>                                
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBEditSubAccountCatgories" runat="server"   CommandName="Edit"  CommandArgument='<%# Container.DataItemIndex %>' ><i class="halflings-icon  edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>  
                                                    <asp:LinkButton ID="LBSubAccountCatgoriesEdit" runat="server"   CommandName="Update">edit</asp:LinkButton>
                                                    <asp:LinkButton ID="LBSubAccountCatgoriesCancel" runat="server"   CommandName="Cancel"><i class="halflings-icon  minus-sign"></i></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBSearchAccount" runat="server" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="DisplayLoadingImage();" ><i class="icon-search"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            </asp:TemplateField>                                
                                        </Columns>
                                    <PagerStyle CssClass="pagination pagination-centered" />
                                    </asp:GridView>                            
                                </div>
                                <div class="span12">
                                    <div class="span3"><label style="color:#000000" for="TBAccountCategoryNameID">ID:</label>
                                        <asp:TextBox ID="TBAccountCategoryNameID" runat="server" CssClass="AccCatNameID"  ></asp:TextBox>
                                    </div>
                                    <div class="span3"><label style="color:#000000" for="DDLHead" >Head:</label>                                                                              
                                        <asp:DropDownList runat="server" ID="DDLAccountName" CssClass="AccName" AutoPostBack="true" OnSelectedIndexChanged="DDLAccountName_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>
                                    <div class="span3"><label style="color:#000000" for="DDLSubAccountName" >Sub Head:</label>                                                                              
                                        <asp:DropDownList runat="server" ID="DDLSubAccountName" CssClass="SubAccName" ></asp:DropDownList>
                                    </div> 
                                    <div class="span3"><label style="color:#000000" for="SubAccount" >Name:</label>                                                                     
                                        <input  name="AccountCategories" placeholder="ex. Assets/Sub Assets" type="text" runat="server" id="TBaccountcategories" class="AccountCategories" onblur="InsertAccCat();" />
                                    </div>                            
                                    <div class="span3">                                 
                                        <asp:LinkButton ID="LinkBtnaccountcategoriesInsert" runat="server" CssClass="btn btn-success span5" OnClientClick="return InsertAccCat();" OnClick="LinkBtnaccountcategoriesInsert_Click" > <i class="icon-save"></i></asp:LinkButton>                  
                                        <asp:LinkButton ID="LinkBtnaccountcategoriesancel" runat="server" CssClass="btn btn-danger span5"  ><i class="icon-undo"></i></asp:LinkButton>
                                        <asp:HiddenField ID="HFAccountCategoryName" runat="server" />
                                        <asp:HiddenField ID="HFAccCatName" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>        
                <div class="clearfix"></div>
                <asp:Panel ID="pnlaccfour" runat="server">
                    <div class="box-content">
                    <div class="box span12">
                        <div class="box-header">
                            <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Sub Categories Account</h2>
                            <div class="box-icon">                            
                                <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
                                <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="controls">
                                <div class="input-append">
                                    <asp:TextBox runat="server" class="span12" ID="TBsubheadcategoryfour" AutoPostBack="True" OnTextChanged="TBsubheadcategoryfour_TextChanged"></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton2" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                </div>
                            </div>
                        <div class="scrollit">
                            <asp:GridView ID="GVFour" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" DataKeyNames="subheadcategoryfourID,subheadcategoryfourGeneratedID"  EmptyDataText="Not Records Exits!" 
                            CssClass="table table-bordered table-striped table-condensed" OnRowCommand="GVFour_RowCommand" OnRowCancelingEdit="GVFour_RowCancelingEdit" OnRowEditing="GVFour_RowEditing" OnRowUpdating="GVFour_RowUpdating">
                                <Columns>
                                    <asp:BoundField DataField="HeadGeneratedID" HeaderText="Account" InsertVisible="False" ReadOnly="True" SortExpression="HeadGeneratedID" />
                                    <asp:BoundField DataField="SubHeadGeneratedID" HeaderText="Sub Account" InsertVisible="False" ReadOnly="True" SortExpression="SubHeadGeneratedID" />
                                    <asp:BoundField DataField="SubHeadCategoriesGeneratedID" HeaderText="Sub Account Categories" InsertVisible="False" ReadOnly="True" SortExpression="SubHeadCategoriesGeneratedID" />
                                    <asp:BoundField DataField="subheadcategoryfourGeneratedID" HeaderText="Sub Account Four Categories" InsertVisible="False" ReadOnly="True" SortExpression="subheadcategoryfourGeneratedID" />
                                    <asp:TemplateField HeaderText="Sub Account Name">
                                    <ItemTemplate>
                                        <asp:Label ID="LblsubheadcategoryfourName" runat="server" Text='<%# Eval("subheadcategoryfourName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TBsubheadcategoryfourName" runat="server" Text='<%# Eval("subheadcategoryfourName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBEditGVFour" runat="server" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>' >edit</asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>  
                                            <asp:LinkButton ID="LBSubAccountCatgoriesfourEdit" runat="server"   CommandName="Update"><i class="halflings-icon  edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="LBSubAccountCatgoriesfourCancel" runat="server"   CommandName="Cancel"><i class="halflings-icon  minus-sign"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBSearchAccount" runat="server" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="DisplayLoadingImage();" ><i class="icon-search"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>                                
                                </Columns>
                                <PagerStyle CssClass="pagination pagination-centered" />
                            </asp:GridView>                            
                        </div>
                        <div class="span12">
                            <div class="span3"><label style="color:#000000" for="TBaccountcategoriesfourGeneratedID">ID:</label>
                                <asp:TextBox ID="TBaccountcategoriesfourGeneratedID" runat="server" CssClass="AccCatfourGenID span12" ></asp:TextBox>
                            </div>
                            <div class="span3"><label style="color:#000000" for="DDLHead" >Head:</label>                                                                              
                                <asp:DropDownList runat="server" ID="DLLCatefourAccName"   CssClass="span12 CatefourAccName" AutoPostBack="True" OnSelectedIndexChanged="DLLCatefourAccName_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                            <div class="span3"><label style="color:#000000" for="DLLCategoriesfourSubAccountName" >Sub Head:</label>                                                                              
                                <asp:DropDownList runat="server" ID="DLLCategoriesfourSubAccountName" CssClass="span12 CatfourSubAccName"   AutoPostBack="True" OnSelectedIndexChanged="DLLCategoriesfourSubAccountName_SelectedIndexChanged" ></asp:DropDownList>
                            </div> 
                            <div class="span3"><label style="color:#000000" for="DLLCategoriesfourSubAccountCategoryName" >Account Category:</label>                                                                     
                                <asp:DropDownList runat="server" ID="DLLCategoriesfourSubAccountCategoryName"  CssClass="span12 CatfourSubAccCatName" ></asp:DropDownList>
                            </div>
                            <div class="span3"><label style="color:#000000" for="TBaccountcategoriesfour" >Name:</label>                                                                     
                                <input  name="AccountCategoriesFour" placeholder="ex. Assets/Sub Assets" type="text" runat="server" id="TBaccountcategoriesfour" class="span12 AccCatFour" onblur="IntAccCatFour();" />
                            </div>                            
                            <div class="span3">                                 
                                <asp:LinkButton ID="LinkBtnaccountcategoriesfourInsert" runat="server" CssClass="btn btn-success span5" OnClientClick="return IntAccCatFour();" OnClick="LinkBtnaccountcategoriesfourInsert_Click" > <i class="icon-save"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkBtnaccountcategoriesfourCancel" runat="server" CssClass="btn btn-danger span5"><i class="icon-undo"></i></asp:LinkButton>
                                <asp:HiddenField ID="HFsubAccountCategoryfour" runat="server" />
                            </div>
                        </div>
                        </div>
                        <asp:HiddenField ID="HFGVFour" runat="server" />                            
                    </div>      
                </div>
                </asp:Panel>
                <div class="clearfix"></div>
                <asp:Panel ID="pnlaccfiv" runat="server">
                    <div class="box-content">
                    <div class="box span12">
                        <div class="box-header">
                            <h2><i class="halflings-icon align-justify"></i><span class="break"></span>Sub Categories Account</h2>
                            <div class="box-icon">
                                <a href="#" class="btn-minimize"><i class="halflings-icon chevron-up"></i></a>
                                <a href="#" class="btn-close"><i class="halflings-icon remove"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="controls">
                                <div class="input-append">
                                    <asp:TextBox runat="server" class="span12" ID="TBSearchsubheadcategoryfive" AutoPostBack="True" OnTextChanged="TBSearchsubheadcategoryfive_TextChanged" ></asp:TextBox><asp:LinkButton runat="server" ID="LinkButton1" CssClass="add-on" ><i class="icon-search"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="scrollit">
                                <asp:GridView ID="GVFive" runat="server" AutoGenerateColumns="False" DataKeyNames="subheadcategoryfiveID,subheadcategoryfiveGeneratedID" ShowHeaderWhenEmpty="true"  EmptyDataText="Not Records Exits!"  CssClass="table table-bordered table-striped table-condensed" OnRowCancelingEdit="GVFive_RowCancelingEdit" OnRowEditing="GVFive_RowEditing" OnRowUpdating="GVFive_RowUpdating"  >
                                    <Columns>
                                        <asp:BoundField DataField="HeadGeneratedID" HeaderText="Account" InsertVisible="False" ReadOnly="True"  SortExpression="HeadGeneratedID" />
                                        <asp:BoundField DataField="SubHeadGeneratedID" HeaderText="Sub Account" InsertVisible="False" ReadOnly="True"  SortExpression="SubHeadGeneratedID" />
                                        <asp:BoundField DataField="SubHeadCategoriesGeneratedID" HeaderText="Sub Account Categories" InsertVisible="False" ReadOnly="True"  SortExpression="SubHeadCategoriesGeneratedID" />
                                        <asp:BoundField DataField="subheadcategoryfourGeneratedID" HeaderText="Sub Account Categories Four" InsertVisible="False" ReadOnly="True"  SortExpression="subheadcategoryfourGeneratedID" />
                                        <asp:BoundField DataField="subheadcategoryfiveGeneratedID" HeaderText="Sub Account Categories Five" InsertVisible="False" ReadOnly="True"  SortExpression="subheadcategoryfiveGeneratedID" />
                                        <asp:TemplateField HeaderText="Sub Account Name">
                                        <ItemTemplate>
                                            <asp:Label ID="LblsubheadcategoryfiveName" runat="server" Text='<%# Eval("subheadcategoryfiveName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TBsubheadcategoryfiveName" runat="server" Text='<%# Eval("subheadcategoryfiveName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBEditGVFive" runat="server" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'>
                                            edit
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>  
                                            <asp:LinkButton ID="LBSubAccountCatgoriesfiveEdit" runat="server"   CommandName="Update"><i class="halflings-icon  edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="LBSubAccountCatgoriesfiveCancel" runat="server"   CommandName="Cancel"><i class="halflings-icon  minus-sign"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination pagination-centered" />
                                </asp:GridView>
                            </div>
                            <div class="span12" style="visibility:hidden;">
                                <div class="span3"><label style="color:#000000" for="TBsubaccountcategoriesfiveGeneratedID">ID:</label>
                                    <asp:TextBox ID="TBsubaccountcategoriesfiveGeneratedID" runat="server" CssClass="SubAccCatFiveGenID span12"  ></asp:TextBox>
                                </div>
                                <div class="span3"><label style="color:#000000" for="DDLHead" >Head:</label>                                                                              
                                    <asp:DropDownList runat="server" ID="DLLCatefiveAccName"  CssClass="span12 CatefiveAccName" AutoPostBack="true" OnSelectedIndexChanged="DLLCatefiveAccName_SelectedIndexChanged" ></asp:DropDownList>                                                    
                                </div>
                                <div class="span3"><label style="color:#000000" for="DLLCategoriesfourSubAccountName" >Sub Head:</label>                                                                              
                                    <asp:DropDownList runat="server" ID="DLLCategoriesfiveSubAccountName" CssClass="span12 CatfiveSubAccName" AutoPostBack="true" OnSelectedIndexChanged="DLLCategoriesfiveSubAccountName_SelectedIndexChanged"></asp:DropDownList>
                                </div> 
                                <div class="span3"><label style="color:#000000" for="DLLCategoriesfiveSubAccountCategoryName" >Account Category:</label>                                                                     
                                    <asp:DropDownList runat="server" ID="DLLCategoriesfiveSubAccountCategoryName" CssClass="span12 CatfiveSubAccCatName" AutoPostBack="true" OnSelectedIndexChanged="DLLCategoriesfiveSubAccountCategoryName_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                                <div class="span3"><label style="color:#000000" for="DLLCategoriesfiveSubAccountCategoryfourName" >Account Category:</label>                                                                     
                                    <asp:DropDownList runat="server" ID="DLLCategoriesfiveSubAccountCategoryfourName" CssClass="span12 CatfiveSubAccCatfourName" ></asp:DropDownList>
                                </div>
                                <div class="span3"><label style="color:#000000" for="TBsubaccountcategoriesfiveInsert" >Name:</label>                                                                     
                                    <input  name="AccountCategoriesFour" placeholder="ex. Assets/Sub Assets" type="text" runat="server" id="TBsubaccountcategoriesfiveInsert" class="span12 AccCatFive" onblur="IntAccCatFive();" />
                                </div>                            
                                <div class="span3">                                 
                                    <asp:LinkButton ID="LinkBtnsubaccountcategoriesfiveInsert" runat="server" CssClass="btn btn-success span5" OnClientClick="return IntAccCatFive();" OnClick="LinkBtnsubaccountcategoriesfiveInsert_Click" ><i class="icon-save"></i></asp:LinkButton>
                                    <asp:LinkButton ID="LinkBtnsubaccountcategoriesfiveCancel" runat="server" CssClass="btn btn-danger span5"><i class="icon-undo"></i></asp:LinkButton>
                                    <asp:HiddenField ID="HFAccountCategoryfive" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>       
                </asp:Panel>
                <div class="row-fluid">
                    <div class="span6">
                        <asp:Panel ID="panelPopUp" runat="server" class="span4" style="display:none;" >
                            <div class="row-fluid">	
                                <div class="box blue span12">
                                    <div class="box-header">
                                        <h2>&nbsp;</h2>
                                    </div>
                                    <div class="box-content">
                                        <div class="row-fluid">	
                                            <div class="span12">
                                                <label style="color:#000000"  class="control-label">The Specified Supplier Doesnt Exists Do you want to create Setup!!</label>                            
                                            </div>
                                        </div>
                                        <div class="row-fluid">	
                                            <div class="span3"></div>
                                            <div class="span6">
                                                <asp:Button ID="btnpanelPopUp" runat="server" CssClass="btn"  Text="Ok"   />
                                            </div>
                                        </div>
                                    <div class="clearfix"></div>
                                    </div>	
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <asp:HiddenField ID="HFCreatePopUp2" runat="server" />
                <div class="row-fluid">
                    <div class="span6">
                        <asp:Panel ID="panelPopUp2" runat="server" class="span4" style="display:none;" >
                            <div class="row-fluid">	
                                <div class="box blue span12">
                                    <div class="box-header">
                                        <h2>&nbsp;</h2>
                                    </div>
                                    <div class="box-content">
                                        <div class="row-fluid">	
                                            <div class="span12">
                                                <label style="color:#000000"  class="control-label">The Specified Supplier Doesnt Exists Do you want to create Setup!!</label>                            
                                            </div>
                                        </div>
                                        <div class="row-fluid">	
                                            <div class="span3"></div>
                                            <div class="span6">
                                                <asp:Button ID="btnpanelPopUp2" runat="server" CssClass="btn"  Text="Ok"   />
                                            </div>
                                        </div>
                                    <div class="clearfix"></div>
                                    </div>	
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>    
                <div class="row-fluid">
                    <div class="span6">
                        <asp:Panel ID="panelPopUp3" runat="server" class="span4" style="display:none;" >
                            <div class="row-fluid">	
                                <div class="box blue span12">
                                    <div class="box-header">
                                        <h2>&nbsp;</h2>
                                    </div>
                                    <div class="box-content">
                                        <div class="row-fluid">	
                                            <div class="span12">
                                                <label style="color:#000000"  class="control-label">The Specified Supplier Doesnt Exists Do you want to create Setup!!</label>                            
                                            </div>
                                        </div>
                                        <div class="row-fluid">	
                                            <div class="span3"></div>
                                            <div class="span6">
                                                <asp:Button ID="btnpanelPopUp3" runat="server" CssClass="btn"  Text="Ok"   />
                                            </div>
                                        </div>
                                    <div class="clearfix"></div>
                                    </div>	
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>    
                <asp:HiddenField ID="HFCreatePopUp4" runat="server" />       
                <div class="row-fluid">
                    <div class="span6">
                        <asp:Panel ID="panelPopUp4" runat="server" class="span4" style="display:none;" >
                            <div class="row-fluid">	
                                <div class="box blue span12">
                                    <div class="box-header">
                                        <h2>&nbsp;</h2>
                                    </div>
                                    <div class="box-content">
                                        <div class="row-fluid">	
                                            <div class="span12">
                                                <label style="color:#000000"  class="control-label">The Specified Supplier Doesnt Exists Do you want to create Setup!!</label>                            
                                            </div>
                                        </div>
                                        <div class="row-fluid">	
                                            <div class="span3"></div>
                                            <div class="span6">
                                                <asp:Button ID="btnpanelPopUp4" runat="server" CssClass="btn"  Text="Ok"   />
                                            </div>
                                        </div>                        
                                        <div class="clearfix"></div>
                                    </div>	
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>           
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Panel ID="PanelAlert" runat="server" CssClass="span6" style="display:none;">
    <div class="modal" id="myModal1">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" title="Close" data-rel="tooltip">×</button>
            <h3><asp:Label ID="lblalertheading" runat="server"></asp:Label></h3>
        </div>
        <div class="modal-body">
            <asp:Label ID="lblalertMessage" runat="server"></asp:Label>
        </div>
        <div class="modal-footer">                 
            <asp:LinkButton ID="LinkBtnAlert" runat="server" CssClass="btn" Text="Close" data-dismiss="modal" title="Close" data-rel="tooltip"> </asp:LinkButton>
        </div>
    </div>
</asp:Panel>
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
            <asp:LinkButton ID="btnalertOk" runat="server" CssClass="btn btn-success" Text="OK" ></asp:LinkButton>
        </div>
        </div>
    </div>
</div>  
<script type="text/javascript" src="Controller/Common.js"></script>  
<script type="text/javascript" src="Controller/ChartofAccount.js"></script>     
</asp:Content>
