<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="SMSOutubox.aspx.cs" Inherits="SMSOutubox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <h5 style="margin-top:30px;margin-bottom:20px;">SMS OUTBOX MESSAGES</h5>
         <asp:Label ID="lblmsg" runat="server"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label><br /><br />

        <div style="display:flex;gap:10px; margin-bottom:5px;margin-left:30px;flex-wrap:wrap; width:950px;" align="left">
            <div style="width:300px;padding:5px;">
                <asp:Label Text="Phone Number"  runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="Phone" style="width:250px;  padding:7px;border-color:#808080; border-radius:5px;"></asp:TextBox>
            </div>
            <div style="width:300px;padding:5px;">
                <asp:Label Text="Mask" runat="server"></asp:Label><br />
                <%--<asp:TextBox runat="server" ID="Mask" style="width:250px;  padding:7px;border-color:#808080; border-radius:5px;"></asp:TextBox>--%>
                <asp:DropDownList runat="server" ID="Mask" style="width:250px;  padding:7px;border-color:#808080; border-radius:5px;">
                    <asp:ListItem Text="8888" Value="8888"></asp:ListItem>
                    <asp:ListItem Text="FlexiPay" Value="FlexiPay"></asp:ListItem>
                    <asp:ListItem Text="MAZIMA" Value="MAZIMA"></asp:ListItem>
                    <asp:ListItem Text="MTNDeviceLoa" Value="MTNDeviceLoa"></asp:ListItem>
                    <asp:ListItem Text="MTNXtraFloat" Value="MTNXtraFloat"></asp:ListItem>
                    <asp:ListItem Text="PEGASUS" Value="PEGASUS"></asp:ListItem>
                    <asp:ListItem Text="PEGPAY" Value="PEGPAY"></asp:ListItem>
                    <asp:ListItem Text="SMSINFO" Value="SMSINFO"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:300px;padding:5px;">
                <asp:Label Text="Sender" runat="server"></asp:Label><br />
                <%--<asp:TextBox runat="server" ></asp:TextBox>--%>
                <asp:DropDownList runat="server" ID="Sender" style="width:250px;  padding:7px;border-color:#808080; border-radius:5px;">
                    <asp:ListItem Text="MTNService" Value="MTNService"></asp:ListItem>
                    <asp:ListItem Text="PEGASUS" Value="PEGASUS"></asp:ListItem>
                    <asp:ListItem Text="PEGPAYSMSAPP" Value="PEGPAYSMSAPP"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:300px;padding:5px;">
                <asp:Label Text="Message" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="Message" style="width:250px; padding:7px;border-color:#808080; border-radius:5px;"></asp:TextBox>
            </div>
            <div style="width:300px;padding:5px;">
                <asp:Label Text="Start Time" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="startTime" TextMode="Date" style="width:250px;  padding:7px;border-color:#808080; border-radius:5px;"></asp:TextBox>
            </div>
            <div style="width:300px;padding:5px;">
                <asp:Label Text="End Time" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="endTime" TextMode="Date" style="width:250px;  padding:7px;border-color:#808080; border-radius:5px;"></asp:TextBox>
            </div>
        </div>
        <div align="left" style="margin-left:30px;">
            <asp:Button ID="SearchTransButton" runat="server" Text="Search Messages" style="border-radius:5px;background-color:teal; border-color:transparent; padding:7px;color:white;" OnClick="SearchTransButton_Click" />
        </div><br /><br />

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="ViewNEW" runat="server">
                <asp:DataGrid runat="server" ID="datagrid_cell" CellSpacing="2" CellPadding="7" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">

                    <AlternatingItemStyle BackColor="White" />
                    <Columns>
                        <asp:BoundColumn ItemStyle-Width="10%"  DataField="Phone" HeaderText="Phone"></asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="50%" DataField="Message" HeaderText="Message"></asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="10%" DataField="Mask" HeaderText="Mask"></asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="10%" DataField="sentDate" HeaderText="Sentdate"></asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="10%" DataField="VendorTranId" HeaderText="VendorTranId"></asp:BoundColumn>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

                </asp:DataGrid>
            </asp:View>
        </asp:MultiView>

    </center>
</asp:Content>

