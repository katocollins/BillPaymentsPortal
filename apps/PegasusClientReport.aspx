<%@ Page Title="Pegasus Client Report" Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true" CodeFile="PegasusClientReport.aspx.cs" Inherits="PegasusClientReport" %>


<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 2px">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                         <tr>
                                           <td style="vertical-align: middle; width: 98%; height: 2px; text-align: center;">
                                              <% 
                                                    string IsError = Session["IsError"] as string;
                                                    if (IsError == null)
                                                    {
                                                        Response.Write("<div>");

                                                    }
                                                    else if (IsError == "True")
                                                    {
                                                        Response.Write("<div class=\"alert alert-danger\">");

                                                    }
                                                    else
                                                    {
                                                        Response.Write("<div class=\"alert alert-success\">");
                                                    } 
                                                %>
                                                <strong>
                                                <asp:Label ID="lblmsg" runat="server"></asp:Label></strong>
                                                <%Response.Write("</div>"); %>
                                                <asp:Label ID="lblCount" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                      </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager><table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                        style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                                                text-align: center">
                            &nbsp;&nbsp;
                            CLIENT REPORT</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #617da6; width: 95%; height: 95px;">
                    <tr>
<%--                      <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 21%; height: 18px;
                            text-align: center">
                            Vendor</td>--%>
<%--                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 26%; height: 18px;
                            text-align: left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            Client Choice</td>--%>
<%--                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 20%; height: 18px;
                            text-align: center">
                            Bank</td>--%>
<%--                        <td class="InterfaceHeaderLabel2" colspan="1" style="vertical-align: middle; width: 179px;
                            height: 18px; text-align: center">
                            Transaction Type</td>--%>
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 26%; height: 18px;
                            text-align: center">
                        </td>
                              <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 21%; height: 18px;
                            text-align: center">Category</td>
                              <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 21%; height: 18px;
                            text-align: center">Status</td>
<%--                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 21%; height: 18px;
                            text-align: center">TranType</td>--%>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 26%; height: 18px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                             <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                          <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                          <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                    </tr>
                    <tr>
                       <%-- <td style="vertical-align: middle; width: 21%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;
                            <asp:TextBox ID="txtVendor" runat="server" style="font: menu" Width="90%"></asp:TextBox></td>--%>

<%--                        <td style="border-color: #617da6; border-width: 1px; vertical-align: middle; width: 26%; height: 35px; text-align: center; ">
                            <asp:DropDownList ID="ddClientChoice" runat="server" CssClass="InterfaceRadioButtonlist"
                                Width="90%" style="font: menu; margin-left: 0px;" Height="33px">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                            </asp:DropDownList></td>--%>
                                <td style="border-color: #617da6; border-width: 1px; vertical-align: middle; width: 26%; height: 35px; text-align: center; ">
                            </td>

                            <td style="border-color: #617da6; border-width: 1px; vertical-align: middle; width: 21%; height: 35px; text-align: center; ">
                            &nbsp;<asp:DropDownList ID="ddCategory" runat="server" CssClass="InterfaceRadioButtonlist"
                                Width="92%" style="font: menu" Height="34px">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                            </asp:DropDownList></td>

                                  <td style="border-color: #617da6; border-width: 1px; vertical-align: middle; width: 26%; height: 35px; text-align: center; ">
                            <asp:DropDownList ID="ddStatus" runat="server" CssClass="InterfaceRadioButtonlist"
                                Width="79%" style="font: menu; margin-left: 0px;" Height="34px">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                            </asp:DropDownList></td>

<%--                                 <td style="border-color: #617da6; border-width: 1px; vertical-align: middle; width: 21%; height: 35px; text-align: center; ">
                            <asp:DropDownList ID="ddTranType" runat="server" CssClass="InterfaceRadioButtonlist"
                                Width="82%" style="font: menu; margin-left: 0px;" Height="33px">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                            </asp:DropDownList></td>--%>
                      
                        <td style="border-color: #617da6; border-width: 1px; vertical-align: middle; width: 26%; height: 35px; text-align: center; ">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="74px" style="font: menu" />
                            </td>

<%--                      <td style="vertical-align: middle; width: 21%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="ddTelecom" runat="server" CssClass="InterfaceDropdownList"
                                Width="100%" style="font: menu">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                            </asp:DropDownList></td>--%>
<%--                             <td style="vertical-align: middle; width: 21%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="ddTranType" runat="server" CssClass="InterfaceDropdownList"
                                Width="100%" style="font: menu">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                            </asp:DropDownList></td>--%>
                        <td style="border-color: #617da6; border-width: 1px; vertical-align: middle; width: 17%; height: 35px; text-align: center; ">
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View3" runat="server">
                        <table>
                           <tr>
                                <td style="width: 20%; height: 2px">
                                    <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                                        border-left: #617da6 1px solid; width: 50%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:RadioButton ID="rdPdf" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                    Text="PDF" /></td>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:RadioButton ID="rdExcel" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                    Text="EXCEL" /></td>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnConvert_Click"
                                                    Style="font: menu" Text="Convert" Width="85px" /></td>
                                        </tr>
                                    </table>
                                </td>
                           </tr>
                       </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover" ID="dataGridResults" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="30" CellPadding="4" CellSpacing="2"  OnRowCommand="dataGridResults_RowCommand">
                                            <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" HorizontalAlign="Left"/>
                                            <RowStyle HorizontalAlign ="left" />
                                           <%-- <Columns>
                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="editing" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>--%>
                                            <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                        </div>
                    </asp:View>
                    <asp:View ID="AccountDetailsView" runat="server">
                                <div class="container">
                                    <div class="row">
                                        <div class="well col-lg-10 col-xs-10 col-sm-10 col-md-6 col-xs-offset-1 col-sm-offset-1 col-md-offset-3">
                                            <div class="row">
                                                <div class="col-xs-6 col-sm-6 col-md-6">
                                                        Name: <strong>
                                                            <asp:Label ID="lblName" runat="server">
                                                            </asp:Label></strong>
                                                        <br />
                                                        Phone:
                                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                        <br />
                                                        Location:
                                                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                        <br />
                                                        Type:
                                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <abbr title="Filer">
                                                            Filer:</abbr>
                                                        <asp:Label ID="lblFiler" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-xs-6 col-sm-6 col-md-6 text-right">
                                                    <p>Created On
                                                        <asp:Label ID="lblDate" runat="server">
                        <em>Date: </em></asp:Label>
                                                    </p>
                                                    <p>
                                                        <em>Beneficiary ID #:
                                                            <asp:Label ID="lblbeneficiaryId" runat="server"></asp:Label></em>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="text-center">
                                                    <label>
                                                            Reason</label>
                                                        <asp:TextBox ID="txtReason" runat="server" style="width:940px" CssClass="form-control" />
                                                        <p class="help-block">
                                                            Why are you deleting this beneficiary?</p>
                                                </div>
                                                <div class="row" id="buttons" runat="server" visible="true">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-primary" Text="Confirm"
                                                            OnClick="btnConfirm_Click" />
                                                       <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-primary" Text="Back"
                                                            OnClick="btnReturn_Click" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     
                                </div>
                            </asp:View>
                </asp:MultiView></td>
        </tr>
    </table>
    <br />
</asp:Content>

