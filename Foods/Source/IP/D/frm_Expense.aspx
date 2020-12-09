<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_Expense.aspx.cs" Inherits="Foods.frm_Expense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <style>
             .completionList {
            border:solid 1px Gray;
            margin:0px;
            padding:3px;
            height: 120px;
            overflow:auto;
            background-color: #FFFFFF;     
        } 

        .listItem {
            color: #191919;
        } 

        .itemHighlighted {
            background-color: #ADD6FF;               
        }
        </style>
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="tb_acct" runat="server" Width="341px"></asp:TextBox>
            <asp:AutoCompleteExtender ServiceMethod="GetCurr" CompletionListCssClass="completionList"
CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
TargetControlID="tb_acct" ID="AutoCompleteExtender"  
runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
            &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Acc No" Height="30px" Width="69px"></asp:Label>
        <asp:TextBox ID="tb_ac" runat="server" Height="20px" OnTextChanged="tb_ac_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
             <asp:AutoCompleteExtender ServiceMethod="GetCurr" CompletionListCssClass="completionList"
CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
TargetControlID="tb_ac" ID="AutoCompleteExtender1"  
runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
    </div>
      <br />
        <div>
        &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text=" DATE" Height="30px" Width="69px"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="date" runat="server" Height="20px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
        <asp:Label ID="Label4" runat="server" Text="Bill No" Height="30px" Width="69px"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="bilno" runat="server" Height="20px"></asp:TextBox>
        <br />
        <div>
            <asp:Label ID="Label7" runat="server" Height="30px" Text="Type Of Expense" Width="112px"></asp:Label>
&nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="dllexptyp" runat="server" Width="341px"></asp:TextBox>
            <asp:AutoCompleteExtender ServiceMethod="GetCurr" CompletionListCssClass="completionList"
CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="10"
TargetControlID="dllexptyp" ID="AutoCompleteExtender2"  
runat="server" FirstRowSelected="false"></asp:AutoCompleteExtender>
            <br />
    </div>
     <div>  
     </div>
        <br />
     <div>
        <asp:Label ID="Label5" runat="server" Text="Amount Paid"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="amtpad" runat="server" Height="20px" OnTextChanged="bn0_TextChanged"></asp:TextBox>
    </div>
        <br />
      <div>
        <asp:Label ID="Label6" runat="server" Text="Expense Remarks"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="exprmk" runat="server" Height="20px"></asp:TextBox>
    </div>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 26px" Text="Save" />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:CommandField ShowDeleteButton="false" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="delete" CommandName="delete" >Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:HiddenField ID="HFUsrId" runat="server" />
    </form>
</body>
</html>
