<%@ Page Title="" Language="C#" MasterPageFile="~/OtherReportsmaster.master" AutoEventWireup="true" CodeFile="OtherReports.aspx.cs" Inherits="OtherReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <table style="width: 100%">
        <tr>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center; text-decoration: underline">
                REPORTING OTHERS</td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center">
                <asp:Label ID="lblWelcome" runat="server" Font-Bold="True" Text="."></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 2px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 2px; text-align: center">
                <asp:Label ID="lblUsage" runat="server" Font-Bold="True" Text="."></asp:Label></td>
        </tr>
        <tr />
    </table>
</asp:Content>

