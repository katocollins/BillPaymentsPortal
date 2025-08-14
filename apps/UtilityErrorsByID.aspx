<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true"
    CodeFile="UtilityErrorsByID.aspx.cs" Inherits="UtilityErrorsByID" Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 86%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            utility bound</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 86%; height: 1px">
                        <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                            border-left: #617da6 1px solid; width: 50%; border-bottom: #617da6 1px solid">
                            <tr>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
                                </td>
                            </tr>
                        </table>
            </td>
        </tr>
        <tr>
            <td style="width: 86%; height: 5px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        &nbsp;<table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            Vendor-code</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            UTILITY&nbsp;</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            agent reference</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            Customer reference</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            customer phone</td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; text-align: center;
                            height: 1px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" Style="font: menu" OnDataBound="cboVendor_DataBound" OnSelectedIndexChanged="cboVendor_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:DropDownList ID="cboUtility" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" Style="font: menu">
                            </asp:DropDownList></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            <asp:TextBox ID="txtpartnerRef" runat="server" Style="font: menu" Width="90%"></asp:TextBox>&nbsp;</td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtCustRef" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtPhone" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            From Date</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            todate</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; text-align: center"
                            colspan="2" rowspan="2">
                            &nbsp;<asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="33px" OnClick="btnOK_Click"
                                Style="font: menu" Text="Search" Width="171px" />&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                        </td>
                    </tr>
                </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 86%; height: 1px;">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 86%; height: 2px">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                            border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="CustomerRef" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                                <%-- <asp:BoundColumn DataField="RecordId" HeaderText="No.">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>--%>
                                <asp:ButtonColumn CommandName="resend" HeaderText="Resend"
                                    Text="Resend">
                                    <HeaderStyle Width="5%" />
<%--                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" Width="5%" />--%>
                                </asp:ButtonColumn>
                                <asp:ButtonColumn CommandName="viewBalance" DataTextField="CustomerRef" HeaderText="CustomerRef"
                                    Text="UserName">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" Width="10%" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="CustomerName" HeaderText="CustomerName">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="10%" />
                                </asp:BoundColumn>
                                <%--  <asp:ButtonColumn CommandName="viewBalance" DataTextField="CustomerRef" HeaderText="CustomerRef"
                            Text="UserName">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" Width="10%" />
                        </asp:ButtonColumn>--%>
                                <asp:BoundColumn DataField="VendorCode" HeaderText="Vendor Code" Visible="false">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="UtilityCode" HeaderText="Utility Code">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="SentToUtility" HeaderText="SentToUtility">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="TranAmount" HeaderText="Amount">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RecordDate" HeaderText="Record Date">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Error Message">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                        </asp:DataGrid>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        &nbsp;<table align="center" cellpadding="0" cellspacing="0" style="width: 55%">
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
                                <td class="InterFaceTableLeftRowUp" style="width: 35%; height: 20px; text-align: center">
                                    <asp:TextBox ID="txt_utilityCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
                                <td class="InterFaceTableLeftRowUp" style="width: 23%; height: 20px; text-align: center">
                                    <asp:TextBox ID="txt_area" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
                                <td class="InterFaceTableLeftRowUp" style="width: 25%; height: 20px; text-align: center">
                                    <asp:TextBox ID="txt_customerReff" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                </td>
                                <td class="InterFaceTableRightRow" style="width: 39%; height: 20px; text-align: center">
                                    <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="Button2_Click" Style="font: menu" Text="IDENTIFY" Width="72%" /></td>
                            </tr>
                        </table>
                        <table class="style12" cellspacing="0" cellpadding="0" width="98%" align="center">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                        <table style="width: 98%" cellspacing="0" cellpadding="0" align="center">
                                            <tbody>
                                                <tr>
                                                    <td style="height: 18px" class="InterfaceHeaderLabel2">
                                                        CUstomer DETAILS</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                    </td>
                                    <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                        <table style="width: 98%" cellspacing="0" cellpadding="0" align="center">
                                            <tbody>
                                                <tr>
                                                    <td style="height: 18px" class="InterfaceHeaderLabel2">
                                                        PAYMENT DETAILS</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; height: 4px; text-align: left" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                        <table style="width: 98%" cellspacing="0" cellpadding="0" align="center">
                                            <tbody>
                                                <tr>
                                                    <td style="height: 20px" class="InterFaceTableLeftRowUp">
                                                        Reference</td>
                                                    <td style="width: 2%; height: 20px" class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td style="height: 20px" class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txt_custRef" runat="server" Width="75%" CssClass="InterfaceTextboxLongReadOnly"
                                                            ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Name</td>
                                                    <td style="width: 2%" class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtname" runat="server" Width="75%" CssClass="InterfaceTextboxLongReadOnly"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Type</td>
                                                    <td style="width: 2%" class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtCustType" runat="server" Width="75%" CssClass="InterfaceTextboxLongReadOnly"
                                                            ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 20px" class="InterFaceTableLeftRowUp">
                                                    </td>
                                                    <td style="width: 2%; height: 20px" class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td style="height: 20px" class="InterFaceTableRightRow">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                    </td>
                                    <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                        <table style="width: 98%" cellspacing="0" cellpadding="0" align="center">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30%" class="InterFaceTableLeftRowUp">
                                                        Balance</td>
                                                    <td style="width: 2%" class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td style="width: 70%; color: red" class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtbal" runat="server" Font-Bold="True" Width="75%" CssClass="InterfaceTextboxLongReadOnly"
                                                            ReadOnly="True" Font-Size="Larger" ForeColor="DarkRed" Height="33px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 30%; height: 20px">
                                                        Amount Entered</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                    </td>
                                                    <td class="InterFaceTableRightRow" style="width: 70%; color: red; height: 20px">
                                                        <asp:TextBox ID="txt_amountPaid" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            Enabled="False" Font-Bold="True" ReadOnly="True" Width="75%"></asp:TextBox></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; height: 5px; text-align: center" colspan="3">
                                        <asp:Button Style="font: menu" ID="Button1" OnClick="ok_Click" runat="server" Font-Bold="True"
                                            Text="Ok" Width="7%" Font-Size="9pt" Height="23px"></asp:Button></td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td style="width: 86%; height: 2px">
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
    </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>
