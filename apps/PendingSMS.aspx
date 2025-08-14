<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ReportMaster.master" CodeFile="PendingSMS.aspx.cs" Inherits="PendingSMS" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div id="page-wrapper">

                <div class="container-fluid">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 98%; height: 2px">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                    <tr>
                                        <td style="vertical-align: middle; width: 98%; height: 2px; text-align: center;">
                                            <% 
                                                string IsError = Session["IsError"] as string;
                                                if (IsError == null)
                                                {
                                                    Response.Write("<div>");

                                                }
                                                else if (IsError == "True")
                                                {
                                                    Response.Write("<div class=\"alert alert-danger\">");

                                                }
                                                else
                                                {
                                                    Response.Write("<div class=\"alert alert-success\">");
                                                }
                                            %>
                                            <strong>
                                                <asp:Label ID="lblmsg" runat="server"></asp:Label></strong>
                                            <%Response.Write("</div>"); %>
                                            <asp:Label ID="lblCount" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table style="width: 100%">
                        <tr>
                            <td style="width: 98%; height: 2px">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                    <tr>
                                        <td class="InterfaceHeaderLabel">TOTAL PENDING SMS REPORT(SMS PORTAL DB)</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <!-- /.row -->
                    <table style="width: 100%; margin-top: 20px;">
                        <tr>
                            <%--<td style="vertical-align: middle; width: 10%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">--%>



                               <%-- <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;"
                                    colspan="2">
                                    <label>From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" Style="font: menu" Width="70%" Height="30px" placeholder="Enter text" />
                                </td>
                                <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;"
                                    colspan="2">
                                    <label>To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" Style="font: menu" Width="70%" Height="30px" placeholder="Enter text" />
                                </td>--%>
                                <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;"
                                    colspan="2">
                                    <asp:Button ID="Button1" Width="130px" Height="29px" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                                </td>
                        </tr>
                    </table>
                    <hr />

                    <br />


                    <hr />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 98%; height: 2px">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                    <tr>
                                        <td style="vertical-align: middle; width: 98%; height: 2px; text-align: center;">
                                            <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:MultiView runat="server" ID="Multiview2">
                        <asp:View runat="server" ID="resultView">
                            <div class="row">
                                <div class="table-responsive">
                                    <label>TOTAL PENDING COUNT -- </label>
                                    <strong>
                                        <asp:Label Font-Size="Large" ID="count" runat="server"></asp:Label></strong>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover"
                                        ID="dataGridResults" AllowPaging="true"
                                        PageSize="30" CellPadding="4" CellSpacing="2">
                                        <AlternatingRowStyle BackColor="#BFE4FF" />
                                        <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                        <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                        <%--<Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="ResendToken" HeaderText="ResendToken"
                                                ShowHeader="True" Text="ResendToken" />
                                        </Columns>--%>
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="EmptyView"></asp:View>
                    </asp:MultiView>
                    <%-- /row 
                       
                        <%-- Scripts--%>
                    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                        EnableScriptLocalization="true">
                    </ajaxToolkit:ToolkitScriptManager>
                    <br />
                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>--%>
                    <%--/Scripts
                        <%--</form>--%>
                    <%--</div>--%>
                    <!-- /.row -->

                </div>
                <!-- /.container-fluid -->
            </div>
            <!-- /#page-wrapper -->
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
</asp:Content>
