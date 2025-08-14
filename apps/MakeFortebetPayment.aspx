<%@ Page Title="Make Fortebet Payment" Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="MakeFortebetPayment.aspx.cs" Inherits="MakeFortebetPayment" %>

<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center; height: 19px;">
                            Make Airtel Fortebet Transaction (s)</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 50%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center">
                            <asp:RadioButtonList ID="rbnMethod" runat="server" AutoPostBack="True"
                                Font-Bold="True" OnSelectedIndexChanged="rbnMethod_SelectedIndexChanged" RepeatDirection="Horizontal"
                                Width="92%">
                                <asp:ListItem Value="0">One By One</asp:ListItem>
                                <asp:ListItem Value="1">BULK</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <br />
                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View4" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center; height: 19px;">
                                    Make Single Payment</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="6" style="vertical-align: top;
                                    height: 26px; text-align: center">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="6" style="vertical-align: top;
                                    height: 26px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                                                text-align: center">
                                    Telecom Id</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                    Phone Number</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                                Amount&nbsp;</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; height: 1px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                &nbsp;<asp:TextBox ID="txtTelecomId" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:TextBox ID="txtAmount" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" Text="Post" Width="140px" OnClick="btnSubmitSingle_Click" />&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 5px; text-align: center;">
                                    </td>
                            </tr>
                        </table>
                            </asp:View>
                        </asp:MultiView></asp:View>
                    <asp:View ID="View2" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center">
                                    MAKE A BULK PAYMENT</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; text-align: left">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                            text-align: center; height: 22px;">
                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                               border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                SelECT BeneFICIARY TYPE</td>

                                            <td class="InterfaceHeaderLabel2" colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                 border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:FileUpload ID="FileUpload1" runat="server" Width="50%" /></td>
                                            <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:Button ID="Button1" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="Button1_Click" Text="UPLOAD" Width="140px" />
                                                &nbsp;</td>
                                        </tr>

                                    </table>
                                </td>
                                <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; text-align: left">
                                </td>
                            </tr>
                                <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                            text-align: center; height: 22px;">
                                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                    <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="Horizontal" HorizontalAlign="Justify"
                                        OnPageIndexChanged="DataGrid2_PageIndexChanged" Style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                                        text-align: justify" Width="100%">
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
                                            <asp:BoundColumn DataField="RecordId" HeaderText="File Id">
                                                <HeaderStyle Width="5%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VendorCode" HeaderText="Vendor Code">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="UtilityCode" HeaderText="Utility Code" Visible="true">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Status" HeaderText="Status">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ProcessedBy" HeaderText="Uploaded By">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="RecordDate" HeaderText="Record Date">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ProcessedDate" HeaderText="Processed Date">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Summary" HeaderText="Results">
                                                <HeaderStyle Width="15%" />
                                                <ItemStyle Width="15%" />
                                            </asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    </asp:DataGrid></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 18px; background-color: white; text-align: center">
                                    </td>
                            </tr>
                        </table>
                            </td>
                            </tr>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 5px; text-align: center;">
                                    </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
    </table>
</asp:Content>

