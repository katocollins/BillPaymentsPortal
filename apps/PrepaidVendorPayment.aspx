<%@ Page Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true"
    CodeFile="PrepaidVendorPayment.aspx.cs" Inherits="PrepaidVendorPayment" Title="PREPAID VENDOR PAYMENT" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:MultiView ID="MultiView5" runat="server">
        <asp:View ID="View5" runat="server">
            <asp:MultiView ID="MultiView2" runat="server">
                <asp:View ID="View2" runat="server">
                    <center>
                        <table cellpadding="5">
                            <tr>
                                <td>
                                    Bulk Payment</td>
                                <td>
                                    Single Payment</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btn_BulkPayment" OnClick="btn_BulkPaymentClicK" Text="Bulk Payment" /></td>
                                <td>
                                    <asp:Button runat="server" ID="Button3" OnClick="btn_SingleClicK" Text="Single Payment" /></td>
                            </tr>
                        </table>
                    </center>
                </asp:View>
                <asp:View ID="View6" runat="server">
                    <center>
                        <table cellpadding="5">
                            <tr>
                                <td>
                                    Prepaid Vendor</td>
                                <td>
                                    Browse For File</td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddl_vendor" runat="server">
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                                <td>
                                    <asp:Button runat="server" ID="Button4" OnClick="btn_uploadClicK" Text="Bulk Payment" /></td>
                            </tr>
                        </table>
                    </center>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <table style="width: 90%" align="center">
                        <tr>
                            <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 75%">
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="width: 26%; height: 20px; text-align: center">
                                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tbody>
                                                    <tr>
                                                        <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                            CUstomer reference</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td class="InterFaceTableMiddleRowUp" style="width: 17%; height: 20px">
                                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tbody>
                                                    <tr>
                                                        <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                            customer area</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px; width: 17%;">
                                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                        Utility</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="InterFaceTableRightRow" style="width: 20%; height: 20px; text-align: center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableRightRow" colspan="4" style="height: 1px; text-align: center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="width: 26%; text-align: center; height: 20px;">
                                            <asp:TextBox ID="txtCustRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
                                        <td class="InterFaceTableMiddleRowUp" style="width: 17%; height: 20px">
                                            <asp:TextBox ID="txt_area" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px; width: 17%;">
                                            <asp:DropDownList ID="ddl_utilityCode" runat="server" Width="268px">
                                            </asp:DropDownList></td>
                                        <td class="InterFaceTableRightRow" style="width: 20%; text-align: center; height: 20px;">
                                            <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                OnClick="Button2_Click" Style="font: menu" Text="IDENTIFY" Width="90%" /></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="width: 26%; height: 20px; text-align: center">
                                        </td>
                                        <td class="InterFaceTableMiddleRowUp" style="width: 17%; height: 20px">
                                        </td>
                                        <td class="InterFaceTableMiddleRowUp" style="width: 17%; height: 20px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="width: 20%; height: 20px; text-align: center">
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
                                                        Account No</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtcode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            Width="75%" ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                        Name</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                        &nbsp;</td>
                                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                                        <asp:TextBox ID="txtname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            Width="75%" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Type</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtCustType" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            ReadOnly="True" Width="75%"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Balance</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtbal" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="75%"
                                                            ReadOnly="True" Font-Bold="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Phone</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            Width="75%" Font-Bold="True" MaxLength="12"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                    </td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                        </td>
                                        <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Amount</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        &nbsp;</td>
                                                    <td class="InterFaceTableRightRow" style="color: red">
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            onkeyup="javascript:this.value=Comma(this.value);" Width="75%" Font-Bold="True"></asp:TextBox>&nbsp;
                                                        *</td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                        Reason</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                    </td>
                                                    <td class="InterFaceTableRightRow" style="color: red; height: 20px;">
                                                        <asp:TextBox ID="txtreason" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="98%"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                    </td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                    </td>
                                                    <td class="InterFaceTableRightRow" style="color: red; height: 20px">
                                                        <asp:Label ID="lbl_utilityCode" runat="server" Text="Label"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
            <table align="center" style="width: 90%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 1px; text-align: center">
                                    <hr />
                                    &nbsp;<asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="Button1_Click" OnClientClick="return confirm('Are you sure you want to post this Payment?');"
                                        Style="font: menu" Text="POST PAYMENT" Width="150px" /></td>
                            </tr>
                        </table>
                        <hr />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table align="center" style="width: 90%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 1px; text-align: center">
                                    <hr />
                                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label></td>
                            </tr>
                        </table>
                        <hr />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    &nbsp; &nbsp; &nbsp; &nbsp;
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
    &nbsp;<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtAmount" ValidChars="0123456789,">
    </ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtPhone" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    &nbsp;
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>
