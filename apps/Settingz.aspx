<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Settingz.aspx.cs" Inherits="Settingz" Title="SYSTEM PARAMETERS" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <table style="width: 100%" align="center">
        <tr>
            <td style="width: 100%; text-align: center;">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px; text-align: center;">
                <div style="text-align: center">
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="vertical-align: middle; height: 41px; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            SYSTEM PARAMETERS</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                    <table style="width: 95%" align="center">
                        <tr>
                            <td style="width: 100%">
                                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="Valueid" HeaderText="Valueid" Visible="False">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Isnum" HeaderText="Isnum" Visible="False">
                            <HeaderStyle Width="5px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" HeaderText="Edit" Text="Variable">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="ValueGroupcode" HeaderText="Group Code">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ValueGroupname" HeaderText="Group Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Valuecode" HeaderText="Value Code">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Valuename" HeaderText="Value Name">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ValueVarriable" HeaderText="Varriable">
                            <HeaderStyle Width="30%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></td>
                        </tr>
                    </table>
                </div>
                &nbsp;</td>
        </tr>
    </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table align="center" cellpadding="0" cellspacing="0" class="style12" style="border-right: #617da6 1px solid;
                border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                <tr>
                    <td colspan="2" style="vertical-align: top; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 70%">
                            <tr>
                                <td colspan="3" style="height: 1px">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                    Parameter Name</td>
                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                    &nbsp;</td>
                                <td class="InterFaceTableRightRow" style="height: 20px">
                                    <asp:TextBox ID="txtname" runat="server" BackColor="#FFFFC0" CssClass="InterfaceTextboxLongReadOnly"
                                        ReadOnly="True" Width="60%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRow">
                                    Varriable</td>
                                <td class="InterFaceTableMiddleRow">
                                </td>
                                <td class="InterFaceTableRightRow">
                                    <asp:TextBox ID="txtvalue" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                        Width="60%" Height="69px" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="InterfaceItemSeparator" colspan="3" style="height: 1px">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="vertical-align: top; text-align: center">
                        <asp:Button ID="BtnSave" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="BtnSave_Click" Style="font: menu" Text="UPDATE PARAMETER" Width="122px" />
                        <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="Button1_Click" Style="font: menu" Text="BACK" Width="122px" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="vertical-align: top; text-align: center">
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView></td>
        </tr>
    </table>
    <br />
    <asp:Label ID="lblcode" runat="server" Text="0" Visible="False"></asp:Label>
    <asp:Label ID="lblIsnum" runat="server" Text="." Visible="False"></asp:Label>
</asp:Content>

