<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Fandoms.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Panel ID="pnlTitle" runat="server" HorizontalAlign="Center">
        <h1>
            <asp:Label ID="lblWelcome" runat="server" Text="Welcome to InfoFandoms!" Font-Bold="True" Font-Italic="True" Font-Size="XX-Large"></asp:Label>
        </h1>
    </asp:Panel>


    <asp:Panel ID="pnlWelcome" runat="server" HorizontalAlign="Center">
        <h2>
            <asp:Label ID="lblWWW" runat="server" Text="Who we are. What we do. Why we do it." Font-Bold="True"></asp:Label>
        </h2>
        <txt>
            We are a group of college students that have been assigned the task of creating
                        a client-server database. So why no have some fun with it?
                        We chose to create a database that allows us to input fandoms
                        and the movies associated with those fandoms! Follow the flow to add your own!</txt>

    </asp:Panel>

    <asp:Panel ID="pnlButtons" runat="server" HorizontalAlign="Center">
        <asp:Button ID="btnAddFandom" runat="server" Text="Add/Edit Fandom" CssClass="btn btn-primary" OnClick="Button1_Click" />
        &nbsp;
        <asp:Button ID="btnAddMovie" runat="server" Text="Add/Edit Movie" CssClass="btn btn-primary" OnClick="btnAddMovie_Click" />
    </asp:Panel>





</asp:Content>


