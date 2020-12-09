<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chckscroll.aspx.cs" Inherits="Foods.Source.IP.D.Global_Test.chckscroll" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
<asp:updatepanel ID="Updatepanel1" runat="server">
    <ContentTemplate>

         <table>
        <tr>
            <td><asp:TextBox ID="tbf" runat="server" AutoPostBack="true" OnTextChanged="tbf_TextChanged"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
    </table>

        <asp:GridView ID="GVPurItems" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Exits!!"  class="table table-striped table-bordered" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Category">  
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPurItm" runat="server" Text='<%# Eval("Category") %>' Visible = "false" />
                                                           <asp:TextBox ID="TBcat" runat="server" Text='<%# Eval("Category") %>' placeholder="Category..."  style="width:80px; height:20px; background:none; border:none;"  AutoPostBack="true" OnTextChanged="TBcat_TextChanged" ></asp:TextBox>
                                                                <asp:AutoCompleteExtender ServiceMethod="GetCat" CompletionListCssClass="completionList"
                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                                TargetControlID="TBcat" ID="AutoCompleteExtender1"  
                                                                runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TbItmDscptin" Text='<%# Eval("Description")%>' runat="server" placeholder="Description..."  style="width:120px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                            <asp:AutoCompleteExtender ServiceMethod="GetPro" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="TbItmDscptin" ID="AutoCompleteExtender2"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Brand" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Tbbrnd" Text='<%# Eval("Brand")%>' placeholder="Brand..."  runat="server" style="width:50px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                            <asp:AutoCompleteExtender ServiceMethod="GetBrnd" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="Tbbrnd" ID="AutoCompleteExtender3"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Units per Packets">
                                                        <ItemTemplate>    
                                                            <asp:TextBox ID="TbPacksiz" runat="server" Text='<%# Eval("PackingSize")%>' placeholder="Packing size..."  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                            <asp:AutoCompleteExtender ServiceMethod="Getpakgsiz" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="TbPacksiz" ID="AutoCompleteExtender5"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>    
                                                            <asp:TextBox ID="Tbunt" runat="server" Text='<%# Eval("Unit")%>' placeholder="Unit..." style="width:80px; height:20px; background:none; border:none;"></asp:TextBox>
                                                            <asp:AutoCompleteExtender ServiceMethod="Getunts" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="Tbunt" ID="AutoCompleteExtender6"  
                                                            runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>    
                                                            <asp:TextBox ID="TB_rat" Text='<%# Eval("Rate")%>' runat="server" placeholder="Rate..."  style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>    
                                                                <asp:TextBox ID="Tbqty" runat="server" Text='<%# Eval("Qty")%>' placeholder="Quantity..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Flag"  runat="server" Text="0" Visible="false" />
                                                            <asp:TextBox ID="Tbamt" runat="server" Text='<%# Eval("Amount")%>' placeholder="Amount..." style="width:80px; height:20px; background:none; border:none;" ></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="HFDPur" Value='<%# Eval("DPurID")%>'  />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>        
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnadd"  runat="server">+</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                     
                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="-"  >
                                                        <ControlStyle CssClass="halflings-icon minus-sign" />
                                                    </asp:CommandField>
                                                </Columns>
                                            </asp:GridView>
    </ContentTemplate>
</asp:updatepanel>

   
    </div>
    </form>
</body>
    <script type = "text/javascript">
        window.onload = function () {
            var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        };
        window.onscroll = function () {
            var scrollY = document.body.scrollTop;
            if (scrollY == 0) {
                if (window.pageYOffset) {
                    scrollY = window.pageYOffset;
                }
                else {
                    scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
                }
            }
            if (scrollY > 0) {
                var input = document.getElementById("scrollY");
                if (input == null) {
                    input = document.createElement("input");
                    input.setAttribute("type", "hidden");
                    input.setAttribute("id", "scrollY");
                    input.setAttribute("name", "scrollY");
                    document.forms[0].appendChild(input);
                }
                input.value = scrollY;
            }
        };
</script>
</html>
