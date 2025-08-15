<%@ Page Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true" CodeFile="InternalPayment.aspx.cs" Inherits="InternalPayment" Title="INTERNAL PAYMENT POSTING" %>
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
    <asp:MultiView ID="MultiView5" runat="server">
        <asp:View ID="View5" runat="server">
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View3" runat="server">
            <table style="width: 90%" align="center">
                <tr>
                    <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 75%">
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 35%; height: 20px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    CUstomer account no/ Invoice Ref</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                </td>
                                <td class="InterFaceTableRightRow" style="width: 20%; height: 20px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="3" style="height: 1px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 35%; text-align: center; height: 20px;">
                                    <asp:TextBox ID="txtCustRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" Font-Size="Medium" Style="text-align: center" Width="90%"></asp:TextBox></td>
                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                </td>
                                <td class="InterFaceTableRightRow" style="width: 20%; text-align: center; height: 20px;">
                                    <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="Button2_Click" Style="font: menu" Text="IDENTIFY" Width="90%" /></td>
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
                                            <td class="InterFaceTableLeftRowUp">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
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
                                                <asp:TextBox ID="txtbal" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="75%" ReadOnly="True" Font-Bold="True"></asp:TextBox></td>
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
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Agent</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%; color: red">
                                                <asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                                    OnDataBound="cboVendor_DataBound" Style="font: menu" Width="75%">
                                                </asp:DropDownList>&nbsp; *</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Agent Ref</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%; color: red">
                                                <asp:TextBox ID="txtagentRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" Width="75%"></asp:TextBox>
                                                *</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Paying for</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%; color: red;">
                                                <asp:DropDownList ID="cboPayType" runat="server" Width="75%" OnDataBound="cboPayType_DataBound">
                                                </asp:DropDownList>
                                                &nbsp;*</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 30%">
                                                Payment Mode</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="width: 70%">
                                                &nbsp;<asp:RadioButton ID="rdcash" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                    Text="CASH" AutoPostBack="True" OnCheckedChanged="rdcash_CheckedChanged" />&nbsp;<asp:RadioButton ID="rdcheque" runat="server" Font-Bold="True"
                                                        GroupName="FileFormat" Text="CHEQUE" AutoPostBack="True" OnCheckedChanged="rdcheque_CheckedChanged" />
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Amount</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="color: red">
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    onkeyup="javascript:this.value=Comma(this.value);"  Width="75%" Font-Bold="True"></asp:TextBox>&nbsp;
                                                *</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Send Sms</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:CheckBox ID="chkSendSms" runat="server" Font-Size="Smaller" Text="Tick If Sms to Be Sent" /></td>
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
                            OnClick="Button1_Click" OnClientClick="return confirm('Are you sure you want to post this Payment?');" Style="font: menu" Text="POST PAYMENT" Width="150px" /></td>
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
 <script type ="text/javascript">
 
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





