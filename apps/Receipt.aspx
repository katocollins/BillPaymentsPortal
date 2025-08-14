<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receipt.aspx.cs" Inherits="Receipt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PAYMENT RECEIPT</title>
      <link href="scripts/WQC_stylesheet.css" rel="stylesheet" type="text/css" />
      <link href="scripts/globalscape.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="lblmsg" runat="server" Font-Bold="False" Font-Names="Arial Narrow"
            ForeColor="Red" Style="font: menu" Text="."></asp:Label><br />
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View2" runat="server">
                <table align="center" style="width: 95%">
                    <tr>
                        <td style="width: 100%; height: 2px; text-align: center">
                            <input id="Button3" accesskey="P" class="PrintButton" onclick="window.print();" size="20"
                                style="font-weight: bold; font-size: 12pt; font-family: cambria" type="button"
                                value="Print Receipt" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 50px; text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 150px; text-align: center">
                            <table align="center" cellpadding="0" cellspacing="0" class="style12" style="border-right: #617da6 2px double;
                                border-top: #617da6 2px double; border-left: #617da6 2px double; border-bottom: #617da6 2px double; height: 25px;"
                                width="100%">
                                <tr>
                                    <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="UMEME LIMITTED"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Text="."></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="vertical-align: top; height: 2px; text-align: center">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Reciept No</td>
                                                <td style="width: 2%; height: 20px;">
                                                </td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblRecieptno" runat="server" Text="Label" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Agent Ref</td>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblAgentRef" runat="server" Text="Label" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Account No</td>
                                                <td style="width: 2%">
                                                    &nbsp;</td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblCustRef" runat="server" Text="Label" Font-Size="Medium"></asp:Label>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; height: 20px; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Customer Name</td>
                                                <td style="width: 2%; height: 20px">
                                                </td>
                                                <td style="width: 65%; height: 45px; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblCustname" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Cashier</td>
                                                <td style="width: 2%; height: 20px">
                                                </td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblcashier" runat="server" Text="Label" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Payment Date</td>
                                                <td style="width: 2%; height: 20px">
                                                </td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblPayDate" runat="server" Text="Label" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                    </td>
                                    <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    OutStanding</td>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lbloldbal" runat="server" Text="Label" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Amount Paid</td>
                                                <td style="width: 2%">
                                                    &nbsp;</td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblamount" runat="server" Text="Label" Font-Size="Medium"></asp:Label>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; width: 35%; font-family: 'Courier New'; text-align: justify">
                                                    Payment Mode</td>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="font-weight: bold; width: 65%; font-family: 'Courier New'; text-align: justify">
                                                    <asp:Label ID="lblPaymode" runat="server" Text="Label" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Provisional.
                                                    Balance
                                                </td>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; height: 45px; text-align: left;">
                                                    <asp:Label ID="lblbal" runat="server" Text="Label" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; text-align: justify; font-weight: bold; font-family: 'Courier New';">
                                                    Cash Office</td>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 65%; font-weight: bold; font-family: 'Courier New'; text-align: justify;">
                                                    <asp:Label ID="lblDistrict" runat="server" Font-Size="Medium" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; width: 35%; font-family: 'Courier New'; text-align: justify">
                                        Signiture</td>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="font-weight: bold; width: 65%; font-family: 'Courier New'; text-align: justify">
                                                    &nbsp;.........................</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 50px; text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 2px; text-align: center">
                            <asp:Button ID="Button4" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                OnClick="Button4_Click" Style="font: menu" Text="RETURN" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 2px; text-align: center">
                            <hr />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblcode" runat="server" Text="." Visible="False" Width="16px"></asp:Label><asp:Label ID="lblvendorcode"
                    runat="server" Text="." Visible="False"></asp:Label></asp:View>
            &nbsp;&nbsp;<asp:View ID="View1" runat="server">
                <table align="center" style="width: 90%">
                    <tr>
                        <td style="width: 100%; height: 2px; text-align: center"><asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                OnClick="Button2_Click" Style="font: menu" Text="PRINT RECEIPT" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 1px; text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 10px; text-align: center">
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                BorderStyle="Solid" Style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                border-bottom: 1px solid" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 1px; text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 2px; text-align: center">
                            <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                OnClick="Button4_Click" Style="font: menu" Text="RETURN" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 2px; text-align: center">
                            <hr />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label13" runat="server" Text="." Visible="False" Width="16px"></asp:Label><asp:Label
                    ID="Label14" runat="server" Text="." Visible="False"></asp:Label></asp:View>
        </asp:MultiView>&nbsp;</div>
    </form>
</body>
</html>
