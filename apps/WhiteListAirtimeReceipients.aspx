<%@ Page Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true"
    CodeFile="WhiteListAirtimeReceipients.aspx.cs" Inherits="WhiteListAirtimeReceipients"
    Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View3" runat="server">
            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%; border-right: #617da6 1px solid;
                border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                <tr>
                    <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                        text-align: center">
                        vendor</td>
                    <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                        text-align: center">
                        Phone Number</td>
                    <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                        text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="ddcolortabsline2" colspan="1" style="vertical-align: middle; height: 1px;
                        text-align: center">
                    </td>
                    <td class="ddcolortabsline2" colspan="2" style="vertical-align: middle; text-align: center;
                        height: 1px;">
                    </td>
                </tr>
                <tr>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                        <asp:DropDownList ID="ddlVendorCode" runat="server" CssClass="InterfaceDropdownList"
                            Font-Bold="True" OnDataBound="cboVendor_DataBound" Style="font: menu" Width="95%">
                        </asp:DropDownList></td>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                        <asp:TextBox ID="txt_customerTele" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                    <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                        border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                        border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                        border-right-color: #617da6;">
                        <asp:Button ID="btn_search" runat="server" Font-Size="9pt" Height="23px" OnClick="btnSearch_Click"
                            Text="Search" Width="85px" Style="font: menu" />&nbsp;</td>
                </tr>
                <tr>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                    </td>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                    </td>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                    </td>
                </tr>
                <tr>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                    </td>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                        <asp:Button ID="btn_addBeneficiary" runat="server" Font-Size="9pt" Height="23px"
                            OnClick="btnOK_Click" Text="Add A Beneficiary" Width="155px" Style="font: menu" /></td>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                        border-right-color: #617da6">
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" />
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <Columns>
                    <asp:BoundColumn DataField="CustomerName" HeaderText="Customer Name">
                        <HeaderStyle Width="30%" />
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CustomerTel" HeaderText="Telephone Number">
                        <HeaderStyle Width="20%" />
                        <ItemStyle Width="120px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="AllowAirtime" HeaderText="Receive Airtime">
                        <HeaderStyle Width="10%" />
                        <ItemStyle Width="120px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="AllowData" HeaderText="Receive Data">
                        <HeaderStyle Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="VendorCode" HeaderText="Vendor Code">
                        <HeaderStyle Width="20%" />
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="btnEdit" HeaderText="Reverse" Text = "Edit">
                        <HeaderStyle Width="10%" />
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="Blue" />
                    </asp:ButtonColumn>
                </Columns>
                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <table align="center" style="width: 70%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                            <tr>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    white list number for data and airtime</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="vertical-align: top; height: 4px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Customer Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px;">
                                                &nbsp;
                                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%" Font-Bold="True" ></asp:TextBox>&nbsp; *</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Phone Number</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px;">
                                                &nbsp;
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%" Font-Bold="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                            </td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px">
                                                &nbsp;<asp:CheckBox ID="cbx_airtime" runat="server" Text="Receive Airtime" ForeColor="Black" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                            </td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px">
                                                <asp:CheckBox ID="cbx_data" runat="server" Text="Receive Data" ForeColor="Black" /></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                            </td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                            </td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px">
                                                &nbsp;<asp:Button ID="btnReturn" runat="server" Text="Cancle" OnClick="add_Return_Click"
                                                    ForeColor="Red" />
                                                <asp:Button ID="add_beneficiary" runat="server" Text="Save Beneficiary" OnClick="add_beneficiary_Click"
                                                    ForeColor="#00C000" /></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                            </td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                            </td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="color: red; height: 20px">
                                                <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="add_delete_Click"
                                                    ForeColor="Red" Visible="False" Width="213px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="vertical-align: top; height: 5px; text-align: center">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <br />

    <script type="text/javascript">
 
 function Comma(Num)
 {
       Num += '';
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       x = Num.split('.');
       x1 = x[0];
       x2 = x.length > 1 ? '.' + x[1] : '';
       var rgx = /(\d+)(\d{3})/;
       while (rgx.test(x1))
       x1 = x1.replace(rgx, '$1' + ',' + '$2');
       return x1 + x2;
 }    
    </script>

    <asp:Label ID="lblcode" runat="server" Text="0" Visible="False"></asp:Label><br />
    <%--&nbsp;<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtAmount" ValidChars="0123456789,">
    </ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtPhone" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    &nbsp;
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />--%>
</asp:Content>
