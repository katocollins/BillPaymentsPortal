<%@ Page Language="C#" MasterPageFile="~/Setup.master" AutoEventWireup="true" CodeFile="PrepaidVendorSettings.aspx.cs" Inherits="PrepaidVendorSettings" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" class="style12" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
        <tr>
            <td style="vertical-align: middle; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            Set Prepaid Vendor Settings here</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="ddcolortabsline2" style="height: 12px">
                &nbsp;</td>
        </tr>
    </table>
    <table align="center" cellpadding="0" cellspacing="0" class="style12" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
    <tr>
    <td style="width: 98%; height: 2px">
    <asp:Label ID="lblPrepaidVendor" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
       
    </tr>
    
    <tr>
    <td colspan="2" style="vertical-align: top; text-align: center">
    <table align="center" cellpadding="0" cellspacing="0" style="width: 70%">
        <tr>
            <td colspan="3" style="height: 1px">
            </td>
        </tr>
     <tr>
    <td class="InterFaceTableLeftRowUp" style="width: 177px">Current Minimum balance:</td>
    <td class="InterFaceTableRightRow"><asp:TextBox ID="curBal" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
   <%-- <asp:TextBox ID="txtminbal" runat="server" CssClass="InterfaceTextboxLongReadOnly" TextMode= "SingleLine" Width="60%">
    </asp:TextBox></td>--%>
    </tr>
    <tr>
    <td class="InterFaceTableLeftRowUp" style="width: 177px">Minimum balance:</td>
    <td class="InterFaceTableRightRow"><asp:TextBox ID="txtminbal" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
   <%-- <asp:TextBox ID="txtminbal" runat="server" CssClass="InterfaceTextboxLongReadOnly" TextMode= "SingleLine" Width="60%">
    </asp:TextBox></td>--%>
    </tr>
    <tr>
    <td class="InterFaceTableLeftRow" style="width: 177px; height: 30px;">Company Contact Person Names:</td>
    <td class="InterFaceTableRightRow" style="height: 30px"><asp:TextBox ID="txtcontactperson" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
   <%-- <asp:TextBox ID="txtcontactperson" runat="server" CssClass="InterfaceTextboxLongReadOnly"TextMode= "SingleLine" Width="60%">
    </asp:TextBox></td>--%>
    </tr>
    <tr>
    <td class="InterFaceTableLeftRow" style="width: 177px; height: 30px;">Company Contact Person Email:</td>
    <td class="InterFaceTableRightRow" style="height: 30px"><asp:TextBox ID="TextBoxEmail" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
   <%-- <asp:TextBox ID="txtcontactperson" runat="server" CssClass="InterfaceTextboxLongReadOnly"TextMode= "SingleLine" Width="60%">
    </asp:TextBox></td>--%>
    </tr>
    <tr></tr>
    <%--<tr>
    <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
     width: 177px; border-bottom: #617da6 1px solid">
    <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnSave_Click"
    Style="font: menu" Text="Save" Width="130px" /></td>
    </tr>--%>
    </table>
        <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnSave_Click"
    Style="font: menu" Text="Save" Width="130px" /></td>
    </tr>
    </table>
    
    
</asp:Content>

