<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true"
    CodeFile="XtraFloatWhiteList.aspx.cs" Inherits="XtraFloatWhiteList" Title="XtraFloat WhiteList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:MultiView runat="server" ID="multiview1">
            <asp:View runat="server" ID="view1">
                <strong>
                    <asp:Label ID="lblmsg" runat="server"></asp:Label></strong>
                <%Response.Write("</div>"); %>
            </asp:View>
        </asp:MultiView>
    </div>
    <table style="width: 100%" id="TABLE1" onclick="return TABLE1_onclick()">
        <tr>
            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                border-right-color: #617da6;">
                Telephone Number&nbsp;
                <br />
                <asp:TextBox ID="txtAccountNumber" runat="server" Style="font: menu" Width="90%"
                    placeholder="Enter text" /></td>
            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                border-right-color: #617da6;">
                Search
                <br />
                <asp:Button ID="btnSubmit" Width="130px" Height="20px" runat="server" Text="Search"
                    OnClick="btnSubmit_Click" /></td>
        </tr>
    </table>
    <div style="width: 100%">
        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover"
            ID="dataGridResults" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging"
            PageSize="5000" CellPadding="4" CellSpacing="2" OnRowCommand="dataGridResults_RowCommand">
            <AlternatingRowStyle BackColor="#BFE4FF" />
            <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
            <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            <Columns>
                <%--        <asp:ButtonField ButtonType="Button" CommandName="ResendToken" HeaderText="ResendTocken"
                    ShowHeader="True" Text="ResendToken" />--%>
            </Columns>
        </asp:GridView>
       <%-- <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </ajaxToolkit:ToolkitScriptManager>
        <br />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtFromDate">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtToDate">
        </ajaxToolkit:CalendarExtender>--%>
    </div>
</asp:Content>
