<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Districts.aspx.cs" Inherits="Districts" Title="DISTRICTS" %>
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
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View3" runat="server">
            <table style="width: 90%" align="center">
                <tr>
                    <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                        <asp:Button ID="btnCallList" runat="server" BorderStyle="Inset" Font-Bold="True"
                            Font-Names="Cambria" Font-Underline="False"
                            Text="DISTRICT LIST" OnClick="btnCallList_Click" /><asp:Button ID="btnAddDistrict" runat="server"
                                BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria" Font-Strikeout="False"
                                Font-Underline="False" OnClick="btnAddDistrict_Click" Text="ADD DISTRICT"
                                Width="148px" /></td>
                </tr>
            </table>
            <hr />
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View4" runat="server">
            <table style="width: 90%" align="center">
                <tr>
                    <td style="width: 98%; height: 2px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 5px">
                        <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 95%; border-bottom: #617da6 1px solid">
                            <tr>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    Search String(Names)</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    REGION</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    IS active</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
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
                                    &nbsp;<asp:TextBox ID="txtSearch" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:DropDownList ID="cboAreas" runat="server" CssClass="InterfaceDropdownList"
                                        OnDataBound="cboAreas_DataBound"
                                        Style="font: menu" Width="95%">
                                    </asp:DropDownList></td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    &nbsp;<asp:CheckBox ID="chkIsactive" runat="server" Font-Bold="True" Text="Tick" /></td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                        Style="font: menu" Text="Search" Width="85px" />&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                        &nbsp;<asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                            text-align: justify" Width="100%">
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
                                <asp:BoundColumn DataField="DistrictID" HeaderText="DistrictID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="No.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DistrictCode" HeaderText="DistrictCode" Visible="False">
                                    <HeaderStyle Width="13%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnEdit" DataTextField="DistrictCode" HeaderText="Edit"
                                    Text="DistrictCode">
                                    <HeaderStyle Width="13%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="DistrictName" HeaderText="District Name">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AreaName" HeaderText="Region">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Active" HeaderText="Active">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                        </asp:DataGrid></td>
                </tr>
            </table>
        </asp:View>
        &nbsp;
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
                                                    DISTRICT DETAILS</td>
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
                                                    DISTRICT REGION</td>
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
                                                Code</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtcode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox>
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
                                                Region</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow"><asp:DropDownList ID="cboRegion" runat="server" CssClass="InterfaceDropdownList"
                                        OnDataBound="cboRegion_DataBound"
                                        Style="font: menu" Width="60%">
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Is Active</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:CheckBox ID="chkActive" runat="server" Font-Bold="True" Text="Tick" />&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="Button1_Click" Style="font: menu" Text="SAVE DISTRICT" Width="150px" /></td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <hr />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>&nbsp;&nbsp;&nbsp;&nbsp;<br />
    
    <script type="text/javascript">

    function changeButtonText(button) {

        button.value = "Processing";

    }

    </script>

    <asp:Label ID="lblcode" runat="server" Text="0" Visible="False"></asp:Label>
</asp:Content>



