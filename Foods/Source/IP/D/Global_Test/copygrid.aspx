<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="copygrid.aspx.cs" Inherits="Foods.Source.IP.D.Global_Test.copygrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="Sct" runat="server"></asp:ScriptManager>
    <table  style="text-align:center; font-weight:bold;" class="table table-striped table-bordered">
        <thead>
            <tr>
                <td>Category</td>
                <td>Items</td>
                <td>Sizes</td>
                <td>Qty</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <asp:TextBox ID="TBCat" CssClass="span10" runat="server" placeholder="Category..." 
                        AutoPostBack="true" OnTextChanged="TBCat_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ServiceMethod="GetCat" CompletionListCssClass="completionList"
                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="TBCat" ID="AutoCompleteExtender6"  
                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="TBItms" CssClass="span10" runat="server" AutoPostBack="true" 
                        placeholder="Items..." OnTextChanged="TBItms_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ServiceMethod="GetPro" CompletionListCssClass="completionList"
                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="TBItms" ID="AutoCompleteExtender1"  
                        runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="TB_Rat" runat="server" palceholder="Rate..." ></asp:TextBox>        
                </td>
                <td colspan="4">
                    <asp:GridView ID="GVStkItems" runat="server" class="table table-striped table-bordered" 
                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" 
                        EmptyDataText="No Record Exits!!" ShowHeader="false"   
                         OnRowDeleting="GVStkItems_RowDeleting" OnRowDataBound="GVStkItems_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:TextBox ID="itmsiz" runat="server" Text='<%# Eval("SIZE")%>' placeholder="Size..."  style="width:30px; height:20px; background:none; border:none;"   ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="ItQty" runat="server"  Text='<%# Eval("QTY")%>' placeholder="0.00" style="width:30px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="ItQty_TextChanged"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                </tr>
            </tbody>
        </table>
         <%--Text='<%# Eval("RATE")%>'--%>
        <asp:Button ID="btn_copy" runat="server" Text="ADD" OnClick="btn_copy_Click" />

       <asp:GridView ID="GVItms" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" 
           EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" >
            <Columns>
                <asp:TemplateField HeaderText="PRODUCT" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_Pro" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size">
                    <ItemTemplate>
                        <asp:TextBox ID="itmsiz" runat="server" Text='<%# Eval("SIZE")%>' placeholder="Size..."  style="width:30px; height:20px; background:none; border:none;"   ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RATE">
                    <ItemTemplate>    
                        <asp:Label ID="lblrat" runat="server"   style="width:80px; height:20px; background:none; border:none;" ></asp:Label>                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="ItmQty" runat="server"  Text='<%# Eval("QTY")%>' placeholder="0.00" style="width:30px; height:20px; background:none; border:none;" AutoPostBack="true" OnTextChanged="ItmQty_TextChanged1" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false" />
                        <asp:TextBox ID="TBamt" runat="server"  Text='<%# Eval("AMT")%>' style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVAmt" ForeColor="Red" ValidationGroup="gvItems" runat="server" ErrorMessage="Please Write Some in Amount" ControlToValidate="TBamt"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>        
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
