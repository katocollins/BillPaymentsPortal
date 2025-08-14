<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="MTNTransactionTrace.aspx.cs" Inherits="MTNTransactionTrace"
    Title="TRACE MTN TRANSACTION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
       <div class="panel-heading" style="font-weight: 700">
            <br />
            Trace MTN Transaction By Phone Number<div class="col-md-2">
                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 98%; border-bottom: #617da6 1px solid">
                        <tr>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 20%; height: 18px;
                                text-align: center">
                                MTN PHONE NUMBER
                            </td>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                text-align: center">
                                UTILITY
                            </td>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                text-align: center">
                                FROM DATE
                            </td>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                text-align: center">
                                TO DATE
                            </td>
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
                                &nbsp;<asp:TextBox ID="txtVendorTranID" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                    Font-Bold="True" Font-Size="Medium" Width="90%"></asp:TextBox>
                            </td>
                            <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                <asp:DropDownList ID="cboUtility" runat="server" 
                                CssClass="InterfaceDropdownList"
                                Style="font: menu" Width="95%">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                border-right-color: #617da6">
                                &nbsp;<asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="txtfromDate_CalendarExtender" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
                            </td>
                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                border-right-color: #617da6">
                                <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
                            </td>
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
                                <asp:Button ID="Button3" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                    OnClick="Button2_Click" Style="font: menu" Text="Search" Width="20%" />
                            </td>
                        </tr>
                    </table>
                    </div>
        </div>
        <div class="panel-body">
            <asp:GridView ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover" UseAccessibleHeader="true" GridLines="None"
                HorizontalAlign="Justify" PageSize="30" DataKeyField="RecordId">
                <Columns>
                    <asp:boundfield  DataField="VendorTranId" HeaderText="ID"></asp:boundfield>
                    <asp:boundfield  DataField="CreationDate" HeaderText="Validation Date"></asp:boundfield>
                    <asp:boundfield  DataField="Request"  HeaderText="Request Data"></asp:boundfield>
                    <asp:boundfield  DataField="Response" HeaderText="Response Data"></asp:boundfield>                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
     <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" 
        runat="Server" 
        EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
</asp:Content>
