<%@ Page Title="" Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true" CodeFile="PrepaidAutoReconciliation.aspx.cs" Inherits="PrepaidAutoReconciliation" %>

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

    
    
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="text-align: center; vertical-align: middle; height: 41px;">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel">
                                        FILE RECONCILIATION</td>
                                </tr>
                            </table>
            </td>
        </tr>
        </table>
            <table cellpadding="0" cellspacing="0" class="style12">
        <tr>
            <td style="width: 100%">
                                                                </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
                <table style="width: 100%">
                          <tr>

                            <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Utility Type</label>
                                    <asp:DropDownList ID="cboType" runat="server" Style="font: menu" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="cboType_SelectedIndexChanged">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                                               <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Utility</label>
                                    <asp:DropDownList ID="cboTelecom" runat="server" Style="font: menu" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="cboTelecom_SelectedIndexChanged">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Account</label>
                                    <asp:DropDownList ID="ddOva" runat="server" Style="font: menu" Width="90%">
                                       
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                                </td>
                                <td style="vertical-align: middle; width: 5%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>From Hour</label>
                                    <asp:DropDownList ID="ddFromHour" runat="server" Style="font: menu" Width="90%">                                      
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                                </td>
                                <td style="vertical-align: middle; width: 5%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>To Hour</label>
                                    <asp:DropDownList ID="ddToHour" runat="server" Style="font: menu" Width="90%">                                      
                                    </asp:DropDownList>
                                </td>
                                <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label >Reconcile From Archieves</label>
                                     <asp:CheckBox Text="" ID="chkboxArchieves" Style="font: menu" Width="90%" runat="server" />
                                </td>
                               <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>File </label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                               </td>
                                 
                          </tr>                            
                       </table>
                      <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
                        <tr>
                            <td style="text-align: center; vertical-align: middle; height: 41px;">
                                   <asp:Button ID="btnSubmit" Width="250px" Height="20px" runat="server" Text="Send For Reconciliation" OnClick="btnOK_Click"/>                                  
                            </td>
                        </tr>
                        </table>
        </asp:View>
        &nbsp;&nbsp;
        <asp:View ID="View2" runat="server">
            <table style="width: 90%" align="center">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                            border-left: #617da6 1px solid; width: 70%; border-bottom: #617da6 1px solid">
                            <tr>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 20%; border-bottom: #617da6 1px solid">
                                    <asp:RadioButton ID="rdPdf" runat="server" Font-Bold="True" GroupName="FileFormat"
                                        Text="PDF" /></td>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 20%; border-bottom: #617da6 1px solid">
                                    <asp:RadioButton ID="rdExcel" runat="server" Font-Bold="True" GroupName="FileFormat"
                                        Text="EXCEL" /></td>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 30%; border-bottom: #617da6 1px solid">
                        <asp:Button ID="Button3" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="CONVERT TO PDF" Width="150px" Font-Bold="True" OnClick="Button3_Click" style="font: menu" /></td>
                                <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                    width: 30%; border-bottom: #617da6 1px solid">
                        <asp:Button ID="Button2" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="RETURN" Width="150px" Font-Bold="True" OnClick="Button1_Click" style="font: menu" /></td>
                            </tr>
                        </table>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 1px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center; height: 2px;">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="Horizontal" HorizontalAlign="Justify" PageSize="50" Style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                            border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="No." HeaderText="No.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VendorRef" HeaderText="Agent Ref">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PayDate" HeaderText="Pay Date">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TransactionAmount" HeaderText="Amount">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Justify" Width="35%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Justify" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp"><asp:Button ID="Button4" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="CONVERT TO PDF" Width="150px" Font-Bold="True" OnClick="Button3_Click" style="font: menu" />
                        <asp:Button ID="Button1" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="RETURN" Width="150px" Font-Bold="True" OnClick="Button1_Click" style="font: menu" /></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />

</asp:Content>

