<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GAnalyticsApplication.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    Hello World
                    <br />
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <table>
                        '<%= SampleRunReport() %>'
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>
    </form>
</body>
</html>
