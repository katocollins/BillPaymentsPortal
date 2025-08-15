<%@ Page Language="C#" MasterPageFile="~/PaymentMaster.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Invoice" Title="INVOICING" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View3" runat="server">
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="vertical-align: middle; height: 41px; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            CREATE INVOICE</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            <table style="width: 90%" align="center">
                <tr>
                    <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 55%">
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 25%; height: 20px; text-align: center">
                                    Invoice Type</td>
                                <td class="InterFaceTableMiddleRowUp" style="height: 20px; width: 2%;">
                                </td>
                                <td class="InterFaceTableRightRow" style="width: 45%; height: 20px; text-align: center"><asp:DropDownList ID="cboPayTypes" runat="server" Width="75%" OnDataBound="cboPayType_DataBound" AutoPostBack="True" OnSelectedIndexChanged="cboPayTypes_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="3" style="height: 1px; text-align: center">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <hr />
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View1" runat="server">
            <table align="center" style="width: 90%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                            <tr>
                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    CUstomer DETAILS</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    PAYMENT DETAILS</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                First Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtfname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Middle Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtmName" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="75%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Last
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtlname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                email Address</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%" Font-Bold="True"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Phone No.</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%">
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%" Font-Bold="True" MaxLength="10"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Amount</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%">
                                                <asp:TextBox ID="txtamount" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" onkeyup="javascript:this.value=Comma(this.value);" Width="75%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Invoice Type</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%">
                                                <asp:TextBox ID="txtpayingfor" runat="server" BackColor="#E0E0E0" ReadOnly="True"
                                                    Width="75%"></asp:TextBox>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Officer</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtofficer" runat="server" BackColor="#E0E0E0" ReadOnly="True" Width="75%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="Button1_Click" Style="font: menu" Text="SAVE INVOICE" Width="150px" /></td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <hr />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table align="center" style="width: 90%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="Button2_Click1" Style="font: menu" Text="PRINT INVOICE" Width="150px" />&nbsp;
                        <asp:Button ID="Button3" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="Button4_Click" Style="font: menu" Text="RETURN" Width="150px" /></td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 1px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 5px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" style="border-right: #617da6 2px double;
                            border-top: #617da6 2px double; border-left: #617da6 2px double; border-bottom: #617da6 2px double;
                            height: 25px" width="98%">
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 1px; text-align: center">
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
            <asp:Label ID="lblcode" runat="server" Text="." Visible="False" Width="16px"></asp:Label><asp:Label
                ID="lblvendorcode" runat="server" Text="." Visible="False"></asp:Label></asp:View>
    </asp:MultiView>
    &nbsp; &nbsp; &nbsp;<br />    
 <script type ="text/javascript">

     function Comma(Num) {
         Num += '';
         Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
         Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
         x = Num.split('.');
         x1 = x[0];
         x2 = x.length > 1 ? '.' + x[1] : '';
         var rgx = /(\d+)(\d{3})/;
         while (rgx.test(x1))
             x1 = x1.replace(rgx, '$1' + ',' + '$2');
         return x1 + x2;
     }
 </script>

    <asp:Label ID="lblvat" runat="server" Text="0" Visible="False"></asp:Label><br />
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtamount" ValidChars="0123456789,">
    </ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtPhone" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    </asp:Content>





