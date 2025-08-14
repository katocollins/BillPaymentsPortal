<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddUtilityCredentials.aspx.cs" 
Inherits="AddUtilityCredentials" 
Title="NEW UTILITY CREDENTIALS"

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
                                        Add utility credentials</td>
                                </tr>
                            </table>
            </td>
        </tr>
        </table>
                <table style="width: 90%" align="center">
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;"><table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                            <tr>
                                <td style="vertical-align: top; height: 5px; text-align: left;" colspan="4">
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width:25%">
                                                Select Vendor:</td>
                                            <td style="width:25%">
                                                <asp:DropDownList ID="ddlVendor" runat="server" CssClass="InterfaceDropdownList" Width="100%" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" OnDataBound="ddlVendor_DataBound">
                                                </asp:DropDownList></td>
                                            <td class="InterFaceTableLeftRowUp" style="width:25%">
                                                Select Utility:</td>
                                            <td style="width:25%">
                                                <asp:DropDownList ID="ddlUtility" runat="server" CssClass="InterfaceDropdownList" Width="100%" OnSelectedIndexChanged="ddlUtility_SelectedIndexChanged" OnDataBound="ddlUtility_DataBound">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:Button ID="btnGetCredentials" runat="server" OnClick="btnGetCredentials_Click"
                                                    Text="GetCredentials" /></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width:25%">
                                                Utility Username:</td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txtUsername" runat="server" Style="font: menu" Width="98%"></asp:TextBox></td>
                                            <td class="InterFaceTableLeftRowUp" style="width:25%">
                                                Utility Password:</td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txtPassword" runat="server" Style="font: menu" Width="98%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width:25%">
                                                BankCode:</td>
                                            <td style="width: 100px">
                                                <asp:TextBox ID="txtBankCode" runat="server" Style="font: menu" Width="98%"></asp:TextBox></td>
                                            <td class="InterFaceTableLeftRowUp" style="width:25%">
                                                </td>
                                            <td style="width: 100px">
                                                </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                    <asp:CheckBox ID="chkPrepayment" runat="server" Text="Tick if Utility Has Certificate:" Font-Bold="True" AutoPostBack="True" OnCheckedChanged="chkPrepayment_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 2px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 2px; text-align: center"><asp:MultiView ID="MultiView3" runat="server">
                                    <asp:View ID="View3" runat="server">
                                        <table cellpadding="0" cellspacing="0" class="style12" align="center" width="99%">
                                            <tr>
                                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                        <tbody>
                                                            <tr>
                                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                    CERTIFICATE details</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    <table style="width: 80%" align="center">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 35%">
                                                Browse Utility Certificate</td>
                                            <td class="InterFaceTableLeftRowUp" style="width: 65%">
                                                <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                                        </tr>
                                    </table>
                                    </asp:View>
                                </asp:MultiView></td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="SAVE" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                    </tr>
                </table>
        </asp:View>
        &nbsp;
    </asp:MultiView>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />

</asp:Content>

