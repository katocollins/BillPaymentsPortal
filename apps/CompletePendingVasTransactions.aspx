<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true"
    CodeFile="CompletePendingVasTransactions.aspx.cs" Inherits="CompletePendingVasTransactions"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table style="width: 90%" align="center">
            <tr>
                <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                    
                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 98%; border-bottom: #617da6 1px solid">
                        <tr>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 20%; height: 18px;
                                text-align: center">
                                Reference id</td>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                text-align: center">
                                TELLER (if ANY)</td>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                text-align: center">
                                FROM DATE</td>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                text-align: center">
                                TO DATE</td>
                        </tr>
                        <tr>
                            <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                border-right-color: #617da6">
                                &nbsp;<asp:TextBox ID="txtCustRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                    Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                border-right-color: #617da6">
                                <asp:TextBox ID="txtPhone" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                border-right-color: #617da6">
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
                        <tr>
                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                                border-right-color: #617da6">
                                <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                    OnClick="Button2_Click" Style="font: menu" Text="Search" Width="20%" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:MultiView ID="MultiView2" runat="server">
            <asp:View ID="View2" runat="server">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="30" Style="border-right: #617da6 1px solid;
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
                        <asp:BoundColumn DataField="ServiceId" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BeneficiaryName" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BeneficiaryID" HeaderText="Beneficiary Id">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReferenceId" HeaderText="Reference Id">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ServiceName" HeaderText="Service Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Reason" HeaderText="UtilityRef" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Status" HeaderText="Status">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="Date">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnFail" HeaderText="Fail" Text="Fail">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="btnCompete" HeaderText="Complete" Text="Complete">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
            </asp:View>
            <asp:View ID="View1" runat="server">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 80%">
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 40%;">
                            Beneficiary
                        </td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px; width: 40%;">
                            <asp:TextBox ID="txtBenId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            Beneficiary Name</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txt_benName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            Transaction Amount</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                            &nbsp;</td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txtTranAmount" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="True" Width="75%" EnableTheming="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            Reference Id</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px;">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txtReferenceId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            Service Name</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txtServiceName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="width: 196px; height: 20px">
                            Status</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txt_status" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            Transaction Date</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txtTranDate" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                Font-Bold="True" ReadOnly="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            Reference/Reason</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txtUtilityRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                Font-Bold="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                        </td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:Label ID="lbl_serviceId" runat="server" Text="." Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 6px; width: 196px;">
                        </td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 6px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 6px">
                            &nbsp; &nbsp;
                            <table style="width: 191px">
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_Fail" runat="server" Text="Fail Transaction" Style="margin-right: 100px"
                                            BackColor="Red" ForeColor="White" Width="143px" OnClick="btn_Fail_Click" /></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Success" runat="server" Text="Complete Transaction" BackColor="Green"
                                            ForeColor="WhiteSmoke" Width="155px" OnClick="btn_Success_Click" Style="margin-left: 200px;" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <center>
                    <table style="width: 1px">
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_message" runat="server" Text="." Width="596px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="Button3" runat="server" Text="Proceed" BackColor="Green" ForeColor="WhiteSmoke"
                                    Width="173px" OnClick="btn_Proceed_Click" /></td>
                        </tr>
                         <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_realId" runat="server" Text="." Width="596px" Visible="False"></asp:Label></td>
                        </tr>
                    </table>
                </center>
            </asp:View>
        </asp:MultiView>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
