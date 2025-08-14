<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true"
    CodeFile="NWSCReversals.aspx.cs" Inherits="NWSCReversals" Title="TRANSACTIONS" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
                                        <td class="InterfaceHeaderLabel">
                                            ADD NWSC REVERSAL</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <!-- /.row -->
                    <table style="width: 100%">
                        <tr>
                              
                            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 0.5px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                                <label>TransactionID</label>
                                <asp:TextBox ID="textbox1" runat="server" Style="font: menu" Width="70%" placeholder="Enter text" />

                            </td>
                                  
                                           
                            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                border-right-color: #617da6;">
                               
                                <div class="button-wrapper">
                                    <asp:Button ID="btnSubmit" Width="130px" Height="20px" runat="server" Text="SEARCH"
                                        OnClick="btnSubmit_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
             
                    <hr />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 98%; height: 2px">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                    <tr>
                                        <td style="vertical-align: middle; width: 98%; height: 2px; text-align: center;">
                                            <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>

                                            <asp:GridView ID="DataGrid1" runat="server" AutoGenerateColumns="false" PageSize="30" CellPadding="4" CellSpacing="2" OnItemCommand="BTN_ItemCommand" OnRowCommand="gvOnRowCommand">
                                        <AlternatingRowStyle BackColor="#BFE4FF" />
                                        <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                        <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                                      <Columns>
                                                          
                                    <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code">
                                        <HeaderStyle  />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CustomerType" HeaderText="Customer Type">
                                        <HeaderStyle  />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TransNo" HeaderText="TransNo">
                                        <HeaderStyle  />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VendorTranId" HeaderText="VendorTranId">
                                        <HeaderStyle />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UtilityCode" HeaderText="Utility Code">
                                        <HeaderStyle  />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CustomerRef" HeaderText="Cust. Reference">
                                        <HeaderStyle  />
                                        <ItemStyle />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TranAmount" HeaderText="Amount">
                                        <HeaderStyle/>
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RecordDate" HeaderText="RecordDate">
                                        <HeaderStyle  />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" HeaderText="Status">
                                        <HeaderStyle  />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SentToUtility" HeaderText="Sent to Utility">
                                        <HeaderStyle />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                        <asp:BoundField DataField="VendorToken" HeaderText="VendorToken">
                                        <HeaderStyle  />
                                        <ItemStyle  />
                                    </asp:BoundField>
                                    <asp:TemplateField  HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtedit" runat="server" Style="font: menu"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="btnComplete" HeaderText="Action" Text="Fail and Reverse">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" ForeColor="Blue"  />
                                    </asp:ButtonField>

                                  </Columns> 
                                     
                                    </asp:GridView>



                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    
                    <%-- /row --%>
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
        
    </asp:MultiView>
</asp:Content>
