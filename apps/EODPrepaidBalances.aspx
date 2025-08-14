<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ReportMaster.master" CodeFile="EODPrepaidBalances.aspx.cs" Inherits="EODPrepaidBalances" %>

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
                    <tr><td><strong><asp:Label ID="lblmsg" runat="server"></asp:Label></strong></td></tr>
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            EOD PREPAID BALANCES</td>
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            VENDOR CODE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 20%; height: 18px;
                            text-align: center">
                            DATE</td>
                        <td class="InterfaceHeaderLabel2" colspan="1" style="vertical-align: middle; width: 10%;
                            height: 18px; text-align: center">
                        </td>
                         <td class="InterfaceHeaderLabel2" colspan="1" style="vertical-align: middle; width: 10%;
                            height: 18px; text-align: center">
                        </td>
                    </tr>
                   <%-- <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                        </td>
                        <td class="ddcolortabsline2" colspan="1" style="vertical-align: middle; height: 1px;
                            text-align: center">
                        </td>
                    </tr>--%>
                    <tr>
                       <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="txtVendorCode" runat="server" ondataBound="cboVendor_DataBound"
                                Style="font: menu" Width="95%">
                            </asp:DropDownList></td>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:TextBox ID="txtDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                      
                       <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 30%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnSubmit_Click"
                                Text="Search" Width="85px" style="font: menu" /></td>
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
            <td style="width: 80%; height: 2px">
                <div style="text-align: center">
                    <table align="center" style="width: 80%">
                        <tr>
                            <td style="width: 100%; text-align: center;">
                                
                             <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                            border-bottom: #617da6 1px solid; text-align: justify" Width="100%" >
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" />
                                 <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <ItemStyle Font-Bold="False" ForeColor="#333333" BackColor="InactiveCaption" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <Columns >
                                    <asp:BoundColumn DataField="vendorCode" HeaderText="Vendor Code">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="15%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Balance"  HeaderText="Balance" >
                                        <HeaderStyle />
                                        <ItemStyle  Width="5%" HorizontalAlign="Right"/>
                                        <ItemStyle Width="15%" />
                                    </asp:BoundColumn>
                                    
                                    <asp:BoundColumn DataField="AsAt"  HeaderText="AsAt" >
                                        <HeaderStyle Width="10%"  />
                                        <ItemStyle Width="15%" />
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
        </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    &nbsp; &nbsp;<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
        CssClass="MyCalendar" Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtDate">
    </ajaxToolkit:CalendarExtender>
   
    <br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

