<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="UssdLogs.aspx.cs" Inherits="UssdLogs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ViewNEW" runat="server">
            </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View3" runat="server">
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
                                               <asp:Label ID="InfoTxt" CssClass="text-danger" runat="server"></asp:Label></strong>
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
                                            Ussd Transactions</td>
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
                                <asp:TextBox ID="TranId" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Enter vendor transaction ID" />
                            </td>

                             <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">VendorCode</asp:Label>
                                <asp:TextBox ID="VendorCode" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Enter vendor code" />
                            </td>

                             <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">Phone Number</asp:Label>
                                <asp:TextBox ID="PhoneNumber" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Enter phone number" />
                            </td>

                             <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">Payment Reference</asp:Label>
                                <asp:TextBox ID="PaymentReference" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Enter payment reference" />
                            </td>

                             <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">Status</asp:Label>
                                <asp:TextBox ID="Status" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Status" />
                            </td>

                            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">From Network</asp:Label>
                                <asp:TextBox ID="FromNetwork" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Enter network" />
                            </td>

                            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">To Network</asp:Label>
                                <asp:TextBox ID="ToNetwork" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Enter network" />
                            </td>

                             <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                 <asp:Label runat="server" Font-Bold="true">TelecomId</asp:Label>
                                <asp:TextBox ID="TelecomId" style="width:90%" runat="server"
                                     CssClass="form-control nunito-font" placeholder="Enter telecom Id" />
                            </td>

                            

                             </tr>
                    </table>

                    <div class="row">
                        <div class="col-md-3">
                                 <asp:Label runat="server" Font-Bold="true">Start Date</asp:Label>
                                 <input type="date" ID="startDate" style="width:90%" runat="server" class="form-control nunito-font" />
                        </div>

                        <div class="col-md-3">
                                 <asp:Label runat="server" Font-Bold="true">End Date</asp:Label>
                                 <input type="date" ID="endDate" style="width:90%" runat="server" class="form-control nunito-font" />
                        </div>

                         <div class="col-md-3 mt-4">
                            <asp:Button ID="Button2" CssClass="btn btn-primary nunito-font"
                                         runat="server" Text="Filter" OnClick="Filter_Click"
                                        />
                        </div>
                        </div>

                   
                    <br />
                  
                   
                      <table>
                        <tr>
                            <td style="width: 20%; height: 2px">
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
                                                Style="font: menu" Text="Convert" Width="85px" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                     <br />
                     
                    <asp:MultiView runat="server" ID="Multiview2">
                        <asp:View runat="server" ID="View4">
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="UssdLogsGridView" runat="server" 
                                         AutoGenerateColumns="false" 
                                        PageSize="30" CellPadding="4" CellSpacing="2" >
                                        <AlternatingRowStyle BackColor="#BFE4FF" />
                                        <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                        <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                                      <Columns>
                                                          
                                                            <asp:BoundField DataField="VendorTranId" HeaderText="VendorTranId" />
                                                            <asp:BoundField DataField="PegPayTranId"  HeaderText="PegPayTranId" />
                                                            <asp:BoundField DataField="FromAccount" HeaderText="FromAccount" />
                                                            <asp:BoundField DataField="CustName" HeaderText="Customer Name" />
                                                            <asp:BoundField DataField="PaymentReference" HeaderText="PaymentReference" />
                                                            <asp:BoundField DataField="TranAmount" HeaderText="TranAmount" />
                                                            <asp:BoundField DataField="TranCharge" HeaderText="TranCharge" />
                                                            <asp:BoundField DataField="MNOCharge"  HeaderText="MNOCharge" />

                                                            <asp:BoundField DataField="CashOutCharge" HeaderText="CashOutCharge" />
                                                            <asp:BoundField DataField="PaymentDate" HeaderText="PaymentDate" />
                                                            <asp:BoundField DataField="RecordDate" HeaderText="RecordDate" />
                                                            <asp:BoundField DataField="Status"  HeaderText="Status" />
                                                            <asp:BoundField DataField="ToAccount" HeaderText="ToAccount" />
                                                            <asp:BoundField DataField="TranType" HeaderText="TranType" />
                                                            <asp:BoundField DataField="PaymentType" HeaderText="PaymentType" />
                                                            <asp:BoundField DataField="FromNetwork"  HeaderText="FromNetwork" />
                                                      
                                                          <asp:BoundField DataField="ToNetwork"  HeaderText="ToNetwork" />
                                                          <asp:BoundField DataField="VendorCode" HeaderText="VendorCode" />
                                                            <asp:BoundField DataField="TelecomId" HeaderText="TelecomId" />
                                                            
                                                      
                                                          
                                                          
                                                            </Columns> 
                                     
                                    </asp:GridView>
                                       </div>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="View5">
                        </asp:View>
                    </asp:MultiView>
                    <%-- /row --%>
                    <%-- Scripts--%>
                    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" EnableScriptGlobalization="true"
                        EnableScriptLocalization="true">
                    </ajaxToolkit:ToolkitScriptManager>
                    <br />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                        Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
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
        <asp:View ID="View6" runat="server">
        </asp:View>
    </asp:MultiView>

 


</asp:Content>


 







