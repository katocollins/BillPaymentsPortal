<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="EditAddDataBundle.aspx.cs" Inherits="EditAddDataBundle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div style="font-family:Bahnschrift">
        <asp:Panel runat="server" ID="BodyPanel">
            <center>
                <div style="display:flex; max-width:500px;">
                    <div style="padding:20px;flex-grow:1;">
                        <p>Vendor To Pegasus mapping</p>
                        <asp:RadioButton runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="ViewV2PSettings_CheckedChanged" ID="ViewV2PSettings" />
                    </div>
                    <div style="padding:20px;flex-grow:1;">
                        <p>Pegasus To Telecom mapping</p>
                        <asp:RadioButton runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="ViewP2TSettings_CheckedChanged" ID="ViewP2TSettings" />
                    </div>
                    <div style="padding:20px;flex-grow:1;">
                        <p>Create A New DataBundle</p>
                        <asp:RadioButton runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="NewDataBundle_CheckedChanged" ID="NewDataBundle" />
                    </div>
                </div>
            </center>
            <asp:MultiView runat="server" ID="MultiView1">


                <%--Shows the table for the various vendor to pegasus configs--%>
                <asp:View runat="server" ID="V2Pmapping">
                    <div>
                        <div style="margin-bottom:20px;">
                            <asp:Datagrid runat="server" ID="PegPayConfigs" AutoGenerateColumns="False" CssClass="table" OnItemDataBound="PegPayConfigs_ItemDataBound" CellPadding="5" GridLines="None" OnItemCommand="PegPayConfigs_ItemCommand">

                                <AlternatingItemStyle BackColor="lightcyan" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                                <Columns>
                                    <asp:BoundColumn DataField="RecordId" HeaderText="Bundle ID">
                                        <HeaderStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Duration" HeaderText="Duration">
                                        <HeaderStyle Width="10px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BundleCode" HeaderText="BundleCode">
                                        <HeaderStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BundleVolume" HeaderText="BundleVolume">
                                        <HeaderStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BundleAmount" HeaderText="BundleAmount">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundColumn>
                                     <asp:BoundColumn DataField="IsActive" HeaderText="IsActive">
                                        <HeaderStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" HeaderText="Edit" CommandName="Edit_Bundle" Text="Edit Bundle">
                                        <HeaderStyle Width="10%" />
                                    </asp:ButtonColumn>
                                </Columns>

                                <HeaderStyle BackColor="Gray" Font-Bold="False" Font-Italic="False" Font-Names="Bahnschrift" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                <ItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Bahnschrift Light" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:Datagrid>
                        </div>
                    </div>
                </asp:View>


                <%--Shows More info for the selected DataBundle--%>
                <asp:View runat="server" ID="MoreInfo">
                    <div align="center">
                        <div style="display:flex; max-width:600px; flex-wrap:wrap;" align="left">

                            <!--Peg Config RecordID -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Bundle Id"></asp:Label><br />
                                <asp:TextBox runat="server" ID="PegRecordId" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleCode -->
                            <div style="width:240px ;padding:7px;">
                                 <asp:Label runat="server" Text="Bundle Code"></asp:Label><br />
                                <asp:TextBox runat="server" ID="PegBundleCode" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg NetWork RecordID -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Network"></asp:Label><br />
                                <asp:TextBox runat="server" ID="PegNetwork" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleAmount -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Amount"></asp:Label><br />
                                <asp:TextBox runat="server" ID="PegBundleAmount" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg Bundle Volume -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Volume"></asp:Label><br />
                                <asp:TextBox runat="server" ID="PegBundleVolume" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleAmount -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Active"></asp:Label><br />
                                
                                <asp:DropDownList runat="server" ID="dll_pegStatus" Height="25" BorderColor="LightGray" BorderWidth="1">
                                    <asp:ListItem Text="Active" Value="true"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="false"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <!--Peg  BundleAmount -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Duration"></asp:Label><br />
                                <asp:TextBox runat="server" ID="PegDuration" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button Height="27" BackColor="Teal" ForeColor="White" Text="Update Bundle Details" runat="server" OnClick="Unnamed_Click" />
                    </div>
                </asp:View>


                <%--Shows table for the Pegasus To telecom data Configurations--%>
                <asp:View runat="server" ID="P2Tmapping">
                    <asp:DataGrid runat="server" ID="TelecomConfigs" AutoGenerateColumns="False" OnItemDataBound="TelecomConfigs_ItemDataBound" CssClass="table" CellPadding="5" GridLines="None" OnItemCommand="TelecomConfigs_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Names="Bahnschrift Light" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White">
                      <AlternatingItemStyle BackColor="lightcyan" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="RecordId" HeaderText="Bundle ID">
                                <HeaderStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                                <HeaderStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Duration" HeaderText="Duration">
                                <HeaderStyle Width="10px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PegasusDataCode" HeaderText="Pegasus Code">
                                <HeaderStyle Width="10px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BundleCode" HeaderText="Bundle Code">
                                <HeaderStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BundleName" HeaderText="Bundle Name">
                                <HeaderStyle Width="13%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IsActive" HeaderText="Bundle Status">
                                <HeaderStyle Width="7%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BundleAmount" HeaderText="BundleAmount">
                                <HeaderStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Selector" HeaderText="Telecom Selector">
                                <HeaderStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn ButtonType="PushButton" HeaderText="Edit" CommandName="Edit_Bundle" Text="Edit Bundle">
                                <HeaderStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>
                        <HeaderStyle BackColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                    </asp:DataGrid>
                </asp:View>
                <asp:View runat="server" ID="MoreInfoClient">
                    <div align="center">
                        <div style="display:flex; max-width:600px; flex-wrap:wrap;" align="left">

                            <!--Peg Config RecordID -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Bundle Id"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsBundleID" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            
                            <!--Peg  BundleCode -->
                            <div style="width:240px ;padding:7px;">
                                 <asp:Label runat="server" Text="Bundle Code"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsBundleCode" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg DataCode -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="PegasusDataCode"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsDataCode" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg NetWork RecordID -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Network"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsNetwork" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg NetWork RecordID -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Bundle Name"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsBundleName" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleAmount -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Amount"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsAmount" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleAmount -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Active"></asp:Label><br />
                                <asp:DropDownList runat="server" ID="dll_smsActive" Height="25" BorderColor="LightGray" BorderWidth="1">
                                    <asp:ListItem Text="Active" Value="true"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="false"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <!--Peg  BundleAmount -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Duration"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsDuration" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleSelector -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Selector"></asp:Label><br />
                                <asp:TextBox runat="server" ID="SmsSelector" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            
                        </div>
                        <asp:Button Height="27" BackColor="Teal" ForeColor="White" Text="Update Bundle Details" runat="server" OnClick="Unnamed_Click1" />
                    </div>
                </asp:View>

                <asp:View runat="server" ID="ViewCreator">
                    <div align="center">
                        <div style="display:flex; max-width:600px; flex-wrap:wrap;" align="left">
                            
                            <!--Peg  BundleCode -->
                            <div style="width:240px ;padding:7px;">
                                 <asp:Label runat="server" Text="TelecomBundle Code"></asp:Label><br />
                                <asp:TextBox runat="server" ID="TextBox2" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg DataCode -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="PegasusDataCode"></asp:Label><br />
                                <asp:TextBox runat="server" ID="TextBox3" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg DataCode -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Bundle name"></asp:Label><br />
                                <asp:TextBox runat="server" ID="TextBox4" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg NetWork RecordID -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Network"></asp:Label><br />
                                <asp:DropDownList runat="server" ID="DropDownList2" Height="25" BorderColor="LightGray" BorderWidth="1">
                                    <asp:ListItem Text="AIRTEL" Value="AIRTEL"></asp:ListItem>
                                    <asp:ListItem Text="MTN" Value="MTN"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <!--Peg  BundleAmount -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Amount"></asp:Label><br />
                                <asp:TextBox runat="server" ID="TextBox6" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleDuration -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Duration"></asp:Label><br />
                                <asp:TextBox runat="server" ID="TextBox7" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleDuration -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Bundle Volume"></asp:Label><br />
                                <asp:TextBox runat="server" ID="TextBox1" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            <!--Peg  BundleSelector -->
                            <div style="width:240px;padding:7px;">
                                <asp:Label runat="server" Text="Selector"></asp:Label><br />
                                <asp:TextBox runat="server" ID="TextBox8" Height="25" BorderColor="LightGray" BorderWidth="1"></asp:TextBox>
                            </div>
                            
                        </div>
                        <asp:Button Height="27" BackColor="Teal" ForeColor="White" Text="Save Bundle Details" runat="server" OnClick="Unnamed_Click2" />
                    </div>
                </asp:View>


            </asp:MultiView>
        </asp:Panel>
    </div>
</asp:Content>

