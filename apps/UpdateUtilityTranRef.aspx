<%@ Page Title="" Language="C#" MasterPageFile="~/OtherReportsmaster.master" AutoEventWireup="true" CodeFile="UpdateUtilityTranRef.aspx.cs" Inherits="UpdateUtilityTranRef" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                         UPDATE UTILITY TRANSACTION REFERENCE</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            VENDOR TRAN ID</td>
                       <%-- <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            VENDOR CODE</td>--%>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                        </td>
                        <td class="ddcolortabsline2" colspan="1" style="vertical-align: middle; height: 1px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="cboTranId" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                      <%--  <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="cboVendorCode" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>--%>
                       
                        <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 30%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" /></td>
                    </tr>
                    <tr>
                        <td colspan="6" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <div style="text-align: center">
                    <table align="center" style="width: 80%">
                        <tr>
                            <td style="width: 100%; text-align: center;">

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
                                    <asp:BoundField DataField="UtilityTranRef" HeaderText="UtilityTranRef">
                                        <HeaderStyle  />
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
                                    <asp:TemplateField  HeaderText="UtilityTranRef">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtedit" runat="server" Style="font: menu"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="btnComplete" HeaderText="Action" Text="Update Utility Tran Ref">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" ForeColor="Blue"  />
                                    </asp:ButtonField>

                                  </Columns> 
                                     
                                    </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
    </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
   
    <br />
</asp:Content>

