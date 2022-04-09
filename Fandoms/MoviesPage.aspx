<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoviesPage.aspx.cs" Inherits="Fandoms.MoviesPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Movies Page (Temp)<br />
            <br />
            <asp:Label ID="lblSelectFandom" runat="server" Text="Please Select a Fandom: " Visible="False"></asp:Label>
            <asp:DropDownList ID="drpFandoms" runat="server" Visible="False" Width="604px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnAddMovie" runat="server" CssClass="btn-primary" Text="Add Movie" />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="337px" Width="927px">
                <Columns>
                    <asp:BoundField HeaderText="Movie ID" />
                    <asp:BoundField HeaderText="Movie Name" />
                    <asp:BoundField HeaderText="Movie Info" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEditMovie" runat="server" CssClass="btn-primary" Text="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Button ID="btnEditMovie" runat="server" CssClass="btn-primary" Text="Edit" />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
