<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="ResendTransaction.aspx.cs" Inherits="ResendTransaction" Title="Untitled Page" %>
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
                            RESEND &nbsp;TRANSACTION(S)
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid; height: 1px;">
            <tr>
            <td style="width: 98%; height: 2px; background-color: #ffff99;">
                <asp:Label ID="lblTxnMsg" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
                    <tr>
                        <td style="width: 98%; height: 2px; background-color: #66ff00;">
                            <strong>
                            SELECT VENDOR</strong>
                            <asp:DropDownList ID="cboVendor" runat="server" Width="226px" style="background-color: #ffffff">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: left">
                            RESEND SINGLE TRANSACTION &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; OR &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; UPLOAD AN EXCEL FILE OF TRANSACTION IDS TO RESEND&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 3px;
                            text-align: left">
                            ENTER TRANSACTION ID:&nbsp;&nbsp;
                            <asp:TextBox ID="txnstatus" runat="server" Style="font: menu; background-color: #ffffff;" Width="14%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ResendtxnBtn" runat="server" Font-Size="9pt" Height="23px" OnClick="btnResendTxn_Click"
                                        Style="font: menu" Text="Search" Width="85px"  OnClientClick = "Confirm()"/>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            UPLOAD EXCEL FILE&nbsp; 
                            <asp:FileUpload id="FileUpload1" runat="server" style="background-color: #ffffff" /><br /><br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="ResendtxnsBtn" runat="server" Text="Upload File" Width="111px" OnClick="ResendtxnsBtn_Click"/></td></tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 11px;
                            text-align: left">
                            <br />
                            &nbsp;&nbsp; &nbsp; <asp:Label ID="Label1" runat="server" Text="."></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                           </td>
                    </tr>
                                        
                    <tr>
                    <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 50px; border-bottom: #617da6 1px solid; height: 20px;">
                        <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            <table style="width: 811px">
                                <tr>
                                    <td style="background-color: #ffffff; width: 337px;">
                                        <input id="OkBtn" style="width: 122px" type="button" runat="server" value="YES"  onserverclick="OkBtn_Click"/></td>
                                    <td style="background-color: #ffffff">
                                        <input id="YesBtn" style="width: 104px" type="button" runat="server" value="YES" onserverclick="YesBtn_Click"/></td>
                                    <td style="background-color: #ffffff">
                                        <input id="NoBtn" type="button" runat="server" value="Cancel" onserverclick="NoBtn_Click"/></td>
                                </tr>
             
                            </table>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <table>
                                               <tr>
            <td style="width: 98%; height: 2px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%"> <%--OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged--%>
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                    <Columns>
                     <asp:BoundColumn DataField="TranId" HeaderText="TranId">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="VendorTranId" HeaderText="VendorTranId">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerRef" HeaderText="CustomerRef">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerName" HeaderText="CustomerName">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="TranAmount" HeaderText="TranAmount">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorCode" HeaderText="VendorCode">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UtilityCode" HeaderText="UtilityCode">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SentToUtility" HeaderText="SentToUtility">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SentToVendor" HeaderText="SentToVendor">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Status" HeaderText="Status">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="RecordDate">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="utilitySentDate" HeaderText="utilitySentDate">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid></td>
        </tr>
                            </table>
                          
                        </asp:View>
                            
                        </asp:MultiView></td>
                            </tr> 
                            
                            </table></td></tr>
                             </table>
</asp:Content>
