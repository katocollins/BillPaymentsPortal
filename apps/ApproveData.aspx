<%@ Page Title="" Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true" CodeFile="ApproveData.aspx.cs" Inherits="ApproveData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <p class="text-center h6">Data Activations Pending Approval </p>
    <br />
    <div style="padding:10px;">
    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4">
        <div class="col">
            <div>
                <asp:Label Text="VendorCode" runat="server"></asp:Label><br />
               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            
               </asp:DropDownList>
            </div>
        </div>
        <div class="col">
            <div>
                <asp:Label Text="Beneficiary Tel" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="beneficiary_tel" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col">
            <div>
                <asp:Label Text="Start Date" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="Sdate" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
         <div class="col">
            <div>
                <asp:Label Text="End Date" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="Edate" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col">
            <div>
               <br />
                <asp:Button runat="server" Text="Search" CssClass="customBtn" OnClick="btnOK_Click" />
            </div>
        </div>
    </div>

    <div>
        <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4">
                        <div class="col">
                             <div>
                                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Bold="True" OnCheckedChanged="CheckBox2_CheckedChanged" Text="Select All" />
                            </div>
                        </div>
                         <div class="col">
                             <div>
                                <asp:Button ID="btnApprove" runat="server" Font-Size="9pt" Height="23px" OnClick="btnApprove_Click" Text="APPROVE TRAN" Width="239px" Style="font: menu" />
                            </div>
                        </div>
                        <div class="col">
                             <div>
                            <asp:Button ID="btnReject" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReject_Click"
                                Text="REJECT TRANS" Width="239px" Style="font: menu" />
                            </div>
                        </div>
                        <div class="col">
                            <div>
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" Font-Bold="True" OnCheckedChanged="chkSelect_CheckedChanged" Text="Select All" />
                            </div>
                        </div>
                  </div>
                <hr />
                <asp:Label ID="lblTotal" runat="server" Text="." Font-Bold="True" ForeColor="#0000C0"></asp:Label></asp:View>
        </asp:MultiView>
    </div>
    <div>
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                    font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                    border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="TransactionId" HeaderText="Transaction Id">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerRef" HeaderText="Phone Number">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="CustomerName" HeaderText="Customer Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UtilityCode" HeaderText="Utility Code">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        
                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                            <HeaderStyle Width="20%" HorizontalAlign="Right" />
                            <ItemStyle Width="120px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="Record Date">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="120px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.IsApproved") %>'
                                    Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle Width="2%" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
    </div>
    </div>
    
    <br />
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="Sdate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="Edate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>


