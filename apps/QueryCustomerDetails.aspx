<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true"
    CodeFile="QueryCustomerDetails.aspx.cs" Inherits="QueryCustomerDetails" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style="width: 55%">
        <tr>
            <td class="InterFaceTableLeftRowUp" style="width: 35%; height: 20px; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                    <tbody>
                        <tr>
                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                Utility code</td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td class="InterFaceTableLeftRowUp" style="width: 23%; height: 20px; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                    <tbody>
                        <tr>
                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                Area</td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td class="InterFaceTableLeftRowUp" style="width: 25%; height: 20px; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                    <tbody>
                        <tr>
                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                CUstomer Ref/ Invoice Ref</td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
            </td>
            <td class="InterFaceTableRightRow" style="width: 39%; height: 20px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="InterFaceTableRightRow" colspan="5" style="height: 1px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="InterFaceTableLeftRowUp" style="width: 35%; text-align: center; height: 20px;">
                <asp:DropDownList ID="ddl_utilities" runat="server" OnSelectedIndexChanged="ddl_utilities_SelectedIndexChanged"
                    Width="214px" AutoPostBack="true">
                </asp:DropDownList></td>
            <td class="InterFaceTableLeftRowUp" style="width: 23%; height: 20px; text-align: center">
                <asp:TextBox ID="txt_area" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                    Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
            <td class="InterFaceTableLeftRowUp" style="width: 25%; height: 20px; text-align: center">
                <asp:TextBox ID="txtCustRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                    Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
            </td>
            <td class="InterFaceTableRightRow" style="width: 39%; text-align: center; height: 20px;">
                <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                    OnClick="Button2_Click" Style="font: menu" Text="IDENTIFY" Width="72%" /></td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View1" runat="server">
            <table align="center" style="width: 90%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                            <tr>
                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    CUstomer DETAILS</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    PAYMENT DETAILS</td>
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
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Reference</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txt_custRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Type</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtCustType" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    ReadOnly="True" Width="75%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                </td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Balance</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%; color: red">
                                                <asp:TextBox ID="txtbal" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="75%"
                                                    ReadOnly="True" Font-Bold="True"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center"><asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                    OnClick="ok_Click" Style="font: menu" Text="Ok" Width="7%" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
