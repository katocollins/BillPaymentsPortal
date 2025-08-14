<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddUtility.aspx.cs" 
Inherits="AddUtility" 
Title="NEW UTILITY"

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
            <table cellpadding="0" cellspacing="0" class="style12">
        <tr>
            <td style="width: 100%">
                                                                </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">

    
    
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="text-align: center; vertical-align: middle; height: 41px;">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel">
                                        CREATE utility</td>
                                </tr>
                            </table>
            </td>
        </tr>
        </table>
                <table style="width: 90%" align="center">
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;"><table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                            <tr>
                                <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                                    <table style="width: 98%" align="center" cellpadding="0" cellspacing="0" >
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    utility DETAILS</td>
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
                                                    utility contact Details</td>
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
                                                Utility
                                                Code</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="80%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Utility
                                                Type</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:DropDownList ID="ddUtilityType" runat="server" Style="font: menu" Width="80%">
                                                    <asp:ListItem>True</asp:ListItem>
                                                    <asp:ListItem>False</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Utility
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="80%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Contact Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtcontact" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="80%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Email</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="80%"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                        Confirm Email</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                    </td>
                                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                                        <asp:TextBox ID="txtconfirmemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            Width="80%"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Is Active</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow"><asp:CheckBox ID="chkIsActive" runat="server" Text="Tick To Activate" Font-Bold="True" /></td>
                                                </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                User</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtUser" runat="server" BackColor="#E0E0E0" CssClass="InterfaceTextboxLongReadOnly"
                                                    ReadOnly="True" Width="80%"></asp:TextBox></td>
                                        </tr>
                                            </table>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                    </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="SAVE AGENT" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                    </tr>
                </table>
        </asp:View>
        &nbsp;
    </asp:MultiView>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />

</asp:Content>

