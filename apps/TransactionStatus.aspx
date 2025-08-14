<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="TransactionStatus.aspx.cs" Inherits="BankErrors" Title="TRANSACTIONS STATUS" %>

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
                        <td class="InterfaceHeaderLabel">GET TRANSACTION STATUS</td>
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
            <tr>
            <td style="width: 98%; height: 2px">
                <asp:Label ID="lblTxnMsg" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: left">
                            Search status of single transaction reference &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;or &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; upload an excel file of transaction references
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 7px;
                            text-align: left">
                            Enter Reference:&nbsp;&nbsp;
                            <asp:TextBox ID="txnstatus" runat="server" Style="font: menu" Width="19%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnTxnStatus" runat="server" Font-Size="9pt" Height="23px" OnClick="btnTxnStatus_Click"
                                        Style="font: menu" Text="Search" Width="85px" />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; upload excel file&nbsp; 
                            <asp:FileUpload id="FileUpload1" runat="server" /><br /><br />
                            </td></tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: left">
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                            <asp:Button ID="Button2" runat="server" Text="Upload File" Width="111px" OnClick="Button2_Click" /></td>
                    </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: left">
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
                                    width: 100px; border-bottom: #617da6 1px solid; height: 18px;">
                                    <asp:RadioButton ID="rdPdf" runat="server" Font-Bold="True" GroupName="FileFormat"
                                        Text="PDF" /></td>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid; height: 18px;">
                                    <asp:RadioButton ID="rdExcel" runat="server" Font-Bold="True" GroupName="FileFormat"
                                        Text="EXCEL" /></td>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid; height: 18px;">
                                    <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnConvert_Click"
                                        Style="font: menu" Text="Download As" Width="85px" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
                             
                    <tr>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                        &nbsp;</td>
                    </tr>
                    <tr>
                    <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid">
                                    </td>
                            </tr> 
                            
                            </table></td></tr>
                            <tr>
            <td style="width: 98%; height: 2px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%" OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
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
                         <asp:BoundColumn DataField="BankVendorTranRef" HeaderText="BankVendorTranRef">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="MtnId" HeaderText="MtnId">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerReference" HeaderText="CustomerReference">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerName" HeaderText="CustomerName">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Tin" HeaderText="Tin">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="TranAmount" HeaderText="TranAmount">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SentToCoreStatus" HeaderText="SentToCoreStatus">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SentToUtilityStatus" HeaderText="SentToUtilityStatus">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
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

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

 


<%--<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="TransactionStatus.aspx.cs" Inherits="BankErrors" Title="TRANSACTIONS STATUS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
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
                        <td class="InterfaceHeaderLabel">GET TRANSACTION STATUS</td>
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
            <tr>
            <td style="width: 98%; height: 2px">
                <asp:Label ID="lblTxnMsg" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: left"><center>
                            Enter Transaction Reference:&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txnstatus" runat="server" Style="font: menu" Width="28%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnTxnStatus" runat="server" Font-Size="9pt" Height="23px" OnClick="btnTxnStatus_Click"
                                        Style="font: menu" Text="Search" Width="85px" /></center></td></tr>
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
                                        Style="font: menu" Text="Download As" Width="85px" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
                             
                    <tr>
                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                        &nbsp;</td>
                    </tr>
                    <tr>
                    <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 100px; border-bottom: #617da6 1px solid">
                                    </td>
                            </tr> 
                            
                            </table></td></tr>
                            <tr>
            <td style="width: 98%; height: 2px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%" OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
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
                         <asp:BoundColumn DataField="BankVendorTranRef" HeaderText="BankVendorTranRef">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="MtnId" HeaderText="MtnId">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerReference" HeaderText="CustomerReference">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerName" HeaderText="CustomerName">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Tin" HeaderText="Tin">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="TranAmount" HeaderText="TranAmount">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SentToCoreStatus" HeaderText="SentToCoreStatus">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SentToUtilityStatus" HeaderText="SentToUtilityStatus">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
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

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

 --%>