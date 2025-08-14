<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MTNDataReports.aspx.cs" Inherits="MTNDataReports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MTN Data Requests Report</title>
    <script  src="<%# ResolveUrl("assets/js/jquery-3.3.1.js")%>")%>"></script>
     <script  src="<%# ResolveUrl("assets/js/moment.js")%>")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/bootstrap/bootstrap.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/js/bootstrap-datetimepicker.min.js")%>"></script>
     <script  src="<%# ResolveUrl("assets/vendors/js/jquery-ui.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/js/bootstrap3-typeahead.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/js/customDataTable.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/dtables/js/jquery.dataTables.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/dataTables.buttons.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/buttons.print.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/buttons.flash.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/jszip.min.js")%>"></script>
    <script   src="<%# ResolveUrl("assets/vendors/datatables/pdfmake.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/vfs_fonts.js")%>"></script>
    <link href="scripts/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/buttons.html5.min.js")%>"></script>
    <script  src="<%# ResolveUrl("assets/vendors/datatables/dataTables.select.min.js")%>"></script>
    <style>
        body{
            padding:5px;
            margin:0px;
            width:100Vw;
        }
         .purchaseForm{
            max-width:850px;
            margin:10px;
            position: relative;
            min-height: 200px;
            border-radius: 20px;
            padding: 40px;
            box-sizing: border-box;
            background: #ecf0f3;
            box-shadow: 14px 14px 20px #cbced1, -14px -14px 20px white;
        }
        .table-container{
            overflow-x:scroll;
            width:95Vw;
        }
        .formbody{
            margin-top:20px;
            margin-bottom:30px;
        }
        .padding{
            padding:10px;
        }
        input{
            padding:7px;
        }
        .w-100{
            width:100vw;
        }
        .th{
            width:200px;
            padding:7px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <asp:Label runat="server" ID="lblmg" Text=""></asp:Label>
            <asp:Label Text="" ID="msg" runat="server"></asp:Label>
            <div class="formbody purchaseForm" style="">
                <div class="row row-cols-1 row-cols-sm-2 row-cols-3" align="left">
                    <div class="col">
                        <asp:Label Text="Start Date" runat="server"></asp:Label><br />
                        <asp:TextBox ID="startDate" CssClass="form-control" runat="server" type="date"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:Label Text="End Date" runat="server"></asp:Label><br />
                        <asp:TextBox ID="endDate" CssClass="form-control" runat="server" type="date"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:Label Text="Customer Number" runat="server"></asp:Label><br />
                        <asp:TextBox ID="telno" CssClass="form-control" runat="server" type="datet"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label Text="Status" runat="server"></asp:Label><br />
                        <asp:DropDownList CssClass="w-100 form-control"  ID="Status" runat="server">
                            <asp:ListItem Enabled="true" Text= "Select Status" Value= "-1"></asp:ListItem>
                            <asp:ListItem Text= "SUCCESS" Value="1"></asp:ListItem>
                            <asp:ListItem Text= "FAILED" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col">
                        <br />
                        <asp:Button CssClass="btn rounded-pill btn-primary" Text="Search Report" OnClick="Unnamed_Click" runat="server" />
                    </div>
                    <div class="col">
                        <br />
                        <asp:Button CssClass="btn rounded-pill btn-primary" OnClick="btnCSV_Click" Text="Download Excel" runat="server" />
                    </div>
                    <%--<div class="col">
                        <asp:TextBox ID="TextBox3" runat="server" type="datetime"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="TextBox4" runat="server" type="datetime"></asp:TextBox>
                    </div>--%>
                </div>
            </div>
        </div>
        <div class="table-container">
            <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover"
                ID="dataGridResults" AllowPaging="true"
                PageSize="10" CellPadding="4" CellSpacing="2">
                <AlternatingRowStyle BackColor="#BFE4FF" />
                <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>