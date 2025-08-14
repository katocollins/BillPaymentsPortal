<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="UmemeDirectPage.aspx.cs" Inherits="UmemeDirectPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
        <table style="width: 90%" align="center">
            <tr>
                <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 98%; border-bottom: #617da6 1px solid">
                        <tr>
                            
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                text-align: center">
                               SELECT BANK
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; height: 1px;
                                text-align: center">
                            </td>
                        </tr>
                        <tr>
                            
                            <td style="vertical-align: middle; width: 27%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                <asp:DropDownList ID="cboUtility" runat="server" 
                                CssClass="InterfaceDropdownList"
                                Style="font: menu" Width="95%">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                            <td  style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                                border-right-color: #617da6">
                            
                                <asp:Button ID="Button1" runat="server" Font-Bold="True" BackColor="Green" Font-Size="9pt" Height="23px"
                                    OnClick="Activate_Click" Style="font: menu" Text="ACTIVATE" Width="20%" />
                                 &nbsp; &nbsp; &nbsp; &nbsp;
                              
                                 <asp:Button ID="Button2" runat="server" Font-Bold="True" BackColor="Red" Font-Size="9pt" Height="23px"
                                    OnClick="Deactivate_Click" Style="font: menu" Text="DEACTIVATE" Width="20%" />

                            </td>
                        </tr>
                        
                    </table>
                    <br />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:MultiView ID="MultiView2" runat="server">
            <asp:View ID="View2" runat="server">
                <h4 style="text-align:center;">ACTIVE UMEME DIRECT</h4>
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" 
                    PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%" >
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
                        <asp:BoundColumn DataField="BankCode" HeaderText="Bank Code" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BankName" HeaderText="Bank Name" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn DataField="LastSwitchDate" HeaderText="LastSwitchDate" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IsActive" HeaderText="Active" Visible="true"></asp:BoundColumn>                       
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" 
                        Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" 
                        Font-Underline="False" 
                        ForeColor="Black" />
                </asp:DataGrid>
            </asp:View>
            <asp:View ID="View1" runat="server">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 80%">
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 40%;">
                            Tran ID
                        </td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px; width: 40%;">
                            <asp:TextBox ID="txtBenId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="True" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            Customer Name</td>
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
                            CURRENT Reference</td>
                        <td class="InterFaceTableMiddleRowUp" style="width: 12%; height: 20px">
                        </td>
                        <td class="InterFaceTableRightRow" style="height: 20px">
                            <asp:TextBox ID="txtUtilityRefOld" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                ReadOnly="true" ForeColor="DarkBlue" Width="75%"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 196px;">
                            NEW Reference</td>
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
                                       <asp:Button ID="btn_Success" runat="server" Text="Update Customer Reference" 
                                       BackColor="Green"
                                            ForeColor="WhiteSmoke" OnClick="btn_Success_Click" Style="margin-left: 200px;" /></td>
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
    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>--%>
</asp:Content>

