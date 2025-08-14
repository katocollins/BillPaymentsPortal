<%@ Page Language="C#" MasterPageFile="~/Setup.master" AutoEventWireup="true" CodeFile="SystemPassword.aspx.cs" Inherits="SystemPassword" Title="CHANGE YOUR SYSTEM PASSWORD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" class="style12" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
        <tr>
            <td style="vertical-align: middle; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            Change your System Password</td>
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
            <td colspan="2" style="vertical-align: top; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 70%">
                    <tr>
                        <td colspan="3" style="height: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp">
                            Old Password</td>
                        <td class="InterFaceTableMiddleRowUp">
                            &nbsp;</td>
                        <td class="InterFaceTableRightRow">
                            <asp:TextBox ID="txtoldpw" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                TextMode="Password" Width="60%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRow">
                            New Password</td>
                        <td class="InterFaceTableMiddleRow">
                        </td>
                        <td class="InterFaceTableRightRow">
                            <asp:TextBox ID="txtnewpw" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                TextMode="Password" Width="60%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRow">
                            Confirm New Password</td>
                        <td class="InterFaceTableMiddleRow">
                        </td>
                        <td class="InterFaceTableRightRow">
                            <asp:TextBox ID="txtconfirmpw" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                TextMode="Password" Width="60%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterfaceItemSeparator" colspan="3" style="height: 1px">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align: top; text-align: center; height: 23px;">
                <asp:Button ID="BtnSave" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                    OnClick="BtnSave_Click" Text="CHANGE PASSWORD" Width="164px" style="font: menu" /></td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align: top; text-align: center">
            </td>
        </tr>
    </table>
    <asp:Label ID="lblUserCode" runat="server" Text="0" Visible="False"></asp:Label>
    
               <script type ="text/javascript">
     function changeButtonText(button) {

        button.value = "Please Wait...";

    }
        
   </script>
</asp:Content>



