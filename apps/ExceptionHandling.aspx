<%@ Page Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true" CodeFile="ExceptionHandling.aspx.cs" 
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
                                        Handle exception</td>
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
                                            <td class="InterFaceTableLeftRowUp" style="width:15%">
                                                TransactionId:</td>
                                            <td style="width: 805px">
                                                <asp:TextBox ID="txtTranId" Enabled="false" runat="server" Style="font: menu" Width="98%"></asp:TextBox></td>
                                            
                                            <td class="InterFaceTableLeftRowUp">
                                                </td>
                                            <td style="width: 100px">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width:15%">
                                                Select Category:</td>
                                            <td style="width:805px">
                                                <asp:DropDownList ID="ddlVendor" runat="server" CssClass="InterfaceDropdownList" Width="100%" >
                                                  <asp:ListItem Text="Flexipay Transaction" Value="FLEXIPAY"></asp:ListItem>
                                                  <asp:ListItem Text="Float" Value="FLOAT"></asp:ListItem>
                                                  <asp:ListItem Text="Bank Charge" Value="CHARGE"></asp:ListItem>
                                                  <asp:ListItem Text="Other Transaction" Value="OTHER"></asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width:15%">
                                                Comment:</td>
                                            <td style="width: 805px">
                                                <asp:TextBox ID="txtComment" runat="server" Style="font: menu" Width="98%"></asp:TextBox></td>
                                            
                                            <td class="InterFaceTableLeftRowUp">
                                                </td>
                                            <td style="width: 100px">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width:15%">
                                                Clear:</td>
                                            <td style="width: 805px">
                                                <asp:CheckBox ID="chkPrepayment" runat="server" Text="Check box to clear exception"/>
                                                </td>

                                            <td class="InterFaceTableLeftRowUp">
                                                </td>
                                            <td style="width: 100px">
                                                </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 2px; text-align: center">
                                </td>
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

