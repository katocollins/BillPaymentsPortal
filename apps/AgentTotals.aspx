<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="AgentTotals.aspx.cs" Inherits="AgentTotals" Title="AGENT TOTALS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            AGENT TRANSACTION TOTALS
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 85%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            AGENT</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            STATUS</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 20%; height: 18px;
                            text-align: center">
                            FROM DATE</td>
                        <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 20%;
                            height: 18px; text-align: center">
                            TO DATE</td>
                        <td class="InterfaceHeaderLabel2" colspan="1" style="vertical-align: middle; width: 10%;
                            height: 18px; text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                        </td>
                        <td class="ddcolortabsline2" colspan="1" style="vertical-align: middle; height: 1px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                OnDataBound="cboVendor_DataBound" Style="font: menu" Width="95%">
                            </asp:DropDownList>&nbsp;</td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:DropDownList ID="cboStatus" runat="server" Style="font: menu" Width="95%">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">NOT RECONCILED</asp:ListItem>
                                <asp:ListItem Value="2">RECONCILED</asp:ListItem>
                                <asp:ListItem Value="3">BATCHED</asp:ListItem>
                                <asp:ListItem Value="4">POSTED</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            &nbsp;<asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 30%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" /></td>
                    </tr>
                    <tr>
                        <td colspan="6" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                            border-left: #617da6 1px solid; width: 50%; border-bottom: #617da6 1px solid">
                            <tr>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid">
                                    <asp:RadioButton ID="rdPdf" runat="server" Font-Bold="True" GroupName="FileFormat"
                                        Text="PDF" /></td>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid">
                                    <asp:RadioButton ID="rdExcel" runat="server" Font-Bold="True" GroupName="FileFormat"
                                        Text="EXCEL" /></td>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid">
                                    <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnConvert_Click"
                                        Style="font: menu" Text="Convert" Width="85px" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <div style="text-align: center">
                    <table align="center" style="width: 80%">
                        <tr>
                            <td style="width: 100%; text-align: center;">
                            <asp:DataGrid style="text-align: justify; border-right: #617da6 1px solid; border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%; border-bottom: #617da6 1px solid;" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Justify" ID="DataGrid1" OnItemCommand="DataGrid1_ItemCommand" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server" Width="100%">
                                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                <EditItemStyle BackColor="#999999" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <ItemStyle Font-Bold="False" ForeColor="#333333" BackColor="InactiveCaption" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <Columns >
                                    <asp:BoundColumn DataField="No." HeaderText="No.">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="VendorCode" HeaderText="Agent Code" Visible="False">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Vendor" HeaderText="Agent Name">
                                        <HeaderStyle Width="50%" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Total" HeaderText="Total Amount">
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                            </asp:DataGrid></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
    </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    &nbsp; &nbsp;<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
        CssClass="MyCalendar" Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

