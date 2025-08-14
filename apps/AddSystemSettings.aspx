<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddSystemSettings.aspx.cs" Inherits="AddSystemSettings" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <%--   <link href="assets/NewBs/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="scripts/bootstrap.min.css" rel="stylesheet" />
    <style>
        .padding{
            padding:20px;
        }
        .width-100{
            width:100px;
        }
        .width-300{
            width:300px;
        }
    </style>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="center" class="padding">
        <%--<div class="row padding" style="max-width:800px;">
            <div class="col">
                <asp:DropDownList ID="ddlUtility" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlUtility_SelectedIndexChanged" OnDataBound="ddlUtility_DataBound">
                 </asp:DropDownList>
            </div>
            <div class="col">
                <asp:Button runat="server" Font-Bold="true" OnClick="btnGetCredentials_Click" CssClass="btn bg-primary w-100 text-light rounded-pill" Text="Select utility" />
            </div>
        </div>--%>

         <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <%--<asp:View ID="View1" runat="server">
                <h6>Add/Edit Utility Bank Credentials </h6>
                <div class="row" align="left" style="max-width:800px;">
                    <div class="col padding">
                        <asp:Label Text="Utility Vendor" runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" Text="Pegasus" Enabled="false" CssClass="form-control" ID="vendor"></asp:TextBox><br />
                        <asp:Label Text="Utility" runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="utility"></asp:TextBox><br />
                        <asp:Label Text="Utility Username" runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="utilName"></asp:TextBox><br />
                        <asp:Label Text="Utiltity Password"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" TextMode="Password"  CssClass="form-control" ID="utilPswd"></asp:TextBox><br />
            
                    </div>
                    <div class="col padding">
                        <asp:Label Text="Bank Name"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="BankName"></asp:TextBox><br />
                        <asp:Label Text="SecretKey"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" placeholder="eg(18)" CssClass="form-control" ID="secretkey"></asp:TextBox><br />
                        <asp:Label Text="Key" runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="valKey"></asp:TextBox> <br />
                        <label class="form-label" for="customFile">Add Certificate</label>
                        <asp:FileUpload runat="server" ID="Cert" CssClass="form-control" />
                    </div>
                </div>
                <asp:Button runat="server" Font-Bold="true" CssClass="btn bg-primary text-light w-300 rounded-pill" OnClick="btnOK_Click" Text="Create Setting" />
            </asp:View>--%>
             <asp:View ID="View1" runat="server">
                <h6>Add/Edit Utility Bank Credentials </h6>
                <div class="row" align="left" style="max-width:800px;">
                    <div class="col padding">
                        <asp:Label Text="Bank Vendor*" runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" Text="Pegasus" Enabled="false" CssClass="form-control" ID="vendor"></asp:TextBox><br />
                        <asp:Label Text="Utility*" runat="server"></asp:Label><br />
                        <%--<asp:TextBox runat="server" CssClass="form-control" ID="utility"></asp:TextBox><br />--%>
                        <asp:DropDownList ID="ddlUtility" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlUtility_SelectedIndexChanged" OnDataBound="ddlUtility_DataBound">
                           </asp:DropDownList><br />
                        <asp:Label Text="Bank Name*" runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="BankName"></asp:TextBox><br />
                        <asp:Label Text="Bank Username*"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="BankUName"></asp:TextBox><br />
                        <asp:Label Text="Bank Url/IP*"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="url"></asp:TextBox><br />


            
                    </div>
                    <div class="col padding">
                        <asp:Label Text="Password*"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" TextMode="Password"  CssClass="form-control" ID="utilPswd"></asp:TextBox><br />
                        <asp:Label Text="Secret Key"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="secretkey"></asp:TextBox><br />
                        <asp:Label Text="Account Id" runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="centerVal"></asp:TextBox> <br />
                        <asp:Label Text="Certificate Key"  runat="server"></asp:Label><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="certKey"></asp:TextBox><br />
                        <label class="form-label" for="customFile">Add Certificate</label>
                        <asp:FileUpload runat="server" ID="Cert" CssClass="form-control" />
                    </div>
                </div>
                <asp:Button runat="server" Font-Bold="true" CssClass="btn bg-primary text-light w-300 rounded-pill" OnClick="btnOK_Click" Text="Create Setting" />
            </asp:View>
             <asp:View  ID="View2" >

             </asp:View>
         </asp:MultiView>
     </div>
    
</asp:Content>

