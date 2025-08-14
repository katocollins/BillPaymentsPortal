<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AreaDetails.aspx.cs" 
Inherits="AreaDetails" 
Title="AREA DETAILS"

Culture="auto" 
UICulture="auto" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
            <table cellpadding="0" cellspacing="0" class="style12">
        <tr>
            <td style="width: 100%">
                                                                </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">

    
    
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="text-align: center; vertical-align: middle; height: 41px;">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel">
                                        OPERATING REGIONS</td>
                                </tr>
                            </table>
            </td>
        </tr>
        </table>
                <table style="width: 90%" align="center">
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;"><table cellpadding="0" cellspacing="0" class="style12" align="center" width="98%">
                            <tr>
                                <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                                    <table style="width: 98%" align="center" cellpadding="0" cellspacing="0" >
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    REGION DETAILS ENTRY</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left" >
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    Region LIST</td>
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
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Is Active</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Font-Bold="True" Text="Tick" /></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableRightRow" colspan="3" style="width: 100%; height: 1px; text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableRightRow" colspan="3" style="width: 100%; text-align: center">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="SAVE REGION" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                                        OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="3" Style="border-right: #617da6 1px solid;
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
                                            <asp:BoundColumn DataField="No." HeaderText="No.">
                                                <HeaderStyle Width="5%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="AreaID" HeaderText="AreaID" Visible="False">
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn DataTextField="AreaCode" HeaderText="Edit" Text="AreaCode" CommandName="btnEdit">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" ForeColor="Blue" />
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="AreaCode" HeaderText="Code">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="AreaName" HeaderText="Name">
                                                <HeaderStyle Width="30%" />
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
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;">
                        <hr />
                                                                </td>
                    </tr>
                </table>
        </asp:View>
        &nbsp;
    </asp:MultiView>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />

</asp:Content>

