<%@ Page Language="C#" MasterPageFile="~/AccountantMaster.master" AutoEventWireup="true" CodeFile="MerchantFiles.aspx.cs" Inherits="ReconcilePrepaidTransactions" Title="Untitled Page" %>
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


<table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%"> <tr>
                                    <td class="InterfaceHeaderLabel">
                                        MERCHANT FILES</td>
                                </tr>
                            </table>
            </td>
        </tr>
        <tr>
         <td style="width: 98%; height: 1px">
            </td>
        </tr>
        <tr>
        <td style="width: 98%;">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                  
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;"><table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                              <td style="vertical-align: middle; width: 35%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label >Year</label>
                                    <asp:TextBox runat="server" ID="txtYear" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="vertical-align: middle; width: 35%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>File Type</label>
                                    <asp:DropDownList ID="ddFileType" runat="server" Style="font: menu" Width="90%"  AutoPostBack="true" OnSelectedIndexChanged="ddFileType_OnSelectedIndexChanged">    
                                        <asp:ListItem Text="Chargeback File" Value="CHARGEBACK"></asp:ListItem> 
                                        <asp:ListItem Text="Settlement File" Value="SETTLEMENT"></asp:ListItem>                                                                         
                                        <asp:ListItem Text="Debit File" Value="DEBIT"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="vertical-align: middle; width: 30%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>File </label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                               </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="UPLOAD" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                    </tr>
                </table>
                </td>
        </tr>
        </table>
            <br />
        
</asp:Content>

