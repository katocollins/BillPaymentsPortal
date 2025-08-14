<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" 
Inherits="AddUser" 
Title="NEW SYSTEM USER"

Culture="auto" 
UICulture="auto" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

    
    
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="text-align: center; vertical-align: middle; height: 41px;">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel">
                                        CREATE SYSTEM USER</td>
                                </tr>
                            </table>
            </td>
        </tr>
        </table>
            <table cellpadding="0" cellspacing="0" class="style12">
        <tr>
            <td style="width: 100%">
                                                                </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
                <table style="width: 90%" align="center">
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;"><table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                            <tr>
                                <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                                    <table style="width: 98%" align="center" cellpadding="0" cellspacing="0" >
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    User Details</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left" >
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    System Accessiblity Details</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                First Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="TxtFname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Middle Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Last Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtLname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Email</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Phone</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtphone" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Job Role</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="InterfaceTextboxMultiline"
                                                            Height="22px" TextMode="MultiLine" Width="60%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <ajaxToolkit:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        User Category</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:DropDownList ID="cboAreas" runat="server" AutoPostBack="True" OnDataBound="cboAreas_DataBound"
                                                            OnSelectedIndexChanged="cboAreas_SelectedIndexChanged" Width="60%" style="font: menu">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Company</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:DropDownList ID="cboBranches" runat="server" OnDataBound="cboBranches_DataBound"
                                                            Width="60%" style="font: menu">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        User Type</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                        &nbsp;</td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:DropDownList ID="cboAccessLevel" runat="server" OnDataBound="cboAccessLevel_DataBound"
                                                            OnSelectedIndexChanged="cboAccessLevel_SelectedIndexChanged" Width="60%" style="font: menu">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                 <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 69px">
                                                        OTP Notification Type</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:DropDownList ID="ddNotificationType" runat="server" OnDataBound="ddlNotificaction_DataBound"
                                                            OnSelectedIndexChanged="ddlNotification_SelectedIndexChanged" Width="60%" style="font: menu">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                Is Active</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Font-Bold="True" Text="Tick" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Is Logged on</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:CheckBox ID="chkIsLoggedon" runat="server" Font-Bold="True" Text="Tick" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Reset Password</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:CheckBox ID="chkResetPassword" runat="server" Font-Bold="True" Text="Tick" /></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                            <asp:MultiView ID="MultiView2" runat="server">
                                <asp:View ID="View3" runat="server">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 40%">
                                        <tr>
                                            <td class="InterFaceTableRightRow" colspan="3" style="height: 5px; text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                UserName</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="SAVE USER" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                    </tr>
                </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                        <asp:Label ID="lblQn" runat="server" Font-Bold="True" ForeColor="Maroon" Text="."></asp:Label><asp:Button
                            ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Yes" /><asp:Button ID="btnNo"
                                runat="server" OnClick="btnNo_Click" Text="No" /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
    <asp:Label ID="lblusername" runat="server" Text="." Visible="False"></asp:Label>

</asp:Content>

