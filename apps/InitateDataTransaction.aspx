<%@ Page Title="" Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true" CodeFile="InitateDataTransaction.aspx.cs" Inherits="InitateDataTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row row-cols-1 row-cols-md-2">
        <div class="col" align="center">
            <div class="shadow-lg" style="margin:3em; padding:10px; border-radius:15px;" align="left">
                <p class="h5 text-center">Initiate Single Activation</p><br />
                <%--<div class="row row-cols-2">
                    <div class="col">
                        <asp:Label runat="server" Text="Customer Name"></asp:Label>
                    </div>
                     <div class="col">
                        <asp:TextBox runat="server" ID="CustName" CssClass="form-control"></asp:TextBox>
                    </div>
                </div><br />--%>
                <div class="row row-cols-2">
                    <div class="col">
                        <asp:Label runat="server" Text="Telephone Number"></asp:Label>
                    </div>
                     <div class="col">
                        <asp:TextBox runat="server" ID="TelNo" CssClass="form-control"></asp:TextBox>
                    </div>
                </div><br />
                <div class="row row-cols-2">
                    <div class="col">
                        <asp:Label runat="server" Text="Vendor Code"></asp:Label>
                    </div>
                     <div class="col">
                        <asp:DropDownList ID="ddl_vendor" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div><br />
                <div class="row row-cols-2">
                    <div class="col">
                        <asp:Label runat="server" Text="Bundle Duration"></asp:Label>
                    </div>
                     <div class="col">
                        <asp:DropDownList id="Duration" Width="100%" CssClass="p-2" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change" runat="server">
                              <asp:ListItem Value="DAILY"> DAILY </asp:ListItem>
                              <asp:ListItem Value="WEEKLY"> WEEKLY </asp:ListItem>
                              <asp:ListItem Value="MONTHLY"> MONTHLY </asp:ListItem>
                              <asp:ListItem Value="3 MONTHS"> 3 MONTHS </asp:ListItem>
                              <asp:ListItem Selected="True" Value=""> --Select Duration-- </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div><br />
                <div class="row row-cols-2">
                    <div class="col">
                        <asp:Label runat="server" Text="Data Bundle"></asp:Label>
                    </div>
                     <div class="col">
                       <asp:DropDownList ID="ddl_bundleCode" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div><br />
                <div class="row row-cols-2">
                    <div class="col">
                        <asp:Label runat="server" Text="Utility"></asp:Label>
                    </div>
                     <div class="col">
                        <asp:DropDownList ID="ddl_utilityCode" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div><br />
                <asp:Button runat="server" Text="Initiate Single Payment" CssClass="customBtn rounded-pill" OnClick="DoSingleActivation" />
            </div>
            
        </div>
         <div class="col" align="center">
            <div class="shadow-lg" style="margin:3em; padding:10px;" align="left">
                <p class="h5 text-center">Initiate Bulk Payment</p><br />
                <div class="input-group mb-3">
                  <label class="input-group-text" for="inputGroupFile01">Upload</label>
                  <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                </div><br />
                <asp:Button runat="server" CssClass="customBtn rounded-pill" ID="Button4" OnClick="btn_uploadClicK" Text="Bulk Payment" />

            </div>

        </div>
        
    </div>
</asp:Content>

