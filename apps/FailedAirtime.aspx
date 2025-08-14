<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="FailedAirtime.aspx.cs" Inherits="FailedAirtime" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

                                            <%--Session code goes here--%>
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
                                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                            </strong>
                                               <strong>
                                                   <asp:Label ID="InfoTxt" CssClass="text-danger" runat="server"></asp:Label>
                                                </strong>
                                            <%Response.Write("</div>"); %>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
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
                                        <td class="InterfaceHeaderLabel">
                                           Failed Airtime Transactions</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <!-- /.row -->
                    <table style="width: 100%">
                        <tr>
                             <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">Vendor Transaction ID</asp:Label>
                                <asp:TextBox ID="VendorTranId" style="width:90%" runat="server" CssClass="form-control nunito-font" placeholder="Enter vendor transaction ID" />

                            </td>

                            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                <div class="button-wrapper">
                                    <asp:Button ID="Filter" CssClass="btn btn-primary nunito-font"
                                         runat="server" Text="Filter" OnClick="Filter_Click"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                  
                    <br />

                    <asp:MultiView runat="server" ID="MultiView2">
                        <asp:View runat="server" ID="View2">
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="AirtimeFailedLogsGridView" runat="server" 
                                         AutoGenerateColumns="false" Width="100%"
                                        PageSize="30" CellPadding="4" CellSpacing="2" >
                                        <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                                <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                                    <Columns>
                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="CustomerType" HeaderText="Customer Type" />
                                                    <asp:BoundField DataField="CustomerTel" HeaderText="Customer Tel" />
                                                    <asp:BoundField DataField="TranAmount" HeaderText="TranAmount" />
                                                    <asp:BoundField DataField="RecordDate"  HeaderText="Record Date" />
                                                    <asp:BoundField DataField="VendorTranId" HeaderText="Vendor TranId" />
                                                    <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" />
                                                    <asp:BoundField DataField="UtilityCode" HeaderText="Utility Code" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                    <asp:BoundField DataField="Reason"  HeaderText="Reason" />
                                                    </Columns> 
                                    </asp:GridView>
                                    <br />
                                        <div class="button-wrapper">
                                            <asp:Button ID="Retry" CssClass="btn btn-primary nunito-font"
                                                    runat="server" Text="Retry" OnClick="Retry_Click"/>
                                        </div>
                                 </div>
                            </div>
                        </asp:View>

                        <asp:View runat="server" ID="View5">
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

